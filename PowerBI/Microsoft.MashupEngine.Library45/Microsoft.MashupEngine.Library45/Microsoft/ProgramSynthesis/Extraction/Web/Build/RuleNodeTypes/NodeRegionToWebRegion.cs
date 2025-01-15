using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001011 RID: 4113
	public struct NodeRegionToWebRegion : IProgramNodeBuilder, IEquatable<NodeRegionToWebRegion>
	{
		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x001A0196 File Offset: 0x0019E396
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x001A019E File Offset: 0x0019E39E
		private NodeRegionToWebRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x001A01A7 File Offset: 0x0019E3A7
		public static NodeRegionToWebRegion CreateUnsafe(ProgramNode node)
		{
			return new NodeRegionToWebRegion(node);
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x001A01B0 File Offset: 0x0019E3B0
		public static NodeRegionToWebRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeRegionToWebRegion)
			{
				return null;
			}
			return new NodeRegionToWebRegion?(NodeRegionToWebRegion.CreateUnsafe(node));
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x001A01E5 File Offset: 0x0019E3E5
		public NodeRegionToWebRegion(GrammarBuilders g, regionStart value0, endNode value1)
		{
			this._node = g.Rule.NodeRegionToWebRegion.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x001A020B File Offset: 0x0019E40B
		public static implicit operator _LetB0(NodeRegionToWebRegion arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x0600793F RID: 31039 RVA: 0x001A0219 File Offset: 0x0019E419
		public regionStart regionStart
		{
			get
			{
				return regionStart.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06007940 RID: 31040 RVA: 0x001A022D File Offset: 0x0019E42D
		public endNode endNode
		{
			get
			{
				return endNode.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001A0241 File Offset: 0x0019E441
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x001A0254 File Offset: 0x0019E454
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x001A027E File Offset: 0x0019E47E
		public bool Equals(NodeRegionToWebRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332A RID: 13098
		private ProgramNode _node;
	}
}
