using System;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000016 RID: 22
	[DataContract]
	public class GatewayCredentialDetails : CredentialDetails
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002B28 File Offset: 0x00000D28
		public GatewayCredentialDetails()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002B30 File Offset: 0x00000D30
		public GatewayCredentialDetails(CredentialDetails original)
			: base(original)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002B39 File Offset: 0x00000D39
		public GatewayCredentialDetails(GatewayCredentialDetails original)
			: base(original)
		{
			this.EffectiveUserName = original.EffectiveUserName;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B4E File Offset: 0x00000D4E
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002B56 File Offset: 0x00000D56
		[DataMember(Name = "effectiveUserName", Order = 45)]
		public string EffectiveUserName { get; set; }
	}
}
