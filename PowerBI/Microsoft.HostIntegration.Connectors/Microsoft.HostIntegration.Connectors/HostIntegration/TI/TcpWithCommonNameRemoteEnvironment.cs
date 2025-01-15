using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074B RID: 1867
	[DataContract]
	[Serializable]
	public class TcpWithCommonNameRemoteEnvironment : TcpWithSslRemoteEnvironment
	{
		// Token: 0x06003B46 RID: 15174 RVA: 0x000C65D0 File Offset: 0x000C47D0
		protected TcpWithCommonNameRemoteEnvironment()
		{
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000C7254 File Offset: 0x000C5454
		protected TcpWithCommonNameRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired)
		{
			this.CertificateCommonName = certificateCommonName;
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000C7284 File Offset: 0x000C5484
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x000C728C File Offset: 0x000C548C
		[DataMember]
		[Description("Common Name of the certificate.")]
		[Category("TCP/IP")]
		public string CertificateCommonName { get; set; }
	}
}
