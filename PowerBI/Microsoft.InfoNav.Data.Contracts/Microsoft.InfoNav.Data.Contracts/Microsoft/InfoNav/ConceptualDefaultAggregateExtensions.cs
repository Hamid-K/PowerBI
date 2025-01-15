using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001E RID: 30
	internal static class ConceptualDefaultAggregateExtensions
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000025AF File Offset: 0x000007AF
		internal static bool IsDefault(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Default;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000025B5 File Offset: 0x000007B5
		internal static bool IsDoNotSummarize(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.None;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000025BB File Offset: 0x000007BB
		internal static bool IsSum(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Sum;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000025C1 File Offset: 0x000007C1
		internal static bool IsCount(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Count;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000025C7 File Offset: 0x000007C7
		internal static bool IsDistinctCount(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.DistinctCount;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000025CD File Offset: 0x000007CD
		internal static bool IsMin(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Min;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000025D3 File Offset: 0x000007D3
		internal static bool IsMax(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Max;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000025D9 File Offset: 0x000007D9
		internal static bool IsAverage(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate == ConceptualDefaultAggregate.Average;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000025DF File Offset: 0x000007DF
		internal static bool IsDefaultOrDoNotSummarize(this ConceptualDefaultAggregate aggregate)
		{
			return aggregate.IsDefault() || aggregate.IsDoNotSummarize();
		}
	}
}
