using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AEB RID: 2795
	public enum StatusDataType
	{
		// Token: 0x040045A1 RID: 17825
		Unknown,
		// Token: 0x040045A2 RID: 17826
		NoChannel,
		// Token: 0x040045A3 RID: 17827
		QueueManagerUnavailable = 22,
		// Token: 0x040045A4 RID: 17828
		CipherSpec = 24,
		// Token: 0x040045A5 RID: 17829
		SslCertificateRequired = 26
	}
}
