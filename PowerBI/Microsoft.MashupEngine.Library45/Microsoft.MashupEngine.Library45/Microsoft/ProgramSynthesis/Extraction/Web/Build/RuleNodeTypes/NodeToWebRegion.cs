using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200100F RID: 4111
	public struct NodeToWebRegion : IProgramNodeBuilder, IEquatable<NodeToWebRegion>
	{
		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06007925 RID: 31013 RVA: 0x0019FFCE File Offset: 0x0019E1CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x0019FFD6 File Offset: 0x0019E1D6
		private NodeToWebRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x0019FFDF File Offset: 0x0019E1DF
		public static NodeToWebRegion CreateUnsafe(ProgramNode node)
		{
			return new NodeToWebRegion(node);
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x0019FFE8 File Offset: 0x0019E1E8
		public static NodeToWebRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeToWebRegion)
			{
				return null;
			}
			return new NodeToWebRegion?(NodeToWebRegion.CreateUnsafe(node));
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x001A001D File Offset: 0x0019E21D
		public NodeToWebRegion(GrammarBuilders g, beginNode value0)
		{
			this._node = g.Rule.NodeToWebRegion.BuildASTNode(value0.Node);
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x001A003C File Offset: 0x0019E23C
		public static implicit operator subNode(NodeToWebRegion arg)
		{
			return subNode.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600792B RID: 31019 RVA: 0x001A004A File Offset: 0x0019E24A
		public beginNode beginNode
		{
			get
			{
				return beginNode.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x001A005E File Offset: 0x0019E25E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x001A0074 File Offset: 0x0019E274
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x001A009E File Offset: 0x0019E29E
		public bool Equals(NodeToWebRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003328 RID: 13096
		private ProgramNode _node;
	}
}
