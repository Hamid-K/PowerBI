using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C06 RID: 7174
	public struct FormatNumericRange : IProgramNodeBuilder, IEquatable<FormatNumericRange>
	{
		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x0600F146 RID: 61766 RVA: 0x0033F552 File Offset: 0x0033D752
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F147 RID: 61767 RVA: 0x0033F55A File Offset: 0x0033D75A
		private FormatNumericRange(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F148 RID: 61768 RVA: 0x0033F563 File Offset: 0x0033D763
		public static FormatNumericRange CreateUnsafe(ProgramNode node)
		{
			return new FormatNumericRange(node);
		}

		// Token: 0x0600F149 RID: 61769 RVA: 0x0033F56C File Offset: 0x0033D76C
		public static FormatNumericRange? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatNumericRange)
			{
				return null;
			}
			return new FormatNumericRange?(FormatNumericRange.CreateUnsafe(node));
		}

		// Token: 0x0600F14A RID: 61770 RVA: 0x0033F5A4 File Offset: 0x0033D7A4
		public FormatNumericRange(GrammarBuilders g, inputNumber value0, numberFormat value1, s value2, roundingSpec value3, roundingSpec value4)
		{
			this._node = g.Rule.FormatNumericRange.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x0600F14B RID: 61771 RVA: 0x0033F5FF File Offset: 0x0033D7FF
		public static implicit operator conv(FormatNumericRange arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x0600F14C RID: 61772 RVA: 0x0033F60D File Offset: 0x0033D80D
		public inputNumber inputNumber
		{
			get
			{
				return inputNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x0600F14D RID: 61773 RVA: 0x0033F621 File Offset: 0x0033D821
		public numberFormat numberFormat
		{
			get
			{
				return numberFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x0600F14E RID: 61774 RVA: 0x0033F635 File Offset: 0x0033D835
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x0600F14F RID: 61775 RVA: 0x0033F649 File Offset: 0x0033D849
		public roundingSpec roundingSpec1
		{
			get
			{
				return roundingSpec.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x0600F150 RID: 61776 RVA: 0x0033F65D File Offset: 0x0033D85D
		public roundingSpec roundingSpec2
		{
			get
			{
				return roundingSpec.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x0600F151 RID: 61777 RVA: 0x0033F671 File Offset: 0x0033D871
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F152 RID: 61778 RVA: 0x0033F684 File Offset: 0x0033D884
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F153 RID: 61779 RVA: 0x0033F6AE File Offset: 0x0033D8AE
		public bool Equals(FormatNumericRange other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF5 RID: 23285
		private ProgramNode _node;
	}
}
