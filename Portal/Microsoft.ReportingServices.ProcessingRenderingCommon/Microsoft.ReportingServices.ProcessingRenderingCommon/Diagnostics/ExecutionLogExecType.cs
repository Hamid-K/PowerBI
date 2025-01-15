using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000067 RID: 103
	public enum ExecutionLogExecType
	{
		// Token: 0x04000171 RID: 369
		Live = 1,
		// Token: 0x04000172 RID: 370
		Cache,
		// Token: 0x04000173 RID: 371
		Snapshot,
		// Token: 0x04000174 RID: 372
		History,
		// Token: 0x04000175 RID: 373
		AdHoc,
		// Token: 0x04000176 RID: 374
		Session,
		// Token: 0x04000177 RID: 375
		Rdce
	}
}
