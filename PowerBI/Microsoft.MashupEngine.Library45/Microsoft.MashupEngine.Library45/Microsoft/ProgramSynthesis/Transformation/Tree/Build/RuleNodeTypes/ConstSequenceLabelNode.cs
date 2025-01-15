using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E68 RID: 7784
	public struct ConstSequenceLabelNode : IProgramNodeBuilder, IEquatable<ConstSequenceLabelNode>
	{
		// Token: 0x17002B9D RID: 11165
		// (get) Token: 0x0601066A RID: 67178 RVA: 0x00389AA2 File Offset: 0x00387CA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601066B RID: 67179 RVA: 0x00389AAA File Offset: 0x00387CAA
		private ConstSequenceLabelNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601066C RID: 67180 RVA: 0x00389AB3 File Offset: 0x00387CB3
		public static ConstSequenceLabelNode CreateUnsafe(ProgramNode node)
		{
			return new ConstSequenceLabelNode(node);
		}

		// Token: 0x0601066D RID: 67181 RVA: 0x00389ABC File Offset: 0x00387CBC
		public static ConstSequenceLabelNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstSequenceLabelNode)
			{
				return null;
			}
			return new ConstSequenceLabelNode?(ConstSequenceLabelNode.CreateUnsafe(node));
		}

		// Token: 0x0601066E RID: 67182 RVA: 0x00389AF4 File Offset: 0x00387CF4
		public ConstSequenceLabelNode(GrammarBuilders g, label value0, attributes value1, construction value2, sequenceChildren value3)
		{
			this._node = g.Rule.ConstSequenceLabelNode.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node });
		}

		// Token: 0x0601066F RID: 67183 RVA: 0x00389B45 File Offset: 0x00387D45
		public static implicit operator construction(ConstSequenceLabelNode arg)
		{
			return construction.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B9E RID: 11166
		// (get) Token: 0x06010670 RID: 67184 RVA: 0x00389B53 File Offset: 0x00387D53
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B9F RID: 11167
		// (get) Token: 0x06010671 RID: 67185 RVA: 0x00389B67 File Offset: 0x00387D67
		public attributes attributes
		{
			get
			{
				return attributes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002BA0 RID: 11168
		// (get) Token: 0x06010672 RID: 67186 RVA: 0x00389B7B File Offset: 0x00387D7B
		public construction construction
		{
			get
			{
				return construction.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17002BA1 RID: 11169
		// (get) Token: 0x06010673 RID: 67187 RVA: 0x00389B8F File Offset: 0x00387D8F
		public sequenceChildren sequenceChildren
		{
			get
			{
				return sequenceChildren.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x06010674 RID: 67188 RVA: 0x00389BA3 File Offset: 0x00387DA3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010675 RID: 67189 RVA: 0x00389BB8 File Offset: 0x00387DB8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010676 RID: 67190 RVA: 0x00389BE2 File Offset: 0x00387DE2
		public bool Equals(ConstSequenceLabelNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A7 RID: 25255
		private ProgramNode _node;
	}
}
