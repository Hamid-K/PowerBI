using System;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000118 RID: 280
	public sealed class BinaryOperatorToken : QueryToken
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0002C90D File Offset: 0x0002AB0D
		public BinaryOperatorToken(BinaryOperatorKind operatorKind, QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.operatorKind = operatorKind;
			this.left = left;
			this.right = right;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00006F67 File Offset: 0x00005167
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.BinaryOperator;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002C942 File Offset: 0x0002AB42
		public BinaryOperatorKind OperatorKind
		{
			get
			{
				return this.operatorKind;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002C94A File Offset: 0x0002AB4A
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0002C952 File Offset: 0x0002AB52
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002C95A File Offset: 0x0002AB5A
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000652 RID: 1618
		private readonly BinaryOperatorKind operatorKind;

		// Token: 0x04000653 RID: 1619
		private readonly QueryToken left;

		// Token: 0x04000654 RID: 1620
		private readonly QueryToken right;
	}
}
