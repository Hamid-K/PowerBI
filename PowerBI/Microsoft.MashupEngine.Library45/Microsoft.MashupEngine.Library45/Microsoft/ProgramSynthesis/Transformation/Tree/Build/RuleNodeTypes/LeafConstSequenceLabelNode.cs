using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E69 RID: 7785
	public struct LeafConstSequenceLabelNode : IProgramNodeBuilder, IEquatable<LeafConstSequenceLabelNode>
	{
		// Token: 0x17002BA2 RID: 11170
		// (get) Token: 0x06010677 RID: 67191 RVA: 0x00389BF6 File Offset: 0x00387DF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010678 RID: 67192 RVA: 0x00389BFE File Offset: 0x00387DFE
		private LeafConstSequenceLabelNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010679 RID: 67193 RVA: 0x00389C07 File Offset: 0x00387E07
		public static LeafConstSequenceLabelNode CreateUnsafe(ProgramNode node)
		{
			return new LeafConstSequenceLabelNode(node);
		}

		// Token: 0x0601067A RID: 67194 RVA: 0x00389C10 File Offset: 0x00387E10
		public static LeafConstSequenceLabelNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafConstSequenceLabelNode)
			{
				return null;
			}
			return new LeafConstSequenceLabelNode?(LeafConstSequenceLabelNode.CreateUnsafe(node));
		}

		// Token: 0x0601067B RID: 67195 RVA: 0x00389C45 File Offset: 0x00387E45
		public LeafConstSequenceLabelNode(GrammarBuilders g, label value0, attributes value1, construction value2)
		{
			this._node = g.Rule.LeafConstSequenceLabelNode.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0601067C RID: 67196 RVA: 0x00389C72 File Offset: 0x00387E72
		public static implicit operator construction(LeafConstSequenceLabelNode arg)
		{
			return construction.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BA3 RID: 11171
		// (get) Token: 0x0601067D RID: 67197 RVA: 0x00389C80 File Offset: 0x00387E80
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BA4 RID: 11172
		// (get) Token: 0x0601067E RID: 67198 RVA: 0x00389C94 File Offset: 0x00387E94
		public attributes attributes
		{
			get
			{
				return attributes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002BA5 RID: 11173
		// (get) Token: 0x0601067F RID: 67199 RVA: 0x00389CA8 File Offset: 0x00387EA8
		public construction construction
		{
			get
			{
				return construction.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06010680 RID: 67200 RVA: 0x00389CBC File Offset: 0x00387EBC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010681 RID: 67201 RVA: 0x00389CD0 File Offset: 0x00387ED0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010682 RID: 67202 RVA: 0x00389CFA File Offset: 0x00387EFA
		public bool Equals(LeafConstSequenceLabelNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A8 RID: 25256
		private ProgramNode _node;
	}
}
