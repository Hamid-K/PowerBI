using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B4 RID: 436
	internal sealed class WithNode : ExprNode
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x00032F18 File Offset: 0x00031118
		public WithNode(Token tok, WithLocalNode local, ExprNode body)
			: base(tok)
		{
			this.Local = local;
			this.Body = body;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00032F2F File Offset: 0x0003112F
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.With;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00032F32 File Offset: 0x00031132
		public override WithNode AsWith
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00032F35 File Offset: 0x00031135
		public override WithNode TestWith
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00032F38 File Offset: 0x00031138
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Local.Accept(visitor);
				this.Body.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004FE RID: 1278
		public readonly WithLocalNode Local;

		// Token: 0x040004FF RID: 1279
		public readonly ExprNode Body;
	}
}
