using System;
using Xamarin.Forms;

namespace TestApp.Interfaces
{
	public class BeaconBase : ViewModelBase, IBeacon
	{
		private double _distance;
		private Color _color = Color.White;

		protected BeaconBase(Guid proximityId, int major, int minor)
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

		#endregion

		public override string ToString ()
		{
			return string.Format ("[Beacon: Major={0}, Minor={1}, ProximityUUID={2}, Distance={3}]", 
				Major, Minor, ProximityUUID, Distance);
		}
	}
}

