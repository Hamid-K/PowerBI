using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x02000055 RID: 85
	public enum AuthenticationType
	{
		// Token: 0x0400022C RID: 556
		None,
		// Token: 0x0400022D RID: 557
		Windows,
		// Token: 0x0400022E RID: 558
		Passport,
		// Token: 0x0400022F RID: 559
		Forms,
		// Token: 0x04000230 RID: 560
		SharePointTrustedUser,
		// Token: 0x04000231 RID: 561
		Federation,
		// Token: 0x04000232 RID: 562
		OAuth
	}
}
