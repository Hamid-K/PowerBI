using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200017D RID: 381
	[Flags]
	internal enum VelocityPacketHeaderFlags : byte
	{
		// Token: 0x04000896 RID: 2198
		None = 0,
		// Token: 0x04000897 RID: 2199
		IsLinkedToNext = 1,
		// Token: 0x04000898 RID: 2200
		MemcacheProtocol = 64,
		// Token: 0x04000899 RID: 2201
		[Obsolete]
		IsLittleEndian_Reserved = 128
	}
}
