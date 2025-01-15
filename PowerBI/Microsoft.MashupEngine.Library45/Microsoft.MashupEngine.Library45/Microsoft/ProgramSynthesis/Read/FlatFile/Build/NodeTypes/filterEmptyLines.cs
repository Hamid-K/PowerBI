using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001288 RID: 4744
	public struct filterEmptyLines : IProgramNodeBuilder, IEquatable<filterEmptyLines>
	{
		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06008F86 RID: 36742 RVA: 0x001E2C32 File Offset: 0x001E0E32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F87 RID: 36743 RVA: 0x001E2C3A File Offset: 0x001E0E3A
		private filterEmptyLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F88 RID: 36744 RVA: 0x001E2C43 File Offset: 0x001E0E43
		public static filterEmptyLines CreateUnsafe(ProgramNode node)
		{
			return new filterEmptyLines(node);
		}

		// Token: 0x06008F89 RID: 36745 RVA: 0x001E2C4C File Offset: 0x001E0E4C
		public static filterEmptyLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.filterEmptyLines)
			{
				return null;
			}
			return new filterEmptyLines?(filterEmptyLines.CreateUnsafe(node));
		}

		// Token: 0x06008F8A RID: 36746 RVA: 0x001E2C86 File Offset: 0x001E0E86
		public static filterEmptyLines CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new filterEmptyLines(new Hole(g.Symbol.filterEmptyLines, holeId));
		}

		// Token: 0x06008F8B RID: 36747 RVA: 0x001E2C9E File Offset: 0x001E0E9E
		public filterEmptyLines(GrammarBuilders g, bool value)
		{
			this = new filterEmptyLines(new LiteralNode(g.Symbol.filterEmptyLines, value));
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06008F8C RID: 36748 RVA: 0x001E2CBC File Offset: 0x001E0EBC
		public bool Value
		{
			get
			{
				return (bool)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F8D RID: 36749 RVA: 0x001E2CD3 File Offset: 0x001E0ED3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F8E RID: 36750 RVA: 0x001E2CE8 File Offset: 0x001E0EE8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F8F RID: 36751 RVA: 0x001E2D12 File Offset: 0x001E0F12
		public bool Equals(filterEmptyLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A79 RID: 14969
		private ProgramNode _node;
	}
}
