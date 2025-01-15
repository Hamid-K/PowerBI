using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;

namespace Microsoft.ProgramSynthesis.Split.Text.Semantics
{
	// Token: 0x0200138D RID: 5005
	public static class Semantics
	{
		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06009B6A RID: 39786 RVA: 0x0020CDAC File Offset: 0x0020AFAC
		public static Dictionary<Regex, Record<bool, Regex, string>> SpecialRegexes { get; }

		// Token: 0x06009B6B RID: 39787 RVA: 0x0020CDB4 File Offset: 0x0020AFB4
		public static List<Record<bool, string, string>> GetSpecialRegexes()
		{
			return new List<Record<bool, string, string>>
			{
				Record.Create<bool, string, string>(true, "(?i)(?<!\\d)((\\d?\\d)(-(\\d?\\d)-|\\/(\\d?\\d)\\/|\\.(\\d?\\d)\\.)(19|20)?\\d\\d|(19|20)?\\d\\d(-(\\d?\\d)-|\\/(\\d?\\d)\\/|\\.(\\d?\\d)\\.)(\\d?\\d)|(\\d\\d(\\/?)(Jan(uary)?|Feb(ruary)?|Mar(ch)?|Apr(il)?|May|Jun(e)?|Jul(y)?|Aug(ust)?|Sep(tember)?|Oct(tober)?|Nov(ember)?|Dec(ember)?)(\\/?)\\d\\d\\d\\d))(?!\\d)", "Date pattern"),
				Record.Create<bool, string, string>(true, "(?<!\\d)([0-2])?\\d:[0-6]\\d(:[0-6]\\d(\\.\\d+)?)((\\s)*([AaPp][Mm])(?![\\p{L}\\p{Nd}]))?", "Time pattern"),
				Record.Create<bool, string, string>(true, "(?<!\\d)([0-2])?\\d:[0-6]\\d((\\s)*([AaPp][Mm])(?![\\p{L}\\p{Nd}]))?", "Simple time pattern (excluding subseconds)"),
				Record.Create<bool, string, string>(true, "(?<![\\d\\.])(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?![\\d\\.])", "IP address pattern"),
				Record.Create<bool, string, string>(true, "\\b(?<![\\p{L}\\p{Nd}./@])[\\p{L}\\p{Nd}._%+-]+@[\\p{L}\\p{Nd}.-]+\\.\\p{L}{2,4}(?![\\p{L}\\p{Nd}./@])\\b", "Email pattern"),
				Record.Create<bool, string, string>(true, "\\b([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})\\b", "MAC pattern"),
				Record.Create<bool, string, string>(true, "(?<![\\.\\d])(((?<![\\.\\d-a-zA-Z])[\\-\\+])?)[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?(?![\\.\\d])", "Decimal number pattern"),
				Record.Create<bool, string, string>(false, "[\\p{Lu}][\\p{L}]+(\\s([\\p{L}]+|\\d+))+", "Phrase pattern"),
				Record.Create<bool, string, string>(false, "\\p{Lu}[\\p{L}]*\\p{Ll}", "Upper camel case pattern"),
				Record.Create<bool, string, string>(true, "(?<![\\p{Ll}./@])(https?:\\/\\/)?\\p{Ll}([\\d\\p{Ll}\\.-]+)\\.([\\p{Ll}\\.]{2,6})([\\/\\w\\.\\?\\-=]*)\\/?(?![\\p{Ll}./@])", "URL pattern"),
				Record.Create<bool, string, string>(true, "[{(]?(?<![\\p{L}\\p{Nd}])[\\p{L}\\p{Nd}]{8}[-]?([\\p{L}\\p{Nd}]{4}[-]?){3}[\\p{L}\\p{Nd}]{12}(?![\\p{L}\\p{Nd}])[)}]?", "GUID pattern"),
				Record.Create<bool, string, string>(true, "(?<![\\d-.])((\\+\\d{1,2}\\s)|\\d-)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}(?![\\d-.])", "Telephone number pattern")
			};
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06009B6C RID: 39788 RVA: 0x00092C94 File Offset: 0x00090E94
		public static IReadOnlyDictionary<string, Token> Tokens
		{
			get
			{
				return Token.NonDisjunctiveTokens;
			}
		}

		// Token: 0x06009B6D RID: 39789 RVA: 0x0020CED0 File Offset: 0x0020B0D0
		static Semantics()
		{
			List<Record<bool, string, string>> specialRegexes = Semantics.GetSpecialRegexes();
			Semantics.SpecialRegexes = new Dictionary<Regex, Record<bool, Regex, string>>();
			foreach (Record<bool, string, string> record in specialRegexes)
			{
				Semantics.SpecialRegexes[new Regex(record.Item2, RegexOptions.Compiled)] = Record.Create<bool, Regex, string>(record.Item1, new Regex("^" + record.Item2 + "$", RegexOptions.Compiled), record.Item3);
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06009B6E RID: 39790 RVA: 0x0020CFB0 File Offset: 0x0020B1B0
		public static double HighFrequencyRatio { get; } = 0.7;

		// Token: 0x06009B6F RID: 39791 RVA: 0x0020CFB8 File Offset: 0x0020B1B8
		public static Record<StringRegion, StringRegion>? Split(StringRegion s, Record<RegularExpression, RegularExpression, RegularExpression> delimiter)
		{
			RegularExpression item = delimiter.Item2;
			RegularExpression item2 = delimiter.Item1;
			RegularExpression item3 = delimiter.Item3;
			PositionMatch[] array = item.Run(s);
			if (array.Length == 0)
			{
				return new Record<StringRegion, StringRegion>?(new Record<StringRegion, StringRegion>(s, null));
			}
			for (int i = 0; i < array.Length; i++)
			{
				uint position = array[i].Position;
				uint num = array[i].Right;
				int num2 = i + 1;
				while (num2 < array.Length && array[num2].Position <= num)
				{
					i++;
					num = array[num2].Right;
					num2++;
				}
				if (item2.LeftMatchesAt(s, position) && item3.MatchesAt(s, num))
				{
					StringRegion stringRegion = s.Slice(s.Start, position);
					StringRegion stringRegion2 = s.Slice(num, s.End);
					return new Record<StringRegion, StringRegion>?(Record.Create<StringRegion, StringRegion>(stringRegion, stringRegion2));
				}
			}
			return new Record<StringRegion, StringRegion>?(new Record<StringRegion, StringRegion>(s, null));
		}

		// Token: 0x06009B70 RID: 39792 RVA: 0x0020D0A6 File Offset: 0x0020B2A6
		public static StringRegion Item1(Record<StringRegion, StringRegion>? pair)
		{
			if (pair == null)
			{
				return null;
			}
			return pair.GetValueOrDefault().Item1;
		}

		// Token: 0x06009B71 RID: 39793 RVA: 0x0020D0BF File Offset: 0x0020B2BF
		public static StringRegion Item2(Record<StringRegion, StringRegion>? pair)
		{
			if (pair == null)
			{
				return null;
			}
			return pair.GetValueOrDefault().Item2;
		}

		// Token: 0x06009B72 RID: 39794 RVA: 0x0020D0D8 File Offset: 0x0020B2D8
		public static IEnumerable<StringRegion> Append(StringRegion first, IEnumerable<StringRegion> output)
		{
			List<StringRegion> list = new List<StringRegion>();
			list.Add(first);
			list.AddRange(output);
			return list;
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x0020D0ED File Offset: 0x0020B2ED
		public static MatchRecord ConstantDelimiter(StringRegion v, string s)
		{
			return Semantics.ConstantDelimiterCommon(v, s, new QuotingConfiguration(new char?('"'), true, new char?('\\'), QuotingStyle.Adaptive));
		}

		// Token: 0x06009B74 RID: 39796 RVA: 0x0020D10B File Offset: 0x0020B30B
		public static MatchRecord ConstantDelimiterWithQuoting(StringRegion v, string s, QuotingConfiguration conf)
		{
			return Semantics.ConstantDelimiterCommon(v, s, conf);
		}

		// Token: 0x06009B75 RID: 39797 RVA: 0x0020D118 File Offset: 0x0020B318
		private static MatchRecord ConstantDelimiterCommon(StringRegion v, string s, QuotingConfiguration conf)
		{
			if (s == null)
			{
				return Semantics.EmptyMatchRecord;
			}
			string text = Regex.Escape(s);
			List<Match> list = Semantics.RegexCache.LookupOrCompute(text, (string pattern) => new Regex(pattern, RegexOptions.Compiled)).NonCachingMatches(v.Value).ToList<Match>();
			List<Record<int, int>> quotedRegions = null;
			if (conf.Style == QuotingStyle.Adaptive)
			{
				quotedRegions = Semantics.GetAdaptiveQuotedRegions(v.Value);
			}
			else if (conf.QuoteChar != null || conf.EscapeChar != null)
			{
				HashSet<int> hashSet = list.Select((Match m) => m.Index).ConvertToHashSet<int>();
				quotedRegions = Semantics.GetQuotedRegions(v.Value, conf, s, hashSet);
				if (quotedRegions == null)
				{
					return Semantics.EmptyMatchRecord;
				}
			}
			IEnumerable<Match> enumerable2;
			if (quotedRegions == null || quotedRegions.Count <= 0)
			{
				IEnumerable<Match> enumerable = list;
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = list.Where((Match m) => !Semantics.InQuotedRegion(quotedRegions, m.Index) && !Semantics.InQuotedRegion(quotedRegions, m.Index + Math.Max(0, m.Length - 1)));
			}
			return new MatchRecord(enumerable2);
		}

		// Token: 0x06009B76 RID: 39798 RVA: 0x0020D240 File Offset: 0x0020B440
		public static MatchRecord FixedWidth(StringRegion v, int[] fieldStartPositions)
		{
			if (fieldStartPositions == null || fieldStartPositions.Any((int p) => p > v.Value.Length || p < 0))
			{
				return Semantics.EmptyMatchRecord;
			}
			return new MatchRecord(fieldStartPositions, fieldStartPositions);
		}

		// Token: 0x06009B77 RID: 39799 RVA: 0x0020D280 File Offset: 0x0020B480
		public static MatchRecord FixedWidthDelimiters(StringRegion v, Record<int, int>[] delimiterPositions)
		{
			if (delimiterPositions == null)
			{
				return Semantics.EmptyMatchRecord;
			}
			List<int> list = new List<int>(delimiterPositions.Length);
			List<int> list2 = new List<int>(delimiterPositions.Length);
			foreach (Record<int, int> record in delimiterPositions)
			{
				int item = record.Item1;
				int item2 = record.Item2;
				if (item < 0 || (long)item2 > (long)((ulong)v.Length) || item > item2)
				{
					return Semantics.EmptyMatchRecord;
				}
				list.Add(item);
				list2.Add(item2);
			}
			return new MatchRecord(list.ToList<int>(), list2.ToList<int>());
		}

		// Token: 0x06009B78 RID: 39800 RVA: 0x0020D30C File Offset: 0x0020B50C
		private static bool InQuotedRegion(List<Record<int, int>> quotedRegions, int position)
		{
			return Semantics.InQuotedRegion(quotedRegions, position, 0, quotedRegions.Count - 1);
		}

		// Token: 0x06009B79 RID: 39801 RVA: 0x0020D320 File Offset: 0x0020B520
		private static bool InQuotedRegion(List<Record<int, int>> quotedRegions, int position, int lowIndex, int highIndex)
		{
			if (quotedRegions.Count == 0 || highIndex < lowIndex)
			{
				return false;
			}
			int num = highIndex - lowIndex;
			if (num <= 1)
			{
				return (quotedRegions[lowIndex].Item1 <= position && quotedRegions[lowIndex].Item2 >= position) || (highIndex > lowIndex && quotedRegions[highIndex].Item1 <= position && quotedRegions[highIndex].Item2 >= position);
			}
			int num2 = lowIndex + num / 2;
			if (quotedRegions[num2].Item1 > position)
			{
				return Semantics.InQuotedRegion(quotedRegions, position, lowIndex, num2 - 1);
			}
			return Semantics.InQuotedRegion(quotedRegions, position, num2, highIndex);
		}

		// Token: 0x06009B7A RID: 39802 RVA: 0x0020D3B4 File Offset: 0x0020B5B4
		public static List<Record<int, int>> GetQuotedRegions(string s, QuotingConfiguration conf, string d, HashSet<int> startPositions)
		{
			if ((conf.QuoteChar != null && d[0] == conf.QuoteChar.Value) || (conf.EscapeChar != null && d[0] == conf.EscapeChar.Value))
			{
				return null;
			}
			List<Record<int, int>> list = new List<Record<int, int>>();
			int num = 0;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (flag4)
				{
					if (startPositions.Contains(i))
					{
						list.Add(Record.Create<int, int>(i, i));
					}
					flag4 = false;
				}
				else
				{
					int num2 = (int)c;
					char? c2 = conf.EscapeChar;
					int? num3 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					if (((num2 == num3.GetValueOrDefault()) & (num3 != null)) && !flag3)
					{
						flag4 = true;
						flag2 = false;
					}
					else if (flag)
					{
						int num4 = (int)c;
						c2 = conf.QuoteChar;
						num3 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						if ((num4 == num3.GetValueOrDefault()) & (num3 != null))
						{
							if (conf.DoubleQuoteEscape && i < s.Length - 1)
							{
								int num5 = (int)s[i + 1];
								c2 = conf.QuoteChar;
								num3 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
								if ((num5 == num3.GetValueOrDefault()) & (num3 != null))
								{
									i++;
									goto IL_025F;
								}
							}
							list.Add(Record.Create<int, int>(num, i));
							flag = false;
							if (conf.Style == QuotingStyle.Standard)
							{
								flag3 = true;
							}
						}
					}
					else if (startPositions.Contains(i))
					{
						flag2 = true;
						flag3 = false;
						i += d.Length - 1;
					}
					else
					{
						int num6 = (int)c;
						c2 = conf.QuoteChar;
						num3 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						if (((num6 == num3.GetValueOrDefault()) & (num3 != null)) && !flag3)
						{
							if (flag2 || conf.Style == QuotingStyle.Flexible)
							{
								flag = true;
								num = i;
							}
							flag2 = false;
						}
						else if (flag2 && !char.IsWhiteSpace(c))
						{
							flag2 = false;
						}
						else if (flag3 && !char.IsWhiteSpace(c))
						{
							return null;
						}
					}
				}
				IL_025F:;
			}
			if (!flag)
			{
				return list;
			}
			return null;
		}

		// Token: 0x06009B7B RID: 39803 RVA: 0x0020D63C File Offset: 0x0020B83C
		private static List<Record<int, int>> GetAdaptiveQuotedRegions(string s)
		{
			List<Record<int, int>> list;
			if (!Semantics.TryGetAdaptiveQuotedRegions(s, true, true, out list) && !Semantics.TryGetAdaptiveQuotedRegions(s, true, false, out list) && !Semantics.TryGetAdaptiveQuotedRegions(s, false, true, out list))
			{
				return null;
			}
			return list;
		}

		// Token: 0x06009B7C RID: 39804 RVA: 0x0020D670 File Offset: 0x0020B870
		private static bool TryGetAdaptiveQuotedRegions(string s, bool doubleQuoteEascape, bool backSlashEscape, out List<Record<int, int>> regions)
		{
			regions = new List<Record<int, int>>();
			bool flag = false;
			bool flag2 = false;
			int num = -1;
			for (int i = 0; i < s.Length; i++)
			{
				if (flag2)
				{
					flag2 = false;
				}
				else if (backSlashEscape && s[i] == '\\')
				{
					flag2 = true;
				}
				else if (s[i] == '"')
				{
					if (flag)
					{
						int num2 = i + 1;
						if (num2 < s.Length && s[num2] == '"')
						{
							if (!doubleQuoteEascape)
							{
								return false;
							}
							i++;
						}
						else
						{
							flag = false;
							int num3 = i;
							regions.Add(new Record<int, int>(num, num3));
						}
					}
					else
					{
						flag = true;
						num = i;
					}
				}
			}
			return !flag;
		}

		// Token: 0x06009B7D RID: 39805 RVA: 0x0016839B File Offset: 0x0016659B
		public static IEnumerable<StringRegion> List(StringRegion v)
		{
			return new StringRegion[] { v };
		}

		// Token: 0x06009B7E RID: 39806 RVA: 0x0020D708 File Offset: 0x0020B908
		public static MatchRecord Empty(StringRegion v)
		{
			return Semantics.ResultCacheForEmpty.LookupOrCompute((int)v.Length, delegate(int key)
			{
				List<int> list = new List<int>(key + 1);
				list.AddRange(Enumerable.Range(0, key + 1));
				MatchRecord matchRecord = new MatchRecord(list, list);
				Semantics.ResultCacheForEmpty.Add((int)v.Length, matchRecord);
				return matchRecord;
			});
		}

		// Token: 0x06009B7F RID: 39807 RVA: 0x0020D744 File Offset: 0x0020B944
		public static MatchRecord ConstStrWithWhitespace(StringRegion v, string s)
		{
			if (s == string.Empty)
			{
				return Semantics.EmptyMatchRecord;
			}
			string text = "\\s*" + Regex.Escape(s) + "\\s*";
			return Semantics.GetMatchRecordForStringRegex(v, text);
		}

		// Token: 0x06009B80 RID: 39808 RVA: 0x0020D784 File Offset: 0x0020B984
		public static MatchRecord ConstAlphStr(StringRegion v, string a)
		{
			string text = "\\s*(?<![A-Za-z])" + Regex.Escape(a) + "(?![A-Za-z])\\s*";
			return Semantics.GetMatchRecordForStringRegex(v, text);
		}

		// Token: 0x06009B81 RID: 39809 RVA: 0x0020D7AE File Offset: 0x0020B9AE
		public static MatchRecord ConstStr(StringRegion v, string s)
		{
			return Semantics.GetMatchRecordForString(v, s);
		}

		// Token: 0x06009B82 RID: 39810 RVA: 0x0020D7B7 File Offset: 0x0020B9B7
		private static IEnumerable<Record<int, int>> GetMatchIndexesForConcat(MatchRecord t, MatchRecord r)
		{
			int i = 0;
			int i2 = 0;
			while (i < t.NumMatches)
			{
				int num = t.StartIndexes[i];
				int num2 = t.EndIndexes[i];
				int num3;
				while (i2 < r.NumMatches && r.StartIndexes[i2] < num2)
				{
					num3 = i2;
					i2 = num3 + 1;
				}
				if (i2 == r.NumMatches)
				{
					break;
				}
				if (r.StartIndexes[i2] == num2)
				{
					yield return Record.Create<int, int>(num, r.EndIndexes[i2]);
				}
				num3 = i;
				i = num3 + 1;
			}
			yield break;
		}

		// Token: 0x06009B83 RID: 39811 RVA: 0x0020D7CE File Offset: 0x0020B9CE
		public static MatchRecord Concat(MatchRecord t, MatchRecord r)
		{
			return new MatchRecord(Semantics.GetMatchIndexesForConcat(t, r), true);
		}

		// Token: 0x06009B84 RID: 39812 RVA: 0x0020D7DD File Offset: 0x0020B9DD
		public static MatchRecord LookBehind(MatchRecord r)
		{
			return new MatchRecord(r.EndIndexes, r.EndIndexes);
		}

		// Token: 0x06009B85 RID: 39813 RVA: 0x0020D7F0 File Offset: 0x0020B9F0
		public static MatchRecord LookAhead(MatchRecord r)
		{
			return new MatchRecord(r.StartIndexes, r.StartIndexes);
		}

		// Token: 0x06009B86 RID: 39814 RVA: 0x0020D803 File Offset: 0x0020BA03
		public static MatchRecord LookAround(MatchRecord r1, MatchRecord delimiterMatches, MatchRecord r2)
		{
			return Semantics.Concat(Semantics.Concat(Semantics.LookBehind(r1), delimiterMatches), Semantics.LookAhead(r2));
		}

		// Token: 0x06009B87 RID: 39815 RVA: 0x0020D81C File Offset: 0x0020BA1C
		public static MatchRecord FieldLookAroundEndPoints(MatchRecord r1, MatchRecord fieldMatches, MatchRecord r2)
		{
			return Semantics.GetEndPointsRecord(Semantics.Concat(Semantics.Concat(Semantics.LookBehind(r1), fieldMatches), Semantics.LookAhead(r2)));
		}

		// Token: 0x06009B88 RID: 39816 RVA: 0x0020D83A File Offset: 0x0020BA3A
		public static MatchRecord FieldEndPoints(MatchRecord fieldMatches)
		{
			return Semantics.GetEndPointsRecord(fieldMatches);
		}

		// Token: 0x06009B89 RID: 39817 RVA: 0x0020D844 File Offset: 0x0020BA44
		private static MatchRecord GetEndPointsRecord(MatchRecord m)
		{
			List<int> list = (from i in m.StartIndexes.Concat(m.EndIndexes).Distinct<int>()
				orderby i
				select i).ToList<int>();
			return new MatchRecord(list, list);
		}

		// Token: 0x06009B8A RID: 39818 RVA: 0x0020D898 File Offset: 0x0020BA98
		public static MatchRecord GetMatchRecord(StringRegion str, Regex regex)
		{
			if (regex == null)
			{
				return Semantics.EmptyMatchRecord;
			}
			return new MatchRecord(regex.NonCachingMatches(str.Value));
		}

		// Token: 0x06009B8B RID: 39819 RVA: 0x0020D8B4 File Offset: 0x0020BAB4
		private static IEnumerable<Record<int, int>> GetMatchesForString(StringRegion str, string s)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException("Length of string must be > 0", "s");
			}
			int end;
			for (uint num = 0U; num < str.Length; num = (uint)end)
			{
				Optional<uint> optional2;
				Optional<uint> optional = (optional2 = str.IndexOfRelative(s, num, StringComparison.Ordinal));
				if (!optional2.HasValue)
				{
					break;
				}
				int value = (int)optional.Value;
				end = value + s.Length;
				yield return Record.Create<int, int>(value, end);
			}
			yield break;
		}

		// Token: 0x06009B8C RID: 39820 RVA: 0x0020D8CB File Offset: 0x0020BACB
		public static MatchRecord GetMatchRecordForString(StringRegion str, string s)
		{
			if (s.Length == 0)
			{
				return Semantics.Empty(str);
			}
			return new MatchRecord(Semantics.GetMatchesForString(str, s), true);
		}

		// Token: 0x06009B8D RID: 39821 RVA: 0x0020D8EC File Offset: 0x0020BAEC
		public static MatchRecord GetMatchRecordForStringRegex(StringRegion str, string regex)
		{
			Regex regex2 = Semantics.RegexCache.LookupOrCompute(regex, (string pattern) => new Regex(pattern, RegexOptions.Compiled));
			return Semantics.GetMatchRecord(str, regex2);
		}

		// Token: 0x06009B8E RID: 39822 RVA: 0x0020D92C File Offset: 0x0020BB2C
		public static MatchRecord SplitMultiple(MatchRecord splitting, MatchRecord d)
		{
			int[] array;
			return MatchRecord.DisjointUnion(splitting, d, out array) ?? Semantics.EmptyMatchRecord;
		}

		// Token: 0x06009B8F RID: 39823 RVA: 0x0020D94B File Offset: 0x0020BB4B
		public static List<MatchRecord> DelimitersList(List<MatchRecord> delimitersList, MatchRecord d)
		{
			return new List<MatchRecord>(delimitersList) { d };
		}

		// Token: 0x06009B90 RID: 39824 RVA: 0x0020D95A File Offset: 0x0020BB5A
		public static List<MatchRecord> EmptyDelimitersList()
		{
			return new List<MatchRecord>();
		}

		// Token: 0x06009B91 RID: 39825 RVA: 0x0020D961 File Offset: 0x0020BB61
		public static Record<int, int, int, int>[] ExtPointsList(Record<int, int, int, int>[] extractionPoints, Record<int, int, int, int>? cndExtPoint)
		{
			return extractionPoints.AppendItem(cndExtPoint.Value).ToArray<Record<int, int, int, int>>();
		}

		// Token: 0x06009B92 RID: 39826 RVA: 0x0020D975 File Offset: 0x0020BB75
		public static Record<int, int, int, int>[] EmptyExtPointsList()
		{
			return new Record<int, int, int, int>[0];
		}

		// Token: 0x06009B93 RID: 39827 RVA: 0x0020D97D File Offset: 0x0020BB7D
		public static bool SpecialCharPattern(StringRegion v, string pattern)
		{
			return pattern.SequenceEqual(v.Value.Where((char c) => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)));
		}

