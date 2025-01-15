using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x02000840 RID: 2112
	public static class DateTimeFormatUtil
	{
		// Token: 0x06002DCF RID: 11727 RVA: 0x000829F0 File Offset: 0x00080BF0
		public static string Escape(string constStr)
		{
			if (constStr.Length == 0)
			{
				return "";
			}
			if (constStr.Length == 1)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("\\{0}", new object[] { constStr }));
			}
			if (!constStr.Contains('"'))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { constStr }));
			}
			if (!constStr.Contains('\''))
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("'{0}'", new object[] { constStr }));
			}
			string text = "\\\"";
			IEnumerable<string> enumerable = constStr.Split(new char[] { '"' });
			Func<string, string> func;
			if ((func = DateTimeFormatUtil.<>O.<0>__Escape) == null)
			{
				func = (DateTimeFormatUtil.<>O.<0>__Escape = new Func<string, string>(DateTimeFormatUtil.Escape));
			}
			return string.Join(text, enumerable.Select(func));
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x00082AB4 File Offset: 0x00080CB4
		public static string Unescape(string constantFormat)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char? c = null;
			bool flag = false;
			foreach (char c2 in constantFormat)
			{
				if (flag)
				{
					flag = false;
					stringBuilder.Append(c2);
				}
				else if (c != null && c2 != c.Value)
				{
					stringBuilder.Append(c2);
				}
				else if (c2 == '\'' || c2 == '"')
				{
					if (c != null)
					{
						c = null;
					}
					else
					{
						c = new char?(c2);
					}
				}
				else if (c2 == '\\')
				{
					flag = true;
				}
				else
				{
					if (DateTimeFormatUtil.FormatSpecialCharacters.Contains(c2))
					{
						throw new ArgumentException("Not a constant format (contains non-constant non-escaped char " + c2.ToLiteral(null) + "): " + constantFormat);
					}
					stringBuilder.Append(c2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00082B9E File Offset: 0x00080D9E
		internal static string EscapeForPosix(string constStr)
		{
			return constStr.Replace("%", "%%");
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00082BB0 File Offset: 0x00080DB0
		internal static string EscapeForMomentJS(string constStr)
		{
			return DateTimeFormatUtil.EscapeForJS(constStr, true);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00082BB9 File Offset: 0x00080DB9
		internal static string EscapeForDayJS(string constStr)
		{
			return DateTimeFormatUtil.EscapeForJS(constStr, false);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00082BC4 File Offset: 0x00080DC4
		private static string EscapeForJS(string constStr, bool momentJs)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < constStr.Length)
			{
				int num = i;
				while (num < constStr.Length && char.IsLetterOrDigit(constStr[num]))
				{
					num++;
				}
				if (i == num)
				{
					if (constStr[i] == '[')
					{
						stringBuilder.Append(momentJs ? "\\[" : "[[]");
					}
					else if (constStr[i] == '\\')
					{
						stringBuilder.Append(momentJs ? "[\\\\]" : "\\\\");
					}
					else
					{
						stringBuilder.Append(constStr[i]);
					}
					i++;
				}
				else
				{
					stringBuilder.Append("[" + constStr.Substring(i, num - i) + "]");
					i = num;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00082C8F File Offset: 0x00080E8F
		internal static string EscapeForPowerApps(string constStr)
		{
			if (constStr.IndexOfAny(DateTimeFormatUtil.PowerAppsSpecialCharacters) != -1)
			{
				return null;
			}
			return constStr;
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x00082CA2 File Offset: 0x00080EA2
		public static Optional<DateTimeFormatMatch> Parse(this DateTimeFormat format, StringRegion sr)
		{
			return DateFormatCache.Parse(format, sr);
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x00082CAB File Offset: 0x00080EAB
		public static Optional<DateTimeFormatMatch> Parse(this DateTimeFormat format, string str)
		{
			return DateFormatCache.Parse(format, new StringRegion(str, Token.DateTimeTokens));
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x00082CA2 File Offset: 0x00080EA2
		public static Optional<DateTimeFormatMatch> Parse(this DateTimeFormat format, LearningCacheSubstring ss)
		{
			return DateFormatCache.Parse(format, ss);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00082CC0 File Offset: 0x00080EC0
		public static bool MatchSameStrings(DateTimeFormat a, DateTimeFormat b)
		{
			if (a.IsNumeric != b.IsNumeric)
			{
				return false;
			}
			int num = a.FormatParts.Sum((DateTimeFormatPart fp) => fp.MinimumLength);
			int num2 = a.FormatParts.Sum((DateTimeFormatPart fp) => fp.MaximumLength);
			int num3 = b.FormatParts.Sum((DateTimeFormatPart fp) => fp.MinimumLength);
			int num4 = b.FormatParts.Sum((DateTimeFormatPart fp) => fp.MaximumLength);
			if (num2 < num3 || num4 < num)
			{
				return false;
			}
			if (a.IsNumeric)
			{
				return true;
			}
			if (a.FormatParts.Count != b.FormatParts.Count)
			{
				return false;
			}
			return a.FormatParts.Zip(b.FormatParts, delegate(DateTimeFormatPart x, DateTimeFormatPart y)
			{
				ConstantDateTimeFormatPart constantDateTimeFormatPart = x as ConstantDateTimeFormatPart;
				ConstantDateTimeFormatPart constantDateTimeFormatPart2 = y as ConstantDateTimeFormatPart;
				return (constantDateTimeFormatPart != null && constantDateTimeFormatPart2 != null && constantDateTimeFormatPart.ConstantString.Equals(constantDateTimeFormatPart2.ConstantString)) || (x.IsNumeric && y.IsNumeric) || x.Equals(y);
			}).All((bool z) => z);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x00082E0C File Offset: 0x0008100C
		public static bool IsAmbiguous(DateTimeFormat a, DateTimeFormat b)
		{
			bool flag = DateTimeFormatUtil.MatchSameStrings(a, b);
			bool bothContainPeriod = a.MatchedParts.Contains(DateTimePart.Period) && b.MatchedParts.Contains(DateTimePart.Period);
			bool flag2 = a.FormatParts.Zip(b.FormatParts, delegate(DateTimeFormatPart x, DateTimeFormatPart y)
			{
				Optional<DateTimePart> matchedPart = x.MatchedPart;
				Optional<DateTimePart> matchedPart2 = y.MatchedPart;
				if (matchedPart.Equals(matchedPart2))
				{
					return false;
				}
				if (!bothContainPeriod)
				{
					return true;
				}
				if (!matchedPart.HasValue || !matchedPart2.HasValue)
				{
					return true;
				}
				if (matchedPart.Value == DateTimePart.HourInPeriod)
				{
					return matchedPart2.Value != DateTimePart.Hour;
				}
				return matchedPart.Value != DateTimePart.Hour || matchedPart2.Value != DateTimePart.HourInPeriod;
			}).Any((bool z) => z);
			return flag && flag2;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x00082E94 File Offset: 0x00081094
		public static IReadOnlyList<DateTimeFormat> SimplifyGroup(IReadOnlyList<DateTimeFormat> group)
		{
			HashSet<DateTimeFormat> hashSet = group.ConvertToHashSet<DateTimeFormat>();
			bool flag;
			do
			{
				flag = false;
				foreach (Record<DateTimeFormat, DateTimeFormat> record in hashSet.UnorderedPairs(false))
				{
					DateTimeFormat dateTimeFormat;
					DateTimeFormat dateTimeFormat2;
					record.Deconstruct(out dateTimeFormat, out dateTimeFormat2);
					DateTimeFormat dateTimeFormat3 = dateTimeFormat;
					DateTimeFormat dateTimeFormat4 = dateTimeFormat2;
					if (hashSet.Contains(dateTimeFormat3) && hashSet.Contains(dateTimeFormat4))
					{
						Record<int, DateTimeFormatPart> record2;
						Record<int, DateTimeFormatPart> record3;
						dateTimeFormat3.FormatParts.Enumerate<DateTimeFormatPart>().ZipWith(dateTimeFormat4.FormatParts.Enumerate<DateTimeFormatPart>()).OnlyOrDefault((Record<Record<int, DateTimeFormatPart>, Record<int, DateTimeFormatPart>> pair) => !pair.Item1.Item2.Equals(pair.Item2.Item2))
							.Deconstruct(out record2, out record3);
						int num;
						DateTimeFormatPart dateTimeFormatPart;
						record2.Deconstruct(out num, out dateTimeFormatPart);
						int num2;
						DateTimeFormatPart dateTimeFormatPart2;
						record3.Deconstruct(out num2, out dateTimeFormatPart2);
						int num3 = num;
						DateTimeFormatPart aPart = dateTimeFormatPart;
						DateTimeFormatPart bPart = dateTimeFormatPart2;
						FormatAttributes unionAttributes = null;
						if (aPart is NumericDateTimeFormatPart && bPart is NumericDateTimeFormatPart && !(aPart.MatchedPart != bPart.MatchedPart) && aPart.BaseFormatString[0] == bPart.BaseFormatString[0] && aPart.MaximumLength == bPart.MaximumLength && aPart.MinimumLength != bPart.MinimumLength && (aPart.Attributes == null || aPart.Attributes.TryMerge(bPart.Attributes, out unionAttributes)) && DateTimeFormatPart.AllowLeadingZerosPartChars.Contains(aPart.BaseFormatString[0]) && DateTimeFormatPart.AllowLeadingZerosFormatAttributes.TryMerge(unionAttributes, out unionAttributes))
						{
							flag = true;
							hashSet.Remove(dateTimeFormat3);
							hashSet.Remove(dateTimeFormat4);
							hashSet.Add(new DateTimeFormat(dateTimeFormat3.FormatParts.MutateAt(num3, (DateTimeFormatPart _) => DateTimeFormatPart.Create(new DateTimeFormatPart[] { aPart, bPart }.ArgMin((DateTimeFormatPart p) => p.MinimumLength).BaseFormatString, unionAttributes))));
						}
					}
				}
			}
			while (flag);
			return hashSet.ToList<DateTimeFormat>();
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000830FC File Offset: 0x000812FC
		public static IReadOnlyList<IReadOnlyList<DateTimeFormat>> GroupParsingFormats(IEnumerable<DateTimeFormat> formats)
		{
			List<IReadOnlyList<DateTimeFormat>> list = new List<IReadOnlyList<DateTimeFormat>>();
			List<DateTimeFormat> list2 = formats.ToList<DateTimeFormat>();
			while (list2.Any<DateTimeFormat>())
			{
				List<DateTimeFormat> group = new List<DateTimeFormat>();
				DateTimeFormat groupSeed = list2[0];
				list2.RemoveAll(delegate(DateTimeFormat f)
				{
					if (f != groupSeed && (!DateTimeFormatUtil.MatchSameStrings(groupSeed, f) || DateTimeFormatUtil.IsAmbiguous(groupSeed, f)))
					{
						return false;
					}
					group.Add(f);
					return true;
				});
				list.Add(DateTimeFormatUtil.SimplifyGroup(group));
			}
			return list;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x00083164 File Offset: 0x00081364
		public static DateTimeFormat GetDefaultDateTimeFormat(this DateTimePartSet parts)
		{
			DateTimePartSet dateTimePartSet = parts.Intersect(DateTimePartSet.DateParts);
			DateTimePartSet dateTimePartSet2 = parts.Intersect(DateTimePartSet.TimeParts);
			List<DateTimeFormatPart> list = new List<DateTimeFormatPart>();
			if (dateTimePartSet.Any())
			{
				DateTimePart? dateTimePart = dateTimePartSet.OnlyOrDefault();
				if (dateTimePart != null)
				{
					DateTimePart valueOrDefault = dateTimePart.GetValueOrDefault();
					switch (valueOrDefault)
					{
					case DateTimePart.Year:
						list.Add(DateTimeFormatPart.Create("yyyy", null));
						break;
					case DateTimePart.Month:
						list.Add(DateTimeFormatPart.Create("MMMM", null));
						break;
					case DateTimePart.Day:
						list.Add(DateTimeFormatPart.Create("d", null));
						break;
					default:
						switch (valueOrDefault)
						{
						case DateTimePart.DayOfWeek:
							list.Add(DateTimeFormatPart.Create("dddd", null));
							break;
						case DateTimePart.Quarter:
							list.Add(new ConstantDateTimeFormatPart("Q"));
							list.Add(DateTimeFormatPart.Create("q", null));
							break;
						case DateTimePart.DayOfYear:
							list.Add(DateTimeFormatPart.Create("j", null));
							break;
						default:
							throw new NotImplementedException("Unrecognized date part: " + valueOrDefault.ToString());
						}
						break;
					}
				}
				else
				{
					if (dateTimePartSet.Contains(DateTimePart.DayOfWeek))
					{
						list.Add(DateTimeFormatPart.Create("dddd", null));
						dateTimePartSet = dateTimePartSet.Clear(DateTimePart.DayOfWeek);
						if (dateTimePartSet.Any())
						{
							list.Add(new ConstantDateTimeFormatPart(", "));
						}
					}
					if (dateTimePartSet.Contains(DateTimePart.Quarter))
					{
						list.Add(new ConstantDateTimeFormatPart("Q"));
						list.Add(DateTimeFormatPart.Create("q", null));
						dateTimePartSet = dateTimePartSet.Clear(DateTimePart.Quarter);
						if (dateTimePartSet.Any())
						{
							list.Add(new ConstantDateTimeFormatPart(" "));
						}
					}
					if (dateTimePartSet.Contains(DateTimePart.Year))
					{
						list.Add(DateTimeFormatPart.Create("yyyy", null));
						dateTimePartSet = dateTimePartSet.Clear(DateTimePart.Year);
					}
					if (dateTimePartSet.Any())
					{
						list.Add(new ConstantDateTimeFormatPart("-"));
					}
					if (dateTimePartSet.Contains(DateTimePart.Month))
					{
						list.Add(DateTimeFormatPart.Create("MM", null));
						dateTimePartSet = dateTimePartSet.Clear(DateTimePart.Month);
					}
					else
					{
						list.Add(new ConstantDateTimeFormatPart("??"));
					}
					if (dateTimePartSet.Any())
					{
						list.Add(new ConstantDateTimeFormatPart("-"));
					}
					if (dateTimePartSet.Contains(DateTimePart.Day))
					{
						list.Add(DateTimeFormatPart.Create("dd", null));
						dateTimePartSet = dateTimePartSet.Clear(DateTimePart.Day);
					}
					if (dateTimePartSet.Any())
					{
						throw new NotImplementedException("Unsupported set of DateTimeParts in GetDefaultDateTimeFormat(): " + string.Join(", ", new object[] { parts }));
					}
				}
			}
			if (list.Any<DateTimeFormatPart>() && dateTimePartSet2.Any())
			{
				list.Add(new ConstantDateTimeFormatPart(" "));
			}
			if (dateTimePartSet2.Any())
			{
				DateTimePart? dateTimePart = dateTimePartSet2.OnlyOrDefault();
				if (dateTimePart != null)
				{
					DateTimePart valueOrDefault2 = dateTimePart.GetValueOrDefault();
					switch (valueOrDefault2)
					{
					case DateTimePart.Hour:
						list.Add(DateTimeFormatPart.Create("H", null));
						break;
					case DateTimePart.Minute:
						list.Add(DateTimeFormatPart.Create("m", null));
						break;
					case DateTimePart.Second:
						list.Add(DateTimeFormatPart.Create("s", null));
						break;
					case DateTimePart.Millisecond:
						list.Add(DateTimeFormatPart.Create("fff", null));
						break;
					default:
						throw new NotImplementedException("Unrecognized time part: " + valueOrDefault2.ToString());
					}
				}
				else
				{
					if (dateTimePartSet2.Contains(DateTimePart.Hour))
					{
						list.Add(DateTimeFormatPart.Create("HH", null));
						dateTimePartSet2 = dateTimePartSet2.Clear(DateTimePart.Hour);
					}
					else
					{
						list.Add(new ConstantDateTimeFormatPart("??"));
					}
					if (dateTimePartSet2.Any())
					{
						list.Add(new ConstantDateTimeFormatPart(":"));
					}
					if (dateTimePartSet2.Contains(DateTimePart.Minute))
					{
						list.Add(DateTimeFormatPart.Create("mm", null));
						dateTimePartSet2 = dateTimePartSet2.Clear(DateTimePart.Minute);
					}
					else
					{
						list.Add(new ConstantDateTimeFormatPart("??"));
					}
					if (dateTimePartSet2.Any())
					{
						list.Add(new ConstantDateTimeFormatPart(":"));
					}
					if (dateTimePartSet2.Contains(DateTimePart.Second))
					{
						list.Add(DateTimeFormatPart.Create("ss", null));
						dateTimePartSet2 = dateTimePartSet2.Clear(DateTimePart.Second);
					}
					else
					{
						list.Add(new ConstantDateTimeFormatPart("??"));
					}
					if (dateTimePartSet2.Contains(DateTimePart.Millisecond))
					{
						list.Add(new ConstantDateTimeFormatPart("."));
						list.Add(DateTimeFormatPart.Create("fff", null));
						dateTimePartSet2 = dateTimePartSet2.Clear(DateTimePart.Millisecond);
					}
					if (dateTimePartSet2.Any())
					{
						string text = "Unsupported set of DateTimeParts in GetDefaultDateTimeFormat(): ";
						DateTimePartSet dateTimePartSet3 = parts;
						throw new NotImplementedException(text + dateTimePartSet3.ToString());
					}
				}
			}
			return new DateTimeFormat(list);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0008361C File Offset: 0x0008181C
		public static bool ContainsParsingFormat(this IEnumerable<DateTimeFormat> haystack, DateTimeFormat needle)
		{
			return haystack.Any((DateTimeFormat el) => el.ContainsParsingFormat(needle));
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x00083648 File Offset: 0x00081848
		public static bool ContainsParsingFormat(this DateTimeFormat haystack, DateTimeFormat needle)
		{
			if (haystack.FormatParts.Count != needle.FormatParts.Count)
			{
				return false;
			}
			if (!haystack.Equals(needle))
			{
				return haystack.FormatParts.ZipWith(needle.FormatParts).All(delegate(Record<DateTimeFormatPart, DateTimeFormatPart> partPair)
				{
					DateTimeFormatPart item = partPair.Item1;
					DateTimeFormatPart item2 = partPair.Item2;
					return item.Equals(item2) || (item.BaseFormatString[0] == item2.BaseFormatString[0] && item.AllowsLeadingZeros() && item2.IsNumeric && item2.MaximumLength == item.MaximumLength);
				});
			}
			return true;
		}

		// Token: 0x04001620 RID: 5664
		private static readonly char[] FormatSpecialCharacters = new char[]
		{
			'd', 'f', 'F', 'g', 'h', 'H', 'K', 'm', 'M', 's',
			't', 'y', 'z', ':', '/', '\\', '\'', '"', '%', 'q',
			'j', 'i', 'V', 'Y', 'o', 'Z'
		};

		// Token: 0x04001621 RID: 5665
		private static readonly char[] PowerAppsSpecialCharacters = new char[] { 'm', 'd', 'y', 'h', 's', 'f' };

		// Token: 0x02000841 RID: 2113
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001622 RID: 5666
			public static Func<string, string> <0>__Escape;
		}
	}
}
