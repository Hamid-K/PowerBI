using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A0 RID: 1184
	public interface ICertificateProvider
	{
		// Token: 0x06002480 RID: 9344
		ClientCertificateData GetCertificateData(string certificateKey);
	}
}
