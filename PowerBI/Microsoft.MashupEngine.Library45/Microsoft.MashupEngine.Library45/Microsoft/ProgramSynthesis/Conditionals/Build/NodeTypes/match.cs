using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes
{
	// Token: 0x02000A50 RID: 2640
	public struct match : IProgramNodeBuilder, IEquatable<match>
	{
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x000CC19E File Offset: 0x000CA39E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000CC1A6 File Offset: 0x000CA3A6
		private match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000CC1AF File Offset: 0x000CA3AF
		public static match CreateUnsafe(ProgramNode node)
		{
			return new match(node);
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000CC1B8 File Offset: 0x000CA3B8
		public static match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.match)
			{
				return null;
			}
			return new match?(match.CreateUnsafe(node));
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x000CC1F2 File Offset: 0x000CA3F2
		public static match CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new match(new Hole(g.Symbol.match, holeId));
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x000CC20A File Offset: 0x000CA40A
		public bool Is_IsNullOrWhiteSpace(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNullOrWhiteSpace;
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000CC224 File Offset: 0x000CA424
		public bool Is_IsNullOrWhiteSpace(GrammarBuilders g, out IsNullOrWhiteSpace value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNullOrWhiteSpace)
			{
				value = IsNullOrWhiteSpace.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNullOrWhiteSpace);
			return false;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000CC25C File Offset: 0x000CA45C
		public IsNullOrWhiteSpace? As_IsNullOrWhiteSpace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNullOrWhiteSpace)
			{
				return null;
			}
			return new IsNullOrWhiteSpace?(IsNullOrWhiteSpace.CreateUnsafe(this.Node));
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000CC29C File Offset: 0x000CA49C
		public IsNullOrWhiteSpace Cast_IsNullOrWhiteSpace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNullOrWhiteSpace)
			{
				return IsNullOrWhiteSpace.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNullOrWhiteSpace is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000CC2F1 File Offset: 0x000CA4F1
		public bool Is_IsNull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNull;
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000CC30B File Offset: 0x000CA50B
		public bool Is_IsNull(GrammarBuilders g, out IsNull value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNull)
			{
				value = IsNull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNull);
			return false;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000CC340 File Offset: 0x000CA540
		public IsNull? As_IsNull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNull)
			{
				return null;
			}
			return new IsNull?(IsNull.CreateUnsafe(this.Node));
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x000CC380 File Offset: 0x000CA580
		public IsNull Cast_IsNull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNull)
			{
				return IsNull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000CC3D5 File Offset: 0x000CA5D5
		public bool Is_IsWhiteSpace(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsWhiteSpace;
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000CC3EF File Offset: 0x000CA5EF
		public bool Is_IsWhiteSpace(GrammarBuilders g, out IsWhiteSpace value)
		{
			if (this.Node.GrammarRule == g.Rule.IsWhiteSpace)
			{
				value = IsWhiteSpace.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsWhiteSpace);
			return false;
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000CC424 File Offset: 0x000CA624
		public IsWhiteSpace? As_IsWhiteSpace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsWhiteSpace)
			{
				return null;
			}
			return new IsWhiteSpace?(IsWhiteSpace.CreateUnsafe(this.Node));
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000CC464 File Offset: 0x000CA664
		public IsWhiteSpace Cast_IsWhiteSpace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsWhiteSpace)
			{
				return IsWhiteSpace.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsWhiteSpace is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000CC4B9 File Offset: 0x000CA6B9
		public bool Is_True(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.True;
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000CC4D3 File Offset: 0x000CA6D3
		public bool Is_True(GrammarBuilders g, out True value)
		{
			if (this.Node.GrammarRule == g.Rule.True)
			{
				value = True.CreateUnsafe(this.Node);
				return true;
			}
			value = default(True);
			return false;
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x000CC508 File Offset: 0x000CA708
		public True? As_True(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.True)
			{
				return null;
			}
			return new True?(True.CreateUnsafe(this.Node));
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000CC548 File Offset: 0x000CA748
		public True Cast_True(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.True)
			{
				return True.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_True is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000CC59D File Offset: 0x000CA79D
		public bool Is_StartsWithString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWithString;
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000CC5B7 File Offset: 0x000CA7B7
		public bool Is_StartsWithString(GrammarBuilders g, out StartsWithString value)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithString)
			{
				value = StartsWithString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StartsWithString);
			return false;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000CC5EC File Offset: 0x000CA7EC
		public StartsWithString? As_StartsWithString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWithString)
			{
				return null;
			}
			return new StartsWithString?(StartsWithString.CreateUnsafe(this.Node));
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000CC62C File Offset: 0x000CA82C
		public StartsWithString Cast_StartsWithString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithString)
			{
				return StartsWithString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWithString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000CC681 File Offset: 0x000CA881
		public bool Is_StartsWithDigit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWithDigit;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x000CC69B File Offset: 0x000CA89B
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

		// Token: 0x0600414A RID: 16714 RVA: 0x000CC6D0 File Offset: 0x000CA8D0
		public StartsWithDigit? As_StartsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWithDigit)
			{
				return null;
			}
			return new StartsWithDigit?(StartsWithDigit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x000CC710 File Offset: 0x000CA910
		public StartsWithDigit Cast_StartsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithDigit)
			{
				return StartsWithDigit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWithDigit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000CC765 File Offset: 0x000CA965
		public bool Is_StartsWithLetter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWithLetter;
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000CC77F File Offset: 0x000CA97F
		public bool Is_StartsWithLetter(GrammarBuilders g, out StartsWithLetter value)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithLetter)
			{
				value = StartsWithLetter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(StartsWithLetter);
			return false;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000CC7B4 File Offset: 0x000CA9B4
		public StartsWithLetter? As_StartsWithLetter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWithLetter)
			{
				return null;
			}
			return new StartsWithLetter?(StartsWithLetter.CreateUnsafe(this.Node));
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000CC7F4 File Offset: 0x000CA9F4
		public StartsWithLetter Cast_StartsWithLetter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWithLetter)
			{
				return StartsWithLetter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWithLetter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000CC849 File Offset: 0x000CAA49
		public bool Is_EndsWithString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndsWithString;
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x000CC863 File Offset: 0x000CAA63
		public bool Is_EndsWithString(GrammarBuilders g, out EndsWithString value)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithString)
			{
				value = EndsWithString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EndsWithString);
			return false;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x000CC898 File Offset: 0x000CAA98
		public EndsWithString? As_EndsWithString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndsWithString)
			{
				return null;
			}
			return new EndsWithString?(EndsWithString.CreateUnsafe(this.Node));
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x000CC8D8 File Offset: 0x000CAAD8
		public EndsWithString Cast_EndsWithString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithString)
			{
				return EndsWithString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndsWithString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000CC92D File Offset: 0x000CAB2D
		public bool Is_EndsWithDigit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndsWithDigit;
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000CC947 File Offset: 0x000CAB47
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

		// Token: 0x06004156 RID: 16726 RVA: 0x000CC97C File Offset: 0x000CAB7C
		public EndsWithDigit? As_EndsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndsWithDigit)
			{
				return null;
			}
			return new EndsWithDigit?(EndsWithDigit.CreateUnsafe(this.Node));
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000CC9BC File Offset: 0x000CABBC
		public EndsWithDigit Cast_EndsWithDigit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithDigit)
			{
				return EndsWithDigit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndsWithDigit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000CCA11 File Offset: 0x000CAC11
		public bool Is_EndsWithLetter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndsWithLetter;
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x000CCA2B File Offset: 0x000CAC2B
		public bool Is_EndsWithLetter(GrammarBuilders g, out EndsWithLetter value)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithLetter)
			{
				value = EndsWithLetter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EndsWithLetter);
			return false;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x000CCA60 File Offset: 0x000CAC60
		public EndsWithLetter? As_EndsWithLetter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndsWithLetter)
			{
				return null;
			}
			return new EndsWithLetter?(EndsWithLetter.CreateUnsafe(this.Node));
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000CCAA0 File Offset: 0x000CACA0
		public EndsWithLetter Cast_EndsWithLetter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWithLetter)
			{
				return EndsWithLetter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndsWithLetter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000CCAF5 File Offset: 0x000CACF5
		public bool Is_ContainsString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ContainsString;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x000CCB0F File Offset: 0x000CAD0F
		public bool Is_ContainsString(GrammarBuilders g, out ContainsString value)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsString)
			{
				value = ContainsString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ContainsString);
			return false;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000CCB44 File Offset: 0x000CAD44
		public ContainsString? As_ContainsString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ContainsString)
			{
				return null;
			}
			return new ContainsString?(ContainsString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x000CCB84 File Offset: 0x000CAD84
		public ContainsString Cast_ContainsString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsString)
			{
				return ContainsString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ContainsString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x000CCBD9 File Offset: 0x000CADD9
		public bool Is_Matches(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Matches;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000CCBF3 File Offset: 0x000CADF3
		public bool Is_Matches(GrammarBuilders g, out Matches value)
		{
			if (this.Node.GrammarRule == g.Rule.Matches)
			{
				value = Matches.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Matches);
			return false;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000CCC28 File Offset: 0x000CAE28
		public Matches? As_Matches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Matches)
			{
				return null;
			}
			return new Matches?(Matches.CreateUnsafe(this.Node));
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000CCC68 File Offset: 0x000CAE68
		public Matches Cast_Matches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Matches)
			{
				return Matches.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Matches is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000CCCBD File Offset: 0x000CAEBD
		public bool Is_StartsWith(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.StartsWith;
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000CCCD7 File Offset: 0x000CAED7
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

		// Token: 0x06004166 RID: 16742 RVA: 0x000CCD0C File Offset: 0x000CAF0C
		public StartsWith? As_StartsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.StartsWith)
			{
				return null;
			}
			return new StartsWith?(StartsWith.CreateUnsafe(this.Node));
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000CCD4C File Offset: 0x000CAF4C
		public StartsWith Cast_StartsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.StartsWith)
			{
				return StartsWith.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_StartsWith is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000CCDA1 File Offset: 0x000CAFA1
		public bool Is_EndsWith(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndsWith;
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000CCDBB File Offset: 0x000CAFBB
		public bool Is_EndsWith(GrammarBuilders g, out EndsWith value)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWith)
			{
				value = EndsWith.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EndsWith);
			return false;
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000CCDF0 File Offset: 0x000CAFF0
		public EndsWith? As_EndsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndsWith)
			{
				return null;
			}
			return new EndsWith?(EndsWith.CreateUnsafe(this.Node));
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000CCE30 File Offset: 0x000CB030
		public EndsWith Cast_EndsWith(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndsWith)
			{
				return EndsWith.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndsWith is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000CCE85 File Offset: 0x000CB085
		public bool Is_Contains(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Contains;
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000CCE9F File Offset: 0x000CB09F
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

		// Token: 0x0600416E RID: 16750 RVA: 0x000CCED4 File Offset: 0x000CB0D4
		public Contains? As_Contains(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Contains)
			{
				return null;
			}
			return new Contains?(Contains.CreateUnsafe(this.Node));
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000CCF14 File Offset: 0x000CB114
		public Contains Cast_Contains(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Contains)
			{
				return Contains.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Contains is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000CCF6C File Offset: 0x000CB16C
		public T Switch<T>(GrammarBuilders g, Func<IsNullOrWhiteSpace, T> func0, Func<IsNull, T> func1, Func<IsWhiteSpace, T> func2, Func<True, T> func3, Func<StartsWithString, T> func4, Func<StartsWithDigit, T> func5, Func<StartsWithLetter, T> func6, Func<EndsWithString, T> func7, Func<EndsWithDigit, T> func8, Func<EndsWithLetter, T> func9, Func<ContainsString, T> func10, Func<Matches, T> func11, Func<StartsWith, T> func12, Func<EndsWith, T> func13, Func<Contains, T> func14)
		{
			IsNullOrWhiteSpace isNullOrWhiteSpace;
			if (this.Is_IsNullOrWhiteSpace(g, out isNullOrWhiteSpace))
			{
				return func0(isNullOrWhiteSpace);
			}
			IsNull isNull;
			if (this.Is_IsNull(g, out isNull))
			{
				return func1(isNull);
			}
			IsWhiteSpace isWhiteSpace;
			if (this.Is_IsWhiteSpace(g, out isWhiteSpace))
			{
				return func2(isWhiteSpace);
			}
			True @true;
			if (this.Is_True(g, out @true))
			{
				return func3(@true);
			}
			StartsWithString startsWithString;
			if (this.Is_StartsWithString(g, out startsWithString))
			{
				return func4(startsWithString);
			}
			StartsWithDigit startsWithDigit;
			if (this.Is_StartsWithDigit(g, out startsWithDigit))
			{
				return func5(startsWithDigit);
			}
			StartsWithLetter startsWithLetter;
			if (this.Is_StartsWithLetter(g, out startsWithLetter))
			{
				return func6(startsWithLetter);
			}
			EndsWithString endsWithString;
			if (this.Is_EndsWithString(g, out endsWithString))
			{
				return func7(endsWithString);
			}
			EndsWithDigit endsWithDigit;
			if (this.Is_EndsWithDigit(g, out endsWithDigit))
			{
				return func8(endsWithDigit);
			}
			EndsWithLetter endsWithLetter;
			if (this.Is_EndsWithLetter(g, out endsWithLetter))
			{
				return func9(endsWithLetter);
			}
			ContainsString containsString;
			if (this.Is_ContainsString(g, out containsString))
			{
				return func10(containsString);
			}
			Matches matches;
			if (this.Is_Matches(g, out matches))
			{
				return func11(matches);
			}
			StartsWith startsWith;
			if (this.Is_StartsWith(g, out startsWith))
			{
				return func12(startsWith);
			}
			EndsWith endsWith;
			if (this.Is_EndsWith(g, out endsWith))
			{
				return func13(endsWith);
			}
			Contains contains;
			if (this.Is_Contains(g, out contains))
			{
				return func14(contains);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x000CD0D4 File Offset: 0x000CB2D4
		public void Switch(GrammarBuilders g, Action<IsNullOrWhiteSpace> func0, Action<IsNull> func1, Action<IsWhiteSpace> func2, Action<True> func3, Action<StartsWithString> func4, Action<StartsWithDigit> func5, Action<StartsWithLetter> func6, Action<EndsWithString> func7, Action<EndsWithDigit> func8, Action<EndsWithLetter> func9, Action<ContainsString> func10, Action<Matches> func11, Action<StartsWith> func12, Action<EndsWith> func13, Action<Contains> func14)
		{
			IsNullOrWhiteSpace isNullOrWhiteSpace;
			if (this.Is_IsNullOrWhiteSpace(g, out isNullOrWhiteSpace))
			{
				func0(isNullOrWhiteSpace);
				return;
			}
			IsNull isNull;
			if (this.Is_IsNull(g, out isNull))
			{
				func1(isNull);
				return;
			}
			IsWhiteSpace isWhiteSpace;
			if (this.Is_IsWhiteSpace(g, out isWhiteSpace))
			{
				func2(isWhiteSpace);
				return;
			}
			True @true;
			if (this.Is_True(g, out @true))
			{
				func3(@true);
				return;
			}
			StartsWithString startsWithString;
			if (this.Is_StartsWithString(g, out startsWithString))
			{
				func4(startsWithString);
				return;
			}
			StartsWithDigit startsWithDigit;
			if (this.Is_StartsWithDigit(g, out startsWithDigit))
			{
				func5(startsWithDigit);
				return;
			}
			StartsWithLetter startsWithLetter;
			if (this.Is_StartsWithLetter(g, out startsWithLetter))
			{
				func6(startsWithLetter);
				return;
			}
			EndsWithString endsWithString;
			if (this.Is_EndsWithString(g, out endsWithString))
			{
				func7(endsWithString);
				return;
			}
			EndsWithDigit endsWithDigit;
			if (this.Is_EndsWithDigit(g, out endsWithDigit))
			{
				func8(endsWithDigit);
				return;
			}
			EndsWithLetter endsWithLetter;
			if (this.Is_EndsWithLetter(g, out endsWithLetter))
			{
				func9(endsWithLetter);
				return;
			}
			ContainsString containsString;
			if (this.Is_ContainsString(g, out containsString))
			{
				func10(containsString);
				return;
			}
			Matches matches;
			if (this.Is_Matches(g, out matches))
			{
				func11(matches);
				return;
			}
			StartsWith startsWith;
			if (this.Is_StartsWith(g, out startsWith))
			{
				func12(startsWith);
				return;
			}
			EndsWith endsWith;
			if (this.Is_EndsWith(g, out endsWith))
			{
				func13(endsWith);
				return;
			}
			Contains contains;
			if (this.Is_Contains(g, out contains))
			{
				func14(contains);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x000CD23A File Offset: 0x000CB43A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x000CD250 File Offset: 0x000CB450
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x000CD27A File Offset: 0x000CB47A
		public bool Equals(match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D8B RID: 7563
		private ProgramNode _node;
	}
}
