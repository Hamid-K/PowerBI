using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200007A RID: 122
	[Flags]
	public enum EventKeywords : long
	{
		// Token: 0x04000171 RID: 369
		None = 0L,
		// Token: 0x04000172 RID: 370
		All = -1L,
		// Token: 0x04000173 RID: 371
		WdiContext = 562949953421312L,
		// Token: 0x04000174 RID: 372
		WdiDiagnostic = 1125899906842624L,
		// Token: 0x04000175 RID: 373
		Sqm = 2251799813685248L,
		// Token: 0x04000176 RID: 374
		AuditFailure = 4503599627370496L,
		// Token: 0x04000177 RID: 375
		AuditSuccess = 9007199254740992L,
		// Token: 0x04000178 RID: 376
		CorrelationHint = 18014398509481984L,
		// Token: 0x04000179 RID: 377
		EventLogClassic = 36028797018963968L
	}
}
