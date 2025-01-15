using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000059 RID: 89
	[DataContract]
	public sealed class GatewayVersionDetails
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000043B6 File Offset: 0x000025B6
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000043BE File Offset: 0x000025BE
		[DataMember(Name = "release")]
		public GatewayReleaseVersion Release { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000043C7 File Offset: 0x000025C7
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000043CF File Offset: 0x000025CF
		[DataMember(Name = "releaseLabels")]
		public GatewayReleaseLabel[] ReleaseLabels { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000043D8 File Offset: 0x000025D8
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000043E0 File Offset: 0x000025E0
		[DataMember(Name = "releaseLabelRules")]
		public ReleaseLabelRule[] ReleaseLabelRules { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000043E9 File Offset: 0x000025E9
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000043F1 File Offset: 0x000025F1
		[DataMember(Name = "releaseStatusRules")]
		public ReleaseStatusRule[] ReleaseStatusRules { get; set; }
	}
}
