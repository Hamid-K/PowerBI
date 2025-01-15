using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001559 RID: 5465
	public struct EndsWithDigit : IProgramNodeBuilder, IEquatable<EndsWithDigit>
	{
		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x0600B27C RID: 45692 RVA: 0x00271FEA File Offset: 0x002701EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B27D RID: 45693 RVA: 0x00271FF2 File Offset: 0x002701F2
		private EndsWithDigit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B27E RID: 45694 RVA: 0x00271FFB File Offset: 0x002701FB
		public static EndsWithDigit CreateUnsafe(ProgramNode node)
		{
			return new EndsWithDigit(node);
		}

		// Token: 0x0600B27F RID: 45695 RVA: 0x00272004 File Offset: 0x00270204
		public static EndsWithDigit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndsWithDigit)
			{
				return null;
			}
			return new EndsWithDigit?(EndsWithDigit.CreateUnsafe(node));
		}

		// Token: 0x0600B280 RID: 45696 RVA: 0x00272039 File Offset: 0x00270239
		public EndsWithDigit(GrammarBuilders g, row value0, columnName value1)
		{
			this._node = g.Rule.EndsWithDigit.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x0027205F File Offset: 0x0027025F
		public static implicit operator condition(EndsWithDigit arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x0600B282 RID: 45698 RVA: 0x0027206D File Offset: 0x0027026D
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x0600B283 RID: 45699 RVA: 0x00272081 File Offset: 0x00270281
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B284 RID: 45700 RVA: 0x00272095 File Offset: 0x00270295
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B285 RID: 45701 RVA: 0x002720A8 File Offset: 0x002702A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B286 RID: 45702 RVA: 0x002720D2 File Offset: 0x002702D2
		public bool Equals(EndsWithDigit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004607 RID: 17927
		private ProgramNode _node;
	}
}
