using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200024C RID: 588
	[Flags]
	public enum TraceFlags : long
	{
		// Token: 0x040011F4 RID: 4596
		None = 0L,
		// Token: 0x040011F5 RID: 4597
		EwsRequest = 1L,
		// Token: 0x040011F6 RID: 4598
		EwsResponse = 2L,
		// Token: 0x040011F7 RID: 4599
		EwsResponseHttpHeaders = 4L,
		// Token: 0x040011F8 RID: 4600
		AutodiscoverRequest = 8L,
		// Token: 0x040011F9 RID: 4601
		AutodiscoverResponse = 16L,
		// Token: 0x040011FA RID: 4602
		AutodiscoverResponseHttpHeaders = 32L,
		// Token: 0x040011FB RID: 4603
		AutodiscoverConfiguration = 64L,
		// Token: 0x040011FC RID: 4604
		DebugMessage = 128L,
		// Token: 0x040011FD RID: 4605
		EwsRequestHttpHeaders = 256L,
		// Token: 0x040011FE RID: 4606
		AutodiscoverRequestHttpHeaders = 512L,
		// Token: 0x040011FF RID: 4607
		All = 9223372036854775807L
	}
}
