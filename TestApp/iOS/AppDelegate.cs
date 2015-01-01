using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using EstimoteBinding;

namespace TestApp.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		ESTBeaconManager _beaconManager;
		int _beaconCount = -1;
		string _uuid = "b9407f30-f5f8-466e-aff9-25556b57fe6d";

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

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
						var beaconCount = e.Beacons == null ? 0 : e.Beacons.Length;
						if (beaconCount != _beaconCount)
						{
							Console.WriteLine("Discovered {0} beacons", e.Beacons.Length);
							_beaconCount = beaconCount;
						}
					};

					_beaconManager.StartEstimoteBeaconsDiscoveryForRegion(region);

				});

			return base.FinishedLaunching (app, options);
		}
	}
}

