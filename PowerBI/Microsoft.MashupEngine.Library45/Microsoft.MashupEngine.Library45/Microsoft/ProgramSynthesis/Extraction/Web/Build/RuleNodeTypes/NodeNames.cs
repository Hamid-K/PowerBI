using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001029 RID: 4137
	public struct NodeNames : IProgramNodeBuilder, IEquatable<NodeNames>
	{
		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06007A35 RID: 31285 RVA: 0x001A1816 File Offset: 0x0019FA16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x001A181E File Offset: 0x0019FA1E
		private NodeNames(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x001A1827 File Offset: 0x0019FA27
		public static NodeNames CreateUnsafe(ProgramNode node)
		{
			return new NodeNames(node);
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x001A1830 File Offset: 0x0019FA30
		public static NodeNames? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NodeNames)
			{
				return null;
			}
			return new NodeNames?(NodeNames.CreateUnsafe(node));
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x001A1865 File Offset: 0x0019FA65
		public NodeNames(GrammarBuilders g, names value0, node value1)
		{
			this._node = g.Rule.NodeNames.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x001A188B File Offset: 0x0019FA8B
		public static implicit operator atomExpr(NodeNames arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06007A3B RID: 31291 RVA: 0x001A1899 File Offset: 0x0019FA99
		public names names
		{
			get
			{
				return names.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x001A18AD File Offset: 0x0019FAAD
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x001A18C1 File Offset: 0x0019FAC1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x001A18D4 File Offset: 0x0019FAD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A3F RID: 31295 RVA: 0x001A18FE File Offset: 0x0019FAFE
		public bool Equals(NodeNames other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003342 RID: 13122
		private ProgramNode _node;
	}
}
