using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200007D RID: 125
	[DataContract]
	public class UnifiedGatewayCredentialDetails
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00004C6D File Offset: 0x00002E6D
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00004C75 File Offset: 0x00002E75
		[RequiredIfNotAnonymous]
		[DataMember(Name = "credentials", Order = 0, EmitDefaultValue = false)]
		public string Credentials { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00004C7E File Offset: 0x00002E7E
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00004C86 File Offset: 0x00002E86
		[Required]
		[DataMember(Name = "credentialType", Order = 10)]
		public CredentialType CredentialType { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00004C8F File Offset: 0x00002E8F
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00004C97 File Offset: 0x00002E97
		[Required]
		[DataMember(Name = "encryptionAlgorithm", Order = 40)]
		public string EncryptionAlgorithm { get; set; }
	}
}
