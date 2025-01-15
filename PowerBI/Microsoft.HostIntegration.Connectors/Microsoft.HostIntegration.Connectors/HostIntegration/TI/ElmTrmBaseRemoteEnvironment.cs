using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000717 RID: 1815
	[DataContract]
	[Serializable]
	public class ElmTrmBaseRemoteEnvironment : TcpWithCommonNameRemoteEnvironment
	{
		// Token: 0x060039A2 RID: 14754 RVA: 0x000C61DF File Offset: 0x000C43DF
		protected ElmTrmBaseRemoteEnvironment()
		{
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000C6214 File Offset: 0x000C4414
		protected ElmTrmBaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName)
		{
			this.UseIbmCicsSecurityExit = useIbmSecurityExit;
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000C6246 File Offset: 0x000C4446
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000C624E File Offset: 0x000C444E
		[DataMember]
		[Category("Security")]
		public bool UseIbmCicsSecurityExit { get; set; }
	}
}
