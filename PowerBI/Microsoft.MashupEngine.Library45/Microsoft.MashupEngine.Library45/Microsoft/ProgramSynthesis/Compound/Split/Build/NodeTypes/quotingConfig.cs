using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000979 RID: 2425
	public struct quotingConfig : IProgramNodeBuilder, IEquatable<quotingConfig>
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x000B2B1E File Offset: 0x000B0D1E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000B2B26 File Offset: 0x000B0D26
		private quotingConfig(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000B2B2F File Offset: 0x000B0D2F
		public static quotingConfig CreateUnsafe(ProgramNode node)
		{
			return new quotingConfig(node);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000B2B38 File Offset: 0x000B0D38
		public static quotingConfig? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.quotingConfig)
			{
				return null;
			}
			return new quotingConfig?(quotingConfig.CreateUnsafe(node));
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000B2B72 File Offset: 0x000B0D72
		public static quotingConfig CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new quotingConfig(new Hole(g.Symbol.quotingConfig, holeId));
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000B2B8A File Offset: 0x000B0D8A
		public quotingConfig(GrammarBuilders g, QuotingConfiguration value)
		{
			this = new quotingConfig(new LiteralNode(g.Symbol.quotingConfig, value));
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000B2BA8 File Offset: 0x000B0DA8
		public QuotingConfiguration Value
		{
			get
			{
				return (QuotingConfiguration)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000B2BBF File Offset: 0x000B0DBF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000B2BD4 File Offset: 0x000B0DD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000B2BFE File Offset: 0x000B0DFE
		public bool Equals(quotingConfig other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A99 RID: 6809
		private ProgramNode _node;
	}
}
