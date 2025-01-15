using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001566 RID: 5478
	public struct FormatNumber : IProgramNodeBuilder, IEquatable<FormatNumber>
	{
		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x0600B310 RID: 45840 RVA: 0x00272D6A File Offset: 0x00270F6A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x00272D72 File Offset: 0x00270F72
		private FormatNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B312 RID: 45842 RVA: 0x00272D7B File Offset: 0x00270F7B
		public static FormatNumber CreateUnsafe(ProgramNode node)
		{
			return new FormatNumber(node);
		}

		// Token: 0x0600B313 RID: 45843 RVA: 0x00272D84 File Offset: 0x00270F84
		public static FormatNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatNumber)
			{
				return null;
			}
			return new FormatNumber?(FormatNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B314 RID: 45844 RVA: 0x00272DB9 File Offset: 0x00270FB9
		public FormatNumber(GrammarBuilders g, number value0, numberFormatDesc value1)
		{
			this._node = g.Rule.FormatNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B315 RID: 45845 RVA: 0x00272DDF File Offset: 0x00270FDF
		public static implicit operator formatNumber(FormatNumber arg)
		{
			return formatNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x0600B316 RID: 45846 RVA: 0x00272DED File Offset: 0x00270FED
		public number number
		{
			get
			{
				return number.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x0600B317 RID: 45847 RVA: 0x00272E01 File Offset: 0x00271001
		public numberFormatDesc numberFormatDesc
		{
			get
			{
				return numberFormatDesc.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B318 RID: 45848 RVA: 0x00272E15 File Offset: 0x00271015
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B319 RID: 45849 RVA: 0x00272E28 File Offset: 0x00271028
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B31A RID: 45850 RVA: 0x00272E52 File Offset: 0x00271052
		public bool Equals(FormatNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004614 RID: 17940
		private ProgramNode _node;
	}
}
