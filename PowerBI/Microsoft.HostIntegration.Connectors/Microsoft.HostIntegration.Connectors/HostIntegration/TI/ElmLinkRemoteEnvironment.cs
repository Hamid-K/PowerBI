using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000718 RID: 1816
	[DataContract]
	[Serializable]
	public class ElmLinkRemoteEnvironment : ElmTrmBaseRemoteEnvironment
	{
		// Token: 0x060039A6 RID: 14758 RVA: 0x000C6257 File Offset: 0x000C4457
		public ElmLinkRemoteEnvironment()
		{
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000C6260 File Offset: 0x000C4460
		public ElmLinkRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName, useIbmSecurityExit)
		{
		}
	}
}
