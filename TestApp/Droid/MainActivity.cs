using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using EstimoteSdk;
using Xamarin.Forms;
using TestApp.Interfaces;
using Android.Bluetooth;

namespace TestApp.Droid
{
	[Activity (Label = "TestApp.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		const int REQUEST_ENABLE_BLUETOOTH = 123321;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			DependencyService.Get<IBeaconDiscovery>();

			LoadApplication (new App ());
		}

		protected override void OnStart ()
		{
			base.OnStart();

			var beaconDiscovery = (BeaconDiscovery)DependencyService.Get<IBeaconDiscovery>();

			if (!beaconDiscovery.IsBluetoothEnabled)
			{
				Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
				StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BLUETOOTH);
			}
			else
			{
				LookForBeacons();
			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == REQUEST_ENABLE_BLUETOOTH)
			{
				if (resultCode == Result.Ok)
				{
					ActionBar.Subtitle = "Scanning...";
					LookForBeacons();
				}
				else
				{
					Toast.MakeText(this, "Bluetooth not enabled.", ToastLength.Long).Show();
					ActionBar.Subtitle = "Bluetooth not enabled.";
				}
			}

			base.OnActivityResult(requestCode, resultCode, data);
		}

		void LookForBeacons()
		{
			var beaconDiscovery = (BeaconDiscovery)DependencyService.Get<IBeaconDiscovery>();

			beaconDiscovery.SetActivity(this);
		}
	}
}

