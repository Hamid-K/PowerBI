using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C41 RID: 7233
	public struct conv : IProgramNodeBuilder, IEquatable<conv>
	{
		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x0600F400 RID: 62464 RVA: 0x00343DBE File Offset: 0x00341FBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F401 RID: 62465 RVA: 0x00343DC6 File Offset: 0x00341FC6
		private conv(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F402 RID: 62466 RVA: 0x00343DCF File Offset: 0x00341FCF
		public static conv CreateUnsafe(ProgramNode node)
		{
			return new conv(node);
		}

		// Token: 0x0600F403 RID: 62467 RVA: 0x00343DD8 File Offset: 0x00341FD8
		public static conv? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.conv)
			{
				return null;
			}
			return new conv?(conv.CreateUnsafe(node));
		}

		// Token: 0x0600F404 RID: 62468 RVA: 0x00343E12 File Offset: 0x00342012
		public static conv CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new conv(new Hole(g.Symbol.conv, holeId));
		}

		// Token: 0x0600F405 RID: 62469 RVA: 0x00343E2A File Offset: 0x0034202A
		public bool Is_SubString(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SubString;
		}

		// Token: 0x0600F406 RID: 62470 RVA: 0x00343E44 File Offset: 0x00342044
		public bool Is_SubString(GrammarBuilders g, out SubString value)
		{
			if (this.Node.GrammarRule == g.Rule.SubString)
			{
				value = SubString.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SubString);
			return false;
		}

		// Token: 0x0600F407 RID: 62471 RVA: 0x00343E7C File Offset: 0x0034207C
		public SubString? As_SubString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SubString)
			{
				return null;
			}
			return new SubString?(SubString.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F408 RID: 62472 RVA: 0x00343EBC File Offset: 0x003420BC
		public SubString Cast_SubString(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SubString)
			{
				return SubString.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SubString is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F409 RID: 62473 RVA: 0x00343F11 File Offset: 0x00342111
		public bool Is_ToLowercase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToLowercase;
		}

		// Token: 0x0600F40A RID: 62474 RVA: 0x00343F2B File Offset: 0x0034212B
		public bool Is_ToLowercase(GrammarBuilders g, out ToLowercase value)
		{
			if (this.Node.GrammarRule == g.Rule.ToLowercase)
			{
				value = ToLowercase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToLowercase);
			return false;
		}

		// Token: 0x0600F40B RID: 62475 RVA: 0x00343F60 File Offset: 0x00342160
		public ToLowercase? As_ToLowercase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToLowercase)
			{
				return null;
			}
			return new ToLowercase?(ToLowercase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F40C RID: 62476 RVA: 0x00343FA0 File Offset: 0x003421A0
		public ToLowercase Cast_ToLowercase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToLowercase)
			{
				return ToLowercase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToLowercase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F40D RID: 62477 RVA: 0x00343FF5 File Offset: 0x003421F5
		public bool Is_ToUppercase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToUppercase;
		}

		// Token: 0x0600F40E RID: 62478 RVA: 0x0034400F File Offset: 0x0034220F
		public bool Is_ToUppercase(GrammarBuilders g, out ToUppercase value)
		{
			if (this.Node.GrammarRule == g.Rule.ToUppercase)
			{
				value = ToUppercase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToUppercase);
			return false;
		}

		// Token: 0x0600F40F RID: 62479 RVA: 0x00344044 File Offset: 0x00342244
		public ToUppercase? As_ToUppercase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToUppercase)
			{
				return null;
			}
			return new ToUppercase?(ToUppercase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F410 RID: 62480 RVA: 0x00344084 File Offset: 0x00342284
		public ToUppercase Cast_ToUppercase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToUppercase)
			{
				return ToUppercase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToUppercase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F411 RID: 62481 RVA: 0x003440D9 File Offset: 0x003422D9
		public bool Is_ToSimpleTitleCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToSimpleTitleCase;
		}

		// Token: 0x0600F412 RID: 62482 RVA: 0x003440F3 File Offset: 0x003422F3
		public bool Is_ToSimpleTitleCase(GrammarBuilders g, out ToSimpleTitleCase value)
		{
			if (this.Node.GrammarRule == g.Rule.ToSimpleTitleCase)
			{
				value = ToSimpleTitleCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToSimpleTitleCase);
			return false;
		}

		// Token: 0x0600F413 RID: 62483 RVA: 0x00344128 File Offset: 0x00342328
		public ToSimpleTitleCase? As_ToSimpleTitleCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToSimpleTitleCase)
			{
				return null;
			}
			return new ToSimpleTitleCase?(ToSimpleTitleCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F414 RID: 62484 RVA: 0x00344168 File Offset: 0x00342368
		public ToSimpleTitleCase Cast_ToSimpleTitleCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToSimpleTitleCase)
			{
				return ToSimpleTitleCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToSimpleTitleCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F415 RID: 62485 RVA: 0x003441BD File Offset: 0x003423BD
		public bool Is_FormatPartialDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FormatPartialDateTime;
		}

		// Token: 0x0600F416 RID: 62486 RVA: 0x003441D7 File Offset: 0x003423D7
		public bool Is_FormatPartialDateTime(GrammarBuilders g, out FormatPartialDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.FormatPartialDateTime)
			{
				value = FormatPartialDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FormatPartialDateTime);
			return false;
		}

		// Token: 0x0600F417 RID: 62487 RVA: 0x0034420C File Offset: 0x0034240C
		public FormatPartialDateTime? As_FormatPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FormatPartialDateTime)
			{
				return null;
			}
			return new FormatPartialDateTime?(FormatPartialDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F418 RID: 62488 RVA: 0x0034424C File Offset: 0x0034244C
		public FormatPartialDateTime Cast_FormatPartialDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FormatPartialDateTime)
			{
				return FormatPartialDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FormatPartialDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F419 RID: 62489 RVA: 0x003442A1 File Offset: 0x003424A1
		public bool Is_FormatNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FormatNumber;
		}

		// Token: 0x0600F41A RID: 62490 RVA: 0x003442BB File Offset: 0x003424BB
		public bool Is_FormatNumber(GrammarBuilders g, out FormatNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.FormatNumber)
			{
				value = FormatNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FormatNumber);
			return false;
		}

		// Token: 0x0600F41B RID: 62491 RVA: 0x003442F0 File Offset: 0x003424F0
		public FormatNumber? As_FormatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FormatNumber)
			{
				return null;
			}
			return new FormatNumber?(FormatNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F41C RID: 62492 RVA: 0x00344330 File Offset: 0x00342530
		public FormatNumber Cast_FormatNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FormatNumber)
			{
				return FormatNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FormatNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F41D RID: 62493 RVA: 0x00344385 File Offset: 0x00342585
		public bool Is_Lookup(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Lookup;
		}

		// Token: 0x0600F41E RID: 62494 RVA: 0x0034439F File Offset: 0x0034259F
		public bool Is_Lookup(GrammarBuilders g, out Lookup value)
		{
			if (this.Node.GrammarRule == g.Rule.Lookup)
			{
				value = Lookup.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Lookup);
			return false;
		}

		// Token: 0x0600F41F RID: 62495 RVA: 0x003443D4 File Offset: 0x003425D4
		public Lookup? As_Lookup(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Lookup)
			{
				return null;
			}
			return new Lookup?(Lookup.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F420 RID: 62496 RVA: 0x00344414 File Offset: 0x00342614
		public Lookup Cast_Lookup(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Lookup)
			{
				return Lookup.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Lookup is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F421 RID: 62497 RVA: 0x00344469 File Offset: 0x00342669
		public bool Is_FormatNumericRange(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FormatNumericRange;
		}

		// Token: 0x0600F422 RID: 62498 RVA: 0x00344483 File Offset: 0x00342683
		public bool Is_FormatNumericRange(GrammarBuilders g, out FormatNumericRange value)
		{
			if (this.Node.GrammarRule == g.Rule.FormatNumericRange)
			{
				value = FormatNumericRange.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FormatNumericRange);
			return false;
		}

		// Token: 0x0600F423 RID: 62499 RVA: 0x003444B8 File Offset: 0x003426B8
		public FormatNumericRange? As_FormatNumericRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FormatNumericRange)
			{
				return null;
			}
			return new FormatNumericRange?(FormatNumericRange.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F424 RID: 62500 RVA: 0x003444F8 File Offset: 0x003426F8
		public FormatNumericRange Cast_FormatNumericRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FormatNumericRange)
			{
				return FormatNumericRange.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FormatNumericRange is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F425 RID: 62501 RVA: 0x0034454D File Offset: 0x0034274D
		public bool Is_FormatDateTimeRange(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FormatDateTimeRange;
		}

		// Token: 0x0600F426 RID: 62502 RVA: 0x00344567 File Offset: 0x00342767
		public bool Is_FormatDateTimeRange(GrammarBuilders g, out FormatDateTimeRange value)
		{
			if (this.Node.GrammarRule == g.Rule.FormatDateTimeRange)
			{
				value = FormatDateTimeRange.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FormatDateTimeRange);
			return false;
		}

		// Token: 0x0600F427 RID: 62503 RVA: 0x0034459C File Offset: 0x0034279C
		public FormatDateTimeRange? As_FormatDateTimeRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FormatDateTimeRange)
			{
				return null;
			}
			return new FormatDateTimeRange?(FormatDateTimeRange.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F428 RID: 62504 RVA: 0x003445DC File Offset: 0x003427DC
		public FormatDateTimeRange Cast_FormatDateTimeRange(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FormatDateTimeRange)
			{
				return FormatDateTimeRange.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FormatDateTimeRange is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F429 RID: 62505 RVA: 0x00344631 File Offset: 0x00342831
		public bool Is_LetSharedParsedNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetSharedParsedNumber;
		}

		// Token: 0x0600F42A RID: 62506 RVA: 0x0034464B File Offset: 0x0034284B
		public bool Is_LetSharedParsedNumber(GrammarBuilders g, out LetSharedParsedNumber value)
		{
			if (this.Node.GrammarRule == g.Rule.LetSharedParsedNumber)
			{
				value = LetSharedParsedNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetSharedParsedNumber);
			return false;
		}

		// Token: 0x0600F42B RID: 62507 RVA: 0x00344680 File Offset: 0x00342880
		public LetSharedParsedNumber? As_LetSharedParsedNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetSharedParsedNumber)
			{
				return null;
			}
			return new LetSharedParsedNumber?(LetSharedParsedNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F42C RID: 62508 RVA: 0x003446C0 File Offset: 0x003428C0
		public LetSharedParsedNumber Cast_LetSharedParsedNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetSharedParsedNumber)
			{
				return LetSharedParsedNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetSharedParsedNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F42D RID: 62509 RVA: 0x00344715 File Offset: 0x00342915
		public bool Is_LetSharedParsedDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetSharedParsedDateTime;
		}

		// Token: 0x0600F42E RID: 62510 RVA: 0x0034472F File Offset: 0x0034292F
		public bool Is_LetSharedParsedDateTime(GrammarBuilders g, out LetSharedParsedDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.LetSharedParsedDateTime)
			{
				value = LetSharedParsedDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetSharedParsedDateTime);
			return false;
		}

		// Token: 0x0600F42F RID: 62511 RVA: 0x00344764 File Offset: 0x00342964
		public LetSharedParsedDateTime? As_LetSharedParsedDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetSharedParsedDateTime)
			{
				return null;
			}
			return new LetSharedParsedDateTime?(LetSharedParsedDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F430 RID: 62512 RVA: 0x003447A4 File Offset: 0x003429A4
		public LetSharedParsedDateTime Cast_LetSharedParsedDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetSharedParsedDateTime)
			{
				return LetSharedParsedDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetSharedParsedDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F431 RID: 62513 RVA: 0x003447FC File Offset: 0x003429FC
		public T Switch<T>(GrammarBuilders g, Func<SubString, T> func0, Func<ToLowercase, T> func1, Func<ToUppercase, T> func2, Func<ToSimpleTitleCase, T> func3, Func<FormatPartialDateTime, T> func4, Func<FormatNumber, T> func5, Func<Lookup, T> func6, Func<FormatNumericRange, T> func7, Func<FormatDateTimeRange, T> func8, Func<LetSharedParsedNumber, T> func9, Func<LetSharedParsedDateTime, T> func10)
		{
			SubString subString;
			if (this.Is_SubString(g, out subString))
			{
				return func0(subString);
			}
			ToLowercase toLowercase;
			if (this.Is_ToLowercase(g, out toLowercase))
			{
				return func1(toLowercase);
			}
			ToUppercase toUppercase;
			if (this.Is_ToUppercase(g, out toUppercase))
			{
				return func2(toUppercase);
			}
			ToSimpleTitleCase toSimpleTitleCase;
			if (this.Is_ToSimpleTitleCase(g, out toSimpleTitleCase))
			{
				return func3(toSimpleTitleCase);
			}
			FormatPartialDateTime formatPartialDateTime;
			if (this.Is_FormatPartialDateTime(g, out formatPartialDateTime))
			{
				return func4(formatPartialDateTime);
			}
			FormatNumber formatNumber;
			if (this.Is_FormatNumber(g, out formatNumber))
			{
				return func5(formatNumber);
			}
			Lookup lookup;
			if (this.Is_Lookup(g, out lookup))
			{
				return func6(lookup);
			}
			FormatNumericRange formatNumericRange;
			if (this.Is_FormatNumericRange(g, out formatNumericRange))
			{
				return func7(formatNumericRange);
			}
			FormatDateTimeRange formatDateTimeRange;
			if (this.Is_FormatDateTimeRange(g, out formatDateTimeRange))
			{
				return func8(formatDateTimeRange);
			}
			LetSharedParsedNumber letSharedParsedNumber;
			if (this.Is_LetSharedParsedNumber(g, out letSharedParsedNumber))
			{
				return func9(letSharedParsedNumber);
			}
			LetSharedParsedDateTime letSharedParsedDateTime;
			if (this.Is_LetSharedParsedDateTime(g, out letSharedParsedDateTime))
			{
				return func10(letSharedParsedDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol conv");
		}

		// Token: 0x0600F432 RID: 62514 RVA: 0x00344910 File Offset: 0x00342B10
		public void Switch(GrammarBuilders g, Action<SubString> func0, Action<ToLowercase> func1, Action<ToUppercase> func2, Action<ToSimpleTitleCase> func3, Action<FormatPartialDateTime> func4, Action<FormatNumber> func5, Action<Lookup> func6, Action<FormatNumericRange> func7, Action<FormatDateTimeRange> func8, Action<LetSharedParsedNumber> func9, Action<LetSharedParsedDateTime> func10)
		{
			SubString subString;
			if (this.Is_SubString(g, out subString))
			{
				func0(subString);
				return;
			}
			ToLowercase toLowercase;
			if (this.Is_ToLowercase(g, out toLowercase))
			{
				func1(toLowercase);
				return;
			}
			ToUppercase toUppercase;
			if (this.Is_ToUppercase(g, out toUppercase))
			{
				func2(toUppercase);
				return;
			}
			ToSimpleTitleCase toSimpleTitleCase;
			if (this.Is_ToSimpleTitleCase(g, out toSimpleTitleCase))
			{
				func3(toSimpleTitleCase);
				return;
			}
			FormatPartialDateTime formatPartialDateTime;
			if (this.Is_FormatPartialDateTime(g, out formatPartialDateTime))
			{
				func4(formatPartialDateTime);
				return;
			}
			FormatNumber formatNumber;
			if (this.Is_FormatNumber(g, out formatNumber))
			{
				func5(formatNumber);
				return;
			}
			Lookup lookup;
			if (this.Is_Lookup(g, out lookup))
			{
				func6(lookup);
				return;
			}
			FormatNumericRange formatNumericRange;
			if (this.Is_FormatNumericRange(g, out formatNumericRange))
			{
				func7(formatNumericRange);
				return;
			}
			FormatDateTimeRange formatDateTimeRange;
			if (this.Is_FormatDateTimeRange(g, out formatDateTimeRange))
			{
				func8(formatDateTimeRange);
				return;
			}
			LetSharedParsedNumber letSharedParsedNumber;
			if (this.Is_LetSharedParsedNumber(g, out letSharedParsedNumber))
			{
				func9(letSharedParsedNumber);
				return;
			}
			LetSharedParsedDateTime letSharedParsedDateTime;
			if (this.Is_LetSharedParsedDateTime(g, out letSharedParsedDateTime))
			{
				func10(letSharedParsedDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol conv");
		}

		// Token: 0x0600F433 RID: 62515 RVA: 0x00344A22 File Offset: 0x00342C22
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F434 RID: 62516 RVA: 0x00344A38 File Offset: 0x00342C38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F435 RID: 62517 RVA: 0x00344A62 File Offset: 0x00342C62
		public bool Equals(conv other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B30 RID: 23344
		private ProgramNode _node;
	}
}
