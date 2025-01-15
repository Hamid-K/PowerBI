using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000268 RID: 616
	public sealed class QueryDefinitionResolver
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x000203D9 File Offset: 0x0001E5D9
		private QueryDefinitionResolver(QuerySourceContext sourceContext, QueryResolutionErrorContext errorContext, QueryTransformTableContext transformContext, HashSet<string> querySourceUsedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap)
		{
			this._errorContext = errorContext;
			this._transformTableContext = transformContext;
			this._queryExpressionResolver = new QueryExpressionResolver(sourceContext, errorContext, this._transformTableContext, querySourceUsedNames, queryDefinitionNames, letMap, parameterMap);
			this._queryDefinitionNames = queryDefinitionNames;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00020412 File Offset: 0x0001E612
		public static bool TryResolveQuery(QueryDefinition query, IConceptualSchema schema, QueryResolutionErrorContext errorContext, out ResolvedQueryDefinition resolvedQuery)
		{
			return QueryDefinitionResolver.TryResolveQuery(query, schema.ToFederatedSchema(), errorContext, out resolvedQuery);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00020422 File Offset: 0x0001E622
		internal static bool TryResolveQuery(QueryDefinition query, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, out ResolvedQueryDefinition resolvedQuery)
		{
			return QueryDefinitionResolver.TryResolveQuery(query, federatedSchema, errorContext, new HashSet<string>(QueryNameComparer.Instance), out resolvedQuery);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00020437 File Offset: 0x0001E637
		internal static bool TryResolveQuery(QueryDefinition query, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> usedNames, out ResolvedQueryDefinition resolvedQuery)
		{
			return QueryDefinitionResolver.TryResolveQuery(query, federatedSchema, errorContext, usedNames, NoOpQueryDefinitionNameRegistrar.Instance, null, null, out resolvedQuery);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0002044B File Offset: 0x0001E64B
		internal static bool TryResolveQuery(QueryDefinition query, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> usedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, out ResolvedQueryDefinition resolvedQuery)
		{
			return QueryDefinitionResolver.TryResolveQuery(query, federatedSchema, errorContext, usedNames, queryDefinitionNames, null, null, out resolvedQuery);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0002045C File Offset: 0x0001E65C
		internal static bool TryResolveQuery(QueryDefinition query, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> usedNames, IQueryDefinitionNameRegistrar queryDefinitionNames, ImmutableDictionary<string, ResolvedQueryLetBinding> letMap, IReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> parameterMap, out ResolvedQueryDefinition resolvedQuery)
		{
			bool flag = true;
			resolvedQuery = null;
			QueryDefinitionResolver.ValidationErrorContext validationErrorContext = new QueryDefinitionResolver.ValidationErrorContext(errorContext);
			new QueryDefinitionValidator(new QueryExpressionValidator(validationErrorContext)).Visit(validationErrorContext, query);
			flag &= !validationErrorContext.HasError;
			IReadOnlyList<ResolvedQueryParameterDeclaration> readOnlyList = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveQueryParameterDeclarations(query.Parameters, federatedSchema, errorContext, usedNames, queryDefinitionNames, parameterMap, out readOnlyList, out parameterMap);
			}
			letMap = letMap ?? QueryResolutionUtils.CreateEmptyLetMap();
			IReadOnlyList<ResolvedQueryLetBinding> readOnlyList2 = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveLetBindings(query.Let, federatedSchema, errorContext, usedNames, queryDefinitionNames, letMap, parameterMap, out readOnlyList2, out letMap);
			}
			QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
			QuerySourceContext querySourceContext = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveQuerySources(query.From, federatedSchema, errorContext, usedNames, queryDefinitionNames, queryTransformTableContext, letMap, parameterMap, out querySourceContext);
			}
			if (flag)
			{
				QueryDefinitionResolver queryDefinitionResolver = new QueryDefinitionResolver(querySourceContext, errorContext, queryTransformTableContext, usedNames, queryDefinitionNames, letMap, parameterMap);
				flag &= queryDefinitionResolver.TryResolveQueryDefinition(query, readOnlyList, readOnlyList2, out resolvedQuery);
			}
			QueryResolutionUtils.EnsureQueryDefinitionResolverContract(errorContext, flag);
			return flag;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00020534 File Offset: 0x0001E734
		internal static bool TryResolveFilter(FilterDefinition filter, IConceptualSchema schema, QueryResolutionErrorContext errorContext, out ResolvedFilterDefinition resolvedFilter)
		{
			return QueryDefinitionResolver.TryResolveFilter(filter, schema.ToFederatedSchema(), errorContext, out resolvedFilter);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00020544 File Offset: 0x0001E744
		internal static bool TryResolveFilter(FilterDefinition filter, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, out ResolvedFilterDefinition resolvedFilter)
		{
			return QueryDefinitionResolver.TryResolveFilter(filter, federatedSchema, errorContext, new HashSet<string>(), out resolvedFilter);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00020554 File Offset: 0x0001E754
		internal static bool TryResolveFilter(FilterDefinition filter, IFederatedConceptualSchema federatedSchema, QueryResolutionErrorContext errorContext, HashSet<string> usedNames, out ResolvedFilterDefinition resolvedFilter)
		{
			bool flag = true;
			resolvedFilter = null;
			QueryDefinitionResolver.ValidationErrorContext validationErrorContext = new QueryDefinitionResolver.ValidationErrorContext(errorContext);
			new QueryDefinitionValidator(new QueryExpressionValidator(validationErrorContext)).Visit(validationErrorContext, filter);
			flag &= !validationErrorContext.HasError;
			ReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> readOnlyDictionary = Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
			ImmutableDictionary<string, ResolvedQueryLetBinding> immutableDictionary = QueryResolutionUtils.CreateEmptyLetMap();
			NoOpQueryDefinitionNameRegistrar instance = NoOpQueryDefinitionNameRegistrar.Instance;
			QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
			QuerySourceContext querySourceContext = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveQuerySources(filter.From, federatedSchema, errorContext, usedNames, instance, queryTransformTableContext, immutableDictionary, readOnlyDictionary, out querySourceContext);
			}
			if (flag)
			{
				QueryDefinitionResolver queryDefinitionResolver = new QueryDefinitionResolver(querySourceContext, errorContext, queryTransformTableContext, usedNames, instance, immutableDictionary, readOnlyDictionary);
				flag &= queryDefinitionResolver.TryResolverFilterDefinition(filter, out resolvedFilter);
			}
			QueryResolutionUtils.EnsureQueryDefinitionResolverContract(errorContext, flag);
			return flag;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000205F0 File Offset: 0x0001E7F0
		internal static bool TryResolveQueryFilters(List<QueryFilter> queryFilters, IFederatedConceptualSchema federatedSchema, List<EntitySource> from, QueryResolutionErrorContext errorContext, HashSet<string> usedNames, out List<ResolvedQueryFilter> resolvedQueryFilters)
		{
			bool flag = true;
			resolvedQueryFilters = null;
			if (queryFilters.Any((QueryFilter f) => !QueryDefinitionValidator.IsValid(f)))
			{
				flag = false;
				errorContext.RegisterError(QueryResolutionMessages.InvalidQueryFilters());
			}
			ReadOnlyDictionary<string, ResolvedQueryParameterDeclaration> readOnlyDictionary = Util.EmptyReadOnlyDictionary<string, ResolvedQueryParameterDeclaration>();
			ImmutableDictionary<string, ResolvedQueryLetBinding> immutableDictionary = QueryResolutionUtils.CreateEmptyLetMap();
			NoOpQueryDefinitionNameRegistrar instance = NoOpQueryDefinitionNameRegistrar.Instance;
			QueryTransformTableContext queryTransformTableContext = new QueryTransformTableContext();
			QuerySourceContext querySourceContext = null;
			if (flag)
			{
				flag &= QueryResolutionUtils.TryResolveQuerySources(from, federatedSchema, errorContext, usedNames, instance, queryTransformTableContext, immutableDictionary, readOnlyDictionary, out querySourceContext);
			}
			if (flag)
			{
				QueryDefinitionResolver queryDefinitionResolver = new QueryDefinitionResolver(querySourceContext, errorContext, queryTransformTableContext, usedNames, instance, immutableDictionary, readOnlyDictionary);
				flag &= QueryResolutionUtils.TryResolveEach<QueryFilter, ResolvedQueryFilter>(queryFilters, new QueryResolutionUtils.TryResolveItem<QueryFilter, ResolvedQueryFilter>(queryDefinitionResolver.TryResolveQueryFilter), out resolvedQueryFilters);
			}
			QueryResolutionUtils.EnsureQueryDefinitionResolverContract(errorContext, flag);
			return flag;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000206A0 File Offset: 0x0001E8A0
		private bool TryResolveQueryDefinition(QueryDefinition query, IReadOnlyList<ResolvedQueryParameterDeclaration> parameterDeclarations, IReadOnlyList<ResolvedQueryLetBinding> letBindings, out ResolvedQueryDefinition resolvedQueryDefinition)
		{
			bool flag = true;
			List<ResolvedQuerySource> list = this._queryExpressionResolver.SourceContext.SourceMap.Values.ToList<ResolvedQuerySource>();
			List<ResolvedQueryFilter> list2;
			List<ResolvedQueryTransform> list3;
			List<ResolvedQuerySortClause> list4;
			List<ResolvedQuerySelect> list5;
			bool flag2 = flag & QueryResolutionUtils.TryResolveEach<QueryFilter, ResolvedQueryFilter>(query.Where.EmptyIfNull<QueryFilter>(), new QueryResolutionUtils.TryResolveItem<QueryFilter, ResolvedQueryFilter>(this.TryResolveQueryFilter), out list2) & QueryResolutionUtils.TryResolveEach<QueryTransform, ResolvedQueryTransform>(query.Transform.EmptyIfNull<QueryTransform>(), new QueryResolutionUtils.TryResolveItem<QueryTransform, ResolvedQueryTransform>(this.TryResolveQueryTransform), out list3) & QueryResolutionUtils.TryResolveEach<QuerySortClause, ResolvedQuerySortClause>(query.OrderBy.EmptyIfNull<QuerySortClause>(), new QueryResolutionUtils.TryResolveItem<QuerySortClause, ResolvedQuerySortClause>(this.TryResolveQuerySortClause), out list4) & QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQuerySelect>(query.Select, new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQuerySelect>(this.TryResolveSelect), out list5);
			List<ResolvedQueryAxis> list6 = null;
			List<ResolvedQueryExpression> list7;
			IReadOnlyList<ResolvedQuerySelect> readOnlyList;
			if (!(flag2 & QueryResolutionUtils.TryResolveEach<QueryAxis, ResolvedQueryAxis>(query.VisualShape.EmptyIfNull<QueryAxis>(), new QueryResolutionUtils.TryResolveItem<QueryAxis, ResolvedQueryAxis>(this.TryResolveVisualShapeAxis), out list6) & QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryExpression>(query.GroupBy.EmptyIfNull<QueryExpressionContainer>(), new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryExpression>(this.TryResolveQueryExpression), out list7) & QueryResolutionUtils.TryValidateSelectNames(this._errorContext, list5, out readOnlyList)))
			{
				resolvedQueryDefinition = null;
				return false;
			}
			string nextName = this._queryDefinitionNames.GetNextName();
			resolvedQueryDefinition = new ResolvedQueryDefinition(parameterDeclarations, letBindings, list, list2, list3, list4, readOnlyList, list6, list7, query.Top, query.Skip, nextName);
			return true;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000207CC File Offset: 0x0001E9CC
		private bool TryResolverFilterDefinition(FilterDefinition filter, out ResolvedFilterDefinition resolvedFilter)
		{
			List<ResolvedQuerySource> list = this._queryExpressionResolver.SourceContext.SourceMap.Values.ToList<ResolvedQuerySource>();
			List<ResolvedQueryFilter> list2;
			if (!QueryResolutionUtils.TryResolveEach<QueryFilter, ResolvedQueryFilter>(filter.Where.EmptyIfNull<QueryFilter>(), new QueryResolutionUtils.TryResolveItem<QueryFilter, ResolvedQueryFilter>(this.TryResolveQueryFilter), out list2))
			{
				resolvedFilter = null;
				return false;
			}
			resolvedFilter = new ResolvedFilterDefinition(list, list2);
			return true;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00020824 File Offset: 0x0001EA24
		private bool TryResolveQueryTransform(QueryTransform transform, out ResolvedQueryTransform resolvedTransform)
		{
			ResolvedQueryTransformInput resolvedQueryTransformInput;
			ResolvedQueryTransformOutput resolvedQueryTransformOutput;
			if (!(true & this.TryResolveTransformInput(transform.Input, out resolvedQueryTransformInput) & this.TryResolveTransformOutput(transform.Output, out resolvedQueryTransformOutput)))
			{
				resolvedTransform = null;
				return false;
			}
			resolvedTransform = new ResolvedQueryTransform(transform.Name, transform.Algorithm, resolvedQueryTransformInput, resolvedQueryTransformOutput);
			return true;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00020870 File Offset: 0x0001EA70
		private bool TryResolveTransformInput(QueryTransformInput input, out ResolvedQueryTransformInput resolvedInput)
		{
			bool flag = true;
			List<ResolvedQueryTransformParameter> list = null;
			if (input.Parameters != null)
			{
				flag &= QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryTransformParameter>(input.Parameters, new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryTransformParameter>(this.TryResolveTransformParameter), out list);
			}
			ResolvedQueryTransformTable resolvedQueryTransformTable;
			if (!(flag & this.TryResolveTransformTable(input.Table, out resolvedQueryTransformTable)))
			{
				resolvedInput = null;
				return false;
			}
			resolvedInput = new ResolvedQueryTransformInput(list, resolvedQueryTransformTable);
			return true;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000208C8 File Offset: 0x0001EAC8
		private bool TryResolveTransformParameter(QueryExpressionContainer param, out ResolvedQueryTransformParameter resolvedParam)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!QueryResolutionUtils.TryResolveQueryExpression(this._queryExpressionResolver, this._errorContext, param, out resolvedQueryExpression))
			{
				resolvedParam = null;
				return false;
			}
			resolvedParam = new ResolvedQueryTransformParameter(param.Name, resolvedQueryExpression);
			return true;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00020900 File Offset: 0x0001EB00
		private bool TryResolveTransformOutput(QueryTransformOutput output, out ResolvedQueryTransformOutput resolvedOutput)
		{
			ResolvedQueryTransformTable resolvedQueryTransformTable;
			if (!this.TryResolveTransformTable(output.Table, out resolvedQueryTransformTable))
			{
				resolvedOutput = null;
				return false;
			}
			resolvedOutput = new ResolvedQueryTransformOutput(resolvedQueryTransformTable);
			return true;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0002092C File Offset: 0x0001EB2C
		private bool TryResolveTransformTable(QueryTransformTable table, out ResolvedQueryTransformTable resolvedTable)
		{
			List<ResolvedQueryTransformTableColumn> list;
			if (!QueryResolutionUtils.TryResolveEach<QueryTransformTableColumn, ResolvedQueryTransformTableColumn>(table.Columns, new QueryResolutionUtils.TryResolveItem<QueryTransformTableColumn, ResolvedQueryTransformTableColumn>(this.TryResolveTransformTableColumn), out list))
			{
				resolvedTable = null;
				return false;
			}
			resolvedTable = new ResolvedQueryTransformTable(table.Name, list);
			if (!this._transformTableContext.TryAddTable(resolvedTable))
			{
				this._errorContext.RegisterError(QueryResolutionMessages.DuplicateTransformName(resolvedTable.Name));
				return false;
			}
			return true;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00020990 File Offset: 0x0001EB90
		private bool TryResolveTransformTableColumn(QueryTransformTableColumn column, out ResolvedQueryTransformTableColumn resolvedColumn)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(column.Expression, out resolvedQueryExpression))
			{
				resolvedColumn = null;
				return false;
			}
			resolvedColumn = new ResolvedQueryTransformTableColumn(column.Expression.Name, column.Role, resolvedQueryExpression);
			return true;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000209CC File Offset: 0x0001EBCC
		private bool TryResolveQueryFilter(QueryFilter queryFilter, out ResolvedQueryFilter resolvedQueryFilter)
		{
			List<ResolvedQueryExpression> list;
			if (!QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryExpression>(queryFilter.Target.EmptyIfNull<QueryExpressionContainer>(), new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryExpression>(this.TryResolveQueryExpression), out list))
			{
				resolvedQueryFilter = null;
				return false;
			}
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(queryFilter.Condition.Expression, out resolvedQueryExpression))
			{
				resolvedQueryFilter = null;
				return false;
			}
			resolvedQueryFilter = resolvedQueryExpression.Filter(list, queryFilter.Annotations);
			return true;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00020A30 File Offset: 0x0001EC30
		private bool TryResolveQuerySortClause(QuerySortClause sortClause, out ResolvedQuerySortClause resolvedQuerySortClause)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(sortClause.Expression, out resolvedQueryExpression))
			{
				resolvedQuerySortClause = null;
				return false;
			}
			resolvedQuerySortClause = resolvedQueryExpression.Sort(sortClause.Direction);
			return true;
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00020A64 File Offset: 0x0001EC64
		private bool TryResolveSelect(QueryExpressionContainer selectClause, out ResolvedQuerySelect resolvedQuerySelect)
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (!this.TryResolveQueryExpression(selectClause, out resolvedQueryExpression))
			{
				resolvedQuerySelect = null;
				return false;
			}
			resolvedQuerySelect = new ResolvedQuerySelect(resolvedQueryExpression, selectClause.Name, selectClause.NativeReferenceName);
			return true;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00020A98 File Offset: 0x0001EC98
		private bool TryResolveVisualShapeAxis(QueryAxis queryAxis, out ResolvedQueryAxis resolvedQueryAxis)
		{
			List<ResolvedQueryAxisGroup> list;
			bool flag = QueryResolutionUtils.TryResolveEach<QueryAxisGroup, ResolvedQueryAxisGroup>(queryAxis.Groups, new QueryResolutionUtils.TryResolveItem<QueryAxisGroup, ResolvedQueryAxisGroup>(this.TryResolveVisualShapeAxisGroup), out list);
			resolvedQueryAxis = (flag ? new ResolvedQueryAxis(queryAxis.Name, list) : null);
			return flag;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		private bool TryResolveVisualShapeAxisGroup(QueryAxisGroup queryAxisGroup, out ResolvedQueryAxisGroup resolvedQueryAxisGroup)
		{
			List<ResolvedQueryExpression> list;
			bool flag = QueryResolutionUtils.TryResolveEach<QueryExpressionContainer, ResolvedQueryExpression>(queryAxisGroup.Keys, new QueryResolutionUtils.TryResolveItem<QueryExpressionContainer, ResolvedQueryExpression>(this.TryResolveQueryExpression), out list);
			resolvedQueryAxisGroup = (flag ? new ResolvedQueryAxisGroup(list, queryAxisGroup.Subtotal) : null);
			return flag;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00020B10 File Offset: 0x0001ED10
		private bool TryResolveQueryExpression(QueryExpressionContainer expressionContainer, out ResolvedQueryExpression resolvedExpression)
		{
			return QueryResolutionUtils.TryResolveQueryExpression(this._queryExpressionResolver, this._errorContext, expressionContainer, out resolvedExpression);
		}

		// Token: 0x040007C6 RID: 1990
		private readonly QueryExpressionResolver _queryExpressionResolver;

		// Token: 0x040007C7 RID: 1991
		private readonly QueryResolutionErrorContext _errorContext;

		// Token: 0x040007C8 RID: 1992
		private readonly QueryTransformTableContext _transformTableContext;

		// Token: 0x040007C9 RID: 1993
		private readonly IQueryDefinitionNameRegistrar _queryDefinitionNames;

		// Token: 0x0200033C RID: 828
		[ImmutableObject(true)]
		private sealed class ValidationErrorContext : IErrorContext
		{
			// Token: 0x06001A17 RID: 6679 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
			internal ValidationErrorContext(QueryResolutionErrorContext errorContext)
			{
				this._errorContext = errorContext;
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0002F0C3 File Offset: 0x0002D2C3
			public bool HasError
			{
				get
				{
					return this._errorContext.HasError;
				}
			}

			// Token: 0x06001A19 RID: 6681 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
			public void RegisterError(string messageTemplate, params object[] args)
			{
				string text = StringUtil.FormatInvariant(messageTemplate, args);
				this._errorContext.RegisterError(QueryResolutionMessages.InvalidQueryDefinition(text));
			}

			// Token: 0x06001A1A RID: 6682 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
			public void RegisterWarning(string messageTemplate, params object[] args)
			{
				string text = StringUtil.FormatInvariant(messageTemplate, args);
				this._errorContext.RegisterWarning(QueryResolutionMessages.InvalidQueryDefinition(text));
			}

			// Token: 0x040009C5 RID: 2501
			private readonly QueryResolutionErrorContext _errorContext;
		}
	}
}
