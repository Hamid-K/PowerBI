using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001388 RID: 5000
	public struct pair : IProgramNodeBuilder, IEquatable<pair>
	{
		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x06009B43 RID: 39747 RVA: 0x0020C29E File Offset: 0x0020A49E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009B44 RID: 39748 RVA: 0x0020C2A6 File Offset: 0x0020A4A6
		private pair(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009B45 RID: 39749 RVA: 0x0020C2AF File Offset: 0x0020A4AF
		public static pair CreateUnsafe(ProgramNode node)
		{
			return new pair(node);
		}

		// Token: 0x06009B46 RID: 39750 RVA: 0x0020C2B8 File Offset: 0x0020A4B8
		public static pair? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pair)
			{
				return null;
			}
			return new pair?(pair.CreateUnsafe(node));
		}

		// Token: 0x06009B47 RID: 39751 RVA: 0x0020C2F2 File Offset: 0x0020A4F2
		public static pair CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pair(new Hole(g.Symbol.pair, holeId));
		}

		// Token: 0x06009B48 RID: 39752 RVA: 0x0020C30A File Offset: 0x0020A50A
		public pair(GrammarBuilders g)
		{
			this = new pair(new VariableNode(g.Symbol.pair));
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06009B49 RID: 39753 RVA: 0x0020C322 File Offset: 0x0020A522
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06009B4A RID: 39754 RVA: 0x0020C32F File Offset: 0x0020A52F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009B4B RID: 39755 RVA: 0x0020C344 File Offset: 0x0020A544
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009B4C RID: 39756 RVA: 0x0020C36E File Offset: 0x0020A56E
		public bool Equals(pair other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DFF RID: 15871
		private ProgramNode _node;
	}
}
