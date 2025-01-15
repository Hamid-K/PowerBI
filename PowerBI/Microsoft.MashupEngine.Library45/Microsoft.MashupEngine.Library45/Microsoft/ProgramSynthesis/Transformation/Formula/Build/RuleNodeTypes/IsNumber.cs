using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001561 RID: 5473
	public struct IsNumber : IProgramNodeBuilder, IEquatable<IsNumber>
	{
		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x0600B2D8 RID: 45784 RVA: 0x0027283A File Offset: 0x00270A3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2D9 RID: 45785 RVA: 0x00272842 File Offset: 0x00270A42
		private IsNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2DA RID: 45786 RVA: 0x0027284B File Offset: 0x00270A4B
		public static IsNumber CreateUnsafe(ProgramNode node)
		{
			return new IsNumber(node);
		}

		// Token: 0x0600B2DB RID: 45787 RVA: 0x00272854 File Offset: 0x00270A54
		public static IsNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsNumber)
			{
				return null;
			}
			return new IsNumber?(IsNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B2DC RID: 45788 RVA: 0x00272889 File Offset: 0x00270A89
		public IsNumber(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.IsNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B2DD RID: 45789 RVA: 0x002728AF File Offset: 0x00270AAF
		public static implicit operator condition(IsNumber arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x0600B2DE RID: 45790 RVA: 0x002728BD File Offset: 0x00270ABD
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x0600B2DF RID: 45791 RVA: 0x002728D1 File Offset: 0x00270AD1
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B2E0 RID: 45792 RVA: 0x002728E5 File Offset: 0x00270AE5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2E1 RID: 45793 RVA: 0x002728F8 File Offset: 0x00270AF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2E2 RID: 45794 RVA: 0x00272922 File Offset: 0x00270B22
		public bool Equals(IsNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460F RID: 17935
		private ProgramNode _node;
	}
}
