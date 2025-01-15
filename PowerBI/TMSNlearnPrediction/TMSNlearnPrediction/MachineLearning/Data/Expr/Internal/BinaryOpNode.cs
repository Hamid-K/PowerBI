using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AF RID: 431
	internal sealed class BinaryOpNode : ExprNode
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x00032CB7 File Offset: 0x00030EB7
		public BinaryOpNode(Token tok, BinaryOp op, ExprNode left, ExprNode right)
			: base(tok)
		{
			this.Left = left;
			this.Right = right;
			this.Op = op;
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00032CD6 File Offset: 0x00030ED6
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.BinaryOp;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00032CD9 File Offset: 0x00030ED9
		public override BinaryOpNode AsBinaryOp
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00032CDC File Offset: 0x00030EDC
		public override BinaryOpNode TestBinaryOp
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00032CDF File Offset: 0x00030EDF
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Left.Accept(visitor);
				this.Right.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004E8 RID: 1256
		public readonly ExprNode Left;

		// Token: 0x040004E9 RID: 1257
		public readonly ExprNode Right;

		// Token: 0x040004EA RID: 1258
		public readonly BinaryOp Op;

		// Token: 0x040004EB RID: 1259
		public bool ReduceToLeft;

		// Token: 0x040004EC RID: 1260
		public bool ReduceToRight;
	}
}
