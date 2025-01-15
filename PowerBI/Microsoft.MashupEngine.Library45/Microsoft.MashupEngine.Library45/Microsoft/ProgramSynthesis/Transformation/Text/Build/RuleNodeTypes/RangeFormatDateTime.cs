using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0E RID: 7182
	public struct RangeFormatDateTime : IProgramNodeBuilder, IEquatable<RangeFormatDateTime>
	{
		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x0600F1A2 RID: 61858 RVA: 0x0033FDEA File Offset: 0x0033DFEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1A3 RID: 61859 RVA: 0x0033FDF2 File Offset: 0x0033DFF2
		private RangeFormatDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1A4 RID: 61860 RVA: 0x0033FDFB File Offset: 0x0033DFFB
		public static RangeFormatDateTime CreateUnsafe(ProgramNode node)
		{
			return new RangeFormatDateTime(node);
		}

		// Token: 0x0600F1A5 RID: 61861 RVA: 0x0033FE04 File Offset: 0x0033E004
		public static RangeFormatDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeFormatDateTime)
			{
				return null;
			}
			return new RangeFormatDateTime?(RangeFormatDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F1A6 RID: 61862 RVA: 0x0033FE39 File Offset: 0x0033E039
		public RangeFormatDateTime(GrammarBuilders g, rangeDateTime value0, sharedDtFormat value1)
		{
			this._node = g.Rule.RangeFormatDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1A7 RID: 61863 RVA: 0x0033FE5F File Offset: 0x0033E05F
		public static implicit operator dtRangeSubstring(RangeFormatDateTime arg)
		{
			return dtRangeSubstring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x0600F1A8 RID: 61864 RVA: 0x0033FE6D File Offset: 0x0033E06D
		public rangeDateTime rangeDateTime
		{
			get
			{
				return rangeDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x0600F1A9 RID: 61865 RVA: 0x0033FE81 File Offset: 0x0033E081
		public sharedDtFormat sharedDtFormat
		{
			get
			{
				return sharedDtFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1AA RID: 61866 RVA: 0x0033FE95 File Offset: 0x0033E095
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1AB RID: 61867 RVA: 0x0033FEA8 File Offset: 0x0033E0A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1AC RID: 61868 RVA: 0x0033FED2 File Offset: 0x0033E0D2
		public bool Equals(RangeFormatDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFD RID: 23293
		private ProgramNode _node;
	}
}
