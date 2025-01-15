using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106F RID: 4207
	public struct selection6 : IProgramNodeBuilder, IEquatable<selection6>
	{
		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x001A740E File Offset: 0x001A560E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D8A RID: 32138 RVA: 0x001A7416 File Offset: 0x001A5616
		private selection6(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D8B RID: 32139 RVA: 0x001A741F File Offset: 0x001A561F
		public static selection6 CreateUnsafe(ProgramNode node)
		{
			return new selection6(node);
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x001A7428 File Offset: 0x001A5628
		public static selection6? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection6)
			{
				return null;
			}
			return new selection6?(selection6.CreateUnsafe(node));
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x001A7462 File Offset: 0x001A5662
		public static selection6 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection6(new Hole(g.Symbol.selection6, holeId));
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x001A747A File Offset: 0x001A567A
		public bool Is_LeafChildrenOf3(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafChildrenOf3;
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x001A7494 File Offset: 0x001A5694
		public bool Is_LeafChildrenOf3(GrammarBuilders g, out LeafChildrenOf3 value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf3)
			{
				value = LeafChildrenOf3.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafChildrenOf3);
			return false;
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x001A74CC File Offset: 0x001A56CC
		public LeafChildrenOf3? As_LeafChildrenOf3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafChildrenOf3)
			{
				return null;
			}
			return new LeafChildrenOf3?(LeafChildrenOf3.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x001A750C File Offset: 0x001A570C
		public LeafChildrenOf3 Cast_LeafChildrenOf3(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf3)
			{
				return LeafChildrenOf3.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafChildrenOf3 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x001A7561 File Offset: 0x001A5761
		public bool Is_selection6_allNodes(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selection6_allNodes;
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x001A757B File Offset: 0x001A577B
		public bool Is_selection6_allNodes(GrammarBuilders g, out selection6_allNodes value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection6_allNodes)
			{
				value = selection6_allNodes.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selection6_allNodes);
			return false;
		}

		// Token: 0x06007D94 RID: 32148 RVA: 0x001A75B0 File Offset: 0x001A57B0
		public selection6_allNodes? As_selection6_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selection6_allNodes)
			{
				return null;
			}
			return new selection6_allNodes?(selection6_allNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D95 RID: 32149 RVA: 0x001A75F0 File Offset: 0x001A57F0
		public selection6_allNodes Cast_selection6_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection6_allNodes)
			{
				return selection6_allNodes.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selection6_allNodes is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D96 RID: 32150 RVA: 0x001A7648 File Offset: 0x001A5848
		public T Switch<T>(GrammarBuilders g, Func<LeafChildrenOf3, T> func0, Func<selection6_allNodes, T> func1)
		{
			LeafChildrenOf3 leafChildrenOf;
			if (this.Is_LeafChildrenOf3(g, out leafChildrenOf))
			{
				return func0(leafChildrenOf);
			}
			selection6_allNodes selection6_allNodes;
			if (this.Is_selection6_allNodes(g, out selection6_allNodes))
			{
				return func1(selection6_allNodes);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection6");
		}

		// Token: 0x06007D97 RID: 32151 RVA: 0x001A76A0 File Offset: 0x001A58A0
		public void Switch(GrammarBuilders g, Action<LeafChildrenOf3> func0, Action<selection6_allNodes> func1)
		{
			LeafChildrenOf3 leafChildrenOf;
			if (this.Is_LeafChildrenOf3(g, out leafChildrenOf))
			{
				func0(leafChildrenOf);
				return;
			}
			selection6_allNodes selection6_allNodes;
			if (this.Is_selection6_allNodes(g, out selection6_allNodes))
			{
				func1(selection6_allNodes);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection6");
		}

		// Token: 0x06007D98 RID: 32152 RVA: 0x001A76F7 File Offset: 0x001A58F7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x001A770C File Offset: 0x001A590C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x001A7736 File Offset: 0x001A5936
		public bool Equals(selection6 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003388 RID: 13192
		private ProgramNode _node;
	}
}
