using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A8 RID: 5544
	public struct condition : IProgramNodeBuilder, IEquatable<condition>
	{
		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x0600B66C RID: 46700 RVA: 0x002798C6 File Offset: 0x00277AC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B66D RID: 46701 RVA: 0x002798CE File Offset: 0x00277ACE
		private condition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B66E RID: 46702 RVA: 0x002798D7 File Offset: 0x00277AD7
		public static condition CreateUnsafe(ProgramNode node)
		{
			return new condition(node);
		}

		// Token: 0x0600B66F RID: 46703 RVA: 0x002798E0 File Offset: 0x00277AE0
		public static condition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.condition)
			{
				return null;
			}
			return new condition?(condition.CreateUnsafe(node));
		}

		// Token: 0x0600B670 RID: 46704 RVA: 0x0027991A File Offset: 0x00277B1A
		public static condition CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new condition(new Hole(g.Symbol.condition, holeId));
		}

		// Token: 0x0600B671 RID: 46705 RVA: 0x00279932 File Offset: 0x00277B32
		public bool Is_StringEquals(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StringEquals;
		}

		// Token: 0x0600B672 RID: 46706 RVA: 0x0027994C File Offset: 0x00277B4C
		public bool Is_StringEquals(GrammarBuilders g, out StringEquals value)
		{
			if (this.Node.GrammarRule == g.Rule.StringEquals)
			{
				value = StringEquals.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StringEquals);
			return false;
		}

		// Token: 0x0600B673 RID: 46707 RVA: 0x00279984 File Offset: 0x00277B84
		public StringEquals? As_StringEquals(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StringEquals)
			{
				return null;
			}
			return new StringEquals?(StringEquals.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B674 RID: 46708 RVA: 0x002799C4 File Offset: 0x00277BC4
		public StringEquals Cast_StringEquals(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StringEquals)
			{
				return StringEquals.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StringEquals is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B675 RID: 46709 RVA: 0x00279A19 File Offset: 0x00277C19
		public bool Is_Contains(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Contains;
		}

		// Token: 0x0600B676 RID: 46710 RVA: 0x00279A33 File Offset: 0x00277C33
		public bool Is_Contains(GrammarBuilders g, out Contains value)
		{
			if (this.Node.GrammarRule == g.Rule.Contains)
			{
				value = Contains.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Contains);
			return false;
		}

		// Token: 0x0600B677 RID: 46711 RVA: 0x00279A68 File Offset: 0x00277C68
		public Contains? As_Contains(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Contains)
			{
				return null;
			}
			return new Contains?(Contains.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B678 RID: 46712 RVA: 0x00279AA8 File Offset: 0x00277CA8
		public Contains Cast_Contains(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Contains)
			{
				return Contains.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Contains is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B679 RID: 46713 RVA: 0x00279AFD File Offset: 0x00277CFD
		public bool Is_StartsWithDigit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWithDigit;
		}

		// Token: 0x0600B67A RID: 46714 RVA: 0x00279B17 File Offset: 0x00277D17
		public bool Is_StartsWithDigit(GrammarBuilders g, out StartsWithDigit value)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithDigit)
			{
				value = StartsWithDigit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StartsWithDigit);
			return false;
		}

		// Token: 0x0600B67B RID: 46715 RVA: 0x00279B4C File Offset: 0x00277D4C
		public StartsWithDigit? As_StartsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWithDigit)
			{
				return null;
			}
			return new StartsWithDigit?(StartsWithDigit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B67C RID: 46716 RVA: 0x00279B8C File Offset: 0x00277D8C
		public StartsWithDigit Cast_StartsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithDigit)
			{
				return StartsWithDigit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWithDigit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B67D RID: 46717 RVA: 0x00279BE1 File Offset: 0x00277DE1
		public bool Is_EndsWithDigit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndsWithDigit;
		}

		// Token: 0x0600B67E RID: 46718 RVA: 0x00279BFB File Offset: 0x00277DFB
		public bool Is_EndsWithDigit(GrammarBuilders g, out EndsWithDigit value)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithDigit)
			{
				value = EndsWithDigit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EndsWithDigit);
			return false;
		}

		// Token: 0x0600B67F RID: 46719 RVA: 0x00279C30 File Offset: 0x00277E30
		public EndsWithDigit? As_EndsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndsWithDigit)
			{
				return null;
			}
			return new EndsWithDigit?(EndsWithDigit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B680 RID: 46720 RVA: 0x00279C70 File Offset: 0x00277E70
		public EndsWithDigit Cast_EndsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithDigit)
			{
				return EndsWithDigit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndsWithDigit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B681 RID: 46721 RVA: 0x00279CC5 File Offset: 0x00277EC5
		public bool Is_StartsWith(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWith;
		}

		// Token: 0x0600B682 RID: 46722 RVA: 0x00279CDF File Offset: 0x00277EDF
		public bool Is_StartsWith(GrammarBuilders g, out StartsWith value)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWith)
			{
				value = StartsWith.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StartsWith);
			return false;
		}

		// Token: 0x0600B683 RID: 46723 RVA: 0x00279D14 File Offset: 0x00277F14
		public StartsWith? As_StartsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWith)
			{
				return null;
			}
			return new StartsWith?(StartsWith.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B684 RID: 46724 RVA: 0x00279D54 File Offset: 0x00277F54
		public StartsWith Cast_StartsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWith)
			{
				return StartsWith.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWith is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B685 RID: 46725 RVA: 0x00279DA9 File Offset: 0x00277FA9
		public bool Is_IsBlank(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsBlank;
		}

		// Token: 0x0600B686 RID: 46726 RVA: 0x00279DC3 File Offset: 0x00277FC3
		public bool Is_IsBlank(GrammarBuilders g, out IsBlank value)
		{
			if (this.Node.GrammarRule == g.Rule.IsBlank)
			{
				value = IsBlank.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsBlank);
			return false;
		}

		// Token: 0x0600B687 RID: 46727 RVA: 0x00279DF8 File Offset: 0x00277FF8
		public IsBlank? As_IsBlank(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsBlank)
			{
				return null;
			}
			return new IsBlank?(IsBlank.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B688 RID: 46728 RVA: 0x00279E38 File Offset: 0x00278038
		public IsBlank Cast_IsBlank(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsBlank)
			{
				return IsBlank.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsBlank is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B689 RID: 46729 RVA: 0x00279E8D File Offset: 0x0027808D
		public bool Is_IsNotBlank(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNotBlank;
		}

		// Token: 0x0600B68A RID: 46730 RVA: 0x00279EA7 File Offset: 0x002780A7
		public bool Is_IsNotBlank(GrammarBuilders g, out IsNotBlank value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNotBlank)
			{
				value = IsNotBlank.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNotBlank);
			return false;
		}

		// Token: 0x0600B68B RID: 46731 RVA: 0x00279EDC File Offset: 0x002780DC
		public IsNotBlank? As_IsNotBlank(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNotBlank)
			{
				return null;
			}
			return new IsNotBlank?(IsNotBlank.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B68C RID: 46732 RVA: 0x00279F1C File Offset: 0x0027811C
		public IsNotBlank Cast_IsNotBlank(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNotBlank)
			{
				return IsNotBlank.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNotBlank is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B68D RID: 46733 RVA: 0x00279F71 File Offset: 0x00278171
		public bool Is_NumberEquals(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NumberEquals;
		}

		// Token: 0x0600B68E RID: 46734 RVA: 0x00279F8B File Offset: 0x0027818B
		public bool Is_NumberEquals(GrammarBuilders g, out NumberEquals value)
		{
			if (this.Node.GrammarRule == g.Rule.NumberEquals)
			{
				value = NumberEquals.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NumberEquals);
			return false;
		}

		// Token: 0x0600B68F RID: 46735 RVA: 0x00279FC0 File Offset: 0x002781C0
		public NumberEquals? As_NumberEquals(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NumberEquals)
			{
				return null;
			}
			return new NumberEquals?(NumberEquals.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B690 RID: 46736 RVA: 0x0027A000 File Offset: 0x00278200
		public NumberEquals Cast_NumberEquals(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NumberEquals)
			{
				return NumberEquals.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NumberEquals is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B691 RID: 46737 RVA: 0x0027A055 File Offset: 0x00278255
		public bool Is_NumberGreaterThan(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NumberGreaterThan;
		}

		// Token: 0x0600B692 RID: 46738 RVA: 0x0027A06F File Offset: 0x0027826F
		public bool Is_NumberGreaterThan(GrammarBuilders g, out NumberGreaterThan value)
		{
			if (this.Node.GrammarRule == g.Rule.NumberGreaterThan)
			{
				value = NumberGreaterThan.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NumberGreaterThan);
			return false;
		}

		// Token: 0x0600B693 RID: 46739 RVA: 0x0027A0A4 File Offset: 0x002782A4
		public NumberGreaterThan? As_NumberGreaterThan(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NumberGreaterThan)
			{
				return null;
			}
			return new NumberGreaterThan?(NumberGreaterThan.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B694 RID: 46740 RVA: 0x0027A0E4 File Offset: 0x002782E4
		public NumberGreaterThan Cast_NumberGreaterThan(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NumberGreaterThan)
			{
				return NumberGreaterThan.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NumberGreaterThan is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B695 RID: 46741 RVA: 0x0027A139 File Offset: 0x00278339
		public bool Is_NumberLessThan(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NumberLessThan;
		}

		// Token: 0x0600B696 RID: 46742 RVA: 0x0027A153 File Offset: 0x00278353
		public bool Is_NumberLessThan(GrammarBuilders g, out NumberLessThan value)
		{
			if (this.Node.GrammarRule == g.Rule.NumberLessThan)
			{
				value = NumberLessThan.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NumberLessThan);
			return false;
		}

		// Token: 0x0600B697 RID: 46743 RVA: 0x0027A188 File Offset: 0x00278388
		public NumberLessThan? As_NumberLessThan(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NumberLessThan)
			{
				return null;
			}
			return new NumberLessThan?(NumberLessThan.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B698 RID: 46744 RVA: 0x0027A1C8 File Offset: 0x002783C8
		public NumberLessThan Cast_NumberLessThan(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NumberLessThan)
			{
				return NumberLessThan.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NumberLessThan is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B699 RID: 46745 RVA: 0x0027A21D File Offset: 0x0027841D
		public bool Is_IsString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsString;
		}

		// Token: 0x0600B69A RID: 46746 RVA: 0x0027A237 File Offset: 0x00278437
		public bool Is_IsString(GrammarBuilders g, out IsString value)
		{
			if (this.Node.GrammarRule == g.Rule.IsString)
			{
				value = IsString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsString);
			return false;
		}

		// Token: 0x0600B69B RID: 46747 RVA: 0x0027A26C File Offset: 0x0027846C
		public IsString? As_IsString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsString)
			{
				return null;
			}
			return new IsString?(IsString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B69C RID: 46748 RVA: 0x0027A2AC File Offset: 0x002784AC
		public IsString Cast_IsString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsString)
			{
				return IsString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B69D RID: 46749 RVA: 0x0027A301 File Offset: 0x00278501
		public bool Is_IsNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNumber;
		}

		// Token: 0x0600B69E RID: 46750 RVA: 0x0027A31B File Offset: 0x0027851B
		public bool Is_IsNumber(GrammarBuilders g, out IsNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNumber)
			{
				value = IsNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNumber);
			return false;
		}

		// Token: 0x0600B69F RID: 46751 RVA: 0x0027A350 File Offset: 0x00278550
		public IsNumber? As_IsNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNumber)
			{
				return null;
			}
			return new IsNumber?(IsNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6A0 RID: 46752 RVA: 0x0027A390 File Offset: 0x00278590
		public IsNumber Cast_IsNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNumber)
			{
				return IsNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6A1 RID: 46753 RVA: 0x0027A3E5 File Offset: 0x002785E5
		public bool Is_IsMatch(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsMatch;
		}

		// Token: 0x0600B6A2 RID: 46754 RVA: 0x0027A3FF File Offset: 0x002785FF
		public bool Is_IsMatch(GrammarBuilders g, out IsMatch value)
		{
			if (this.Node.GrammarRule == g.Rule.IsMatch)
			{
				value = IsMatch.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsMatch);
			return false;
		}

		// Token: 0x0600B6A3 RID: 46755 RVA: 0x0027A434 File Offset: 0x00278634
		public IsMatch? As_IsMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsMatch)
			{
				return null;
			}
			return new IsMatch?(IsMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6A4 RID: 46756 RVA: 0x0027A474 File Offset: 0x00278674
		public IsMatch Cast_IsMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsMatch)
			{
				return IsMatch.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsMatch is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6A5 RID: 46757 RVA: 0x0027A4C9 File Offset: 0x002786C9
		public bool Is_ContainsMatch(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ContainsMatch;
		}

		// Token: 0x0600B6A6 RID: 46758 RVA: 0x0027A4E3 File Offset: 0x002786E3
		public bool Is_ContainsMatch(GrammarBuilders g, out ContainsMatch value)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsMatch)
			{
				value = ContainsMatch.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ContainsMatch);
			return false;
		}

		// Token: 0x0600B6A7 RID: 46759 RVA: 0x0027A518 File Offset: 0x00278718
		public ContainsMatch? As_ContainsMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ContainsMatch)
			{
				return null;
			}
			return new ContainsMatch?(ContainsMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6A8 RID: 46760 RVA: 0x0027A558 File Offset: 0x00278758
		public ContainsMatch Cast_ContainsMatch(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsMatch)
			{
				return ContainsMatch.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ContainsMatch is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6A9 RID: 46761 RVA: 0x0027A5AD File Offset: 0x002787AD
		public bool Is_condition_or(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.condition_or;
		}

		// Token: 0x0600B6AA RID: 46762 RVA: 0x0027A5C7 File Offset: 0x002787C7
		public bool Is_condition_or(GrammarBuilders g, out condition_or value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.condition_or)
			{
				value = condition_or.CreateUnsafe(this.Node);
				return true;
			}
			value = default(condition_or);
			return false;
		}

		// Token: 0x0600B6AB RID: 46763 RVA: 0x0027A5FC File Offset: 0x002787FC
		public condition_or? As_condition_or(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.condition_or)
			{
				return null;
			}
			return new condition_or?(condition_or.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6AC RID: 46764 RVA: 0x0027A63C File Offset: 0x0027883C
		public condition_or Cast_condition_or(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.condition_or)
			{
				return condition_or.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_condition_or is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B6AD RID: 46765 RVA: 0x0027A694 File Offset: 0x00278894
		public T Switch<T>(GrammarBuilders g, Func<StringEquals, T> func0, Func<Contains, T> func1, Func<StartsWithDigit, T> func2, Func<EndsWithDigit, T> func3, Func<StartsWith, T> func4, Func<IsBlank, T> func5, Func<IsNotBlank, T> func6, Func<NumberEquals, T> func7, Func<NumberGreaterThan, T> func8, Func<NumberLessThan, T> func9, Func<IsString, T> func10, Func<IsNumber, T> func11, Func<IsMatch, T> func12, Func<ContainsMatch, T> func13, Func<condition_or, T> func14)
		{
			StringEquals stringEquals;
			if (this.Is_StringEquals(g, out stringEquals))
			{
				return func0(stringEquals);
			}
			Contains contains;
			if (this.Is_Contains(g, out contains))
			{
				return func1(contains);
			}
			StartsWithDigit startsWithDigit;
			if (this.Is_StartsWithDigit(g, out startsWithDigit))
			{
				return func2(startsWithDigit);
			}
			EndsWithDigit endsWithDigit;
			if (this.Is_EndsWithDigit(g, out endsWithDigit))
			{
				return func3(endsWithDigit);
			}
			StartsWith startsWith;
			if (this.Is_StartsWith(g, out startsWith))
			{
				return func4(startsWith);
			}
			IsBlank isBlank;
			if (this.Is_IsBlank(g, out isBlank))
			{
				return func5(isBlank);
			}
			IsNotBlank isNotBlank;
			if (this.Is_IsNotBlank(g, out isNotBlank))
			{
				return func6(isNotBlank);
			}
			NumberEquals numberEquals;
			if (this.Is_NumberEquals(g, out numberEquals))
			{
				return func7(numberEquals);
			}
			NumberGreaterThan numberGreaterThan;
			if (this.Is_NumberGreaterThan(g, out numberGreaterThan))
			{
				return func8(numberGreaterThan);
			}
			NumberLessThan numberLessThan;
			if (this.Is_NumberLessThan(g, out numberLessThan))
			{
				return func9(numberLessThan);
			}
			IsString isString;
			if (this.Is_IsString(g, out isString))
			{
				return func10(isString);
			}
			IsNumber isNumber;
			if (this.Is_IsNumber(g, out isNumber))
			{
				return func11(isNumber);
			}
			IsMatch isMatch;
			if (this.Is_IsMatch(g, out isMatch))
			{
				return func12(isMatch);
			}
			ContainsMatch containsMatch;
			if (this.Is_ContainsMatch(g, out containsMatch))
			{
				return func13(containsMatch);
			}
			condition_or condition_or;
			if (this.Is_condition_or(g, out condition_or))
			{
				return func14(condition_or);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol condition");
		}

		// Token: 0x0600B6AE RID: 46766 RVA: 0x0027A7FC File Offset: 0x002789FC
		public void Switch(GrammarBuilders g, Action<StringEquals> func0, Action<Contains> func1, Action<StartsWithDigit> func2, Action<EndsWithDigit> func3, Action<StartsWith> func4, Action<IsBlank> func5, Action<IsNotBlank> func6, Action<NumberEquals> func7, Action<NumberGreaterThan> func8, Action<NumberLessThan> func9, Action<IsString> func10, Action<IsNumber> func11, Action<IsMatch> func12, Action<ContainsMatch> func13, Action<condition_or> func14)
		{
			StringEquals stringEquals;
			if (this.Is_StringEquals(g, out stringEquals))
			{
				func0(stringEquals);
				return;
			}
			Contains contains;
			if (this.Is_Contains(g, out contains))
			{
				func1(contains);
				return;
			}
			StartsWithDigit startsWithDigit;
			if (this.Is_StartsWithDigit(g, out startsWithDigit))
			{
				func2(startsWithDigit);
				return;
			}
			EndsWithDigit endsWithDigit;
			if (this.Is_EndsWithDigit(g, out endsWithDigit))
			{
				func3(endsWithDigit);
				return;
			}
			StartsWith startsWith;
			if (this.Is_StartsWith(g, out startsWith))
			{
				func4(startsWith);
				return;
			}
			IsBlank isBlank;
			if (this.Is_IsBlank(g, out isBlank))
			{
				func5(isBlank);
				return;
			}
			IsNotBlank isNotBlank;
			if (this.Is_IsNotBlank(g, out isNotBlank))
			{
				func6(isNotBlank);
				return;
			}
			NumberEquals numberEquals;
			if (this.Is_NumberEquals(g, out numberEquals))
			{
				func7(numberEquals);
				return;
			}
			NumberGreaterThan numberGreaterThan;
			if (this.Is_NumberGreaterThan(g, out numberGreaterThan))
			{
				func8(numberGreaterThan);
				return;
			}
			NumberLessThan numberLessThan;
			if (this.Is_NumberLessThan(g, out numberLessThan))
			{
				func9(numberLessThan);
				return;
			}
			IsString isString;
			if (this.Is_IsString(g, out isString))
			{
				func10(isString);
				return;
			}
			IsNumber isNumber;
			if (this.Is_IsNumber(g, out isNumber))
			{
				func11(isNumber);
				return;
			}
			IsMatch isMatch;
			if (this.Is_IsMatch(g, out isMatch))
			{
				func12(isMatch);
				return;
			}
			ContainsMatch containsMatch;
			if (this.Is_ContainsMatch(g, out containsMatch))
			{
				func13(containsMatch);
				return;
			}
			condition_or condition_or;
			if (this.Is_condition_or(g, out condition_or))
			{
				func14(condition_or);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol condition");
		}

		// Token: 0x0600B6AF RID: 46767 RVA: 0x0027A962 File Offset: 0x00278B62
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B6B0 RID: 46768 RVA: 0x0027A978 File Offset: 0x00278B78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B6B1 RID: 46769 RVA: 0x0027A9A2 File Offset: 0x00278BA2
		public bool Equals(condition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004656 RID: 18006
		private ProgramNode _node;
	}
}
