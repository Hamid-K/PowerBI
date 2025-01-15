using System;
using Microsoft.MachineLearning.Internal.Lexer;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001B1 RID: 433
	internal sealed class ListNode : Node
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00032D6F File Offset: 0x00030F6F
		public ListNode(Token tok, Node[] items, Token[] delimiters)
			: base(tok)
		{
			this.Items = items;
			this.Delimiters = delimiters;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00032D86 File Offset: 0x00030F86
		public override NodeKind Kind
		{
			get
			{
				return NodeKind.List;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00032D89 File Offset: 0x00030F89
		public override ListNode AsList
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00032D8C File Offset: 0x00030F8C
		public override ListNode TestList
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00032D90 File Offset: 0x00030F90
		public override void Accept(NodeVisitor visitor)
		{
			if (visitor.PreVisit(this))
			{
				foreach (Node node in this.Items)
				{
					node.Accept(visitor);
				}
				visitor.PostVisit(this);
			}
		}

		// Token: 0x040004F1 RID: 1265
		public readonly Node[] Items;

		// Token: 0x040004F2 RID: 1266
		public readonly Token[] Delimiters;
	}
}
