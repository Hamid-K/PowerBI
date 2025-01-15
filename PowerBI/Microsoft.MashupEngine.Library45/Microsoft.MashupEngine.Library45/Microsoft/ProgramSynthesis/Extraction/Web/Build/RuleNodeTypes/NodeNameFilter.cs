using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001041 RID: 4161
	public struct NodeNameFilter : IProgramNodeBuilder, IEquatable<NodeNameFilter>
	{
		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x06007B38 RID: 31544 RVA: 0x001A2F4E File Offset: 0x001A114E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x001A2F56 File Offset: 0x001A1156
		private NodeNameFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x001A2F5F File Offset: 0x001A115F
		public static NodeNameFilter CreateUnsafe(ProgramNode node)
		{
			return new NodeNameFilter(node);
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x001A2F68 File Offset: 0x001A1168
		public static NodeNameFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeNameFilter)
			{
				return null;
			}
			return new NodeNameFilter?(NodeNameFilter.CreateUnsafe(node));
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x001A2F9D File Offset: 0x001A119D
		public NodeNameFilter(GrammarBuilders g, nodeName value0, nodeCollection value1)
		{
			this._node = g.Rule.NodeNameFilter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x001A2FC3 File Offset: 0x001A11C3
		public static implicit operator nodeCollection(NodeNameFilter arg)
		{
			return nodeCollection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x06007B3E RID: 31550 RVA: 0x001A2FD1 File Offset: 0x001A11D1
		public nodeName nodeName
		{
			get
			{
				return nodeName.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x06007B3F RID: 31551 RVA: 0x001A2FE5 File Offset: 0x001A11E5
		public nodeCollection nodeCollection
		{
			get
			{
				return nodeCollection.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x001A2FF9 File Offset: 0x001A11F9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x001A300C File Offset: 0x001A120C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x001A3036 File Offset: 0x001A1236
		public bool Equals(NodeNameFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400335A RID: 13146
		private ProgramNode _node;
	}
}
