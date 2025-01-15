using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000492 RID: 1170
	[Flags]
	public enum CertificateDataOptions
	{
		// Token: 0x04000CAE RID: 3246
		None = 0,
		// Token: 0x04000CAF RID: 3247
		VerifyServiceCertificateRevocation = 1,
		// Token: 0x04000CB0 RID: 3248
		VerifyServiceCertificateName = 2
	}
}
