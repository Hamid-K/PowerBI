using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074A RID: 1866
	[DataContract]
	[Serializable]
	public class TcpWithSslRemoteEnvironment : TcpWithSsoRemoteEnvironment
	{
		// Token: 0x06003B40 RID: 15168 RVA: 0x000C71F4 File Offset: 0x000C53F4
		protected TcpWithSslRemoteEnvironment()
		{
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000C71FC File Offset: 0x000C53FC
		protected TcpWithSslRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports)
		{
			this.UseSsl = useSsl;
			this.ServerVerificationRequired = serverVerificationRequired;
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000C7230 File Offset: 0x000C5430
		// (set) Token: 0x06003B43 RID: 15171 RVA: 0x000C7238 File Offset: 0x000C5438
		[DataMember]
		[Description("Use SSL")]
		[Category("TCP/IP")]
		public bool UseSsl { get; set; }

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000C7241 File Offset: 0x000C5441
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x000C7249 File Offset: 0x000C5449
		[DataMember]
		[Description("Server certificate verification required")]
		[Category("TCP/IP")]
		public bool ServerVerificationRequired { get; set; }
	}
}
