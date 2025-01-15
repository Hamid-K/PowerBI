using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200106C RID: 4204
	public struct selection4 : IProgramNodeBuilder, IEquatable<selection4>
	{
		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06007D59 RID: 32089 RVA: 0x001A6CA6 File Offset: 0x001A4EA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x001A6CAE File Offset: 0x001A4EAE
		private selection4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D5B RID: 32091 RVA: 0x001A6CB7 File Offset: 0x001A4EB7
		public static selection4 CreateUnsafe(ProgramNode node)
		{
			return new selection4(node);
		}

		// Token: 0x06007D5C RID: 32092 RVA: 0x001A6CC0 File Offset: 0x001A4EC0
		public static selection4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection4)
			{
				return null;
			}
			return new selection4?(selection4.CreateUnsafe(node));
		}

		// Token: 0x06007D5D RID: 32093 RVA: 0x001A6CFA File Offset: 0x001A4EFA
		public static selection4 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection4(new Hole(g.Symbol.selection4, holeId));
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x001A6D12 File Offset: 0x001A4F12
		public bool Is_LeafChildrenOf2(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafChildrenOf2;
		}

		// Token: 0x06007D5F RID: 32095 RVA: 0x001A6D2C File Offset: 0x001A4F2C
		public bool Is_LeafChildrenOf2(GrammarBuilders g, out LeafChildrenOf2 value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf2)
			{
				value = LeafChildrenOf2.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafChildrenOf2);
			return false;
		}

		// Token: 0x06007D60 RID: 32096 RVA: 0x001A6D64 File Offset: 0x001A4F64
		public LeafChildrenOf2? As_LeafChildrenOf2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafChildrenOf2)
			{
				return null;
			}
			return new LeafChildrenOf2?(LeafChildrenOf2.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D61 RID: 32097 RVA: 0x001A6DA4 File Offset: 0x001A4FA4
		public LeafChildrenOf2 Cast_LeafChildrenOf2(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf2)
			{
				return LeafChildrenOf2.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafChildrenOf2 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D62 RID: 32098 RVA: 0x001A6DF9 File Offset: 0x001A4FF9
		public bool Is_selection4_allNodes(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selection4_allNodes;
		}

		// Token: 0x06007D63 RID: 32099 RVA: 0x001A6E13 File Offset: 0x001A5013
		public bool Is_selection4_allNodes(GrammarBuilders g, out selection4_allNodes value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection4_allNodes)
			{
				value = selection4_allNodes.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selection4_allNodes);
			return false;
		}

		// Token: 0x06007D64 RID: 32100 RVA: 0x001A6E48 File Offset: 0x001A5048
		public selection4_allNodes? As_selection4_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selection4_allNodes)
			{
				return null;
			}
			return new selection4_allNodes?(selection4_allNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x001A6E88 File Offset: 0x001A5088
		public selection4_allNodes Cast_selection4_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection4_allNodes)
			{
				return selection4_allNodes.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selection4_allNodes is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x001A6EE0 File Offset: 0x001A50E0
		public T Switch<T>(GrammarBuilders g, Func<LeafChildrenOf2, T> func0, Func<selection4_allNodes, T> func1)
		{
			LeafChildrenOf2 leafChildrenOf;
			if (this.Is_LeafChildrenOf2(g, out leafChildrenOf))
			{
				return func0(leafChildrenOf);
			}
			selection4_allNodes selection4_allNodes;
			if (this.Is_selection4_allNodes(g, out selection4_allNodes))
			{
				return func1(selection4_allNodes);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection4");
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x001A6F38 File Offset: 0x001A5138
		public void Switch(GrammarBuilders g, Action<LeafChildrenOf2> func0, Action<selection4_allNodes> func1)
		{
			LeafChildrenOf2 leafChildrenOf;
			if (this.Is_LeafChildrenOf2(g, out leafChildrenOf))
			{
				func0(leafChildrenOf);
				return;
			}
			selection4_allNodes selection4_allNodes;
			if (this.Is_selection4_allNodes(g, out selection4_allNodes))
			{
				func1(selection4_allNodes);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection4");
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x001A6F8F File Offset: 0x001A518F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x001A6FA4 File Offset: 0x001A51A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x001A6FCE File Offset: 0x001A51CE
		public bool Equals(selection4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003385 RID: 13189
		private ProgramNode _node;
	}
}
