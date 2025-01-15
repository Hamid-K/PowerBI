using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000719 RID: 1817
	[DataContract]
	[Serializable]
	public class ElmUserDataRemoteEnvironment : ElmTrmBaseRemoteEnvironment
	{
		// Token: 0x060039A8 RID: 14760 RVA: 0x000C6257 File Offset: 0x000C4457
		public ElmUserDataRemoteEnvironment()
		{
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000C628C File Offset: 0x000C448C
		public ElmUserDataRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName, useIbmSecurityExit)
		{
		}
	}
}
