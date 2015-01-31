using System;
using System.Drawing;

using ObjCRuntime;
using Foundation;
using UIKit;

using CoreLocation;
using CoreBluetooth;

namespace EstimoteBinding
{

	// @interface ESTFirmwareInfoVO : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTFirmwareInfoVO {

		// @property (nonatomic, strong) NSString * hardwareVersion;
		[Export ("hardwareVersion", ArgumentSemantic.Retain)]
		string HardwareVersion { get; set; }

		// @property (nonatomic, strong) NSString * firmwareVersion;
		[Export ("firmwareVersion", ArgumentSemantic.Retain)]
		string FirmwareVersion { get; set; }

		// @property (nonatomic, strong) NSString * changelog;
		[Export ("changelog", ArgumentSemantic.Retain)]
		string Changelog { get; set; }

		// @property (assign, nonatomic) BOOL isUpdateAvailable;
		[Export ("isUpdateAvailable", ArgumentSemantic.UnsafeUnretained)]
		bool IsUpdateAvailable { get; set; }
	}

	// @interface ESTDefinitions : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTDefinitions {

	}

	// @interface ESTBeaconDefinitions : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTBeaconDefinitions {

	}

	// @interface ESTBeaconVO : NSObject <NSCoding>
	[BaseType (typeof (NSObject))]
	interface ESTBeaconVO {

		// -(instancetype)initWithData:(NSDictionary *)data;
		[Export ("initWithData:")]
		IntPtr Constructor (NSDictionary data);

		// @property (nonatomic, strong) NSString * UUID;
		[Export ("UUID", ArgumentSemantic.Retain)]
		string UUID { get; set; }

		// @property (nonatomic, strong) NSNumber * major;
		[Export ("major", ArgumentSemantic.Retain)]
		NSNumber Major { get; set; }

		// @property (nonatomic, strong) NSNumber * minor;
		[Export ("minor", ArgumentSemantic.Retain)]
		NSNumber Minor { get; set; }

		// @property (nonatomic, strong) NSString * macAddress;
		[Export ("macAddress", ArgumentSemantic.Retain)]
		string MacAddress { get; set; }

		// @property (nonatomic, strong) NSNumber * batteryLifeExpectancy;
		[Export ("batteryLifeExpectancy", ArgumentSemantic.Retain)]
		NSNumber BatteryLifeExpectancy { get; set; }

		// @property (nonatomic, strong) NSString * hardware;
		[Export ("hardware", ArgumentSemantic.Retain)]
		string Hardware { get; set; }

		// @property (nonatomic, strong) NSString * firmware;
		[Export ("firmware", ArgumentSemantic.Retain)]
		string Firmware { get; set; }

		// @property (assign, nonatomic) ESTBeaconPower power;
		[Export ("power", ArgumentSemantic.UnsafeUnretained)]
		ESTBeaconPower Power { get; set; }

		// @property (assign, nonatomic) NSInteger advInterval;
		[Export ("advInterval", ArgumentSemantic.UnsafeUnretained)]
		nint AdvInterval { get; set; }

		// @property (assign, nonatomic) BOOL isSecured;
		[Export ("isSecured", ArgumentSemantic.UnsafeUnretained)]
		bool IsSecured { get; set; }

		// @property (nonatomic, strong) NSString * name;
		[Export ("name", ArgumentSemantic.Retain)]
		string Name { get; set; }

		// @property (nonatomic) ESTBeaconColor color;
		[Export ("color")]
		ESTBeaconColor Color { get; set; }
	}

	// @protocol ESTBeaconDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESTBeaconDelegate {

		// @optional -(void)beaconConnectionDidSucceeded:(ESTBeacon *)beacon;
		[Export ("beaconConnectionDidSucceeded:"), EventArgs("BeaconConnectionDidSucceeded")]
		void BeaconConnectionDidSucceeded (ESTBeacon beacon);

		// @optional -(void)beaconConnectionDidFail:(ESTBeacon *)beacon withError:(NSError *)error;
		[Export ("beaconConnectionDidFail:withError:"), EventArgs("v")]
		void BeaconConnectionDidFail (ESTBeacon beacon, NSError error);

		// @optional -(void)beacon:(ESTBeacon *)beacon didDisconnectWithError:(NSError *)error;
		[Export ("beacon:didDisconnectWithError:"), EventArgs("DidDisconnectWithError")]
		void DidDisconnectWithError (ESTBeacon beacon, NSError error);

		// @optional -(void)beacon:(ESTBeacon *)beacon accelerometerStateChanged:(BOOL)state;
		[Export ("beacon:accelerometerStateChanged:"), EventArgs("AccelerometerStateChanged")]
		void AccelerometerStateChanged (ESTBeacon beacon, bool state);
	}

	// @interface ESTBeacon : NSObject <NSCopying, NSCoding>
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESTBeaconDelegate)})]
	interface ESTBeacon {

		// @property (nonatomic, weak) id<ESTBeaconDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, weak) id<ESTBeaconDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESTBeaconDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) NSNumber * major;
		[Export ("major")]
		NSNumber Major { get; }

		// @property (readonly, nonatomic) NSNumber * minor;
		[Export ("minor")]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) ESTBeaconColor color;
		[Export ("color")]
		ESTBeaconColor Color { get; }

		// @property (readonly, nonatomic) NSInteger rssi;
		[Export ("rssi")]
		nint Rssi { get; }

		// @property (readonly, nonatomic) ESTConnectionStatus connectionStatus;
		[Export ("connectionStatus")]
		ESTConnectionStatus ConnectionStatus { get; }

		// @property (readonly, nonatomic) NSUUID * proximityUUID;
		[Export ("proximityUUID")]
		NSUuid ProximityUUID { get; }

		// @property (readonly, nonatomic) NSNumber * distance;
		[Export ("distance")]
		NSNumber Distance { get; }

		// @property (readonly, nonatomic) CLProximity proximity;
		[Export ("proximity")]
		CLProximity Proximity { get; }

		// @property (readonly, nonatomic) NSString * macAddress;
		[Export ("macAddress")]
		string MacAddress { get; }

		// @property (readonly, nonatomic) NSNumber * measuredPower;
		[Export ("measuredPower")]
		NSNumber MeasuredPower { get; }

		// @property (readonly, nonatomic) ESTBeaconFirmwareState firmwareState;
		[Export ("firmwareState")]
		ESTBeaconFirmwareState FirmwareState { get; }

		// @property (readonly, nonatomic) CBPeripheral * peripheral;
		[Export ("peripheral")]
		CBPeripheral Peripheral { get; }

		// @property (readonly, nonatomic) NSString * name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSUUID * motionProximityUUID;
		[Export ("motionProximityUUID")]
		NSUuid MotionProximityUUID { get; }

		// @property (readonly, nonatomic) NSNumber * power;
		[Export ("power")]
		NSNumber Power { get; }

		// @property (readonly, nonatomic) NSNumber * advInterval;
		[Export ("advInterval")]
		NSNumber AdvInterval { get; }

		// @property (readonly, nonatomic) NSNumber * batteryLevel;
		[Export ("batteryLevel")]
		NSNumber BatteryLevel { get; }

		// @property (readonly, nonatomic) NSNumber * remainingLifetime;
		[Export ("remainingLifetime")]
		NSNumber RemainingLifetime { get; }

		// @property (readonly, nonatomic) ESTBeaconBatteryType batteryType;
		[Export ("batteryType")]
		ESTBeaconBatteryType BatteryType { get; }

		// @property (readonly, nonatomic) NSString * hardwareVersion;
		[Export ("hardwareVersion")]
		string HardwareVersion { get; }

		// @property (readonly, nonatomic) NSString * firmwareVersion;
		[Export ("firmwareVersion")]
		string FirmwareVersion { get; }

		// @property (readonly, nonatomic) ESTFirmwareUpdate firmwareUpdateInfo;
		[Export ("firmwareUpdateInfo")]
		ESTFirmwareUpdate FirmwareUpdateInfo { get; }

		// @property (readonly, nonatomic) BOOL isMoving;
		[Export ("isMoving")]
		bool IsMoving { get; }

		// @property (readonly, nonatomic) BOOL isSecured;
		[Export ("isSecured")]
		bool IsSecured { get; }

		// @property (readonly, nonatomic) BOOL isAccelerometerAvailable;
		[Export ("isAccelerometerAvailable")]
		bool IsAccelerometerAvailable { get; }

		// @property (readonly, nonatomic) BOOL isAccelerometerEditAvailable;
		[Export ("isAccelerometerEditAvailable")]
		bool IsAccelerometerEditAvailable { get; }

		// @property (readonly, nonatomic) BOOL accelerometerEnabled;
		[Export ("accelerometerEnabled")]
		bool AccelerometerEnabled { get; }

		// @property (readonly, nonatomic) BOOL motionUUIDEnabled;
		[Export ("motionUUIDEnabled")]
		bool MotionUUIDEnabled { get; }

		// @property (readonly, nonatomic) ESTBeaconPowerSavingMode basicPowerMode;
		[Export ("basicPowerMode")]
		ESTBeaconPowerSavingMode BasicPowerMode { get; }

		// @property (readonly, nonatomic) ESTBeaconPowerSavingMode smartPowerMode;
		[Export ("smartPowerMode")]
		ESTBeaconPowerSavingMode SmartPowerMode { get; }

		// @property (readonly, nonatomic) ESTBeaconEstimoteSecureUUID estimoteSecureUUID;
		[Export ("estimoteSecureUUID")]
		ESTBeaconEstimoteSecureUUID EstimoteSecureUUID { get; }

		// -(void)connect;
		[Export ("connect")]
		void Connect ();

		// -(void)connectWithAttempts:(NSInteger)attempts connectionTimeout:(NSInteger)timeout;
		[Export ("connectWithAttempts:connectionTimeout:")]
		void ConnectWithAttempts (nint attempts, nint timeout);

		// -(void)disconnect;
		[Export ("disconnect")]
		void Disconnect ();

		// -(void)readTemperatureWithCompletion:(ESTNumberCompletionBlock)completion;
		[Export ("readTemperatureWithCompletion:")]
		void ReadTemperatureWithCompletion (Action<NSNumber, NSError> completion);

		// -(void)calibrateTemperatureWithReferenceTemperature:(NSNumber *)temperature completion:(ESTNumberCompletionBlock)completion;
		[Export ("calibrateTemperatureWithReferenceTemperature:completion:")]
		void CalibrateTemperatureWithReferenceTemperature (NSNumber temperature, Action<NSNumber, NSError> completion);

		// -(void)readAccelerometerCountWithCompletion:(ESTNumberCompletionBlock)completion;
		[Export ("readAccelerometerCountWithCompletion:")]
		void ReadAccelerometerCountWithCompletion (Action<NSNumber, NSError> completion);

		// -(void)resetAccelerometerCountWithCompletion:(ESTUnsignedShortCompletionBlock)completion;
		[Export ("resetAccelerometerCountWithCompletion:")]
		void ResetAccelerometerCountWithCompletion (Action<ushort, NSError> completion);

		// -(void)writeName:(NSString *)name completion:(ESTStringCompletionBlock)completion;
		[Export ("writeName:completion:")]
		void WriteName (string name, Action<NSString, NSError> completion);

		// -(void)writeProximityUUID:(NSString *)pUUID completion:(ESTStringCompletionBlock)completion;
		[Export ("writeProximityUUID:completion:")]
		void WriteProximityUUID (string pUUID, Action<NSString, NSError> completion);

		// -(void)writeMajor:(unsigned short)major completion:(ESTUnsignedShortCompletionBlock)completion;
		[Export ("writeMajor:completion:")]
		void WriteMajor (ushort major, Action<ushort, NSError> completion);

		// -(void)writeMinor:(unsigned short)minor completion:(ESTUnsignedShortCompletionBlock)completion;
		[Export ("writeMinor:completion:")]
		void WriteMinor (ushort minor, Action<ushort, NSError> completion);

		// -(void)writeAdvInterval:(unsigned short)interval completion:(ESTUnsignedShortCompletionBlock)completion;
		[Export ("writeAdvInterval:completion:")]
		void WriteAdvInterval (ushort interval, Action<ushort, NSError> completion);

		// -(void)writePower:(ESTBeaconPower)power completion:(ESTPowerCompletionBlock)completion;
		[Export ("writePower:completion:")]
		void WritePower (ESTBeaconPower power, Action<ESTBeaconPower, NSError> completion);

		// -(void)resetToFactorySettingsWithCompletion:(ESTCompletionBlock)completion;
		[Export ("resetToFactorySettingsWithCompletion:")]
		void ResetToFactorySettingsWithCompletion (Action<NSError> completion);

		// -(void)enableAccelerometer:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
		[Export ("enableAccelerometer:completion:")]
		void EnableAccelerometer (bool enable, Action<sbyte, NSError> completion);

		// -(void)enableMotionUUID:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
		[Export ("enableMotionUUID:completion:")]
		void EnableMotionUUID (bool enable, Action<sbyte, NSError> completion);

		// -(void)enableBasicPowerMode:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
		[Export ("enableBasicPowerMode:completion:")]
		void EnableBasicPowerMode (bool enable, Action<sbyte, NSError> completion);

		// -(void)enableSmartPowerMode:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
		[Export ("enableSmartPowerMode:completion:")]
		void EnableSmartPowerMode (bool enable, Action<sbyte, NSError> completion);

		// -(void)enableEstimoteSecureUUID:(BOOL)enable completion:(ESTBoolCompletionBlock)completion;
		[Export ("enableEstimoteSecureUUID:completion:")]
		void EnableEstimoteSecureUUID (bool enable, Action<sbyte, NSError> completion);

		// -(void)checkFirmwareUpdateWithCompletion:(ESTFirmwareInfoCompletionBlock)completion;
		[Export ("checkFirmwareUpdateWithCompletion:")]
		void CheckFirmwareUpdateWithCompletion (Action<ESTFirmwareInfoVO, NSError> completion);

		// -(void)updateFirmwareWithProgress:(ESTProgressBlock)progress completion:(ESTCompletionBlock)completion;
		[Export ("updateFirmwareWithProgress:completion:")]
		void UpdateFirmwareWithProgress (Action<int, NSString, NSError> progress, Action<NSError> completion);

		// -(BOOL)isEqualToBeacon:(ESTBeacon *)beacon;
		[Export ("isEqualToBeacon:")]
		bool IsEqualToBeacon (ESTBeacon beacon);

		// -(ESTBeaconVO *)valueObject;
		[Export ("valueObject")]
		ESTBeaconVO ValueObject ();
	}

	// @interface ESTBeaconFirmwareVO : ESTFirmwareInfoVO
	[BaseType (typeof (ESTFirmwareInfoVO))]
	interface ESTBeaconFirmwareVO {

		// @property (nonatomic, strong) NSString * firmwareUrl;
		[Export ("firmwareUrl", ArgumentSemantic.Retain)]
		string FirmwareUrl { get; set; }

		// @property (nonatomic, strong) NSString * firmwareCleanerUrl;
		[Export ("firmwareCleanerUrl", ArgumentSemantic.Retain)]
		string FirmwareCleanerUrl { get; set; }
	}

	// @interface ESTBeaconRegion : CLBeaconRegion
	[BaseType (typeof (CLBeaconRegion))]
	interface ESTBeaconRegion {

		// -(instancetype)initWithProximityUUID:(NSUUID *)proximityUUID identifier:(NSString *)identifier secured:(BOOL)secured;
		[Export ("initWithProximityUUID:identifier:secured:")]
		IntPtr Constructor (NSUuid proximityUUID, string identifier, bool secured);

		// -(instancetype)initWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major identifier:(NSString *)identifier secured:(BOOL)secured;
		[Export ("initWithProximityUUID:major:identifier:secured:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, string identifier, bool secured);

		// -(instancetype)initWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor identifier:(NSString *)identifier secured:(BOOL)secured;
		[Export ("initWithProximityUUID:major:minor:identifier:secured:")]
		IntPtr Constructor (NSUuid proximityUUID, ushort major, ushort minor, string identifier, bool secured);

		// @property (assign, nonatomic) BOOL isSecured;
		[Export ("isSecured", ArgumentSemantic.UnsafeUnretained)]
		bool IsSecured { get; set; }

		// @property (assign, nonatomic) BOOL inMotion;
		[Export ("inMotion", ArgumentSemantic.UnsafeUnretained)]
		bool InMotion { get; set; }
	}

	// @protocol ESTBeaconManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESTBeaconManagerDelegate {

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didStartMonitoringForRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didStartMonitoringForRegion:"), EventArgs("DidStartMonitoringForRegion")]
		void DidStartMonitoringForRegion (ESTBeaconManager manager, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager monitoringDidFailForRegion:(ESTBeaconRegion *)region withError:(NSError *)error;
		[Export ("beaconManager:monitoringDidFailForRegion:withError:"), EventArgs("MonitoringDidFailForRegion")]
		void MonitoringDidFailForRegion (ESTBeaconManager manager, ESTBeaconRegion region, NSError error);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didEnterRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didEnterRegion:"), EventArgs("DidEnterRegion")]
		void DidEnterRegion (ESTBeaconManager manager, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didExitRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didExitRegion:"), EventArgs("DidExitRegion")]
		void DidExitRegion (ESTBeaconManager manager, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didDetermineState:(CLRegionState)state forRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didDetermineState:forRegion:"), EventArgs("DidDetermineState")]
		void DidDetermineState (ESTBeaconManager manager, CLRegionState state, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didRangeBeacons:(NSArray *)beacons inRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didRangeBeacons:inRegion:"), EventArgs("DidRangeBeacons")]
		void DidRangeBeacons (ESTBeaconManager manager, NSObject [] beacons, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager rangingBeaconsDidFailForRegion:(ESTBeaconRegion *)region withError:(NSError *)error;
		[Export ("beaconManager:rangingBeaconsDidFailForRegion:withError:"), EventArgs("RangingBeaconsDidFailForRegion")]
		void RangingBeaconsDidFailForRegion (ESTBeaconManager manager, ESTBeaconRegion region, NSError error);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didDiscoverBeacons:(NSArray *)beacons inRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didDiscoverBeacons:inRegion:"), EventArgs("DidDiscoverBeacons")]
		void DidDiscoverBeacons (ESTBeaconManager manager, NSObject [] beacons, ESTBeaconRegion region);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didFailDiscoveryInRegion:(ESTBeaconRegion *)region;
		[Export ("beaconManager:didFailDiscoveryInRegion:"), EventArgs("DidFailDiscoveryInRegion")]
		void DidFailDiscoveryInRegion (ESTBeaconManager manager, ESTBeaconRegion region);

		// @optional -(void)beaconManagerDidStartAdvertising:(ESTBeaconManager *)manager error:(NSError *)error;
		[Export ("beaconManagerDidStartAdvertising:error:"), EventArgs("DidStartAdvertising")]
		void DidStartAdvertising (ESTBeaconManager manager, NSError error);

		// @optional -(void)beaconManager:(ESTBeaconManager *)manager didChangeAuthorizationStatus:(CLAuthorizationStatus)status;
		[Export ("beaconManager:didChangeAuthorizationStatus:"), EventArgs("DidChangeAuthorizationStatus")]
		void DidChangeAuthorizationStatus (ESTBeaconManager manager, CLAuthorizationStatus status);
	}

	// @interface ESTBeaconManager : NSObject <CLLocationManagerDelegate>
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESTBeaconManagerDelegate)})]
	interface ESTBeaconManager {

		// -(instancetype)initWithDelegate:(id<ESTBeaconManagerDelegate>)delegate;
		[Export ("initWithDelegate:")]
		IntPtr Constructor (ESTBeaconManagerDelegate del);

		// @property (nonatomic, weak) id<ESTBeaconManagerDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, weak) id<ESTBeaconManagerDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESTBeaconManagerDelegate Delegate { get; set; }

		// @property (nonatomic) BOOL avoidUnknownStateBeacons;
		[Export ("avoidUnknownStateBeacons")]
		bool AvoidUnknownStateBeacons { get; set; }

		// @property (nonatomic) NSInteger preventUnknownUpdateCount;
		[Export ("preventUnknownUpdateCount")]
		nint PreventUnknownUpdateCount { get; set; }

		// @property (nonatomic) BOOL returnAllRangedBeaconsAtOnce;
		[Export ("returnAllRangedBeaconsAtOnce")]
		bool ReturnAllRangedBeaconsAtOnce { get; set; }

		// -(void)updateRangeLimit:(NSInteger)limit;
		[Export ("updateRangeLimit:")]
		void UpdateRangeLimit (nint limit);

		// +(CLAuthorizationStatus)authorizationStatus;
		[Static, Export ("authorizationStatus")]
		CLAuthorizationStatus AuthorizationStatus ();

		// -(void)requestWhenInUseAuthorization;
		[Export ("requestWhenInUseAuthorization")]
		void RequestWhenInUseAuthorization ();

		// -(void)requestAlwaysAuthorization;
		[Export ("requestAlwaysAuthorization")]
		void RequestAlwaysAuthorization ();

		// -(void)startMonitoringForRegion:(ESTBeaconRegion *)region;
		[Export ("startMonitoringForRegion:")]
		void StartMonitoringForRegion (ESTBeaconRegion region);

		// -(void)stopMonitoringForRegion:(ESTBeaconRegion *)region;
		[Export ("stopMonitoringForRegion:")]
		void StopMonitoringForRegion (ESTBeaconRegion region);

		// -(void)startRangingBeaconsInRegion:(ESTBeaconRegion *)region;
		[Export ("startRangingBeaconsInRegion:")]
		void StartRangingBeaconsInRegion (ESTBeaconRegion region);

		// -(void)stopRangingBeaconsInRegion:(ESTBeaconRegion *)region;
		[Export ("stopRangingBeaconsInRegion:")]
		void StopRangingBeaconsInRegion (ESTBeaconRegion region);

		// -(void)requestStateForRegion:(ESTBeaconRegion *)region;
		[Export ("requestStateForRegion:")]
		void RequestStateForRegion (ESTBeaconRegion region);

		// -(void)startEstimoteBeaconsDiscoveryForRegion:(ESTBeaconRegion *)region;
		[Export ("startEstimoteBeaconsDiscoveryForRegion:")]
		void StartEstimoteBeaconsDiscoveryForRegion (ESTBeaconRegion region);

		// -(void)startEstimoteBeaconDiscoveryForRegion:(ESTBeaconRegion *)region updateInterval:(float)interval;
		[Export ("startEstimoteBeaconDiscoveryForRegion:updateInterval:")]
		void StartEstimoteBeaconDiscoveryForRegion (ESTBeaconRegion region, float interval);

		// -(void)stopEstimoteBeaconDiscovery;
		[Export ("stopEstimoteBeaconDiscovery")]
		void StopEstimoteBeaconDiscovery ();

		// -(void)startAdvertisingWithProximityUUID:(NSUUID *)proximityUUID major:(CLBeaconMajorValue)major minor:(CLBeaconMinorValue)minor identifier:(NSString *)identifier;
		[Export ("startAdvertisingWithProximityUUID:major:minor:identifier:")]
		void StartAdvertisingWithProximityUUID (NSUuid proximityUUID, ushort major, ushort minor, string identifier);

		// -(void)stopAdvertising;
		[Export ("stopAdvertising")]
		void StopAdvertising ();

		// -(void)fetchEstimoteBeaconsWithCompletion:(ESTArrayCompletionBlock)completion;
		[Export ("fetchEstimoteBeaconsWithCompletion:")]
		void FetchEstimoteBeaconsWithCompletion (Action<NSArray, NSError> completion);

		// +(NSArray *)recentlyUsedUUIDs;
		[Static, Export ("recentlyUsedUUIDs")]
		NSObject [] RecentlyUsedUUIDs ();

		// +(NSArray *)recentlyCachedBeacons;
		[Static, Export ("recentlyCachedBeacons")]
		NSObject [] RecentlyCachedBeacons ();

		// +(BOOL)shouldUseRotation;
		[Static, Export ("shouldUseRotation")]
		bool ShouldUseRotation ();
	}

	// @interface ESTBeaconUpdateConfig : NSObject <NSCoding, NSCopying>
	[BaseType (typeof (NSObject))]
	interface ESTBeaconUpdateConfig {

		// @property (nonatomic, strong) NSNumber * advInterval;
		[Export ("advInterval", ArgumentSemantic.Retain)]
		NSNumber AdvInterval { get; set; }

		// @property (nonatomic, strong) NSNumber * power;
		[Export ("power", ArgumentSemantic.Retain)]
		NSNumber Power { get; set; }

		// @property (nonatomic, strong) NSNumber * basicPowerMode;
		[Export ("basicPowerMode", ArgumentSemantic.Retain)]
		NSNumber BasicPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * smartPowerMode;
		[Export ("smartPowerMode", ArgumentSemantic.Retain)]
		NSNumber SmartPowerMode { get; set; }

		// @property (nonatomic, strong) NSNumber * estimoteSecureUUID;
		[Export ("estimoteSecureUUID", ArgumentSemantic.Retain)]
		NSNumber EstimoteSecureUUID { get; set; }
	}

	// @protocol ESBeaconUpdateInfoDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESBeaconUpdateInfoDelegate {

		// @required -(void)beaconUpdateInfoInitialized:(id)beaconUpdateInfo;
		[Export ("beaconUpdateInfoInitialized:"), EventArgs("BeaconUpdateInfoInitialized")]
		[Abstract]
		void BeaconUpdateInfoInitialized (NSObject beaconUpdateInfo);
	}

	// @interface ESTBeaconUpdateInfo : NSObject <NSCoding>
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESBeaconUpdateInfoDelegate)})]
	interface ESTBeaconUpdateInfo {

		// -(instancetype)initWithBeacon:(ESTBeacon *)beacon config:(ESTBeaconUpdateConfig *)config;
		[Export ("initWithBeacon:config:")]
		IntPtr Constructor (ESTBeacon beacon, ESTBeaconUpdateConfig config);

		// -(instancetype)initWithBeacon:(ESTBeacon *)beacon config:(ESTBeaconUpdateConfig *)config delegate:(id<ESBeaconUpdateInfoDelegate>)delegate;
		[Export ("initWithBeacon:config:delegate:")]
		IntPtr Constructor (ESTBeacon beacon, ESTBeaconUpdateConfig config, ESBeaconUpdateInfoDelegate del);

		// @property (nonatomic, strong) ESTBeacon * beacon;
		[Export ("beacon", ArgumentSemantic.Retain)]
		ESTBeacon Beacon { get; set; }

		// @property (nonatomic, strong) NSString * peripheralID;
		[Export ("peripheralID", ArgumentSemantic.Retain)]
		string PeripheralID { get; set; }

		// @property (nonatomic, strong) ESTBeaconUpdateConfig * config;
		[Export ("config", ArgumentSemantic.Retain)]
		ESTBeaconUpdateConfig Config { get; set; }

		// @property (assign, nonatomic) id<ESBeaconUpdateInfoDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) id<ESBeaconUpdateInfoDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESBeaconUpdateInfoDelegate Delegate { get; set; }

		// @property (assign, nonatomic) ESBeaconUpdateInfoStatus status;
		[Export ("status", ArgumentSemantic.UnsafeUnretained)]
		ESBeaconUpdateInfoStatus Status { get; set; }

		// @property (nonatomic, strong) NSError * error;
		[Export ("error", ArgumentSemantic.Retain)]
		NSError Error { get; set; }

		// -(void)findPeripheral;
		[Export ("findPeripheral")]
		void FindPeripheral ();

		// -(void)updateWithConfig:(ESTBeaconUpdateConfig *)config;
		[Export ("updateWithConfig:")]
		void UpdateWithConfig (ESTBeaconUpdateConfig config);

		// -(NSString *)description;
		[Export ("description")]
		string UpdateDescription ();
	}

	// @interface ESTBulkUpdater : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTBulkUpdater {

		// @property (nonatomic, strong) NSArray * beaconInfos;
		[Export ("beaconInfos", ArgumentSemantic.Retain)]
		NSObject [] BeaconInfos { get; set; }

		// @property (readonly, nonatomic) ESTBulkUpdaterMode mode;
		[Export ("mode")]
		ESTBulkUpdaterMode Mode { get; }

		// @property (readonly, nonatomic) ESBulkUpdaterStatus status;
		[Export ("status")]
		ESBulkUpdaterStatus Status { get; }

		// +(ESTBulkUpdater *)sharedInstance;
		[Static, Export ("sharedInstance")]
		ESTBulkUpdater SharedInstance ();

		// -(void)startWithBeaconInfos:(NSArray *)beaconInfos timeout:(NSTimeInterval)timeout;
		[Export ("startWithBeaconInfos:timeout:")]
		void StartWithBeaconInfos (NSObject [] beaconInfos, double timeout);

		// -(BOOL)isUpdateInProgressForBeacon:(ESTBeacon *)beacon;
		[Export ("isUpdateInProgressForBeacon:")]
		bool IsUpdateInProgressForBeacon (ESTBeacon beacon);

		// -(BOOL)isBeaconWaitingForUpdate:(ESTBeacon *)beacon;
		[Export ("isBeaconWaitingForUpdate:")]
		bool IsBeaconWaitingForUpdate (ESTBeacon beacon);

		// -(NSArray *)getBeaconUpdateInfosForBeacon:(ESTBeacon *)beacon;
		[Export ("getBeaconUpdateInfosForBeacon:")]
		NSObject [] GetBeaconUpdateInfosForBeacon (ESTBeacon beacon);

		// -(NSTimeInterval)getTimeLeftToTimeout;
		[Export ("getTimeLeftToTimeout")]
		double GetTimeLeftToTimeout ();

		// -(void)cancel;
		[Export ("cancel")]
		void Cancel ();
	}

	// @interface ESTConfig : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTConfig {

		// +(void)setupAppID:(NSString *)appID andAppToken:(NSString *)appToken;
		[Static, Export ("setupAppID:andAppToken:")]
		void SetupAppID (string appID, string appToken);

		// +(BOOL)isAuthorized;
		[Static, Export ("isAuthorized")]
		bool IsAuthorized ();

		// +(void)enableAnalytics:(BOOL)enable;
		[Static, Export ("enableAnalytics:")]
		void EnableAnalytics (bool enable);

		// +(BOOL)isAnalyticsEnabled;
		[Static, Export ("isAnalyticsEnabled")]
		bool IsAnalyticsEnabled ();
	}

	// @interface ESTNearable : NSObject <NSCopying>
	[BaseType (typeof (NSObject))]
	interface ESTNearable {

		// @property (readonly, assign, nonatomic) ESTNearableType type;
		[Export ("type", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableType Type { get; }

		// @property (readonly, nonatomic, strong) NSString * identifier;
		[Export ("identifier", ArgumentSemantic.Retain)]
		string Identifier { get; }

		// @property (readonly, nonatomic, strong) NSString * hardwareVersion;
		[Export ("hardwareVersion", ArgumentSemantic.Retain)]
		string HardwareVersion { get; }

		// @property (readonly, nonatomic, strong) NSString * firmwareVersion;
		[Export ("firmwareVersion", ArgumentSemantic.Retain)]
		string FirmwareVersion { get; }

		// @property (readonly, assign, nonatomic) NSInteger rssi;
		[Export ("rssi", ArgumentSemantic.UnsafeUnretained)]
		nint Rssi { get; }

		// @property (readonly, assign, nonatomic) ESTNearableZone zone;
		[Export ("zone", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableZone NearableZone { get; }

		// @property (readonly, assign, nonatomic) double idleBatteryVoltage;
		[Export ("idleBatteryVoltage", ArgumentSemantic.UnsafeUnretained)]
		double IdleBatteryVoltage { get; }

		// @property (readonly, assign, nonatomic) double stressBatteryVoltage;
		[Export ("stressBatteryVoltage", ArgumentSemantic.UnsafeUnretained)]
		double StressBatteryVoltage { get; }

		// @property (readonly, assign, nonatomic) unsigned long long currentMotionStateDuration;
		[Export ("currentMotionStateDuration", ArgumentSemantic.UnsafeUnretained)]
		ulong CurrentMotionStateDuration { get; }

		// @property (readonly, assign, nonatomic) unsigned long long previousMotionStateDuration;
		[Export ("previousMotionStateDuration", ArgumentSemantic.UnsafeUnretained)]
		ulong PreviousMotionStateDuration { get; }

		// @property (readonly, assign, nonatomic) BOOL isMoving;
		[Export ("isMoving", ArgumentSemantic.UnsafeUnretained)]
		bool IsMoving { get; }

		// @property (readonly, assign, nonatomic) ESTNearableOrientation orientation;
		[Export ("orientation", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableOrientation Orientation { get; }

		// @property (readonly, assign, nonatomic) NSInteger xAcceleration;
		[Export ("xAcceleration", ArgumentSemantic.UnsafeUnretained)]
		nint XAcceleration { get; }

		// @property (readonly, assign, nonatomic) NSInteger yAcceleration;
		[Export ("yAcceleration", ArgumentSemantic.UnsafeUnretained)]
		nint YAcceleration { get; }

		// @property (readonly, assign, nonatomic) NSInteger zAcceleration;
		[Export ("zAcceleration", ArgumentSemantic.UnsafeUnretained)]
		nint ZAcceleration { get; }

		// @property (readonly, assign, nonatomic) double temperature;
		[Export ("temperature", ArgumentSemantic.UnsafeUnretained)]
		double Temperature { get; }

		// @property (readonly, assign, nonatomic) NSInteger txPower;
		[Export ("txPower", ArgumentSemantic.UnsafeUnretained)]
		nint TxPower { get; }

		// @property (readonly, assign, nonatomic) NSInteger channel;
		[Export ("channel", ArgumentSemantic.UnsafeUnretained)]
		nint Channel { get; }

		// @property (readonly, assign, nonatomic) ESTNearableFirmwareState firmwareState;
		[Export ("firmwareState", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableFirmwareState FirmwareState { get; }

		// -(NSString *)nameForType:(ESTNearableType)type;
		[Export ("nameForType:")]
		string NameForType (ESTNearableType type);
	}

	// @protocol ESTTriggerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESTTriggerDelegate {

		// @optional -(void)triggerDidChangeState:(ESTTrigger *)trigger;
		[Export ("triggerDidChangeState:"), EventArgs("TriggerDidChangeState")]
		void TriggerDidChangeState (ESTTrigger trigger);
	}

	// @interface ESTTrigger : NSObject
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESTTriggerDelegate)})]
	interface ESTTrigger {

		// -(instancetype)initWithRules:(NSArray *)rules identifier:(NSString *)identifier;
		[Export ("initWithRules:identifier:")]
		IntPtr Constructor (NSObject [] rules, string identifier);

		// @property (assign, nonatomic) id<ESTTriggerDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) id<ESTTriggerDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESTTriggerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSArray * rules;
		[Export ("rules", ArgumentSemantic.Retain)]
		NSObject [] Rules { get; }

		// @property (readonly, nonatomic, strong) NSString * identifier;
		[Export ("identifier", ArgumentSemantic.Retain)]
		string Identifier { get; }

		// @property (readonly, assign, nonatomic) BOOL state;
		[Export ("state", ArgumentSemantic.UnsafeUnretained)]
		bool State { get; }
	}

	// @interface ESTRule : NSObject
	[BaseType (typeof (NSObject))]
	interface ESTRule {

		// @property (assign, nonatomic) BOOL state;
		[Export ("state", ArgumentSemantic.UnsafeUnretained)]
		bool State { get; set; }

		// -(void)update;
		[Export ("update")]
		void Update ();
	}

	// @interface ESTDateRule : ESTRule
	[BaseType (typeof (ESTRule))]
	interface ESTDateRule {

		// @property (nonatomic, strong) NSNumber * afterHour;
		[Export ("afterHour", ArgumentSemantic.Retain)]
		NSNumber AfterHour { get; set; }

		// @property (nonatomic, strong) NSNumber * beforeHour;
		[Export ("beforeHour", ArgumentSemantic.Retain)]
		NSNumber BeforeHour { get; set; }

		// +(instancetype)hourLaterThan:(int)hour;
		[Static, Export ("hourLaterThan:")]
		ESTDateRule HourLaterThan (int hour);

		// +(instancetype)hourEarlierThan:(int)hour;
		[Static, Export ("hourEarlierThan:")]
		ESTDateRule HourEarlierThan (int hour);

		// +(instancetype)hourBetween:(int)firstHour and:(int)secondHour;
		[Static, Export ("hourBetween:and:")]
		ESTDateRule HourBetween (int firstHour, int secondHour);
	}

	// @interface ESTNearableRule : ESTRule
	[BaseType (typeof (ESTRule))]
	interface ESTNearableRule {

		// -(instancetype)initWithNearableIdentifier:(NSString *)identifier;
		[Export ("initWithNearableIdentifier:")]
		IntPtr Constructor (string identifier);

		// -(instancetype)initWithNearableType:(ESTNearableType)type;
		[Export ("initWithNearableType:")]
		IntPtr Constructor (ESTNearableType type);

		// @property (readonly, nonatomic, strong) NSString * nearableIdentifier;
		[Export ("nearableIdentifier", ArgumentSemantic.Retain)]
		string NearableIdentifier { get; }

		// @property (readonly, assign, nonatomic) ESTNearableType nearableType;
		[Export ("nearableType", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableType NearableType { get; }

		// -(void)updateWithNearable:(ESTNearable *)nearable;
		[Export ("updateWithNearable:")]
		void UpdateWithNearable (ESTNearable nearable);
	}

	// @interface ESTMotionRule : ESTNearableRule
	[BaseType (typeof (ESTNearableRule))]
	interface ESTMotionRule {

		// @property (assign, nonatomic) BOOL motionState;
		[Export ("motionState", ArgumentSemantic.UnsafeUnretained)]
		bool MotionState { get; set; }

		// +(instancetype)motionStateEquals:(BOOL)motionState forNearableIdentifier:(NSString *)identifier;
		[Static, Export ("motionStateEquals:forNearableIdentifier:")]
		ESTMotionRule MotionStateEquals (bool motionState, string identifier);

		// +(instancetype)motionStateEquals:(BOOL)motionState forNearableType:(ESTNearableType)type;
		[Static, Export ("motionStateEquals:forNearableType:")]
		ESTMotionRule MotionStateEquals (bool motionState, ESTNearableType type);
	}

	// @protocol ESTNearableManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESTNearableManagerDelegate {

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearables:(NSArray *)nearables withType:(ESTNearableType)type;
		[Export ("nearableManager:didRangeNearables:withType:"), EventArgs("DidRangeNearables")]
		void DidRangeNearables (ESTNearableManager manager, NSObject [] nearables, ESTNearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didRangeNearable:(ESTNearable *)nearable;
		[Export ("nearableManager:didRangeNearable:"), EventArgs("DidRangeNearable")]
		void DidRangeNearable (ESTNearableManager manager, ESTNearable nearable);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager rangingFailedWithError:(NSError *)error;
		[Export ("nearableManager:rangingFailedWithError:"), EventArgs("RangingFailedWithError")]
		void RangingFailedWithError (ESTNearableManager manager, NSError error);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterTypeRegion:(ESTNearableType)type;
		[Export ("nearableManager:didEnterTypeRegion:"), EventArgs("DidEnterTypeRegion")]
		void DidEnterTypeRegion (ESTNearableManager manager, ESTNearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didExitTypeRegion:(ESTNearableType)type;
		[Export ("nearableManager:didExitTypeRegion:"), EventArgs("DidExitTypeRegion")]
		void DidExitTypeRegion (ESTNearableManager manager, ESTNearableType type);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didEnterIdentifierRegion:(NSString *)identifier;
		[Export ("nearableManager:didEnterIdentifierRegion:"), EventArgs("DidEnterIdentifierRegion")]
		void DidEnterIdentifierRegion (ESTNearableManager manager, string identifier);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager didExitIdentifierRegion:(NSString *)identifier;
		[Export ("nearableManager:didExitIdentifierRegion:"), EventArgs("DidExitIdentifierRegion")]
		void DidExitIdentifierRegion (ESTNearableManager manager, string identifier);

		// @optional -(void)nearableManager:(ESTNearableManager *)manager monitoringFailedWithError:(NSError *)error;
		[Export ("nearableManager:monitoringFailedWithError:"), EventArgs("MonitoringFailedWithError")]
		void MonitoringFailedWithError (ESTNearableManager manager, NSError error);
	}

	// @interface ESTNearableManager : NSObject
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESTNearableManagerDelegate)})]
	interface ESTNearableManager {

		// @property (nonatomic, weak) id<ESTNearableManagerDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, weak) id<ESTNearableManagerDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESTNearableManagerDelegate Delegate { get; set; }

		// -(void)startMonitoringForIdentifier:(NSString *)identifier;
		[Export ("startMonitoringForIdentifier:")]
		void StartMonitoringForIdentifier (string identifier);

		// -(void)stopMonitoringForIdentifier:(NSString *)identifier;
		[Export ("stopMonitoringForIdentifier:")]
		void StopMonitoringForIdentifier (string identifier);

		// -(void)startMonitoringForType:(ESTNearableType)type;
		[Export ("startMonitoringForType:")]
		void StartMonitoringForType (ESTNearableType type);

		// -(void)stopMonitoringForType:(ESTNearableType)type;
		[Export ("stopMonitoringForType:")]
		void StopMonitoringForType (ESTNearableType type);

		// -(void)stopMonitoring;
		[Export ("stopMonitoring")]
		void StopMonitoring ();

		// -(void)startRangingForIdentifier:(NSString *)identifier;
		[Export ("startRangingForIdentifier:")]
		void StartRangingForIdentifier (string identifier);

		// -(void)stopRangingForIdentifier:(NSString *)identifier;
		[Export ("stopRangingForIdentifier:")]
		void StopRangingForIdentifier (string identifier);

		// -(void)startRangingForType:(ESTNearableType)type;
		[Export ("startRangingForType:")]
		void StartRangingForType (ESTNearableType type);

		// -(void)stopRangingForType:(ESTNearableType)type;
		[Export ("stopRangingForType:")]
		void StopRangingForType (ESTNearableType type);

		// -(void)stopRanging;
		[Export ("stopRanging")]
		void StopRanging ();
	}

	// @interface ESTOrientationRule : ESTNearableRule
	[BaseType (typeof (ESTNearableRule))]
	interface ESTOrientationRule {

		// @property (assign, nonatomic) ESTNearableOrientation orientation;
		[Export ("orientation", ArgumentSemantic.UnsafeUnretained)]
		ESTNearableOrientation Orientation { get; set; }

		// +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableIdentifier:(NSString *)identifier;
		[Static, Export ("orientationEquals:forNearableIdentifier:")]
		ESTOrientationRule OrientationEquals (ESTNearableOrientation orientation, string identifier);

		// +(instancetype)orientationEquals:(ESTNearableOrientation)orientation forNearableType:(ESTNearableType)type;
		[Static, Export ("orientationEquals:forNearableType:")]
		ESTOrientationRule OrientationEquals (ESTNearableOrientation orientation, ESTNearableType type);
	}

	// @interface ESTProximityRule : ESTNearableRule
	[BaseType (typeof (ESTNearableRule))]
	interface ESTProximityRule {

		// @property (assign, nonatomic) BOOL inRange;
		[Export ("inRange", ArgumentSemantic.UnsafeUnretained)]
		bool InRange { get; set; }

		// +(instancetype)inRangeOfNearableIdentifier:(NSString *)identifier;
		[Static, Export ("inRangeOfNearableIdentifier:")]
		ESTProximityRule InRangeOfNearableIdentifier (string identifier);

		// +(instancetype)inRangeOfNearableType:(ESTNearableType)type;
		[Static, Export ("inRangeOfNearableType:")]
		ESTProximityRule InRangeOfNearableType (ESTNearableType type);

		// +(instancetype)outsideRangeOfNearableIdentifier:(NSString *)identifier;
		[Static, Export ("outsideRangeOfNearableIdentifier:")]
		ESTProximityRule OutsideRangeOfNearableIdentifier (string identifier);

		// +(instancetype)outsideRangeOfNearableType:(ESTNearableType)type;
		[Static, Export ("outsideRangeOfNearableType:")]
		ESTProximityRule OutsideRangeOfNearableType (ESTNearableType type);
	}

	// @interface ESTTemperatureRule : ESTNearableRule
	[BaseType (typeof (ESTNearableRule))]
	interface ESTTemperatureRule {

		// @property (nonatomic, strong) NSNumber * maxValue;
		[Export ("maxValue", ArgumentSemantic.Retain)]
		NSNumber MaxValue { get; set; }

		// @property (nonatomic, strong) NSNumber * minValue;
		[Export ("minValue", ArgumentSemantic.Retain)]
		NSNumber MinValue { get; set; }

		// +(instancetype)temperatureGraterThan:(double)value forNearableIdentifier:(NSString *)identifier;
		[Static, Export ("temperatureGraterThan:forNearableIdentifier:")]
		ESTTemperatureRule TemperatureGraterThan (double value, string identifier);

		// +(instancetype)temperatureLowerThan:(double)value forNearableIdentifier:(NSString *)identifier;
		[Static, Export ("temperatureLowerThan:forNearableIdentifier:")]
		ESTTemperatureRule TemperatureLowerThan (double value, string identifier);

		// +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableIdentifier:(NSString *)identifier;
		[Static, Export ("temperatureBetween:and:forNearableIdentifier:")]
		ESTTemperatureRule TemperatureBetween (double minValue, double maxValue, string identifier);

		// +(instancetype)temperatureGraterThan:(double)value forNearableType:(ESTNearableType)type;
		[Static, Export ("temperatureGraterThan:forNearableType:")]
		ESTTemperatureRule TemperatureGraterThan (double value, ESTNearableType type);

		// +(instancetype)temperatureLowerThan:(double)value forNearableType:(ESTNearableType)type;
		[Static, Export ("temperatureLowerThan:forNearableType:")]
		ESTTemperatureRule TemperatureLowerThan (double value, ESTNearableType type);

		// +(instancetype)temperatureBetween:(double)minValue and:(double)maxValue forNearableType:(ESTNearableType)type;
		[Static, Export ("temperatureBetween:and:forNearableType:")]
		ESTTemperatureRule TemperatureBetween (double minValue, double maxValue, ESTNearableType type);
	}

	// @protocol ESTTriggerManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface ESTTriggerManagerDelegate {

		// @optional -(void)triggerManager:(ESTTriggerManager *)manager triggerChangedState:(ESTTrigger *)trigger;
		[Export ("triggerManager:triggerChangedState:"), EventArgs("TriggerChangedState")]
		void TriggerChangedState (ESTTriggerManager manager, ESTTrigger trigger);
	}

	// @interface ESTTriggerManager : NSObject <ESTTriggerDelegate>
	[BaseType (typeof (NSObject),
		Delegates=new string [] { "WeakDelegate" }, 
		Events=new Type [] {typeof(ESTTriggerManagerDelegate)})]
	interface ESTTriggerManager : ESTTriggerDelegate {

		// @property (assign, nonatomic) id<ESTTriggerManagerDelegate> delegate;
		[Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
		[NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) id<ESTTriggerManagerDelegate> delegate;
		[Wrap ("WeakDelegate")]
		ESTTriggerManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) NSArray * triggers;
		[Export ("triggers", ArgumentSemantic.Retain)]
		NSObject [] Triggers { get; }

		// -(void)startMonitoringForTrigger:(ESTTrigger *)trigger;
		[Export ("startMonitoringForTrigger:")]
		void StartMonitoringForTrigger (ESTTrigger trigger);

		// -(void)stopMonitoringForTriggerWithIdentifier:(NSString *)identifier;
		[Export ("stopMonitoringForTriggerWithIdentifier:")]
		void StopMonitoringForTriggerWithIdentifier (string identifier);
	}
}

