using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AD RID: 429
	internal sealed class BoolLitNode : ExprNode
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00032C30 File Offset: 0x00030E30
		public BoolLitNode(Token tok)
			: base(tok)
		{
			base.SetValue((tok.Kind == 62) ? DvBool.True : DvBool.False);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00032C55 File Offset: 0x00030E55
		public bool Value
		{
			get
			{
				return this.Token.Kind == 62;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x00032C66 File Offset: 0x00030E66
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.BoolLit;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00032C6A File Offset: 0x00030E6A
		public override BoolLitNode AsBoolLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00032C6D File Offset: 0x00030E6D
		public override BoolLitNode TestBoolLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00032C70 File Offset: 0x00030E70
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
