using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000088 RID: 136
	internal class ResolvedQueryDefinitionRewriter
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00013763 File Offset: 0x00011963
		internal ResolvedQueryDefinitionRewriter(IEnumerable<ResolvedQueryExpressionRewriter> expressionRewriters)
		{
			this._crossReferenceFixup = new ResolvedQueryExpressionReferenceRewriter();
			expressionRewriters = expressionRewriters.Concat(this._crossReferenceFixup);
			this._exprRewriters = expressionRewriters.AsReadOnlyCollection<ResolvedQueryExpressionRewriter>();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013790 File Offset: 0x00011990
		internal virtual ResolvedQueryDefinition Rewrite(ResolvedQueryDefinition query)
		{
			IReadOnlyList<ResolvedQueryParameterDeclaration> readOnlyList = this.RewriteParameters(query.Parameters);
			IReadOnlyList<ResolvedQueryLetBinding> readOnlyList2 = this.RewriteLetBindings(query.Let);
			IReadOnlyList<ResolvedQuerySource> readOnlyList3 = query.From.Rewrite(new Func<ResolvedQuerySource, ResolvedQuerySource>(this.RewriteSource));
			IReadOnlyList<ResolvedQueryFilter> readOnlyList4 = query.Where.Rewrite(new Func<ResolvedQueryFilter, ResolvedQueryFilter>(this.RewriteFilter));
			IReadOnlyList<ResolvedQueryTransform> readOnlyList5 = query.Transform.Rewrite(new Func<ResolvedQueryTransform, ResolvedQueryTransform>(this.RewriteTransform));
			IReadOnlyList<ResolvedQuerySortClause> readOnlyList6 = query.OrderBy.Rewrite(new Func<ResolvedQuerySortClause, ResolvedQuerySortClause>(this.RewriteSortClause));
			IReadOnlyList<ResolvedQuerySelect> readOnlyList7 = query.Select.Rewrite(new Func<ResolvedQuerySelect, ResolvedQuerySelect>(this.RewriteSelect));
			IReadOnlyList<ResolvedQueryAxis> readOnlyList8 = query.VisualShape.Rewrite(new Func<ResolvedQueryAxis, ResolvedQueryAxis>(this.RewriteVisualShape));
			IReadOnlyList<ResolvedQueryExpression> readOnlyList9 = query.GroupBy.Rewrite(new Func<ResolvedQueryExpression, ResolvedQueryExpression>(this.RewriteExpression));
			readOnlyList3 = this.AppendNewSources(readOnlyList3);
			if (readOnlyList == query.Parameters && readOnlyList2 == query.Let && readOnlyList3 == query.From && readOnlyList4 == query.Where && readOnlyList5 == query.Transform && readOnlyList6 == query.OrderBy && readOnlyList7 == query.Select && readOnlyList9 == query.GroupBy)
			{
				return query;
			}
			this._crossReferenceFixup.RemoveLetBindings(readOnlyList2);
			this._crossReferenceFixup.RemoveTransforms();
			return new ResolvedQueryDefinition(readOnlyList, readOnlyList2, readOnlyList3, readOnlyList4, readOnlyList5, readOnlyList6, readOnlyList7, readOnlyList8, readOnlyList9, query.Top, query.Skip, query.Name);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000138F6 File Offset: 0x00011AF6
		protected virtual IReadOnlyList<ResolvedQuerySource> GetNewSources()
		{
			return Util.EmptyReadOnlyCollection<ResolvedQuerySource>();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000138FD File Offset: 0x00011AFD
		protected virtual IReadOnlyList<ResolvedQueryParameterDeclaration> RewriteParameters(IReadOnlyList<ResolvedQueryParameterDeclaration> parameters)
		{
			return parameters.Rewrite(new Func<ResolvedQueryParameterDeclaration, ResolvedQueryParameterDeclaration>(this.RewriteParameter));
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00013914 File Offset: 0x00011B14
		private ResolvedQueryParameterDeclaration RewriteParameter(ResolvedQueryParameterDeclaration parameter)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(parameter.TypeExpression);
			if (resolvedQueryExpression == parameter.TypeExpression)
			{
				return parameter;
			}
			ResolvedQueryParameterDeclaration resolvedQueryParameterDeclaration = new ResolvedQueryParameterDeclaration(parameter.Name, resolvedQueryExpression);
			this._crossReferenceFixup.AddUpdatedParameterDeclaration(resolvedQueryParameterDeclaration);
			return resolvedQueryParameterDeclaration;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00013953 File Offset: 0x00011B53
		protected virtual IReadOnlyList<ResolvedQueryLetBinding> RewriteLetBindings(IReadOnlyList<ResolvedQueryLetBinding> letBindings)
		{
			return letBindings.Rewrite(new Func<ResolvedQueryLetBinding, ResolvedQueryLetBinding>(this.RewriteLetBinding));
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00013968 File Offset: 0x00011B68
		private IReadOnlyList<ResolvedQuerySource> AppendNewSources(IReadOnlyList<ResolvedQuerySource> from)
		{
			IReadOnlyList<ResolvedQuerySource> newSources = this.GetNewSources();
			if (newSources.IsNullOrEmpty<ResolvedQuerySource>())
			{
				return from;
			}
			List<ResolvedQuerySource> list = new List<ResolvedQuerySource>(from);
			list.AddRange(newSources);
			return list;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00013994 File Offset: 0x00011B94
		private ResolvedQueryLetBinding RewriteLetBinding(ResolvedQueryLetBinding letBinding)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(letBinding.Expression);
			if (resolvedQueryExpression == letBinding.Expression)
			{
				return letBinding;
			}
			ResolvedQueryLetBinding resolvedQueryLetBinding = new ResolvedQueryLetBinding(letBinding.Name, resolvedQueryExpression);
			this._crossReferenceFixup.AddUpdatedLetBinding(resolvedQueryLetBinding);
			return resolvedQueryLetBinding;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000139D4 File Offset: 0x00011BD4
		private ResolvedQuerySource RewriteSource(ResolvedQuerySource source)
		{
			ResolvedExpressionSource resolvedExpressionSource = source as ResolvedExpressionSource;
			if (resolvedExpressionSource == null)
			{
				return source;
			}
			ResolvedQueryExpression expression = resolvedExpressionSource.Expression;
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(expression);
			if (expression == resolvedQueryExpression)
			{
				return source;
			}
			ResolvedExpressionSource resolvedExpressionSource2 = new ResolvedExpressionSource(source.Name, resolvedQueryExpression);
			this._crossReferenceFixup.AddUpdatedExpressionSource(resolvedExpressionSource2);
			return resolvedExpressionSource2;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00013A1C File Offset: 0x00011C1C
		private ResolvedQueryFilter RewriteFilter(ResolvedQueryFilter filter)
		{
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = filter.Target.Rewrite(new Func<ResolvedQueryExpression, ResolvedQueryExpression>(this.RewriteExpression));
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(filter.Condition);
			if (readOnlyList == filter.Target && resolvedQueryExpression == filter.Condition)
			{
				return filter;
			}
			return resolvedQueryExpression.Filter(readOnlyList, null);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00013A6C File Offset: 0x00011C6C
		private ResolvedQuerySortClause RewriteSortClause(ResolvedQuerySortClause sortClause)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(sortClause.Expression);
			if (resolvedQueryExpression == sortClause.Expression)
			{
				return sortClause;
			}
			return resolvedQueryExpression.Sort(sortClause.Direction);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013AA0 File Offset: 0x00011CA0
		protected ResolvedQuerySelect RewriteSelect(ResolvedQuerySelect select)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteSelectExpression(select.Expression);
			if (resolvedQueryExpression == select.Expression)
			{
				return select;
			}
			return new ResolvedQuerySelect(resolvedQueryExpression, select.Name, select.NativeReferenceName);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00013AD7 File Offset: 0x00011CD7
		protected virtual ResolvedQueryExpression RewriteSelectExpression(ResolvedQueryExpression expression)
		{
			return this.RewriteExpression(expression);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00013AE0 File Offset: 0x00011CE0
		protected ResolvedQueryAxis RewriteVisualShape(ResolvedQueryAxis axis)
		{
			IReadOnlyList<ResolvedQueryAxisGroup> readOnlyList = axis.Groups.Rewrite(new Func<ResolvedQueryAxisGroup, ResolvedQueryAxisGroup>(this.RewriteAxisGroup));
			if (readOnlyList != axis.Groups)
			{
				return new ResolvedQueryAxis(axis.Name, readOnlyList);
			}
			return axis;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00013B1C File Offset: 0x00011D1C
		protected ResolvedQueryAxisGroup RewriteAxisGroup(ResolvedQueryAxisGroup axisGroup)
		{
			IReadOnlyList<ResolvedQueryExpression> readOnlyList = axisGroup.Keys.Rewrite(new Func<ResolvedQueryExpression, ResolvedQueryExpression>(this.RewriteExpression));
			if (readOnlyList != axisGroup.Keys)
			{
				return new ResolvedQueryAxisGroup(readOnlyList, axisGroup.Subtotal);
			}
			return axisGroup;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00013B58 File Offset: 0x00011D58
		private ResolvedQueryExpression RewriteExpression(ResolvedQueryExpression expression)
		{
			for (int i = 0; i < this._exprRewriters.Count; i++)
			{
				expression = expression.Accept<ResolvedQueryExpression>(this._exprRewriters[i]);
			}
			return expression;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00013B90 File Offset: 0x00011D90
		private ResolvedQueryTransform RewriteTransform(ResolvedQueryTransform transform)
		{
			ResolvedQueryTransformInput resolvedQueryTransformInput = this.RewriteTransformInput(transform.Input);
			ResolvedQueryTransformOutput resolvedQueryTransformOutput = this.RewriteTransformOutput(transform.Output);
			if (transform.Input == resolvedQueryTransformInput && transform.Output == resolvedQueryTransformOutput)
			{
				return transform;
			}
			return new ResolvedQueryTransform(transform.Name, transform.Algorithm, resolvedQueryTransformInput, resolvedQueryTransformOutput);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013BE0 File Offset: 0x00011DE0
		private ResolvedQueryTransformInput RewriteTransformInput(ResolvedQueryTransformInput input)
		{
			IReadOnlyList<ResolvedQueryTransformParameter> readOnlyList = null;
			if (input.Parameters != null)
			{
				readOnlyList = input.Parameters.Rewrite(new Func<ResolvedQueryTransformParameter, ResolvedQueryTransformParameter>(this.RewriteTransformParameter));
			}
			ResolvedQueryTransformTable resolvedQueryTransformTable = this.RewriteTransformTable(input.Table);
			if (input.Parameters == readOnlyList && input.Table == resolvedQueryTransformTable)
			{
				return input;
			}
			return new ResolvedQueryTransformInput(readOnlyList, resolvedQueryTransformTable);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00013C38 File Offset: 0x00011E38
		private ResolvedQueryTransformOutput RewriteTransformOutput(ResolvedQueryTransformOutput output)
		{
			ResolvedQueryTransformTable resolvedQueryTransformTable = this.RewriteTransformTable(output.Table);
			if (output.Table == resolvedQueryTransformTable)
			{
				return output;
			}
			return new ResolvedQueryTransformOutput(resolvedQueryTransformTable);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00013C64 File Offset: 0x00011E64
		private ResolvedQueryTransformParameter RewriteTransformParameter(ResolvedQueryTransformParameter param)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(param.Expression);
			if (param.Expression == resolvedQueryExpression)
			{
				return param;
			}
			return new ResolvedQueryTransformParameter(param.Name, resolvedQueryExpression);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00013C98 File Offset: 0x00011E98
		private ResolvedQueryTransformTable RewriteTransformTable(ResolvedQueryTransformTable table)
		{
			IReadOnlyList<ResolvedQueryTransformTableColumn> readOnlyList = table.Columns.Rewrite(new Func<ResolvedQueryTransformTableColumn, ResolvedQueryTransformTableColumn>(this.RewriteTransformTableColumn));
			if (table.Columns == readOnlyList)
			{
				return table;
			}
			ResolvedQueryTransformTable resolvedQueryTransformTable = new ResolvedQueryTransformTable(table.Name, readOnlyList);
			this._crossReferenceFixup.AddUpdatedTable(resolvedQueryTransformTable);
			return resolvedQueryTransformTable;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00013CE4 File Offset: 0x00011EE4
		private ResolvedQueryTransformTableColumn RewriteTransformTableColumn(ResolvedQueryTransformTableColumn column)
		{
			ResolvedQueryExpression resolvedQueryExpression = this.RewriteExpression(column.Expression);
			if (column.Expression == resolvedQueryExpression)
			{
				return column;
			}
			return new ResolvedQueryTransformTableColumn(column.Name, column.Role, resolvedQueryExpression);
		}

		// Token: 0x040002F2 RID: 754
		private readonly ResolvedQueryExpressionReferenceRewriter _crossReferenceFixup;

		// Token: 0x040002F3 RID: 755
		private readonly ReadOnlyCollection<ResolvedQueryExpressionRewriter> _exprRewriters;
	}
}
