using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000169 RID: 361
	public sealed class BinaryOperatorToken : QueryToken
	{
		// Token: 0x06000F4D RID: 3917 RVA: 0x0002BBAA File Offset: 0x00029DAA
		public BinaryOperatorToken(BinaryOperatorKind operatorKind, QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0002BBDF File Offset: 0x00029DDF
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.BinaryOperator;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0002BBE2 File Offset: 0x00029DE2
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0002BBEA File Offset: 0x00029DEA
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0002BBF2 File Offset: 0x00029DF2
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002BBFA File Offset: 0x00029DFA
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007AF RID: 1967
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x040007B0 RID: 1968
		private readonly QueryToken left;

		// Token: 0x040007B1 RID: 1969
		private readonly QueryToken right;
	}
}
