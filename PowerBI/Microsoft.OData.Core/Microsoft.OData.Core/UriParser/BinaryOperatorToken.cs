using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B7 RID: 439
	public sealed class BinaryOperatorToken : QueryToken
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x0003BC5E File Offset: 0x00039E5E
		public BinaryOperatorToken(BinaryOperatorKind operatorKind, QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x000397C8 File Offset: 0x000379C8
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.BinaryOperator;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0003BC93 File Offset: 0x00039E93
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0003BC9B File Offset: 0x00039E9B
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0003BCA3 File Offset: 0x00039EA3
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0003BCAB File Offset: 0x00039EAB
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040008FD RID: 2301
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x040008FE RID: 2302
		private readonly QueryToken left;

		// Token: 0x040008FF RID: 2303
		private readonly QueryToken right;
	}
}
