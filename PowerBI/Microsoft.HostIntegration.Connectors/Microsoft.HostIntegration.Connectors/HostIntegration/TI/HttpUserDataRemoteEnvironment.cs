using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000710 RID: 1808
	[DataContract]
	[Serializable]
	public class HttpUserDataRemoteEnvironment : HttpBaseRemoteEnvironment
	{
		// Token: 0x06003949 RID: 14665 RVA: 0x000BF640 File Offset: 0x000BD840
		public HttpUserDataRemoteEnvironment()
		{
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000BF690 File Offset: 0x000BD890
		public HttpUserDataRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, bool allowRedirects, string aliasTransactionId, string userAgent)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, allowRedirects, aliasTransactionId, userAgent)
		{
		}
	}
}
