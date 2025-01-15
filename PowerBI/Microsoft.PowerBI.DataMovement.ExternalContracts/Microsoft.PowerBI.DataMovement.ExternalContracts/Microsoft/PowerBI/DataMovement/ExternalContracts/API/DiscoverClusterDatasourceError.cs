using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000044 RID: 68
	[DataContract]
	public sealed class DiscoverClusterDatasourceError
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003B34 File Offset: 0x00001D34
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00003B3C File Offset: 0x00001D3C
		[DataMember(Name = "gatewayId")]
		public long GatewayId { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00003B45 File Offset: 0x00001D45
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00003B4D File Offset: 0x00001D4D
		[DataMember(Name = "gatewayObjectId")]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003B56 File Offset: 0x00001D56
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00003B5E File Offset: 0x00001D5E
		[DataMember(Name = "gatewayName")]
		public string GatewayName { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003B67 File Offset: 0x00001D67
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00003B6F File Offset: 0x00001D6F
		[DataMember(Name = "gatewayAnnotation")]
		public string GatewayAnnotation { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003B78 File Offset: 0x00001D78
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00003B80 File Offset: 0x00001D80
		[DataMember(Name = "errorMessage")]
		public string ErrorMessage { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003B89 File Offset: 0x00001D89
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00003B91 File Offset: 0x00001D91
		[DataMember(Name = "errorCode")]
		public string ErrorCode { get; set; }
	}
}
