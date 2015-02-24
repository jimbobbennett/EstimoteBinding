using System;
using Estimote;
using TestApp.Interfaces;
using Xamarin.Forms;

namespace TestApp.iOS
{
	public class Beacon : ViewModelBase, IBeacon
	{
		private double _distance;
		private Color _color;

		public Beacon(Guid proximityId, int major, int minor)
		{
			ProximityUUID = proximityId;
			Major = major;
			Minor = minor;
		}

		#region IBeacon implementation

		public int Major { get ; private set; }
		public int Minor { get ; private set; }
		public Guid ProximityUUID { get ; private set; }

		public double Distance 
		{
			get { return _distance; }
			set { SetProperty(ref _distance, value); }
		}

		public Color Color
		{
			get { return _color; }
			set { SetProperty(ref _color, value); }
		}

		public void SetColor(BeaconColor color)
		{
			switch (color)
			{
                case BeaconColor.Mint:
					Color = Color.FromHex("#9FCDAE");
					break;
                case BeaconColor.Blueberry:
					Color = Color.FromHex("#2E3192");
					break;
                case BeaconColor.Ice:
					Color = Color.FromHex("#6ECEF5");
					break;
                case BeaconColor.White:
					Color = Color.White;
					break;
				default:
					Color = Color.Transparent;
					break;
			}
		}

		#endregion

		public override string ToString ()
		{
			return string.Format ("[Beacon: Major={0}, Minor={1}, ProximityUUID={2}, Distance={3}]", 
				Major, Minor, ProximityUUID, Distance);
		}
	}
}

