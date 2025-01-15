using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E64 RID: 7780
	public struct HasNChildren : IProgramNodeBuilder, IEquatable<HasNChildren>
	{
		// Token: 0x17002B90 RID: 11152
		// (get) Token: 0x0601063D RID: 67133 RVA: 0x00389692 File Offset: 0x00387892
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601063E RID: 67134 RVA: 0x0038969A File Offset: 0x0038789A
		private HasNChildren(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601063F RID: 67135 RVA: 0x003896A3 File Offset: 0x003878A3
		public static HasNChildren CreateUnsafe(ProgramNode node)
		{
			return new HasNChildren(node);
		}

		// Token: 0x06010640 RID: 67136 RVA: 0x003896AC File Offset: 0x003878AC
		public static HasNChildren? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.HasNChildren)
			{
				return null;
			}
			return new HasNChildren?(HasNChildren.CreateUnsafe(node));
		}

		// Token: 0x06010641 RID: 67137 RVA: 0x003896E1 File Offset: 0x003878E1
		public HasNChildren(GrammarBuilders g, x value0, path value1, k value2)
		{
			this._node = g.Rule.HasNChildren.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06010642 RID: 67138 RVA: 0x0038970E File Offset: 0x0038790E
		public static implicit operator pred(HasNChildren arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B91 RID: 11153
		// (get) Token: 0x06010643 RID: 67139 RVA: 0x0038971C File Offset: 0x0038791C
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002B92 RID: 11154
		// (get) Token: 0x06010644 RID: 67140 RVA: 0x00389730 File Offset: 0x00387930
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002B93 RID: 11155
		// (get) Token: 0x06010645 RID: 67141 RVA: 0x00389744 File Offset: 0x00387944
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06010646 RID: 67142 RVA: 0x00389758 File Offset: 0x00387958
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010647 RID: 67143 RVA: 0x0038976C File Offset: 0x0038796C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010648 RID: 67144 RVA: 0x00389796 File Offset: 0x00387996
		public bool Equals(HasNChildren other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A3 RID: 25251
		private ProgramNode _node;
	}
}
