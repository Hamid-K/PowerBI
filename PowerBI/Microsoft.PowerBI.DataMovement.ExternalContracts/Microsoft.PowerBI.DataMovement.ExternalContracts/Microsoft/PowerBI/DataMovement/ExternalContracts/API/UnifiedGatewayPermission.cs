using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200007F RID: 127
	[DataContract]
	public class UnifiedGatewayPermission
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00004D7C File Offset: 0x00002F7C
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00004D84 File Offset: 0x00002F84
		[Required]
		[DataMember(Name = "tenantId")]
		public string TentanId { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00004D8D File Offset: 0x00002F8D
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00004D95 File Offset: 0x00002F95
		[Required]
		[DataMember(Name = "role")]
		public string Role { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00004D9E File Offset: 0x00002F9E
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00004DA6 File Offset: 0x00002FA6
		[DataMember(Name = "allowedDataSourceTypes", EmitDefaultValue = false)]
		public IList<string> AllowedDataSourceTypes { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00004DAF File Offset: 0x00002FAF
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00004DB7 File Offset: 0x00002FB7
		[Required]
		[DataMember(Name = "clusterId", EmitDefaultValue = false)]
		public string ClusterId { get; set; }
	}
}
