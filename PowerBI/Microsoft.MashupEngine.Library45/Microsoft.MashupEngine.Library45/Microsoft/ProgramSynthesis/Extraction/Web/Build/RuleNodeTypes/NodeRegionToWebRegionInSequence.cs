using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001012 RID: 4114
	public struct NodeRegionToWebRegionInSequence : IProgramNodeBuilder, IEquatable<NodeRegionToWebRegionInSequence>
	{
		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06007944 RID: 31044 RVA: 0x001A0292 File Offset: 0x0019E492
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x001A029A File Offset: 0x0019E49A
		private NodeRegionToWebRegionInSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x001A02A3 File Offset: 0x0019E4A3
		public static NodeRegionToWebRegionInSequence CreateUnsafe(ProgramNode node)
		{
			return new NodeRegionToWebRegionInSequence(node);
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x001A02AC File Offset: 0x0019E4AC
		public static NodeRegionToWebRegionInSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeRegionToWebRegionInSequence)
			{
				return null;
			}
			return new NodeRegionToWebRegionInSequence?(NodeRegionToWebRegionInSequence.CreateUnsafe(node));
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001A02E1 File Offset: 0x0019E4E1
		public NodeRegionToWebRegionInSequence(GrammarBuilders g, regionStart value0, endNode value1)
		{
			this._node = g.Rule.NodeRegionToWebRegionInSequence.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001A0307 File Offset: 0x0019E507
		public static implicit operator mapRegionInSequence(NodeRegionToWebRegionInSequence arg)
		{
			return mapRegionInSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x0600794A RID: 31050 RVA: 0x001A0315 File Offset: 0x0019E515
		public regionStart regionStart
		{
			get
			{
				return regionStart.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x0600794B RID: 31051 RVA: 0x001A0329 File Offset: 0x0019E529
		public endNode endNode
		{
			get
			{
				return endNode.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x001A033D File Offset: 0x0019E53D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x001A0350 File Offset: 0x0019E550
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x001A037A File Offset: 0x0019E57A
		public bool Equals(NodeRegionToWebRegionInSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332B RID: 13099
		private ProgramNode _node;
	}
}
