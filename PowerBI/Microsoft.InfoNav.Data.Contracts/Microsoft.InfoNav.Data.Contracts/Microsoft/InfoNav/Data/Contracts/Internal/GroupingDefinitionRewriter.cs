using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E0 RID: 480
	public sealed class GroupingDefinitionRewriter
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00019667 File Offset: 0x00017867
		private GroupingDefinitionRewriter(GroupingDefinitionRewriter.RewriterHelper helper)
		{
			this._helper = helper;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00019676 File Offset: 0x00017876
		public static GroupingDefinition Rewrite(GroupingDefinition definition, Func<IReadOnlyList<EntitySource>, QueryExpressionRewriter> expressionRewriterFactory, Func<EntitySource, EntitySource> sourceRewriter)
		{
			return new GroupingDefinitionRewriter(new GroupingDefinitionRewriter.RewriterHelper(definition, expressionRewriterFactory, sourceRewriter)).RewriteInternal();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001968C File Offset: 0x0001788C
		private GroupingDefinition RewriteInternal()
		{
			GroupingDefinition definition = this._helper.Definition;
			List<EntitySource> list = null;
			if (definition.Sources != null)
			{
				list = definition.Sources.Rewrite(new Func<EntitySource, EntitySource>(this._helper.RewriteEntitySource));
			}
			List<QueryExpressionContainer> list2 = null;
			if (definition.GroupedColumns != null)
			{
				list2 = definition.GroupedColumns.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this._helper.RewriteTopLevelExpression));
			}
			List<GroupItem> list3 = null;
			if (definition.GroupItems != null)
			{
				list3 = definition.GroupItems.Rewrite(new Func<GroupItem, GroupItem>(this.RewriteGroupItem));
			}
			BinItem binItem = null;
			if (definition.BinItem != null)
			{
				binItem = this.RewriteBinItem(definition.BinItem);
			}
			PartitionTable partitionTable = null;
			if (definition.PartitionTable != null)
			{
				partitionTable = this.RewritePartitionTable(definition.PartitionTable);
			}
			if (list == definition.Sources && list2 == definition.GroupedColumns && list3 == definition.GroupItems && binItem == definition.BinItem && partitionTable == definition.PartitionTable)
			{
				return definition;
			}
			return new GroupingDefinition
			{
				Version = definition.Version,
				Sources = list,
				GroupedColumns = list2,
				GroupItems = list3,
				BinItem = binItem,
				PartitionTable = partitionTable
			};
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000197BC File Offset: 0x000179BC
		private GroupItem RewriteGroupItem(GroupItem groupItem)
		{
			QueryExpressionContainer queryExpressionContainer = null;
			if (groupItem.Expression != null)
			{
				queryExpressionContainer = this._helper.RewriteTopLevelExpression(groupItem.Expression);
			}
			if (queryExpressionContainer == groupItem.Expression)
			{
				return groupItem;
			}
			return new GroupItem
			{
				DisplayName = groupItem.DisplayName,
				Expression = queryExpressionContainer,
				BlankDefaultPlaceholder = groupItem.BlankDefaultPlaceholder
			};
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0001981C File Offset: 0x00017A1C
		private BinItem RewriteBinItem(BinItem binItem)
		{
			QueryExpressionContainer queryExpressionContainer = this._helper.RewriteTopLevelExpression(binItem.Expression);
			if (queryExpressionContainer == binItem.Expression)
			{
				return binItem;
			}
			return new BinItem
			{
				Expression = queryExpressionContainer
			};
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00019854 File Offset: 0x00017A54
		private PartitionTable RewritePartitionTable(PartitionTable partitionTable)
		{
			PartitionTableDefinition partitionTableDefinition = this.RewritePartitionTableDefinition(partitionTable.Definition);
			PartitionTableResult partitionTableResult = this.RewritePartitionTableResult(partitionTable.Result);
			if (partitionTableDefinition == partitionTable.Definition && partitionTableResult == partitionTable.Result)
			{
				return partitionTable;
			}
			return new PartitionTable
			{
				Definition = partitionTableDefinition,
				Result = partitionTableResult
			};
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000198A4 File Offset: 0x00017AA4
		private PartitionTableDefinition RewritePartitionTableDefinition(PartitionTableDefinition partitionTableDefinition)
		{
			if (partitionTableDefinition == null)
			{
				return null;
			}
			List<Partition> list = partitionTableDefinition.Partitions.Rewrite(new Func<Partition, Partition>(this.RewritePartition));
			QueryDefinition queryDefinition = this._helper.Rewrite(partitionTableDefinition.TableDefinition);
			if (list == partitionTableDefinition.Partitions && queryDefinition == partitionTableDefinition.TableDefinition)
			{
				return partitionTableDefinition;
			}
			return new PartitionTableDefinition
			{
				Partitions = list,
				TableDefinition = queryDefinition,
				DefaultPartitionPrefix = partitionTableDefinition.DefaultPartitionPrefix,
				ItemIdColumns = partitionTableDefinition.ItemIdColumns,
				PartitionIdColumn = partitionTableDefinition.PartitionIdColumn
			};
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00019934 File Offset: 0x00017B34
		private PartitionTableResult RewritePartitionTableResult(PartitionTableResult tableResult)
		{
			if (tableResult == null || tableResult.ItemIdMappings == null)
			{
				return tableResult;
			}
			IList<IList<PartitionTableIdentityMapping>> list2 = tableResult.ItemIdMappings.Rewrite((IList<PartitionTableIdentityMapping> list) => list.Rewrite(new Func<PartitionTableIdentityMapping, PartitionTableIdentityMapping>(this.RewriteIdentityMapping)));
			if (list2 == tableResult.ItemIdMappings)
			{
				return tableResult;
			}
			return new PartitionTableResult
			{
				TableName = tableResult.TableName,
				PartitionIdColumn = tableResult.PartitionIdColumn,
				ItemIdMappings = list2
			};
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0001999C File Offset: 0x00017B9C
		private Partition RewritePartition(Partition partition)
		{
			if (partition == null)
			{
				return null;
			}
			List<QueryExpressionContainer> list = partition.PartitionIds.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this._helper.RewriteStandaloneExpression));
			if (list == partition.PartitionIds)
			{
				return partition;
			}
			return new Partition
			{
				DisplayName = partition.DisplayName,
				PartitionIds = list
			};
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000199F4 File Offset: 0x00017BF4
		private PartitionTableIdentityMapping RewriteIdentityMapping(PartitionTableIdentityMapping mapping)
		{
			QueryExpressionContainer queryExpressionContainer = this._helper.RewriteStandaloneExpression(mapping.PartitionTableColumn);
			QueryExpressionContainer queryExpressionContainer2 = this._helper.RewriteStandaloneExpression(mapping.SourceTableColumn);
			if (queryExpressionContainer == mapping.PartitionTableColumn && queryExpressionContainer2 == mapping.SourceTableColumn)
			{
				return mapping;
			}
			return new PartitionTableIdentityMapping
			{
				PartitionTableColumn = queryExpressionContainer,
				SourceTableColumn = queryExpressionContainer2
			};
		}

		// Token: 0x040006B4 RID: 1716
		private readonly GroupingDefinitionRewriter.RewriterHelper _helper;

		// Token: 0x02000328 RID: 808
		private sealed class RewriterHelper
		{
			// Token: 0x060019DE RID: 6622 RVA: 0x0002E83B File Offset: 0x0002CA3B
			internal RewriterHelper(GroupingDefinition definition, Func<IReadOnlyList<EntitySource>, QueryExpressionRewriter> expressionRewriterFactory, Func<EntitySource, EntitySource> sourceRewriter)
			{
				this._expressionRewriterFactory = expressionRewriterFactory;
				this._sourceRewriter = sourceRewriter;
				this._topLevelRewriter = expressionRewriterFactory(definition.Sources);
				this._standaloneRewriter = expressionRewriterFactory(Util.EmptyReadOnlyCollection<EntitySource>());
				this.Definition = definition;
			}

			// Token: 0x060019DF RID: 6623 RVA: 0x0002E87B File Offset: 0x0002CA7B
			internal EntitySource RewriteEntitySource(EntitySource source)
			{
				source = this._sourceRewriter(source);
				if (source.Expression != null)
				{
					return this.RewriteEntitySourceSubquery(source);
				}
				return source;
			}

			// Token: 0x060019E0 RID: 6624 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
			private EntitySource RewriteEntitySourceSubquery(EntitySource source)
			{
				QuerySubqueryExpression subquery = source.Expression.Subquery;
				QueryDefinition queryDefinition = this.Rewrite(subquery.Query);
				if (subquery.Query == queryDefinition)
				{
					return source;
				}
				return new EntitySource
				{
					Entity = source.Entity,
					EntitySet = source.EntitySet,
					Expression = new QuerySubqueryExpression
					{
						Query = queryDefinition
					},
					Name = source.Name,
					Schema = source.Schema,
					Type = source.Type
				};
			}

			// Token: 0x060019E1 RID: 6625 RVA: 0x0002E930 File Offset: 0x0002CB30
			internal QueryDefinition Rewrite(QueryDefinition definition)
			{
				QueryExpressionRewriter queryExpressionRewriter = this._expressionRewriterFactory(definition.From);
				return QueryDefinitionRewriter.Rewrite(definition, queryExpressionRewriter, new Func<EntitySource, EntitySource>(this.RewriteEntitySource));
			}

			// Token: 0x060019E2 RID: 6626 RVA: 0x0002E962 File Offset: 0x0002CB62
			internal QueryExpressionContainer RewriteTopLevelExpression(QueryExpressionContainer expression)
			{
				return this.RewriteExpression(expression, this._topLevelRewriter);
			}

			// Token: 0x060019E3 RID: 6627 RVA: 0x0002E971 File Offset: 0x0002CB71
			internal QueryExpressionContainer RewriteStandaloneExpression(QueryExpressionContainer expression)
			{
				return this.RewriteExpression(expression, this._standaloneRewriter);
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x0002E980 File Offset: 0x0002CB80
			private QueryExpressionContainer RewriteExpression(QueryExpressionContainer expressionContainer, QueryExpressionRewriter rewriter)
			{
				QueryExpression expression = expressionContainer.Expression;
				QueryExpression queryExpression = expression.Accept<QueryExpression>(rewriter);
				if (expression == queryExpression)
				{
					return expressionContainer;
				}
				return new QueryExpressionContainer(queryExpression, expressionContainer.Name, expressionContainer.NativeReferenceName);
			}

			// Token: 0x040009A7 RID: 2471
			private readonly Func<IReadOnlyList<EntitySource>, QueryExpressionRewriter> _expressionRewriterFactory;

			// Token: 0x040009A8 RID: 2472
			private readonly Func<EntitySource, EntitySource> _sourceRewriter;

			// Token: 0x040009A9 RID: 2473
			private readonly QueryExpressionRewriter _topLevelRewriter;

			// Token: 0x040009AA RID: 2474
			private readonly QueryExpressionRewriter _standaloneRewriter;

			// Token: 0x040009AB RID: 2475
			internal readonly GroupingDefinition Definition;
		}
	}
}
