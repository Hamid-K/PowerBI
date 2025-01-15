using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F5 RID: 501
	[DataContract]
	public sealed class QueryBindingDescriptorAggregateContainer
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0001A7AC File Offset: 0x000189AC
		// (set) Token: 0x06000D9E RID: 3486 RVA: 0x0001A7B4 File Offset: 0x000189B4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public DataShapeBindingPercentileAggregate Percentile { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0001A7BD File Offset: 0x000189BD
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0001A7C5 File Offset: 0x000189C5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBindingMedianAggregate Median { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0001A7CE File Offset: 0x000189CE
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0001A7D6 File Offset: 0x000189D6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DataShapeBindingMinAggregate Min { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0001A7DF File Offset: 0x000189DF
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0001A7E7 File Offset: 0x000189E7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public DataShapeBindingMaxAggregate Max { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0001A7F0 File Offset: 0x000189F0
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0001A7F8 File Offset: 0x000189F8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DataShapeBindingAverageAggregate Average { get; set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0001A801 File Offset: 0x00018A01
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x0001A809 File Offset: 0x00018A09
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public AggregateScope Scope { get; set; }
	}
}