		// Token: 0x06009B94 RID: 39828 RVA: 0x0020D9AF File Offset: 0x0020BBAF
		public static Record<int, int, int, int>? ConditionalExtract(bool pred, Record<int, int, int, int>? extPoint, Record<int, int, int, int>? cndExtPoint)
		{
			if (pred)
			{
				return extPoint;
			}
			return cndExtPoint;
		}

		// Token: 0x06009B95 RID: 39829 RVA: 0x0020D9B8 File Offset: 0x0020BBB8
		public static SplitCell[] ExtractionSplit(StringRegion v, List<MatchRecord> delimitersList, Record<int, int, int, int>[] extractionPoints)
		{
			SplitCell[] array = new SplitCell[extractionPoints.Length];
			for (int i = 0; i < extractionPoints.Length; i++)
			{
				Record<int, int, int, int> record = extractionPoints[i];
				if (record.Item1 == -2)
				{
					array[i] = SplitCell.Empty;
				}
				else
				{
					uint num2;
					if (record.Item1 >= 0)
					{
						MatchRecord matchRecord = delimitersList[record.Item1];
						int num = ((record.Item2 >= 0) ? record.Item2 : (matchRecord.NumMatches + record.Item2));
						if (num < 0 || num >= matchRecord.NumMatches)
						{
							array[i] = SplitCell.Empty;
							goto IL_0124;
						}
						num2 = (uint)matchRecord.EndIndexes[num];
					}
					else
					{
						num2 = 0U;
					}
					uint num4;
					if (record.Item3 >= 0)
					{
						MatchRecord matchRecord2 = delimitersList[record.Item3];
						int num3 = ((record.Item4 >= 0) ? record.Item4 : (matchRecord2.NumMatches + record.Item4));
						if (num3 < 0 || num3 >= matchRecord2.NumMatches)
						{
							array[i] = SplitCell.Empty;
							goto IL_0124;
						}
						num4 = (uint)matchRecord2.StartIndexes[num3];
					}
					else
					{
						num4 = v.Length;
					}
					if (num2 > num4)
					{
						array[i] = SplitCell.Empty;
					}
					else
					{
						array[i] = new SplitCell(v.Slice(num2, num4), false);
					}
				}
				IL_0124:;
			}
			return array;
		}

