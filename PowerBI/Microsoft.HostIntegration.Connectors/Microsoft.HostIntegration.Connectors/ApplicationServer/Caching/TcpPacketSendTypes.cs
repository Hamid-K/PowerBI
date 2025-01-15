using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200019C RID: 412
	[Flags]
	internal enum TcpPacketSendTypes
	{
		// Token: 0x04000955 RID: 2389
		None = 0,
		// Token: 0x04000956 RID: 2390
		Immediate = 1,
		// Token: 0x04000957 RID: 2391
		Queue = 2,
		// Token: 0x04000958 RID: 2392
		Quit = 4,
		// Token: 0x04000959 RID: 2393
		Ignore = 8
	}
}
