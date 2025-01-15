using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E92 RID: 7826
	public struct p : IProgramNodeBuilder, IEquatable<p>
	{
		// Token: 0x17002BF1 RID: 11249
		// (get) Token: 0x06010892 RID: 67730 RVA: 0x0038DCCE File Offset: 0x0038BECE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010893 RID: 67731 RVA: 0x0038DCD6 File Offset: 0x0038BED6
		private p(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010894 RID: 67732 RVA: 0x0038DCDF File Offset: 0x0038BEDF
		public static p CreateUnsafe(ProgramNode node)
		{
			return new p(node);
		}

		// Token: 0x06010895 RID: 67733 RVA: 0x0038DCE8 File Offset: 0x0038BEE8
		public static p? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.p)
			{
				return null;
			}
			return new p?(p.CreateUnsafe(node));
		}

		// Token: 0x06010896 RID: 67734 RVA: 0x0038DD22 File Offset: 0x0038BF22
		public static p CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new p(new Hole(g.Symbol.p, holeId));
		}

		// Token: 0x06010897 RID: 67735 RVA: 0x0038DD3A File Offset: 0x0038BF3A
		public p(GrammarBuilders g, int value)
		{
			this = new p(new LiteralNode(g.Symbol.p, value));
		}

		// Token: 0x17002BF2 RID: 11250
		// (get) Token: 0x06010898 RID: 67736 RVA: 0x0038DD58 File Offset: 0x0038BF58
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06010899 RID: 67737 RVA: 0x0038DD6F File Offset: 0x0038BF6F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601089A RID: 67738 RVA: 0x0038DD84 File Offset: 0x0038BF84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601089B RID: 67739 RVA: 0x0038DDAE File Offset: 0x0038BFAE
		public bool Equals(p other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D1 RID: 25297
		private ProgramNode _node;
	}
}
