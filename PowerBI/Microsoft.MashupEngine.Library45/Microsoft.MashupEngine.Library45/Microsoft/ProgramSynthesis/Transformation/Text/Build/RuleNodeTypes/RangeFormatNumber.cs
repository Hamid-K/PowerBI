using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0A RID: 7178
	public struct RangeFormatNumber : IProgramNodeBuilder, IEquatable<RangeFormatNumber>
	{
		// Token: 0x17002848 RID: 10312
		// (get) Token: 0x0600F177 RID: 61815 RVA: 0x0033FA12 File Offset: 0x0033DC12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F178 RID: 61816 RVA: 0x0033FA1A File Offset: 0x0033DC1A
		private RangeFormatNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F179 RID: 61817 RVA: 0x0033FA23 File Offset: 0x0033DC23
		public static RangeFormatNumber CreateUnsafe(ProgramNode node)
		{
			return new RangeFormatNumber(node);
		}

		// Token: 0x0600F17A RID: 61818 RVA: 0x0033FA2C File Offset: 0x0033DC2C
		public static RangeFormatNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeFormatNumber)
			{
				return null;
			}
			return new RangeFormatNumber?(RangeFormatNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F17B RID: 61819 RVA: 0x0033FA61 File Offset: 0x0033DC61
		public RangeFormatNumber(GrammarBuilders g, rangeNumber value0, sharedNumberFormat value1)
		{
			this._node = g.Rule.RangeFormatNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F17C RID: 61820 RVA: 0x0033FA87 File Offset: 0x0033DC87
		public static implicit operator rangeSubstring(RangeFormatNumber arg)
		{
			return rangeSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002849 RID: 10313
		// (get) Token: 0x0600F17D RID: 61821 RVA: 0x0033FA95 File Offset: 0x0033DC95
		public rangeNumber rangeNumber
		{
			get
			{
				return rangeNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700284A RID: 10314
		// (get) Token: 0x0600F17E RID: 61822 RVA: 0x0033FAA9 File Offset: 0x0033DCA9
		public sharedNumberFormat sharedNumberFormat
		{
			get
			{
				return sharedNumberFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F17F RID: 61823 RVA: 0x0033FABD File Offset: 0x0033DCBD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F180 RID: 61824 RVA: 0x0033FAD0 File Offset: 0x0033DCD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F181 RID: 61825 RVA: 0x0033FAFA File Offset: 0x0033DCFA
		public bool Equals(RangeFormatNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF9 RID: 23289
		private ProgramNode _node;
	}
}
