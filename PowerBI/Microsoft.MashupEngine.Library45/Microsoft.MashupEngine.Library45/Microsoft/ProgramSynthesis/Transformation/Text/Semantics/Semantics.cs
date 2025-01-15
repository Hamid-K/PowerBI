using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CC4 RID: 7364
	public static class Semantics
	{
		// Token: 0x0600F9A1 RID: 63905 RVA: 0x00351040 File Offset: 0x0034F240
		static Semantics()
		{
			double log52 = Math.Log(52.0);
			double log26 = Math.Log(26.0);
			double log2 = Math.Log(2.0);
			string text = FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[]
			{
				new string(NumberOptions.DecimalMarkOptions)
			}));
			KeyValuePair<string, Token>[] array = new KeyValuePair<string, Token>[]
			{
				new KeyValuePair<string, Token>("SemiColonOrComma", new RegexToken("(\\;|\\,)", "SemiColonOrComma", 15, -5.5, (string s) => -log2, true, true, null)),
				new KeyValuePair<string, Token>("CommaOrSpaceLeftBracket", new RegexToken("(\\s?\\,|;|(\\s+\\()|\\-|\\))", "CommaOrSpaceLeftBracket", 14, -5.5, (string s) => -log2, true, true, null)),
				new KeyValuePair<string, Token>("Upper Case", new RegexToken("\\p{Lu}", "Upper Case", 100, -5.5, (string s) => -log26, false, true, null)),
				new KeyValuePair<string, Token>("List of Lower Case", new RegexToken("\\p{Ll}(\\p{Ll})+", "List of Lower Case", 10, -5.5, (string s) => -log26 * (double)s.Length, false, true, null)),
				new KeyValuePair<string, Token>("List of Upper and Lower Case", new RegexToken("(\\p{Ll}|\\p{Lu})(\\p{Ll}|\\p{Lu})+", "List of Upper and Lower Case", 5, -5.5, (string s) => -log52 * (double)s.Length, false, true, null)),
				new KeyValuePair<string, Token>("GeneralNumber", new RegexToken(FormattableString.Invariant(FormattableStringFactory.Create("(?<!\\d)[-+(]?{0}?((\\d{{1,4}}({1}\\d{{2,4}})+)|\\d+)?(\\d{2}?|{3}\\d+)([eE][-+]?\\d+)?[-+)]?(?!\\d)", new object[] { "\\p{Sc}", "[,.'\\p{Pd}\\p{Pc}\\p{Po}\\p{Zs}]", text, text })), "GeneralNumber", 0, -5.5, (string s) => 0.0, true, false, null))
			};
			Token.RegisterTokens(array);
			Semantics.Tokens = Token.Tokens.Concat(array).ToDictionary((KeyValuePair<string, Token> kv) => kv.Key, (KeyValuePair<string, Token> kv) => kv.Value);
		}

		// Token: 0x0600F9A2 RID: 63906 RVA: 0x00351291 File Offset: 0x0034F491
		public static Token GetStaticTokenByName(string name)
		{
			return Semantics.Tokens.MaybeGet(name).OrElseDefault<Token>();
		}

		// Token: 0x0600F9A3 RID: 63907 RVA: 0x001D459D File Offset: 0x001D279D
		[LazySemantics]
		public static ValueSubstring IfThenElse(bool pred, ValueSubstring t, ValueSubstring st)
		{
			if (!pred)
			{
				return st;
			}
			return t;
		}

		// Token: 0x0600F9A4 RID: 63908 RVA: 0x003512A4 File Offset: 0x0034F4A4
		public static ValueSubstring ConstStr(string s)
		{
			return ValueSubstring.Create(s, null, null, null, null);
		}

		// Token: 0x0600F9A5 RID: 63909 RVA: 0x003512CC File Offset: 0x0034F4CC
		public static ValueSubstring ChooseInput(IRow row, string columnName)
		{
			object obj = Semantics.LookupInput(row, columnName);
			if (obj != null)
			{
				return Semantics.AsValueSubstring(obj);
			}
			return null;
		}

		// Token: 0x0600F9A6 RID: 63910 RVA: 0x003512EC File Offset: 0x0034F4EC
		public static object LookupInput(IRow row, string columnName)
		{
			object obj;
			if (!row.TryGetValue(columnName, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x0600F9A7 RID: 63911 RVA: 0x00351308 File Offset: 0x0034F508
		public static ValueSubstring IndexInputString(IRow row, int index)
		{
			string text = null;
			IndexableRowWrapper indexableRowWrapper = row as IndexableRowWrapper;
			if (indexableRowWrapper != null)
			{
				indexableRowWrapper.IndexableRow.TryGetString(index, out text);
			}
			return ValueSubstring.Create(text, null, null, null, null);
		}

		// Token: 0x0600F9A8 RID: 63912 RVA: 0x0035134C File Offset: 0x0034F54C
		private static ValueSubstring AsValueSubstring(object obj)
		{
			ValueSubstring valueSubstring;
			if ((valueSubstring = obj as ValueSubstring) == null)
			{
				valueSubstring = ValueSubstring.Create(obj as string, null, null, null, null);
			}
			return valueSubstring;
		}

		// Token: 0x0600F9A9 RID: 63913 RVA: 0x00351384 File Offset: 0x0034F584
		public static decimal? AsDecimal(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is decimal)
			{
				return new decimal?((decimal)obj);
			}
			if (obj is double)
			{
				return new decimal?((decimal)((double)obj));
			}
			if (obj is int)
			{
				return new decimal?((int)obj);
			}
			if (obj is long)
			{
				return new decimal?((long)obj);
			}
			if (obj is float)
			{
				return new decimal?((decimal)((float)obj));
			}
			return null;
		}

		// Token: 0x0600F9AA RID: 63914 RVA: 0x00351420 File Offset: 0x0034F620
		public static PartialDateTime AsPartialDateTime(object obj)
		{
			PartialDateTime partialDateTime = obj as PartialDateTime;
			if (partialDateTime != null)
			{
				return partialDateTime;
			}
			DateTime? dateTime = obj as DateTime?;
			if (dateTime != null)
			{
				return PartialDateTime.Create(dateTime.Value);
			}
			return null;
		}

		// Token: 0x0600F9AB RID: 63915 RVA: 0x00351462 File Offset: 0x0034F662
		public static ValueSubstring Concat(ValueSubstring s1, ValueSubstring s2)
		{
			if (s1 == null || s2 == null)
			{
				return null;
			}
			return s1.Concat(s2);
		}

		// Token: 0x0600F9AC RID: 63916 RVA: 0x00351480 File Offset: 0x0034F680
		public static ValueSubstring ToLowercase(ValueSubstring s)
		{
			return ValueSubstring.Create((s != null) ? s.Value.ToLowerInvariant() : null, null, null, null, null);
		}

		// Token: 0x0600F9AD RID: 63917 RVA: 0x003514B8 File Offset: 0x0034F6B8
		public static ValueSubstring ToUppercase(ValueSubstring s)
		{
			return ValueSubstring.Create((s != null) ? s.Value.ToUpperInvariant() : null, null, null, null, null);
		}

		// Token: 0x0600F9AE RID: 63918 RVA: 0x003514F0 File Offset: 0x0034F6F0
		public static ValueSubstring ToSimpleTitleCase(ValueSubstring s)
		{
			if (s == null)
			{
				return null;
			}
			char[] array = s.Value.ToCharArray();
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				if (char.IsLetter(array[i]))
				{
					if (flag)
					{
						array[i] = char.ToLowerInvariant(array[i]);
					}
					else
					{
						array[i] = char.ToUpperInvariant(array[i]);
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			return ValueSubstring.Create(new string(array), null, null, null, null);
		}

		// Token: 0x0600F9AF RID: 63919 RVA: 0x00351570 File Offset: 0x0034F770
		public static ValueSubstring SubStr(ValueSubstring sr, Record<uint?, uint?>? pp)
		{
			uint? item = pp.Value.Item1;
			uint? item2 = pp.Value.Item2;
			if (item == null || item2 == null)
			{
				return null;
			}
			uint? num = item;
			uint num2 = sr.Length;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				return null;
			}
			num = item2;
			num2 = sr.Length;
			if ((num.GetValueOrDefault() > num2) & (num != null))
			{
				return null;
			}
			num = item;
			uint? num3 = item2;
			if ((num.GetValueOrDefault() > num3.GetValueOrDefault()) & ((num != null) & (num3 != null)))
			{
				return null;
			}
			return sr.SliceRelative(item.Value, new uint?(item2.Value));
		}

		// Token: 0x0600F9B0 RID: 63920 RVA: 0x0035162C File Offset: 0x0034F82C
		public static ValueSubstring RSubStr(ValueSubstring s, uint? idx)
		{
			if (idx == null)
			{
				return null;
			}
			return s.SliceRelative(idx.Value, null);
		}

		// Token: 0x0600F9B1 RID: 63921 RVA: 0x0035165C File Offset: 0x0034F85C
		public static uint? Add(uint? x, uint? y)
		{
			if (x == null || y == null)
			{
				return null;
			}
			uint? num = x;
			uint? num2 = y;
			if (!((num != null) & (num2 != null)))
			{
				return null;
			}
			return new uint?(num.GetValueOrDefault() + num2.GetValueOrDefault());
		}

		// Token: 0x0600F9B2 RID: 63922 RVA: 0x003516B8 File Offset: 0x0034F8B8
		public static uint? AbsolutePosition(ValueSubstring s, int k)
		{
			int num = ((k < 0) ? (s.Source.Length + k + 1) : k);
			if (num >= 0 && num <= s.Source.Length)
			{
				return new uint?((uint)num);
			}
			return null;
		}

		// Token: 0x0600F9B3 RID: 63923 RVA: 0x00351700 File Offset: 0x0034F900
		public static uint? RelativePosition(ValueSubstring s, int k)
		{
			int num = (int)((k < 0) ? (s.Length + (uint)k + 1U) : ((uint)k));
			if (num >= 0 && (long)num <= (long)((ulong)s.Length))
			{
				return new uint?((uint)num);
			}
			return null;
		}

		// Token: 0x0600F9B4 RID: 63924 RVA: 0x0035173E File Offset: 0x0034F93E
		public static uint? RegexPosition(ValueSubstring sr, Record<RegularExpression, RegularExpression>? RR, int k)
		{
			return RegularExpressionPositions.RegexPosition(sr, RR.Value, k);
		}

		// Token: 0x0600F9B5 RID: 63925 RVA: 0x00351750 File Offset: 0x0034F950
		public static uint? RegexPositionRelative(ValueSubstring sr, Record<RegularExpression, RegularExpression>? RR, int k)
		{
			uint? num = RegularExpressionPositions.RegexPosition(sr, RR.Value, k);
			uint start = sr.Start;
			if (num == null)
			{
				return null;
			}
			return new uint?(num.GetValueOrDefault() - start);
		}

		// Token: 0x0600F9B6 RID: 63926 RVA: 0x00351794 File Offset: 0x0034F994
		public static Record<uint?, uint?>? RegexPositionPair(ValueSubstring sr, RegularExpression r, int k)
		{
			if (sr == null)
			{
				return null;
			}
			IReadOnlyList<PositionMatch> readOnlyList = r.Run(sr);
			int num = ((k < 0) ? (readOnlyList.Count + k) : (k - 1));
			if (num >= readOnlyList.Count || num < 0)
			{
				return null;
			}
			PositionMatch positionMatch = readOnlyList[num];
			return new Record<uint?, uint?>?(new Record<uint?, uint?>(new uint?(positionMatch.Position - sr.Start), new uint?(positionMatch.Right - sr.Start)));
		}

		// Token: 0x0600F9B7 RID: 63927 RVA: 0x0035181C File Offset: 0x0034FA1C
		public static Record<uint?, uint?>? ExternalExtractorPositionPair(ValueSubstring x, CustomExtractor extractor, int k)
		{
			if (x == null)
			{
				return null;
			}
			IReadOnlyList<Record<uint, uint>> readOnlyList = (from t in extractor.Extract(x.Source)
				where t.Item1 >= x.Start && t.Item2 <= x.End
				select t).ToList<Record<uint, uint>>();
			int num = ((k < 0) ? (readOnlyList.Count + k) : k);
			if (readOnlyList.Count == 0 || num >= readOnlyList.Count)
			{
				return null;
			}
			Record<uint, uint> record = readOnlyList[num];
			return new Record<uint?, uint?>?(new Record<uint?, uint?>(new uint?(record.Item1), new uint?(record.Item2)));
		}

		// Token: 0x0600F9B8 RID: 63928 RVA: 0x003518CC File Offset: 0x0034FACC
		public static decimal? ParseNumber(ValueSubstring sr, NumberFormatDetails formatDetails)
		{
			if (sr == null || sr.Length <= 0U)
			{
				return null;
			}
			NumberStyles numberStyles = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
			if (formatDetails.TrailingSign)
			{
				numberStyles |= NumberStyles.AllowTrailingSign;
			}
			if (formatDetails.CurrencySymbol.HasValue)
			{
				numberStyles |= NumberStyles.AllowCurrencySymbol;
			}
			bool flag = false;
			if (formatDetails.AllowParseParensAsNegative && sr.Source[(int)sr.Start] == '(')
			{
				if (sr.Source[(int)(sr.End - 1U)] != ')')
				{
					return null;
				}
				flag = true;
				sr = sr.Slice(sr.Start + 1U, new uint?(sr.End - 1U));
			}
			decimal num;
			if (decimal.TryParse(sr.Value, numberStyles, formatDetails.NumberFormatInfo, out num))
			{
				return new decimal?(flag ? (-num) : num);
			}
			if (!sr.Value.Any((char c) => char.IsDigit(c) && (c < '0' || c > '9')))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(sr.Value.Length);
			foreach (char c2 in sr.Value)
			{
				stringBuilder.Append(char.IsDigit(c2) ? Semantics.Digits[(int)char.GetNumericValue(c2)] : c2);
			}
			if (decimal.TryParse(stringBuilder.ToString(), numberStyles, formatDetails.NumberFormatInfo, out num))
			{
				return new decimal?(flag ? (-num) : num);
			}
			return null;
		}

		// Token: 0x0600F9B9 RID: 63929 RVA: 0x00351A70 File Offset: 0x0034FC70
		public static decimal? RoundNumber(decimal? number, RoundingSpec roundingSpecification)
		{
			if (number == null)
			{
				return null;
			}
			decimal zero = roundingSpecification.Zero;
			decimal delta = roundingSpecification.Delta;
			decimal num = Math.Floor((number.Value - zero) / delta) * delta + zero;
			decimal num2;
			if (roundingSpecification.Mode == RoundingMode.UpOrNext)
			{
				num2 = num + delta;
			}
			else
			{
				if (!(num == number.Value))
				{
					switch (roundingSpecification.Mode)
					{
					case RoundingMode.Nearest:
					{
						decimal? num3 = (number - num) * 2;
						decimal num4 = delta;
						if ((num3.GetValueOrDefault() < num4) & (num3 != null))
						{
							num2 = num;
							goto IL_01CF;
						}
						num2 = num + delta;
						goto IL_01CF;
					}
					case RoundingMode.Down:
						num2 = num;
						goto IL_01CF;
					case RoundingMode.Up:
						num2 = num + delta;
						goto IL_01CF;
					case RoundingMode.TowardZero:
					{
						decimal? num3 = number;
						decimal num4 = 0m;
						num2 = (((num3.GetValueOrDefault() < num4) & (num3 != null)) ? (num + delta) : num);
						goto IL_01CF;
					}
					case RoundingMode.AwayFromZero:
					{
						decimal? num3 = number;
						decimal num4 = 0m;
						num2 = (((num3.GetValueOrDefault() < num4) & (num3 != null)) ? num : (num + delta));
						goto IL_01CF;
					}
					}
					throw new NotImplementedException("Unknown rounding mode: " + roundingSpecification.Mode.ToString());
				}
				num2 = number.Value;
			}
			IL_01CF:
			return new decimal?(num2.Normalize());
		}

		// Token: 0x0600F9BA RID: 63930 RVA: 0x00351C57 File Offset: 0x0034FE57
		[LazySemantics]
		public static NumberFormat BuildNumberFormat(uint? minTrailingZeros, uint? maxTrailingZeros, uint? minTrailingZerosAndWhitespace, uint? minLeadingZeros, uint? minLeadingZerosAndWhitespace, NumberFormatDetails details)
		{
			return new NumberFormat(minTrailingZeros.SomeIfNotNull<uint>(), maxTrailingZeros.SomeIfNotNull<uint>(), minTrailingZerosAndWhitespace.SomeIfNotNull<uint>(), minLeadingZeros.SomeIfNotNull<uint>(), minLeadingZerosAndWhitespace.SomeIfNotNull<uint>(), details);
		}

		// Token: 0x0600F9BB RID: 63931 RVA: 0x00351C80 File Offset: 0x0034FE80
		public static ValueSubstring FormatNumber(decimal? number, NumberFormat format)
		{
			return ValueSubstring.Create(Semantics.FormatNumberToString(number, format), null, null, null, null);
		}

		// Token: 0x0600F9BC RID: 63932 RVA: 0x00351CAD File Offset: 0x0034FEAD
		private static string FormatNumberToString(decimal? number, NumberFormat format)
		{
			if (number == null)
			{
				return null;
			}
			return format.ToString(number.Value);
		}

		// Token: 0x0600F9BD RID: 63933 RVA: 0x00351CC8 File Offset: 0x0034FEC8
		public static ValueSubstring FormatNumericRange(decimal? number, NumberFormat format, string delimiter, RoundingSpec lowerRounding, RoundingSpec upperRounding)
		{
			string text = Semantics.FormatNumberToString(Semantics.RoundNumber(number, lowerRounding), format);
			string text2 = Semantics.FormatNumberToString(Semantics.RoundNumber(number, upperRounding), format);
			if (text == null || text2 == null)
			{
				return null;
			}
			return ValueSubstring.Create(text + delimiter + text2, null, null, null, null);
		}

		// Token: 0x0600F9BE RID: 63934 RVA: 0x00351D1C File Offset: 0x0034FF1C
		public static PartialDateTime ParsePartialDateTime(ValueSubstring s, DateTimeFormat[] inputFormats)
		{
			if (s == null)
			{
				return null;
			}
			PartialDateTime partialDateTime = null;
			for (int i = 0; i < inputFormats.Length; i++)
			{
				Optional<DateTimeFormatMatch> optional = DateFormatCache.Parse(inputFormats[i], s);
				if (optional.HasValue)
				{
					PartialDateTime partialDateTime2 = optional.Value.PartialDateTime;
					if (partialDateTime2 != null)
					{
						if (partialDateTime != null)
						{
							if (!partialDateTime.Equals(partialDateTime2))
							{
								return null;
							}
						}
						else
						{
							partialDateTime = partialDateTime2;
						}
					}
				}
			}
			return partialDateTime;
		}

		// Token: 0x0600F9BF RID: 63935 RVA: 0x00351D8C File Offset: 0x0034FF8C
		public static ValueSubstring FormatPartialDateTime(PartialDateTime dt, DateTimeFormat outputFormat)
		{
			string text = Semantics.FormatPartialDateTimeToString(dt, outputFormat);
			IType type = new FormattedPartialDateTimeType(outputFormat);
			return ValueSubstring.Create(text, null, null, type, null);
		}

		// Token: 0x0600F9C0 RID: 63936 RVA: 0x00351DC0 File Offset: 0x0034FFC0
		private static string FormatPartialDateTimeToString(PartialDateTime dt, DateTimeFormat outputFormat)
		{
			if (dt == null)
			{
				return null;
			}
			return outputFormat.ToString(dt);
		}

		// Token: 0x0600F9C1 RID: 63937 RVA: 0x00351DD4 File Offset: 0x0034FFD4
		internal static DateTimePart? GetLargestStandardDateTimePart(PartialDateTime dt)
		{
			foreach (DateTimePart dateTimePart in DateTimePartList.StandardDateTimeDescending)
			{
				if (dt.Parts.Contains(dateTimePart))
				{
					return new DateTimePart?(dateTimePart);
				}
			}
			return null;
		}

		// Token: 0x0600F9C2 RID: 63938 RVA: 0x00351E40 File Offset: 0x00350040
		internal static DateTimePart? GetSmallestStandardDateTimePart(PartialDateTime dt)
		{
			foreach (DateTimePart dateTimePart in DateTimePartList.StandardDateTimeAscending)
			{
				if (dt.Parts.Contains(dateTimePart))
				{
					return new DateTimePart?(dateTimePart);
				}
			}
			return null;
		}

		// Token: 0x0600F9C3 RID: 63939 RVA: 0x00351EAC File Offset: 0x003500AC
		internal static DateTimePart? GetLargestStandardTimePart(PartialDateTime dt)
		{
			foreach (DateTimePart dateTimePart in DateTimePartList.StandardTimeDescending)
			{
				if (dt.Parts.Contains(dateTimePart))
				{
					return new DateTimePart?(dateTimePart);
				}
			}
			return null;
		}

		// Token: 0x0600F9C4 RID: 63940 RVA: 0x00351F18 File Offset: 0x00350118
		internal static DateTimePart? GetSmallestStandardTimePart(PartialDateTime dt)
		{
			foreach (DateTimePart dateTimePart in DateTimePartList.StandardDateTimeAscending)
			{
				if (dt.Parts.Contains(dateTimePart))
				{
					return new DateTimePart?(dateTimePart);
				}
			}
			return null;
		}

		// Token: 0x0600F9C5 RID: 63941 RVA: 0x00351F84 File Offset: 0x00350184
		internal static DateTimePart GetNextLargerPart(DateTimePart unit)
		{
			switch (unit)
			{
			case DateTimePart.Hour:
				return DateTimePart.Day;
			case DateTimePart.Minute:
				return DateTimePart.Hour;
			case DateTimePart.Second:
				return DateTimePart.Minute;
			case DateTimePart.Millisecond:
				return DateTimePart.Second;
			default:
				throw new NotImplementedException("Unknown next unit: " + unit.ToString());
			}
		}

		// Token: 0x0600F9C6 RID: 63942 RVA: 0x00351FC4 File Offset: 0x003501C4
		internal static DateTimePart GetNextSmallerPart(DateTimePart unit)
		{
			switch (unit)
			{
			case DateTimePart.Day:
				return DateTimePart.Hour;
			case DateTimePart.Hour:
				return DateTimePart.Minute;
			case DateTimePart.Minute:
				return DateTimePart.Second;
			case DateTimePart.Second:
				return DateTimePart.Millisecond;
			default:
				throw new NotImplementedException("Unknown next unit: " + unit.ToString());
			}
		}

		// Token: 0x0600F9C7 RID: 63943 RVA: 0x00352004 File Offset: 0x00350204
		private static DateTimePartSet IncludeSmallerParts(DateTimePartSet parts)
		{
			bool flag = false;
			DateTimePart dateTimePart = DateTimePart.Day;
			do
			{
				dateTimePart = Semantics.GetNextSmallerPart(dateTimePart);
				if (parts.Contains(dateTimePart))
				{
					flag = true;
				}
				else if (flag)
				{
					parts = parts.Set(dateTimePart);
				}
			}
			while (dateTimePart != DateTimePart.Millisecond);
			return parts;
		}

		// Token: 0x0600F9C8 RID: 63944 RVA: 0x0035203C File Offset: 0x0035023C
		internal static PartialDateTime GetPartialDateTimeOnlyTime(long milliseconds, DateTimePartSet? optionalPartSpec = null)
		{
			int[] array = new int[DateTimePartUtil.PartKindCount];
			DateTimePartSet dateTimePartSet = (optionalPartSpec ?? DateTimePartSet.StandardTimeParts).Intersect(DateTimePartSet.StandardTimeParts);
			long num = milliseconds;
			for (DateTimePart dateTimePart = DateTimePart.Millisecond; dateTimePart != DateTimePart.Day; dateTimePart = Semantics.GetNextLargerPart(dateTimePart))
			{
				int num2 = dateTimePart.MaxValue() + 1;
				array[(int)dateTimePart] = (dateTimePartSet.Contains(dateTimePart) ? ((int)(num % (long)num2)) : 0);
				num /= (long)num2;
			}
			return PartialDateTime.Create(new PartialDateTimeData(dateTimePartSet, array)).Value;
		}

		// Token: 0x0600F9C9 RID: 63945 RVA: 0x003520D0 File Offset: 0x003502D0
		internal static long GetMillisecondsForPart(DateTimePart part)
		{
			long num = 1L;
			for (DateTimePart dateTimePart = DateTimePart.Millisecond; dateTimePart != part; dateTimePart = Semantics.GetNextLargerPart(dateTimePart))
			{
				num *= (long)((ulong)(dateTimePart.MaxValue() + 1));
			}
			return num;
		}

		// Token: 0x0600F9CA RID: 63946 RVA: 0x003520FC File Offset: 0x003502FC
		internal static long GetTotalMilliseconds(PartialDateTime dt)
		{
			return (((long)dt.Hour.OrElseDefault<int>() * (long)(DateTimePart.Minute.MaxValue() + 1) + (long)dt.Minute.OrElseDefault<int>()) * (long)(DateTimePart.Second.MaxValue() + 1) + (long)dt.Second.OrElseDefault<int>()) * (long)(DateTimePart.Millisecond.MaxValue() + 1) + (long)dt.Millisecond.OrElseDefault<int>();
		}

		// Token: 0x0600F9CB RID: 63947 RVA: 0x0035215C File Offset: 0x0035035C
		internal static PartialDateTime Add(PartialDateTime datetime, int amount, DateTimePart part)
		{
			long totalMilliseconds = Semantics.GetTotalMilliseconds(datetime);
			long millisecondsForPart = Semantics.GetMillisecondsForPart(part);
			long num = totalMilliseconds + millisecondsForPart * (long)amount;
			if (num < 0L)
			{
				num += Semantics.GetMillisecondsForPart(Semantics.GetNextLargerPart(Semantics.GetLargestStandardTimePart(datetime) ?? DateTimePart.Hour));
			}
			return Semantics.GetPartialDateTimeOnlyTime(num, new DateTimePartSet?(Semantics.IncludeSmallerParts(datetime.Parts)));
		}

		// Token: 0x0600F9CC RID: 63948 RVA: 0x003521C0 File Offset: 0x003503C0
		public static PartialDateTime RoundPartialDateTime(PartialDateTime dateTime, DateTimeRoundingSpec roundingSpecification)
		{
			if (dateTime == null)
			{
				return null;
			}
			PartialDateTime zero = roundingSpecification.Zero;
			int delta = (int)roundingSpecification.Delta;
			DateTimePart unit = roundingSpecification.Unit;
			if (unit != DateTimePart.Year)
			{
				long totalMilliseconds = Semantics.GetTotalMilliseconds(dateTime);
				long millisecondsForPart = Semantics.GetMillisecondsForPart(unit);
				long totalMilliseconds2 = Semantics.GetTotalMilliseconds(zero);
				long num = (long)delta * millisecondsForPart;
				DateTimePart dateTimePart = Semantics.GetLargestStandardTimePart(dateTime) ?? DateTimePart.Hour;
				long millisecondsForPart2 = Semantics.GetMillisecondsForPart(Semantics.GetNextLargerPart(dateTimePart));
				long num2 = totalMilliseconds;
				if (num2 < totalMilliseconds2)
				{
					num2 += millisecondsForPart2;
				}
				num2 = (num2 - totalMilliseconds2) / num * num + totalMilliseconds2;
				DateTimePartSet dateTimePartSet = Semantics.IncludeSmallerParts(new DateTimePartSet(new DateTimePart[] { dateTimePart }));
				PartialDateTime partialDateTimeOnlyTime = Semantics.GetPartialDateTimeOnlyTime(num2, new DateTimePartSet?(dateTimePartSet));
				PartialDateTime partialDateTimeOnlyTime2 = Semantics.GetPartialDateTimeOnlyTime(totalMilliseconds, new DateTimePartSet?(dateTimePartSet));
				PartialDateTime partialDateTime;
				if (roundingSpecification.Mode == RoundingMode.UpOrNext)
				{
					partialDateTime = Semantics.Add(partialDateTimeOnlyTime, delta, unit);
				}
				else if (partialDateTimeOnlyTime == partialDateTimeOnlyTime2)
				{
					partialDateTime = partialDateTimeOnlyTime2;
				}
				else
				{
					RoundingMode roundingMode = roundingSpecification.Mode;
					if (roundingMode != RoundingMode.Down)
					{
						if (roundingMode != RoundingMode.Up)
						{
							throw new NotImplementedException("Unknown rounding mode: " + roundingSpecification.Mode.ToString());
						}
						partialDateTime = Semantics.Add(partialDateTimeOnlyTime, delta, unit);
					}
					else
					{
						partialDateTime = partialDateTimeOnlyTime;
					}
				}
				if (roundingSpecification.UpperExcludePart != null)
				{
					RoundingMode roundingMode = roundingSpecification.Mode;
					if (roundingMode - RoundingMode.Up > 1)
					{
						throw new NotImplementedException("Unexpected rounding mode with exclusion: " + roundingSpecification.Mode.ToString());
					}
					partialDateTime = Semantics.Add(partialDateTime, (int)(-(int)roundingSpecification.UpperExcludeAmount), roundingSpecification.UpperExcludePart.Value);
				}
				return partialDateTime;
			}
			if (!dateTime.Contains(DateTimePart.Year))
			{
				return null;
			}
			float value = (float)dateTime.Year.Value;
			int delta2 = (int)roundingSpecification.Delta;
			float num3 = value / roundingSpecification.Delta;
			int num4;
			switch (roundingSpecification.Mode)
			{
			case RoundingMode.Nearest:
				num4 = (int)Math.Floor((double)num3 + 0.5) * delta2;
				break;
			case RoundingMode.Down:
				num4 = (int)Math.Floor((double)num3) * delta2;
				break;
			case RoundingMode.Up:
				num4 = (int)Math.Ceiling((double)num3) * delta2;
				break;
			case RoundingMode.UpOrNext:
				num4 = ((int)Math.Floor((double)num3) + 1) * delta2;
				break;
			default:
				throw new NotImplementedException(string.Format("Unknown rounding mode: {0}", roundingSpecification.Mode));
			}
			return PartialDateTime.Create(PartialDateTimeData.Empty.With(DateTimePart.Year, num4).Value).OrElse(null);
		}

		// Token: 0x0600F9CD RID: 63949 RVA: 0x0035244C File Offset: 0x0035064C
		public static ValueSubstring FormatDateTimeRange(PartialDateTime datetime, DateTimeFormat format, string delimiter, DateTimeRoundingSpec lowerRounding, DateTimeRoundingSpec upperRounding)
		{
			string text = Semantics.FormatPartialDateTimeToString(Semantics.RoundPartialDateTime(datetime, lowerRounding), format);
			string text2 = Semantics.FormatPartialDateTimeToString(Semantics.RoundPartialDateTime(datetime, upperRounding), format);
			if (text == null || text2 == null)
			{
				return null;
			}
			return ValueSubstring.Create(text + delimiter + text2, null, null, null, null);
		}

		// Token: 0x0600F9CE RID: 63950 RVA: 0x003524A0 File Offset: 0x003506A0
		[LazySemantics]
		public static ValueSubstring Lookup(ValueSubstring x, IReadOnlyDictionary<Optional<string>, string> lookupDictionary)
		{
			string text;
			if (!lookupDictionary.TryGetValue(((x != null) ? x.Value : null).SomeIfNotNull<string>(), out text))
			{
				return null;
			}
			return ValueSubstring.Create(text, null, null, null, null);
		}

		// Token: 0x04005C88 RID: 23688
		internal const string GeneralNumberTokenName = "GeneralNumber";

		// Token: 0x04005C89 RID: 23689
		public static readonly IReadOnlyDictionary<string, Token> Tokens;

		// Token: 0x04005C8A RID: 23690
		private static readonly char[] Digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
	}
}
