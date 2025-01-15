using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000E8 RID: 232
	internal sealed class MappedColumnExtractor : DefaultResolvedQueryExpressionVisitor
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0000C82F File Offset: 0x0000AA2F
		private MappedColumnExtractor(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000C83E File Offset: 0x0000AA3E
		public static bool HasMappedParameter(ResolvedQueryExpression expression, IErrorContext errorContext)
		{
			return !MappedColumnExtractor.ExtractMappedColumns(expression, errorContext).IsNullOrEmpty<IConceptualColumn>();
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000C850 File Offset: 0x0000AA50
		public static IReadOnlyList<IConceptualColumn> ExtractMappedColumns(ResolvedQueryExpression expression, IErrorContext errorContext)
		{
			MappedColumnExtractor mappedColumnExtractor = new MappedColumnExtractor(errorContext);
			expression.Accept(mappedColumnExtractor);
			return mappedColumnExtractor._mappedColumns;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000C874 File Offset: 0x0000AA74
		public override void Visit(ResolvedQueryColumnExpression expression)
		{
			IReadOnlyList<ConceptualMParameter> mappedParameter = expression.Column.GetMappedParameter();
			if (mappedParameter != null && mappedParameter.Count > 0)
			{
				if (this._mappedColumns == null)
				{
					this._mappedColumns = new List<IConceptualColumn>();
				}
				this._mappedColumns.Add(expression.Column);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		public override void Visit(ResolvedQueryAggregationExpression expression)
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000C8C3 File Offset: 0x0000AAC3
		public override void Visit(ResolvedQueryExistsExpression expression)
		{
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000C8C5 File Offset: 0x0000AAC5
		public override void Visit(ResolvedQueryScopedEvalExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public override void Visit(ResolvedQueryFilteredEvalExpression expression)
		{
			if (ModelParametersExtractor.AnyFilterHasParameterMapping(expression.Filters, this._errorContext))
			{
				this._errorContext.RegisterError(QueryValidationMessages.ParameterMappingsFoundWithinFilteredEvalExpressionFilter, new object[0]);
			}
		}

		// Token: 0x0400029F RID: 671
		private readonly IErrorContext _errorContext;

		// Token: 0x040002A0 RID: 672
		private List<IConceptualColumn> _mappedColumns;
	}
}
