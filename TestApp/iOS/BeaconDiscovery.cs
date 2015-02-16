using System;
using TestApp.iOS;
using TestApp.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using EstimoteBinding;
using Foundation;
using System.Threading.Tasks;
using System.Linq;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconDiscovery))]

namespace TestApp.iOS
{
	public class BeaconDiscovery : IBeaconDiscovery
	{
		private ObservableCollection<IBeacon> _beacons = new ObservableCollection<IBeacon>();
		private ESTBeaconManager _beaconManager = new ESTBeaconManager();
		private readonly object _syncObj = new object();
		private readonly object _rangeSyncObj = new object();
		private bool _isInitialized;
		private int _regionId = 1;

		private Dictionary<Guid, ESTBeaconRegion> _subscribedRegions = new Dictionary<Guid, ESTBeaconRegion>();
		private Dictionary<Tuple<Guid, int, int>, IBeacon> _rangedBeacons = new Dictionary<Tuple<Guid, int, int>, IBeacon>();

		public BeaconDiscovery ()
		{
			Beacons = new ReadOnlyObservableCollection<IBeacon>(_beacons);
		}

		#region IBeaconDiscovery implementation

		public event EventHandler<EventArgs<IBeacon>> BeaconDiscovered;

		private void OnBeaconDiscovered(IBeacon beacon)
		{
			var handler = BeaconDiscovered;
			if (handler != null) handler(this, new EventArgs<IBeacon>(beacon));
		}

		public void StartLookingForBeacons (IEnumerable<Guid> uuids)
		{
			Task.Factory.StartNew(() => RangeAndDiscoverBeacons(uuids));
		}

		private void RangeAndDiscoverBeacons(IEnumerable<Guid> uuids)
		{
			lock (_syncObj)
			{
				if (!_isInitialized)
				{
					_beaconManager.ReturnAllRangedBeaconsAtOnce = true;
					_beaconManager.AvoidUnknownStateBeacons = true;
					_beaconManager.RequestAlwaysAuthorization();

					_beaconManager.DidDiscoverBeacons += DidDiscoverBeacons;
					_beaconManager.DidRangeBeacons += DidRangeBeacons;

					_isInitialized = true;
				}

				foreach(var guid in uuids.Where(u => !_subscribedRegions.ContainsKey(u)))
				{
					var nsuuid = new NSUuid(guid.ToString());
					var region = new ESTBeaconRegion (nsuuid, "MyRegion" + _regionId.ToString(), false);

					_beaconManager.StartEstimoteBeaconsDiscoveryForRegion(region);
					_beaconManager.StartRangingBeaconsInRegion(region);

					_regionId++;
				}
			}
		}

		private void DidRangeBeacons (object sender, DidRangeBeaconsEventArgs args)
		{
			lock (_rangeSyncObj)
			{
				foreach(var beacon in args.Beacons.OfType<ESTBeacon>())
				{
					var guid = Guid.Parse(beacon.ProximityUUID.AsString());
					var major = beacon.Major.Int32Value;
					var minor = beacon.Minor.Int32Value;
					var distance = beacon.Distance.DoubleValue;

					var key = Tuple.Create(guid, major, minor);

					IBeacon b;

					if (!_rangedBeacons.TryGetValue(key, out b))
					{
						b = new Beacon(guid, major, minor);
						((Beacon)b).SetColor(beacon.Color);

						_rangedBeacons.Add(key, b);
						_beacons.Add(b);

						Console.WriteLine("Ranged beacon " + beacon.ToString());
					}

					((Beacon)b).Distance = distance;
				}
			}
		}

		private void DidDiscoverBeacons(object sender, DidDiscoverBeaconsEventArgs args)
		{
		}

		public ReadOnlyObservableCollection<IBeacon> Beacons { get; private set; }

		#endregion
	}
}

