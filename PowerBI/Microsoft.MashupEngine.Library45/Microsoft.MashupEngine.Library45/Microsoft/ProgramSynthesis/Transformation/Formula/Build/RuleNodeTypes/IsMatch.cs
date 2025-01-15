using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001562 RID: 5474
	public struct IsMatch : IProgramNodeBuilder, IEquatable<IsMatch>
	{
		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x0600B2E3 RID: 45795 RVA: 0x00272936 File Offset: 0x00270B36
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2E4 RID: 45796 RVA: 0x0027293E File Offset: 0x00270B3E
		private IsMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2E5 RID: 45797 RVA: 0x00272947 File Offset: 0x00270B47
		public static IsMatch CreateUnsafe(ProgramNode node)
		{
			return new IsMatch(node);
		}

		// Token: 0x0600B2E6 RID: 45798 RVA: 0x00272950 File Offset: 0x00270B50
		public static IsMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsMatch)
			{
				return null;
			}
			return new IsMatch?(IsMatch.CreateUnsafe(node));
		}

		// Token: 0x0600B2E7 RID: 45799 RVA: 0x00272985 File Offset: 0x00270B85
		public IsMatch(GrammarBuilders g, row value0, columnName value1, isMatchRegex value2)
		{
			this._node = g.Rule.IsMatch.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B2E8 RID: 45800 RVA: 0x002729B2 File Offset: 0x00270BB2
		public static implicit operator condition(IsMatch arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x0600B2E9 RID: 45801 RVA: 0x002729C0 File Offset: 0x00270BC0
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x0600B2EA RID: 45802 RVA: 0x002729D4 File Offset: 0x00270BD4
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x0600B2EB RID: 45803 RVA: 0x002729E8 File Offset: 0x00270BE8
		public isMatchRegex isMatchRegex
		{
			get
			{
				return isMatchRegex.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B2EC RID: 45804 RVA: 0x002729FC File Offset: 0x00270BFC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2ED RID: 45805 RVA: 0x00272A10 File Offset: 0x00270C10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2EE RID: 45806 RVA: 0x00272A3A File Offset: 0x00270C3A
		public bool Equals(IsMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004610 RID: 17936
		private ProgramNode _node;
	}
}
