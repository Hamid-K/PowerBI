using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001A0 RID: 416
	internal sealed class QueryLookupValueExpression : QueryExtensionExpressionBase
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x0003C715 File Offset: 0x0003A915
		internal QueryLookupValueExpression(ConceptualResultType conceptualResultType, QueryExpression resultColumn, IReadOnlyList<QueryLookupTuple> lookupTuples)
			: base(conceptualResultType)
		{
			this._resultColumn = resultColumn;
			this._lookupTuples = lookupTuples;
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0003C72C File Offset: 0x0003A92C
		internal QueryExpression ResultColumn
		{
			get
			{
				return this._resultColumn;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0003C734 File Offset: 0x0003A934
		internal IReadOnlyList<QueryLookupTuple> LookupTuples
		{
			get
			{
				return this._lookupTuples;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0003C73C File Offset: 0x0003A93C
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0003C748 File Offset: 0x0003A948
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryLookupValueExpression queryLookupValueExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryLookupValueExpression>(this, other, out flag, out queryLookupValueExpression))
			{
				return flag;
			}
			return this.ResultColumn.Equals(queryLookupValueExpression.ResultColumn) && this.LookupTuples.SequenceEqual(queryLookupValueExpression.LookupTuples);
		}

		// Token: 0x04000B91 RID: 2961
		private readonly QueryExpression _resultColumn;

		// Token: 0x04000B92 RID: 2962
		private readonly IReadOnlyList<QueryLookupTuple> _lookupTuples;
	}
}
