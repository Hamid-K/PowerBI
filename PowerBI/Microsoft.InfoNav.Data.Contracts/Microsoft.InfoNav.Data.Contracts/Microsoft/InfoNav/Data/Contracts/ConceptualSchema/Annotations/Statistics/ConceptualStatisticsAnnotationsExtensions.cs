using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics
{
	// Token: 0x0200013C RID: 316
	public static class ConceptualStatisticsAnnotationsExtensions
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public static ConceptualColumnStatistics GetStatistics(this IConceptualColumn column)
		{
			ColumnStatisticsAnnotation columnStatisticsAnnotation;
			if (!column.TryGetAnnotation(out columnStatisticsAnnotation))
			{
				return null;
			}
			return columnStatisticsAnnotation.Statistics;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00010CDC File Offset: 0x0000EEDC
		public static bool HasStatistics(this IConceptualColumn column, out ConceptualColumnStatistics statistics)
		{
			statistics = column.GetStatistics();
			return statistics != null;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00010CEC File Offset: 0x0000EEEC
		public static int? GetRowCount(this IConceptualEntity entity)
		{
			EntityRowCountAnnotation entityRowCountAnnotation;
			if (entity.TryGetAnnotation(out entityRowCountAnnotation))
			{
				return new int?(entityRowCountAnnotation.RowCount);
			}
			return null;
		}
	}
}
