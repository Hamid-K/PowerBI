using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000174 RID: 372
	[Flags]
	internal enum SocketProtocolTypes
	{
		// Token: 0x04000870 RID: 2160
		None = 0,
		// Token: 0x04000871 RID: 2161
		VelocityWire = 1,
		// Token: 0x04000872 RID: 2162
		MemcacheBinary = 2,
		// Token: 0x04000873 RID: 2163
		MemcacheText = 4
	}
}
