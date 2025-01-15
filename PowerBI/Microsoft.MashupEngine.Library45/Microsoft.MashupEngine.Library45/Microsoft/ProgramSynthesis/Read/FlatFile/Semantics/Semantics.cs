using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Semantics
{
	// Token: 0x0200128F RID: 4751
	public static class Semantics
	{
		// Token: 0x06008FCC RID: 36812 RVA: 0x001E32C0 File Offset: 0x001E14C0
		public static ITable<string> Csv(string input, IReadOnlyList<string> columnNames, int skip, int skipFooter, string delimiter, bool filterEmptyLines, Optional<string> commentStr, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuoteEscape)
		{
			return Semantics.Csv(input, columnNames, skip, skipFooter, delimiter, filterEmptyLines, commentStr, quoteChar, escapeChar, doubleQuoteEscape, true);
		}

		// Token: 0x06008FCD RID: 36813 RVA: 0x001E32E3 File Offset: 0x001E14E3
		public static ITable<string> Fw(string input, IReadOnlyList<string> columnNames, int skip, int skipFooter, IReadOnlyList<Record<int, int?>> fieldPositions, bool filterEmptyLines, Optional<string> commentStr)
		{
			return Semantics.Fw(new StringReader(input), columnNames, skip, skipFooter, fieldPositions, filterEmptyLines, commentStr, true);
		}

		// Token: 0x06008FCE RID: 36814 RVA: 0x001E32FC File Offset: 0x001E14FC
		internal static ITable<string> Csv(string input, IReadOnlyList<string> columnNames, int skip, int skipFooter, string delimiter, bool filterEmptyLines, Optional<string> commentStr, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuoteEscape, bool trim)
		{
			return Semantics.Csv(new StringReader(input), columnNames, skip, skipFooter, delimiter, filterEmptyLines, commentStr, quoteChar, escapeChar, doubleQuoteEscape, trim);
		}

		// Token: 0x06008FCF RID: 36815 RVA: 0x001E3325 File Offset: 0x001E1525
		public static ITable<string> Fw(string input, IReadOnlyList<string> columnNames, int skip, int skipFooter, IReadOnlyList<Record<int, int?>> fieldPositions, bool filterEmptyLines, Optional<string> commentStr, bool trim)
		{
			return Semantics.Fw(new StringReader(input), columnNames, skip, skipFooter, fieldPositions, filterEmptyLines, commentStr, trim);
		}

		// Token: 0x06008FD0 RID: 36816 RVA: 0x001E3340 File Offset: 0x001E1540
		internal static ITable<string> Csv(TextReader input, IReadOnlyList<string> columnNames, int skip, int skipFooter, string delimiter, bool filterEmptyLines, Optional<string> commentStr, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuoteEscape, bool trim)
		{
			IEnumerable<IReadOnlyList<string>> enumerable = Semantics.Csv(Semantics.SplitLines(input, false), columnNames.Count.Some<int>(), delimiter, quoteChar, escapeChar, doubleQuoteEscape, trim, true).SkipRows(skip, skipFooter).FilterRows(filterEmptyLines, commentStr, true);
			return new Table<string>(columnNames, enumerable, null);
		}

		// Token: 0x06008FD1 RID: 36817 RVA: 0x001E338C File Offset: 0x001E158C
		internal static ITable<string> Fw(TextReader input, IReadOnlyList<string> columnNames, int skip, int skipFooter, IReadOnlyList<Record<int, int?>> fieldPositions, bool filterEmptyLines, Optional<string> commentStr, bool trim)
		{
			IEnumerable<IReadOnlyList<string>> enumerable = Semantics.Fw(Semantics.SplitLines(input, true).SkipRows(skip, skipFooter), fieldPositions, trim).FilterRows(filterEmptyLines, commentStr, false);
			return new Table<string>(columnNames, enumerable, null);
		}

		// Token: 0x06008FD2 RID: 36818 RVA: 0x001E33C3 File Offset: 0x001E15C3
		private static IEnumerable<T> SkipRows<T>(this IEnumerable<T> rows, int skip, int skipFooter)
		{
			if (skip > 0)
			{
				rows = rows.Skip(skip);
			}
			if (skipFooter > 0)
			{
				rows = rows.DropLast(skipFooter);
			}
			return rows;
		}

		// Token: 0x06008FD3 RID: 36819 RVA: 0x001E33E0 File Offset: 0x001E15E0
		private static IEnumerable<IReadOnlyList<string>> FilterRows(this IEnumerable<IReadOnlyList<string>> rows, bool filterEmptyLines, Optional<string> commentStr, bool isCsv)
		{
			if (filterEmptyLines)
			{
				IEnumerable<IReadOnlyList<string>> enumerable;
				if (!isCsv)
				{
					enumerable = rows.Where((IReadOnlyList<string> row) => !Semantics.IsFwEmptyRow(row));
				}
				else
				{
					enumerable = rows.Where((IReadOnlyList<string> row) => !Semantics.IsCsvEmptyRow(row));
				}
				rows = enumerable;
			}
			if (commentStr.HasValue)
			{
				rows = rows.Where((IReadOnlyList<string> row) => !Semantics.IsCommentRow(row, commentStr.Value));
			}
			return rows;
		}

		// Token: 0x06008FD4 RID: 36820 RVA: 0x001E3470 File Offset: 0x001E1670
		internal static IEnumerable<IReadOnlyList<string>> Csv(IEnumerable<string> lines, Optional<int> columnCount, string delimiter, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuoteEscape, bool trim, bool lastIncomplete)
		{
			Semantics.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.trim = trim;
			CS$<>8__locals1.delimiter = delimiter;
			CS$<>8__locals1.columnCount = columnCount;
			CS$<>8__locals1.field = new StringBuilder();
			CS$<>8__locals1.row = new List<string>();
			Semantics.CsvState state = Semantics.CsvState.FieldStart;
			foreach (string line in lines)
			{
				for (int i = 0; i < line.Length; i++)
				{
					char c = line[i];
					bool flag = CS$<>8__locals1.delimiter.Length > 0;
					switch (state)
					{
					case Semantics.CsvState.FieldStart:
						if (quoteChar.HasValue && c == quoteChar.Value)
						{
							if (!CS$<>8__locals1.trim)
							{
								CS$<>8__locals1.field.Append(c);
							}
							state = Semantics.CsvState.InQuotedField;
						}
						else if (escapeChar.HasValue && c == escapeChar.Value)
						{
							if (!CS$<>8__locals1.trim)
							{
								CS$<>8__locals1.field.Append(c);
							}
							state = Semantics.CsvState.Escape;
						}
						else if (flag && Semantics.StartsAt(line, i, CS$<>8__locals1.delimiter))
						{
							Semantics.<Csv>g__AddField|9_0(ref CS$<>8__locals1);
							i += CS$<>8__locals1.delimiter.Length - 1;
						}
						else if (c == '\r' || c == '\n')
						{
							Semantics.<Csv>g__AddField|9_0(ref CS$<>8__locals1);
							yield return Semantics.<Csv>g__GetRow|9_1(ref CS$<>8__locals1);
							i = line.Length;
						}
						else if (!Semantics.SpaceTrimChars.Contains(c))
						{
							CS$<>8__locals1.field.Append(c);
							state = Semantics.CsvState.InField;
						}
						else if (!CS$<>8__locals1.trim)
						{
							CS$<>8__locals1.field.Append(c);
						}
						break;
					case Semantics.CsvState.InField:
						if (escapeChar.HasValue && c == escapeChar.Value)
						{
							if (!CS$<>8__locals1.trim)
							{
								CS$<>8__locals1.field.Append(c);
							}
							state = Semantics.CsvState.Escape;
						}
						else if (flag && Semantics.StartsAt(line, i, CS$<>8__locals1.delimiter))
						{
							Semantics.<Csv>g__AddField|9_0(ref CS$<>8__locals1);
							i += CS$<>8__locals1.delimiter.Length - 1;
							state = Semantics.CsvState.FieldStart;
						}
						else if (c == '\r' || c == '\n')
						{
							Semantics.<Csv>g__AddField|9_0(ref CS$<>8__locals1);
							yield return Semantics.<Csv>g__GetRow|9_1(ref CS$<>8__locals1);
							state = Semantics.CsvState.FieldStart;
							i = line.Length;
						}
						else
						{
							CS$<>8__locals1.field.Append(c);
						}
						break;
					case Semantics.CsvState.InQuotedField:
						if (escapeChar.HasValue && c == escapeChar.Value)
						{
							if (!CS$<>8__locals1.trim)
							{
								CS$<>8__locals1.field.Append(c);
							}
							state = Semantics.CsvState.EscapeQuoted;
						}
						else if (c == quoteChar.Value)
						{
							if (doubleQuoteEscape && i + 1 < line.Length && line[i + 1] == quoteChar.Value)
							{
								CS$<>8__locals1.field.Append(c);
								if (!CS$<>8__locals1.trim)
								{
									CS$<>8__locals1.field.Append(c);
								}
								i++;
							}
							else
							{
								if (!CS$<>8__locals1.trim)
								{
									CS$<>8__locals1.field.Append(c);
								}
								state = Semantics.CsvState.InField;
							}
						}
						else
						{
							CS$<>8__locals1.field.Append(c);
						}
						break;
					case Semantics.CsvState.Escape:
						CS$<>8__locals1.field.Append(c);
						state = Semantics.CsvState.InField;
						break;
					case Semantics.CsvState.EscapeQuoted:
						CS$<>8__locals1.field.Append(c);
						state = Semantics.CsvState.InQuotedField;
						break;
					}
				}
				line = null;
			}
			IEnumerator<string> enumerator = null;
			if (CS$<>8__locals1.field.Length == 0 && CS$<>8__locals1.row.Count == 0 && state == Semantics.CsvState.FieldStart)
			{
				yield break;
			}
			if (((state != Semantics.CsvState.FieldStart && state != Semantics.CsvState.InField) || (CS$<>8__locals1.columnCount.HasValue && CS$<>8__locals1.columnCount.Value != CS$<>8__locals1.row.Count + 1)) && !lastIncomplete)
			{
				yield break;
			}
			Semantics.<Csv>g__AddField|9_0(ref CS$<>8__locals1);
			yield return Semantics.<Csv>g__GetRow|9_1(ref CS$<>8__locals1);
			yield break;
			yield break;
		}

		// Token: 0x06008FD5 RID: 36821 RVA: 0x001E34C0 File Offset: 0x001E16C0
		private static bool StartsAt(string src, int pos, string s)
		{
			int num = 0;
			while (pos + num < src.Length && num < s.Length)
			{
				if (src[pos + num] != s[num])
				{
					return false;
				}
				num++;
			}
			return num == s.Length;
		}

		// Token: 0x06008FD6 RID: 36822 RVA: 0x001E3506 File Offset: 0x001E1706
		internal static bool IsCsvEmptyRow(IReadOnlyList<string> row)
		{
			if (string.IsNullOrWhiteSpace(row[0]))
			{
				return row.Skip(1).All((string cell) => cell == null);
			}
			return false;
		}

		// Token: 0x06008FD7 RID: 36823 RVA: 0x001E3543 File Offset: 0x001E1743
		internal static bool IsFwEmptyRow(IReadOnlyList<string> row)
		{
			Func<string, bool> func;
			if ((func = Semantics.<>O.<0>__IsNullOrWhiteSpace) == null)
			{
				func = (Semantics.<>O.<0>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
			}
			return row.All(func);
		}

		// Token: 0x06008FD8 RID: 36824 RVA: 0x001E3568 File Offset: 0x001E1768
		internal static bool IsCommentRow(IReadOnlyList<string> row, string commentStr)
		{
			foreach (string text in row)
			{
				if (text == null)
				{
					return false;
				}
				if (text.TrimStart(Array.Empty<char>()).StartsWith(commentStr))
				{
					return true;
				}
				if (!string.IsNullOrEmpty(text))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06008FD9 RID: 36825 RVA: 0x001E35D8 File Offset: 0x001E17D8
		internal static IEnumerable<IReadOnlyList<string>> Fw(IEnumerable<string> lines, IReadOnlyList<Record<int, int?>> fieldPositions, bool trim)
		{
			return lines.Select((string line) => fieldPositions.Select((Record<int, int?> pos) => Semantics.FwSplit(line, pos.Item1, pos.Item2, trim)).ToList<string>());
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x001E360C File Offset: 0x001E180C
		private static string FwSplit(string line, int start, int? end, bool trim)
		{
			if (line.Length < start)
			{
				return null;
			}
			string text;
			if (end != null)
			{
				int? num = end;
				int length = line.Length;
				if (!((num.GetValueOrDefault() > length) & (num != null)))
				{
					text = line.Substring(start, end.Value - start);
					goto IL_004A;
				}
			}
			text = line.Substring(start);
			IL_004A:
			string text2 = text;
			if (!trim)
			{
				return text2;
			}
			return text2.Trim(Semantics.SpaceTrimChars.ToArray<char>());
		}

		// Token: 0x06008FDB RID: 36827 RVA: 0x001E3679 File Offset: 0x001E1879
		public static IEnumerable<string> SplitLines(string s, bool trimEnd = false)
		{
			return Semantics.SplitLines(new StringReader(s), trimEnd);
		}

		// Token: 0x06008FDC RID: 36828 RVA: 0x001E3687 File Offset: 0x001E1887
		public static IEnumerable<string> SplitLines(TextReader reader, bool trimEnd = false)
		{
			StringBuilder sb = new StringBuilder();
			for (;;)
			{
				int num = reader.Read();
				if (num == -1)
				{
					break;
				}
				char c = (char)num;
				if (c == '\r' || c == '\n')
				{
					if (!trimEnd)
					{
						sb.Append(c);
					}
					if (c == '\r' && reader.Peek() == 10)
					{
						c = (char)reader.Read();
						if (!trimEnd)
						{
							sb.Append(c);
						}
					}
					yield return sb.ToString();
					sb.Clear();
				}
				else
				{
					sb.Append(c);
				}
			}
			if (sb.Length > 0)
			{
				yield return sb.ToString();
			}
			yield break;
		}

		// Token: 0x06008FDD RID: 36829 RVA: 0x001E369E File Offset: 0x001E189E
		public static StringRegion CreateStringRegion(string str)
		{
			return Semantics.CreateStringRegion(str);
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x001E36A6 File Offset: 0x001E18A6
		public static ITable<string> StringRegionToStringTable(ITable<StringRegion> table)
		{
			if (table != null)
			{
				return new Table<string>(table.ColumnNames, table.Rows.Select((IEnumerable<StringRegion> row) => row.Select((StringRegion cell) => cell.Value)), null);
			}
			return null;
		}

		// Token: 0x06008FE0 RID: 36832 RVA: 0x001E36FC File Offset: 0x001E18FC
		[CompilerGenerated]
		internal static void <Csv>g__AddField|9_0(ref Semantics.<>c__DisplayClass9_0 A_0)
		{
			string text = A_0.field.ToString();
			A_0.field.Clear();
			if (A_0.trim && A_0.delimiter.Length == 0)
			{
				text = text.Trim(Semantics.SpaceTrimChars);
			}
			A_0.row.Add(text);
		}

		// Token: 0x06008FE1 RID: 36833 RVA: 0x001E3750 File Offset: 0x001E1950
		[CompilerGenerated]
		internal static IReadOnlyList<string> <Csv>g__GetRow|9_1(ref Semantics.<>c__DisplayClass9_0 A_0)
		{
			if (A_0.columnCount.HasValue)
			{
				while (A_0.row.Count < A_0.columnCount.Value)
				{
					A_0.row.Add(null);
				}
				while (A_0.row.Count > A_0.columnCount.Value)
				{
					A_0.row.RemoveAt(A_0.row.Count - 1);
				}
			}
			IReadOnlyList<string> readOnlyList = A_0.row.ToList<string>();
			A_0.row.Clear();
			return readOnlyList;
		}

		// Token: 0x04003A80 RID: 14976
		private static readonly char[] SpaceTrimChars = new char[] { ' ', '\t' };

		// Token: 0x02001290 RID: 4752
		private enum CsvState
		{
			// Token: 0x04003A82 RID: 14978
			FieldStart,
			// Token: 0x04003A83 RID: 14979
			InField,
			// Token: 0x04003A84 RID: 14980
			InQuotedField,
			// Token: 0x04003A85 RID: 14981
			Escape,
			// Token: 0x04003A86 RID: 14982
			EscapeQuoted
		}

		// Token: 0x02001291 RID: 4753
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003A87 RID: 14983
			public static Func<string, bool> <0>__IsNullOrWhiteSpace;
		}
	}
}
