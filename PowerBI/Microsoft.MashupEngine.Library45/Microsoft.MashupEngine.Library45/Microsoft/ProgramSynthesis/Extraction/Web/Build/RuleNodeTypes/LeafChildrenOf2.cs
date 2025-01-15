using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101A RID: 4122
	public struct LeafChildrenOf2 : IProgramNodeBuilder, IEquatable<LeafChildrenOf2>
	{
		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06007998 RID: 31128 RVA: 0x001A0A12 File Offset: 0x0019EC12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x001A0A1A File Offset: 0x0019EC1A
		private LeafChildrenOf2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x001A0A23 File Offset: 0x0019EC23
		public static LeafChildrenOf2 CreateUnsafe(ProgramNode node)
		{
			return new LeafChildrenOf2(node);
		}

		// Token: 0x0600799B RID: 31131 RVA: 0x001A0A2C File Offset: 0x0019EC2C
		public static LeafChildrenOf2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafChildrenOf2)
			{
				return null;
			}
			return new LeafChildrenOf2?(LeafChildrenOf2.CreateUnsafe(node));
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x001A0A61 File Offset: 0x0019EC61
		public LeafChildrenOf2(GrammarBuilders g, selection5 value0)
		{
			this._node = g.Rule.LeafChildrenOf2.BuildASTNode(value0.Node);
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x001A0A80 File Offset: 0x0019EC80
		public static implicit operator selection4(LeafChildrenOf2 arg)
		{
			return selection4.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x0600799E RID: 31134 RVA: 0x001A0A8E File Offset: 0x0019EC8E
		public selection5 selection5
		{
			get
			{
				return selection5.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x001A0AA2 File Offset: 0x0019ECA2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x001A0AB8 File Offset: 0x0019ECB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x001A0AE2 File Offset: 0x0019ECE2
		public bool Equals(LeafChildrenOf2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003333 RID: 13107
		private ProgramNode _node;
	}
}
