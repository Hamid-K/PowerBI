using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CF RID: 463
	public sealed class UnaryOperatorToken : QueryToken
	{
		// Token: 0x06001529 RID: 5417 RVA: 0x0003C568 File Offset: 0x0003A768
		public UnaryOperatorToken(UnaryOperatorKind operatorKind, QueryToken operand)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(operand, "operand");
			this.operatorKind = operatorKind;
			this.operand = operand;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x000384BC File Offset: 0x000366BC
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.UnaryOperator;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0003C58A File Offset: 0x0003A78A
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0003C592 File Offset: 0x0003A792
		public QueryToken Operand
		{
			get
			{
				return this.operand;
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003C59A File Offset: 0x0003A79A
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000930 RID: 2352
		private readonly UnaryOperatorKind operatorKind;

		// Token: 0x04000931 RID: 2353
		private readonly QueryToken operand;
	}
}
