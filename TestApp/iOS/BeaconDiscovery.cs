using System;
using TestApp.iOS;
using TestApp.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Foundation;
using System.Threading.Tasks;
using System.Linq;
using Estimote;

[assembly: Xamarin.Forms.Dependency (typeof (BeaconDiscovery))]

namespace TestApp.iOS
{
	public class BeaconDiscovery : IBeaconDiscovery
	{
		private readonly ObservableCollection<IBeacon> _beacons = new ObservableCollection<IBeacon>();
        private readonly BeaconManager _beaconManager = new BeaconManager();
		private readonly object _syncObj = new object();
		private readonly object _rangeSyncObj = new object();
		private bool _isInitialized;
		private int _regionId = 1;

        private Dictionary<Guid, BeaconRegion> _subscribedRegions = new Dictionary<Guid, BeaconRegion>();
		private readonly Dictionary<Tuple<Guid, int, int>, IBeacon> _rangedBeacons = new Dictionary<Tuple<Guid, int, int>, IBeacon>();

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

					_beaconManager.DiscoveredBeacons += DidDiscoverBeacons;
					_beaconManager.RangedBeacons += DidRangeBeacons;

					_isInitialized = true;
				}

				foreach(var guid in uuids.Where(u => !_subscribedRegions.ContainsKey(u)))
				{
					var nsuuid = new NSUuid(guid.ToString());
					var region = new BeaconRegion (nsuuid, "MyRegion" + _regionId, false);

					_beaconManager.StartEstimoteBeaconsDiscovery(region);
					_beaconManager.StartRangingBeacons(region);

					_regionId++;
				}
			}
		}

		private void DidRangeBeacons (object sender, RangedBeaconsArgsEventArgs args)
		{
			lock (_rangeSyncObj)
			{
				foreach(var beacon in args.Beacons)
				{
					var guid = Guid.Parse(beacon.ProximityUUID.AsString());
					var major = Convert.ToInt32(beacon.Major);
					var minor = Convert.ToInt32(beacon.Minor);
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

		private void DidDiscoverBeacons(object sender, DiscoveredBeaconsArgsEventArgs args)
		{
		}

		public ReadOnlyObservableCollection<IBeacon> Beacons { get; private set; }

		#endregion
	}
}

