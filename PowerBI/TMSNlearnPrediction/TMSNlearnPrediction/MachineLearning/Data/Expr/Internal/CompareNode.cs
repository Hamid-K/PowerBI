using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B3 RID: 435
	internal sealed class CompareNode : ExprNode
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x00032E74 File Offset: 0x00031074
		public CompareNode(Token tok, CompareOp op, ListNode operands)
			: base(tok)
		{
			this.Op = op;
			this.Operands = operands;
			switch (op)
			{
			default:
				this.TidLax = 35;
				this.TidStrict = 36;
				return;
			case CompareOp.NotEqual:
				this.TidLax = 41;
				this.TidStrict = 34;
				return;
			case CompareOp.IncrChain:
				this.TidLax = 40;
				this.TidStrict = 38;
				return;
			case CompareOp.DecrChain:
				this.TidLax = 45;
				this.TidStrict = 43;
				return;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00032EF1 File Offset: 0x000310F1
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Compare;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00032EF4 File Offset: 0x000310F4
		public override CompareNode AsCompare
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00032EF7 File Offset: 0x000310F7
		public override CompareNode TestCompare
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00032EFA File Offset: 0x000310FA
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Operands.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004F9 RID: 1273
		public readonly CompareOp Op;

		// Token: 0x040004FA RID: 1274
		public readonly ListNode Operands;

		// Token: 0x040004FB RID: 1275
		public readonly TokKind TidStrict;

		// Token: 0x040004FC RID: 1276
		public readonly TokKind TidLax;

		// Token: 0x040004FD RID: 1277
		public ExprTypeKind ArgTypeKind;
	}
}
