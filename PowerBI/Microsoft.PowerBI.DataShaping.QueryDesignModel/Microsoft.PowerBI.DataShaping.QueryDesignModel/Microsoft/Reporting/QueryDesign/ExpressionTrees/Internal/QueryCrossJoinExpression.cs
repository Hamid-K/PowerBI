using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200016F RID: 367
	internal sealed class QueryCrossJoinExpression : QueryExpression
	{
		// Token: 0x06001468 RID: 5224 RVA: 0x0003ADEE File Offset: 0x00038FEE
		internal QueryCrossJoinExpression(ConceptualResultType conceptualResultType, IEnumerable<QueryExpressionBinding> inputs)
			: base(conceptualResultType)
		{
			this._inputs = ArgumentValidation.CheckNotNullOrEmpty<QueryExpressionBinding>(inputs, "inputs").ToReadOnlyCollection<QueryExpressionBinding>();
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003AE0D File Offset: 0x0003900D
		public ReadOnlyCollection<QueryExpressionBinding> Inputs
		{
			get
			{
				return this._inputs;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0003AE15 File Offset: 0x00039015
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0003AE28 File Offset: 0x00039028
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryCrossJoinExpression queryCrossJoinExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryCrossJoinExpression>(this, other, out flag, out queryCrossJoinExpression))
			{
				return flag;
			}
			return this.Inputs.SequenceEqual(queryCrossJoinExpression.Inputs);
		}

		// Token: 0x04000B2B RID: 2859
		private readonly ReadOnlyCollection<QueryExpressionBinding> _inputs;
	}
}
