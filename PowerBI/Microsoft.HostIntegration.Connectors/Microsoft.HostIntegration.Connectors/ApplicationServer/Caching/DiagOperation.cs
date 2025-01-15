using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001AA RID: 426
	internal class DiagOperation
	{
		// Token: 0x040009BD RID: 2493
		[ThreadStatic]
		public static int ReqId;

		// Token: 0x040009BE RID: 2494
		[ThreadStatic]
		public static string ChannelName;

		// Token: 0x040009BF RID: 2495
		[ThreadStatic]
		public static DiagOperationState OpState;
	}
}
