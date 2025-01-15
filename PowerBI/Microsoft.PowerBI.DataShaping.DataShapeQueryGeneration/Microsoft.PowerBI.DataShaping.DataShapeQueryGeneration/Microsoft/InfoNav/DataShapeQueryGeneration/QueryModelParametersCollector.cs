using System;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000093 RID: 147
	internal sealed class QueryModelParametersCollector
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x00014D89 File Offset: 0x00012F89
		private QueryModelParametersCollector(IErrorContext errorContext)
		{
			this._parameterMappings = null;
			this._errorContext = errorContext;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00014D9F File Offset: 0x00012F9F
		public static ParameterMappings Collect(ResolvedQueryDefinition query, IErrorContext errorContext)
		{
			QueryModelParametersCollector queryModelParametersCollector = new QueryModelParametersCollector(errorContext);
			queryModelParametersCollector.Visit(query);
			return queryModelParametersCollector._parameterMappings;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public void Visit(ResolvedQueryDefinition query)
		{
			query.Let.ExecuteOnCollection(new Action<ResolvedQueryLetBinding>(this.Visit));
			bool flag = false;
			foreach (ResolvedQuerySource resolvedQuerySource in query.From)
			{
				flag |= this.Visit(resolvedQuerySource);
			}
			if (flag)
			{
				ParameterMappings parameterMappings = ModelParametersExtractor.ExtractParameterMappings(query.Where, this._errorContext);
				if (this._parameterMappings == null)
				{
					this._parameterMappings = parameterMappings;
					return;
				}
				if (this._parameterMappings != parameterMappings)
				{
					this._errorContext.RegisterError(QueryValidationMessages.DifferentMappingsWithinSubqueries, Array.Empty<object>());
					return;
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00014E6C File Offset: 0x0001306C
		private void Visit(ResolvedQueryLetBinding letBinding)
		{
			this.Visit(letBinding.Expression);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00014E7C File Offset: 0x0001307C
		private bool Visit(ResolvedQuerySource source)
		{
			ResolvedExpressionSource resolvedExpressionSource = source as ResolvedExpressionSource;
			if (resolvedExpressionSource != null)
			{
				this.Visit(resolvedExpressionSource);
			}
			return source is ResolvedEntitySource;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014EA5 File Offset: 0x000130A5
		private void Visit(ResolvedExpressionSource source)
		{
			this.Visit(source.Expression);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00014EB4 File Offset: 0x000130B4
		private void Visit(ResolvedQueryExpression expression)
		{
			ResolvedQuerySubqueryExpression resolvedQuerySubqueryExpression = expression as ResolvedQuerySubqueryExpression;
			if (resolvedQuerySubqueryExpression != null)
			{
				this.Visit(resolvedQuerySubqueryExpression);
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00014ED4 File Offset: 0x000130D4
		private void Visit(ResolvedQuerySubqueryExpression subqueryExpr)
		{
			ResolvedQueryDefinition subquery = subqueryExpr.Subquery;
			this.Visit(subquery);
		}

		// Token: 0x04000324 RID: 804
		private IErrorContext _errorContext;

		// Token: 0x04000325 RID: 805
		private ParameterMappings _parameterMappings;
	}
}
