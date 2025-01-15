using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.RelayPacketContracts
{
	// Token: 0x02000008 RID: 8
	[Flags]
	public enum ControlFlags : byte
	{
		// Token: 0x04000015 RID: 21
		None = 0,
		// Token: 0x04000016 RID: 22
		EndOfData = 1,
		// Token: 0x04000017 RID: 23
		HasTelemetry = 2,
		// Token: 0x04000018 RID: 24
		HasCorrectDataSize = 4
	}
}
