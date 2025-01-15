using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002A0 RID: 672
	public class QueryDefinitionRewriter
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x000246BC File Offset: 0x000228BC
		public QueryDefinitionRewriter(QueryExpressionRewriter expressionRewriter = null, Func<EntitySource, EntitySource> sourceRewriter = null)
		{
			this._expressionRewriter = expressionRewriter;
			this._sourceRewriter = sourceRewriter;
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x000246D2 File Offset: 0x000228D2
		public static QueryDefinition Rewrite(QueryDefinition queryDefinition, QueryExpressionRewriter expressionRewriter, Func<EntitySource, EntitySource> sourceRewriter = null)
		{
			return new QueryDefinitionRewriter(expressionRewriter, sourceRewriter).Rewrite(queryDefinition);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000246E4 File Offset: 0x000228E4
		public virtual QueryDefinition Rewrite(QueryDefinition queryDefinition)
		{
			if (queryDefinition == null)
			{
				return null;
			}
			List<QueryExpressionContainer> list = this.RewriteLetClause(queryDefinition.Let);
			List<EntitySource> list2 = this.RewriteFromClause(queryDefinition.From);
			List<QueryFilter> list3 = this.RewriteWhereClause(queryDefinition.Where);
			List<QuerySortClause> list4 = this.RewriteOrderByClause(queryDefinition.OrderBy);
			List<QueryExpressionContainer> list5 = this.RewriteSelectClause(queryDefinition.Select);
			List<QueryExpressionContainer> list6 = this.RewriteGroupByClause(queryDefinition.GroupBy);
			List<QueryTransform> list7 = this.RewriteQueryTransformClause(queryDefinition.Transform);
			return QueryDefinitionRewriter.CreateQueryDefinitionIfNeeded(queryDefinition, list, list2, list3, list4, list5, list6, list7);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0002476C File Offset: 0x0002296C
		protected static QueryDefinition CreateQueryDefinitionIfNeeded(QueryDefinition queryDefinition, List<QueryExpressionContainer> let, List<EntitySource> from, List<QueryFilter> where, List<QuerySortClause> orderBy, List<QueryExpressionContainer> select, List<QueryExpressionContainer> groupBy, List<QueryTransform> transform)
		{
			if (queryDefinition == null)
			{
				return null;
			}
			if (let == queryDefinition.Let && from == queryDefinition.From && where == queryDefinition.Where && orderBy == queryDefinition.OrderBy && select == queryDefinition.Select && groupBy == queryDefinition.GroupBy && transform == queryDefinition.Transform)
			{
				return queryDefinition;
			}
			return new QueryDefinition
			{
				Version = queryDefinition.Version,
				DatabaseName = queryDefinition.DatabaseName,
				Let = let,
				From = from,
				Where = where,
				OrderBy = orderBy,
				Select = select,
				GroupBy = groupBy,
				Top = queryDefinition.Top,
				Transform = transform
			};
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00024827 File Offset: 0x00022A27
		internal static QueryFilter RewriteFilter(QueryFilter filter, QueryExpressionRewriter rewriter)
		{
			return QueryDefinitionRewriter.RewriteFilter(filter, new Func<QueryExpressionContainer, QueryExpressionContainer>(rewriter.RewriteContainer));
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0002483B File Offset: 0x00022A3B
		protected virtual List<QueryExpressionContainer> RewriteLetClause(List<QueryExpressionContainer> let)
		{
			return let.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteLetBinding));
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00024850 File Offset: 0x00022A50
		protected virtual QueryExpressionContainer RewriteLetBinding(QueryExpressionContainer letBinding)
		{
			return this.RewriteExpression(letBinding);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00024859 File Offset: 0x00022A59
		protected virtual List<EntitySource> RewriteFromClause(List<EntitySource> from)
		{
			if (this._sourceRewriter == null)
			{
				return from.Rewrite(new Func<EntitySource, EntitySource>(this.RewriteEntitySource));
			}
			return from.Rewrite(this._sourceRewriter);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00024884 File Offset: 0x00022A84
		protected virtual EntitySource RewriteEntitySource(EntitySource entitySource)
		{
			if (entitySource.Expression == null)
			{
				return entitySource;
			}
			QueryExpressionContainer queryExpressionContainer = this.RewriteExpression(entitySource.Expression);
			if (queryExpressionContainer == entitySource.Expression)
			{
				return entitySource;
			}
			return new EntitySource
			{
				Name = entitySource.Name,
				EntitySet = entitySource.EntitySet,
				Entity = entitySource.Entity,
				Schema = entitySource.Schema,
				Type = entitySource.Type,
				Expression = queryExpressionContainer
			};
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00024901 File Offset: 0x00022B01
		protected virtual List<QueryFilter> RewriteWhereClause(List<QueryFilter> where)
		{
			return where.Rewrite(new Func<QueryFilter, QueryFilter>(this.RewriteWhereFilter));
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00024916 File Offset: 0x00022B16
		protected virtual QueryFilter RewriteWhereFilter(QueryFilter filter)
		{
			return QueryDefinitionRewriter.RewriteFilter(filter, new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteExpression));
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0002492A File Offset: 0x00022B2A
		protected virtual List<QuerySortClause> RewriteOrderByClause(List<QuerySortClause> orderBy)
		{
			return orderBy.Rewrite(new Func<QuerySortClause, QuerySortClause>(this.RewriteOrderBy));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00024940 File Offset: 0x00022B40
		protected virtual QuerySortClause RewriteOrderBy(QuerySortClause sortClause)
		{
			if (sortClause == null)
			{
				return null;
			}
			QueryExpressionContainer queryExpressionContainer = this.RewriteExpression(sortClause.Expression);
			if (queryExpressionContainer == sortClause.Expression)
			{
				return sortClause;
			}
			return new QuerySortClause
			{
				Expression = queryExpressionContainer,
				Direction = sortClause.Direction
			};
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00024982 File Offset: 0x00022B82
		protected virtual List<QueryExpressionContainer> RewriteSelectClause(List<QueryExpressionContainer> select)
		{
			return select.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteSelect));
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00024997 File Offset: 0x00022B97
		protected virtual QueryExpressionContainer RewriteSelect(QueryExpressionContainer select)
		{
			return this.RewriteExpression(select);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x000249A0 File Offset: 0x00022BA0
		protected virtual List<QueryExpressionContainer> RewriteGroupByClause(List<QueryExpressionContainer> groupBy)
		{
			return groupBy.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteGroupBy));
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000249B5 File Offset: 0x00022BB5
		protected virtual QueryExpressionContainer RewriteGroupBy(QueryExpressionContainer groupBy)
		{
			return this.RewriteExpression(groupBy);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000249BE File Offset: 0x00022BBE
		protected virtual List<QueryTransform> RewriteQueryTransformClause(List<QueryTransform> transform)
		{
			return transform.Rewrite(new Func<QueryTransform, QueryTransform>(this.RewriteQueryTransform));
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000249D4 File Offset: 0x00022BD4
		protected virtual QueryTransform RewriteQueryTransform(QueryTransform queryTransform)
		{
			if (queryTransform == null)
			{
				return null;
			}
			QueryTransformInput queryTransformInput = this.RewriteQueryTransformInput(queryTransform.Input);
			QueryTransformOutput queryTransformOutput = this.RewriteQueryTransformOutput(queryTransform.Output);
			if (queryTransformInput == queryTransform.Input && queryTransformOutput == queryTransform.Output)
			{
				return queryTransform;
			}
			return new QueryTransform
			{
				Input = queryTransformInput,
				Output = queryTransformOutput,
				Algorithm = queryTransform.Algorithm,
				Name = queryTransform.Name
			};
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00024A40 File Offset: 0x00022C40
		private QueryTransformInput RewriteQueryTransformInput(QueryTransformInput queryTransformInput)
		{
			if (queryTransformInput == null)
			{
				return null;
			}
			List<QueryExpressionContainer> list = queryTransformInput.Parameters.Rewrite(new Func<QueryExpressionContainer, QueryExpressionContainer>(this.RewriteExpression));
			QueryTransformTable queryTransformTable = this.RewriteQueryTransformTable(queryTransformInput.Table);
			if (list == queryTransformInput.Parameters && queryTransformTable == queryTransformInput.Table)
			{
				return queryTransformInput;
			}
			return new QueryTransformInput
			{
				Parameters = list,
				Table = queryTransformTable
			};
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00024AA0 File Offset: 0x00022CA0
		private QueryTransformOutput RewriteQueryTransformOutput(QueryTransformOutput queryTransformOutput)
		{
			if (queryTransformOutput == null)
			{
				return null;
			}
			QueryTransformTable queryTransformTable = this.RewriteQueryTransformTable(queryTransformOutput.Table);
			if (queryTransformTable == queryTransformOutput.Table)
			{
				return queryTransformOutput;
			}
			return new QueryTransformOutput
			{
				Table = queryTransformTable
			};
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00024AD8 File Offset: 0x00022CD8
		private QueryTransformTable RewriteQueryTransformTable(QueryTransformTable queryTransformTable)
		{
			if (queryTransformTable == null)
			{
				return null;
			}
			List<QueryTransformTableColumn> list = queryTransformTable.Columns.Rewrite(new Func<QueryTransformTableColumn, QueryTransformTableColumn>(this.RewriteQueryTransformTableColumn));
			if (list == queryTransformTable.Columns)
			{
				return queryTransformTable;
			}
			return new QueryTransformTable
			{
				Columns = list,
				Name = queryTransformTable.Name
			};
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00024B28 File Offset: 0x00022D28
		private QueryTransformTableColumn RewriteQueryTransformTableColumn(QueryTransformTableColumn queryTransformTableColumn)
		{
			if (queryTransformTableColumn == null)
			{
				return null;
			}
			QueryExpressionContainer queryExpressionContainer = this.RewriteExpression(queryTransformTableColumn.Expression);
			if (queryExpressionContainer == queryTransformTableColumn.Expression)
			{
				return queryTransformTableColumn;
			}
			return new QueryTransformTableColumn
			{
				Expression = queryExpressionContainer,
				Role = queryTransformTableColumn.Role
			};
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00024B6A File Offset: 0x00022D6A
		private QueryExpressionContainer RewriteExpression(QueryExpressionContainer expressionContainer)
		{
			if (this._expressionRewriter != null)
			{
				return this._expressionRewriter.RewriteContainer(expressionContainer);
			}
			return expressionContainer;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00024B84 File Offset: 0x00022D84
		private static QueryFilter RewriteFilter(QueryFilter filter, Func<QueryExpressionContainer, QueryExpressionContainer> rewriteExpression)
		{
			if (filter == null)
			{
				return null;
			}
			List<QueryExpressionContainer> list = filter.Target.Rewrite(rewriteExpression);
			QueryExpressionContainer queryExpressionContainer = rewriteExpression(filter.Condition);
			if (list == filter.Target && queryExpressionContainer == filter.Condition)
			{
				return filter;
			}
			return new QueryFilter
			{
				Target = list,
				Condition = queryExpressionContainer
			};
		}

		// Token: 0x0400083F RID: 2111
		private readonly QueryExpressionRewriter _expressionRewriter;

		// Token: 0x04000840 RID: 2112
		private readonly Func<EntitySource, EntitySource> _sourceRewriter;
	}
}
