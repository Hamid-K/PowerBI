using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4F RID: 7247
	public struct number : IProgramNodeBuilder, IEquatable<number>
	{
		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x0600F518 RID: 62744 RVA: 0x00346F82 File Offset: 0x00345182
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F519 RID: 62745 RVA: 0x00346F8A File Offset: 0x0034518A
		private number(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F51A RID: 62746 RVA: 0x00346F93 File Offset: 0x00345193
		public static number CreateUnsafe(ProgramNode node)
		{
			return new number(node);
		}

		// Token: 0x0600F51B RID: 62747 RVA: 0x00346F9C File Offset: 0x0034519C
		public static number? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.number)
			{
				return null;
			}
			return new number?(number.CreateUnsafe(node));
		}

		// Token: 0x0600F51C RID: 62748 RVA: 0x00346FD6 File Offset: 0x003451D6
		public static number CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new number(new Hole(g.Symbol.number, holeId));
		}

		// Token: 0x0600F51D RID: 62749 RVA: 0x00346FEE File Offset: 0x003451EE
		public bool Is_number_inputNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.number_inputNumber;
		}

		// Token: 0x0600F51E RID: 62750 RVA: 0x00347008 File Offset: 0x00345208
		public bool Is_number_inputNumber(GrammarBuilders g, out number_inputNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_inputNumber)
			{
				value = number_inputNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(number_inputNumber);
			return false;
		}

		// Token: 0x0600F51F RID: 62751 RVA: 0x00347040 File Offset: 0x00345240
		public number_inputNumber? As_number_inputNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.number_inputNumber)
			{
				return null;
			}
			return new number_inputNumber?(number_inputNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F520 RID: 62752 RVA: 0x00347080 File Offset: 0x00345280
		public number_inputNumber Cast_number_inputNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.number_inputNumber)
			{
				return number_inputNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_number_inputNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F521 RID: 62753 RVA: 0x003470D5 File Offset: 0x003452D5
		public bool Is_RoundNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RoundNumber;
		}

		// Token: 0x0600F522 RID: 62754 RVA: 0x003470EF File Offset: 0x003452EF
		public bool Is_RoundNumber(GrammarBuilders g, out RoundNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.RoundNumber)
			{
				value = RoundNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RoundNumber);
			return false;
		}

		// Token: 0x0600F523 RID: 62755 RVA: 0x00347124 File Offset: 0x00345324
		public RoundNumber? As_RoundNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RoundNumber)
			{
				return null;
			}
			return new RoundNumber?(RoundNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F524 RID: 62756 RVA: 0x00347164 File Offset: 0x00345364
		public RoundNumber Cast_RoundNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RoundNumber)
			{
				return RoundNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RoundNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F525 RID: 62757 RVA: 0x003471BC File Offset: 0x003453BC
		public T Switch<T>(GrammarBuilders g, Func<number_inputNumber, T> func0, Func<RoundNumber, T> func1)
		{
			number_inputNumber number_inputNumber;
			if (this.Is_number_inputNumber(g, out number_inputNumber))
			{
				return func0(number_inputNumber);
			}
			RoundNumber roundNumber;
			if (this.Is_RoundNumber(g, out roundNumber))
			{
				return func1(roundNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number");
		}

		// Token: 0x0600F526 RID: 62758 RVA: 0x00347214 File Offset: 0x00345414
		public void Switch(GrammarBuilders g, Action<number_inputNumber> func0, Action<RoundNumber> func1)
		{
			number_inputNumber number_inputNumber;
			if (this.Is_number_inputNumber(g, out number_inputNumber))
			{
				func0(number_inputNumber);
				return;
			}
			RoundNumber roundNumber;
			if (this.Is_RoundNumber(g, out roundNumber))
			{
				func1(roundNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol number");
		}

		// Token: 0x0600F527 RID: 62759 RVA: 0x0034726B File Offset: 0x0034546B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F528 RID: 62760 RVA: 0x00347280 File Offset: 0x00345480
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F529 RID: 62761 RVA: 0x003472AA File Offset: 0x003454AA
		public bool Equals(number other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3E RID: 23358
		private ProgramNode _node;
	}
}
