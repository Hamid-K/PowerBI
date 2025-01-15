using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001560 RID: 5472
	public struct IsString : IProgramNodeBuilder, IEquatable<IsString>
	{
		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x0600B2CD RID: 45773 RVA: 0x0027273E File Offset: 0x0027093E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x00272746 File Offset: 0x00270946
		private IsString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2CF RID: 45775 RVA: 0x0027274F File Offset: 0x0027094F
		public static IsString CreateUnsafe(ProgramNode node)
		{
			return new IsString(node);
		}

		// Token: 0x0600B2D0 RID: 45776 RVA: 0x00272758 File Offset: 0x00270958
		public static IsString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsString)
			{
				return null;
			}
			return new IsString?(IsString.CreateUnsafe(node));
		}

		// Token: 0x0600B2D1 RID: 45777 RVA: 0x0027278D File Offset: 0x0027098D
		public IsString(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.IsString.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B2D2 RID: 45778 RVA: 0x002727B3 File Offset: 0x002709B3
		public static implicit operator condition(IsString arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x0600B2D3 RID: 45779 RVA: 0x002727C1 File Offset: 0x002709C1
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x0600B2D4 RID: 45780 RVA: 0x002727D5 File Offset: 0x002709D5
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x002727E9 File Offset: 0x002709E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2D6 RID: 45782 RVA: 0x002727FC File Offset: 0x002709FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x00272826 File Offset: 0x00270A26
		public bool Equals(IsString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460E RID: 17934
		private ProgramNode _node;
	}
}
