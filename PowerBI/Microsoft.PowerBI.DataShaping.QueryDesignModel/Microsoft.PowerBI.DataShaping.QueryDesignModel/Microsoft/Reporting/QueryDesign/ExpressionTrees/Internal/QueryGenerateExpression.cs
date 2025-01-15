using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000187 RID: 391
	internal sealed class QueryGenerateExpression : QueryExpression
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0003B951 File Offset: 0x00039B51
		internal QueryGenerateExpression(QueryGenerateKind generateKind, ConceptualResultType conceptualResultType, IReadOnlyList<QueryExpressionBinding> inputs)
			: base(conceptualResultType)
		{
			this.GenerateKind = generateKind;
			this.Inputs = inputs;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0003B968 File Offset: 0x00039B68
		public IReadOnlyList<QueryExpressionBinding> Inputs { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0003B970 File Offset: 0x00039B70
		public QueryGenerateKind GenerateKind { get; }

		// Token: 0x0600152A RID: 5418 RVA: 0x0003B978 File Offset: 0x00039B78
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003B984 File Offset: 0x00039B84
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryGenerateExpression queryGenerateExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryGenerateExpression>(this, other, out flag, out queryGenerateExpression))
			{
				return flag;
			}
			return this.Inputs.SequenceEqual(queryGenerateExpression.Inputs) && this.GenerateKind == queryGenerateExpression.GenerateKind;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Inputs.GetHashCode(), this.GenerateKind.GetHashCode());
		}
	}
}
