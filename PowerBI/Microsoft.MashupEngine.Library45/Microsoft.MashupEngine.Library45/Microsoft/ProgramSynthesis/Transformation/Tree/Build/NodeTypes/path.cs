using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling.Tree;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E93 RID: 7827
	public struct path : IProgramNodeBuilder, IEquatable<path>
	{
		// Token: 0x17002BF3 RID: 11251
		// (get) Token: 0x0601089C RID: 67740 RVA: 0x0038DDC2 File Offset: 0x0038BFC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601089D RID: 67741 RVA: 0x0038DDCA File Offset: 0x0038BFCA
		private path(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601089E RID: 67742 RVA: 0x0038DDD3 File Offset: 0x0038BFD3
		public static path CreateUnsafe(ProgramNode node)
		{
			return new path(node);
		}

		// Token: 0x0601089F RID: 67743 RVA: 0x0038DDDC File Offset: 0x0038BFDC
		public static path? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.path)
			{
				return null;
			}
			return new path?(path.CreateUnsafe(node));
		}

		// Token: 0x060108A0 RID: 67744 RVA: 0x0038DE16 File Offset: 0x0038C016
		public static path CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new path(new Hole(g.Symbol.path, holeId));
		}

		// Token: 0x060108A1 RID: 67745 RVA: 0x0038DE2E File Offset: 0x0038C02E
		public path(GrammarBuilders g, TreePath value)
		{
			this = new path(new LiteralNode(g.Symbol.path, value));
		}

		// Token: 0x17002BF4 RID: 11252
		// (get) Token: 0x060108A2 RID: 67746 RVA: 0x0038DE47 File Offset: 0x0038C047
		public TreePath Value
		{
			get
			{
				return (TreePath)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060108A3 RID: 67747 RVA: 0x0038DE5E File Offset: 0x0038C05E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060108A4 RID: 67748 RVA: 0x0038DE74 File Offset: 0x0038C074
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060108A5 RID: 67749 RVA: 0x0038DE9E File Offset: 0x0038C09E
		public bool Equals(path other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D2 RID: 25298
		private ProgramNode _node;
	}
}
