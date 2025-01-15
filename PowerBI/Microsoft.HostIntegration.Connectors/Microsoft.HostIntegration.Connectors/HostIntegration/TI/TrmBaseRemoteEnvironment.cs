using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200074D RID: 1869
	[DataContract]
	[Serializable]
	public class TrmBaseRemoteEnvironment : ElmTrmBaseRemoteEnvironment
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x000C6257 File Offset: 0x000C4457
		protected TrmBaseRemoteEnvironment()
		{
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000C72C0 File Offset: 0x000C54C0
		public TrmBaseRemoteEnvironment(string reClassId, RemoteEnvironmentClass reClass, string name, int codePage, int timeOut, bool isDefault, bool securityFromClientContext, string ssoApplication, string address, string ports, bool useSsl, bool serverVerificationRequired, string certificateCommonName, bool useIbmSecurityExit, string concurrentServerTransactionId)
			: base(reClassId, reClass, name, codePage, timeOut, isDefault, securityFromClientContext, ssoApplication, address, ports, useSsl, serverVerificationRequired, certificateCommonName, useIbmSecurityExit)
		{
			this.ConcurrentServerTransactionId = concurrentServerTransactionId;
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000C72F4 File Offset: 0x000C54F4
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x000C72FC File Offset: 0x000C54FC
		[DataMember]
		[Description("Concurrent Server Transaction Id")]
		[Category("CICS")]
		public string ConcurrentServerTransactionId { get; set; }
	}
}
