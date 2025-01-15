using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001069 RID: 4201
	public struct selection2 : IProgramNodeBuilder, IEquatable<selection2>
	{
		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06007D29 RID: 32041 RVA: 0x001A653E File Offset: 0x001A473E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D2A RID: 32042 RVA: 0x001A6546 File Offset: 0x001A4746
		private selection2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D2B RID: 32043 RVA: 0x001A654F File Offset: 0x001A474F
		public static selection2 CreateUnsafe(ProgramNode node)
		{
			return new selection2(node);
		}

		// Token: 0x06007D2C RID: 32044 RVA: 0x001A6558 File Offset: 0x001A4758
		public static selection2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection2)
			{
				return null;
			}
			return new selection2?(selection2.CreateUnsafe(node));
		}

		// Token: 0x06007D2D RID: 32045 RVA: 0x001A6592 File Offset: 0x001A4792
		public static selection2 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection2(new Hole(g.Symbol.selection2, holeId));
		}

		// Token: 0x06007D2E RID: 32046 RVA: 0x001A65AA File Offset: 0x001A47AA
		public bool Is_LeafChildrenOf1(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafChildrenOf1;
		}

		// Token: 0x06007D2F RID: 32047 RVA: 0x001A65C4 File Offset: 0x001A47C4
		public bool Is_LeafChildrenOf1(GrammarBuilders g, out LeafChildrenOf1 value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf1)
			{
				value = LeafChildrenOf1.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafChildrenOf1);
			return false;
		}

		// Token: 0x06007D30 RID: 32048 RVA: 0x001A65FC File Offset: 0x001A47FC
		public LeafChildrenOf1? As_LeafChildrenOf1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafChildrenOf1)
			{
				return null;
			}
			return new LeafChildrenOf1?(LeafChildrenOf1.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D31 RID: 32049 RVA: 0x001A663C File Offset: 0x001A483C
		public LeafChildrenOf1 Cast_LeafChildrenOf1(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf1)
			{
				return LeafChildrenOf1.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafChildrenOf1 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D32 RID: 32050 RVA: 0x001A6691 File Offset: 0x001A4891
		public bool Is_selection2_allNodes(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selection2_allNodes;
		}

		// Token: 0x06007D33 RID: 32051 RVA: 0x001A66AB File Offset: 0x001A48AB
		public bool Is_selection2_allNodes(GrammarBuilders g, out selection2_allNodes value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection2_allNodes)
			{
				value = selection2_allNodes.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selection2_allNodes);
			return false;
		}

		// Token: 0x06007D34 RID: 32052 RVA: 0x001A66E0 File Offset: 0x001A48E0
		public selection2_allNodes? As_selection2_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selection2_allNodes)
			{
				return null;
			}
			return new selection2_allNodes?(selection2_allNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D35 RID: 32053 RVA: 0x001A6720 File Offset: 0x001A4920
		public selection2_allNodes Cast_selection2_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection2_allNodes)
			{
				return selection2_allNodes.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selection2_allNodes is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D36 RID: 32054 RVA: 0x001A6778 File Offset: 0x001A4978
		public T Switch<T>(GrammarBuilders g, Func<LeafChildrenOf1, T> func0, Func<selection2_allNodes, T> func1)
		{
			LeafChildrenOf1 leafChildrenOf;
			if (this.Is_LeafChildrenOf1(g, out leafChildrenOf))
			{
				return func0(leafChildrenOf);
			}
			selection2_allNodes selection2_allNodes;
			if (this.Is_selection2_allNodes(g, out selection2_allNodes))
			{
				return func1(selection2_allNodes);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection2");
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x001A67D0 File Offset: 0x001A49D0
		public void Switch(GrammarBuilders g, Action<LeafChildrenOf1> func0, Action<selection2_allNodes> func1)
		{
			LeafChildrenOf1 leafChildrenOf;
			if (this.Is_LeafChildrenOf1(g, out leafChildrenOf))
			{
				func0(leafChildrenOf);
				return;
			}
			selection2_allNodes selection2_allNodes;
			if (this.Is_selection2_allNodes(g, out selection2_allNodes))
			{
				func1(selection2_allNodes);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection2");
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x001A6827 File Offset: 0x001A4A27
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x001A683C File Offset: 0x001A4A3C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x001A6866 File Offset: 0x001A4A66
		public bool Equals(selection2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003382 RID: 13186
		private ProgramNode _node;
	}
}
