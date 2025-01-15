using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001AA RID: 426
	internal sealed class IdentNode : ExprNode
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00032A95 File Offset: 0x00030C95
		public IdentNode(IdentToken tok)
			: base(tok)
		{
			this.Value = tok.Value;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00032AAA File Offset: 0x00030CAA
		public IdentNode(Token tok, string value, bool missing = false)
			: base(tok)
		{
			this.Value = value;
			this.IsMissing = missing;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00032AC1 File Offset: 0x00030CC1
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Ident;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x00032AC5 File Offset: 0x00030CC5
		public override IdentNode AsIdent
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00032AC8 File Offset: 0x00030CC8
		public override IdentNode TestIdent
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00032ACB File Offset: 0x00030CCB
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040004E2 RID: 1250
		public readonly string Value;

		// Token: 0x040004E3 RID: 1251
		public readonly bool IsMissing;

		// Token: 0x040004E4 RID: 1252
		public Node Referent;
	}
}
