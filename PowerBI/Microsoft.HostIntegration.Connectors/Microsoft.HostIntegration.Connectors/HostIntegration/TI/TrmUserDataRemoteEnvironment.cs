using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074F RID: 1871
	[DataContract]
	[Serializable]
	public class TrmUserDataRemoteEnvironment : TrmBaseRemoteEnvironment
	{
		// Token: 0x06003B52 RID: 15186 RVA: 0x000C7305 File Offset: 0x000C5505
		public TrmUserDataRemoteEnvironment()
		{
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000C7340 File Offset: 0x000C5540
		public TrmUserDataRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit, string concurrentServerTransactionId)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName, useIbmSecurityExit, concurrentServerTransactionId)
		{
		}
	}
}
