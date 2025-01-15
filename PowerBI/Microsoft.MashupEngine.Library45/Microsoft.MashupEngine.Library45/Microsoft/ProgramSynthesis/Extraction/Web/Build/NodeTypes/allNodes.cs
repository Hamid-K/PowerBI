using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x020010A0 RID: 4256
	public struct allNodes : IProgramNodeBuilder, IEquatable<allNodes>
	{
		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x06008047 RID: 32839 RVA: 0x001AD3C2 File Offset: 0x001AB5C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x001AD3CA File Offset: 0x001AB5CA
		private allNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x001AD3D3 File Offset: 0x001AB5D3
		public static allNodes CreateUnsafe(ProgramNode node)
		{
			return new allNodes(node);
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x001AD3DC File Offset: 0x001AB5DC
		public static allNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.allNodes)
			{
				return null;
			}
			return new allNodes?(allNodes.CreateUnsafe(node));
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x001AD416 File Offset: 0x001AB616
		public static allNodes CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new allNodes(new Hole(g.Symbol.allNodes, holeId));
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x001AD42E File Offset: 0x001AB62E
		public allNodes(GrammarBuilders g)
		{
			this = new allNodes(new VariableNode(g.Symbol.allNodes));
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x0600804D RID: 32845 RVA: 0x001AD446 File Offset: 0x001AB646
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x001AD453 File Offset: 0x001AB653
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600804F RID: 32847 RVA: 0x001AD468 File Offset: 0x001AB668
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x001AD492 File Offset: 0x001AB692
		public bool Equals(allNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B9 RID: 13241
		private ProgramNode _node;
	}
}
