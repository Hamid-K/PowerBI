using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000EB RID: 235
	internal sealed class ExpressionJoinPredicate : IJoinPredicate
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002384D File Offset: 0x00021A4D
		internal ExpressionJoinPredicate(QueryExpression expression, bool isAnchored)
		{
			this.PredicateExpression = ArgumentValidation.CheckNotNull<QueryExpression>(expression, "expression");
			this.IsAnchored = isAnchored;
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002386D File Offset: 0x00021A6D
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00023875 File Offset: 0x00021A75
		internal QueryExpression PredicateExpression { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002387E File Offset: 0x00021A7E
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x00023886 File Offset: 0x00021A86
		public bool IsAnchored { get; private set; }

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002388F File Offset: 0x00021A8F
		public QueryExpression ToPredicateExpression()
		{
			return this.PredicateExpression;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00023897 File Offset: 0x00021A97
		public IJoinPredicate ToPredicateWithCanonicalQueryExpressions()
		{
			return new ExpressionJoinPredicate(this.PredicateExpression, this.IsAnchored);
		}
	}
}
