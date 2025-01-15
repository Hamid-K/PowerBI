using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ModelParameters;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A4 RID: 420
	internal sealed class QueryMParameterDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
		internal QueryMParameterDeclarationExpression(string parameterName, QueryExpression expression)
			: base(expression.ConceptualResultType)
		{
			this.ParameterName = parameterName;
			this.Expression = expression;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
		public QueryExpression Expression { get; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0003C9C8 File Offset: 0x0003ABC8
		public string ParameterName { get; }

		// Token: 0x060015B8 RID: 5560 RVA: 0x0003C9D0 File Offset: 0x0003ABD0
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0003C9DC File Offset: 0x0003ABDC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryMParameterDeclarationExpression queryMParameterDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryMParameterDeclarationExpression>(this, other, out flag, out queryMParameterDeclarationExpression))
			{
				return flag;
			}
			return ParameterMappings.ParameterNameComparer.Equals(this.ParameterName, queryMParameterDeclarationExpression.ParameterName) && this.Expression.Equals(queryMParameterDeclarationExpression.Expression);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0003CA23 File Offset: 0x0003AC23
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Expression.GetHashCode(), this.ParameterName.GetHashCode());
		}
	}
}
