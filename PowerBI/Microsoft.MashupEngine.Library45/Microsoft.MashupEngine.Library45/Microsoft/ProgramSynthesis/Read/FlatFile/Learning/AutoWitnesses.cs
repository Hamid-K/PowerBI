using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Read.FlatFile.Semantics;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Learning
{
	// Token: 0x020012AE RID: 4782
	public class AutoWitnesses
	{
		// Token: 0x06009051 RID: 36945 RVA: 0x001E5A4F File Offset: 0x001E3C4F
		private int MaxMissingColumns(int totalColumns)
		{
			if (totalColumns <= 2)
			{
				return 0;
			}
			if (totalColumns <= 4)
			{
				return 1;
			}
			return (int)Math.Floor((double)(0.2f * (float)totalColumns));
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x001E5A6C File Offset: 0x001E3C6C
		private static double ValidColumnsThreshold(int colNum, string delimiter)
		{
			if (delimiter == null)
			{
				return 0.5;
			}
			if (colNum > 5)
			{
				return 0.2;
			}
			return 0.5;
		}

		// Token: 0x06009053 RID: 36947 RVA: 0x001E5A92 File Offset: 0x001E3C92
		public AutoWitnesses(Options options)
		{
			this._options = options;
		}

		// Token: 0x06009054 RID: 36948 RVA: 0x001E5AA4 File Offset: 0x001E3CA4
		public IEnumerable<LearnResult> LearnAuto(string input, int? k, Guid? guid, CancellationToken cancel)
		{
			IReadOnlyList<string> lines = this.GetLines(input);
			if (this._options.FwSchema != null)
			{
				LearnFwResult learnFwResult = this.LearnFixedWidthFromSchema(lines, guid, cancel);
				if (learnFwResult == null)
				{
					return Enumerable.Empty<LearnResult>();
				}
				return new LearnFwResult[] { learnFwResult };
			}
			else
			{
				if (lines == null)
				{
					throw new ArgumentException("input", "No input provided");
				}
				AutoWitnesses.TopK topK = new AutoWitnesses.TopK(k);
				IReadOnlyList<string> newLineSeps = AutoWitnesses.GetNewLineSeps(lines);
				IEnumerable<AutoWitnesses.DelimiterConfig> enumerable = this.LearnDelimiterConfigs(lines, cancel).ToList<AutoWitnesses.DelimiterConfig>();
				bool flag = true;
				bool flag2 = true;
				foreach (AutoWitnesses.DelimiterConfig delimiterConfig in enumerable)
				{
					if (cancel.IsCancellationRequested)
					{
						return Enumerable.Empty<LearnResult>();
					}
					if (flag2 && flag && !delimiterConfig.IsStandardDelimiter && k != null && topK.Count >= k.Value)
					{
						break;
					}
					flag2 &= delimiterConfig.IsStandardDelimiter;
					if (k != null && topK.Count >= k.Value && delimiterConfig.DelimiterScore <= topK.LowestScore)
					{
						break;
					}
					float num = delimiterConfig.DelimiterScore;
					foreach (Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>> record in this.LearnBasicResultAndTable(input, lines, delimiterConfig, guid, cancel))
					{
						LearnResult learnResult;
						IReadOnlyList<IReadOnlyList<string>> readOnlyList;
						record.Deconstruct(out learnResult, out readOnlyList);
						LearnResult result = learnResult;
						IReadOnlyList<IReadOnlyList<string>> readOnlyList2 = readOnlyList;
						LearnCsvResult learnCsvResult = result as LearnCsvResult;
						int num3;
						ISet<int> set;
						float num2;
						if (learnCsvResult != null)
						{
							num2 = this.ComputeConsistencyScore(ref learnCsvResult, ref readOnlyList2, out num3, out set);
							if (this._options.Delimiter == null && (double)num2 <= 0.0)
							{
								continue;
							}
							IEnumerable<IReadOnlyList<string>> enumerable2 = readOnlyList2;
							Func<IReadOnlyList<string>, bool> func;
							if ((func = AutoWitnesses.<>O.<0>__IsBadDataRow) == null)
							{
								func = (AutoWitnesses.<>O.<0>__IsBadDataRow = new Func<IReadOnlyList<string>, bool>(AutoWitnesses.IsBadDataRow));
							}
							if (enumerable2.Any(func))
							{
								num *= 0.7f;
							}
						}
						else if (result is LearnFwResult)
						{
							num2 = 1f;
							num3 = readOnlyList2[0].Count;
							if (num3 == 1)
							{
								num = 0f;
							}
							result.CommentStr = AutoWitnesses.LearnCommentString(readOnlyList2, num3.Some<int>());
							bool flag3;
							set = AutoWitnesses.GetFilteredRows(readOnlyList2, result.CommentStr, out flag3, false);
							result.HasEmptyLines = flag3;
						}
						else
						{
							if (!(result is LearnETextResult))
							{
								throw new Exception(string.Format("Unknown ${0}: ${1}", "LearnResult", result.GetType()));
							}
							num2 = 1f;
							num3 = readOnlyList2[0].Count;
							if (num3 == 1)
							{
								continue;
							}
							set = new HashSet<int>();
						}
						float num4 = num;
						num4 *= num2;
						bool flag4 = num2 >= 1f;
						flag = flag && flag4;
						if (k == null || topK.Count < k.Value || num4 > topK.LowestScore)
						{
							ISet<int> set2 = AutoWitnesses.LearnEmptyColumns(readOnlyList2, num3, result.Skip, set);
							int num5 = num3 - set2.Count;
							int num6;
							result.RawColumnNames = this.LearnHeader(readOnlyList2, result.Skip, delimiterConfig.Delimiter, set2, out num6);
							result.Skip += num6;
							int num7 = readOnlyList2.Count - result.Skip - set.Count;
							if (result.RawColumnNames != null)
							{
								num7++;
							}
							result.ColumnNames = this.CleanColumnNames(result.RawColumnNames, num3);
							result.SkipEmptyAndCommentCount = set.Count((int x) => x < result.Skip);
							result.HasMultiLineRows = AutoWitnesses.HasMultiLineRows(readOnlyList2);
							result.NewLineStrings = newLineSeps;
							float num8 = 1f * (float)num7 / (float)lines.Count * (float)(num5 - 1) / (float)num5;
							num4 *= num8;
							if (delimiterConfig.Delimiter != null)
							{
								if (delimiterConfig.Delimiter.All((char ch) => ch == ' ') && (set2.Count > 0 || AutoWitnesses.CheckExtraSpaces(lines, result, num3, set)))
								{
									num4 *= 0.5f;
								}
							}
							topK.Add(result, num4);
							if (flag4)
							{
								break;
							}
						}
					}
				}
				return topK;
			}
		}

		// Token: 0x06009055 RID: 36949 RVA: 0x001E5F70 File Offset: 0x001E4170
		private static bool IsBadDataRow(IReadOnlyList<string> row)
		{
			if (row == null)
			{
				return false;
			}
			Stack<char> stack = new Stack<char>();
			bool flag = true;
			foreach (string text in row)
			{
				if (text == null)
				{
					return false;
				}
				Stack<char> stack2 = new Stack<char>();
				foreach (char c in text)
				{
					char c2;
					if (AutoWitnesses.StandardParens.Values.Contains(c))
					{
						stack.Push(c);
						stack2.Push(c);
					}
					else if (AutoWitnesses.StandardParens.TryGetValue(c, out c2))
					{
						if (stack.Count == 0 || stack.Pop() != c2)
						{
							return false;
						}
						if (stack2.Count == 0 || stack2.Pop() != c2)
						{
							flag = false;
						}
					}
				}
				flag &= stack2.Count == 0;
			}
			return stack.Count == 0 && !flag;
		}

		// Token: 0x06009056 RID: 36950 RVA: 0x001E607C File Offset: 0x001E427C
		private static IReadOnlyList<string> GetNewLineSeps(IReadOnlyList<string> lines)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in lines)
			{
				int length = text.Length;
				if (length >= 2 && text[length - 2] == '\r' && text[length - 1] == '\n')
				{
					hashSet.Add("\r\n");
				}
				else if (length >= 1 && text[length - 1] == '\r')
				{
					hashSet.Add("\r");
				}
				else if (length >= 1 && text[length - 1] == '\n')
				{
					hashSet.Add("\n");
				}
			}
			return (from x in hashSet
				orderby x.Length descending, x[0]
				select x).ToList<string>();
		}

		// Token: 0x06009057 RID: 36951 RVA: 0x001E6180 File Offset: 0x001E4380
		private IReadOnlyList<string> GetLines(string input)
		{
			if (input == null)
			{
				return null;
			}
			IEnumerable<string> enumerable = Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.SplitLines(input, false);
			if (this._options.LearnLineLimit != null)
			{
				enumerable = enumerable.Take(this._options.LearnLineLimit.Value);
			}
			return enumerable.ToList<string>();
		}

		// Token: 0x06009058 RID: 36952 RVA: 0x001E61CF File Offset: 0x001E43CF
		private static bool HasMultiLineRows(IReadOnlyList<IReadOnlyList<string>> rows)
		{
			return rows.Any((IReadOnlyList<string> row) => row.Any(delegate(string cell)
			{
				if (cell != null)
				{
					return cell.Any((char ch) => ch == '\r' || ch == '\n');
				}
				return false;
			}));
		}

		// Token: 0x06009059 RID: 36953 RVA: 0x001E61F8 File Offset: 0x001E43F8
		private float ComputeConsistencyScore(ref LearnCsvResult result, ref IReadOnlyList<IReadOnlyList<string>> rows, out int columnCount, out ISet<int> filteredRows)
		{
			result.CommentStr = AutoWitnesses.LearnCommentString(rows, Optional<int>.Nothing);
			bool flag;
			filteredRows = AutoWitnesses.GetFilteredRows(rows, result.CommentStr, out flag, true);
			result.HasEmptyLines = flag;
			List<int> list = rows.Select((IReadOnlyList<string> row) => row.Count((string cell) => cell != null)).ToList<int>();
			Dictionary<int, int> dictionary = AutoWitnesses.ComputeCountFreqs(list, result.Skip, filteredRows);
			columnCount = dictionary.ArgMax((KeyValuePair<int, int> x) => x.Value).Key;
			if (columnCount == 1 && !string.IsNullOrEmpty(result.Delimiter))
			{
				return 0f;
			}
			if (!result.CommentStr.HasValue)
			{
				result.CommentStr = AutoWitnesses.LearnCommentString(rows, columnCount.Some<int>());
				if (result.CommentStr.HasValue)
				{
					bool flag2;
					filteredRows = AutoWitnesses.GetFilteredRows(rows, result.CommentStr, out flag2, true);
				}
			}
			if (this._options.Skip == null)
			{
				while (result.Skip < list.Count && (list[result.Skip] != columnCount || filteredRows.Contains(result.Skip)))
				{
					LearnCsvResult learnCsvResult = result;
					int num = learnCsvResult.Skip + 1;
					learnCsvResult.Skip = num;
				}
			}
			int tmpSkip = result.Skip;
			if (result.CommentStr.HasValue && filteredRows.Any((int i) => i < tmpSkip))
			{
				result.CommentStr = AutoWitnesses.LearnCommentString(rows.Skip(tmpSkip), columnCount.Some<int>());
				if (!result.CommentStr.HasValue)
				{
					bool flag2;
					filteredRows = AutoWitnesses.GetFilteredRows(rows, result.CommentStr, out flag2, true);
				}
			}
			if (rows.Count - result.Skip - result.SkipFooter <= 0)
			{
				return float.MinValue;
			}
			if (this._options.SkipFooter == null && rows.Count > 2 && !filteredRows.Contains(rows.Count - 1))
			{
				int count = rows.Last<IReadOnlyList<string>>().Count;
				if (count != columnCount)
				{
					list.RemoveAt(list.Count - 1);
					Dictionary<int, int> dictionary2 = dictionary;
					int num = count;
					int num2 = dictionary2[num];
					dictionary2[num] = num2 - 1;
					if (dictionary[count] == 0)
					{
						dictionary.Remove(count);
					}
				}
			}
			if (this._options.Skip == null && result.Skip > 0)
			{
				dictionary = AutoWitnesses.ComputeCountFreqs(list, result.Skip, filteredRows);
			}
			float num3 = 1f;
			int tmpColumnCount = columnCount;
			if (dictionary.Count > 1)
			{
				IDictionary<int, int> dictionary3 = dictionary.Where((KeyValuePair<int, int> kvp) => kvp.Key != tmpColumnCount).ToDictionary<int, int>();
				if (dictionary3.Any((KeyValuePair<int, int> kvp) => kvp.Key > tmpColumnCount))
				{
					num3 = 0f;
				}
				else
				{
					int maxAllowedMissingColumns = this.MaxMissingColumns(columnCount);
					if (dictionary3.Max((KeyValuePair<int, int> kvp) => tmpColumnCount - kvp.Key) > maxAllowedMissingColumns)
					{
						return 0f;
					}
					int num4 = dictionary3.Sum((KeyValuePair<int, int> kvp) => (tmpColumnCount - kvp.Key) * kvp.Value);
					int num5 = dictionary.Sum((KeyValuePair<int, int> kvp) => maxAllowedMissingColumns * kvp.Value);
					num3 = 1f - (float)num4 / (float)num5;
				}
			}
			rows = AutoWitnesses.GetRegularTable(columnCount, rows);
			return num3;
		}

		// Token: 0x0600905A RID: 36954 RVA: 0x001E6588 File Offset: 0x001E4788
		private static bool CheckExtraSpaces(IReadOnlyList<string> lines, LearnResult result, int columnCount, ISet<int> filteredRows)
		{
			IEnumerable<IReadOnlyList<string>> enumerable = AutoWitnesses.EvalResult(result, lines, columnCount, false).Skip(result.Skip);
			int num = result.Skip;
			foreach (IReadOnlyList<string> readOnlyList in enumerable)
			{
				if (!filteredRows.Contains(num))
				{
					for (int i = 0; i < readOnlyList.Count; i++)
					{
						string text = readOnlyList[i];
						if (text != null && ((i > 0 && text.StartsWith(" ")) || (i < readOnlyList.Count - 1 && text.EndsWith(" "))))
						{
							return true;
						}
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x0600905B RID: 36955 RVA: 0x001E6644 File Offset: 0x001E4844
		private IEnumerable<Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>> LearnBasicResultAndTable(string input, IReadOnlyList<string> lines, AutoWitnesses.DelimiterConfig config, Guid? guid, CancellationToken cancel)
		{
			if (config.Delimiter == null)
			{
				if (this._options.LearnFw)
				{
					Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>? record = this.LearnFixedWidth(lines, guid, cancel);
					if (record != null)
					{
						yield return record.Value;
					}
				}
				if (this._options.LearnExtractionText)
				{
					Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>? record2 = this.LearnEText(input, lines, guid, cancel);
					if (record2 != null)
					{
						yield return record2.Value;
					}
				}
				yield break;
			}
			List<Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>> tmpResults = new List<Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>>();
			foreach (Optional<char> quote in config.QuoteChars)
			{
				foreach (Optional<char> optional in config.EscapeChars)
				{
					if (cancel.IsCancellationRequested)
					{
						yield break;
					}
					LearnResult learnResult = new LearnCsvResult
					{
						Skip = this._options.Skip.GetValueOrDefault(),
						SkipFooter = this._options.SkipFooter.GetValueOrDefault(),
						Delimiter = config.Delimiter,
						EscapeChar = optional,
						QuoteChar = quote,
						DoubleQuoteEscape = quote.HasValue,
						FilterEmptyLines = true
					};
					IReadOnlyList<IReadOnlyList<string>> readOnlyList = AutoWitnesses.EvalResult(learnResult, lines, 0, true);
					if (string.IsNullOrWhiteSpace(config.Delimiter) && (quote.HasValue || optional.HasValue) && lines.Count == readOnlyList.Count)
					{
						tmpResults.Add(Record.Create<LearnResult, IReadOnlyList<IReadOnlyList<string>>>(learnResult, readOnlyList));
					}
					else
					{
						yield return Record.Create<LearnResult, IReadOnlyList<IReadOnlyList<string>>>(learnResult, readOnlyList);
					}
				}
				IEnumerator<Optional<char>> enumerator2 = null;
				quote = default(Optional<char>);
			}
			IEnumerator<Optional<char>> enumerator = null;
			foreach (Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>> record3 in tmpResults)
			{
				yield return record3;
			}
			List<Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>>.Enumerator enumerator3 = default(List<Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600905C RID: 36956 RVA: 0x001E667C File Offset: 0x001E487C
		private Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>? LearnEText(string input, IReadOnlyList<string> lines, Guid? guid, CancellationToken cancel)
		{
			lines = lines.Select((string line) => line.TrimEnd(new char[] { '\r', '\n' })).ToList<string>();
			int num = this._options.Skip ?? AutoWitnesses.LearnPrefixSkip(lines, null);
			IEnumerable<string> enumerable = lines.Skip(num);
			Func<string, StringRegion> func;
			if ((func = AutoWitnesses.<>O.<1>__CreateStringRegion) == null)
			{
				func = (AutoWitnesses.<>O.<1>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
			}
			IReadOnlyList<StringRegion> readOnlyList = enumerable.Select(func).DropLast(this._options.SkipFooter.GetValueOrDefault()).ToList<StringRegion>();
			SplitProgram p = new SplitSession(null, null, null)
			{
				Inputs = { readOnlyList },
				Constraints = 
				{
					new IncludeDelimitersInOutput(false)
				}
			}.Learn(null, cancel, guid);
			if (p == null)
			{
				return null;
			}
			Session session = new Session(null, null);
			IReadOnlyList<List<ExampleCell>> readOnlyList2 = (from line in readOnlyList.Take(5)
				select (from cell in p.Run(line)
					select new ExampleCell(cell.CellValue.Value, true)).ToList<ExampleCell>()).ToList<List<ExampleCell>>();
			session.AddExample(input, new Table<ExampleCell>(readOnlyList2[0].Select((ExampleCell _, int i) => "Column" + (i + 1).ToString()), readOnlyList2, null));
			Microsoft.ProgramSynthesis.Extraction.Text.Program program = session.Learn(null, cancel, guid);
			if (program == null)
			{
				return null;
			}
			LearnETextResult learnETextResult = new LearnETextResult();
			learnETextResult.Skip = num;
			learnETextResult.SkipFooter = this._options.SkipFooter.GetValueOrDefault();
			learnETextResult.LearnedProgram = program;
			IReadOnlyList<IReadOnlyList<string>> readOnlyList3 = program.Run(input).Rows.Cast<IReadOnlyList<string>>().ToList<IReadOnlyList<string>>();
			return new Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>?(Record.Create<LearnResult, IReadOnlyList<IReadOnlyList<string>>>(learnETextResult, readOnlyList3));
		}

		// Token: 0x0600905D RID: 36957 RVA: 0x001E6848 File Offset: 0x001E4A48
		private Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>? LearnFixedWidth(IReadOnlyList<string> lines, Guid? guid, CancellationToken cancel)
		{
			lines = lines.Select((string line) => line.TrimEnd(new char[] { '\r', '\n' })).ToList<string>();
			int num = this._options.Skip ?? AutoWitnesses.LearnPrefixSkip(lines, null);
			IReadOnlyList<Record<int, int?>> readOnlyList = this._options.FieldPositions;
			if (readOnlyList == null)
			{
				IEnumerable<string> enumerable = lines.Skip(num);
				Func<string, StringRegion> func;
				if ((func = AutoWitnesses.<>O.<1>__CreateStringRegion) == null)
				{
					func = (AutoWitnesses.<>O.<1>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
				}
				IReadOnlyList<StringRegion> readOnlyList2 = enumerable.Select(func).DropLast(this._options.SkipFooter.GetValueOrDefault()).ToList<StringRegion>();
				SplitProgram splitProgram = new SplitSession(null, null, null)
				{
					Inputs = { readOnlyList2 },
					Constraints = 
					{
						new FixedWidthConstraint()
					}
				}.Learn(null, cancel, guid);
				if (splitProgram == null)
				{
					if (this._options.LearnCsv || this._options.LearnExtractionText)
					{
						return null;
					}
					readOnlyList = new Record<int, int?>[] { Record.Create<int, int?>(0, null) };
				}
				else
				{
					readOnlyList = splitProgram.Properties.FieldPositions;
				}
			}
			LearnFwResult learnFwResult = new LearnFwResult();
			learnFwResult.Skip = num;
			learnFwResult.FieldPositions = readOnlyList;
			learnFwResult.FilterEmptyLines = true;
			learnFwResult.SkipFooter = this._options.SkipFooter.GetValueOrDefault();
			IReadOnlyList<IReadOnlyList<string>> readOnlyList3 = AutoWitnesses.EvalResult(learnFwResult, lines, 0, true);
			return new Record<LearnResult, IReadOnlyList<IReadOnlyList<string>>>?(Record.Create<LearnResult, IReadOnlyList<IReadOnlyList<string>>>(learnFwResult, readOnlyList3));
		}

		// Token: 0x0600905E RID: 36958 RVA: 0x001E69CC File Offset: 0x001E4BCC
		private LearnFwResult LearnFixedWidthFromSchema(IReadOnlyList<string> lines, Guid? guid, CancellationToken cancel)
		{
			IReadOnlyList<FwColumnFormat> readOnlyList = FwFormatLearner.Learn(this._options.FwSchema, guid, cancel);
			if (readOnlyList == null)
			{
				return null;
			}
			IReadOnlyList<Record<int, int?>> readOnlyList2 = readOnlyList.Select((FwColumnFormat f) => Record.Create<int, int?>(f.Start - 1, f.End)).ToList<Record<int, int?>>();
			IReadOnlyList<string> readOnlyList3 = null;
			if (readOnlyList.Any((FwColumnFormat f) => f.Name != null))
			{
				readOnlyList3 = readOnlyList.Select((FwColumnFormat f, int i) => f.Name).ToList<string>();
			}
			else if (readOnlyList.Any((FwColumnFormat f) => f.Description != null))
			{
				readOnlyList3 = readOnlyList.Select((FwColumnFormat f, int i) => f.Description).ToList<string>();
			}
			return new LearnFwResult
			{
				RawColumnNames = readOnlyList3,
				ColumnNames = this.CleanColumnNames(readOnlyList3, readOnlyList3.Count),
				FieldPositions = readOnlyList2,
				FilterEmptyLines = true,
				NewLineStrings = ((lines != null) ? AutoWitnesses.GetNewLineSeps(lines) : null)
			};
		}

		// Token: 0x0600905F RID: 36959 RVA: 0x001E6B04 File Offset: 0x001E4D04
		private string[] CleanColumnNames(IReadOnlyList<string> rawColumnNames, int columnCount)
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>();
			for (int i = 0; i < columnCount; i++)
			{
				string text = ((rawColumnNames != null) ? rawColumnNames[i] : null);
				ColumnNameCleaningType columnNameCleaning = this._options.ColumnNameCleaning;
				string text2;
				if (columnNameCleaning != ColumnNameCleaningType.UnicodeAlphaNumeric)
				{
					if (columnNameCleaning != ColumnNameCleaningType.AsciiAlphaNumeric)
					{
						text2 = text;
					}
					else
					{
						text2 = AutoWitnesses.ToValidIdentifier(text);
					}
				}
				else
				{
					text2 = string.Concat<char>(text.Where((char ch) => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)));
				}
				string text3 = text2;
				if (string.IsNullOrWhiteSpace(text3))
				{
					text3 = string.Format("column{0}", i + 1);
				}
				string text4 = text3.ToLower();
				int num = 1;
				string text5 = text3;
				while (hashSet.Contains(text4))
				{
					string text6 = (char.IsDigit(text5[text5.Length - 1]) ? "_" : string.Empty);
					text3 = string.Format("{0}{1}{2}", text5, text6, ++num);
					text4 = text3.ToLower();
				}
				hashSet.Add(text4);
				list.Add(text3);
			}
			return list.ToArray();
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x001E6C30 File Offset: 0x001E4E30
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
				bool flag5 = AutoWitnesses.IsAsciiDigit(c);
				bool flag6 = AutoWitnesses.IsAsciiLetter(c);
				bool flag7 = i + 1 < name.Length && (AutoWitnesses.IsAsciiDigit(name[i + 1]) || AutoWitnesses.IsAsciiLetter(name[i + 1]));
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

		// Token: 0x06009061 RID: 36961 RVA: 0x001E6D30 File Offset: 0x001E4F30
		private static Dictionary<int, int> ComputeCountFreqs(IReadOnlyList<int> columnCounts, int skip, ISet<int> filteredRows)
		{
			return (from x in columnCounts.Skip(skip).Where((int _, int i) => !filteredRows.Contains(i + skip))
				group x by x).ToDictionary((IGrouping<int, int> grp) => grp.Key, (IGrouping<int, int> grp) => grp.Count<int>());
		}

		// Token: 0x06009062 RID: 36962 RVA: 0x001E6DD5 File Offset: 0x001E4FD5
		private IEnumerable<AutoWitnesses.DelimiterConfig> LearnDelimiterConfigs(IReadOnlyList<string> lines, CancellationToken cancel)
		{
			if (!this._options.LearnCsv)
			{
				if (this._options.LearnFw || this._options.LearnExtractionText)
				{
					yield return new AutoWitnesses.DelimiterConfig(null, null, null);
				}
				yield break;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			if (this._options.Delimiter == null)
			{
				dictionary.Add(string.Empty, int.MaxValue);
			}
			else
			{
				dictionary.Add(this._options.Delimiter, int.MaxValue);
			}
			Dictionary<string, HashSet<Optional<char>>> delimiterQuotes = new Dictionary<string, HashSet<Optional<char>>> { 
			{
				",",
				new HashSet<Optional<char>> { '"'.Some<char>() }
			} };
			Dictionary<string, HashSet<Optional<char>>> delimiterEscapes = new Dictionary<string, HashSet<Optional<char>>>();
			HashSet<Optional<char>> allQuotes = new HashSet<Optional<char>> { Optional<char>.Nothing };
			HashSet<Optional<char>> allEscapes = new HashSet<Optional<char>> { Optional<char>.Nothing };
			foreach (string text in lines)
			{
				if (cancel.IsCancellationRequested)
				{
					yield break;
				}
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					char? c2 = ((i > 0) ? new char?(text[i - 1]) : null);
					if (c == '\r' || c == '\n')
					{
						break;
					}
					char? c3 = ((i + 1 < text.Length) ? new char?(text[i + 1]) : null);
					if (c3 != null)
					{
						char value = c3.Value;
						if (AutoWitnesses.StandardEscapeScores.ContainsKey(c) && (value == '\r' || value == '\n' || AutoWitnesses.StandardQuoteScores.ContainsKey(value)))
						{
							allEscapes.Add(c.Some<char>());
						}
					}
					if (AutoWitnesses.StandardQuoteScores.ContainsKey(c))
					{
						allQuotes.Add(c.Some<char>());
					}
					if (this._options.Delimiter == null && (lines.Count != 1 || AutoWitnesses.StandardDelimiterScores.ContainsKey(c.ToString())) && !AutoWitnesses.IsAsciiLetterOrDigit(c) && !AutoWitnesses.IgnoreDelimiterChars.Contains(c))
					{
						char? c4 = ((c2 != null && AutoWitnesses.StandardEscapeScores.ContainsKey(c2.Value)) ? c2 : null);
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(c);
						string text2 = stringBuilder.ToString();
						dictionary.GetAndIncrement(text2);
						if (c4 != null)
						{
							AutoWitnesses.<LearnDelimiterConfigs>g__AddChar|41_0(delimiterEscapes, text2, c4.Value);
						}
						bool flag = c == ' ';
						if (!AutoWitnesses.StandardDelimiterScores.ContainsKey(c.ToString()))
						{
							for (int j = i + 1; j < Math.Min(text.Length, i + 5); j++)
							{
								char c5 = text[j];
								if ((flag && c5 != ' ') || (!flag && c5 == ' ') || AutoWitnesses.IgnoreDelimiterChars.Contains(c5) || AutoWitnesses.StandardDelimiterScores.ContainsKey(c5.ToString()))
								{
									break;
								}
								stringBuilder.Append(c5);
								if (!AutoWitnesses.IsAsciiLetterOrDigit(c5))
								{
									text2 = stringBuilder.ToString();
									dictionary.GetAndIncrement(text2);
									if (c4 != null)
									{
										AutoWitnesses.<LearnDelimiterConfigs>g__AddChar|41_0(delimiterEscapes, text2, c4.Value);
									}
								}
							}
						}
					}
				}
			}
			int minDelimiterCount = Math.Max(2, lines.Count / 10);
			IEnumerable<string> enumerable = from kvp in dictionary
				where kvp.Value >= minDelimiterCount || AutoWitnesses.StandardDelimiterScores.ContainsKey(kvp.Key)
				select kvp.Key;
			if (this._options.LearnFw)
			{
				enumerable = enumerable.AppendItem(null);
			}
			IEnumerable<string> enumerable2 = enumerable;
			Func<string, float> func;
			if ((func = AutoWitnesses.<>O.<2>__DelimiterScore) == null)
			{
				func = (AutoWitnesses.<>O.<2>__DelimiterScore = new Func<string, float>(AutoWitnesses.DelimiterScore));
			}
			enumerable = enumerable2.OrderByDescending(func).ThenByDescending(delegate(string del)
			{
				if (del != null)
				{
					return del.Length;
				}
				return int.MaxValue;
			});
			using (IEnumerator<string> enumerator2 = enumerable.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string delimiter = enumerator2.Current;
					IReadOnlyList<Optional<char>> readOnlyList;
					IReadOnlyList<Optional<char>> readOnlyList2;
					if (!string.IsNullOrEmpty(delimiter))
					{
						IEnumerable<Optional<char>> enumerable3 = from ch in allQuotes.Union(delimiterQuotes.GetOrDefault(delimiter, new HashSet<Optional<char>>()))
							where !ch.HasValue || !delimiter.Contains(ch.Value)
							select ch;
						Func<Optional<char>, float> func2;
						if ((func2 = AutoWitnesses.<>O.<3>__QuoteScore) == null)
						{
							func2 = (AutoWitnesses.<>O.<3>__QuoteScore = new Func<Optional<char>, float>(AutoWitnesses.QuoteScore));
						}
						readOnlyList = enumerable3.OrderByDescending(func2).ToList<Optional<char>>();
						IEnumerable<Optional<char>> enumerable4 = from ch in allEscapes.Union(delimiterEscapes.GetOrDefault(delimiter, new HashSet<Optional<char>>()))
							where !ch.HasValue || !delimiter.Contains(ch.Value)
							select ch;
						Func<Optional<char>, float> func3;
						if ((func3 = AutoWitnesses.<>O.<4>__EscapeScore) == null)
						{
							func3 = (AutoWitnesses.<>O.<4>__EscapeScore = new Func<Optional<char>, float>(AutoWitnesses.EscapeScore));
						}
						readOnlyList2 = enumerable4.OrderByDescending(func3).ToList<Optional<char>>();
					}
					else
					{
						readOnlyList = new Optional<char>[] { Optional<char>.Nothing };
						readOnlyList2 = new Optional<char>[] { Optional<char>.Nothing };
					}
					yield return new AutoWitnesses.DelimiterConfig(delimiter, readOnlyList, readOnlyList2);
				}
			}
			IEnumerator<string> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x001E6DF4 File Offset: 0x001E4FF4
		private static Optional<string> LearnCommentString(IEnumerable<IReadOnlyList<string>> rows, Optional<int> colNum)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (IReadOnlyList<string> readOnlyList in rows)
			{
				if (((colNum.HasValue && colNum.Value != readOnlyList.Count) || (!colNum.HasValue && readOnlyList.Count == 1)) && !string.IsNullOrEmpty(readOnlyList[0]))
				{
					string text = readOnlyList[0].TrimStart(Array.Empty<char>());
					hashSet.AddRange(AutoWitnesses.LikelyCommentStrings.Where(new Func<string, bool>(text.StartsWith)));
				}
			}
			if (hashSet.Count == 1)
			{
				return hashSet.First<string>().Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x001E6EBC File Offset: 0x001E50BC
		private static ISet<int> GetFilteredRows(IReadOnlyList<IReadOnlyList<string>> rows, Optional<string> commentStr, out bool hasEmptyLines, bool isCsv)
		{
			Func<IReadOnlyList<string>, bool> func;
			if (!isCsv)
			{
				if ((func = AutoWitnesses.<>O.<6>__IsFwEmptyRow) == null)
				{
					func = (AutoWitnesses.<>O.<6>__IsFwEmptyRow = new Func<IReadOnlyList<string>, bool>(Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.IsFwEmptyRow));
				}
			}
			else if ((func = AutoWitnesses.<>O.<5>__IsCsvEmptyRow) == null)
			{
				func = (AutoWitnesses.<>O.<5>__IsCsvEmptyRow = new Func<IReadOnlyList<string>, bool>(Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.IsCsvEmptyRow));
			}
			Func<IReadOnlyList<string>, bool> func2 = func;
			hasEmptyLines = false;
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < rows.Count; i++)
			{
				IReadOnlyList<string> readOnlyList = rows[i];
				bool flag = func2(readOnlyList);
				if (flag)
				{
					hasEmptyLines = true;
				}
				if (flag || (commentStr.HasValue && Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.IsCommentRow(readOnlyList, commentStr.Value)))
				{
					hashSet.Add(i);
				}
			}
			return hashSet;
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x001E6F58 File Offset: 0x001E5158
		private static IReadOnlyList<IReadOnlyList<string>> EvalResult(LearnResult result, IReadOnlyList<string> lines, int columnCount = 0, bool trim = true)
		{
			return result.Switch<IEnumerable<IReadOnlyList<string>>>((LearnCsvResult csvRes) => Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.Csv(lines, (columnCount == 0) ? Optional<int>.Nothing : columnCount.Some<int>(), csvRes.Delimiter, csvRes.QuoteChar, csvRes.EscapeChar, csvRes.DoubleQuoteEscape, trim, true), (LearnFwResult fwRes) => Microsoft.ProgramSynthesis.Read.FlatFile.Semantics.Semantics.Fw(lines, fwRes.FieldPositions, trim), delegate(LearnETextResult etextRes)
			{
				throw new NotImplementedException("EvalResult with LearnETextResult should be unreachable.");
			}).DropLast(result.SkipFooter).ToList<IReadOnlyList<string>>();
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x001E6FD0 File Offset: 0x001E51D0
		private static IReadOnlyList<IReadOnlyList<string>> GetRegularTable(int columnCount, IReadOnlyList<IReadOnlyList<string>> originalTable)
		{
			AutoWitnesses.<>c__DisplayClass45_0 CS$<>8__locals1 = new AutoWitnesses.<>c__DisplayClass45_0();
			CS$<>8__locals1.columnCount = columnCount;
			if (originalTable.All((IReadOnlyList<string> row) => row.Count == CS$<>8__locals1.columnCount))
			{
				return originalTable;
			}
			return originalTable.Select(new Func<IReadOnlyList<string>, IReadOnlyList<string>>(CS$<>8__locals1.<GetRegularTable>g__AdjustRow|1)).ToList<IReadOnlyList<string>>();
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x001E7018 File Offset: 0x001E5218
		private static ISet<int> LearnEmptyColumns(IReadOnlyList<IReadOnlyList<string>> rows, int columnCount, int skip, ISet<int> filteredRows)
		{
			HashSet<int> hashSet = new HashSet<int>();
			int col;
			Func<IReadOnlyList<string>, int, bool> <>9__0;
			int num;
			for (col = 0; col < columnCount; col = num)
			{
				IEnumerable<IReadOnlyList<string>> enumerable = rows.Skip(skip);
				Func<IReadOnlyList<string>, int, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IReadOnlyList<string> row, int i) => !filteredRows.Contains(skip + i) && !string.IsNullOrEmpty(row[col]));
				}
				if (!enumerable.Where(func).Any<IReadOnlyList<string>>())
				{
					hashSet.Add(col);
				}
				num = col + 1;
			}
			return hashSet;
		}

		// Token: 0x06009068 RID: 36968 RVA: 0x001E70A8 File Offset: 0x001E52A8
		public IReadOnlyList<string> LearnHeader(IReadOnlyList<IReadOnlyList<string>> rows, int originalSkip, string delimiter, ISet<int> emptyColumns, out int additionalSkip)
		{
			additionalSkip = 0;
			if (rows.Count <= 1)
			{
				return null;
			}
			int num = originalSkip;
			if (this._options.Skip == null)
			{
				int num2 = 0;
				while (num2 + num < rows.Count)
				{
					if (rows[num2 + num].Any((string cell) => !string.IsNullOrWhiteSpace(cell)))
					{
						break;
					}
					num2++;
				}
				if (num2 > 0)
				{
					num += num2;
				}
			}
			if (delimiter != null && this._options.Skip == null)
			{
				int num3 = AutoWitnesses.LearnPrefixSkip((from row in rows.Skip(num)
					select row[0]).ToList<string>(), delimiter);
				if (num3 > 0)
				{
					num += num3;
				}
			}
			IReadOnlyList<string> readOnlyList = null;
			if (num > 0)
			{
				readOnlyList = AutoWitnesses.LearnColumnNamesInSkippedRows(rows.Take(num).ToList<IReadOnlyList<string>>(), delimiter, emptyColumns);
			}
			if ((readOnlyList == null || !readOnlyList.All(new Func<string, bool>(AutoWitnesses.<LearnHeader>g__IsPreferredColumnName|47_0))) && this._options.Skip == null)
			{
				int num4;
				IReadOnlyList<string> readOnlyList2 = AutoWitnesses.LearnColumnNamesInData(rows.Skip(num).Take(20).ToList<IReadOnlyList<string>>(), delimiter, emptyColumns, out num4);
				if (readOnlyList2 != null && (readOnlyList == null || readOnlyList.Count(new Func<string, bool>(AutoWitnesses.<LearnHeader>g__IsPreferredColumnName|47_0)) < readOnlyList2.Count(new Func<string, bool>(AutoWitnesses.<LearnHeader>g__IsPreferredColumnName|47_0))))
				{
					readOnlyList = readOnlyList2;
					num += num4;
				}
			}
			additionalSkip = num - originalSkip;
			return readOnlyList;
		}

		// Token: 0x06009069 RID: 36969 RVA: 0x001E7224 File Offset: 0x001E5424
		private static int LearnPrefixSkip(IReadOnlyList<string> rows, string delimiter)
		{
			IReadOnlyList<ISet<RegularExpression>> readOnlyList = rows.Select((string row) => AutoWitnesses.LearnPrefixRegexes(row, delimiter, 1)).ToList<ISet<RegularExpression>>();
			if (readOnlyList.All((ISet<RegularExpression> re) => re.Count == 0 || (re.Count == 1 && re.First<RegularExpression>().Tokens[0].Name == "WhiteSpace")))
			{
				readOnlyList = rows.Select((string row) => AutoWitnesses.LearnPrefixRegexes(row, delimiter, 2)).ToList<ISet<RegularExpression>>();
			}
			if (readOnlyList.All((ISet<RegularExpression> re) => re.Count == 0))
			{
				return 0;
			}
			int i;
			for (i = Math.Min(rows.Count / 2, 30); i > 0; i--)
			{
				ISet<RegularExpression> set = readOnlyList.Take(i).SelectMany((ISet<RegularExpression> x) => x).ConvertToHashSet<RegularExpression>();
				ISet<RegularExpression> dataRegexes = readOnlyList.Skip(i).SelectMany((ISet<RegularExpression> x) => x).ConvertToHashSet<RegularExpression>();
				IEnumerable<string> enumerable = rows.Skip(i);
				Func<string, bool> func;
				if ((func = AutoWitnesses.<>O.<7>__IsNullOrWhiteSpace) == null)
				{
					func = (AutoWitnesses.<>O.<7>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
				}
				if (!enumerable.All(func) && !set.Any((RegularExpression re) => dataRegexes.Contains(re)))
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600906A RID: 36970 RVA: 0x001E738C File Offset: 0x001E558C
		private static ISet<RegularExpression> LearnPrefixRegexes(string s, string delimiter, int len)
		{
			if (s == null)
			{
				return new HashSet<RegularExpression>();
			}
			if (!string.IsNullOrEmpty(delimiter))
			{
				s += delimiter;
			}
			HashSet<RegularExpression> hashSet = new HashSet<RegularExpression>();
			foreach (Record<RegularExpression, Regex> record in AutoWitnesses.PrefixRegexes.Value)
			{
				RegularExpression regularExpression;
				Regex regex;
				record.Deconstruct(out regularExpression, out regex);
				RegularExpression regularExpression2 = regularExpression;
				Regex regex2 = regex;
				if (regularExpression2.Count == len && regex2.IsMatch(s))
				{
					hashSet.Add(regularExpression2);
				}
			}
			return hashSet;
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x001E7420 File Offset: 0x001E5620
		private static IEnumerable<Record<RegularExpression, Regex>> GetPrefixRegexes()
		{
			AbstractRegexToken whiteSpaceToken = (AbstractRegexToken)Token.Tokens["WhiteSpace"];
			foreach (AbstractRegexToken token in Token.NonDisjunctiveTokens.Values.OfType<AbstractRegexToken>())
			{
				if (token.UseForLearning && token.Name != "Alphanum" && token.Name != "Line Separator")
				{
					yield return Record.Create<RegularExpression, Regex>(RegularExpression.Create(new AbstractRegexToken[] { token }, 0), new Regex(FormattableString.Invariant(FormattableStringFactory.Create("\\A({0})", new object[] { token.Regex })), RegexOptions.Compiled));
					yield return Record.Create<RegularExpression, Regex>(RegularExpression.Create(new AbstractRegexToken[] { whiteSpaceToken, token }, 0), new Regex(FormattableString.Invariant(FormattableStringFactory.Create("\\A({0}{1})", new object[] { whiteSpaceToken.Regex, token.Regex })), RegexOptions.Compiled));
				}
				token = null;
			}
			IEnumerator<AbstractRegexToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x001E742C File Offset: 0x001E562C
		private static IReadOnlyList<string> LearnColumnNamesInSkippedRows(IReadOnlyList<IReadOnlyList<string>> skippedRows, string delimiter, ISet<int> emptyColumns)
		{
			Func<string, int, bool> <>9__0;
			for (int i = skippedRows.Count - 1; i >= 0; i--)
			{
				IEnumerable<string> enumerable = skippedRows[i];
				Func<string, string> func;
				if ((func = AutoWitnesses.<>O.<8>__TrimColumnNames) == null)
				{
					func = (AutoWitnesses.<>O.<8>__TrimColumnNames = new Func<string, string>(AutoWitnesses.TrimColumnNames));
				}
				IReadOnlyList<string> readOnlyList = enumerable.Select(func).ToList<string>();
				IEnumerable<string> enumerable2 = readOnlyList;
				Func<string, int, bool> func2;
				if ((func2 = <>9__0) == null)
				{
					func2 = (<>9__0 = (string _, int c) => !emptyColumns.Contains(c));
				}
				List<string> list = enumerable2.Where(func2).ToList<string>();
				int count = ((IReadOnlyCollection<string>)list).Count;
				if ((double)list.Select((string name) => !string.IsNullOrWhiteSpace(name) && AutoWitnesses.ColumnNameRegex.IsMatch(name)).ToList<bool>().Count((bool x) => x) > AutoWitnesses.ValidColumnsThreshold(count, delimiter) * (double)count)
				{
					return readOnlyList;
				}
			}
			return null;
		}

		// Token: 0x0600906D RID: 36973 RVA: 0x001E751C File Offset: 0x001E571C
		private static IReadOnlyList<string> LearnColumnNamesInData(IReadOnlyList<IReadOnlyList<string>> rows, string delimiter, ISet<int> emptyColumns, out int skip)
		{
			skip = 1;
			if (rows.Count <= 1)
			{
				return null;
			}
			IReadOnlyList<IReadOnlyList<string>> readOnlyList2;
			if (emptyColumns.Count != 0)
			{
				Func<string, int, bool> <>9__1;
				IReadOnlyList<IReadOnlyList<string>> readOnlyList = rows.Select(delegate(IReadOnlyList<string> row)
				{
					Func<string, int, bool> func4;
					if ((func4 = <>9__1) == null)
					{
						func4 = (<>9__1 = (string _, int i) => !emptyColumns.Contains(i));
					}
					return row.Where(func4).ToList<string>();
				}).ToList<List<string>>();
				readOnlyList2 = readOnlyList;
			}
			else
			{
				readOnlyList2 = rows;
			}
			IReadOnlyList<IReadOnlyList<string>> readOnlyList3 = readOnlyList2;
			int count = readOnlyList3[0].Count;
			int num = Math.Min(readOnlyList3.Count / 2, 10);
			while (skip <= num)
			{
				IEnumerable<string> enumerable = readOnlyList3[skip - 1];
				Func<string, string> func;
				if ((func = AutoWitnesses.<>O.<8>__TrimColumnNames) == null)
				{
					func = (AutoWitnesses.<>O.<8>__TrimColumnNames = new Func<string, string>(AutoWitnesses.TrimColumnNames));
				}
				IReadOnlyList<string> readOnlyList4 = enumerable.Select(func).ToList<string>();
				IReadOnlyList<bool> readOnlyList5 = readOnlyList4.Select((string name) => !string.IsNullOrWhiteSpace(name) && AutoWitnesses.ColumnNameRegex.IsMatch(name)).ToList<bool>();
				if ((double)readOnlyList5.Count((bool x) => x) > AutoWitnesses.ValidColumnsThreshold(count, delimiter) * (double)count)
				{
					bool flag = false;
					int num3;
					int i;
					for (i = 0; i < readOnlyList4.Count; i = num3)
					{
						if (readOnlyList5[i])
						{
							string text = readOnlyList3[skip - 1][i];
							ISet<RegularExpression> set = AutoWitnesses.LearnLongestColumnNameRegexes(text);
							ISet<string> set2 = (from row in readOnlyList3.Skip(skip)
								select row[i] into s
								where !string.IsNullOrEmpty(s)
								select s).ConvertToHashSet<string>();
							decimal num2;
							if (set2.Count != 0 || !decimal.TryParse(text, out num2))
							{
								foreach (string text2 in set2)
								{
									ISet<RegularExpression> set3 = AutoWitnesses.LearnLongestColumnNameRegexes(text2);
									set.ExceptWith(set3);
									if (set.Count == 0)
									{
										break;
									}
								}
								if (set.Count > 0)
								{
									flag = true;
								}
								if (!flag && set2.Count > 1 && !set2.Contains(readOnlyList4[i]) && 1.0 * (double)set2.Count / (double)(readOnlyList3.Count - skip) <= 0.2)
								{
									flag = true;
								}
								if (flag)
								{
									IEnumerable<string> enumerable2 = rows[skip - 1];
									Func<string, string> func2;
									if ((func2 = AutoWitnesses.<>O.<8>__TrimColumnNames) == null)
									{
										func2 = (AutoWitnesses.<>O.<8>__TrimColumnNames = new Func<string, string>(AutoWitnesses.TrimColumnNames));
									}
									return enumerable2.Select(func2).ToList<string>();
								}
							}
						}
						num3 = i + 1;
					}
					break;
				}
				IEnumerable<string> enumerable3 = readOnlyList4;
				Func<string, bool> func3;
				if ((func3 = AutoWitnesses.<>O.<9>__IsNullOrEmpty) == null)
				{
					func3 = (AutoWitnesses.<>O.<9>__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty));
				}
				if (enumerable3.Count(func3) <= count / 2)
				{
					return null;
				}
				skip++;
			}
			return null;
		}

		// Token: 0x0600906E RID: 36974 RVA: 0x001E7814 File Offset: 0x001E5A14
		private static ISet<RegularExpression> LearnLongestColumnNameRegexes(string s)
		{
			if (s == null)
			{
				return new HashSet<RegularExpression>();
			}
			List<RegularExpression> list = (from re in RegularExpression.LearnRightMatches(new StringRegion(s, AutoWitnesses.ColumnNameLearningTokens.Value), 0U, 2, 1)
				where re.Count > 0
				select re).ToList<RegularExpression>();
			if (list.Count == 0)
			{
				return new HashSet<RegularExpression>();
			}
			int maxLength = list.Max((RegularExpression re) => re.Count);
			return list.Where((RegularExpression re) => re.Count == maxLength).ConvertToHashSet<RegularExpression>();
		}

		// Token: 0x0600906F RID: 36975 RVA: 0x001E78C2 File Offset: 0x001E5AC2
		private static string TrimColumnNames(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			return s.Trim((char[])AutoWitnesses.ColumnNameTrimChars);
		}

		// Token: 0x06009070 RID: 36976 RVA: 0x000B4ACD File Offset: 0x000B2CCD
		private static bool IsAsciiLetter(char ch)
		{
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
		}

		// Token: 0x06009071 RID: 36977 RVA: 0x000B4ABC File Offset: 0x000B2CBC
		private static bool IsAsciiDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x06009072 RID: 36978 RVA: 0x001E78DE File Offset: 0x001E5ADE
		private static bool IsAsciiLetterOrDigit(char ch)
		{
			return AutoWitnesses.IsAsciiLetter(ch) || AutoWitnesses.IsAsciiDigit(ch);
		}

		// Token: 0x06009073 RID: 36979 RVA: 0x001E78F0 File Offset: 0x001E5AF0
		private static float DelimiterScore(string del)
		{
			if (del != null)
			{
				if (del.Length > 0)
				{
					if (del.All((char ch) => ch == ' '))
					{
						goto IL_004F;
					}
				}
				if (string.IsNullOrEmpty(del))
				{
					return 0f;
				}
				float num = AutoWitnesses.StandardDelimiterScores.GetOrDefault(del, 0.5f);
				if (AutoWitnesses.UnlikelyDelimiterChars.Any((char ch) => del.Contains(ch)))
				{
					num *= 0.1f;
				}
				return num;
			}
			IL_004F:
			return 0.4f;
		}

		// Token: 0x06009074 RID: 36980 RVA: 0x001E799C File Offset: 0x001E5B9C
		private static float QuoteScore(Optional<char> quote)
		{
			if (!quote.HasValue)
			{
				return 0.9f;
			}
			return AutoWitnesses.StandardQuoteScores.GetOrDefault(quote.Value, 0.5f);
		}

		// Token: 0x06009075 RID: 36981 RVA: 0x001E79C3 File Offset: 0x001E5BC3
		private static float EscapeScore(Optional<char> escape)
		{
			if (!escape.HasValue)
			{
				return 0.9f;
			}
			return AutoWitnesses.StandardEscapeScores.GetOrDefault(escape.Value, 0.5f);
		}

		// Token: 0x06009077 RID: 36983 RVA: 0x001E7BB2 File Offset: 0x001E5DB2
		[CompilerGenerated]
		internal static void <LearnDelimiterConfigs>g__AddChar|41_0(IDictionary<string, HashSet<Optional<char>>> set, string delimiter, char ch)
		{
			if (!set.ContainsKey(delimiter))
			{
				set[delimiter] = new HashSet<Optional<char>>();
			}
			set[delimiter].Add(ch.Some<char>());
		}

		// Token: 0x06009078 RID: 36984 RVA: 0x001E7BDC File Offset: 0x001E5DDC
		[CompilerGenerated]
		internal static bool <LearnHeader>g__IsPreferredColumnName|47_0(string name)
		{
			return !string.IsNullOrWhiteSpace(name) && AutoWitnesses.PreferredColumnNameRegex.IsMatch(name);
		}

		// Token: 0x04003AF7 RID: 15095
		private static readonly IDictionary<string, float> StandardDelimiterScores = new Dictionary<string, float>
		{
			{ ",", 1f },
			{ "\t", 1f },
			{ ";", 0.9f },
			{ "|", 0.8f }
		};

		// Token: 0x04003AF8 RID: 15096
		private const float BadDataDelimiterFactor = 0.7f;

		// Token: 0x04003AF9 RID: 15097
		private static readonly IReadOnlyDictionary<char, char> StandardParens = new Dictionary<char, char>
		{
			{ ')', '(' },
			{ ']', '[' },
			{ '}', '{' }
		};

		// Token: 0x04003AFA RID: 15098
		private static readonly ISet<char> UnlikelyDelimiterChars = new HashSet<char> { '"', '(', ')', '.', '-', '/', '@', ':', '\\' };

		// Token: 0x04003AFB RID: 15099
		private const float UnlikelyDelimiterCharScoreFactor = 0.1f;

		// Token: 0x04003AFC RID: 15100
		private const float DefaultDelimiterScore = 0.5f;

		// Token: 0x04003AFD RID: 15101
		private const float FwAllSpaceDelimiterScore = 0.4f;

		// Token: 0x04003AFE RID: 15102
		private const float AllSpaceExtraSpaceFactor = 0.5f;

		// Token: 0x04003AFF RID: 15103
		private const float NoDelimiterScore = 0f;

		// Token: 0x04003B00 RID: 15104
		private static readonly ISet<char> IgnoreDelimiterChars = new HashSet<char> { '\r', '\n', '"' };

		// Token: 0x04003B01 RID: 15105
		private const int MaxDelimiterLength = 5;

		// Token: 0x04003B02 RID: 15106
		private static readonly IDictionary<char, float> StandardQuoteScores = new Dictionary<char, float>
		{
			{ '"', 1f },
			{ '|', 0.7f }
		};

		// Token: 0x04003B03 RID: 15107
		private static readonly IDictionary<char, float> StandardEscapeScores = new Dictionary<char, float> { { '\\', 1f } };

		// Token: 0x04003B04 RID: 15108
		private const float DefaultQuoteAndEscapeScore = 0.5f;

		// Token: 0x04003B05 RID: 15109
		private const float EmptyQuoteAndEscapeScore = 0.9f;

		// Token: 0x04003B06 RID: 15110
		private static readonly ISet<string> LikelyCommentStrings = new HashSet<string> { "#", "%" };

		// Token: 0x04003B07 RID: 15111
		private static readonly IReadOnlyCollection<char> ColumnNameTrimChars = new char[] { '"', ' ', '\t' };

		// Token: 0x04003B08 RID: 15112
		private static readonly Regex ColumnNameRegex = new Regex("^[\\d._]*[\\p{L}\\s/._\\-()\\[\\]#]+\\d*\\)?$", RegexOptions.Compiled);

		// Token: 0x04003B09 RID: 15113
		private static readonly Regex PreferredColumnNameRegex = new Regex("^[\\p{L}_#]+[\\p{L}\\d\\s/._\\-()\\[\\]#]*$", RegexOptions.Compiled);

		// Token: 0x04003B0A RID: 15114
		private static readonly Lazy<IReadOnlyDictionary<string, Token>> ColumnNameLearningTokens = new Lazy<IReadOnlyDictionary<string, Token>>(delegate
		{
			Dictionary<string, Token> dictionary = Token.NonDisjunctiveTokens.ToDictionary<string, Token>();
			dictionary.Remove("Digits");
			dictionary.Remove("Alphanum");
			return dictionary;
		});

		// Token: 0x04003B0B RID: 15115
		private static readonly Lazy<IReadOnlyList<Record<RegularExpression, Regex>>> PrefixRegexes = new Lazy<IReadOnlyList<Record<RegularExpression, Regex>>>(() => AutoWitnesses.GetPrefixRegexes().ToList<Record<RegularExpression, Regex>>());

		// Token: 0x04003B0C RID: 15116
		private const string WhiteSpaceTokenName = "WhiteSpace";

		// Token: 0x04003B0D RID: 15117
		private const int MaxColumnNameLearningRows = 20;

		// Token: 0x04003B0E RID: 15118
		private readonly Options _options;

		// Token: 0x020012AF RID: 4783
		private struct DelimiterConfig
		{
			// Token: 0x06009079 RID: 36985 RVA: 0x001E7BF3 File Offset: 0x001E5DF3
			public DelimiterConfig(string delimiter, IReadOnlyList<Optional<char>> quoteChars = null, IReadOnlyList<Optional<char>> escapeChars = null)
			{
				this.Delimiter = delimiter;
				this.QuoteChars = quoteChars ?? new List<Optional<char>>();
				this.EscapeChars = escapeChars ?? new List<Optional<char>>();
				this.DelimiterScore = AutoWitnesses.DelimiterScore(delimiter);
			}

			// Token: 0x170018D0 RID: 6352
			// (get) Token: 0x0600907A RID: 36986 RVA: 0x001E7C28 File Offset: 0x001E5E28
			public readonly string Delimiter { get; }

			// Token: 0x170018D1 RID: 6353
			// (get) Token: 0x0600907B RID: 36987 RVA: 0x001E7C30 File Offset: 0x001E5E30
			public readonly float DelimiterScore { get; }

			// Token: 0x170018D2 RID: 6354
			// (get) Token: 0x0600907C RID: 36988 RVA: 0x001E7C38 File Offset: 0x001E5E38
			public readonly IReadOnlyList<Optional<char>> QuoteChars { get; }

			// Token: 0x170018D3 RID: 6355
			// (get) Token: 0x0600907D RID: 36989 RVA: 0x001E7C40 File Offset: 0x001E5E40
			public readonly IReadOnlyList<Optional<char>> EscapeChars { get; }

			// Token: 0x170018D4 RID: 6356
			// (get) Token: 0x0600907E RID: 36990 RVA: 0x001E7C48 File Offset: 0x001E5E48
			public bool IsStandardDelimiter
			{
				get
				{
					return !string.IsNullOrEmpty(this.Delimiter) && AutoWitnesses.StandardDelimiterScores.ContainsKey(this.Delimiter);
				}
			}

			// Token: 0x0600907F RID: 36991 RVA: 0x001E7C6C File Offset: 0x001E5E6C
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"(",
					this.Delimiter.ToLiteral(null),
					", ",
					this.QuoteChars.ToLiteral(null),
					", ",
					this.EscapeChars.ToLiteral(null),
					")"
				});
			}
		}

		// Token: 0x020012B0 RID: 4784
		private class TopK : IEnumerable<LearnResult>, IEnumerable
		{
			// Token: 0x06009080 RID: 36992 RVA: 0x001E7CD1 File Offset: 0x001E5ED1
			public TopK(int? k)
			{
				this._results = new List<Record<LearnResult, float>>(k.GetValueOrDefault());
				this._k = k;
			}

			// Token: 0x170018D5 RID: 6357
			// (get) Token: 0x06009081 RID: 36993 RVA: 0x001E7CF2 File Offset: 0x001E5EF2
			public float LowestScore
			{
				get
				{
					if (this._results.Count != 0)
					{
						return this._results[this.Count - 1].Item2;
					}
					return float.MinValue;
				}
			}

			// Token: 0x170018D6 RID: 6358
			// (get) Token: 0x06009082 RID: 36994 RVA: 0x001E7D1F File Offset: 0x001E5F1F
			public int Count
			{
				get
				{
					return this._results.Count;
				}
			}

			// Token: 0x06009083 RID: 36995 RVA: 0x001E7D2C File Offset: 0x001E5F2C
			public void Add(LearnResult result, float score)
			{
				if (score <= this.LowestScore && this._k != null && this._results.Count >= this._k.Value)
				{
					return;
				}
				Record<LearnResult, float> record = Record.Create<LearnResult, float>(result, score);
				for (int i = 0; i < this._results.Count; i++)
				{
					Record<LearnResult, float> record2 = this._results[i];
					if (record.Item2 > record2.Item2)
					{
						this._results[i] = record;
						record = record2;
					}
				}
				if (this._k == null || this._results.Count < this._k.Value)
				{
					this._results.Add(record);
				}
			}

			// Token: 0x06009084 RID: 36996 RVA: 0x001E7DE1 File Offset: 0x001E5FE1
			public IEnumerator<LearnResult> GetEnumerator()
			{
				return this._results.Select((Record<LearnResult, float> x) => x.Item1).GetEnumerator();
			}

			// Token: 0x06009085 RID: 36997 RVA: 0x001E7E12 File Offset: 0x001E6012
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04003B13 RID: 15123
			private readonly List<Record<LearnResult, float>> _results;

			// Token: 0x04003B14 RID: 15124
			private readonly int? _k;
		}

		// Token: 0x020012B2 RID: 4786
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003B17 RID: 15127
			public static Func<IReadOnlyList<string>, bool> <0>__IsBadDataRow;

			// Token: 0x04003B18 RID: 15128
			public static Func<string, StringRegion> <1>__CreateStringRegion;

			// Token: 0x04003B19 RID: 15129
			public static Func<string, float> <2>__DelimiterScore;

			// Token: 0x04003B1A RID: 15130
			public static Func<Optional<char>, float> <3>__QuoteScore;

			// Token: 0x04003B1B RID: 15131
			public static Func<Optional<char>, float> <4>__EscapeScore;

			// Token: 0x04003B1C RID: 15132
			public static Func<IReadOnlyList<string>, bool> <5>__IsCsvEmptyRow;

			// Token: 0x04003B1D RID: 15133
			public static Func<IReadOnlyList<string>, bool> <6>__IsFwEmptyRow;

			// Token: 0x04003B1E RID: 15134
			public static Func<string, bool> <7>__IsNullOrWhiteSpace;

			// Token: 0x04003B1F RID: 15135
			public static Func<string, string> <8>__TrimColumnNames;

			// Token: 0x04003B20 RID: 15136
			public static Func<string, bool> <9>__IsNullOrEmpty;
		}
	}
}
