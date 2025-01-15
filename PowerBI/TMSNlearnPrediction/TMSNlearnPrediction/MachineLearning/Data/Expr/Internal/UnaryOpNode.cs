using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AE RID: 430
	internal sealed class UnaryOpNode : ExprNode
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00032C79 File Offset: 0x00030E79
		public UnaryOpNode(Token tok, UnaryOp op, ExprNode arg)
			: base(tok)
		{
			this.Arg = arg;
			this.Op = op;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00032C90 File Offset: 0x00030E90
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.UnaryOp;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00032C93 File Offset: 0x00030E93
		public override UnaryOpNode AsUnaryOp
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00032C96 File Offset: 0x00030E96
		public override UnaryOpNode TestUnaryOp
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00032C99 File Offset: 0x00030E99
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Arg.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004E6 RID: 1254
		public readonly ExprNode Arg;

		// Token: 0x040004E7 RID: 1255
		public readonly UnaryOp Op;
	}
}
