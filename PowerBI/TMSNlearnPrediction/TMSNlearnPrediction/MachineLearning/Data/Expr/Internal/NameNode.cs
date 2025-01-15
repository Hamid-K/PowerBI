using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A9 RID: 425
	internal sealed class NameNode : Node
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x00032A6D File Offset: 0x00030C6D
		public NameNode(IdentToken tok)
			: base(tok)
		{
			this.Value = tok.Value;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00032A82 File Offset: 0x00030C82
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.Name;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00032A86 File Offset: 0x00030C86
		public override NameNode AsName
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00032A89 File Offset: 0x00030C89
		public override NameNode TestName
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00032A8C File Offset: 0x00030C8C
		public override void Accept(NodeVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040004E1 RID: 1249
		public readonly string Value;
	}
}
