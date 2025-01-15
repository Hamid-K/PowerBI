using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000080 RID: 128
	[DataContract]
	public class UnifiedGatewayPrincipalEntryResponse : UnifiedGatewayPermission
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00004DC8 File Offset: 0x00002FC8
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00004DD0 File Offset: 0x00002FD0
		[Required]
		[DataMember(Name = "objectId")]
		public string ObjectId { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00004DD9 File Offset: 0x00002FD9
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00004DE1 File Offset: 0x00002FE1
		[Required]
		[DataMember(Name = "principalType")]
		public string PrincipalType { get; set; }
	}
}
