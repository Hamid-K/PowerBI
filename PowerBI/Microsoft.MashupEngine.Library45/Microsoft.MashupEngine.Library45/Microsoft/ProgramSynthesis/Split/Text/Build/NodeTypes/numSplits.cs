using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001383 RID: 4995
	public struct numSplits : IProgramNodeBuilder, IEquatable<numSplits>
	{
		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x06009B11 RID: 39697 RVA: 0x0020BDFA File Offset: 0x00209FFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B12 RID: 39698 RVA: 0x0020BE02 File Offset: 0x0020A002
		private numSplits(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B13 RID: 39699 RVA: 0x0020BE0B File Offset: 0x0020A00B
		public static numSplits CreateUnsafe(ProgramNode node)
		{
			return new numSplits(node);
		}

		// Token: 0x06009B14 RID: 39700 RVA: 0x0020BE14 File Offset: 0x0020A014
		public static numSplits? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numSplits)
			{
				return null;
			}
			return new numSplits?(numSplits.CreateUnsafe(node));
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x0020BE4E File Offset: 0x0020A04E
		public static numSplits CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numSplits(new Hole(g.Symbol.numSplits, holeId));
		}

		// Token: 0x06009B16 RID: 39702 RVA: 0x0020BE66 File Offset: 0x0020A066
		public numSplits(GrammarBuilders g, int value)
		{
			this = new numSplits(new LiteralNode(g.Symbol.numSplits, value));
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x06009B17 RID: 39703 RVA: 0x0020BE84 File Offset: 0x0020A084
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x0020BE9B File Offset: 0x0020A09B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x0020BEB0 File Offset: 0x0020A0B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B1A RID: 39706 RVA: 0x0020BEDA File Offset: 0x0020A0DA
		public bool Equals(numSplits other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFA RID: 15866
		private ProgramNode _node;
	}
}
