using System;

namespace Microsoft.Identity.Client.TelemetryCore
{
	// Token: 0x020001DE RID: 478
	internal enum AssertionType
	{
		// Token: 0x0400086B RID: 2155
		None,
		// Token: 0x0400086C RID: 2156
		CertificateWithoutSni,
		// Token: 0x0400086D RID: 2157
		CertificateWithSni,
		// Token: 0x0400086E RID: 2158
		Secret,
		// Token: 0x0400086F RID: 2159
		ClientAssertion,
		// Token: 0x04000870 RID: 2160
		ManagedIdentity
	}
}
