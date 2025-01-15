using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	internal enum WorkloadGroupParameterType
	{
		// Token: 0x040008CB RID: 2251
		Importance,
		// Token: 0x040008CC RID: 2252
		RequestMaxMemoryGrantPercent,
		// Token: 0x040008CD RID: 2253
		RequestMaxCpuTimeSec,
		// Token: 0x040008CE RID: 2254
		RequestMemoryGrantTimeoutSec,
		// Token: 0x040008CF RID: 2255
		MaxDop,
		// Token: 0x040008D0 RID: 2256
		GroupMaxRequests,
		// Token: 0x040008D1 RID: 2257
		GroupMinMemoryPercent
	}
}
