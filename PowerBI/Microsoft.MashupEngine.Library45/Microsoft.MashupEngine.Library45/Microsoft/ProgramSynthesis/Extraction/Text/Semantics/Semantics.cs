using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Semantics
{
	// Token: 0x02000F95 RID: 3989
	public static class Semantics
	{
		// Token: 0x06006E42 RID: 28226 RVA: 0x0016838C File Offset: 0x0016658C
		public static ITable<StringRegion> Table(IReadOnlyList<string> columnNames, IEnumerable<IReadOnlyList<StringRegion>> rows)
		{
			return new Table<StringRegion>(columnNames, rows.ToList<IReadOnlyList<StringRegion>>(), null);
		}

		// Token: 0x06006E43 RID: 28227 RVA: 0x0016839B File Offset: 0x0016659B
		[LazySemantics]
		public static IReadOnlyList<StringRegion> List(StringRegion extract)
		{
			return new StringRegion[] { extract };
		}

		// Token: 0x06006E44 RID: 28228 RVA: 0x001683A7 File Offset: 0x001665A7
		[LazySemantics]
		public static IReadOnlyList<StringRegion> Prepend(StringRegion extractTup, IReadOnlyList<StringRegion> colSplit)
		{
			return colSplit.PrependItem(extractTup).ToList<StringRegion>();
		}

		// Token: 0x06006E45 RID: 28229 RVA: 0x001683B5 File Offset: 0x001665B5
		public static StringRegion First(Record<StringRegion, StringRegion> tup)
		{
			return tup.Item1;
		}

		// Token: 0x06006E46 RID: 28230 RVA: 0x001683BD File Offset: 0x001665BD
		public static StringRegion Second(Record<StringRegion, StringRegion> tup)
		{
			return tup.Item2;
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x001683C5 File Offset: 0x001665C5
		internal static int Position(StringRegion row, int k)
		{
			if (k <= 0)
			{
				return (int)(row.End + (uint)k + 1U);
			}
			return (int)(row.Start + (uint)k - 1U);
		}

		// Token: 0x06006E48 RID: 28232 RVA: 0x001683E0 File Offset: 0x001665E0
		public static StringRegion Slice(StringRegion row, int start, int end)
		{
			if (row == null || start == 0 || end == 0)
			{
				return null;
			}
			int num = Semantics.Position(row, start);
			int num2 = Semantics.Position(row, end);
			if ((long)num < (long)((ulong)row.Start) || (long)num > (long)((ulong)row.End) || (long)num2 < (long)((ulong)row.Start) || (long)num2 > (long)((ulong)row.End) || num > num2)
			{
				return null;
			}
			return row.Slice((uint)num, (uint)num2);
		}

		// Token: 0x06006E49 RID: 28233 RVA: 0x00168448 File Offset: 0x00166648
		public static StringRegion Substring(StringRegion row, int index, int length)
		{
			if (length < 0)
			{
				return null;
			}
			return Semantics.Slice(row, index, index + length);
		}

		// Token: 0x06006E4A RID: 28234 RVA: 0x0016845C File Offset: 0x0016665C
		public static StringRegion BetweenDelimiters(StringRegion row, Optional<string> prefix, Optional<string> suffix)
		{
			StringRegion stringRegion = row;
			if (prefix.HasValue && prefix.Value != null)
			{
				Optional<uint> optional = Semantics.FindIndex(row, prefix.Value, 1);
				if (!optional.HasValue)
				{
					return null;
				}
				stringRegion = stringRegion.Slice((uint)((ulong)optional.Value + (ulong)((long)prefix.Value.Length)), stringRegion.End);
			}
			if (suffix.HasValue && suffix.Value != null)
			{
				Optional<uint> optional2 = Semantics.FindIndex(stringRegion, suffix.Value, 1);
				if (!optional2.HasValue)
				{
					return null;
				}
				stringRegion = stringRegion.Slice(stringRegion.Start, optional2.Value);
			}
			return stringRegion;
		}

		// Token: 0x06006E4B RID: 28235 RVA: 0x001684FC File Offset: 0x001666FC
		public static Record<StringRegion, StringRegion> SplitPosition(StringRegion row, int k)
		{
			if (row == null)
			{
				return default(Record<StringRegion, StringRegion>);
			}
			int num = Semantics.Position(row, k);
			if ((long)num < (long)((ulong)row.Start))
			{
				return new Record<StringRegion, StringRegion>(null, row);
			}
			if ((long)num > (long)((ulong)row.End))
			{
				return new Record<StringRegion, StringRegion>(row, null);
			}
			return new Record<StringRegion, StringRegion>(row.Slice(row.Start, (uint)num), row.Slice((uint)num, row.End));
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x0016856C File Offset: 0x0016676C
		public static Record<StringRegion, StringRegion> SplitDelimiter(StringRegion row, string str, int k)
		{
			Optional<uint> optional = Semantics.FindIndex(row, str, k);
			if (optional.HasValue)
			{
				return new Record<StringRegion, StringRegion>(row.Slice(row.Start, optional.Value), row.Slice(optional.Value + (uint)str.Length, row.End));
			}
			return new Record<StringRegion, StringRegion>(row, null);
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x001685C5 File Offset: 0x001667C5
		internal static IEnumerable<uint> FindAllIndexes(StringRegion s, string str)
		{
			uint len = (uint)str.Length;
			for (uint relPos = s.Start; relPos < s.End; relPos += len)
			{
				Optional<uint> optional = s.PositionOf(str, relPos, StringComparison.Ordinal);
				if (!optional.HasValue)
				{
					yield break;
				}
				relPos = optional.Value;
				yield return relPos;
			}
			yield break;
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x001685DC File Offset: 0x001667DC
		private static Optional<uint> FindIndex(StringRegion s, string str, int k)
		{
			if (k > 0)
			{
				return Semantics.FindAllIndexes(s, str).Skip(k - 1).MaybeFirst<uint>();
			}
			Optional<uint> optional = Optional<uint>.Nothing;
			int num = (int)s.End;
			while ((long)num > (long)((ulong)s.Start))
			{
				optional = s.LastPositionOf(str, (uint)num, StringComparison.Ordinal);
				if (!optional.HasValue || k == -1)
				{
					break;
				}
				k++;
				num = (int)optional.Value;
				num -= str.Length;
			}
			return optional;
		}

		// Token: 0x06006E4F RID: 28239 RVA: 0x0016864C File Offset: 0x0016684C
		public static IReadOnlyList<StringRegion> Group(Regex re, IReadOnlyList<StringRegion> skip)
		{
			return Semantics.MergeUsingPred((int index) => re.IsMatch(skip[index].Value), skip);
		}

		// Token: 0x06006E50 RID: 28240 RVA: 0x00168684 File Offset: 0x00166884
		public static IReadOnlyList<StringRegion> MergeEvery(int k, IReadOnlyList<StringRegion> skip)
		{
			return Semantics.MergeUsingPred((int index) => index % k == 0, skip);
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x001686A4 File Offset: 0x001668A4
		private static IReadOnlyList<StringRegion> MergeUsingPred(Predicate<int> pred, IReadOnlyList<StringRegion> skip)
		{
			List<StringRegion> list = new List<StringRegion>();
			StringRegion wholeRegion = skip[0].WholeRegion;
			int num = 0;
			for (int i = 1; i < skip.Count; i++)
			{
				if (pred(i))
				{
					list.Add(wholeRegion.Slice(skip[num].Start, skip[i - 1].End));
					num = i;
				}
			}
			list.Add(wholeRegion.Slice(skip[num].Start, skip.Last<StringRegion>().End));
			return list;
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x0016872C File Offset: 0x0016692C
		public static IReadOnlyList<StringRegion> Select(Regex re, IReadOnlyList<StringRegion> skip)
		{
			return skip.Where((StringRegion line) => re.IsMatch(line.Value)).ToList<StringRegion>();
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x0016875D File Offset: 0x0016695D
		public static IReadOnlyList<StringRegion> Skip(int k, IReadOnlyList<StringRegion> lines)
		{
			return lines.Skip(k).ToList<StringRegion>();
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x0016876C File Offset: 0x0016696C
		public static IReadOnlyList<StringRegion> SplitLines(StringRegion v)
		{
			List<StringRegion> list = new List<StringRegion>();
			string source = v.Source;
			uint num = 0U;
			uint num2;
			for (num2 = v.Start; num2 < v.End; num2 += 1U)
			{
				char c = source[(int)num2];
				if (c == '\r' || c == '\n')
				{
					list.Add(v.Slice(num, num2));
					if (c == '\r' && num2 + 1U < v.End && source[(int)(num2 + 1U)] == '\n')
					{
						num2 += 1U;
					}
					num = num2 + 1U;
				}
			}
			if (num2 > num)
			{
				list.Add(v.Slice(num, num2));
			}
			return list;
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x001687FC File Offset: 0x001669FC
		public static StringRegion Trim(StringRegion s)
		{
			uint num = s.Start;
			uint num2 = s.End;
			while (num < s.End)
			{
				if (!char.IsWhiteSpace(s.Source[(int)num]))
				{
					break;
				}
				num += 1U;
			}
			while (num2 > num && char.IsWhiteSpace(s.Source[(int)(num2 - 1U)]))
			{
				num2 -= 1U;
			}
			if (num != s.Start || num2 != s.End)
			{
				return s.Slice(num, num2);
			}
			return s;
		}

		// Token: 0x06006E56 RID: 28246 RVA: 0x00168874 File Offset: 0x00166A74
		public static StringRegion CreateStringRegion(string input)
		{
			return new StringRegion(input, null);
		}
	}
}
