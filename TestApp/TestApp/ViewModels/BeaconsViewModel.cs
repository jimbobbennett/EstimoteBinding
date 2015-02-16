using System;
using Xamarin.Forms;
using TestApp.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TestApp
{
	public class BeaconsViewModel
	{
		IBeaconDiscovery _beaconDiscovery;
		ObservableCollection<BeaconViewModel> _beacons = new ObservableCollection<BeaconViewModel>();

		public BeaconsViewModel ()
		{
			_beaconDiscovery = DependencyService.Get<IBeaconDiscovery>();

			Beacons = new ReadOnlyObservableCollection<BeaconViewModel>(_beacons);

			((INotifyCollectionChanged)_beaconDiscovery.Beacons).CollectionChanged += BeaconsCollectionChanged;
			LoadBeacons();
		}

		public void BeaconsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			LoadBeacons ();

		}

		private void LoadBeacons ()
		{
			_beacons.Clear ();
			foreach (var beacon in _beaconDiscovery.Beacons) {
				_beacons.Add (new BeaconViewModel (beacon));
			}
		}

		public ReadOnlyObservableCollection<BeaconViewModel> Beacons { get; private set; }
	}
}

