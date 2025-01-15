using System;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000716 RID: 1814
	[DataContract]
	[Serializable]
	public class DpcRemoteEnvironment : TcpWithCommonNameRemoteEnvironment
	{
		// Token: 0x060039A0 RID: 14752 RVA: 0x000C61DF File Offset: 0x000C43DF
		public DpcRemoteEnvironment()
		{
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000C61E8 File Offset: 0x000C43E8
		public DpcRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName)
		{
		}
	}
}
