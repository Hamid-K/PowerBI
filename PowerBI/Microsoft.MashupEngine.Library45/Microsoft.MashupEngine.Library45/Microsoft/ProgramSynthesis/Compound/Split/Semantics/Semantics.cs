using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split.Semantics
{
	// Token: 0x02000988 RID: 2440
	public static class Semantics
	{
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x00092C94 File Offset: 0x00090E94
		public static IReadOnlyDictionary<string, Token> Tokens
		{
			get
			{
				return Token.NonDisjunctiveTokens;
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000B3A51 File Offset: 0x000B1C51
		public static ITable<StringRegion> TableFromCells(IEnumerable<SplitCell[]> cellsList, bool hasHeader)
		{
			return Semantics.TableFromRecords(cellsList.Select((SplitCell[] row) => (from cell in row
				where !cell.IsDelimiter
				select cell.CellValue).ToArray<StringRegion>()), hasHeader);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x000B3A80 File Offset: 0x000B1C80
		private static ITable<StringRegion> TableFromRecords(IEnumerable<StringRegion[]> rows, bool hasHeader)
		{
			rows = rows.Memoize(2);
			IEnumerable<string> enumerable;
			if (!hasHeader)
			{
				enumerable = null;
			}
			else
			{
				StringRegion[] array = rows.FirstOrDefault<StringRegion[]>();
				if (array == null)
				{
					enumerable = null;
				}
				else
				{
					enumerable = array.Select((StringRegion col) => ((col != null) ? col.Value.Trim(Semantics.ColumnNameTrimChars.ToArray<char>()) : null) ?? string.Empty).ToArray<string>();
				}
			}
			return new Table<StringRegion>(enumerable, rows.Skip((hasHeader > false) ? 1 : 0), null);
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x000B3AE4 File Offset: 0x000B1CE4
		public static ITable<StringRegion> SelectColumns(int[] columnList, ITable<StringRegion> table)
		{
			Semantics.<>c__DisplayClass7_0 CS$<>8__locals1 = new Semantics.<>c__DisplayClass7_0();
			CS$<>8__locals1.columnList = columnList;
			return new Table<StringRegion>(CS$<>8__locals1.<SelectColumns>g__Filter|0<string>(table.ColumnNames.ToArray<string>()), table.Rows.Select((IEnumerable<StringRegion> row) => base.<SelectColumns>g__Filter|0<StringRegion>(row.ToArray<StringRegion>())), null);
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x000B3B2C File Offset: 0x000B1D2C
		public static IEnumerable<StringRegion> SplitFile(StringRegion v)
		{
			return v.SplitLines(null);
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x000B3B48 File Offset: 0x000B1D48
		public static IEnumerable<StringRegion> MergeRecordLines(IEnumerable<IEnumerable<StringRegion>> recordLines)
		{
			foreach (IEnumerable<StringRegion> enumerable in recordLines)
			{
				string text = string.Join("", enumerable.Select((StringRegion l) => l.Value));
				int num = text.Length;
				if (num > 0 && text[num - 1] == '\n')
				{
					num--;
					if (num > 0 && text[num - 1] == '\r')
					{
						num--;
					}
				}
				yield return new StringRegion(text.Substring(0, num), Semantics.Tokens);
			}
			IEnumerator<IEnumerable<StringRegion>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003A7E RID: 14974 RVA: 0x000B3B58 File Offset: 0x000B1D58
		public static ITable<StringRegion> NoSplit(IEnumerable<StringRegion> records, bool hasHeader)
		{
			return Semantics.TableFromRecords(records.Select((StringRegion r) => new StringRegion[] { r }), hasHeader);
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x000B3B88 File Offset: 0x000B1D88
		public static ITable<StringRegion> RecordsToTable(IEnumerable<StringRegion> records, bool hasHeader, IReadOnlyList<int> selectColumns, SplitProgram splitTextProgram = null)
		{
			ITable<StringRegion> table = ((splitTextProgram == null) ? Semantics.NoSplit(records, hasHeader) : Semantics.TableFromCells(records.Select(new Func<StringRegion, SplitCell[]>(splitTextProgram.Run)), hasHeader));
			if (selectColumns != null)
			{
				table = Semantics.SelectColumns(selectColumns.ToArray<int>(), table);
			}
			return table;
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x000B3BD2 File Offset: 0x000B1DD2
		public static ITable<StringRegion> MultiRecordSplit(IEnumerable<List<MultiRecordMatch?>> matches)
		{
			IEnumerable<string> enumerable = new string[0];
			Func<List<MultiRecordMatch?>, IEnumerable<StringRegion>> func;
			if ((func = Semantics.<>O.<0>__GetRow) == null)
			{
				func = (Semantics.<>O.<0>__GetRow = new Func<List<MultiRecordMatch?>, IEnumerable<StringRegion>>(Semantics.GetRow));
			}
			return new Table<StringRegion>(enumerable, matches.Select(func), null);
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x000B3C04 File Offset: 0x000B1E04
		private static IEnumerable<StringRegion> GetRow(List<MultiRecordMatch?> matchList)
		{
			List<MultiRecordMatch?> list = new List<MultiRecordMatch?>();
			using (List<MultiRecordMatch?>.Enumerator enumerator = matchList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MultiRecordMatch? match = enumerator.Current;
					MultiRecordMatch? multiRecordMatch = match;
					if (multiRecordMatch != null && list.Any((MultiRecordMatch? fm) => match.Value.OverlapsWith(fm)))
					{
						multiRecordMatch = null;
					}
					list.Add(multiRecordMatch);
				}
			}
			return list.Select(delegate(MultiRecordMatch? m)
			{
				if (m == null)
				{
					return null;
				}
				return m.GetValueOrDefault().Value;
			}).ToArray<StringRegion>();
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000B3CBC File Offset: 0x000B1EBC
		public static List<MultiRecordMatch?> Empty()
		{
			return new List<MultiRecordMatch?>();
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x000B3CC4 File Offset: 0x000B1EC4
		public static List<MultiRecordMatch?> SelectorList(Optional<MultiRecordMatch> match, List<MultiRecordMatch?> subList)
		{
			subList.Insert(0, match.HasValue ? new MultiRecordMatch?(match.Value) : null);
			return subList;
		}

		// Token: 0x06003A84 RID: 14980 RVA: 0x000B3CFC File Offset: 0x000B1EFC
		public static Optional<MultiRecordMatch> KthLine(int k, StringRegion[] rowRecords)
		{
			if (k > rowRecords.Length)
			{
				return Optional<MultiRecordMatch>.Nothing;
			}
			StringRegion stringRegion = rowRecords[k - 1];
			return new MultiRecordMatch((uint)(k - 1), (uint)(k - 1), stringRegion.Start, stringRegion.End, stringRegion).Some<MultiRecordMatch>();
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000B3D38 File Offset: 0x000B1F38
		private static Record<uint, StringRegion>? KeySepMatch(string key, string sep, StringRegion record)
		{
			string value = record.Value;
			int num = value.IndexOf(key, StringComparison.Ordinal);
			if (num < 0)
			{
				return null;
			}
			if (!Semantics.IsKeyValWhiteSpace(value.Substring(0, num)))
			{
				return null;
			}
			int num2 = ((sep == null) ? (num + key.Length) : value.IndexOf(sep, num + key.Length, StringComparison.Ordinal));
			int num3 = ((sep != null) ? sep.Length : 1);
			if (num2 < 0)
			{
				return null;
			}
			if (!Semantics.IsKeyValWhiteSpace(value.Substring(num + key.Length, num2 - num - key.Length)))
			{
				return null;
			}
			uint num4 = record.Start + (uint)num;
			uint num5 = record.Start + (uint)num2 + (uint)num3;
			return new Record<uint, StringRegion>?(Record.Create<uint, StringRegion>(num4, record.Slice(num5, record.End)));
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000B3E0E File Offset: 0x000B200E
		private static bool IsKeyValWhiteSpace(string s)
		{
			return string.IsNullOrEmpty((s != null) ? s.Trim((char[])Semantics.KeyValueTrimChars) : null);
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x000B3E2C File Offset: 0x000B202C
		internal static bool IsQuotedValue(string value)
		{
			int num = value.IndexOf('"');
			if (num < 0 || !Semantics.IsKeyValWhiteSpace(value.Substring(0, num)))
			{
				return false;
			}
			int num2 = value.LastIndexOf('"');
			return num2 != num && Semantics.IsKeyValWhiteSpace(value.Substring(num2 + 1));
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000B3E78 File Offset: 0x000B2078
		public static Optional<MultiRecordMatch> KthKeyValue(string key, string sep, int k, StringRegion[] rowRecords)
		{
			uint num = 0U;
			foreach (StringRegion stringRegion in rowRecords)
			{
				num += 1U;
				Record<uint, StringRegion>? record = Semantics.KeySepMatch(key, sep, stringRegion);
				if (record != null && --k <= 0)
				{
					uint num2;
					StringRegion stringRegion2;
					record.Value.Deconstruct(out num2, out stringRegion2);
					return new MultiRecordMatch(num - 1U, num - 1U, num2, stringRegion2.End, stringRegion2).Some<MultiRecordMatch>();
				}
			}
			return Optional<MultiRecordMatch>.Nothing;
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000B3EEC File Offset: 0x000B20EC
		public static Optional<MultiRecordMatch> KthTwoLineKeyValue(string key, string sep, int k, StringRegion[] rowRecords)
		{
			uint num = 0U;
			uint? num2 = null;
			foreach (StringRegion stringRegion in rowRecords)
			{
				num += 1U;
				if (num2 != null)
				{
					if (--k <= 0)
					{
						return new MultiRecordMatch(num - 2U, num - 1U, num2.Value, stringRegion.End, stringRegion).Some<MultiRecordMatch>();
					}
					num2 = null;
				}
				else
				{
					Record<uint, StringRegion>? record = Semantics.KeySepMatch(key, sep, stringRegion);
					if (record != null && Semantics.IsKeyValWhiteSpace(record.Value.Item2.Value))
					{
						num2 = new uint?(record.Value.Item1);
					}
				}
			}
			return Optional<MultiRecordMatch>.Nothing;
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x000B3FA4 File Offset: 0x000B21A4
		private static Optional<MultiRecordMatch> CreateMultiLineMatch(uint recordIndex, uint startMatch, string newLineSep, IReadOnlyCollection<StringRegion> values)
		{
			string text = (string.IsNullOrEmpty(newLineSep) ? Environment.NewLine : newLineSep);
			StringRegion stringRegion = values.Last<StringRegion>();
			StringRegion stringRegion2 = new StringRegion(string.Join(text, values.Select((StringRegion sr) => sr.Value)), Semantics.Tokens);
			return new MultiRecordMatch((uint)((ulong)recordIndex - (ulong)((long)values.Count) - 1UL), recordIndex - 2U, startMatch, stringRegion.End, stringRegion2).Some<MultiRecordMatch>();
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x000B4020 File Offset: 0x000B2220
		public static Optional<MultiRecordMatch> KthKeyQuote(string key, int k, string newLineSep, StringRegion[] rowRecords)
		{
			uint num = 0U;
			uint? num2 = null;
			List<StringRegion> list = new List<StringRegion>();
			foreach (StringRegion stringRegion in rowRecords)
			{
				num += 1U;
				if (num2 != null)
				{
					if (!Semantics.IsQuotedValue(stringRegion.Value))
					{
						return Semantics.CreateMultiLineMatch(num, num2.Value, newLineSep, list);
					}
					list.Add(stringRegion);
				}
				else
				{
					Record<uint, StringRegion>? record = Semantics.KeySepMatch(key, null, stringRegion);
					if (record != null)
					{
						uint num3;
						StringRegion stringRegion2;
						record.Value.Deconstruct(out num3, out stringRegion2);
						if (Semantics.IsQuotedValue(stringRegion2.Value) && --k <= 0)
						{
							num2 = new uint?(num3);
							list.Add(stringRegion2);
						}
					}
				}
			}
			if (num2 == null)
			{
				return Optional<MultiRecordMatch>.Nothing;
			}
			return Semantics.CreateMultiLineMatch(num + 1U, num2.Value, newLineSep, list);
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000B4100 File Offset: 0x000B2300
		private static Record<uint, uint, StringRegion>? KeyValueFwMatch(string key, int fwPos, StringRegion record)
		{
			string value = record.Value;
			int num = value.IndexOf(key, StringComparison.Ordinal);
			if (num < 0 || num + key.Length + fwPos >= value.Length)
			{
				return null;
			}
			if (!Semantics.IsKeyValWhiteSpace(value.Substring(0, num)))
			{
				return null;
			}
			if (!Semantics.IsKeyValWhiteSpace(value.Substring(num + key.Length, fwPos)))
			{
				return null;
			}
			uint num2 = record.Start + (uint)num;
			uint num3 = num2 + (uint)key.Length + (uint)fwPos;
			return new Record<uint, uint, StringRegion>?(Record.Create<uint, uint, StringRegion>(num2, num3, record.Slice(num3, record.End)));
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000B41A1 File Offset: 0x000B23A1
		private static bool IsKeyValueFwValue(uint startPos, StringRegion record)
		{
			return startPos >= record.Start && startPos <= record.End && Semantics.IsKeyValWhiteSpace(record.Slice(record.Start, startPos).Value);
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x000B41D0 File Offset: 0x000B23D0
		public static Optional<MultiRecordMatch> KthKeyValueFw(string key, int fwPos, int k, string newLineSep, StringRegion[] rowRecord)
		{
			uint num = 0U;
			uint? num2 = null;
			uint num3 = 0U;
			List<StringRegion> list = new List<StringRegion>();
			foreach (StringRegion stringRegion in rowRecord)
			{
				num += 1U;
				if (num2 != null)
				{
					if (!Semantics.IsKeyValueFwValue(num3, stringRegion))
					{
						return Semantics.CreateMultiLineMatch(num, num2.Value, newLineSep, list);
					}
					list.Add(stringRegion);
				}
				else
				{
					Record<uint, uint, StringRegion>? record = Semantics.KeyValueFwMatch(key, fwPos, stringRegion);
					if (record != null && --k <= 0)
					{
						uint num4;
						uint num5;
						StringRegion stringRegion2;
						record.Value.Deconstruct(out num4, out num5, out stringRegion2);
						num2 = new uint?(num4);
						num3 = num5;
						list.Add(stringRegion2);
					}
				}
			}
			if (num2 == null)
			{
				return Optional<MultiRecordMatch>.Nothing;
			}
			return Semantics.CreateMultiLineMatch(num + 1U, num2.Value, newLineSep, list);
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x000B42A7 File Offset: 0x000B24A7
		public static IEnumerable<StringRegion[]> BreakLine(IEnumerable<StringRegion> records)
		{
			string text = null;
			List<StringRegion> rowRecords = new List<StringRegion>();
			bool first = true;
			foreach (StringRegion record in records)
			{
				string value = record.Source;
				if (Semantics.IsKeyValWhiteSpace(text) && !Semantics.IsKeyValWhiteSpace(value))
				{
					if (first)
					{
						first = false;
					}
					else
					{
						yield return rowRecords.ToArray();
					}
					rowRecords.Clear();
				}
				rowRecords.Add(record);
				text = value;
				value = null;
				record = null;
			}
			IEnumerator<StringRegion> enumerator = null;
			if (!first && rowRecords.Count > 0)
			{
				yield return rowRecords.ToArray();
			}
			yield break;
			yield break;
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x000B42B7 File Offset: 0x000B24B7
		public static IEnumerable<StringRegion[]> KeyValue(string key, string sep, IEnumerable<StringRegion> records)
		{
			List<StringRegion> rowRecords = new List<StringRegion>();
			bool first = true;
			foreach (StringRegion record in records)
			{
				if (Semantics.KeySepMatch(key, sep, record) != null)
				{
					if (first)
					{
						first = false;
					}
					else
					{
						yield return rowRecords.ToArray();
					}
					rowRecords.Clear();
				}
				rowRecords.Add(record);
				record = null;
			}
			IEnumerator<StringRegion> enumerator = null;
			yield return rowRecords.ToArray();
			yield break;
			yield break;
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000B42D5 File Offset: 0x000B24D5
		public static IEnumerable<StringRegion[]> TwoLineKeyValue(string key, string sep, IEnumerable<StringRegion> records)
		{
			List<StringRegion> rowRecords = new List<StringRegion>();
			bool first = true;
			foreach (StringRegion record in records)
			{
				Record<uint, StringRegion>? record2 = Semantics.KeySepMatch(key, sep, record);
				if (record2 != null && Semantics.IsKeyValWhiteSpace(record2.Value.Item2.Value))
				{
					if (first)
					{
						first = false;
					}
					else
					{
						yield return rowRecords.ToArray();
					}
					rowRecords.Clear();
				}
				rowRecords.Add(record);
				record = null;
			}
			IEnumerator<StringRegion> enumerator = null;
			yield return rowRecords.ToArray();
			yield break;
			yield break;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000B42F3 File Offset: 0x000B24F3
		public static IEnumerable<StringRegion[]> KeyQuote(string key, IEnumerable<StringRegion> records)
		{
			List<StringRegion> rowRecords = new List<StringRegion>();
			bool first = true;
			foreach (StringRegion record in records)
			{
				Record<uint, StringRegion>? record2 = Semantics.KeySepMatch(key, null, record);
				if (record2 != null && Semantics.IsQuotedValue(record2.Value.Item2.Value))
				{
					if (first)
					{
						first = false;
					}
					else
					{
						yield return rowRecords.ToArray();
					}
					rowRecords.Clear();
				}
				rowRecords.Add(record);
				record = null;
			}
			IEnumerator<StringRegion> enumerator = null;
			yield return rowRecords.ToArray();
			yield break;
			yield break;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000B430A File Offset: 0x000B250A
		public static IEnumerable<IEnumerable<StringRegion>> SplitSequence(RegularExpression r, IEnumerable<StringRegion> lines)
		{
			List<StringRegion> list = null;
			foreach (StringRegion line in lines)
			{
				if (list == null)
				{
					if (Semantics.StartsWith(line, r))
					{
						list = new List<StringRegion> { line };
					}
				}
				else
				{
					if (Semantics.StartsWith(line, r))
					{
						yield return list;
						list = new List<StringRegion>();
					}
					list.Add(line);
					line = null;
				}
			}
			IEnumerator<StringRegion> enumerator = null;
			if (list != null && list.Count > 0)
			{
				yield return list;
			}
			yield break;
			yield break;
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000B4321 File Offset: 0x000B2521
		public static IEnumerable<IEnumerable<StringRegion>> Sequence(IEnumerable<StringRegion> lines)
		{
			return lines.Select((StringRegion line) => new StringRegion[] { line });
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000B4348 File Offset: 0x000B2548
		public static IEnumerable<StringRegion> Skip(int k, Optional<int> headerIndex, IEnumerable<StringRegion> allLines)
		{
			if (!headerIndex.HasValue)
			{
				return allLines.Skip(k);
			}
			return allLines.Where((StringRegion _, int i) => i >= k || i == headerIndex.Value);
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000B4395 File Offset: 0x000B2595
		public static IEnumerable<StringRegion> SkipFooter(int k, IEnumerable<StringRegion> allLines)
		{
			Queue<StringRegion> buffer = new Queue<StringRegion>(k + 1);
			foreach (StringRegion stringRegion in allLines)
			{
				buffer.Enqueue(stringRegion);
				if (buffer.Count > k)
				{
					yield return buffer.Dequeue();
				}
			}
			IEnumerator<StringRegion> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000B43AC File Offset: 0x000B25AC
		public static bool StartsWith(StringRegion line, RegularExpression r)
		{
			return r.MatchesAt(line, line.Start);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000B43BB File Offset: 0x000B25BB
		public static IEnumerable<StringRegion> FilterRecords(bool skipEmpty, Optional<string> delimiter, Optional<string> commentStr, bool hasCommentHeader, IEnumerable<StringRegion> records)
		{
			foreach (StringRegion stringRegion in records)
			{
				string text = stringRegion.Source.Trim((char[])Semantics.WhiteSpaceChars);
				if (!skipEmpty || !Semantics.IsEmptyRecord(stringRegion.Source, text, delimiter))
				{
					if (commentStr.HasValue && Semantics.IsCommentRecord(text, commentStr.Value))
					{
						if (!hasCommentHeader)
						{
							continue;
						}
						hasCommentHeader = false;
					}
					yield return stringRegion;
				}
			}
			IEnumerator<StringRegion> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000B43E8 File Offset: 0x000B25E8
		internal static bool IsEmptyRecord(string record, string trimmedRecord, Optional<string> delimiter)
		{
			return string.IsNullOrEmpty(trimmedRecord) && (!delimiter.HasValue || !record.Contains(delimiter.Value));
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000B440F File Offset: 0x000B260F
		internal static bool IsCommentRecord(string trimmedRecord, string commentStr)
		{
			return trimmedRecord.StartsWith(commentStr);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000B4418 File Offset: 0x000B2618
		public static IEnumerable<StringRegion> QuoteRecords(QuotingConfiguration conf, Optional<string> delimiter, IEnumerable<StringRegion> lines)
		{
			string del = (delimiter.HasValue ? delimiter.Value : null);
			RecordStatus recordStatus = RecordStatus.FieldStart;
			StringBuilder builder = new StringBuilder();
			foreach (StringRegion stringRegion in lines)
			{
				bool flag;
				bool flag2;
				recordStatus = Semantics.GetStatusAfter(stringRegion, conf, del, recordStatus, out flag, out flag2);
				builder.Append(stringRegion);
				if (recordStatus == RecordStatus.Error)
				{
					yield break;
				}
				if (recordStatus == RecordStatus.EndRecord)
				{
					yield return new StringRegion(builder.ToString(), Semantics.Tokens);
					builder.Clear();
					recordStatus = RecordStatus.FieldStart;
				}
			}
			IEnumerator<StringRegion> enumerator = null;
			if (recordStatus != RecordStatus.FieldStart)
			{
				yield return new StringRegion(builder.ToString(), Semantics.Tokens);
			}
			yield break;
			yield break;
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000B4438 File Offset: 0x000B2638
		internal static RecordStatus GetStatusAfter(StringRegion region, QuotingConfiguration conf, string delimiter, RecordStatus status, out bool regionHasQuote, out bool regionHasEscape)
		{
			if (conf.Style == QuotingStyle.Adaptive)
			{
				throw new Exception("Adaptive not supported by GetStatusAfter");
			}
			string value = region.Value;
			regionHasQuote = false;
			regionHasEscape = false;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				switch (status)
				{
				case RecordStatus.FieldStart:
					if (Semantics.StartsWithAtIndex(value, i, delimiter))
					{
						i += delimiter.Length - 1;
					}
					else
					{
						char? c2 = conf.EscapeChar;
						int? num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						int num2 = (int)c;
						if ((num.GetValueOrDefault() == num2) & (num != null))
						{
							regionHasEscape = true;
							status = RecordStatus.Escape;
						}
						else
						{
							int num3 = (int)c;
							c2 = conf.QuoteChar;
							num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
							if ((num3 == num.GetValueOrDefault()) & (num != null))
							{
								regionHasQuote = true;
								status = RecordStatus.InQuotedField;
							}
							else if (!char.IsWhiteSpace(c))
							{
								status = RecordStatus.InField;
							}
						}
					}
					break;
				case RecordStatus.FieldEnd:
					if (Semantics.StartsWithAtIndex(value, i, delimiter))
					{
						status = RecordStatus.FieldStart;
						i += delimiter.Length - 1;
					}
					else if (!char.IsWhiteSpace(c))
					{
						return RecordStatus.Error;
					}
					break;
				case RecordStatus.InField:
					if (Semantics.StartsWithAtIndex(value, i, delimiter))
					{
						status = RecordStatus.FieldStart;
						i += delimiter.Length - 1;
					}
					else
					{
						int num4 = (int)c;
						char? c2 = conf.QuoteChar;
						int? num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						if (((num4 == num.GetValueOrDefault()) & (num != null)) && conf.Style == QuotingStyle.Flexible)
						{
							regionHasQuote = true;
							status = RecordStatus.InQuotedField;
						}
						else
						{
							c2 = conf.EscapeChar;
							num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
							int num2 = (int)c;
							if ((num.GetValueOrDefault() == num2) & (num != null))
							{
								regionHasEscape = true;
								status = RecordStatus.Escape;
							}
						}
					}
					break;
				case RecordStatus.InQuotedField:
				{
					int num5 = (int)c;
					char? c2 = conf.QuoteChar;
					int? num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					if ((num5 == num.GetValueOrDefault()) & (num != null))
					{
						if (conf.DoubleQuoteEscape && i < value.Length - 1)
						{
							int num6 = (int)value[i + 1];
							c2 = conf.QuoteChar;
							num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
							if ((num6 == num.GetValueOrDefault()) & (num != null))
							{
								i++;
								break;
							}
						}
						regionHasQuote = true;
						status = ((conf.Style == QuotingStyle.Flexible) ? RecordStatus.InField : RecordStatus.FieldEnd);
					}
					else
					{
						c2 = conf.EscapeChar;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						int num2 = (int)c;
						if ((num.GetValueOrDefault() == num2) & (num != null))
						{
							regionHasEscape = true;
							status = RecordStatus.EscapeInQuotedField;
						}
					}
					break;
				}
				case RecordStatus.Escape:
					if (i < value.Length - 1 || (c != '\r' && c != '\n'))
					{
						status = RecordStatus.InField;
					}
					break;
				case RecordStatus.EscapeInQuotedField:
					if (i < value.Length - 1 || (c != '\r' && c != '\n'))
					{
						status = RecordStatus.InQuotedField;
					}
					break;
				}
			}
			if (status == RecordStatus.FieldStart || status == RecordStatus.FieldEnd || status == RecordStatus.InField)
			{
				status = RecordStatus.EndRecord;
			}
			else if (status == RecordStatus.Escape)
			{
				status = RecordStatus.InField;
			}
			else if (status == RecordStatus.EscapeInQuotedField)
			{
				status = RecordStatus.InQuotedField;
			}
			return status;
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000B47B8 File Offset: 0x000B29B8
		private static bool StartsWithAtIndex(string s1, int i, string s2)
		{
			if (s2 == null)
			{
				return false;
			}
			int num = 0;
			while (i < s1.Length && num < s2.Length)
			{
				if (s1[i] != s2[num])
				{
					return false;
				}
				i++;
				num++;
			}
			return num == s2.Length;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000B4804 File Offset: 0x000B2A04
		public static IEnumerable<StringRegion> SplitLines(this StringRegion input, int? readLineCount = null)
		{
			List<StringRegion> list = new List<StringRegion>();
			int i = 0;
			int num = 0;
			string source = input.Source;
			int length = source.Length;
			while (i < length)
			{
				char c = source[i];
				if (c == '\r' || c == '\n')
				{
					int num2 = num;
					num = i + 1;
					if (c == '\r' && num < length && source[num] == '\n')
					{
						i++;
						num++;
					}
					if (readLineCount != null)
					{
						int count = list.Count;
						int? num3 = readLineCount;
						if ((count == num3.GetValueOrDefault()) & (num3 != null))
						{
							break;
						}
					}
					list.Add(new StringRegion(source.Substring(num2, i - num2 + 1), input.Cache.StaticTokens));
				}
				i++;
			}
			if (i > num)
			{
				if (readLineCount != null)
				{
					int count2 = list.Count;
					int? num3 = readLineCount;
					if (!((count2 < num3.GetValueOrDefault()) & (num3 != null)))
					{
						return list;
					}
				}
				list.Add(new StringRegion(source.Substring(num, i - num), input.Cache.StaticTokens));
			}
			return list;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000B4908 File Offset: 0x000B2B08
		public static IEnumerable<StringRegion> SplitLines(TextReader inputReader)
		{
			StringBuilder sb = new StringBuilder();
			for (;;)
			{
				int num = inputReader.Read();
				if (num == -1)
				{
					break;
				}
				sb.Append((char)num);
				if (num == 13 || num == 10)
				{
					if (num == 13 && inputReader.Peek() == 10)
					{
						num = inputReader.Read();
						sb.Append((char)num);
					}
					yield return new StringRegion(sb.ToString(), Semantics.Tokens);
					sb.Clear();
				}
			}
			if (sb.Length > 0)
			{
				yield return new StringRegion(sb.ToString(), Semantics.Tokens);
			}
			yield break;
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000B4918 File Offset: 0x000B2B18
		public static ITable<T> WithCleanedColumnNames<T>(this ITable<T> table, ColumnNamesCleaning columnNameCleaning = ColumnNamesCleaning.AsciiAlphaNumeric, IReadOnlyList<int> selectedColumns = null)
		{
			return new Table<T>(Semantics.CleanColumnNames(table.ColumnNames, 0, columnNameCleaning, selectedColumns), table.Rows, null);
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000B4934 File Offset: 0x000B2B34
		public static string[] CleanColumnNames(IEnumerable<string> columnNames, int columnCount = 0, ColumnNamesCleaning columnNameCleaning = ColumnNamesCleaning.AsciiAlphaNumeric, IReadOnlyList<int> selectedColumns = null)
		{
			IEnumerable<string> enumerable = columnNames;
			if (columnNames == null)
			{
				enumerable = from i in Enumerable.Range(1, columnCount)
					select string.Empty;
			}
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>();
			int num = 1;
			foreach (string text in enumerable)
			{
				string text2;
				if (columnNameCleaning != ColumnNamesCleaning.UnicodeAlphaNumeric)
				{
					if (columnNameCleaning != ColumnNamesCleaning.AsciiAlphaNumeric)
					{
						text2 = text;
					}
					else
					{
						text2 = Semantics.ToValidIdentifier(text);
					}
				}
				else
				{
					text2 = string.Concat<char>(text.Where((char ch) => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)));
				}
				if (string.IsNullOrWhiteSpace(text2))
				{
					if (selectedColumns != null)
					{
						num = selectedColumns[list.Count] + 1;
					}
					text2 = string.Format("column{0}", num);
				}
				string text3 = text2.ToLower();
				num++;
				int num2 = 1;
				string text4 = text2;
				while (hashSet.Contains(text3))
				{
					string text5 = (char.IsDigit(text4[text4.Length - 1]) ? "_" : string.Empty);
					text2 = string.Format("{0}{1}{2}", text4, text5, ++num2);
					text3 = text2.ToLower();
				}
				hashSet.Add(text3);
				list.Add(text2);
			}
			return list.ToArray();
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000B4ABC File Offset: 0x000B2CBC
		private static bool IsAsciiDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000B4ACD File Offset: 0x000B2CCD
		private static bool IsAsciiLetter(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000B4AEC File Offset: 0x000B2CEC
		private static string ToValidIdentifier(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(name.Length + 1);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				bool flag5 = Semantics.IsAsciiDigit(c);
				bool flag6 = Semantics.IsAsciiLetter(c);
				bool flag7 = i + 1 < name.Length && (Semantics.IsAsciiDigit(name[i + 1]) || Semantics.IsAsciiLetter(name[i + 1]));
				if (flag5 || flag6)
				{
					if (!flag2 && flag5)
					{
						stringBuilder.Append('_');
					}
					flag2 = true;
					flag3 = flag3 || flag6;
					stringBuilder.Append(c);
					flag = false;
				}
				else
				{
					bool flag8 = char.IsLetter(c);
					flag4 = flag4 || flag8;
					if (!flag && ((flag2 && flag7) || flag8))
					{
						stringBuilder.Append('_');
						flag = true;
					}
				}
			}
			if (!flag3 && flag4)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000B4BEC File Offset: 0x000B2DEC
		public static ITable<string> WithCleanCells(this ITable<StringRegion> table, QuotingConfiguration? conf)
		{
			return new Table<string>(table.ColumnNames, table.Rows.Select((IEnumerable<StringRegion> row) => Semantics.CleanRow(row.Select(delegate(StringRegion cell)
			{
				if (cell == null)
				{
					return null;
				}
				return cell.Value;
			}), conf)), null);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000B4C2C File Offset: 0x000B2E2C
		public static IEnumerable<string> CleanRow(IEnumerable<string> row, QuotingConfiguration? conf)
		{
			if (conf == null || conf.Value.Style != QuotingStyle.Adaptive)
			{
				return row.Select((string cell) => Semantics.CleanCell(cell, conf)).ToList<string>();
			}
			return Semantics.CleanRowAdaptive(row);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000B4C8C File Offset: 0x000B2E8C
		private static string CleanCell(string cell, QuotingConfiguration? quoteConf)
		{
			if (cell == null)
			{
				return string.Empty;
			}
			if (quoteConf == null)
			{
				return cell.Trim((char[])Semantics.WhiteSpaceChars);
			}
			QuotingConfiguration value = quoteConf.Value;
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			bool flag5 = false;
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < cell.Length)
			{
				char c = cell[i];
				if (flag5)
				{
					flag5 = false;
					goto IL_01EE;
				}
				int num = (int)c;
				char? c2 = value.EscapeChar;
				int? num2 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				if ((num == num2.GetValueOrDefault()) & (num2 != null))
				{
					flag5 = true;
				}
				else if (flag2)
				{
					int num3 = (int)c;
					c2 = value.QuoteChar;
					num2 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					if (!((num3 == num2.GetValueOrDefault()) & (num2 != null)))
					{
						goto IL_01EE;
					}
					if (value.DoubleQuoteEscape && i < cell.Length - 1)
					{
						int num4 = (int)cell[i + 1];
						c2 = value.QuoteChar;
						num2 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						if ((num4 == num2.GetValueOrDefault()) & (num2 != null))
						{
							i++;
							goto IL_01EE;
						}
					}
					if (value.Style == QuotingStyle.Standard)
					{
						return stringBuilder.ToString();
					}
					flag3 = true;
					flag2 = false;
					goto IL_01EE;
				}
				else
				{
					int num5 = (int)c;
					c2 = value.QuoteChar;
					num2 = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					if ((num5 == num2.GetValueOrDefault()) & (num2 != null))
					{
						if (!flag4 && value.Style != QuotingStyle.Flexible)
						{
							goto IL_01EE;
						}
						flag = flag && flag4;
						flag2 = true;
						flag4 = false;
						if (value.Style != QuotingStyle.Standard)
						{
							goto IL_01EE;
						}
					}
					else if (!flag4 || !Semantics.IsWhiteSpace(c))
					{
						if (flag3 && !Semantics.IsWhiteSpace(c))
						{
							flag = false;
							goto IL_01EE;
						}
						goto IL_01EE;
					}
				}
				IL_01FB:
				i++;
				continue;
				IL_01EE:
				flag4 = false;
				stringBuilder.Append(c);
				goto IL_01FB;
			}
			string text = stringBuilder.ToString();
			if (flag && flag3)
			{
				text = text.TrimEnd(Array.Empty<char>());
				text = text.Substring(1, text.Length - 2);
			}
			return text;
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000B4ED8 File Offset: 0x000B30D8
		private static IEnumerable<string> CleanRowAdaptive(IEnumerable<string> row)
		{
			List<string> list = row.ToList<string>();
			IEnumerable<string> enumerable;
			if ((enumerable = Semantics.TryCleanRowAdaptive(list, true, true)) == null && (enumerable = Semantics.TryCleanRowAdaptive(list, true, false)) == null)
			{
				enumerable = Semantics.TryCleanRowAdaptive(list, false, false) ?? list;
			}
			return enumerable;
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000B4F14 File Offset: 0x000B3114
		private static IEnumerable<string> TryCleanRowAdaptive(IReadOnlyList<string> row, bool doubleQuoteEscape, bool backSlashEscape)
		{
			string[] array = new string[row.Count];
			for (int i = 0; i < row.Count; i++)
			{
				string text;
				if (!Semantics.TryCleanCellAdaptive(row[i], doubleQuoteEscape, backSlashEscape, out text))
				{
					return null;
				}
				array[i] = text;
			}
			return array;
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x000B4F58 File Offset: 0x000B3158
		private static bool TryCleanCellAdaptive(string s, bool doubleQuoteEscape, bool backSlashEscape, out string result)
		{
			result = string.Empty;
			if (s == null)
			{
				return true;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = true;
			int i = 0;
			while (i < s.Length)
			{
				if (flag4 && !Semantics.IsWhiteSpace(s[i]))
				{
					flag5 = false;
				}
				if (flag2)
				{
					flag2 = false;
					goto IL_00A8;
				}
				if (backSlashEscape && s[i] == '\\')
				{
					flag2 = true;
					flag3 = false;
				}
				else
				{
					if (s[i] != '"')
					{
						goto IL_00A8;
					}
					if (!flag)
					{
						flag5 = flag5 && flag3;
						flag = true;
						goto IL_00A8;
					}
					int num = i + 1;
					if (num >= s.Length || s[num] != '"')
					{
						flag4 = true;
						flag = false;
						goto IL_00A8;
					}
					if (!doubleQuoteEscape)
					{
						return false;
					}
					stringBuilder.Append('"');
					i++;
				}
				IL_00C8:
				i++;
				continue;
				IL_00A8:
				if (!Semantics.IsWhiteSpace(s[i]))
				{
					flag3 = false;
				}
				stringBuilder.Append(s[i]);
				goto IL_00C8;
			}
			if (flag)
			{
				return false;
			}
			result = stringBuilder.ToString().Trim((char[])Semantics.WhiteSpaceChars);
			if (flag5 && flag4)
			{
				result = result.Substring(1, result.Length - 2);
			}
			return true;
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x000B5077 File Offset: 0x000B3277
		private static bool IsWhiteSpace(char c)
		{
			return Semantics.WhiteSpaceChars.Contains(c);
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x000B5084 File Offset: 0x000B3284
		internal static IReadOnlyList<IReadOnlyList<StringRegion>> SplitIntoLines(IEnumerable<StringRegion> additionalInputs, int lineNumLimit, int lineLengthLimit, out bool linesTrimmed)
		{
			linesTrimmed = false;
			IEnumerable<List<StringRegion>> enumerable = additionalInputs.Select((StringRegion input) => input.SplitLines(new int?(lineNumLimit)).ToList<StringRegion>());
			List<List<StringRegion>> list = new List<List<StringRegion>>();
			foreach (List<StringRegion> list2 in enumerable)
			{
				for (int i = 0; i < list2.Count; i++)
				{
					if ((ulong)list2[i].Length > (ulong)((long)lineLengthLimit))
					{
						list2[i] = new StringRegion(list2[i].Source.Substring(0, lineLengthLimit), Semantics.Tokens);
						linesTrimmed = true;
					}
				}
				list.Add(list2);
			}
			return list;
		}

		// Token: 0x04001AA8 RID: 6824
		public static IReadOnlyList<char> ColumnNameTrimChars = new char[] { ' ', '\t', '"' };

		// Token: 0x04001AA9 RID: 6825
		internal static readonly IReadOnlyCollection<char> WhiteSpaceChars = new char[] { ' ', '\t', '\n', '\r' };

		// Token: 0x04001AAA RID: 6826
		internal static readonly IReadOnlyCollection<char> KeyValueTrimChars = new char[] { ' ', '\t', '.' };

		// Token: 0x02000989 RID: 2441
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001AAB RID: 6827
			public static Func<List<MultiRecordMatch?>, IEnumerable<StringRegion>> <0>__GetRow;
		}
	}
}
