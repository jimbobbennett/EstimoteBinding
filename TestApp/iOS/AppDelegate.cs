using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;
using EstimoteBinding;
using MonoTouch.CoreLocation;

namespace TestApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		ESTBeaconManager _beaconManager;
		string _uuid = "b9407f30-f5f8-466e-aff9-25556b57fe6d";

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();

			InvokeInBackground(() => 
				{
					_beaconManager = new ESTBeaconManager();

					var uuid = new NSUuid (_uuid);
					var region = new ESTBeaconRegion (uuid, "MyRegion", false);

					_beaconManager.ReturnAllRangedBeaconsAtOnce = true;
					_beaconManager.AvoidUnknownStateBeacons = true;

					_beaconManager.RequestAlwaysAuthorization();

					_beaconManager.DidDiscoverBeacons += (sender, e) => 
					{
						if (e.Beacons == null || e.Beacons.Length == 0)
						{
							Console.WriteLine("Discovered no beacons");
						}
						else
						{
							Console.WriteLine("Discovered {0} beacons", e.Beacons.Length);
						}
					};

					_beaconManager.StartEstimoteBeaconsDiscoveryForRegion(region);

				});

			return true;
		}
	}
}

