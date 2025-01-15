using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001020 RID: 4128
	public struct LeafChildrenOf4 : IProgramNodeBuilder, IEquatable<LeafChildrenOf4>
	{
		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x060079D6 RID: 31190 RVA: 0x001A0F9A File Offset: 0x0019F19A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079D7 RID: 31191 RVA: 0x001A0FA2 File Offset: 0x0019F1A2
		private LeafChildrenOf4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079D8 RID: 31192 RVA: 0x001A0FAB File Offset: 0x0019F1AB
		public static LeafChildrenOf4 CreateUnsafe(ProgramNode node)
		{
			return new LeafChildrenOf4(node);
		}

		// Token: 0x060079D9 RID: 31193 RVA: 0x001A0FB4 File Offset: 0x0019F1B4
		public static LeafChildrenOf4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafChildrenOf4)
			{
				return null;
			}
			return new LeafChildrenOf4?(LeafChildrenOf4.CreateUnsafe(node));
		}

		// Token: 0x060079DA RID: 31194 RVA: 0x001A0FE9 File Offset: 0x0019F1E9
		public LeafChildrenOf4(GrammarBuilders g, selection9 value0)
		{
			this._node = g.Rule.LeafChildrenOf4.BuildASTNode(value0.Node);
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x001A1008 File Offset: 0x0019F208
		public static implicit operator selection8(LeafChildrenOf4 arg)
		{
			return selection8.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x060079DC RID: 31196 RVA: 0x001A1016 File Offset: 0x0019F216
		public selection9 selection9
		{
			get
			{
				return selection9.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x001A102A File Offset: 0x0019F22A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079DE RID: 31198 RVA: 0x001A1040 File Offset: 0x0019F240
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079DF RID: 31199 RVA: 0x001A106A File Offset: 0x0019F26A
		public bool Equals(LeafChildrenOf4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003339 RID: 13113
		private ProgramNode _node;
	}
}
