using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000CD RID: 205
	internal static class StatisticsGenerator
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00007DA8 File Offset: 0x00005FA8
		internal static ContractConceptualSchemaStatistics CreateStatistics(this IConceptualSchema schema)
		{
			List<ContractConceptualEntitySetStatistics> list = new List<ContractConceptualEntitySetStatistics>(schema.Entities.Count);
			int num = 0;
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				if (conceptualEntity is IConceptualPod)
				{
					num++;
				}
				else
				{
					ConceptualEntityStatistics statistics = conceptualEntity.Statistics;
					if (statistics != null)
					{
						ConceptualPrimitiveType? conceptualPrimitiveType = null;
						if (statistics.HasDefaultLabel)
						{
							conceptualPrimitiveType = new ConceptualPrimitiveType?(statistics.DefaultLabelDataType);
						}
						ConceptualPrimitiveType? conceptualPrimitiveType2 = null;
						if (statistics.HasDefaultImage)
						{
							conceptualPrimitiveType2 = new ConceptualPrimitiveType?(statistics.DefaultImageDataType);
						}
						ContractConceptualEntitySetStatistics contractConceptualEntitySetStatistics = new ContractConceptualEntitySetStatistics(conceptualEntity.GetRowCount(), statistics.ColumnCount, statistics.MeasureCount, statistics.KpiCount, statistics.HierarchyCount, statistics.HiddenColumnCount, statistics.OrderByColumnCount, statistics.IsDateTable, conceptualPrimitiveType, statistics.RowIdentifierDataTypes, conceptualPrimitiveType2, statistics.DefaultFieldSetDataTypes, statistics.KeepUniqueRowsColumnsCount, statistics.ColumnCountByDataCategory.ToDictionary<ConceptualDataCategory, int>(), statistics.ColumnCountByDataType.ToDictionary<ConceptualPrimitiveType, int>(), statistics.ColumnCountByDefaultAggregate.ToDictionary<ConceptualDefaultAggregate, int>());
						list.Add(contractConceptualEntitySetStatistics);
					}
				}
			}
			return new ContractConceptualSchemaStatistics(list, StatisticsGenerator.GetConceptualSchemaTelemetryIdentifier(schema, list, num), (schema.Statistics != null) ? schema.Statistics.ActiveRelationshipCount : 0, (schema.Statistics != null) ? schema.Statistics.InactiveRelationshipCount : 0, (schema.Statistics != null) ? schema.Statistics.IslandSubgraphCount : 0, (schema.Statistics != null) ? schema.Statistics.InflectionPointsCount : 0, num);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00007F50 File Offset: 0x00006150
		private static int GetConceptualSchemaTelemetryIdentifier(IConceptualSchema schema, List<ContractConceptualEntitySetStatistics> contractTableStats, int podCount)
		{
			List<object> list = new List<object>();
			if (schema.Statistics != null)
			{
				list.Add(schema.Statistics.ActiveRelationshipCount);
				list.Add(schema.Statistics.InactiveRelationshipCount);
				list.Add(schema.Statistics.InflectionPointsCount);
				list.Add(schema.Statistics.IslandSubgraphCount);
				list.Add(schema.Entities.Count);
			}
			list.AddRange(schema.Entities.Select(StatisticsGenerator.ConceptualEntityEdmNameSelector));
			foreach (ContractConceptualEntitySetStatistics contractConceptualEntitySetStatistics in contractTableStats)
			{
				list.Add(contractConceptualEntitySetStatistics.ColumnCount);
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDataCategory.Keys.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDataCategory.Values.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDataType.Keys.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDataType.Values.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDefaultAggregate.Keys.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.ColumnCountByDefaultAggregate.Values.Cast<object>());
				list.AddRange(contractConceptualEntitySetStatistics.DefaultFieldSetDataTypes.Cast<object>());
				list.Add(contractConceptualEntitySetStatistics.DefaultImageDataType);
				list.Add(contractConceptualEntitySetStatistics.DefaultLabelDataType);
				list.Add(contractConceptualEntitySetStatistics.HiddenColumnCount);
				list.Add(contractConceptualEntitySetStatistics.HierarchyCount);
				list.Add(contractConceptualEntitySetStatistics.IsDateTable);
				list.Add(contractConceptualEntitySetStatistics.IsHidden);
				list.Add(contractConceptualEntitySetStatistics.KeepUniqueRowsColumnsCount);
				list.Add(contractConceptualEntitySetStatistics.KpiCount);
				list.Add(contractConceptualEntitySetStatistics.MeasureCount);
				list.Add(contractConceptualEntitySetStatistics.OrderByColumnCount);
				list.AddRange(contractConceptualEntitySetStatistics.RowIdentifierDataTypes.Cast<object>());
			}
			return Hashing.CombineHash<object>(list, null);
		}

		// Token: 0x040004D2 RID: 1234
		private static readonly Func<IConceptualEntity, string> ConceptualEntityEdmNameSelector = (IConceptualEntity e) => e.EdmName;
	}
}
