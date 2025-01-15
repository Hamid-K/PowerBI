using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0B RID: 7179
	public struct RangeRoundNumber : IProgramNodeBuilder, IEquatable<RangeRoundNumber>
	{
		// Token: 0x1700284B RID: 10315
		// (get) Token: 0x0600F182 RID: 61826 RVA: 0x0033FB0E File Offset: 0x0033DD0E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F183 RID: 61827 RVA: 0x0033FB16 File Offset: 0x0033DD16
		private RangeRoundNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F184 RID: 61828 RVA: 0x0033FB1F File Offset: 0x0033DD1F
		public static RangeRoundNumber CreateUnsafe(ProgramNode node)
		{
			return new RangeRoundNumber(node);
		}

		// Token: 0x0600F185 RID: 61829 RVA: 0x0033FB28 File Offset: 0x0033DD28
		public static RangeRoundNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeRoundNumber)
			{
				return null;
			}
			return new RangeRoundNumber?(RangeRoundNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F186 RID: 61830 RVA: 0x0033FB5D File Offset: 0x0033DD5D
		public RangeRoundNumber(GrammarBuilders g, sharedParsedNumber value0, roundingSpec value1)
		{
			this._node = g.Rule.RangeRoundNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F187 RID: 61831 RVA: 0x0033FB83 File Offset: 0x0033DD83
		public static implicit operator rangeNumber(RangeRoundNumber arg)
		{
			return rangeNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x0600F188 RID: 61832 RVA: 0x0033FB91 File Offset: 0x0033DD91
		public sharedParsedNumber sharedParsedNumber
		{
			get
			{
				return sharedParsedNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x0600F189 RID: 61833 RVA: 0x0033FBA5 File Offset: 0x0033DDA5
		public roundingSpec roundingSpec
		{
			get
			{
				return roundingSpec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F18A RID: 61834 RVA: 0x0033FBB9 File Offset: 0x0033DDB9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F18B RID: 61835 RVA: 0x0033FBCC File Offset: 0x0033DDCC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F18C RID: 61836 RVA: 0x0033FBF6 File Offset: 0x0033DDF6
		public bool Equals(RangeRoundNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFA RID: 23290
		private ProgramNode _node;
	}
}
