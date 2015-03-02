using System;
using Estimote;
using TestApp.Interfaces;
using Xamarin.Forms;

namespace TestApp.iOS
{
	public class Beacon : BeaconBase
	{
		public Beacon(Guid proximityId, int major, int minor)
			: base(proximityId, major, minor)
		{
		}

		internal void SetColor(BeaconColor color)
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
	}
}