		// Token: 0x06009B96 RID: 39830 RVA: 0x0020DAF8 File Offset: 0x0020BCF8
		public static SplitCell[] SplitRegion(StringRegion v, MatchRecord splitMatches, int[] ignoreIndexes, int numSplits, bool delimiterStart, bool delimiterEnd, bool includeDelimiters, FillStrategy fillStrategy)
		{
			HashSet<int> hashSet = ignoreIndexes.ConvertToHashSet<int>();
			List<SplitCell> list = new List<SplitCell>();
			uint num = 0U;
			bool flag = true;
			for (int i = 0; i < splitMatches.NumMatches; i++)
			{
				if (!hashSet.Contains(i))
				{
					uint num2 = (uint)splitMatches.StartIndexes[i];
					uint num3 = (uint)splitMatches.EndIndexes[i];
					if (!delimiterStart || i != 0 || num2 != num)
					{
						list.Add(new SplitCell(v.Slice(num, num2), false));
						if (delimiterStart && i == 0)
						{
							flag = false;
						}
					}
					if (includeDelimiters && num2 != num3)
					{
						list.Add(new SplitCell(v.Slice(num2, num3), true));
					}
					num = num3;
				}
			}
			if (!delimiterEnd || v.End != num)
			{
				list.Add(new SplitCell(v.Slice(num, v.End), false));
				if (delimiterEnd)
				{
					flag = false;
				}
			}
			flag = flag && list.Count == numSplits;
			if (flag)
			{
				return list.ToArray();
			}
			SplitCell[] array = new SplitCell[numSplits];
			switch (fillStrategy)
			{
			case FillStrategy.Null:
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = ((j < list.Count) ? new SplitCell(null, list[j].IsDelimiter) : SplitCell.Empty);
				}
				break;
			}
			case FillStrategy.LeftToRight:
			{
				for (int k = 0; k < array.Length; k++)
				{
					if (k < list.Count)
					{
						if (k == array.Length - 1)
						{
							uint start = list[k].CellValue.Start;
							uint end = list[list.Count - 1].CellValue.End;
							array[k] = new SplitCell(v.Slice(start, end), list[k].IsDelimiter);
						}
						else
						{
							array[k] = list[k];
						}
					}
					else
					{
						array[k] = SplitCell.Empty;
					}
				}
				break;
			}
			case FillStrategy.RightToLeft:
			{
				for (int l = 0; l < array.Length; l++)
				{
					int num4 = array.Length - (l + 1);
					if (l < list.Count)
					{
						int num5 = list.Count - (l + 1);
						if (num4 == 0)
						{
							uint start2 = list[0].CellValue.Start;
							uint end2 = list[num5].CellValue.End;
							array[num4] = new SplitCell(v.Slice(start2, end2), list[num5].IsDelimiter);
						}
						else
						{
							array[num4] = list[num5];
						}
					}
					else
					{
						array[num4] = SplitCell.Empty;
					}
				}
				break;
			}
			}
			return array;
		}

		// Token: 0x06009B97 RID: 39831 RVA: 0x0020DD8C File Offset: 0x0020BF8C
		public static MatchRecord RegexMatch(StringRegion v, RegularExpression regex)
		{
			CachedList cachedList;
			v.Cache.TryGetMatchPositionsFor(regex.Tokens[0], out cachedList);
			if (cachedList != null)
			{
				return new MatchRecord(cachedList.Select((PositionMatch m) => Record.Create<int, int>((int)m.Position, (int)(m.Position + m.Length))), true);
			}
			return Semantics.EmptyMatchRecord;
		}

		// Token: 0x06009B98 RID: 39832 RVA: 0x0020DDE4 File Offset: 0x0020BFE4
		public static bool IsFullMatch(Regex r, string s)
		{
			if (r == null || s == null)
			{
				return false;
			}
			Match match = r.Match(s);
			return match.Success && match.Length == s.Length;
		}

		// Token: 0x06009B99 RID: 39833 RVA: 0x0020DE1C File Offset: 0x0020C01C
		public static List<Regex> GetDataTypeRegexes()
		{
			string[] nonDataTypeRegexes = new string[] { "Phrase pattern", "Upper camel case pattern" };
			return (from kvp in Semantics.SpecialRegexes
				where !nonDataTypeRegexes.Contains(kvp.Value.Item3)
				select kvp.Key).ToList<Regex>();
		}

		// Token: 0x06009B9A RID: 39834 RVA: 0x0020DE8A File Offset: 0x0020C08A
		public static MatchRecord FieldMatch(StringRegion v, RegularExpression fregex)
		{
			return Semantics.GetMatchRecord(v, fregex.Regex);
		}

		// Token: 0x06009B9B RID: 39835 RVA: 0x0020DE98 File Offset: 0x0020C098
		public static object GEN_LookAround(object o1, object o2, object o3)
		{
			object[][] array = (object[][])o1;
			object[][] array2 = (object[][])o2;
			object[][] array3 = (object[][])o3;
			List<Record<object[], object[][]>> list = new List<Record<object[], object[][]>>();
			MatchRecord[][] array4 = array.Select((object[] t) => t.Select((object v) => Semantics.LookBehind((MatchRecord)v)).ToArray<MatchRecord>()).ToArray<MatchRecord[]>();
			MatchRecord[][] array5 = array3.Select((object[] t) => t.Select((object v) => Semantics.LookAhead((MatchRecord)v)).ToArray<MatchRecord>()).ToArray<MatchRecord[]>();
			Dictionary<object[], object[]> dictionary = new Dictionary<object[], object[]>();
			for (int i = 0; i < array4.Length; i++)
			{
				Dictionary<object[], object[]> dictionary2 = dictionary;
				object[] array6 = array4[i];
				dictionary2[array6] = array[i];
			}
			Dictionary<object[], object[]> dictionary3 = new Dictionary<object[], object[]>();
			for (int j = 0; j < array5.Length; j++)
			{
				Dictionary<object[], object[]> dictionary4 = dictionary3;
				object[] array6 = array5[j];
				dictionary4[array6] = array3[j];
			}
			List<Record<object[], object[][]>> list2 = (List<Record<object[], object[][]>>)Semantics.GEN_Concat(array4, array2);
			Dictionary<object[], object[][]> dictionary5 = new Dictionary<object[], object[][]>();
			foreach (Record<object[], object[][]> record in list2)
			{
				dictionary5[record.Item1] = record.Item2;
			}
			foreach (Record<object[], object[][]> record2 in ((List<Record<object[], object[][]>>)Semantics.GEN_Concat(list2.Select((Record<object[], object[][]> t) => t.Item1).ToArray<object[]>(), array5)))
			{
				object[] item = record2.Item1;
				object[][] array7 = dictionary5[record2.Item2[0]];
				object[][] array8 = new object[][]
				{
					dictionary[array7[0]],
					array7[1],
					dictionary3[record2.Item2[1]]
				};
				list.Add(Record.Create<object[], object[][]>(item, array8));
			}
			return list;
		}

		// Token: 0x06009B9C RID: 39836 RVA: 0x0020E0A8 File Offset: 0x0020C2A8
		public static object GEN_FieldLookAroundEndPoints(object o1, object o2, object o3)
		{
			object[][] array = (object[][])o1;
			object[][] array2 = (object[][])o2;
			object[][] array3 = (object[][])o3;
			List<Record<object[], object[][]>> list = new List<Record<object[], object[][]>>();
			MatchRecord[][] array4 = array.Select((object[] t) => t.Select((object v) => Semantics.LookBehind((MatchRecord)v)).ToArray<MatchRecord>()).ToArray<MatchRecord[]>();
			MatchRecord[][] array5 = array3.Select((object[] t) => t.Select((object v) => Semantics.LookAhead((MatchRecord)v)).ToArray<MatchRecord>()).ToArray<MatchRecord[]>();
			Dictionary<object[], object[]> dictionary = new Dictionary<object[], object[]>();
			for (int i = 0; i < array4.Length; i++)
			{
				Dictionary<object[], object[]> dictionary2 = dictionary;
				object[] array6 = array4[i];
				dictionary2[array6] = array[i];
			}
			Dictionary<object[], object[]> dictionary3 = new Dictionary<object[], object[]>();
			for (int j = 0; j < array5.Length; j++)
			{
				Dictionary<object[], object[]> dictionary4 = dictionary3;
				object[] array6 = array5[j];
				dictionary4[array6] = array3[j];
			}
			List<Record<object[], object[][]>> list2 = (List<Record<object[], object[][]>>)Semantics.GEN_Concat(array4, array2);
			Dictionary<object[], object[][]> dictionary5 = new Dictionary<object[], object[][]>();
			foreach (Record<object[], object[][]> record in list2)
			{
				dictionary5[record.Item1] = record.Item2;
			}
			foreach (Record<object[], object[][]> record2 in ((List<Record<object[], object[][]>>)Semantics.GEN_Concat(list2.Select((Record<object[], object[][]> t) => t.Item1).ToArray<object[]>(), array5)))
			{
				IEnumerable<MatchRecord> enumerable = record2.Item1.Cast<MatchRecord>();
				Func<MatchRecord, MatchRecord> func;
				if ((func = Semantics.<>O.<0>__GetEndPointsRecord) == null)
				{
					func = (Semantics.<>O.<0>__GetEndPointsRecord = new Func<MatchRecord, MatchRecord>(Semantics.GetEndPointsRecord));
				}
				object[] array7 = enumerable.Select(func).Cast<object>().ToArray<object>();
				object[][] array8 = dictionary5[record2.Item2[0]];
				object[][] array9 = new object[][]
				{
					dictionary[array8[0]],
					array8[1],
					dictionary3[record2.Item2[1]]
				};
				list.Add(Record.Create<object[], object[][]>(array7, array9));
			}
			return list;
		}

		// Token: 0x06009B9D RID: 39837 RVA: 0x0020E2EC File Offset: 0x0020C4EC
		public static object GEN_Concat(object o1, object o2)
		{
			object[][] array = (object[][])o1;
			object[][] array2 = (object[][])o2;
			List<Record<object[], object[][]>> list = new List<Record<object[], object[][]>>();
			if (array.Length == 0)
			{
				return list;
			}
			int num = array[0].Length;
			int highFrequencyBound = (int)((double)num * Semantics.HighFrequencyRatio);
			array2 = array2.Where((object[] t) => t.Count((object v) => ((MatchRecord)v).StartIndexes.Count > 0) >= highFrequencyBound).ToArray<object[]>();
			if (array.Length == 0)
			{
				return list;
			}
			Dictionary<object[], Dictionary<object[], HashSet<Record<int, int>>[]>> dictionary = new Dictionary<object[], Dictionary<object[], HashSet<Record<int, int>>[]>>();
			Dictionary<int, HashSet<Record<object[], int>>>[] array3 = new Dictionary<int, HashSet<Record<object[], int>>>[num];
			for (int i = 0; i < num; i++)
			{
				array3[i] = new Dictionary<int, HashSet<Record<object[], int>>>();
				foreach (object[] array5 in array)
				{
					MatchRecord matchRecord = (MatchRecord)array5[i];
					for (int k = 0; k < matchRecord.EndIndexes.Count; k++)
					{
						int num2 = matchRecord.EndIndexes[k];
						if (!array3[i].ContainsKey(num2))
						{
							array3[i][num2] = new HashSet<Record<object[], int>>();
						}
						array3[i][num2].Add(Record.Create<object[], int>(array5, matchRecord.StartIndexes[k]));
					}
				}
			}
			for (int l = 0; l < num; l++)
			{
				foreach (object[] array6 in array2)
				{
					MatchRecord matchRecord2 = (MatchRecord)array6[l];
					for (int m = 0; m < matchRecord2.StartIndexes.Count; m++)
					{
						int num3 = matchRecord2.StartIndexes[m];
						int num4 = matchRecord2.EndIndexes[m];
						if (array3[l].ContainsKey(num3))
						{
							foreach (Record<object[], int> record in array3[l][num3])
							{
								object[] item = record.Item1;
								int item2 = record.Item2;
								if (!dictionary.ContainsKey(item))
								{
									dictionary[item] = new Dictionary<object[], HashSet<Record<int, int>>[]>();
								}
								if (!dictionary[item].ContainsKey(array6))
								{
									dictionary[item][array6] = new HashSet<Record<int, int>>[num];
									for (int n = 0; n < num; n++)
									{
										dictionary[item][array6][n] = new HashSet<Record<int, int>>();
									}
								}
								dictionary[item][array6][l].Add(Record.Create<int, int>(item2, num4));
							}
						}
					}
				}
			}
			foreach (KeyValuePair<object[], Dictionary<object[], HashSet<Record<int, int>>[]>> keyValuePair in dictionary)
			{
				foreach (KeyValuePair<object[], HashSet<Record<int, int>>[]> keyValuePair2 in keyValuePair.Value)
				{
					if (keyValuePair2.Value.Count((HashSet<Record<int, int>> s) => s.Any<Record<int, int>>()) >= highFrequencyBound)
					{
						object[] array7 = keyValuePair2.Value.Select((HashSet<Record<int, int>> s) => new MatchRecord(s, false)).ToArray<object>();
						list.Add(Record.Create<object[], object[][]>(array7, new object[][] { keyValuePair.Key, keyValuePair2.Key }));
					}
				}
			}
			return list;
		}

		// Token: 0x04003E11 RID: 15889
		private static readonly MatchRecord EmptyMatchRecord = new MatchRecord(new int[0], new int[0]);

		// Token: 0x04003E13 RID: 15891
		public const string DatePatternName = "Date pattern";

		// Token: 0x04003E14 RID: 15892
		public const string TimePatternName = "Time pattern";

		// Token: 0x04003E15 RID: 15893
		public const string SimpleTimePatternName = "Simple time pattern (excluding subseconds)";

		// Token: 0x04003E16 RID: 15894
		public const string IPAddressPatternName = "IP address pattern";

		// Token: 0x04003E17 RID: 15895
		public const string EmailPatternName = "Email pattern";

		// Token: 0x04003E18 RID: 15896
		public const string MACPattern = "MAC pattern";

		// Token: 0x04003E19 RID: 15897
		public const string DecimalNumberPatternName = "Decimal number pattern";

		// Token: 0x04003E1A RID: 15898
		public const string PhrasePatternName = "Phrase pattern";

		// Token: 0x04003E1B RID: 15899
		public const string UpperCamelCasePatternName = "Upper camel case pattern";

		// Token: 0x04003E1C RID: 15900
		public const string URLPatternName = "URL pattern";

		// Token: 0x04003E1D RID: 15901
		public const string GUIDPatternName = "GUID pattern";

		// Token: 0x04003E1E RID: 15902
		public const string TelelphonePatternName = "Telephone number pattern";

		// Token: 0x04003E20 RID: 15904
		private const int EmptyMatchRecordCacheSize = 512;

		// Token: 0x04003E21 RID: 15905
		private static readonly ConcurrentLruCache<int, MatchRecord> ResultCacheForEmpty = new ConcurrentLruCache<int, MatchRecord>(512, null, null, null);

		// Token: 0x04003E22 RID: 15906
		private const int RegexCacheSize = 1024;

		// Token: 0x04003E23 RID: 15907
		private static readonly ConcurrentLruCache<string, Regex> RegexCache = new ConcurrentLruCache<string, Regex>(1024, null, null, null);

		// Token: 0x0200138E RID: 5006
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003E24 RID: 15908
			public static Func<MatchRecord, MatchRecord> <0>__GetEndPointsRecord;
		}
	}
}
