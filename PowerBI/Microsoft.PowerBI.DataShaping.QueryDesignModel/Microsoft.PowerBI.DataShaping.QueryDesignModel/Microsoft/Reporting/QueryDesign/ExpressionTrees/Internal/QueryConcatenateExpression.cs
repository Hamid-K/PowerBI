using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200016B RID: 363
	internal sealed class QueryConcatenateExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001455 RID: 5205 RVA: 0x0003AC14 File Offset: 0x00038E14
		internal QueryConcatenateExpression(IReadOnlyList<QueryExpression> inputs)
			: base(ConceptualPrimitiveResultType.Text)
		{
			this._inputs = inputs;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0003AC28 File Offset: 0x00038E28
		public IReadOnlyList<QueryExpression> Inputs
		{
			get
			{
				return this._inputs;
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0003AC30 File Offset: 0x00038E30
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0003AC44 File Offset: 0x00038E44
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryConcatenateExpression queryConcatenateExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryConcatenateExpression>(this, other, out flag, out queryConcatenateExpression))
			{
				return flag;
			}
			return this.Inputs.SequenceEqual(queryConcatenateExpression.Inputs);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0003AC71 File Offset: 0x00038E71
		public override int GetHashCode()
		{
			return Hashing.CombineHashReadonly<QueryExpression>(this.Inputs, QueryExpression.Comparer);
		}

		// Token: 0x04000B25 RID: 2853
		private readonly IReadOnlyList<QueryExpression> _inputs;
	}
}
