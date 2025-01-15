using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B0 RID: 432
	internal sealed class ConditionalNode : ExprNode
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00032D09 File Offset: 0x00030F09
		public ConditionalNode(Token tok, ExprNode cond, ExprNode left, Token tokColon, ExprNode right)
			: base(tok)
		{
			this.Cond = cond;
			this.Left = left;
			this.Right = right;
			this.TokColon = tokColon;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00032D30 File Offset: 0x00030F30
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Conditional;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00032D33 File Offset: 0x00030F33
		public override ConditionalNode AsConditional
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00032D36 File Offset: 0x00030F36
		public override ConditionalNode TestConditional
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00032D39 File Offset: 0x00030F39
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Cond.Accept(visitor);
				this.Left.Accept(visitor);
				this.Right.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004ED RID: 1261
		public readonly ExprNode Cond;

		// Token: 0x040004EE RID: 1262
		public readonly ExprNode Left;

		// Token: 0x040004EF RID: 1263
		public readonly Token TokColon;

		// Token: 0x040004F0 RID: 1264
		public readonly ExprNode Right;
	}
}
