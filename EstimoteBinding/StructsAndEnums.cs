using System;
using ObjCRuntime;

namespace EstimoteBinding
{
	public enum ESTFirmwareUpdate {
		None,
		Available,
		NotAvailable
	}

	public enum ESTConnectionStatus {
		Connecting,
		Connected,
		Disconnected
	}

	public enum ESTBeaconPower : sbyte {
		Level1 = -30,
		Level2 = -20,
		Level3 = -16,
		Level4 = -12,
		Level5 = -8,
		Level6 = -4,
		Level7 = 0,
		Level8 = 4
	}

	public enum ESTBeaconBatteryType {
		Unknown = 0,
		CR2450,
		CR2477
	}

	public enum ESTBeaconFirmwareState {
		Boot,
		App
	}

	public enum ESTBeaconColor {
		Unknown = 0,
		Mint = 1,
		Ice,
		Blueberry,
		White,
		Transparent
	}

	public enum ESTBeaconPowerSavingMode {
		Unknown,
		On,
		Off,
		NotAvailable
	}

	public enum ESTBeaconEstimoteSecureUUID {
		Unknown,
		On,
		Off,
		NotAvailable
	}

	public enum ESTBeaconCharInfoType {
		WriteRead,
		WriteOnly
	}

	[Native]
	public enum ESBeaconUpdateInfoStatus : long /* nint */ {
		Idle,
		ReadyToUpdate,
		Updating,
		UpdateSuccess,
		UpdateFailed
	}

	[Native]
	public enum ESBulkUpdaterStatus : long /* nint */ {
		Idle,
		Updating,
		Completed
	}

	[Native]
	public enum ESTBulkUpdaterMode : long /* nint */ {
		Foreground,
		Background
	}

	[Native]
	public enum ESTNearableType : long /* nint */ {
		Unknown = 0,
		Dog,
		Car,
		Fridge,
		Bag,
		Bike,
		Chair,
		Bed,
		Door,
		Shoe,
		Generic,
		All
	}

	[Native]
	public enum ESTNearableOrientation : long /* nint */ {
		Unknown = 0,
		Horizontal,
		HorizontalUpsideDown,
		Vertical,
		VerticalUpsideDown,
		LeftSide,
		RightSide
	}

	[Native]
	public enum ESTNearableZone : long /* nint */ {
		Unknown = 0,
		Immediate,
		Near,
		Far
	}

	[Native]
	public enum ESTNearableFirmwareState : long /* nint */ {
		Boot = 0,
		App
	}
}

