using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009A5 RID: 2469
	public static class FwFormatLearner
	{
		// Token: 0x06003B48 RID: 15176 RVA: 0x000B6F20 File Offset: 0x000B5120
		internal static IReadOnlyList<FwColumnFormat> Learn(string formatDescription, Guid? guid)
		{
			List<string> lines = (from line in formatDescription.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
				where FwFormatLearner.NumberRegex.IsMatch(line) || FwFormatLearner.HeaderKeywords.Any(new Func<string, bool>(line.ToLowerInvariant().Contains))
				select line).ToList<string>();
			List<int> list = (from idx in Enumerable.Range(0, Math.Max(lines.Count - FwFormatLearner.MinDetectionRowCount, 0))
				where FwFormatLearner.FirstRowRegex.IsMatch(lines[idx])
				select idx).ToList<int>();
			if (list.Count == 0)
			{
				return null;
			}
			foreach (List<Constraint<StringRegion, SplitCell[]>> list2 in new List<Constraint<StringRegion, SplitCell[]>>[]
			{
				new List<Constraint<StringRegion, SplitCell[]>>
				{
					new FixedWidthConstraint()
				},
				new List<Constraint<StringRegion, SplitCell[]>>
				{
					new SimpleDelimiter()
				},
				new List<Constraint<StringRegion, SplitCell[]>>()
			})
			{
				foreach (int num in list)
				{
					SplitSession splitSession = new SplitSession(null, null, null);
					splitSession.Constraints.Add(list2);
					List<string> list3 = lines.Skip(num).Take(FwFormatLearner.DetectionRowCount).ToList<string>();
					NotifyingCollection<StringRegion> inputs = splitSession.Inputs;
					IEnumerable<string> enumerable = list3;
					Func<string, StringRegion> func;
					if ((func = FwFormatLearner.<>O.<0>__CreateStringRegion) == null)
					{
						func = (FwFormatLearner.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
					}
					inputs.Add(enumerable.Select(func));
					SplitProgram program = splitSession.Learn(null, default(CancellationToken), guid);
					if (!(program == null))
					{
						bool flag = num > 0;
						List<string> list4 = (flag ? lines.Skip(num - 1).ToList<string>() : lines.Skip(num).ToList<string>());
						List<List<string>> list5;
						if (program.Properties.FieldPositions != null)
						{
							List<Record<int, int?>> splitPositions = FwFormatLearner.MergeFields(program.Properties.FieldPositions, list3);
							list5 = list4.Select((string row) => FwFormatLearner.Split(splitPositions, row)).ToList<List<string>>();
						}
						else
						{
							list5 = list4.Select((string row) => (from c in program.Run(SplitSession.CreateStringRegion(row))
								where !c.IsDelimiter
								select c).Select(delegate(SplitCell c)
							{
								StringRegion cellValue = c.CellValue;
								if (cellValue == null)
								{
									return null;
								}
								return cellValue.Value;
							}).ToList<string>()).ToList<List<string>>();
						}
						if (list5.Count != 0 && list5[0].Count >= 2)
						{
							IReadOnlyList<FwColumnFormat> readOnlyList = FwFormatLearner.LearnColumnFormats(list5, flag);
							if (readOnlyList != null)
							{
								return readOnlyList;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000B71D4 File Offset: 0x000B53D4
		private static IReadOnlyList<FwColumnFormat> LearnColumnFormats(List<List<string>> table, bool hasHeader)
		{
			List<string> list = null;
			if (hasHeader)
			{
				list = table[0].Select(delegate(string cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.Trim().ToLowerInvariant();
				}).ToList<string>();
				table.RemoveAt(0);
				if (!list.Any(new Func<string, bool>(FwFormatLearner.HeaderKeywords.Contains)))
				{
					list = null;
				}
			}
			IReadOnlyList<string> readOnlyList = table[0];
			int count = table[0].Count;
			for (int i = 0; i < count; i++)
			{
				if (RangeFormatExtractor.RangeRegex.IsMatch(readOnlyList[i]))
				{
					RangeFormatExtractor rangeFormatExtractor = new RangeFormatExtractor(i, -1, -1, -1);
					if (rangeFormatExtractor.Extract(readOnlyList) != null)
					{
						IReadOnlyList<FwColumnFormat> readOnlyList2 = FwFormatLearner.ValidateAndExecuteExtractor(rangeFormatExtractor, list, table, new int[] { i });
						if (readOnlyList2 != null)
						{
							return readOnlyList2;
						}
					}
				}
			}
			List<IReadOnlyList<FwColumnFormat>> list2 = new List<IReadOnlyList<FwColumnFormat>>();
			for (int j = 0; j < count; j++)
			{
				if (PositionFormatExtractor.NumberRegex.IsMatch(readOnlyList[j]))
				{
					foreach (int num in Enumerable.Range(j + 1, count - j - 1).Concat(Enumerable.Range(0, j)).ToList<int>())
					{
						if (PositionFormatExtractor.NumberRegex.IsMatch(readOnlyList[num]))
						{
							FwFormatExtractor fwFormatExtractor = new StartEndFormatExtractor(j, num, -1, -1, -1);
							if (fwFormatExtractor.Extract(readOnlyList) != null)
							{
								IReadOnlyList<FwColumnFormat> readOnlyList3 = FwFormatLearner.ValidateAndExecuteExtractor(fwFormatExtractor, list, table, new int[] { j, num });
								if (readOnlyList3 != null)
								{
									list2.Add(readOnlyList3);
								}
								else
								{
									fwFormatExtractor = new StartLengthFormatExtractor(j, num, -1, -1, -1);
									if (fwFormatExtractor.Extract(readOnlyList) != null)
									{
										readOnlyList3 = FwFormatLearner.ValidateAndExecuteExtractor(fwFormatExtractor, list, table, new int[] { j, num });
										if (readOnlyList3 != null)
										{
											list2.Add(readOnlyList3);
										}
									}
								}
							}
						}
					}
				}
			}
			return FwFormatLearner.TopFormat(list2);
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000B73D8 File Offset: 0x000B55D8
		private static IReadOnlyList<FwColumnFormat> TopFormat(List<IReadOnlyList<FwColumnFormat>> possibleFormats)
		{
			if (possibleFormats.Count == 0)
			{
				return null;
			}
			IReadOnlyList<FwColumnFormat> readOnlyList = possibleFormats[0];
			double num = FwFormatLearner.<TopFormat>g__CalculateCoverage|17_0(readOnlyList);
			foreach (IReadOnlyList<FwColumnFormat> readOnlyList2 in possibleFormats.Skip(1))
			{
				double num2 = FwFormatLearner.<TopFormat>g__CalculateCoverage|17_0(readOnlyList2);
				if (num2 > num)
				{
					num = num2;
					readOnlyList = readOnlyList2;
				}
			}
			return readOnlyList;
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000B744C File Offset: 0x000B564C
		private static IReadOnlyList<FwColumnFormat> ValidateAndExecuteExtractor(FwFormatExtractor extractor, IReadOnlyList<string> header, IReadOnlyList<IReadOnlyList<string>> rows, int[] usedIndices)
		{
			List<IReadOnlyList<string>> list = rows.Take(FwFormatLearner.DetectionRowCount).ToList<IReadOnlyList<string>>();
			FwColumnFormat fwColumnFormat = extractor.Extract(list[0]);
			int num = 1;
			foreach (IReadOnlyList<string> readOnlyList in list.Skip(1))
			{
				FwColumnFormat fwColumnFormat2 = extractor.Extract(readOnlyList);
				if (fwColumnFormat2 != null)
				{
					FwColumnFormat.ColumnPosition columnPosition = fwColumnFormat2.RelativePosition(fwColumnFormat);
					if (columnPosition == FwColumnFormat.ColumnPosition.After || columnPosition == FwColumnFormat.ColumnPosition.Within)
					{
						num++;
						if (columnPosition == FwColumnFormat.ColumnPosition.After)
						{
							fwColumnFormat = fwColumnFormat2;
						}
					}
				}
			}
			if ((double)num / (double)list.Count < FwFormatLearner.ValidRangeRatio)
			{
				return null;
			}
			FwFormatLearner.UpdateNameTypeDescription(extractor, header, list, usedIndices);
			return FwFormatLearner.HandleNoisesAndSubfields((from format in rows.Select(new Func<IReadOnlyList<string>, FwColumnFormat>(extractor.Extract))
				where format != null
				select format).ToList<FwColumnFormat>());
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000B7540 File Offset: 0x000B5740
		private static void UpdateNameTypeDescription(FwFormatExtractor extractor, IReadOnlyList<string> header, IReadOnlyList<IReadOnlyList<string>> rows, int[] usedIndices)
		{
			HashSet<int> hashSet = usedIndices.ConvertToHashSet<int>();
			int count = rows[0].Count;
			int columnIndex3;
			int num;
			for (columnIndex3 = 0; columnIndex3 < count; columnIndex3 = num + 1)
			{
				if (!hashSet.Contains(columnIndex3))
				{
					if ((double)(from cell in rows.Select(delegate(IReadOnlyList<string> row)
						{
							string text = row[columnIndex3];
							if (text == null)
							{
								return null;
							}
							return text.Trim();
						})
						where string.IsNullOrWhiteSpace(cell) || FwFormatLearner.ColumnNameRegex.IsMatch(cell)
						select cell).Distinct<string>().Count<string>() / (double)rows.Count > FwFormatLearner.NameUniquenessRatio)
					{
						extractor.NameIndex = columnIndex3;
						hashSet.Add(columnIndex3);
						break;
					}
				}
				num = columnIndex3;
			}
			int columnIndex4;
			for (columnIndex4 = 0; columnIndex4 < count; columnIndex4 = num + 1)
			{
				if (!hashSet.Contains(columnIndex4))
				{
					List<string> list = rows.Select(delegate(IReadOnlyList<string> row)
					{
						string text2 = row[columnIndex4];
						if (text2 == null)
						{
							return null;
						}
						return text2.Trim().ToLowerInvariant();
					}).ToList<string>();
					if (list.All((string cell) => string.IsNullOrWhiteSpace(cell) || FwFormatLearner.TypeRegex.IsMatch(cell)))
					{
						if (list.Any((string cell) => FwFormatLearner.Types.Contains(cell)))
						{
							extractor.TypeIndex = columnIndex4;
							hashSet.Add(columnIndex4);
							break;
						}
					}
				}
				num = columnIndex4;
			}
			int columnIndex;
			for (columnIndex = 0; columnIndex < count; columnIndex = num + 1)
			{
				if (!hashSet.Contains(columnIndex) && rows.Average(delegate(IReadOnlyList<string> row)
				{
					string text3 = row[columnIndex];
					if (text3 == null)
					{
						return 0;
					}
					IEnumerable<char> enumerable = text3.Trim();
					Func<char, bool> func;
					if ((func = FwFormatLearner.<>O.<1>__IsWhiteSpace) == null)
					{
						func = (FwFormatLearner.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
					}
					return enumerable.Count(func);
				}) > FwFormatLearner.DescriptionHasWhitespaces)
				{
					extractor.DescriptionIndex = columnIndex;
					hashSet.Add(columnIndex);
					break;
				}
				num = columnIndex;
			}
			if (header == null)
			{
				return;
			}
			if (extractor.NameIndex < 0)
			{
				for (int i = 0; i < header.Count; i++)
				{
					if (!hashSet.Contains(i) && FwFormatLearner.NameKeywords.Contains(header[i]))
					{
						extractor.NameIndex = i;
						hashSet.Add(i);
						break;
					}
				}
			}
			if (extractor.TypeIndex < 0)
			{
				int columnIndex2;
				for (columnIndex2 = 0; columnIndex2 < header.Count; columnIndex2 = num + 1)
				{
					if (!hashSet.Contains(columnIndex2) && FwFormatLearner.TypeKeywords.Contains(header[columnIndex2]))
					{
						if (rows.Select((IReadOnlyList<string> row) => row[columnIndex2]).All((string cell) => string.IsNullOrWhiteSpace(cell) || FwFormatLearner.TypeRegex.IsMatch(cell)))
						{
							extractor.TypeIndex = columnIndex2;
							hashSet.Add(columnIndex2);
							break;
						}
					}
					num = columnIndex2;
				}
			}
			if (extractor.DescriptionIndex < 0)
			{
				for (int j = 0; j < header.Count; j++)
				{
					if (!hashSet.Contains(j) && FwFormatLearner.DescriptionKeywords.Contains(header[j]))
					{
						extractor.DescriptionIndex = j;
						return;
					}
				}
			}
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000B78A4 File Offset: 0x000B5AA4
		private static List<FwColumnFormat> HandleNoisesAndSubfields(IReadOnlyList<FwColumnFormat> columnFormats)
		{
			List<FwColumnFormat> list = columnFormats.ToList<FwColumnFormat>();
			for (int i = list.Count - 1; i >= 1; i--)
			{
				FwColumnFormat.ColumnPosition columnPosition = list[i].RelativePosition(list[i - 1]);
				if (columnPosition == FwColumnFormat.ColumnPosition.After || columnPosition == FwColumnFormat.ColumnPosition.Within)
				{
					break;
				}
				list.RemoveAt(i);
			}
			List<FwColumnFormat> list2 = new List<FwColumnFormat> { list.First<FwColumnFormat>() };
			List<FwColumnFormat> list3 = new List<FwColumnFormat>();
			bool flag = false;
			for (int j = 1; j < list.Count; j++)
			{
				FwColumnFormat fwColumnFormat = list2.Last<FwColumnFormat>();
				FwColumnFormat fwColumnFormat2 = list[j];
				FwColumnFormat.ColumnPosition columnPosition2 = fwColumnFormat2.RelativePosition(fwColumnFormat);
				if (flag)
				{
					if (columnPosition2 == FwColumnFormat.ColumnPosition.Within)
					{
						if (fwColumnFormat2.RelativePosition(list3.Last<FwColumnFormat>()) != FwColumnFormat.ColumnPosition.After)
						{
							return null;
						}
						list3.Add(fwColumnFormat2);
					}
					else
					{
						if (columnPosition2 != FwColumnFormat.ColumnPosition.After)
						{
							return null;
						}
						list2.RemoveAt(list2.Count - 1);
						list2.AddRange(list3);
						list2.Add(fwColumnFormat2);
						list3.Clear();
						flag = false;
					}
				}
				else if (columnPosition2 == FwColumnFormat.ColumnPosition.After)
				{
					list2.Add(fwColumnFormat2);
				}
				else if (columnPosition2 == FwColumnFormat.ColumnPosition.Within)
				{
					flag = true;
					list3.Add(fwColumnFormat2);
				}
				else if (columnPosition2 != FwColumnFormat.ColumnPosition.Equal)
				{
					return null;
				}
			}
			if (list2.Count >= FwFormatLearner.MinColumnCount)
			{
				return list2;
			}
			return null;
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		private static List<string> Split(IEnumerable<Record<int, int?>> splitPositions, string line)
		{
			return splitPositions.Select((Record<int, int?> p) => line.Slice(new int?(p.Item1), p.Item2, 1)).ToList<string>();
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000B7A08 File Offset: 0x000B5C08
		private static List<Record<int, int?>> MergeFields(Record<int, int?>[] splitPositions, List<string> lines)
		{
			FwFormatLearner.<>c__DisplayClass22_0 CS$<>8__locals1 = new FwFormatLearner.<>c__DisplayClass22_0();
			CS$<>8__locals1.newSplitPositions = new List<Record<int, int?>>(splitPositions);
			List<List<string>> list = lines.Select((string line) => FwFormatLearner.Split(CS$<>8__locals1.newSplitPositions, line)).ToList<List<string>>();
			CS$<>8__locals1.succColumn = list.Select((List<string> row) => row.Last<string>()).ToList<string>();
			int i;
			int j;
			for (i = CS$<>8__locals1.newSplitPositions.Count - 2; i >= 0; i = j - 1)
			{
				List<string> column = list.Select((List<string> row) => row[i]).ToList<string>();
				if (Enumerable.Range(0, column.Count).Any((int idx) => FwFormatLearner.<MergeFields>g__Mergeable|22_0(column[idx], CS$<>8__locals1.succColumn[idx])))
				{
					Record<int, int?> record = new Record<int, int?>(CS$<>8__locals1.newSplitPositions[i].Item1, CS$<>8__locals1.newSplitPositions[i + 1].Item2);
					CS$<>8__locals1.newSplitPositions.RemoveAt(i + 1);
					CS$<>8__locals1.newSplitPositions[i] = record;
				}
				CS$<>8__locals1.succColumn = column;
				j = i;
			}
			return CS$<>8__locals1.newSplitPositions;
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x000B7E2C File Offset: 0x000B602C
		[CompilerGenerated]
		internal static double <TopFormat>g__CalculateCoverage|17_0(IReadOnlyList<FwColumnFormat> format)
		{
			int num = 0;
			for (int i = 1; i < format.Count; i++)
			{
				int? end = format[i - 1].End;
				if (end != null)
				{
					num += format[i].Start - end.Value;
				}
			}
			return 1.0 - (double)num / (double)(format.Last<FwColumnFormat>().End ?? format.Last<FwColumnFormat>().Start);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000B7EB4 File Offset: 0x000B60B4
		[CompilerGenerated]
		internal static bool <MergeFields>g__Mergeable|22_0(string cell, string nextCell)
		{
			if (cell == null || nextCell == null)
			{
				return false;
			}
			if (cell.Length < 0 || nextCell.Length < 0)
			{
				return false;
			}
			IEnumerable<char> enumerable = cell.Reverse<char>();
			Func<char, bool> func;
			if ((func = FwFormatLearner.<>O.<1>__IsWhiteSpace) == null)
			{
				func = (FwFormatLearner.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
			}
			int num = enumerable.TakeWhile(func).Count<char>();
			Func<char, bool> func2;
			if ((func2 = FwFormatLearner.<>O.<1>__IsWhiteSpace) == null)
			{
				func2 = (FwFormatLearner.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
			}
			int num2 = nextCell.TakeWhile(func2).Count<char>();
			return num + num2 < 2;
		}

		// Token: 0x04001B4B RID: 6987
		private static readonly Regex NumberRegex = new Regex("\\d", RegexOptions.Compiled);

		// Token: 0x04001B4C RID: 6988
		private static readonly Regex FirstRowRegex = new Regex("(\\D+0*|^0*)1\\D+", RegexOptions.Compiled);

		// Token: 0x04001B4D RID: 6989
		private static readonly Regex ColumnNameRegex = new Regex("^[#\\p{Lu}\\p{Ll}][#-_.\\p{Lu}\\p{Ll}]*( [#-_.\\p{Lu}\\p{Ll}]+)*$", RegexOptions.Compiled);

		// Token: 0x04001B4E RID: 6990
		private static readonly Regex TypeRegex = new Regex("^[\\p{Lu}\\p{Ll}][-_.\\p{Lu}\\p{Ll}0-9]*$", RegexOptions.Compiled);

		// Token: 0x04001B4F RID: 6991
		private static readonly int DetectionRowCount = 5;

		// Token: 0x04001B50 RID: 6992
		private static readonly int MinDetectionRowCount = 2;

		// Token: 0x04001B51 RID: 6993
		private static readonly int MinColumnCount = 2;

		// Token: 0x04001B52 RID: 6994
		private static readonly double ValidRangeRatio = 0.5;

		// Token: 0x04001B53 RID: 6995
		private static readonly double NameUniquenessRatio = 0.7;

		// Token: 0x04001B54 RID: 6996
		private static readonly double DescriptionHasWhitespaces = 2.0;

		// Token: 0x04001B55 RID: 6997
		private static readonly HashSet<string> NameKeywords = new HashSet<string> { "name", "field", "variable" };

		// Token: 0x04001B56 RID: 6998
		private static readonly HashSet<string> TypeKeywords = new HashSet<string> { "type", "fmt", "format" };

		// Token: 0x04001B57 RID: 6999
		private static readonly HashSet<string> DescriptionKeywords = new HashSet<string> { "description" };

		// Token: 0x04001B58 RID: 7000
		private static readonly HashSet<string> HeaderKeywords = FwFormatLearner.NameKeywords.Concat(FwFormatLearner.TypeKeywords).Concat(FwFormatLearner.DescriptionKeywords).Concat(new string[] { "start", "begin", "end", "length", "position", "pos" })
			.ConvertToHashSet<string>();

		// Token: 0x04001B59 RID: 7001
		private static readonly HashSet<string> Types = new HashSet<string>
		{
			"bool", "byte", "char", "decimal", "double", "float", "int", "uint", "long", "short",
			"string", "boolean", "str", "number", "bit", "numeric", "date", "datetime", "time", "text",
			"real", "character"
		};

		// Token: 0x020009A6 RID: 2470
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001B5A RID: 7002
			public static Func<string, StringRegion> <0>__CreateStringRegion;

			// Token: 0x04001B5B RID: 7003
			public static Func<char, bool> <1>__IsWhiteSpace;
		}
	}
}
