using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200070F RID: 1807
	[DataContract]
	[Serializable]
	public class HttpLinkRemoteEnvironment : HttpBaseRemoteEnvironment
	{
		// Token: 0x06003945 RID: 14661 RVA: 0x000BF640 File Offset: 0x000BD840
		public HttpLinkRemoteEnvironment()
		{
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000BF648 File Offset: 0x000BD848
		public HttpLinkRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, bool allowRedirects, string aliasTransactionId, string userAgent, string mirrorProgramName)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, allowRedirects, aliasTransactionId, userAgent)
		{
			this.MirrorProgram = mirrorProgramName;
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000BF67E File Offset: 0x000BD87E
		// (set) Token: 0x06003948 RID: 14664 RVA: 0x000BF686 File Offset: 0x000BD886
		[DataMember]
		[DisplayName("MirrorProgram *")]
		[Description("Name of the mirror program")]
		[Category("CICS HTTP")]
		public string MirrorProgram { get; set; }
	}
}
