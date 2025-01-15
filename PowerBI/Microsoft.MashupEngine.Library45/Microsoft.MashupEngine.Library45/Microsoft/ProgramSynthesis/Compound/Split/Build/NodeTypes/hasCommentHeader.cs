using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097E RID: 2430
	public struct hasCommentHeader : IProgramNodeBuilder, IEquatable<hasCommentHeader>
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000B2FE2 File Offset: 0x000B11E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000B2FEA File Offset: 0x000B11EA
		private hasCommentHeader(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000B2FF3 File Offset: 0x000B11F3
		public static hasCommentHeader CreateUnsafe(ProgramNode node)
		{
			return new hasCommentHeader(node);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000B2FFC File Offset: 0x000B11FC
		public static hasCommentHeader? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.hasCommentHeader)
			{
				return null;
			}
			return new hasCommentHeader?(hasCommentHeader.CreateUnsafe(node));
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000B3036 File Offset: 0x000B1236
		public static hasCommentHeader CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new hasCommentHeader(new Hole(g.Symbol.hasCommentHeader, holeId));
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000B304E File Offset: 0x000B124E
		public hasCommentHeader(GrammarBuilders g, bool value)
		{
			this = new hasCommentHeader(new LiteralNode(g.Symbol.hasCommentHeader, value));
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000B306C File Offset: 0x000B126C
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000B3083 File Offset: 0x000B1283
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000B3098 File Offset: 0x000B1298
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000B30C2 File Offset: 0x000B12C2
		public bool Equals(hasCommentHeader other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9E RID: 6814
		private ProgramNode _node;
	}
}
