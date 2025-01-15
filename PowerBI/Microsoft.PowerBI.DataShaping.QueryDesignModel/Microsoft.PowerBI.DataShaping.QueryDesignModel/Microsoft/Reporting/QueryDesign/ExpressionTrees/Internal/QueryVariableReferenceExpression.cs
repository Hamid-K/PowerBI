using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001CC RID: 460
	internal sealed class QueryVariableReferenceExpression : QueryExpression
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x0003E190 File Offset: 0x0003C390
		internal QueryVariableReferenceExpression(string variableName, ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
			this.VariableName = variableName;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0003E1A0 File Offset: 0x0003C3A0
		public string VariableName { get; }

		// Token: 0x06001680 RID: 5760 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryVariableReferenceExpression queryVariableReferenceExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryVariableReferenceExpression>(this, other, out flag, out queryVariableReferenceExpression))
			{
				return flag;
			}
			return this.VariableName.Equals(queryVariableReferenceExpression.VariableName, EdmItem.IdentityComparison);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0003E1E6 File Offset: 0x0003C3E6
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.ConceptualResultType.GetHashCode(), EdmItem.IdentityComparer.GetHashCode(this.VariableName));
		}
	}
}
