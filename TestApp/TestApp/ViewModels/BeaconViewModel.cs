using System;
using TestApp.Interfaces;
using System.ComponentModel;

namespace TestApp
{
	public class BeaconViewModel : ViewModelBase
	{
		IBeacon _beacon;

		public BeaconViewModel (IBeacon beacon)
		{
			this._beacon = beacon;
			_beacon.PropertyChanged += BeaconPropertyChanged;
		}

		private void BeaconPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Distance")
				OnPropertyChanged("Details");
		}

		public string Title { get { return _beacon.ProximityUUID.ToString(); } }

		public string Details 
		{ 
			get 
			{ 
				return string.Format("Major: {0}, Minor: {1}, Distance: {2:0.00}", 
					_beacon.Major, _beacon.Minor, _beacon.Distance);
			} 
		}
	}
}

