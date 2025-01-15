using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AC RID: 428
	internal sealed class StrLitNode : ExprNode
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x00032BF7 File Offset: 0x00030DF7
		public StrLitNode(StrLitToken tok)
			: base(tok)
		{
			this.Value = new DvText(tok.Value);
			base.SetValue(this.Value);
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x00032C1D File Offset: 0x00030E1D
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.StrLit;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00032C21 File Offset: 0x00030E21
		public override StrLitNode AsStrLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x00032C24 File Offset: 0x00030E24
		public override StrLitNode TestStrLit
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00032C27 File Offset: 0x00030E27
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040004E5 RID: 1253
		public readonly DvText Value;
	}
}
