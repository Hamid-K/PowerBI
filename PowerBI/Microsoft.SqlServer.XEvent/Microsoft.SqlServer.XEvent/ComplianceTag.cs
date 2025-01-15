using System;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x02000017 RID: 23
	public enum ComplianceTag
	{
		// Token: 0x04000103 RID: 259
		Untagged = 1000,
		// Token: 0x04000104 RID: 260
		SystemMetadata = 2000,
		// Token: 0x04000105 RID: 261
		CustomerMetadata = 3000,
		// Token: 0x04000106 RID: 262
		ResourceName = 4000,
		// Token: 0x04000107 RID: 263
		ObjectName = 5000,
		// Token: 0x04000108 RID: 264
		EUPI = 6000,
		// Token: 0x04000109 RID: 265
		ErrorMessage = 7000,
		// Token: 0x0400010A RID: 266
		SensitiveData = 8000
	}
}
