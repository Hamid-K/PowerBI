using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations.Statistics;
using Microsoft.Lucia.Core.DomainModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000080 RID: 128
	public static class ConceptualStatisticsExtensions
	{
		// Token: 0x06000241 RID: 577 RVA: 0x000053F8 File Offset: 0x000035F8
		public static ConceptualColumnStatistics GetStatistics(this IConceptualColumn column)
		{
			ConceptualColumnStatistics statistics = column.GetStatistics();
			if (statistics != null)
			{
				return statistics;
			}
			IColumnStatisticsAnnotation columnStatisticsAnnotation;
			if (column.TryGetAnnotation(out columnStatisticsAnnotation))
			{
				return columnStatisticsAnnotation.ColumnStatistics;
			}
			return null;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005424 File Offset: 0x00003624
		public static int? GetRowCount(this IConceptualEntity entity)
		{
			int? rowCount = entity.GetRowCount();
			if (rowCount != null)
			{
				return rowCount;
			}
			Microsoft.Lucia.Core.DomainModel.EntityRowCountAnnotation entityRowCountAnnotation;
			if (entity.TryGetAnnotation(out entityRowCountAnnotation))
			{
				return new int?(entityRowCountAnnotation.RowCount);
			}
			return null;
		}
	}
}
