using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000051 RID: 81
	internal interface ITrustedHeaderVerification
	{
		// Token: 0x060002A3 RID: 675
		void VerifyTrustedUserAccess(string processAccountName, string userName, string item);
	}
}
