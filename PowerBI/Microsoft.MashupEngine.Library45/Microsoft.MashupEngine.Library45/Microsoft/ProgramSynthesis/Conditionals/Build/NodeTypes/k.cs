using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A52 RID: 2642
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000CD37E File Offset: 0x000CB57E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000CD386 File Offset: 0x000CB586
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000CD38F File Offset: 0x000CB58F
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x000CD398 File Offset: 0x000CB598
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x000CD3D2 File Offset: 0x000CB5D2
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000CD3EA File Offset: 0x000CB5EA
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x000CD408 File Offset: 0x000CB608
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x000CD41F File Offset: 0x000CB61F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x000CD434 File Offset: 0x000CB634
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000CD45E File Offset: 0x000CB65E
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8D RID: 7565
		private ProgramNode _node;
	}
}
