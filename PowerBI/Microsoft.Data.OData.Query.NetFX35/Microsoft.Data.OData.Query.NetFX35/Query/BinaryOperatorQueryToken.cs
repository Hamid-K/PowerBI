using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200000A RID: 10
	public sealed class BinaryOperatorQueryToken : QueryToken
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027DB File Offset: 0x000009DB
		public BinaryOperatorQueryToken(BinaryOperatorKind operatorKind, QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000280E File Offset: 0x00000A0E
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.BinaryOperator;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002811 File Offset: 0x00000A11
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002819 File Offset: 0x00000A19
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002821 File Offset: 0x00000A21
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000029 RID: 41
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x0400002A RID: 42
		private readonly QueryToken left;

		// Token: 0x0400002B RID: 43
		private readonly QueryToken right;
	}
}
