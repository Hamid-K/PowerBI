using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001072 RID: 4210
	public struct selection8 : IProgramNodeBuilder, IEquatable<selection8>
	{
		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06007DB9 RID: 32185 RVA: 0x001A7B76 File Offset: 0x001A5D76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007DBA RID: 32186 RVA: 0x001A7B7E File Offset: 0x001A5D7E
		private selection8(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007DBB RID: 32187 RVA: 0x001A7B87 File Offset: 0x001A5D87
		public static selection8 CreateUnsafe(ProgramNode node)
		{
			return new selection8(node);
		}

		// Token: 0x06007DBC RID: 32188 RVA: 0x001A7B90 File Offset: 0x001A5D90
		public static selection8? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selection8)
			{
				return null;
			}
			return new selection8?(selection8.CreateUnsafe(node));
		}

		// Token: 0x06007DBD RID: 32189 RVA: 0x001A7BCA File Offset: 0x001A5DCA
		public static selection8 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selection8(new Hole(g.Symbol.selection8, holeId));
		}

		// Token: 0x06007DBE RID: 32190 RVA: 0x001A7BE2 File Offset: 0x001A5DE2
		public bool Is_LeafChildrenOf4(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeafChildrenOf4;
		}

		// Token: 0x06007DBF RID: 32191 RVA: 0x001A7BFC File Offset: 0x001A5DFC
		public bool Is_LeafChildrenOf4(GrammarBuilders g, out LeafChildrenOf4 value)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf4)
			{
				value = LeafChildrenOf4.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeafChildrenOf4);
			return false;
		}

		// Token: 0x06007DC0 RID: 32192 RVA: 0x001A7C34 File Offset: 0x001A5E34
		public LeafChildrenOf4? As_LeafChildrenOf4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeafChildrenOf4)
			{
				return null;
			}
			return new LeafChildrenOf4?(LeafChildrenOf4.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DC1 RID: 32193 RVA: 0x001A7C74 File Offset: 0x001A5E74
		public LeafChildrenOf4 Cast_LeafChildrenOf4(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeafChildrenOf4)
			{
				return LeafChildrenOf4.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeafChildrenOf4 is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DC2 RID: 32194 RVA: 0x001A7CC9 File Offset: 0x001A5EC9
		public bool Is_selection8_allNodes(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selection8_allNodes;
		}

		// Token: 0x06007DC3 RID: 32195 RVA: 0x001A7CE3 File Offset: 0x001A5EE3
		public bool Is_selection8_allNodes(GrammarBuilders g, out selection8_allNodes value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection8_allNodes)
			{
				value = selection8_allNodes.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selection8_allNodes);
			return false;
		}

		// Token: 0x06007DC4 RID: 32196 RVA: 0x001A7D18 File Offset: 0x001A5F18
		public selection8_allNodes? As_selection8_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selection8_allNodes)
			{
				return null;
			}
			return new selection8_allNodes?(selection8_allNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007DC5 RID: 32197 RVA: 0x001A7D58 File Offset: 0x001A5F58
		public selection8_allNodes Cast_selection8_allNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selection8_allNodes)
			{
				return selection8_allNodes.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selection8_allNodes is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007DC6 RID: 32198 RVA: 0x001A7DB0 File Offset: 0x001A5FB0
		public T Switch<T>(GrammarBuilders g, Func<LeafChildrenOf4, T> func0, Func<selection8_allNodes, T> func1)
		{
			LeafChildrenOf4 leafChildrenOf;
			if (this.Is_LeafChildrenOf4(g, out leafChildrenOf))
			{
				return func0(leafChildrenOf);
			}
			selection8_allNodes selection8_allNodes;
			if (this.Is_selection8_allNodes(g, out selection8_allNodes))
			{
				return func1(selection8_allNodes);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection8");
		}

		// Token: 0x06007DC7 RID: 32199 RVA: 0x001A7E08 File Offset: 0x001A6008
		public void Switch(GrammarBuilders g, Action<LeafChildrenOf4> func0, Action<selection8_allNodes> func1)
		{
			LeafChildrenOf4 leafChildrenOf;
			if (this.Is_LeafChildrenOf4(g, out leafChildrenOf))
			{
				func0(leafChildrenOf);
				return;
			}
			selection8_allNodes selection8_allNodes;
			if (this.Is_selection8_allNodes(g, out selection8_allNodes))
			{
				func1(selection8_allNodes);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selection8");
		}

		// Token: 0x06007DC8 RID: 32200 RVA: 0x001A7E5F File Offset: 0x001A605F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007DC9 RID: 32201 RVA: 0x001A7E74 File Offset: 0x001A6074
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007DCA RID: 32202 RVA: 0x001A7E9E File Offset: 0x001A609E
		public bool Equals(selection8 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400338B RID: 13195
		private ProgramNode _node;
	}
}
