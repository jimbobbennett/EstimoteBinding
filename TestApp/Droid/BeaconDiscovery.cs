using System;
using TestApp.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TestApp.Droid;
using EstimoteSdk;
using Android.Content;
using Java.Util.Concurrent;
using Android.App;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconDiscovery))]

namespace TestApp.Droid
{
	public class BeaconDiscovery : Java.Lang.Object, IBeaconDiscovery, BeaconManager.IServiceReadyCallback
	{
		private readonly ObservableCollection<IBeacon> _beacons = new ObservableCollection<IBeacon>();
		private BeaconManager _beaconManager;
		private readonly object _syncObj = new object();
		private readonly object _rangeSyncObj = new object();
		private bool _isInitialized;
		private List<Guid> _initialGuids = new List<Guid>();
		private int _regionId = 1;
		private bool _isReady = false;

		private Dictionary<Guid, Region> _subscribedRegions = new Dictionary<Guid, Region>();
		private readonly Dictionary<Tuple<Guid, int, int>, TestApp.Droid.Beacon> _rangedBeacons = new Dictionary<Tuple<Guid, int, int>, TestApp.Droid.Beacon>();

		public BeaconDiscovery ()
		{
			Beacons = new ReadOnlyObservableCollection<IBeacon>(_beacons);
		}

		public void SetActivity(MainActivity activity)
		{
			_beaconManager = new BeaconManager(activity);

			_beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);
			_beaconManager.Ranging += (s, e) => 
			{
				Console.WriteLine("Ranging");

				lock (_rangeSyncObj)
				{
					foreach(var beacon in e.Beacons)
					{
						var key = Tuple.Create(Guid.Parse(beacon.ProximityUUID), beacon.Major, beacon.Minor);
						TestApp.Droid.Beacon foundBeacon;

						if (!_rangedBeacons.TryGetValue(key, out foundBeacon))
						{
							foundBeacon = new TestApp.Droid.Beacon(key.Item1, key.Item2, key.Item3);
							_rangedBeacons.Add(key, foundBeacon);
							_beacons.Add(foundBeacon);
							OnBeaconDiscovered(foundBeacon);
						}

						foundBeacon.SetDistance(beacon.MeasuredPower, beacon.Rssi);
					}
				}
			};

			_beaconManager.EnteredRegion += (sender, e) => 
			{
				Console.WriteLine("Entered Region");
			};

			_beaconManager.ExitedRegion += (sender, e) => 
			{        
				Console.WriteLine("Exited Region");
			};

			_beaconManager.Connect(this);
		}

		public bool IsBluetoothEnabled { get { return _beaconManager != null && _beaconManager.IsBluetoothEnabled; } }

		#region IBeaconDiscovery implementation

		public event EventHandler<EventArgs<IBeacon>> BeaconDiscovered;

		private void OnBeaconDiscovered(IBeacon beacon)
		{
			var handler = BeaconDiscovered;
			if (handler != null) handler(this, new EventArgs<IBeacon>(beacon));
		}

		internal void Initialise()
		{
			lock (_syncObj)
			{
				if (!_isInitialized && _beaconManager != null && _beaconManager.IsBluetoothEnabled && _isReady)
				{
					_isInitialized = true;
					Task.Factory.StartNew(() => RangeAndDiscoverBeacons(_initialGuids));
				}
			}
		}

		public void StartLookingForBeacons (IEnumerable<Guid> uuids)
		{
			lock (_syncObj)
			{
				if (!_isInitialized && _beaconManager != null && _beaconManager.IsBluetoothEnabled && _isReady)
				{
					Task.Factory.StartNew(() => RangeAndDiscoverBeacons(uuids));
				}
				else
				{
					_initialGuids.AddRange(uuids);
				}
			}
		}

		private void RangeAndDiscoverBeacons(IEnumerable<Guid> uuids)
		{
			lock (_syncObj)
			{
				if (!_isInitialized)
				{
					//_beaconManager.ReturnAllRangedBeaconsAtOnce = true;
					//_beaconManager.AvoidUnknownStateBeacons = true;
					//_beaconManager.RequestAlwaysAuthorization();

					//_beaconManager.DiscoveredBeacons += DidDiscoverBeacons;
					//_beaconManager.RangedBeacons += DidRangeBeacons;

					_isInitialized = true;
				}

				foreach(var guid in uuids.Where(u => !_subscribedRegions.ContainsKey(u)))
				{
					//var nsuuid = new NSUuid(guid.ToString());
					var region = new Region ("MyRegion" + _regionId, guid.ToString(), null, null);

					//_beaconManager.StartEstimoteBeaconsDiscovery(region);
					_beaconManager.StartRanging(region);

					_regionId++;
				}
			}
		}

//		private void DidRangeBeacons (object sender, RangedBeaconsArgsEventArgs args)
//		{
//			lock (_rangeSyncObj)
//			{
//				foreach(var beacon in args.Beacons)
//				{
//					var guid = Guid.Parse(beacon.ProximityUUID.AsString());
//					var major = Convert.ToInt32(beacon.Major);
//					var minor = Convert.ToInt32(beacon.Minor);
//					var distance = beacon.Distance.DoubleValue;
//
//					var key = Tuple.Create(guid, major, minor);
//
//					IBeacon b;
//
//					if (!_rangedBeacons.TryGetValue(key, out b))
//					{
//						b = new Beacon(guid, major, minor);
//						((Beacon)b).SetColor(beacon.Color);
//
//						_rangedBeacons.Add(key, b);
//						_beacons.Add(b);
//
//						Console.WriteLine("Ranged beacon " + beacon.ToString());
//					}
//
//					((Beacon)b).Distance = distance;
//				}
//			}
//		}
//
//		private void DidDiscoverBeacons(object sender, DiscoveredBeaconsArgsEventArgs args)
//		{
//		}

		public ReadOnlyObservableCollection<IBeacon> Beacons { get; private set; }

		#endregion

		public void OnServiceReady ()
		{
			_isReady = true;
			Initialise();
		}
	}
}

