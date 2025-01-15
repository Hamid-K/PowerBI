using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001028 RID: 4136
	public struct NodeName : IProgramNodeBuilder, IEquatable<NodeName>
	{
		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06007A2A RID: 31274 RVA: 0x001A171A File Offset: 0x0019F91A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A2B RID: 31275 RVA: 0x001A1722 File Offset: 0x0019F922
		private NodeName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A2C RID: 31276 RVA: 0x001A172B File Offset: 0x0019F92B
		public static NodeName CreateUnsafe(ProgramNode node)
		{
			return new NodeName(node);
		}

		// Token: 0x06007A2D RID: 31277 RVA: 0x001A1734 File Offset: 0x0019F934
		public static NodeName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeName)
			{
				return null;
			}
			return new NodeName?(NodeName.CreateUnsafe(node));
		}

		// Token: 0x06007A2E RID: 31278 RVA: 0x001A1769 File Offset: 0x0019F969
		public NodeName(GrammarBuilders g, name value0, node value1)
		{
			this._node = g.Rule.NodeName.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A2F RID: 31279 RVA: 0x001A178F File Offset: 0x0019F98F
		public static implicit operator atomExpr(NodeName arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06007A30 RID: 31280 RVA: 0x001A179D File Offset: 0x0019F99D
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06007A31 RID: 31281 RVA: 0x001A17B1 File Offset: 0x0019F9B1
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A32 RID: 31282 RVA: 0x001A17C5 File Offset: 0x0019F9C5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A33 RID: 31283 RVA: 0x001A17D8 File Offset: 0x0019F9D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x001A1802 File Offset: 0x0019FA02
		public bool Equals(NodeName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003341 RID: 13121
		private ProgramNode _node;
	}
}
