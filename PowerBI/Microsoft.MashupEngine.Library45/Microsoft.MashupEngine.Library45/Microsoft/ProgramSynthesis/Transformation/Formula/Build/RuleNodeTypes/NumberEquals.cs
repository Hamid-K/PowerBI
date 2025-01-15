using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200155D RID: 5469
	public struct NumberEquals : IProgramNodeBuilder, IEquatable<NumberEquals>
	{
		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x0600B2A9 RID: 45737 RVA: 0x002723F6 File Offset: 0x002705F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B2AA RID: 45738 RVA: 0x002723FE File Offset: 0x002705FE
		private NumberEquals(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B2AB RID: 45739 RVA: 0x00272407 File Offset: 0x00270607
		public static NumberEquals CreateUnsafe(ProgramNode node)
		{
			return new NumberEquals(node);
		}

		// Token: 0x0600B2AC RID: 45740 RVA: 0x00272410 File Offset: 0x00270610
		public static NumberEquals? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NumberEquals)
			{
				return null;
			}
			return new NumberEquals?(NumberEquals.CreateUnsafe(node));
		}

		// Token: 0x0600B2AD RID: 45741 RVA: 0x00272445 File Offset: 0x00270645
		public NumberEquals(GrammarBuilders g, row value0, columnName value1, numberEqualsValue value2)
		{
			this._node = g.Rule.NumberEquals.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600B2AE RID: 45742 RVA: 0x00272472 File Offset: 0x00270672
		public static implicit operator condition(NumberEquals arg)
		{
			return condition.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x0600B2AF RID: 45743 RVA: 0x00272480 File Offset: 0x00270680
		public row row
		{
			get
			{
				return row.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x0600B2B0 RID: 45744 RVA: 0x00272494 File Offset: 0x00270694
		public columnName columnName
		{
			get
			{
				return columnName.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x0600B2B1 RID: 45745 RVA: 0x002724A8 File Offset: 0x002706A8
		public numberEqualsValue numberEqualsValue
		{
			get
			{
				return numberEqualsValue.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600B2B2 RID: 45746 RVA: 0x002724BC File Offset: 0x002706BC
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B2B3 RID: 45747 RVA: 0x002724D0 File Offset: 0x002706D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B2B4 RID: 45748 RVA: 0x002724FA File Offset: 0x002706FA
		public bool Equals(NumberEquals other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400460B RID: 17931
		private ProgramNode _node;
	}
}
