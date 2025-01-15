using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E7 RID: 487
	internal sealed class GroupingDefinitionResolver
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x0001A194 File Offset: 0x00018394
		private GroupingDefinitionResolver(QuerySourceContext sourceContext, QueryResolutionErrorContext errorContext, IQueryDefinitionNameRegistrar queryDefinitionNames, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap)
		{
			this._errorContext = errorContext;
			this._queryExpressionResolver = new QueryExpressionResolver(sourceContext, errorContext, null, new HashSet<string>(), NoOpQueryDefinitionNameRegistrar.Instance, letMap, parameterMap);
			QuerySourceContext querySourceContext = new QuerySourceContext(Util.EmptyReadOnlyDictionary<string, ResolvedQuerySource>(), sourceContext.FederatedSchema);
			this._standaloneExpressionResolver = new QueryExpressionResolver(querySourceContext, errorContext, null, new HashSet<string>(), queryDefinitionNames, letMap, parameterMap);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001A1F4 File Offset: 0x000183F4
		internal static bool TryResolveGroupingDefinition(GroupingDefinition groupingDefinition, IConceptualSchema schema, QueryResolutionErrorContext errorContext, out ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			bool flag = true;
			resolvedGroupingDefinition = null;
			ReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> readOnlyDictionary = Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
			ImmutableDictionary<string, ResolvedQueryLetBinding> immutableDictionary = QueryResolutionUtils.CreateEmptyLetMap();
			IQueryDefinitionNameRegistrar instance = NoOpQueryDefinitionNameRegistrar.Instance;
			QuerySourceContext querySourceContext = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveQuerySources(groupingDefinition.Sources, schema.ToFederatedSchema(), errorContext, new HashSet<string>(), instance, null, immutableDictionary, readOnlyDictionary, out querySourceContext);
			}
			if (flag)
			{
				GroupingDefinitionResolver groupingDefinitionResolver = new GroupingDefinitionResolver(querySourceContext, errorContext, instance, immutableDictionary, readOnlyDictionary);
				flag &= groupingDefinitionResolver.TryResolveGroupingDefinition(groupingDefinition, schema, out resolvedGroupingDefinition);
			}
			QueryResolutionUtils.EnsureQueryDefinitionResolverContract(errorContext, flag);
			return flag;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001A264 File Offset: 0x00018464
		private bool TryResolveGroupingDefinition(GroupingDefinition groupingDefinition, IConceptualSchema schema, out ResolvedGroupingDefinition resolvedGroupingDefinition)
		{
			bool flag = true;
			List<ResolvedQuerySource> list = this._queryExpressionResolver.SourceContext.SourceMap.Values.ToList<ResolvedQuerySource>();
			List<ResolvedQueryExpression> list2;
			flag &= QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryExpression>(groupingDefinition.GroupedColumns.EmptyIfNull<QueryExpressionContainer>(), new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryExpression>(this.TryResolveQueryExpression), out list2);
			List<ResolvedGroupItem> list3 = null;
			if (groupingDefinition.GroupItems != null)
			{
				flag &= QueryResolutionUtils.TryResolveEach<GroupItem, ResolvedGroupItem>(groupingDefinition.GroupItems, new QueryResolutionUtils.TryResolveItem<GroupItem, ResolvedGroupItem>(this.TryResolveGroupItem), out list3);
			}
			ResolvedBinItem resolvedBinItem = null;
			if (groupingDefinition.BinItem != null)
			{
				flag &= this.TryResolveBinItem(groupingDefinition.BinItem, out resolvedBinItem);
			}
			ResolvedPartitionTable resolvedPartitionTable = null;
			if (groupingDefinition.PartitionTable != null)
			{
				flag &= this.TryResolvePartitionTable(groupingDefinition.PartitionTable, schema, out resolvedPartitionTable);
			}
			if (!flag)
			{
				resolvedGroupingDefinition = null;
				return false;
			}
			resolvedGroupingDefinition = new ResolvedGroupingDefinition(list, list2, list3, resolvedBinItem, resolvedPartitionTable);
			return true;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001A334 File Offset: 0x00018534
		private bool TryResolveBinItem(BinItem binItem, out ResolvedBinItem resolvedBinItem)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(binItem.Expression, out resolvedQueryExpression))
			{
				resolvedBinItem = null;
				return false;
			}
			resolvedBinItem = new ResolvedBinItem(resolvedQueryExpression);
			return true;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001A360 File Offset: 0x00018560
		private bool TryResolveGroupItem(GroupItem groupItem, out ResolvedGroupItem resolvedGroupItem)
		{
			if (groupItem.Expression == null)
			{
				resolvedGroupItem = new ResolvedGroupItem(groupItem.DisplayName, null, groupItem.BlankDefaultPlaceholder);
				return true;
			}
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(groupItem.Expression, out resolvedQueryExpression))
			{
				resolvedGroupItem = null;
				return false;
			}
			resolvedGroupItem = new ResolvedGroupItem(groupItem.DisplayName, resolvedQueryExpression, groupItem.BlankDefaultPlaceholder);
			return true;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001A3BC File Offset: 0x000185BC
		private bool TryResolvePartitionTable(PartitionTable partitionTable, IConceptualSchema schema, out ResolvedPartitionTable resolvedPartitionTable)
		{
			QueryDefinitionNameRegistrar queryDefinitionNameRegistrar = new QueryDefinitionNameRegistrar();
			queryDefinitionNameRegistrar.PushName("TableDefinition", false);
			PartitionTableDefinition definition = partitionTable.Definition;
			ResolvedQueryDefinition resolvedQueryDefinition;
			if (!QueryDefinitionResolver.TryResolveQuery(definition.TableDefinition, schema.ToFederatedSchema(), this._errorContext, new HashSet<string>(QueryNameComparer.Instance), queryDefinitionNameRegistrar, QueryResolutionUtils.CreateEmptyLetMap(), Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>(), out resolvedQueryDefinition))
			{
				resolvedPartitionTable = null;
				return false;
			}
			List<ResolvedPartition> list = null;
			if (definition.Partitions != null && !QueryResolutionUtils.TryResolveEach<Partition, ResolvedPartition>(definition.Partitions, new QueryResolutionUtils.TryResolveItem<Partition, ResolvedPartition>(this.TryResolvedPartition), out list))
			{
				resolvedPartitionTable = null;
				return false;
			}
			ResolvedPartitionTableDefinition resolvedPartitionTableDefinition = new ResolvedPartitionTableDefinition(resolvedQueryDefinition, definition.ItemIdColumns.ToList<string>(), definition.PartitionIdColumn, list, definition.DefaultPartitionPrefix);
			PartitionTableResult result = partitionTable.Result;
			ResolvedPartitionTableResult resolvedPartitionTableResult = null;
			if (result != null)
			{
				List<List<ResolvedPartitionTableIdentityMapping>> list2 = null;
				if (result.ItemIdMappings != null && !QueryResolutionUtils.TryResolveEach<IList<PartitionTableIdentityMapping>, List<ResolvedPartitionTableIdentityMapping>>(result.ItemIdMappings, new QueryResolutionUtils.TryResolveItem<IList<PartitionTableIdentityMapping>, List<ResolvedPartitionTableIdentityMapping>>(this.TryResolveIdentityMappings), out list2))
				{
					resolvedPartitionTable = null;
					return false;
				}
				resolvedPartitionTableResult = new ResolvedPartitionTableResult(result.TableName, result.PartitionIdColumn, list2);
			}
			resolvedPartitionTable = new ResolvedPartitionTable(resolvedPartitionTableDefinition, resolvedPartitionTableResult);
			return true;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001A4C8 File Offset: 0x000186C8
		private bool TryResolvedPartition(Partition partition, out ResolvedPartition resolvedPartition)
		{
			List<ResolvedQueryExpression> list;
			if (!QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryExpression>(partition.PartitionIds, new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryExpression>(this.TryResolveQueryExpression), out list))
			{
				resolvedPartition = null;
				return false;
			}
			resolvedPartition = new ResolvedPartition(partition.DisplayName, list);
			return true;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001A504 File Offset: 0x00018704
		private bool TryResolveIdentityMappings(IList<PartitionTableIdentityMapping> mappings, out List<ResolvedPartitionTableIdentityMapping> resolvedMappings)
		{
			return QueryResolutionUtils.TryResolveEach<PartitionTableIdentityMapping, ResolvedPartitionTableIdentityMapping>(mappings, new QueryResolutionUtils.TryResolveItem<PartitionTableIdentityMapping, ResolvedPartitionTableIdentityMapping>(this.TryResolveIdentityMapping), out resolvedMappings);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001A51C File Offset: 0x0001871C
		private bool TryResolveIdentityMapping(PartitionTableIdentityMapping mapping, out ResolvedPartitionTableIdentityMapping resolvedMapping)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			ResolvedQueryExpression resolvedQueryExpression2;
			if (!this.TryResolveStandaloneExpression(mapping.PartitionTableColumn, out resolvedQueryExpression) || !this.TryResolveStandaloneExpression(mapping.SourceTableColumn, out resolvedQueryExpression2))
			{
				resolvedMapping = null;
				return false;
			}
			resolvedMapping = new ResolvedPartitionTableIdentityMapping(resolvedQueryExpression, resolvedQueryExpression2);
			return true;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001A558 File Offset: 0x00018758
		private bool TryResolveQueryExpression(QueryExpressionContainer expressionContainer, out ResolvedQueryExpression resolvedExpression)
		{
			return QueryResolutionUtils.TryResolveQueryExpression(this._queryExpressionResolver, this._errorContext, expressionContainer, out resolvedExpression);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001A56D File Offset: 0x0001876D
		private bool TryResolveStandaloneExpression(QueryExpressionContainer expressionContainer, out ResolvedQueryExpression resolvedExpression)
		{
			return QueryResolutionUtils.TryResolveQueryExpression(this._standaloneExpressionResolver, this._errorContext, expressionContainer, out resolvedExpression);
		}

		// Token: 0x040006C7 RID: 1735
		private const string CandidateNameForTableDefinition = "TableDefinition";

		// Token: 0x040006C8 RID: 1736
		private readonly QueryExpressionResolver _queryExpressionResolver;

		// Token: 0x040006C9 RID: 1737
		private readonly QueryExpressionResolver _standaloneExpressionResolver;

		// Token: 0x040006CA RID: 1738
		private readonly QueryResolutionErrorContext _errorContext;
	}
}
