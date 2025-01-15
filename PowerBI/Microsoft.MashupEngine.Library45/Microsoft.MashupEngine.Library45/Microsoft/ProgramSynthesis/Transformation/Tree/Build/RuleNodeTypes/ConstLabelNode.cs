using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E67 RID: 7783
	public struct ConstLabelNode : IProgramNodeBuilder, IEquatable<ConstLabelNode>
	{
		// Token: 0x17002B99 RID: 11161
		// (get) Token: 0x0601065E RID: 67166 RVA: 0x0038998A File Offset: 0x00387B8A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601065F RID: 67167 RVA: 0x00389992 File Offset: 0x00387B92
		private ConstLabelNode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010660 RID: 67168 RVA: 0x0038999B File Offset: 0x00387B9B
		public static ConstLabelNode CreateUnsafe(ProgramNode node)
		{
			return new ConstLabelNode(node);
		}

		// Token: 0x06010661 RID: 67169 RVA: 0x003899A4 File Offset: 0x00387BA4
		public static ConstLabelNode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstLabelNode)
			{
				return null;
			}
			return new ConstLabelNode?(ConstLabelNode.CreateUnsafe(node));
		}

		// Token: 0x06010662 RID: 67170 RVA: 0x003899D9 File Offset: 0x00387BD9
		public ConstLabelNode(GrammarBuilders g, label value0, attributes value1, children value2)
		{
			this._node = g.Rule.ConstLabelNode.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06010663 RID: 67171 RVA: 0x00389A06 File Offset: 0x00387C06
		public static implicit operator construction(ConstLabelNode arg)
		{
			return construction.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B9A RID: 11162
		// (get) Token: 0x06010664 RID: 67172 RVA: 0x00389A14 File Offset: 0x00387C14
		public label label
		{
			get
			{
				return label.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B9B RID: 11163
		// (get) Token: 0x06010665 RID: 67173 RVA: 0x00389A28 File Offset: 0x00387C28
		public attributes attributes
		{
			get
			{
				return attributes.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002B9C RID: 11164
		// (get) Token: 0x06010666 RID: 67174 RVA: 0x00389A3C File Offset: 0x00387C3C
		public children children
		{
			get
			{
				return children.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06010667 RID: 67175 RVA: 0x00389A50 File Offset: 0x00387C50
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010668 RID: 67176 RVA: 0x00389A64 File Offset: 0x00387C64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010669 RID: 67177 RVA: 0x00389A8E File Offset: 0x00387C8E
		public bool Equals(ConstLabelNode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A6 RID: 25254
		private ProgramNode _node;
	}
}
