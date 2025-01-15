using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074E RID: 1870
	[DataContract]
	[Serializable]
	public class TrmLinkRemoteEnvironment : TrmBaseRemoteEnvironment
	{
		// Token: 0x06003B50 RID: 15184 RVA: 0x000C7305 File Offset: 0x000C5505
		public TrmLinkRemoteEnvironment()
		{
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000C7310 File Offset: 0x000C5510
		public TrmLinkRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit, string concurrentServerTransactionId)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName, useIbmSecurityExit, concurrentServerTransactionId)
		{
		}
	}
}
