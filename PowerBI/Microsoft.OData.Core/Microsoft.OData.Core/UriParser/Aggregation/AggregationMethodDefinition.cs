using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001FF RID: 511
	public sealed class AggregationMethodDefinition
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x0003F3AA File Offset: 0x0003D5AA
		private AggregationMethodDefinition(AggregationMethod aggregationMethodType)
		{
			this.MethodKind = aggregationMethodType;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0003F3B9 File Offset: 0x0003D5B9
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x0003F3C1 File Offset: 0x0003D5C1
		public AggregationMethod MethodKind { get; private set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x0003F3CA File Offset: 0x0003D5CA
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x0003F3D2 File Offset: 0x0003D5D2
		public string MethodLabel { get; private set; }

		// Token: 0x060016A1 RID: 5793 RVA: 0x0003F3DC File Offset: 0x0003D5DC
		public static AggregationMethodDefinition Custom(string customMethodLabel)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(customMethodLabel, "customMethodLabel");
			return new AggregationMethodDefinition(AggregationMethod.Custom)
			{
				MethodLabel = customMethodLabel
			};
		}

		// Token: 0x04000A3A RID: 2618
		public static AggregationMethodDefinition Sum = new AggregationMethodDefinition(AggregationMethod.Sum);

		// Token: 0x04000A3B RID: 2619
		public static AggregationMethodDefinition Min = new AggregationMethodDefinition(AggregationMethod.Min);

		// Token: 0x04000A3C RID: 2620
		public static AggregationMethodDefinition Max = new AggregationMethodDefinition(AggregationMethod.Max);

		// Token: 0x04000A3D RID: 2621
		public static AggregationMethodDefinition Average = new AggregationMethodDefinition(AggregationMethod.Average);

		// Token: 0x04000A3E RID: 2622
		public static AggregationMethodDefinition CountDistinct = new AggregationMethodDefinition(AggregationMethod.CountDistinct);

		// Token: 0x04000A3F RID: 2623
		public static AggregationMethodDefinition VirtualPropertyCount = new AggregationMethodDefinition(AggregationMethod.VirtualPropertyCount);
	}
}
