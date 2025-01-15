using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E66 RID: 7782
	public struct LeafConstLabelNode : IProgramNodeBuilder, IEquatable<LeafConstLabelNode>
	{
		// Token: 0x17002B96 RID: 11158
		// (get) Token: 0x06010653 RID: 67155 RVA: 0x0038988E File Offset: 0x00387A8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010654 RID: 67156 RVA: 0x00389896 File Offset: 0x00387A96
		private LeafConstLabelNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010655 RID: 67157 RVA: 0x0038989F File Offset: 0x00387A9F
		public static LeafConstLabelNode CreateUnsafe(ProgramNode node)
		{
			return new LeafConstLabelNode(node);
		}

		// Token: 0x06010656 RID: 67158 RVA: 0x003898A8 File Offset: 0x00387AA8
		public static LeafConstLabelNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LeafConstLabelNode)
			{
				return null;
			}
			return new LeafConstLabelNode?(LeafConstLabelNode.CreateUnsafe(node));
		}

		// Token: 0x06010657 RID: 67159 RVA: 0x003898DD File Offset: 0x00387ADD
		public LeafConstLabelNode(GrammarBuilders g, label value0, attributes value1)
		{
			this._node = g.Rule.LeafConstLabelNode.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06010658 RID: 67160 RVA: 0x00389903 File Offset: 0x00387B03
		public static implicit operator construction(LeafConstLabelNode arg)
		{
			return construction.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B97 RID: 11159
		// (get) Token: 0x06010659 RID: 67161 RVA: 0x00389911 File Offset: 0x00387B11
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B98 RID: 11160
		// (get) Token: 0x0601065A RID: 67162 RVA: 0x00389925 File Offset: 0x00387B25
		public attributes attributes
		{
			get
			{
				return attributes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0601065B RID: 67163 RVA: 0x00389939 File Offset: 0x00387B39
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601065C RID: 67164 RVA: 0x0038994C File Offset: 0x00387B4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601065D RID: 67165 RVA: 0x00389976 File Offset: 0x00387B76
		public bool Equals(LeafConstLabelNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A5 RID: 25253
		private ProgramNode _node;
	}
}
