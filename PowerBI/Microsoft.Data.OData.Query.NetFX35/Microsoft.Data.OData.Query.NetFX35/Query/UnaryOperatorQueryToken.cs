using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000055 RID: 85
	public sealed class UnaryOperatorQueryToken : QueryToken
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000B630 File Offset: 0x00009830
		public UnaryOperatorQueryToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000B651 File Offset: 0x00009851
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000B654 File Offset: 0x00009854
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B65C File Offset: 0x0000985C
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x040001FC RID: 508
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x040001FD RID: 509
		private readonly QueryToken operand;
	}
}
