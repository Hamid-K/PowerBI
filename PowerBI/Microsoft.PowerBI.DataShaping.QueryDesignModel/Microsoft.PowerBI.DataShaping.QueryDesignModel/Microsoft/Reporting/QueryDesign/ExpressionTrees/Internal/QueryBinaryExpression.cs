using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000166 RID: 358
	internal abstract class QueryBinaryExpression : QueryExpression
	{
		// Token: 0x06001442 RID: 5186 RVA: 0x0003AA3A File Offset: 0x00038C3A
		protected QueryBinaryExpression(ConceptualResultType conceptualResultType, QueryExpression left, QueryExpression right)
			: base(conceptualResultType)
		{
			this._left = ArgumentValidation.CheckNotNull<QueryExpression>(left, "left");
			this._right = ArgumentValidation.CheckNotNull<QueryExpression>(right, "right");
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0003AA65 File Offset: 0x00038C65
		public QueryExpression Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003AA6D File Offset: 0x00038C6D
		public QueryExpression Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0003AA78 File Offset: 0x00038C78
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryBinaryExpression queryBinaryExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryBinaryExpression>(this, other, out flag, out queryBinaryExpression))
			{
				return flag;
			}
			return this.Left.Equals(queryBinaryExpression.Left) && this.Right.Equals(queryBinaryExpression.Right);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0003AABA File Offset: 0x00038CBA
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Left.GetHashCode(), this.Right.GetHashCode());
		}

		// Token: 0x04000B14 RID: 2836
		private readonly QueryExpression _left;

		// Token: 0x04000B15 RID: 2837
		private readonly QueryExpression _right;
	}
}
