using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000736 RID: 1846
	[DataContract]
	[Serializable]
	public class HttpBaseRemoteEnvironment : TcpWithSslRemoteEnvironment
	{
		// Token: 0x060039D9 RID: 14809 RVA: 0x000C65D0 File Offset: 0x000C47D0
		public HttpBaseRemoteEnvironment()
		{
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000C65D8 File Offset: 0x000C47D8
		protected HttpBaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, bool allowRedirects, string aliasTransactionId, string userAgent)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired)
		{
			this.AllowRedirects = allowRedirects;
			this.AliasTransactionId = aliasTransactionId;
			this.UserAgent = userAgent;
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x000C6618 File Offset: 0x000C4818
		// (set) Token: 0x060039DC RID: 14812 RVA: 0x000C6620 File Offset: 0x000C4820
		[DataMember]
		[Description("Allow redirects?")]
		[Category("CICS HTTP")]
		public bool AllowRedirects { get; set; }

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000C6629 File Offset: 0x000C4829
		// (set) Token: 0x060039DE RID: 14814 RVA: 0x000C6631 File Offset: 0x000C4831
		[DataMember]
		[Description("Alias transaction Id")]
		[Category("CICS HTTP")]
		public string AliasTransactionId { get; set; }

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000C663A File Offset: 0x000C483A
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000C6642 File Offset: 0x000C4842
		[DataMember]
		[Description("User agent name")]
		[Category("CICS HTTP")]
		public string UserAgent { get; set; }
	}
}
