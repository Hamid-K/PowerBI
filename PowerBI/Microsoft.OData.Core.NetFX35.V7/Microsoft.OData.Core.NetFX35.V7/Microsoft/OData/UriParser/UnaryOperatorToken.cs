using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000182 RID: 386
	public sealed class UnaryOperatorToken : QueryToken
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0002C2F3 File Offset: 0x0002A4F3
		public UnaryOperatorToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00028B88 File Offset: 0x00026D88
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0002C315 File Offset: 0x0002A515
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0002C31D File Offset: 0x0002A51D
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002C325 File Offset: 0x0002A525
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040007FE RID: 2046
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x040007FF RID: 2047
		private readonly QueryToken operand;
	}
}
