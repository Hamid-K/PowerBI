using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155B RID: 5467
	public struct IsBlank : IProgramNodeBuilder, IEquatable<IsBlank>
	{
		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x0600B293 RID: 45715 RVA: 0x002721FE File Offset: 0x002703FE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B294 RID: 45716 RVA: 0x00272206 File Offset: 0x00270406
		private IsBlank(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B295 RID: 45717 RVA: 0x0027220F File Offset: 0x0027040F
		public static IsBlank CreateUnsafe(ProgramNode node)
		{
			return new IsBlank(node);
		}

		// Token: 0x0600B296 RID: 45718 RVA: 0x00272218 File Offset: 0x00270418
		public static IsBlank? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.IsBlank)
			{
				return null;
			}
			return new IsBlank?(IsBlank.CreateUnsafe(node));
		}

		// Token: 0x0600B297 RID: 45719 RVA: 0x0027224D File Offset: 0x0027044D
		public IsBlank(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.IsBlank.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B298 RID: 45720 RVA: 0x00272273 File Offset: 0x00270473
		public static implicit operator condition(IsBlank arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x0600B299 RID: 45721 RVA: 0x00272281 File Offset: 0x00270481
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x0600B29A RID: 45722 RVA: 0x00272295 File Offset: 0x00270495
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B29B RID: 45723 RVA: 0x002722A9 File Offset: 0x002704A9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B29C RID: 45724 RVA: 0x002722BC File Offset: 0x002704BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B29D RID: 45725 RVA: 0x002722E6 File Offset: 0x002704E6
		public bool Equals(IsBlank other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004609 RID: 17929
		private ProgramNode _node;
	}
}
