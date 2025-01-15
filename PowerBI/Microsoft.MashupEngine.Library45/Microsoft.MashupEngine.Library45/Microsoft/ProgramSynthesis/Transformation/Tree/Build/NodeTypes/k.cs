using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E91 RID: 7825
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x17002BEF RID: 11247
		// (get) Token: 0x06010888 RID: 67720 RVA: 0x0038DBDA File Offset: 0x0038BDDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010889 RID: 67721 RVA: 0x0038DBE2 File Offset: 0x0038BDE2
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601088A RID: 67722 RVA: 0x0038DBEB File Offset: 0x0038BDEB
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x0601088B RID: 67723 RVA: 0x0038DBF4 File Offset: 0x0038BDF4
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x0601088C RID: 67724 RVA: 0x0038DC2E File Offset: 0x0038BE2E
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x0601088D RID: 67725 RVA: 0x0038DC46 File Offset: 0x0038BE46
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17002BF0 RID: 11248
		// (get) Token: 0x0601088E RID: 67726 RVA: 0x0038DC64 File Offset: 0x0038BE64
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0601088F RID: 67727 RVA: 0x0038DC7B File Offset: 0x0038BE7B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010890 RID: 67728 RVA: 0x0038DC90 File Offset: 0x0038BE90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010891 RID: 67729 RVA: 0x0038DCBA File Offset: 0x0038BEBA
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062D0 RID: 25296
		private ProgramNode _node;
	}
}
