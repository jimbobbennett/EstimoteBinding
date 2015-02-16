using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TestApp.Interfaces
{
	public interface IBeaconDiscovery
	{
		ReadOnlyObservableCollection<IBeacon> Beacons { get; }

		void StartLookingForBeacons(IEnumerable<Guid> uuids);

		event EventHandler<EventArgs<IBeacon>> BeaconDiscovered;
	}
}

