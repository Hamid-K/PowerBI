using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101D RID: 4125
	public struct LeafChildrenOf3 : IProgramNodeBuilder, IEquatable<LeafChildrenOf3>
	{
		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x060079B7 RID: 31159 RVA: 0x001A0CD6 File Offset: 0x0019EED6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079B8 RID: 31160 RVA: 0x001A0CDE File Offset: 0x0019EEDE
		private LeafChildrenOf3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079B9 RID: 31161 RVA: 0x001A0CE7 File Offset: 0x0019EEE7
		public static LeafChildrenOf3 CreateUnsafe(ProgramNode node)
		{
			return new LeafChildrenOf3(node);
		}

		// Token: 0x060079BA RID: 31162 RVA: 0x001A0CF0 File Offset: 0x0019EEF0
		public static LeafChildrenOf3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafChildrenOf3)
			{
				return null;
			}
			return new LeafChildrenOf3?(LeafChildrenOf3.CreateUnsafe(node));
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x001A0D25 File Offset: 0x0019EF25
		public LeafChildrenOf3(GrammarBuilders g, selection7 value0)
		{
			this._node = g.Rule.LeafChildrenOf3.BuildASTNode(value0.Node);
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x001A0D44 File Offset: 0x0019EF44
		public static implicit operator selection6(LeafChildrenOf3 arg)
		{
			return selection6.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x001A0D52 File Offset: 0x0019EF52
		public selection7 selection7
		{
			get
			{
				return selection7.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x001A0D66 File Offset: 0x0019EF66
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x001A0D7C File Offset: 0x0019EF7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x001A0DA6 File Offset: 0x0019EFA6
		public bool Equals(LeafChildrenOf3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003336 RID: 13110
		private ProgramNode _node;
	}
}
