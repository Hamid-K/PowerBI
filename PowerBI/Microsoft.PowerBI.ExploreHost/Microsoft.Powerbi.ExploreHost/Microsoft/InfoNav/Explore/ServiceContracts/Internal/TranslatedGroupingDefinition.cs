using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.ServiceContracts.QueryTranslation;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000008 RID: 8
	public sealed class TranslatedGroupingDefinition
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020A4 File Offset: 0x000002A4
		public TranslatedGroupingDefinition(string groupingColumnDaxExpression)
		{
			this.GroupingColumnDaxExpression = groupingColumnDaxExpression;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B3 File Offset: 0x000002B3
		public TranslatedGroupingDefinition(TranslatedPartitionTable partitionTable)
		{
			this.PartitionTable = partitionTable;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C2 File Offset: 0x000002C2
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020CA File Offset: 0x000002CA
		public string GroupingColumnDaxExpression { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020D3 File Offset: 0x000002D3
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020DB File Offset: 0x000002DB
		public TranslatedPartitionTable PartitionTable { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000020E4 File Offset: 0x000002E4
		internal static TranslatedGroupingDefinition Create(SemanticQueryToDaxTranslationResult innerResult)
		{
			ClusteringTranslationResult clusteringTranslationResult = innerResult.ClusteringTranslationResult;
			if (clusteringTranslationResult != null)
			{
				PartitionTableResult partitionTableResult = new PartitionTableResult();
				partitionTableResult.ItemIdMappings = clusteringTranslationResult.ItemIdMappings.Select((IReadOnlyList<PartitionTableIdentityMapping> m) => m.Evaluate<PartitionTableIdentityMapping>()).ToList<IList<PartitionTableIdentityMapping>>();
				partitionTableResult.PartitionIdColumn = clusteringTranslationResult.PartitionIdColumnName;
				partitionTableResult.TableName = clusteringTranslationResult.MappingTableName;
				PartitionTableResult partitionTableResult2 = partitionTableResult;
				return new TranslatedGroupingDefinition(new TranslatedPartitionTable(clusteringTranslationResult.MappingTableExpression, clusteringTranslationResult.GroupingColumnExpression, clusteringTranslationResult.PartitionIdColumnName, partitionTableResult2));
			}
			return new TranslatedGroupingDefinition(innerResult.DaxExpression);
		}
	}
}
