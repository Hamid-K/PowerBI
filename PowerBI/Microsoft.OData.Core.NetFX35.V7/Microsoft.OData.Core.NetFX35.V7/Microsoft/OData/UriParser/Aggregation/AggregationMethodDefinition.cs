using System;

namespace Microsoft.OData.UriParser.Aggregation
{
	// Token: 0x020001C2 RID: 450
	public sealed class AggregationMethodDefinition
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x0003164C File Offset: 0x0002F84C
		private AggregationMethodDefinition(AggregationMethod aggregationMethodType)
		{
			this.MethodKind = aggregationMethodType;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0003165B File Offset: 0x0002F85B
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00031663 File Offset: 0x0002F863
		public AggregationMethod MethodKind { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0003166C File Offset: 0x0002F86C
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00031674 File Offset: 0x0002F874
		public string MethodLabel { get; private set; }

		// Token: 0x060011B9 RID: 4537 RVA: 0x00031680 File Offset: 0x0002F880
		public static AggregationMethodDefinition Custom(string customMethodLabel)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(customMethodLabel, "customMethodLabel");
			return new AggregationMethodDefinition(AggregationMethod.Custom)
			{
				MethodLabel = customMethodLabel
			};
		}

		// Token: 0x040008FE RID: 2302
		public static AggregationMethodDefinition Sum = new AggregationMethodDefinition(AggregationMethod.Sum);

		// Token: 0x040008FF RID: 2303
		public static AggregationMethodDefinition Min = new AggregationMethodDefinition(AggregationMethod.Min);

		// Token: 0x04000900 RID: 2304
		public static AggregationMethodDefinition Max = new AggregationMethodDefinition(AggregationMethod.Max);

		// Token: 0x04000901 RID: 2305
		public static AggregationMethodDefinition Average = new AggregationMethodDefinition(AggregationMethod.Average);

		// Token: 0x04000902 RID: 2306
		public static AggregationMethodDefinition CountDistinct = new AggregationMethodDefinition(AggregationMethod.CountDistinct);

		// Token: 0x04000903 RID: 2307
		public static AggregationMethodDefinition VirtualPropertyCount = new AggregationMethodDefinition(AggregationMethod.VirtualPropertyCount);
	}
}
