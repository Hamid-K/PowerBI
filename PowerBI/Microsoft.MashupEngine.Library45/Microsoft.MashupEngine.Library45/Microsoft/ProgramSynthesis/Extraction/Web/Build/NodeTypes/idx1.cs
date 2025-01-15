using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001093 RID: 4243
	public struct idx1 : IProgramNodeBuilder, IEquatable<idx1>
	{
		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x001AC7AA File Offset: 0x001AA9AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x001AC7B2 File Offset: 0x001AA9B2
		private idx1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x001AC7BB File Offset: 0x001AA9BB
		public static idx1 CreateUnsafe(ProgramNode node)
		{
			return new idx1(node);
		}

		// Token: 0x06007FC8 RID: 32712 RVA: 0x001AC7C4 File Offset: 0x001AA9C4
		public static idx1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.idx1)
			{
				return null;
			}
			return new idx1?(idx1.CreateUnsafe(node));
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x001AC7FE File Offset: 0x001AA9FE
		public static idx1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new idx1(new Hole(g.Symbol.idx1, holeId));
		}

		// Token: 0x06007FCA RID: 32714 RVA: 0x001AC816 File Offset: 0x001AAA16
		public idx1(GrammarBuilders g, int value)
		{
			this = new idx1(new LiteralNode(g.Symbol.idx1, value));
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06007FCB RID: 32715 RVA: 0x001AC834 File Offset: 0x001AAA34
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x001AC84B File Offset: 0x001AAA4B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FCD RID: 32717 RVA: 0x001AC860 File Offset: 0x001AAA60
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x001AC88A File Offset: 0x001AAA8A
		public bool Equals(idx1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AC RID: 13228
		private ProgramNode _node;
	}
}
