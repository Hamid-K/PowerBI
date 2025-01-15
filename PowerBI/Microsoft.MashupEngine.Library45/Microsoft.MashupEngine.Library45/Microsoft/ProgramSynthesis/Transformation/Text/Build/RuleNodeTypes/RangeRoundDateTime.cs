using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0F RID: 7183
	public struct RangeRoundDateTime : IProgramNodeBuilder, IEquatable<RangeRoundDateTime>
	{
		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x0600F1AD RID: 61869 RVA: 0x0033FEE6 File Offset: 0x0033E0E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F1AE RID: 61870 RVA: 0x0033FEEE File Offset: 0x0033E0EE
		private RangeRoundDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F1AF RID: 61871 RVA: 0x0033FEF7 File Offset: 0x0033E0F7
		public static RangeRoundDateTime CreateUnsafe(ProgramNode node)
		{
			return new RangeRoundDateTime(node);
		}

		// Token: 0x0600F1B0 RID: 61872 RVA: 0x0033FF00 File Offset: 0x0033E100
		public static RangeRoundDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeRoundDateTime)
			{
				return null;
			}
			return new RangeRoundDateTime?(RangeRoundDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F1B1 RID: 61873 RVA: 0x0033FF35 File Offset: 0x0033E135
		public RangeRoundDateTime(GrammarBuilders g, sharedParsedDt value0, dtRoundingSpec value1)
		{
			this._node = g.Rule.RangeRoundDateTime.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F1B2 RID: 61874 RVA: 0x0033FF5B File Offset: 0x0033E15B
		public static implicit operator rangeDateTime(RangeRoundDateTime arg)
		{
			return rangeDateTime.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x0600F1B3 RID: 61875 RVA: 0x0033FF69 File Offset: 0x0033E169
		public sharedParsedDt sharedParsedDt
		{
			get
			{
				return sharedParsedDt.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x0600F1B4 RID: 61876 RVA: 0x0033FF7D File Offset: 0x0033E17D
		public dtRoundingSpec dtRoundingSpec
		{
			get
			{
				return dtRoundingSpec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F1B5 RID: 61877 RVA: 0x0033FF91 File Offset: 0x0033E191
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F1B6 RID: 61878 RVA: 0x0033FFA4 File Offset: 0x0033E1A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F1B7 RID: 61879 RVA: 0x0033FFCE File Offset: 0x0033E1CE
		public bool Equals(RangeRoundDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFE RID: 23294
		private ProgramNode _node;
	}
}
