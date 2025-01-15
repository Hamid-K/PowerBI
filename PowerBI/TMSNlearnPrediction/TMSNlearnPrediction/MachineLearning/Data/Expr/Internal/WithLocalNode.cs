using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B5 RID: 437
	internal sealed class WithLocalNode : Node
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x00032F62 File Offset: 0x00031162
		public WithLocalNode(Token tok, string name, ExprNode value)
			: base(tok)
		{
			this.Name = name;
			this.Value = value;
			this.Index = -1;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00032F80 File Offset: 0x00031180
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.WithLocal;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00032F84 File Offset: 0x00031184
		public override WithLocalNode AsWithLocal
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00032F87 File Offset: 0x00031187
		public override WithLocalNode TestWithLocal
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00032F8A File Offset: 0x0003118A
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				this.Value.Accept(visitor);
				visitor.PostVisit(this);
			}
		}

		// Token: 0x04000500 RID: 1280
		public readonly string Name;

		// Token: 0x04000501 RID: 1281
		public readonly ExprNode Value;

		// Token: 0x04000502 RID: 1282
		public int UseCount;

		// Token: 0x04000503 RID: 1283
		public int Index;

		// Token: 0x04000504 RID: 1284
		public int GenCount;
	}
}
