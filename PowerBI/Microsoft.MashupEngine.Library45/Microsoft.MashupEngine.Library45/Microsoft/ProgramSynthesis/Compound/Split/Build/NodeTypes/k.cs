using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000978 RID: 2424
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000B2A2A File Offset: 0x000B0C2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000B2A32 File Offset: 0x000B0C32
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000B2A3B File Offset: 0x000B0C3B
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000B2A44 File Offset: 0x000B0C44
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000B2A7E File Offset: 0x000B0C7E
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000B2A96 File Offset: 0x000B0C96
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000B2AB4 File Offset: 0x000B0CB4
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000B2ACB File Offset: 0x000B0CCB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000B2AE0 File Offset: 0x000B0CE0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000B2B0A File Offset: 0x000B0D0A
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A98 RID: 6808
		private ProgramNode _node;
	}
}
