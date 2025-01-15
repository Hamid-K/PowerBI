using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001010 RID: 4112
	public struct NodeToWebRegionInSequence : IProgramNodeBuilder, IEquatable<NodeToWebRegionInSequence>
	{
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x0600792F RID: 31023 RVA: 0x001A00B2 File Offset: 0x0019E2B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x001A00BA File Offset: 0x0019E2BA
		private NodeToWebRegionInSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x001A00C3 File Offset: 0x0019E2C3
		public static NodeToWebRegionInSequence CreateUnsafe(ProgramNode node)
		{
			return new NodeToWebRegionInSequence(node);
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x001A00CC File Offset: 0x0019E2CC
		public static NodeToWebRegionInSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeToWebRegionInSequence)
			{
				return null;
			}
			return new NodeToWebRegionInSequence?(NodeToWebRegionInSequence.CreateUnsafe(node));
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x001A0101 File Offset: 0x0019E301
		public NodeToWebRegionInSequence(GrammarBuilders g, node value0)
		{
			this._node = g.Rule.NodeToWebRegionInSequence.BuildASTNode(value0.Node);
		}

		// Token: 0x06007934 RID: 31028 RVA: 0x001A0120 File Offset: 0x0019E320
		public static implicit operator mapNodeInSequence(NodeToWebRegionInSequence arg)
		{
			return mapNodeInSequence.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06007935 RID: 31029 RVA: 0x001A012E File Offset: 0x0019E32E
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x001A0142 File Offset: 0x0019E342
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007937 RID: 31031 RVA: 0x001A0158 File Offset: 0x0019E358
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007938 RID: 31032 RVA: 0x001A0182 File Offset: 0x0019E382
		public bool Equals(NodeToWebRegionInSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003329 RID: 13097
		private ProgramNode _node;
	}
}
