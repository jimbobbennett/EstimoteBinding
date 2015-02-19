using System;
using TestApp.Interfaces;
using System.ComponentModel;
using Xamarin.Forms;

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

		public string Title { get { return _beacon.ProximityUUID.ToString("N"); } }

		public string Details 
		{ 
			get 
			{ 
				return string.Format("{0}.{1}, Distance: {2:0.00}m", 
					_beacon.Major, _beacon.Minor, _beacon.Distance);
			} 
		}

		public Color Color { get { return _beacon.Color.MultiplyAlpha(0.5); } }
	}
}

