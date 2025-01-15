using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ModelReferences;

namespace Microsoft.InfoNav.Data.Contracts.Grouping
{
	// Token: 0x020000EE RID: 238
	public static class GroupingSchemaReferenceExtractor
	{
		// Token: 0x06000648 RID: 1608 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public static ISet<QueryExpression> ExtractReferences(GroupingDefinition definition)
		{
			HashSet<QueryExpression> hashSet = new HashSet<QueryExpression>();
			IEnumerable<QueryExpressionContainer> enumerable = Util.EmptyReadOnlyCollection<QueryExpressionContainer>();
			if (!definition.GroupedColumns.IsNullOrEmpty<QueryExpressionContainer>())
			{
				enumerable = enumerable.Concat(definition.GroupedColumns);
			}
			if (!definition.GroupItems.IsNullOrEmpty<GroupItem>())
			{
				enumerable = enumerable.Concat(from item in definition.GroupItems
					select item.Expression into e
					where e != null
					select e);
			}
			if (definition.BinItem != null)
			{
				enumerable = enumerable.Concat(definition.BinItem.Expression.ArrayWrap<QueryExpressionContainer>());
			}
			SchemaReferenceExtractor.ExtractReferences(definition.Sources, enumerable.ToList<QueryExpressionContainer>(), hashSet);
			PartitionTable partitionTable = definition.PartitionTable;
			if (partitionTable != null)
			{
				SchemaReferenceExtractor.ExtractReferences(partitionTable.Definition.TableDefinition, hashSet);
				PartitionTableResult result = partitionTable.Result;
				if (result != null)
				{
					QuerySourceRefExpression querySourceRefExpression = new QuerySourceRefExpression
					{
						Entity = result.TableName
					};
					hashSet.Add(querySourceRefExpression);
					if (!string.IsNullOrEmpty(result.PartitionIdColumn))
					{
						hashSet.Add(querySourceRefExpression.Column(result.PartitionIdColumn));
					}
					if (result.ItemIdMappings != null)
					{
						foreach (PartitionTableIdentityMapping partitionTableIdentityMapping in result.ItemIdMappings.SelectMany<PartitionTableIdentityMapping>())
						{
							hashSet.Add(partitionTableIdentityMapping.SourceTableColumn.Expression);
							hashSet.Add(partitionTableIdentityMapping.PartitionTableColumn.Expression);
						}
					}
				}
			}
			return hashSet;
		}
	}
}
