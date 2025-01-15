using System;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200048D RID: 1165
	public enum AnalyticsWellKnownEventID : ushort
	{
		// Token: 0x040017B6 RID: 6070
		DoNotUse = 64256,
		// Token: 0x040017B7 RID: 6071
		OutofRange,
		// Token: 0x040017B8 RID: 6072
		TraceInformation,
		// Token: 0x040017B9 RID: 6073
		TraceListenerWrite,
		// Token: 0x040017BA RID: 6074
		RunnerEvent,
		// Token: 0x040017BB RID: 6075
		ExceptionCritical,
		// Token: 0x040017BC RID: 6076
		ExceptionError,
		// Token: 0x040017BD RID: 6077
		ExceptionWarning,
		// Token: 0x040017BE RID: 6078
		ExceptionInformation,
		// Token: 0x040017BF RID: 6079
		ExceptionVerbose
	}
}
