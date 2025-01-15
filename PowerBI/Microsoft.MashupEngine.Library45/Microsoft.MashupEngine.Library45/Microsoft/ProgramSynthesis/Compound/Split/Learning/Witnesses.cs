using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009B3 RID: 2483
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06003B75 RID: 15221 RVA: 0x000B8148 File Offset: 0x000B6348
		private splitRecords LearnAutoMultiRecord(IReadOnlyList<StringRegion> records, CancellationToken cancel)
		{
			splitRecords splitRecords = default(splitRecords);
			double num = 0.0;
			foreach (Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]> record in this.LearnPrimarySelectors(records, cancel))
			{
				primarySelector primarySelector;
				StringRegion[][] array;
				columnSelector columnSelector;
				MultiRecordMatch[] array2;
				record.Deconstruct(out primarySelector, out array, out columnSelector, out array2);
				primarySelector primarySelector2 = primarySelector;
				StringRegion[][] array3 = array;
				columnSelector columnSelector2 = columnSelector;
				MultiRecordMatch[] array4 = array2;
				if (cancel.IsCancellationRequested)
				{
					break;
				}
				Record<columnSelector, MultiRecordMatch?[]>[] array5 = this.LearnKeySeps(array3, array4).ToArray<Record<columnSelector, MultiRecordMatch?[]>>();
				Record<columnSelector, MultiRecordMatch?[]>[] array6 = this.LearnFwKeySeps(array3, array4).ToArray<Record<columnSelector, MultiRecordMatch?[]>>();
				foreach (Record<columnSelectorList, List<MultiRecordMatch?>[]> record2 in this.LearnColumnSelectorLists(array5, array3.Length, cancel).Concat(this.LearnColumnSelectorLists(array6, array3.Length, cancel)))
				{
					columnSelectorList columnSelectorList;
					List<MultiRecordMatch?>[] array7;
					record2.Deconstruct(out columnSelectorList, out array7);
					columnSelectorList columnSelectorList2 = columnSelectorList;
					List<MultiRecordMatch?>[] array8 = array7;
					if (cancel.IsCancellationRequested)
					{
						break;
					}
					for (int i = 0; i < array4.Length; i++)
					{
						array8[i].Insert(0, new MultiRecordMatch?(array4[i]));
					}
					if (!columnSelectorList2.Is_Empty(this._build))
					{
						columnSelectorList columnSelectorList3 = this._build.Node.Rule.SelectorList(columnSelector2, columnSelectorList2);
						double num2 = this.ResultScore(records, array3, array8, columnSelectorList3);
						if (num2 > num)
						{
							num = num2;
							splitRecords = this._build.Node.Rule.MultiRecordSplit(this._build.Node.Rule.LetMultiRecordSplit(primarySelector2, this._build.Node.Rule.MapColumnSelector(columnSelectorList3, this._build.Node.Variable.rowRecords)));
						}
					}
				}
			}
			return splitRecords;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000B834C File Offset: 0x000B654C
		private double ResultScore(IEnumerable<StringRegion> records, StringRegion[][] subRecords, List<MultiRecordMatch?>[] matches, columnSelectorList columnSelectorList)
		{
			IReadOnlyList<double> selectorWeights = this.GetSelectorWeights(columnSelectorList);
			long num = records.Sum((StringRegion rec) => (long)((ulong)rec.Length));
			double num2 = 0.0;
			foreach (Record<StringRegion[], List<MultiRecordMatch?>> record in subRecords.ZipWith(matches))
			{
				StringRegion[] array;
				List<MultiRecordMatch?> list;
				record.Deconstruct(out array, out list);
				StringRegion[] array2 = array;
				List<MultiRecordMatch?> list2 = list;
				int num3 = 0;
				foreach (MultiRecordMatch? multiRecordMatch in list2)
				{
					if (multiRecordMatch != null)
					{
						double num4 = selectorWeights[num3++];
						MultiRecordMatch value = multiRecordMatch.Value;
						for (uint num5 = value.StartRecord; num5 <= value.EndRecord; num5 += 1U)
						{
							StringRegion stringRegion = array2[(int)num5];
							uint num6 = ((num5 < value.EndRecord) ? stringRegion.Length : value.EndIndex);
							if (num5 == value.StartRecord)
							{
								num6 -= value.StartIndex;
							}
							num2 += num6 * num4;
						}
					}
				}
			}
			double num7 = num2 / (double)num;
			if (num7 < 0.5)
			{
				return double.MinValue;
			}
			return num7;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000B84C4 File Offset: 0x000B66C4
		private IReadOnlyList<double> GetSelectorWeights(columnSelectorList list)
		{
			List<double> list2 = new List<double>();
			SelectorList selectorList;
			while (list.Is_SelectorList(this._build, out selectorList))
			{
				list = selectorList.columnSelectorList;
				columnSelector columnSelector = selectorList.columnSelector;
				list2.Add(Witnesses.ColumnSelectorWeights[columnSelector.Node.GrammarRule.Id]);
			}
			return list2;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000B851D File Offset: 0x000B671D
		private IEnumerable<Record<columnSelectorList, List<MultiRecordMatch?>[]>> LearnColumnSelectorLists(IList<Record<columnSelector, MultiRecordMatch?[]>> selectorsAndMatches, int subRecordsNum, CancellationToken cancel)
		{
			if (selectorsAndMatches.Count == 0 || cancel.IsCancellationRequested)
			{
				yield return Record.Create<columnSelectorList, List<MultiRecordMatch?>[]>(this._build.Node.Rule.Empty(), (from _ in Enumerable.Range(0, subRecordsNum)
					select new List<MultiRecordMatch?>()).ToArray<List<MultiRecordMatch?>>());
				yield break;
			}
			columnSelector firstSelector;
			MultiRecordMatch?[] firstMatches;
			selectorsAndMatches[0].Deconstruct(out firstSelector, out firstMatches);
			List<Record<columnSelector, MultiRecordMatch?[]>> list = new List<Record<columnSelector, MultiRecordMatch?[]>>();
			Record<columnSelector, MultiRecordMatch?[]>[] restSelectorsAndMatches = selectorsAndMatches.Skip(1).ToArray<Record<columnSelector, MultiRecordMatch?[]>>();
			foreach (Record<columnSelector, MultiRecordMatch?[]> record in restSelectorsAndMatches)
			{
				columnSelector columnSelector;
				MultiRecordMatch?[] array2;
				record.Deconstruct(out columnSelector, out array2);
				if (!firstMatches.ZipWith(array2).Any((Record<MultiRecordMatch?, MultiRecordMatch?> ms) => ms.Item1 != null && ms.Item1.Value.OverlapsWith(ms.Item2)))
				{
					list.Add(record);
				}
			}
			bool noOverlap = list.Count == selectorsAndMatches.Count - 1;
			IEnumerator<Record<columnSelectorList, List<MultiRecordMatch?>[]>> enumerator;
			if (noOverlap || !firstSelector.Is_KthTwoLineKeyValue(this._build))
			{
				foreach (Record<columnSelectorList, List<MultiRecordMatch?>[]> record2 in this.LearnColumnSelectorLists(list, subRecordsNum, cancel))
				{
					columnSelectorList columnSelectorList;
					List<MultiRecordMatch?>[] array3;
					record2.Deconstruct(out columnSelectorList, out array3);
					columnSelectorList columnSelectorList2 = columnSelectorList;
					List<MultiRecordMatch?>[] array4 = array3;
					for (int j = 0; j < subRecordsNum; j++)
					{
						array4[j].Add(firstMatches[j]);
					}
					yield return Record.Create<columnSelectorList, List<MultiRecordMatch?>[]>(this._build.Node.Rule.SelectorList(firstSelector, columnSelectorList2), array4);
				}
				enumerator = null;
			}
			if (noOverlap)
			{
				yield break;
			}
			foreach (Record<columnSelectorList, List<MultiRecordMatch?>[]> record3 in this.LearnColumnSelectorLists(restSelectorsAndMatches, subRecordsNum, cancel))
			{
				yield return record3;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000B8542 File Offset: 0x000B6742
		private IEnumerable<Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>> LearnPrimarySelectors(IReadOnlyList<StringRegion> records, CancellationToken cancel)
		{
			StringRegion[][] breakLineResult = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.BreakLine(records).ToArray<StringRegion[]>();
			StringRegion[] array = breakLineResult.Select((StringRegion[] rows) => rows.FirstOrDefault<StringRegion>()).ToArray<StringRegion>();
			MultiRecordMatch[] array2 = breakLineResult.Select((StringRegion[] subRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthLine(1, subRecs).Value).ToArray<MultiRecordMatch>();
			if (array.Length > 1)
			{
				if (!this.LearnPrimaryKeySeps(array).Any<Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>>())
				{
					yield return Record.Create<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>(this._build.Node.Rule.BreakLine(this._build.Node.Variable.records), breakLineResult, this._build.Node.Rule.KthLine(this._build.Node.Rule.k(1), this._build.Node.Variable.rowRecord), array2);
				}
				else
				{
					MultiRecordMatch[] array3 = array.Select((StringRegion val) => new MultiRecordMatch(0U, 0U, 0U, 0U, val.Slice(val.Start, val.Start))).ToArray<MultiRecordMatch>();
					foreach (Record<columnSelector, MultiRecordMatch?[]> record in this.LearnKeySeps(breakLineResult, array3))
					{
						columnSelector columnSelector;
						MultiRecordMatch?[] array4;
						record.Deconstruct(out columnSelector, out array4);
						columnSelector columnSelector2 = columnSelector;
						MultiRecordMatch?[] array5 = array4;
						MultiRecordMatch[] array6 = (from m in array5
							where m != null
							select m.Value).ToArray<MultiRecordMatch>();
						if (array6.Length == array5.Length)
						{
							yield return Record.Create<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>(this._build.Node.Rule.BreakLine(this._build.Node.Variable.records), breakLineResult, columnSelector2, array6);
						}
					}
					IEnumerator<Record<columnSelector, MultiRecordMatch?[]>> enumerator = null;
				}
			}
			foreach (Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]> record2 in this.LearnPrimaryKeySeps(records))
			{
				yield return record2;
				if (cancel.IsCancellationRequested)
				{
					yield break;
				}
			}
			IEnumerator<Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000B8560 File Offset: 0x000B6760
		private static int FirstIndexOfAny(string source, string[] values, out string value)
		{
			value = null;
			for (int i = 0; i < source.Length; i++)
			{
				foreach (string text in values)
				{
					if (source.Length >= i + text.Length)
					{
						bool flag = true;
						for (int k = 0; k < text.Length; k++)
						{
							if (source[i + k] != text[k])
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							value = text;
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000B85E0 File Offset: 0x000B67E0
		private static bool IsKeyValueRecord(StringRegion record, out string key, out string sep)
		{
			key = null;
			sep = null;
			string value = record.Value.Trim((char[])Witnesses.WhitespaceChars);
			if (string.IsNullOrEmpty(value) || Witnesses.BadKeyValueLines.Any((Regex re) => re.IsMatch(value)))
			{
				return false;
			}
			int num = Witnesses.FirstIndexOfAny(value, (string[])Witnesses.LikelySeparators, out sep);
			if (num > 0)
			{
				string potentialKey2 = value.Substring(0, num).TrimEnd((char[])Witnesses.WhitespaceChars);
				if (Witnesses.LikelyKeys.Any((Regex keyRe) => keyRe.IsMatch(potentialKey2)))
				{
					key = potentialKey2;
					return true;
				}
			}
			else
			{
				sep = null;
				int num2 = value.TrimStart((char[])Witnesses.WhitespaceChars).IndexOfAny((char[])Witnesses.WhitespaceChars);
				if (num2 > 0)
				{
					string potentialKey = value.Substring(0, num2).TrimStart((char[])Witnesses.WhitespaceChars);
					if (Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsQuotedValue(value.Substring(num2)) && Witnesses.LikelyKeys.Any((Regex keyRe) => keyRe.IsMatch(potentialKey)))
					{
						key = potentialKey;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000B8734 File Offset: 0x000B6934
		private static bool IsKeyValueFwRecord(StringRegion record, out string key, out int fwPos)
		{
			key = null;
			fwPos = 0;
			string text = record.Value.TrimStart((char[])Witnesses.WhitespaceChars);
			int num = text.IndexOfAny((char[])Witnesses.WhitespaceChars);
			if (num < 0)
			{
				return false;
			}
			string potentialKey = text.Substring(0, num);
			if (!Witnesses.LikelyKeys.Any((Regex keyRe) => keyRe.IsMatch(potentialKey)))
			{
				return false;
			}
			string text2 = text.Substring(num);
			string text3 = text2.TrimStart((char[])Witnesses.WhitespaceChars);
			if (string.IsNullOrEmpty(text3))
			{
				return false;
			}
			key = potentialKey;
			fwPos = text2.Length - text3.Length;
			return true;
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x000B87E0 File Offset: 0x000B69E0
		private IEnumerable<Record<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>> LearnPrimaryKeySeps(IReadOnlyList<StringRegion> records)
		{
			HashSet<Record<string, string>> learned = new HashSet<Record<string, string>>();
			foreach (StringRegion stringRegion in records)
			{
				string key;
				string sep;
				if (Witnesses.IsKeyValueRecord(stringRegion, out key, out sep))
				{
					Record<string, string> record = Record.Create<string, string>(key, sep);
					if (!learned.Contains(record))
					{
						learned.Add(record);
						if (sep == null)
						{
							StringRegion[][] array = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KeyQuote(key, records).ToArray<StringRegion[]>();
							if (array.Length > 1)
							{
								MultiRecordMatch[] array2 = array.Select((StringRegion[] subRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthKeyQuote(key, 1, this.Options.NewLineRecordSeparator, subRecs).Value).ToArray<MultiRecordMatch>();
								if (array2.Length == array.Length)
								{
									yield return Record.Create<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>(this._build.Node.Rule.KeyQuote(this._build.Node.Rule.key(key), this._build.Node.Variable.records), array, this._build.Node.Rule.KthKeyQuote(this._build.Node.Rule.key(key), this._build.Node.Rule.k(1), this._build.Node.Rule.newLineSep(this.Options.NewLineRecordSeparator), this._build.Node.Variable.rowRecord), array2);
								}
							}
						}
						else
						{
							StringRegion[][] array3 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KeyValue(key, sep, records).ToArray<StringRegion[]>();
							if (array3.Length > 1)
							{
								MultiRecordMatch[] array4 = array3.Select((StringRegion[] subRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthKeyValue(key, sep, 1, subRecs).Value).ToArray<MultiRecordMatch>();
								if (array4.All((MultiRecordMatch m) => string.IsNullOrWhiteSpace(m.Value.Value)))
								{
									array3 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.TwoLineKeyValue(key, sep, records).ToArray<StringRegion[]>();
									array4 = (from subRecs in array3
										select Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthTwoLineKeyValue(key, sep, 1, subRecs) into m
										where m.HasValue
										select m.Value).ToArray<MultiRecordMatch>();
									if (array4.Length == array3.Length)
									{
										if (!array4.All((MultiRecordMatch m) => string.IsNullOrWhiteSpace(m.Value.Value)))
										{
											yield return Record.Create<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>(this._build.Node.Rule.TwoLineKeyValue(this._build.Node.Rule.key(key), this._build.Node.Rule.sep(sep), this._build.Node.Variable.records), array3, this._build.Node.Rule.KthTwoLineKeyValue(this._build.Node.Rule.key(key), this._build.Node.Rule.sep(sep), this._build.Node.Rule.k(1), this._build.Node.Variable.rowRecord), array4);
										}
									}
								}
								else
								{
									yield return Record.Create<primarySelector, StringRegion[][], columnSelector, MultiRecordMatch[]>(this._build.Node.Rule.KeyValue(this._build.Node.Rule.key(key), this._build.Node.Rule.sep(sep), this._build.Node.Variable.records), array3, this._build.Node.Rule.KthKeyValue(this._build.Node.Rule.key(key), this._build.Node.Rule.sep(sep), this._build.Node.Rule.k(1), this._build.Node.Variable.rowRecord), array4);
								}
							}
						}
					}
				}
			}
			IEnumerator<StringRegion> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x000B87F8 File Offset: 0x000B69F8
		private bool CheckKeyValueResult(MultiRecordMatch[] primaryMatches, MultiRecordMatch?[] resultMatches)
		{
			if (resultMatches.Any((MultiRecordMatch? m) => m != null))
			{
				return primaryMatches.ZipWith(resultMatches).All((Record<MultiRecordMatch, MultiRecordMatch?> ms) => ms.Item2 == null || !ms.Item1.OverlapsWith(ms.Item2));
			}
			return false;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x000B885C File Offset: 0x000B6A5C
		private bool CheckKeyValueFwResult(MultiRecordMatch[] primaryMatches, MultiRecordMatch?[] resultMatches)
		{
			if (!this.CheckKeyValueResult(primaryMatches, resultMatches))
			{
				return false;
			}
			if (resultMatches.All((MultiRecordMatch? m) => string.IsNullOrWhiteSpace((m != null) ? m.GetValueOrDefault().Value.Value : null)))
			{
				return false;
			}
			return !(from m in resultMatches
				where m != null
				select m.Value.Value.Value.Trim()).ConvertToHashSet<string>().All((string v) => Witnesses.LikelySeparators.Any(new Func<string, bool>(v.StartsWith)));
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x000B8913 File Offset: 0x000B6B13
		private IEnumerable<Record<columnSelector, MultiRecordMatch?[]>> LearnFwKeySeps(StringRegion[][] records, MultiRecordMatch[] primaryMatches)
		{
			HashSet<Record<string, int, int>> learnedKeyFw = new HashSet<Record<string, int, int>>();
			foreach (StringRegion[] array2 in records)
			{
				Dictionary<Record<string, int>, int> keyFwCount = new Dictionary<Record<string, int>, int>();
				StringRegion[] array3 = array2;
				for (int j = 0; j < array3.Length; j++)
				{
					StringRegion stringRegion = array3[j];
					string fwKey;
					int fwPos;
					if (Witnesses.IsKeyValueFwRecord(stringRegion, out fwKey, out fwPos))
					{
						Record<string, int> record = Record.Create<string, int>(fwKey, fwPos);
						int kFw = keyFwCount.GetOrAdd(record, 1);
						Dictionary<Record<string, int>, int> dictionary = keyFwCount;
						Record<string, int> record2 = record;
						int num = dictionary[record2];
						dictionary[record2] = num + 1;
						Record<string, int, int> record3 = Record.Create<string, int, int>(fwKey, fwPos, kFw);
						if (!learnedKeyFw.Contains(record3))
						{
							MultiRecordMatch?[] array4 = records.Select((StringRegion[] rowRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthKeyValueFw(fwKey, fwPos, kFw, this.Options.NewLineRecordSeparator, rowRecs)).Select(delegate(Optional<MultiRecordMatch> m)
							{
								if (!m.HasValue)
								{
									return null;
								}
								return new MultiRecordMatch?(m.Value);
							}).ToArray<MultiRecordMatch?>();
							if (this.CheckKeyValueFwResult(primaryMatches, array4))
							{
								learnedKeyFw.Add(record3);
								yield return Record.Create<columnSelector, MultiRecordMatch?[]>(this._build.Node.Rule.KthKeyValueFw(this._build.Node.Rule.key(fwKey), this._build.Node.Rule.fwPos(fwPos), this._build.Node.Rule.k(kFw), this._build.Node.Rule.newLineSep(this.Options.NewLineRecordSeparator), this._build.Node.Variable.rowRecord), array4);
							}
						}
					}
				}
				array3 = null;
				keyFwCount = null;
			}
			StringRegion[][] array = null;
			yield break;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x000B8931 File Offset: 0x000B6B31
		private IEnumerable<Record<columnSelector, MultiRecordMatch?[]>> LearnKeySeps(StringRegion[][] records, MultiRecordMatch[] primaryMatches)
		{
			HashSet<Record<string, string, int>> learnedKeySeps = new HashSet<Record<string, string, int>>();
			HashSet<Record<string, string, int>> learnedKeySepTwoLines = new HashSet<Record<string, string, int>>();
			HashSet<Record<string, int>> learnedKeyQuotes = new HashSet<Record<string, int>>();
			foreach (StringRegion[] array2 in records)
			{
				Dictionary<Record<string, string>, int> keySepsCount = new Dictionary<Record<string, string>, int>();
				Dictionary<Record<string, string>, int> keySepTwoLinesCount = new Dictionary<Record<string, string>, int>();
				Dictionary<string, int> keyQuotesCount = new Dictionary<string, int>();
				foreach (StringRegion stringRegion in array2)
				{
					Witnesses.<>c__DisplayClass18_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass18_0();
					CS$<>8__locals1.<>4__this = this;
					if (Witnesses.IsKeyValueRecord(stringRegion, out CS$<>8__locals1.key, out CS$<>8__locals1.sep))
					{
						Record<string, string> keySep = Record.Create<string, string>(CS$<>8__locals1.key, CS$<>8__locals1.sep);
						CS$<>8__locals1.k = keySepsCount.GetOrAdd(keySep, 1);
						Dictionary<Record<string, string>, int> dictionary = keySepsCount;
						Record<string, string> record = keySep;
						int num = dictionary[record];
						dictionary[record] = num + 1;
						Record<string, string, int> kthKeySep = Record.Create<string, string, int>(CS$<>8__locals1.key, CS$<>8__locals1.sep, CS$<>8__locals1.k);
						MultiRecordMatch?[] result = records.Select((StringRegion[] rowRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthKeyValue(CS$<>8__locals1.key, CS$<>8__locals1.sep, CS$<>8__locals1.k, rowRecs)).Select(delegate(Optional<MultiRecordMatch> m)
						{
							if (!m.HasValue)
							{
								return null;
							}
							return new MultiRecordMatch?(m.Value);
						}).ToArray<MultiRecordMatch?>();
						if (CS$<>8__locals1.sep != null && !learnedKeySeps.Contains(kthKeySep))
						{
							learnedKeySeps.Add(kthKeySep);
							if (this.CheckKeyValueResult(primaryMatches, result))
							{
								yield return Record.Create<columnSelector, MultiRecordMatch?[]>(this._build.Node.Rule.KthKeyValue(this._build.Node.Rule.key(CS$<>8__locals1.key), this._build.Node.Rule.sep(CS$<>8__locals1.sep), this._build.Node.Rule.k(CS$<>8__locals1.k), this._build.Node.Variable.rowRecord), result);
							}
						}
						if (CS$<>8__locals1.sep != null)
						{
							if (result.All((MultiRecordMatch? m) => string.IsNullOrWhiteSpace((m != null) ? m.GetValueOrDefault().Value.Value : null)))
							{
								int k3 = keySepTwoLinesCount.GetOrAdd(keySep, 1);
								Dictionary<Record<string, string>, int> dictionary2 = keySepTwoLinesCount;
								record = keySep;
								num = dictionary2[record];
								dictionary2[record] = num + 1;
								if (!learnedKeySepTwoLines.Contains(kthKeySep))
								{
									learnedKeySepTwoLines.Add(kthKeySep);
									result = records.Select((StringRegion[] rowRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthTwoLineKeyValue(CS$<>8__locals1.key, CS$<>8__locals1.sep, k3, rowRecs)).Select(delegate(Optional<MultiRecordMatch> m)
									{
										if (!m.HasValue)
										{
											return null;
										}
										return new MultiRecordMatch?(m.Value);
									}).ToArray<MultiRecordMatch?>();
									if (this.CheckKeyValueResult(primaryMatches, result))
									{
										yield return Record.Create<columnSelector, MultiRecordMatch?[]>(this._build.Node.Rule.KthTwoLineKeyValue(this._build.Node.Rule.key(CS$<>8__locals1.key), this._build.Node.Rule.sep(CS$<>8__locals1.sep), this._build.Node.Rule.k(k3), this._build.Node.Variable.rowRecord), result);
									}
								}
							}
						}
						if (CS$<>8__locals1.sep == null)
						{
							int k2 = keyQuotesCount.GetOrAdd(CS$<>8__locals1.key, 1);
							Dictionary<string, int> dictionary3 = keyQuotesCount;
							string key = CS$<>8__locals1.key;
							num = dictionary3[key];
							dictionary3[key] = num + 1;
							Record<string, int> record2 = Record.Create<string, int>(CS$<>8__locals1.key, k2);
							if (!learnedKeyQuotes.Contains(record2))
							{
								learnedKeyQuotes.Add(record2);
								result = records.Select((StringRegion[] rowRecs) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KthKeyQuote(CS$<>8__locals1.key, k2, CS$<>8__locals1.<>4__this.Options.NewLineRecordSeparator, rowRecs)).Select(delegate(Optional<MultiRecordMatch> m)
								{
									if (!m.HasValue)
									{
										return null;
									}
									return new MultiRecordMatch?(m.Value);
								}).ToArray<MultiRecordMatch?>();
								if (this.CheckKeyValueResult(primaryMatches, result))
								{
									yield return Record.Create<columnSelector, MultiRecordMatch?[]>(this._build.Node.Rule.KthKeyQuote(this._build.Node.Rule.key(CS$<>8__locals1.key), this._build.Node.Rule.k(k2), this._build.Node.Rule.newLineSep(this.Options.NewLineRecordSeparator), this._build.Node.Variable.rowRecord), result);
								}
							}
						}
						CS$<>8__locals1 = null;
						keySep = default(Record<string, string>);
						kthKeySep = default(Record<string, string, int>);
						result = null;
					}
				}
				StringRegion[] array3 = null;
				keySepsCount = null;
				keySepTwoLinesCount = null;
				keyQuotesCount = null;
			}
			StringRegion[][] array = null;
			yield break;
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x000B8950 File Offset: 0x000B6B50
		public Witnesses(Grammar grammar, Options options = null)
			: base(grammar)
		{
			this._build = Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders.Instance(grammar);
			this._startSymbol = this._build.Symbol.splitLines;
			this.Options = options ?? new Options();
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x000B89AC File Offset: 0x000B6BAC
		private Options Options { get; }

		// Token: 0x06003B84 RID: 15236 RVA: 0x000B89B4 File Offset: 0x000B6BB4
		private ProgramSetBuilder<splitLines> LearnAutoSplit(IEnumerable<IEnumerable<StringRegion>> splitInputs, CancellationToken cancel)
		{
			IEnumerable<ProgramSetBuilder<splitLines>> enumerable = splitInputs.Select((IEnumerable<StringRegion> input) => this.LearnAutoSplit(input, cancel));
			Func<ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>> func;
			if ((func = Witnesses.<>O.<0>__Intersect) == null)
			{
				func = (Witnesses.<>O.<0>__Intersect = new Func<ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>>(ProgramSetBuilderEx.Intersect<splitLines>));
			}
			return enumerable.Aggregate(func);
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000B8A08 File Offset: 0x000B6C08
		private ProgramSetBuilder<splitLines> LearnAutoSplit(IEnumerable<StringRegion> splitInput, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass31_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass31_0();
			CS$<>8__locals1.<>4__this = this;
			IReadOnlyList<Cache> readOnlyList = this.BuildCaches(splitInput, cancel);
			if (readOnlyList.Count == 0)
			{
				return ProgramSetBuilder.Empty<splitLines>(this._startSymbol);
			}
			CS$<>8__locals1.programNodes = new List<splitLines>();
			using (IEnumerator<Cache> enumerator = readOnlyList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Witnesses.<>c__DisplayClass31_1 CS$<>8__locals2 = new Witnesses.<>c__DisplayClass31_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.cache = enumerator.Current;
					if (CS$<>8__locals2.cache.Rows.Length != 0)
					{
						if (cancel.IsCancellationRequested)
						{
							return CS$<>8__locals2.CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
						}
						allRecords recordsNode = this.CreateRecords(CS$<>8__locals2.cache);
						dataLines dataRecordsNode = this.CreateConversionSkippedToData(this.CreateConversionRecordsToSkipped(recordsNode));
						splitSequence[] array = this.LearnSplitAtEveryLine(CS$<>8__locals2.cache.Rows);
						if (array.Length != 0)
						{
							IEnumerable<splitLines> enumerable = array.Select((splitSequence ssNode) => CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateSplitSequenceLet(dataRecordsNode, ssNode));
							CS$<>8__locals2.CS$<>8__locals1.programNodes.AddRange(enumerable);
						}
						else
						{
							if (cancel.IsCancellationRequested)
							{
								return CS$<>8__locals2.CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
							}
							HashSet<RegularExpression> symbolRegexes = CS$<>8__locals2.cache.Rows[0].SymbolRegexes;
							if (symbolRegexes.Count > 0 && !this.Options.IgnoreFilterHeader)
							{
								var enumerable2 = from regex in symbolRegexes
									let headerRows = CS$<>8__locals2.cache.Rows.Count((RowCache row) => row.SymbolRegexes.Contains(regex))
									where Witnesses.IsLikelyHeaderRatio(headerRows, CS$<>8__locals2.cache.Rows.Length)
									select regex into regex
									select new
									{
										regex = regex,
										dtRows = CS$<>8__locals2.cache.Rows.Where((RowCache row) => !row.SymbolRegexes.Contains(regex)).ToArray<RowCache>()
									};
								var func;
								if ((func = CS$<>8__locals2.CS$<>8__locals1.<>9__7) == null)
								{
									func = (CS$<>8__locals2.CS$<>8__locals1.<>9__7 = <>h__TransparentIdentifier0 => new
									{
										<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
										ssNodes = CS$<>8__locals2.CS$<>8__locals1.<>4__this.LearnSplitSequence(<>h__TransparentIdentifier0.dtRows)
									});
								}
								var enumerable3 = from <>h__TransparentIdentifier1 in enumerable2.Select(func)
									where <>h__TransparentIdentifier1.ssNodes.Length != 0
									select <>h__TransparentIdentifier1;
								var func2;
								if ((func2 = CS$<>8__locals2.CS$<>8__locals1.<>9__9) == null)
								{
									func2 = (CS$<>8__locals2.CS$<>8__locals1.<>9__9 = <>h__TransparentIdentifier1 => new
									{
										<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
										startsWithNode = CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateStartsWith(<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.regex)
									});
								}
								splitLines[] array2 = (from <>h__TransparentIdentifier2 in enumerable3.Select(func2)
									from ssNode in <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.ssNodes
									select CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateSplitSequenceLet(CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateFilterHeader(<>h__TransparentIdentifier2.startsWithNode, recordsNode), ssNode)).ToArray<splitLines>();
								if (array2.Length != 0)
								{
									CS$<>8__locals2.CS$<>8__locals1.programNodes.AddRange(array2);
									continue;
								}
							}
							if (cancel.IsCancellationRequested)
							{
								return CS$<>8__locals2.CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
							}
							if (!this.Options.IgnoreSelectData)
							{
								var enumerable4 = from regex in CS$<>8__locals2.cache.Rows.SelectMany((RowCache row) => row.PrefixRegexes).ConvertToHashSet<RegularExpression>()
									let dtRows = CS$<>8__locals2.cache.Rows.Where((RowCache row) => row.PrefixRegexes.Contains(regex)).ToArray<RowCache>()
									where Witnesses.IsLikelyDataRatio(dtRows.Length, CS$<>8__locals2.cache.Rows.Length)
									select <>h__TransparentIdentifier0;
								var func3;
								if ((func3 = CS$<>8__locals2.CS$<>8__locals1.<>9__17) == null)
								{
									func3 = (CS$<>8__locals2.CS$<>8__locals1.<>9__17 = <>h__TransparentIdentifier0 => new
									{
										<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
										ssNodes = CS$<>8__locals2.CS$<>8__locals1.<>4__this.LearnSplitSequence(<>h__TransparentIdentifier0.dtRows)
									});
								}
								var enumerable5 = from <>h__TransparentIdentifier1 in enumerable4.Select(func3)
									let duplicateCount = dtRows.Length - dtRows.Select((RowCache row) => row.Region.Value).ConvertToHashSet<string>().Count
									where ssNodes.Length != 0
									select <>h__TransparentIdentifier2;
								var func4;
								if ((func4 = CS$<>8__locals2.CS$<>8__locals1.<>9__20) == null)
								{
									func4 = (CS$<>8__locals2.CS$<>8__locals1.<>9__20 = <>h__TransparentIdentifier2 => new
									{
										<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
										startsWithNode = CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateStartsWith(<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.regex)
									});
								}
								splitLines[] array3 = (from <>h__TransparentIdentifier3 in enumerable5.Select(func4)
									from ssNode in ssNodes
									orderby duplicateCount, regex.Score descending
									select CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateSplitSequenceLet(CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateSelectData(startsWithNode, recordsNode), ssNode)).ToArray<splitLines>();
								if (array3.Length != 0)
								{
									CS$<>8__locals2.CS$<>8__locals1.programNodes.AddRange(array3);
									continue;
								}
							}
							if (cancel.IsCancellationRequested)
							{
								return CS$<>8__locals2.CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
							}
							if (this.Options.IgnoreSkip)
							{
								return ProgramSetBuilder.Empty<splitLines>(this._startSymbol);
							}
							int i = 0;
							bool flag = false;
							if (this.Options.SkipLinesCount != null)
							{
								i = this.Options.SkipLinesCount.Value;
								flag = true;
							}
							else
							{
								foreach (int num in this.SkipLineMaxCandidates)
								{
									for (i = Math.Min(num, CS$<>8__locals2.cache.Rows.Length / 2); i > 0; i--)
									{
										IEnumerable<RegularExpression> enumerable6 = CS$<>8__locals2.cache.Rows.Take(i).SelectMany((RowCache row) => row.PrefixRegexes).ConvertToHashSet<RegularExpression>();
										HashSet<RegularExpression> dataRegexes = CS$<>8__locals2.cache.Rows.Skip(i).SelectMany((RowCache row) => row.PrefixRegexes).ConvertToHashSet<RegularExpression>();
										if (!enumerable6.Any((RegularExpression regex) => dataRegexes.Contains(regex)))
										{
											flag = true;
											break;
										}
									}
									if (flag)
									{
										break;
									}
								}
							}
							if (cancel.IsCancellationRequested)
							{
								return CS$<>8__locals2.CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
							}
							if (!flag)
							{
								i = 0;
							}
							RowCache[] array4 = CS$<>8__locals2.cache.Rows.Skip(i).ToArray<RowCache>();
							splitSequence[] array5 = this.LearnSplitSequence(array4);
							if (array5.Length == 0)
							{
								array5 = new splitSequence[] { this.CreateSequence() };
							}
							skippedRecords skippedRecords = ((i == 0) ? this.CreateConversionRecordsToSkipped(recordsNode) : this.CreateSkip(i, recordsNode));
							dataLines dataLinesNode = this.CreateConversionSkippedToData(skippedRecords);
							CS$<>8__locals2.CS$<>8__locals1.programNodes.AddRange(array5.Select((splitSequence ssNode) => CS$<>8__locals2.CS$<>8__locals1.<>4__this.CreateSplitSequenceLet(dataLinesNode, ssNode)));
						}
					}
				}
			}
			return CS$<>8__locals1.<LearnAutoSplit>g__CreateProgramSet|0();
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x000B91E8 File Offset: 0x000B73E8
		private allRecords CreateRecords(Cache cache)
		{
			if (cache.Configuration.QuoteChar == null && cache.Configuration.EscapeChar == null)
			{
				return this.CreateConversionLinesToRecords();
			}
			return this.CreateQuoteRecords(cache.Configuration);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x000B9238 File Offset: 0x000B7438
		private splitSequence[] LearnSplitSequence(RowCache[] rowCaches)
		{
			splitSequence[] array = this.LearnSplitAtEveryLine(rowCaches);
			if (array.Length != 0)
			{
				return array;
			}
			if (this.Options.IgnoreSplitSequence)
			{
				return new splitSequence[0];
			}
			return (from regex in rowCaches[0].PrefixRegexes
				let regexCount = rowCaches.Count((RowCache row) => row.PrefixRegexes.Contains(regex))
				where Witnesses.IsLikelySplitRatio(regexCount, rowCaches.Length)
				orderby regex.Score descending
				select new
				{
					Regex = regex,
					Count = regexCount
				} into splitRegex
				select this.CreateSplitSequence(splitRegex.Regex)).ToArray<splitSequence>();
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x000B9310 File Offset: 0x000B7510
		private splitSequence[] LearnSplitAtEveryLine(RowCache[] rowCaches)
		{
			HashSet<RegularExpression> hashSet = null;
			foreach (RegularExpression[] array in rowCaches.Select((RowCache row) => row.PrefixRegexes))
			{
				if (hashSet == null)
				{
					hashSet = array.ConvertToHashSet<RegularExpression>();
				}
				else
				{
					hashSet.IntersectWith(array);
				}
			}
			if (hashSet == null || hashSet.Count == 0)
			{
				return new splitSequence[0];
			}
			if (this.Options.IgnoreSplitSequence)
			{
				return new splitSequence[] { this.CreateSequence() };
			}
			return new splitSequence[] { this.CreateSequence() }.Concat(hashSet.Select(new Func<RegularExpression, splitSequence>(this.CreateSplitSequence))).ToArray<splitSequence>();
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x000B93EC File Offset: 0x000B75EC
		private static bool IsLikelyHeaderRatio(int headerCount, int totalCount)
		{
			if (totalCount < 5)
			{
				return headerCount <= 2;
			}
			if (totalCount < 10)
			{
				return headerCount <= 3;
			}
			if (totalCount < 20)
			{
				return headerCount <= 5;
			}
			if (totalCount < 50)
			{
				return headerCount <= 10;
			}
			return (double)headerCount / (double)totalCount < 0.1;
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x000B9440 File Offset: 0x000B7640
		private static bool IsLikelyDataRatio(int dataCount, int totalCount)
		{
			if (totalCount < 5)
			{
				return dataCount >= 3;
			}
			if (totalCount < 10)
			{
				return dataCount >= 6;
			}
			if (totalCount < 20)
			{
				return dataCount >= 15;
			}
			if (totalCount < 50)
			{
				return dataCount >= 30;
			}
			return (double)dataCount / (double)totalCount >= 0.6;
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x000B9495 File Offset: 0x000B7695
		private static bool IsLikelySplitRatio(int recordCount, int totalCount)
		{
			return (double)recordCount / (double)totalCount > 0.2;
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000B94A8 File Offset: 0x000B76A8
		private IReadOnlyList<Cache> BuildCaches(IEnumerable<StringRegion> rows, CancellationToken cancel)
		{
			StringRegion[] array = rows.ToArray<StringRegion>();
			RowCache[] array2 = this.BuildRowCaches(array, 1);
			if (this.Options.MaxRowPrefixRegexTokens == 1)
			{
				if (array2.All((RowCache c) => c.PrefixRegexes.Length == 0 || (c.PrefixRegexes.Length == 1 && c.PrefixRegexes[0].Count == 1 && c.PrefixRegexes[0].Tokens[0].Name == "WhiteSpace")))
				{
					array2 = this.BuildRowCaches(array, 2);
				}
			}
			if (this.Options.IgnoreQuote)
			{
				return new Cache[]
				{
					new Cache(new QuotingConfiguration(null, false, null, QuotingStyle.Standard), array2)
				};
			}
			List<Cache> list = new List<Cache>();
			foreach (char? c3 in Witnesses.LikelyQuoteChars)
			{
				List<Cache> list2 = new List<Cache>();
				foreach (char? c2 in Witnesses.LikelyEscapeChars)
				{
					if (cancel.IsCancellationRequested)
					{
						break;
					}
					int num = 0;
					RecordStatus recordStatus = RecordStatus.FieldStart;
					QuotingConfiguration quotingConfiguration = new QuotingConfiguration(c3, c3 != null, c2, (c3 != null) ? QuotingStyle.Flexible : QuotingStyle.Standard);
					List<RowCache> recordCaches = new List<RowCache>();
					bool flag = false;
					bool flag2 = false;
					for (int i = 0; i < array.Length; i++)
					{
						bool flag3;
						bool flag4;
						recordStatus = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.GetStatusAfter(array[i], quotingConfiguration, null, recordStatus, out flag3, out flag4);
						flag = flag || flag3;
						flag2 = flag2 || flag4;
						if (recordStatus == RecordStatus.EndRecord)
						{
							recordCaches.Add(array2[num]);
							num = i + 1;
							recordStatus = RecordStatus.FieldStart;
						}
					}
					if ((c3 == null || flag) && (c2 == null || flag2) && recordCaches.Count >= Math.Min(2, array.Length))
					{
						Cache cache = new Cache(quotingConfiguration, recordCaches.ToArray());
						if (list2.Any((Cache c) => recordCaches.Count != c.Rows.Length))
						{
							list2.Insert(0, cache);
						}
						else
						{
							list2.Add(cache);
						}
					}
				}
				list.AddRange(list2);
				list2.Clear();
			}
			return list.ToArray();
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000B9708 File Offset: 0x000B7908
		private RowCache[] BuildRowCaches(StringRegion[] rows, int prefixLength)
		{
			RowCache[] array = new RowCache[rows.Length];
			for (int i = 0; i < rows.Length; i++)
			{
				StringRegion stringRegion = rows[i];
				RegularExpression[] array2 = ((this.Options.MaxRowPrefixRegexTokens == 1) ? Witnesses.LearnPrefixRegexesSingle(stringRegion, prefixLength) : this.LearnPrefixRegexMultiple(stringRegion));
				HashSet<RegularExpression> hashSet = array2.Where((RegularExpression r) => r.Tokens[0].IsSymbol).ConvertToHashSet<RegularExpression>();
				array[i] = new RowCache(stringRegion, array2, hashSet);
			}
			return array;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000B9788 File Offset: 0x000B7988
		private RegularExpression[] LearnPrefixRegexMultiple(StringRegion s)
		{
			Dictionary<string, Token> dictionary = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens.Where((KeyValuePair<string, Token> token) => token.Value is AbstractRegexToken && token.Value.UseForLearning && token.Value.Name != "Alphanum" && token.Value.Name != "Line Separator").ToDictionary<string, Token>();
			s = new StringRegion(s.Value, dictionary);
			return (from r in RegularExpression.LearnRightMatches(s, 0U, this.Options.MaxRowPrefixRegexTokens, 0)
				where r.Count > 0
				select r into t
				orderby -t.Score
				select t).ToArray<RegularExpression>();
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000B9834 File Offset: 0x000B7A34
		private static RegularExpression[] LearnPrefixRegexesSingle(StringRegion s, int prefixLength)
		{
			return (from regexes in Witnesses.PrefixRegexes.Value
				where regexes.Item1.Count == prefixLength && regexes.Item2.IsMatch(s.Value)
				select regexes.Item1).ToArray<RegularExpression>();
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000B9899 File Offset: 0x000B7A99
		private static IEnumerable<Record<RegularExpression, Regex>> GetPrefixRegexes()
		{
			AbstractRegexToken whiteSpaceToken = (AbstractRegexToken)Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens["WhiteSpace"];
			foreach (AbstractRegexToken token in Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens.Values.OfType<AbstractRegexToken>())
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

		// Token: 0x06003B91 RID: 15249 RVA: 0x000B98A2 File Offset: 0x000B7AA2
		private splitLines CreateSplitSequenceLet(dataLines dataLines, splitSequence splitSequence)
		{
			return this._build.Node.Rule.SplitSequenceLet(dataLines, splitSequence);
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000B98BC File Offset: 0x000B7ABC
		private splitSequence CreateSplitSequence(RegularExpression r)
		{
			return this._build.Node.Rule.SplitSequence(this._build.Node.Rule.r(r), this._build.Node.Variable.ls);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000B9909 File Offset: 0x000B7B09
		private splitSequence CreateSequence()
		{
			return this._build.Node.Rule.Sequence(this._build.Node.Variable.ls);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000B9938 File Offset: 0x000B7B38
		private skippedRecords CreateSkip(int k, allRecords records)
		{
			return this._build.Node.Rule.Skip(this._build.Node.Rule.k(k), this._build.Node.Rule.headerIndex(Optional<int>.Nothing), this._build.Node.UnnamedConversion.skippedFooter_allRecords(records));
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000B99A0 File Offset: 0x000B7BA0
		private dataLines CreateFilterHeader(basicLinePredicate basicLinePredicate, skippedRecords records)
		{
			return this._build.Node.Rule.FilterHeader(basicLinePredicate, records);
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000B99B9 File Offset: 0x000B7BB9
		private dataLines CreateFilterHeader(basicLinePredicate basicLinePredicate, allRecords records)
		{
			return this.CreateFilterHeader(basicLinePredicate, this.CreateConversionRecordsToSkipped(records));
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000B99C9 File Offset: 0x000B7BC9
		private dataLines CreateSelectData(basicLinePredicate basicLinePredicate, allRecords records)
		{
			return this._build.Node.Rule.SelectData(basicLinePredicate, this.CreateConversionRecordsToSkipped(records));
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000B99E8 File Offset: 0x000B7BE8
		private allRecords CreateQuoteRecords(QuotingConfiguration conf)
		{
			return this._build.Node.Rule.QuoteRecords(this._build.Node.Rule.quotingConfig(conf), this._build.Node.Rule.delimiter(Optional<string>.Nothing), this._build.Node.Variable.allLines);
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000B9A4F File Offset: 0x000B7C4F
		private allRecords CreateConversionLinesToRecords()
		{
			return this._build.Node.UnnamedConversion.allRecords_allLines(this._build.Node.Variable.allLines);
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000B9A7B File Offset: 0x000B7C7B
		private skippedRecords CreateConversionRecordsToSkipped(allRecords records)
		{
			return this._build.Node.UnnamedConversion.skippedRecords_skippedFooter(this._build.Node.UnnamedConversion.skippedFooter_allRecords(records));
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000B9AA8 File Offset: 0x000B7CA8
		private dataLines CreateConversionSkippedToData(skippedRecords records)
		{
			return this._build.Node.UnnamedConversion.dataLines_skippedRecords(records);
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000B9AC0 File Offset: 0x000B7CC0
		private basicLinePredicate CreateStartsWith(RegularExpression r)
		{
			return this._build.Node.Rule.StartsWith(this._build.Node.Variable.s, this._build.Node.Rule.r(r));
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x000B9B0D File Offset: 0x000B7D0D
		public IEnumerable<Witnesses.LearnResult> LearnAuto(IReadOnlyList<IReadOnlyList<StringRegion>> linesInputs, IFeature feature, int k, CancellationToken cancel, Guid? guid)
		{
			if (linesInputs.IsEmpty<IReadOnlyList<StringRegion>>())
			{
				throw new InvalidOperationException("No input.");
			}
			if (this.Options.LinesTrimmed)
			{
				this.Options.IgnoreQuote = true;
			}
			if (this.Options.FixedWidthSchema != null)
			{
				IReadOnlyList<string> readOnlyList;
				topSplit? topSplit = this.LearnFwFromSchema(this.Options.FixedWidthSchema, out readOnlyList, guid);
				if (topSplit != null)
				{
					yield return new Witnesses.LearnResult(topSplit.Value, readOnlyList, null, 0, false);
				}
				yield break;
			}
			if (this.Options.AllowMultiRecord)
			{
				IReadOnlyList<string> readOnlyList2;
				topSplit? topSplit2 = this.LearnMultiRecordProgram(linesInputs, out readOnlyList2);
				if (topSplit2 != null)
				{
					yield return new Witnesses.LearnResult(topSplit2.Value, null, readOnlyList2, 0, false);
					int num = k - 1;
					k = num;
					if (k <= 0)
					{
						yield break;
					}
				}
			}
			List<Witnesses.LearnResult> partialResults = new List<Witnesses.LearnResult>();
			bool needLearnSplitText = !this.Options.LinesTrimmed && !this.Options.IgnoreSplitRecords;
			ProgramSet set = this.LearnAutoSplit(linesInputs, cancel).Set;
			IEnumerable<splitLines> enumerable = (needLearnSplitText ? set.AllElements : set.TopK(feature, k, null, null)).Select(new Func<ProgramNode, splitLines>(this._build.Node.Cast.splitLines));
			foreach (splitLines splitLines in enumerable)
			{
				splitLines splitLines2 = splitLines;
				List<StringRegion> list;
				IReadOnlyList<string> readOnlyList4;
				IReadOnlyList<StringRegion> readOnlyList3 = this.ExecutesSplitFileProgram(splitLines2, linesInputs, this.Options.SkipFooterLinesCount, false, out list, out readOnlyList4);
				bool flag = this.Options.IgnoreFilterHeader && this.Options.IgnoreSelectData;
				QuotingConfiguration quotingConfiguration = this.GetQuotingConf(splitLines2);
				IReadOnlyList<StringRegion> readOnlyList5 = (flag ? Witnesses.FilterRecords(readOnlyList3) : readOnlyList3);
				SplitProgram textProg = null;
				int num2 = 0;
				if (needLearnSplitText)
				{
					textProg = this.LearnSplitTextProgramWithQuoting(splitLines2, quotingConfiguration, linesInputs, ref readOnlyList3, ref readOnlyList5, list, !flag, out num2);
					if (textProg == null && this.Options.DelimiterStringsProvided)
					{
						yield break;
					}
				}
				if (textProg != null && !quotingConfiguration.Equals(textProg.Properties.QuotingConfiguration))
				{
					quotingConfiguration = textProg.Properties.QuotingConfiguration;
					splitLines splitLines3 = splitLines2;
					QuotingConfiguration? quotingConfiguration2 = new QuotingConfiguration?(quotingConfiguration);
					string delimiter = textProg.Properties.Delimiter;
					splitLines2 = this.SplitFileProgWith(splitLines3, null, null, null, null, delimiter, null, null, quotingConfiguration2);
				}
				int[] array = null;
				string text = null;
				bool flag2 = false;
				List<List<string>> list2;
				if (flag)
				{
					if (textProg == null && !this.Options.IgnoreSplitRecords && (quotingConfiguration.QuoteChar != null || quotingConfiguration.EscapeChar != null))
					{
						continue;
					}
					int num3;
					splitLines? splitLines4 = this.LearnSkipAndFilter(splitLines2, textProg, readOnlyList3, readOnlyList5.Count < readOnlyList3.Count, num2, out num3, out flag2, out text, out list2);
					if (splitLines4 != null)
					{
						splitLines2 = splitLines4.Value;
					}
					array = Witnesses.FindSelectColumns(list2, quotingConfiguration);
					num2 = num3;
					if (this.Options.SkipLinesCount == null)
					{
						SplitProgram textProg3 = textProg;
						if (((textProg3 != null) ? textProg3.Properties.Delimiter : null) != null)
						{
							int num4 = Witnesses.SkipCommentsInData(list2, array);
							if (num4 > 0)
							{
								array = Witnesses.FindSelectColumns(list2, quotingConfiguration);
								num2 += num4;
							}
						}
					}
				}
				else
				{
					list2 = readOnlyList3.Select((StringRegion row) => Witnesses.SplitIntoCells(textProg, row, false)).ToList<List<string>>();
				}
				if (num2 != 0)
				{
					splitLines2 = this.SplitFileProgWith(splitLines2, new int?(this.GetSkipLines(splitLines2) + num2), null, null, null, null, null, null, null);
					IReadOnlyList<string> readOnlyList6;
					this.ExecutesSplitFileProgram(splitLines2, linesInputs, 0, false, out list, out readOnlyList6);
				}
				bool flag3;
				IReadOnlyList<string> readOnlyList7 = this.LearnColumnNames(textProg, list, list2, array, out flag3, ref splitLines2);
				if (this.Options.SkipFooterLinesCount > 0)
				{
					splitLines splitLines5 = splitLines2;
					int? num5 = new int?(this.Options.SkipFooterLinesCount);
					splitLines2 = this.SplitFileProgWith(splitLines5, null, null, num5, null, null, null, null, null);
				}
				if (this.Options.FilterEmptyColummns && flag3)
				{
					array = Witnesses.FindSelectColumns(list2, quotingConfiguration);
				}
				topSplit topSplit3 = this.CreateTopSplit(splitLines2, textProg, array);
				IEnumerable<StringRegion> enumerable2 = list;
				SplitProgram textProg2 = textProg;
				int emptyOrCommentSkipLines = Witnesses.GetEmptyOrCommentSkipLines(enumerable2, (textProg2 != null) ? textProg2.Properties.Delimiter : null, text);
				Witnesses.LearnResult learnResult = new Witnesses.LearnResult(topSplit3, readOnlyList7, readOnlyList4, emptyOrCommentSkipLines, flag2);
				if (needLearnSplitText && textProg == null)
				{
					partialResults.Add(learnResult);
				}
				else
				{
					yield return learnResult;
					int num = k - 1;
					k = num;
					if (k <= 0)
					{
						yield break;
					}
				}
			}
			IEnumerator<splitLines> enumerator = null;
			foreach (Witnesses.LearnResult learnResult2 in partialResults.Take(k))
			{
				yield return learnResult2;
			}
			IEnumerator<Witnesses.LearnResult> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x000B9B44 File Offset: 0x000B7D44
		private IReadOnlyList<string> LearnColumnNames(SplitProgram textProg, IReadOnlyList<StringRegion> skippedLines, List<List<string>> table, IReadOnlyList<int> selectedColumns, out bool headerInData, ref splitLines splitFileProg)
		{
			IReadOnlyList<string> readOnlyList = this.FindColumnNamesInSkippedLines(textProg, skippedLines, ref splitFileProg);
			headerInData = false;
			if (this.Options.SkipLinesCount == null && readOnlyList == null)
			{
				readOnlyList = this.PromoteDataToHeaderProgram((textProg != null) ? textProg.Properties.Delimiter : null, table, selectedColumns, ref splitFileProg);
				if (readOnlyList != null)
				{
					headerInData = true;
				}
			}
			return readOnlyList;
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x000B9BA0 File Offset: 0x000B7DA0
		private static int GetEmptyOrCommentSkipLines(IEnumerable<StringRegion> skippedLines, string delimiter, string commentStr)
		{
			int num = 0;
			Optional<string> optional = ((delimiter != null) ? delimiter.Some<string>() : Optional<string>.Nothing);
			foreach (StringRegion stringRegion in skippedLines)
			{
				string text = stringRegion.Source.Trim((char[])Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.WhiteSpaceChars);
				if ((commentStr != null && Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsCommentRecord(text, commentStr)) || Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsEmptyRecord(stringRegion.Source, text, optional))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x000B9C30 File Offset: 0x000B7E30
		private IReadOnlyList<string> PromoteDataToHeaderProgram(string delimiter, List<List<string>> table, IReadOnlyList<int> selectedColumns, ref splitLines splitFileProg)
		{
			if (table.Count < 2)
			{
				return null;
			}
			List<List<string>> list = (from row in table.Take(20)
				select row.Select((string cell) => cell.Trim((char[])Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.ColumnNameTrimChars)).ToList<string>()).ToList<List<string>>();
			int j;
			for (j = 1; j < list.Count; j++)
			{
				if (list[j - 1].Any((string cell) => !string.IsNullOrWhiteSpace(cell)))
				{
					break;
				}
			}
			IReadOnlyList<string> header = list[j - 1].Select((string cell) => cell.Trim(Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.ColumnNameTrimChars.ToArray<char>())).ToList<string>();
			IReadOnlyList<int> selectedColumns2 = selectedColumns;
			int i;
			IReadOnlyList<string> readOnlyList = ((selectedColumns2 != null) ? selectedColumns2.Select((int i) => header[i]).ToList<string>() : null);
			IReadOnlyList<string> readOnlyList2 = readOnlyList ?? header;
			Func<int, double> func = ((delimiter != null) ? Witnesses.ValidColumnsThreshold : Witnesses.FixedWidthValidColumnsThreshold);
			if (Witnesses.ValidColumnsRatio(readOnlyList2) < func(readOnlyList2.Count))
			{
				return null;
			}
			IEnumerable<List<string>> enumerable = list.Skip(j);
			List<List<string>> list2 = ((selectedColumns == null) ? enumerable : enumerable.Select((List<string> row) => selectedColumns.Select((int i) => row[i]).ToList<string>())).ToList<List<string>>();
			IEnumerable<string> enumerable2 = readOnlyList2;
			Func<string, HashSet<RegularExpression>> func2;
			if ((func2 = Witnesses.<>O.<1>__LearnLongestPrefixRegexes) == null)
			{
				func2 = (Witnesses.<>O.<1>__LearnLongestPrefixRegexes = new Func<string, HashSet<RegularExpression>>(Witnesses.LearnLongestPrefixRegexes));
			}
			List<HashSet<RegularExpression>> list3 = enumerable2.Select(func2).ToList<HashSet<RegularExpression>>();
			bool flag = false;
			int i2;
			for (i = 0; i < readOnlyList2.Count; i = i2 + 1)
			{
				if (Witnesses.HeaderRegex.IsMatch(readOnlyList2[i]))
				{
					HashSet<RegularExpression> hashSet = list3[i];
					HashSet<string> hashSet2 = list2.Select((List<string> row) => row[i]).ConvertToHashSet<string>();
					foreach (string text in hashSet2)
					{
						HashSet<RegularExpression> hashSet3 = Witnesses.LearnLongestPrefixRegexes(text);
						hashSet.ExceptWith(hashSet3);
						if (hashSet.Count == 0)
						{
							break;
						}
					}
					if (hashSet.Count > 0 || (hashSet2.Count > 1 && (double)hashSet2.Count / (double)list2.Count <= 0.2 && !hashSet2.Contains(readOnlyList2[i])))
					{
						flag = true;
						break;
					}
				}
				i2 = i;
			}
			if (!flag)
			{
				return null;
			}
			int num = this.GetSkipLines(splitFileProg) + j;
			int num2 = num - 1;
			splitFileProg = this.SplitFileProgWith(splitFileProg, new int?(num), new int?(num2), null, null, null, null, null, null);
			if (j > 0)
			{
				table.RemoveRange(0, j);
			}
			return header;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000B9F50 File Offset: 0x000B8150
		private static HashSet<RegularExpression> LearnLongestPrefixRegexes(string cell)
		{
			List<RegularExpression> list = RegularExpression.LearnRightMatches(new StringRegion(cell, Witnesses.ColumnNameLearningTokens.Value), 0U, 2, 1).ToList<RegularExpression>();
			if (list.Count == 0)
			{
				return new HashSet<RegularExpression>();
			}
			int maxLength = list.Max((RegularExpression r) => r.Count);
			return list.Where((RegularExpression r) => r.Count == maxLength).ConvertToHashSet<RegularExpression>();
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x000B9FD4 File Offset: 0x000B81D4
		private IReadOnlyList<string> FindColumnNamesInSkippedLines(SplitProgram textProgram, IReadOnlyList<StringRegion> headerLines, ref splitLines splitFileProg)
		{
			if (headerLines.Count == 0)
			{
				return null;
			}
			QuotingConfiguration? quotingConfiguration = ((textProgram != null) ? new QuotingConfiguration?(textProgram.Properties.QuotingConfiguration) : null);
			string text = ((textProgram != null) ? textProgram.Properties.Delimiter : null);
			IReadOnlyList<StringRegion> readOnlyList = ((quotingConfiguration != null && (quotingConfiguration.Value.QuoteChar != null || quotingConfiguration.Value.EscapeChar != null)) ? Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.QuoteRecords(quotingConfiguration.Value, (text != null) ? text.Some<string>() : Optional<string>.Nothing, headerLines) : headerLines).ToList<StringRegion>();
			Func<int, double> func = ((text != null) ? Witnesses.ValidColumnsThreshold : Witnesses.FixedWidthValidColumnsThreshold);
			for (int i = readOnlyList.Count - 1; i >= 0; i--)
			{
				StringRegion stringRegion = readOnlyList[i];
				IReadOnlyList<string> readOnlyList2 = Witnesses.SplitIntoCells(textProgram, new StringRegion(Witnesses.TrimEndingNewLine(stringRegion.Value), Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens), true);
				if (Witnesses.ValidColumnsRatio(readOnlyList2) > func(readOnlyList2.Count))
				{
					string commentStr = this.GetCommentStr(splitFileProg);
					bool skipEmpty = this.GetSkipEmpty(splitFileProg);
					bool flag = false;
					if (commentStr != null && Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsCommentRecord(stringRegion.Source.Trim((char[])Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.WhiteSpaceChars), commentStr))
					{
						flag = true;
					}
					splitLines splitLines = splitFileProg;
					int? num = new int?(this.GetSkipLines(splitFileProg));
					int? num2 = new int?(i);
					bool? flag2 = new bool?(skipEmpty);
					string text2 = commentStr;
					bool? flag3 = new bool?(flag);
					string text3 = text;
					splitFileProg = this.SplitFileProgWith(splitLines, num, num2, null, flag2, text3, text2, flag3, null);
					return readOnlyList2;
				}
			}
			return null;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x000BA1A0 File Offset: 0x000B83A0
		private static int SkipCommentsInData(List<List<string>> table, IReadOnlyList<int> selectedCols)
		{
			Witnesses.<>c__DisplayClass76_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass76_0();
			CS$<>8__locals1.selectedCols = selectedCols;
			int i;
			IReadOnlyList<IReadOnlyList<string>> readOnlyList = table.Take(20).Select(delegate(List<string> row)
			{
				IReadOnlyList<int> selectedCols2 = CS$<>8__locals1.selectedCols;
				return ((selectedCols2 != null) ? selectedCols2.Select((int i) => row[i]).ToList<string>() : null) ?? row;
			}).ToList<List<string>>();
			if (readOnlyList.Count < 2)
			{
				return 0;
			}
			CS$<>8__locals1.colNum = readOnlyList[0].Count;
			if (CS$<>8__locals1.colNum < 3)
			{
				return 0;
			}
			IReadOnlyList<int> readOnlyList2 = readOnlyList.Select(delegate(IReadOnlyList<string> row)
			{
				Func<string, bool> func;
				if ((func = Witnesses.<>O.<2>__IsNullOrEmpty) == null)
				{
					func = (Witnesses.<>O.<2>__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty));
				}
				return row.Count(func);
			}).ToList<int>();
			for (i = readOnlyList2.Count / 2; i > 0; i--)
			{
				IReadOnlyList<int> readOnlyList3 = readOnlyList2.Skip(i).ToList<int>();
				double dataSparseAvg = readOnlyList3.Average();
				int num = readOnlyList3.Count((int h) => CS$<>8__locals1.<SkipCommentsInData>g__IsSparse|2(h, dataSparseAvg));
				IEnumerable<int> enumerable = readOnlyList2.Take(i);
				if (1.0 * (double)num / (double)readOnlyList3.Count <= 0.2 && enumerable.All((int h) => CS$<>8__locals1.<SkipCommentsInData>g__IsSparse|2(h, dataSparseAvg)))
				{
					break;
				}
			}
			if (i > 0)
			{
				table.RemoveRange(0, i);
			}
			return i;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x000BA2D4 File Offset: 0x000B84D4
		private static int[] FindSelectColumns(IReadOnlyList<IReadOnlyList<string>> rows, QuotingConfiguration conf)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (IReadOnlyList<string> readOnlyList in rows)
			{
				IReadOnlyList<string> readOnlyList2 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.CleanRow(readOnlyList, new QuotingConfiguration?(conf)).ToList<string>();
				for (int i = 0; i < readOnlyList2.Count; i++)
				{
					if (!string.IsNullOrEmpty(readOnlyList2[i]))
					{
						hashSet.Add(i);
					}
				}
				if (hashSet.Count == readOnlyList.Count)
				{
					return null;
				}
			}
			if (hashSet.Count <= 0)
			{
				return null;
			}
			return hashSet.OrderBy((int x) => x).ToArray<int>();
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x000BA3A8 File Offset: 0x000B85A8
		private splitLines? LearnSkipAndFilter(splitLines splitFileProg, SplitProgram splitProg, IReadOnlyList<StringRegion> records, bool recordsFiltered, int skipLineCount, out int newSkip, out bool hasEmptyLines, out string commentStrVal, out List<List<string>> table)
		{
			Witnesses.<>c__DisplayClass78_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass78_0();
			CS$<>8__locals1.splitProg = splitProg;
			hasEmptyLines = false;
			commentStrVal = null;
			table = new List<List<string>>();
			newSkip = skipLineCount;
			Witnesses.<>c__DisplayClass78_0 CS$<>8__locals2 = CS$<>8__locals1;
			SplitProgram splitProg2 = CS$<>8__locals1.splitProg;
			Optional<string>? optional;
			if (splitProg2 == null)
			{
				optional = null;
			}
			else
			{
				string delimiter = splitProg2.Properties.Delimiter;
				optional = ((delimiter != null) ? new Optional<string>?(delimiter.Some<string>()) : null);
			}
			CS$<>8__locals2.del = optional ?? Optional<string>.Nothing;
			bool flag = this.Options.SkipLinesCount != null;
			List<List<string>> list = records.Select(delegate(StringRegion record)
			{
				SplitProgram splitProg4 = CS$<>8__locals1.splitProg;
				List<string> list5;
				if (splitProg4 == null)
				{
					list5 = null;
				}
				else
				{
					list5 = (from cell in splitProg4.Run(record)
						where !cell.IsDelimiter
						select cell).Select(delegate(SplitCell cell)
					{
						StringRegion cellValue = cell.CellValue;
						if (cellValue == null)
						{
							return null;
						}
						return cellValue.Value;
					}).ToList<string>();
				}
				List<string> list6;
				if ((list6 = list5) == null)
				{
					(list6 = new List<string>()).Add(record.Source);
				}
				return list6;
			}).ToList<List<string>>();
			List<bool> list2 = list.Select((List<string> row) => row.All((string c) => c != null)).ToList<bool>();
			CS$<>8__locals1.trimmedRecords = records.Select((StringRegion record) => record.Source.Trim((char[])Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.WhiteSpaceChars)).ToList<string>();
			List<bool> list3 = records.Select((StringRegion record, int i) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsEmptyRecord(record.Source, CS$<>8__locals1.trimmedRecords[i], CS$<>8__locals1.del)).ToList<bool>();
			if (!flag)
			{
				while (newSkip > 0 && (list3[newSkip - 1] || list2[newSkip - 1]))
				{
					newSkip--;
				}
			}
			HashSet<string> hashSet = new HashSet<string>();
			if (recordsFiltered)
			{
				int num;
				int i;
				for (i = newSkip; i < list.Count; i = num)
				{
					if (!list3[i] && (CS$<>8__locals1.splitProg == null || !list2[i]))
					{
						string text = Witnesses.LikelyCommentStrs.FirstOrDefault((string cstr) => Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsCommentRecord(CS$<>8__locals1.trimmedRecords[i], cstr));
						if (text != null)
						{
							hashSet.Add(text);
							if (hashSet.Count > 1)
							{
								return null;
							}
						}
					}
					num = i + 1;
				}
			}
			bool flag2 = true;
			bool flag3 = false;
			string text2 = hashSet.FirstOrDefault<string>();
			for (int j = newSkip; j < list.Count; j++)
			{
				bool flag4 = text2 != null && Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.IsCommentRecord(CS$<>8__locals1.trimmedRecords[j], text2);
				if (list3[j] || flag4)
				{
					if (!flag && flag2)
					{
						newSkip++;
					}
					else
					{
						hasEmptyLines |= list3[j];
						flag3 = flag3 || flag4;
					}
				}
				else
				{
					if (!list2[j])
					{
						return null;
					}
					flag2 = false;
					List<List<string>> list4 = table;
					IEnumerable<string> enumerable = list[j];
					SplitProgram splitProg3 = CS$<>8__locals1.splitProg;
					list4.Add(Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.CleanRow(enumerable, (splitProg3 != null) ? new QuotingConfiguration?(splitProg3.Properties.QuotingConfiguration) : null).ToList<string>());
				}
			}
			if (!flag3)
			{
				text2 = null;
			}
			bool? flag5 = new bool?(true);
			string text3 = (CS$<>8__locals1.del.HasValue ? CS$<>8__locals1.del.Value : null);
			string text4 = text2;
			bool? flag6 = new bool?(false);
			return new splitLines?(this.SplitFileProgWith(splitFileProg, null, null, null, flag5, text3, text4, flag6, null));
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x000BA710 File Offset: 0x000B8910
		private SplitProgram LearnSplitTextProgramWithQuoting(splitLines splitFileProg, QuotingConfiguration quotingConf, IReadOnlyList<IReadOnlyList<StringRegion>> linesInputs, ref IReadOnlyList<StringRegion> records, ref IReadOnlyList<StringRegion> filteredRecords, IReadOnlyList<StringRegion> skippedLines, bool noSkip, out int skipLineCount)
		{
			Witnesses.<>c__DisplayClass79_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass79_0();
			CS$<>8__locals1.skipLineMax = ((this.Options.SkipLinesCount != null || noSkip) ? 0 : Math.Min(15, Math.Max(filteredRecords.Count - 20, 0)));
			IReadOnlyList<StringRegion> readOnlyList = filteredRecords.Skip(CS$<>8__locals1.skipLineMax).ToList<StringRegion>();
			bool flag = quotingConf.QuoteChar != null;
			bool flag2 = quotingConf.EscapeChar != null;
			IReadOnlyList<StringRegion> readOnlyList2;
			if (!flag && !flag2)
			{
				readOnlyList2 = readOnlyList;
			}
			else
			{
				IReadOnlyList<StringRegion> readOnlyList3 = readOnlyList.Where((StringRegion rec) => rec.Value.IndexOfAny((char[])Witnesses.NewLineChars) < 0).ToList<StringRegion>();
				readOnlyList2 = readOnlyList3;
			}
			IReadOnlyList<StringRegion> readOnlyList4 = readOnlyList2;
			bool flag3 = readOnlyList4.Count < readOnlyList.Count;
			bool flag4 = readOnlyList4.Count >= 20 && flag3;
			if (flag4)
			{
				readOnlyList = readOnlyList4;
			}
			QuotingConfiguration[] array;
			if (!flag)
			{
				(array = new QuotingConfiguration[1])[0] = quotingConf;
			}
			else
			{
				QuotingConfiguration[] array2 = new QuotingConfiguration[2];
				array2[0] = quotingConf.ToStandard();
				array = array2;
				array2[1] = quotingConf;
			}
			QuotingConfiguration[] array3 = array;
			StringRegion stringRegion = skippedLines.LastOrDefault<StringRegion>();
			CS$<>8__locals1.textProg = this.LearnSplitTextProgram(readOnlyList, stringRegion, array3, flag3);
			if (CS$<>8__locals1.textProg != null && flag)
			{
				bool flag5 = false;
				if (CS$<>8__locals1.textProg.Properties.Delimiter != null && CS$<>8__locals1.textProg.Properties.QuotingConfiguration.Style == QuotingStyle.Flexible)
				{
					flag5 = flag4 && !CS$<>8__locals1.<LearnSplitTextProgramWithQuoting>g__CheckSplitTextRun|1(filteredRecords);
				}
				else if (CS$<>8__locals1.textProg.Properties.Delimiter != null && CS$<>8__locals1.textProg.Properties.QuotingConfiguration.Style == QuotingStyle.Standard)
				{
					QuotingConfiguration? quotingConfiguration = new QuotingConfiguration?(CS$<>8__locals1.textProg.Properties.QuotingConfiguration);
					string delimiter = CS$<>8__locals1.textProg.Properties.Delimiter;
					splitLines splitLines = this.SplitFileProgWith(splitFileProg, null, null, null, null, delimiter, null, null, quotingConfiguration);
					List<StringRegion> list;
					IReadOnlyList<string> readOnlyList6;
					IReadOnlyList<StringRegion> readOnlyList5 = this.ExecutesSplitFileProgram(splitLines, linesInputs, this.Options.SkipFooterLinesCount, false, out list, out readOnlyList6);
					if (records.Count == readOnlyList5.Count)
					{
						if (!records.ZipWith(readOnlyList5).Any((Record<StringRegion, StringRegion> xs) => xs.Item1.Value != xs.Item2.Value))
						{
							IReadOnlyList<StringRegion> readOnlyList7 = Witnesses.FilterRecords(readOnlyList5);
							flag5 = !CS$<>8__locals1.<LearnSplitTextProgramWithQuoting>g__CheckSplitTextRun|1(readOnlyList7);
							if (!flag5)
							{
								records = readOnlyList5;
								filteredRecords = readOnlyList7;
								flag3 = records.Any((StringRegion rec) => rec.Value.IndexOfAny((char[])Witnesses.NewLineChars) >= 0);
								goto IL_02E0;
							}
							goto IL_02E0;
						}
					}
					flag5 = flag4 && !CS$<>8__locals1.<LearnSplitTextProgramWithQuoting>g__CheckSplitTextRun|1(filteredRecords);
				}
				IL_02E0:
				if (flag5)
				{
					CS$<>8__locals1.textProg = this.LearnSplitTextProgram(records.Skip(CS$<>8__locals1.skipLineMax).ToList<StringRegion>(), stringRegion, new QuotingConfiguration[] { quotingConf }, flag3);
				}
			}
			else if (CS$<>8__locals1.textProg != null && CS$<>8__locals1.textProg.Properties.Delimiter != null && CS$<>8__locals1.textProg.Properties.QuotingConfiguration.Style != QuotingStyle.Adaptive && !this.Options.IgnoreQuote)
			{
				IReadOnlyList<StringRegion> readOnlyList8 = filteredRecords.Skip(CS$<>8__locals1.skipLineMax).Concat(this.Options.AdditionalRecords).ToList<StringRegion>();
				SplitProgram splitProgram = Witnesses.TryGetQuotedProgram(CS$<>8__locals1.textProg, readOnlyList8);
				if (splitProgram != null)
				{
					CS$<>8__locals1.textProg = splitProgram;
				}
			}
			if (CS$<>8__locals1.textProg != null && (flag || flag2) && (CS$<>8__locals1.textProg.Properties.QuotingConfiguration.Style == QuotingStyle.Adaptive || (flag3 && CS$<>8__locals1.textProg.Properties.FieldPositions != null)))
			{
				CS$<>8__locals1.textProg = null;
			}
			skipLineCount = CS$<>8__locals1.skipLineMax;
			if (CS$<>8__locals1.textProg != null && CS$<>8__locals1.textProg.Properties.Delimiter == null && skipLineCount > 0)
			{
				skipLineCount = 0;
				CS$<>8__locals1.textProg = this.LearnSplitTextProgramsWithTimeout(records, new QuotingConfiguration[0], flag3);
			}
			return CS$<>8__locals1.textProg;
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x000BAB68 File Offset: 0x000B8D68
		private SplitProgram LearnSplitTextProgram(IReadOnlyList<StringRegion> records, StringRegion lastSkippedLine, IReadOnlyList<QuotingConfiguration> quotingConfs, bool hasMultilineRecs)
		{
			List<StringRegion> list = this.Options.AdditionalRecords.Concat(records).ToList<StringRegion>();
			SplitProgram splitProgram = null;
			if (Witnesses.IsPotentialDelimitedRecord(lastSkippedLine))
			{
				list.Insert(0, lastSkippedLine);
				splitProgram = this.LearnSplitTextProgramsWithTimeout(list, quotingConfs, hasMultilineRecs);
				list.RemoveAt(0);
			}
			if (splitProgram == null || splitProgram.Properties.Delimiter == null)
			{
				splitProgram = this.LearnSplitTextProgramsWithTimeout(list, quotingConfs, hasMultilineRecs);
			}
			return splitProgram;
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000BABD4 File Offset: 0x000B8DD4
		private SplitProgram LearnSplitTextProgramsWithTimeout(IReadOnlyCollection<StringRegion> records, IReadOnlyList<QuotingConfiguration> configList, bool hasMultilineRecs)
		{
			List<Constraint<StringRegion, SplitCell[]>> list = new List<Constraint<StringRegion, SplitCell[]>>();
			foreach (Constraint<StringRegion, SplitCell[]> constraint in this.Options.TextConstraints)
			{
				if (hasMultilineRecs)
				{
					if (constraint is SimpleDelimitersOrFixedWidth)
					{
						list.Add(new SimpleDelimiter());
						continue;
					}
					if (constraint is FixedWidthConstraint)
					{
						return null;
					}
				}
				list.Add(constraint);
			}
			list.AddRange(configList.Select((QuotingConfiguration conf) => new QuotingConfigurationConstraint(conf)));
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (!Debugger.IsAttached)
			{
				cancellationTokenSource.CancelAfter(this.Options.TimeLimit);
			}
			SplitProgram splitProgram;
			try
			{
				splitProgram = SplitProgramLearner.Instance.LearnTopK(list, 1, records, cancellationTokenSource.Token).FirstOrDefault<SplitProgram>();
			}
			catch (TaskCanceledException)
			{
				splitProgram = null;
			}
			return splitProgram;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000BACD4 File Offset: 0x000B8ED4
		private static bool IsPotentialDelimitedRecord(StringRegion srRecord)
		{
			string text = ((srRecord != null) ? srRecord.Value : null);
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			HashSet<char> hashSet = new HashSet<char>();
			foreach (char c in text)
			{
				if (hashSet.Count == Witnesses.LikelyDelimiters.Count)
				{
					return false;
				}
				if (Witnesses.LikelyDelimiters.Contains(c) && !hashSet.Contains(c))
				{
					string[] array = text.Split(new char[] { c });
					if (Witnesses.ValidColumnsRatio(array) > Witnesses.ValidColumnsThreshold(array.Length))
					{
						return true;
					}
					hashSet.Add(c);
				}
			}
			return false;
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x000BAD7C File Offset: 0x000B8F7C
		private static double ValidColumnsRatio(IReadOnlyCollection<string> cells)
		{
			return (double)cells.Count((string cell) => Witnesses.HeaderRegex.IsMatch(cell)) / (double)cells.Count;
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x000BADAC File Offset: 0x000B8FAC
		private static SplitProgram TryGetQuotedProgram(SplitProgram program, IReadOnlyList<StringRegion> records)
		{
			Witnesses.<>c__DisplayClass84_0 CS$<>8__locals1;
			CS$<>8__locals1.records = records;
			CS$<>8__locals1.output = Witnesses.<TryGetQuotedProgram>g__RunProgram|84_0(program, ref CS$<>8__locals1);
			QuotingConfiguration quotingConfiguration = new QuotingConfiguration(new char?('"'), true, program.Properties.QuotingConfiguration.EscapeChar, QuotingStyle.Flexible);
			SplitProgram splitProgram = Witnesses.SplitTextProgWithQuoting(program, quotingConfiguration.ToStandard());
			if (Witnesses.<TryGetQuotedProgram>g__CompareOutput|84_1(Witnesses.<TryGetQuotedProgram>g__RunProgram|84_0(splitProgram, ref CS$<>8__locals1), ref CS$<>8__locals1))
			{
				return splitProgram;
			}
			SplitProgram splitProgram2 = Witnesses.SplitTextProgWithQuoting(program, quotingConfiguration);
			if (!Witnesses.<TryGetQuotedProgram>g__CompareOutput|84_1(Witnesses.<TryGetQuotedProgram>g__RunProgram|84_0(splitProgram2, ref CS$<>8__locals1), ref CS$<>8__locals1))
			{
				return null;
			}
			return splitProgram2;
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000BAE34 File Offset: 0x000B9034
		private static IReadOnlyList<StringRegion> FilterRecords(IReadOnlyList<StringRegion> records)
		{
			List<StringRegion> list = new List<StringRegion>();
			foreach (StringRegion stringRegion in records)
			{
				string trimmed = stringRegion.Source.Trim((char[])Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.WhiteSpaceChars);
				if (!string.IsNullOrEmpty(trimmed) && !Witnesses.LikelyCommentStrs.Any((string c) => trimmed.StartsWith(c)))
				{
					list.Add(stringRegion);
				}
			}
			if (list.Count <= 0)
			{
				return records;
			}
			return list;
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x000BAED8 File Offset: 0x000B90D8
		private topSplit? LearnFwFromSchema(string schema, out IReadOnlyList<string> columnNames, Guid? guid)
		{
			columnNames = null;
			IReadOnlyList<FwColumnFormat> readOnlyList = FwFormatLearner.Learn(schema, guid);
			if (readOnlyList == null)
			{
				return null;
			}
			Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders.Nodes node = this._build.Node;
			dataLines dataLines = node.UnnamedConversion.dataLines_skippedRecords(node.UnnamedConversion.skippedRecords_skippedFooter(node.UnnamedConversion.skippedFooter_allRecords(node.UnnamedConversion.allRecords_allLines(node.Variable.allLines))));
			splitLines splitLines = node.Rule.SplitSequenceLet(dataLines, node.Rule.Sequence(node.Variable.ls));
			IReadOnlyList<Record<int, int?>> readOnlyList2 = readOnlyList.Select((FwColumnFormat f) => Record.Create<int, int?>(f.Start - 1, f.End)).ToList<Record<int, int?>>();
			regionSplit regionSplit = Witnesses.CreateRegionSplitFromFieldPositions(Witnesses.SplitTextBuilder, readOnlyList2);
			if (readOnlyList.Any((FwColumnFormat column) => column.Name != null))
			{
				columnNames = readOnlyList.Select((FwColumnFormat column) => column.Name).ToList<string>();
			}
			else if (readOnlyList.Any((FwColumnFormat column) => column.Description != null))
			{
				columnNames = readOnlyList.Select((FwColumnFormat column) => column.Description).ToList<string>();
			}
			return new topSplit?(this.CreateTopSplit(splitLines, new regionSplit?(regionSplit), null));
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x000BB05C File Offset: 0x000B925C
		public static regionSplit CreateRegionSplitFromFieldPositions(Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders builder, IReadOnlyList<Record<int, int?>> fieldPositions)
		{
			Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders.Nodes node = builder.Node;
			List<Record<int, int>> list = new List<Record<int, int>>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < fieldPositions.Count - 1; i++)
			{
				Record<int, int?> record = fieldPositions[i];
				if (i == 0 && record.Item1 > 0)
				{
					list.Add(Record.Create<int, int>(record.Item1, record.Item1));
					list2.Add(0);
				}
				Record<int, int?> record2 = fieldPositions[i + 1];
				int num = record.Item2 ?? (record.Item1 - 1);
				list.Add(Record.Create<int, int>(num, record2.Item1));
			}
			Record<int, int?> record3 = fieldPositions.Last<Record<int, int?>>();
			if (record3.Item2 != null)
			{
				list.Add(Record.Create<int, int>(record3.Item2.Value, record3.Item2.Value));
				list2.Add(list.Count - 1);
			}
			fixedWidthMatches fixedWidthMatches = node.Rule.FixedWidthDelimiters(node.Variable.v, node.Rule.delimiterPositions(list.ToArray()));
			return node.Rule.SplitRegion(node.Variable.v, node.UnnamedConversion.splitMatches_fixedWidthMatches(fixedWidthMatches), node.Rule.ignoreIndexes(list2.ToArray()), node.Rule.numSplits(fieldPositions.Count), node.Rule.delimiterStart(false), node.Rule.delimiterEnd(false), node.Rule.includeDelimiters(false), node.Rule.fillStrategy(FillStrategy.Null));
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000BB1FC File Offset: 0x000B93FC
		private topSplit? LearnMultiRecordProgram(IReadOnlyList<IReadOnlyList<StringRegion>> inputLines, out IReadOnlyList<string> newLines)
		{
			splitLines splitLines = this._build.Node.Rule.SplitSequenceLet(this._build.Node.UnnamedConversion.dataLines_skippedRecords(this._build.Node.UnnamedConversion.skippedRecords_skippedFooter(this._build.Node.UnnamedConversion.skippedFooter_allRecords(this._build.Node.UnnamedConversion.allRecords_allLines(this._build.Node.Variable.allLines)))), this._build.Node.Rule.Sequence(this._build.Node.Variable.ls));
			List<StringRegion> list;
			IReadOnlyList<string> readOnlyList2;
			IReadOnlyList<StringRegion> readOnlyList = this.ExecutesSplitFileProgram(splitLines, inputLines, 0, true, out list, out readOnlyList2);
			newLines = readOnlyList2;
			this.Options.NewLineRecordSeparator = newLines.FirstOrDefault<string>() ?? string.Empty;
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (!Debugger.IsAttached)
			{
				cancellationTokenSource.CancelAfter(this.Options.TimeLimit);
			}
			splitRecords splitRecords = this.LearnAutoMultiRecord(readOnlyList, cancellationTokenSource.Token);
			if (splitRecords.Node == null)
			{
				return null;
			}
			return new topSplit?(this.CreateTopSplit(splitLines, splitRecords, null));
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000BB338 File Offset: 0x000B9538
		internal List<StringRegion> ExecutesSplitFileProgram(splitLines fileProg, IReadOnlyList<IReadOnlyList<StringRegion>> linesInputs, int skipFooterLineCount, bool disallowLastRecordTrimming, out List<StringRegion> headerLines, out IReadOnlyList<string> newLines)
		{
			List<StringRegion> list = new List<StringRegion>();
			headerLines = new List<StringRegion>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary["\r"] = 0;
			dictionary["\n"] = 0;
			dictionary["\r\n"] = 0;
			Dictionary<string, int> dictionary2 = dictionary;
			int? num = new int?(0);
			bool? flag = new bool?(false);
			splitLines splitLines = this.SplitFileProgWith(fileProg, num, null, null, flag, null, null, null, null);
			foreach (IReadOnlyList<StringRegion> readOnlyList in linesInputs)
			{
				List<StringRegion> list2 = new List<StringRegion>();
				foreach (IEnumerable<StringRegion> enumerable in this.RunFileProg(fileProg, readOnlyList))
				{
					StringRegion[] array = enumerable.ToArray<StringRegion>();
					string text = string.Join("", array.Select((StringRegion l) => l.Value));
					string text2 = Witnesses.TrimEndingNewLine(text);
					list2.Add(new StringRegion(text2, Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens));
					string text3 = text.Substring(text2.Length);
					if (!string.IsNullOrEmpty(text3))
					{
						Dictionary<string, int> dictionary3 = dictionary2;
						string text4 = text3;
						dictionary3[text4]++;
					}
				}
				if (!list2.IsEmpty<StringRegion>())
				{
					if (skipFooterLineCount > 0 && readOnlyList.Count < this.Options.ReadInputLineCount && list2.Count > skipFooterLineCount)
					{
						list2.RemoveRange(list2.Count - skipFooterLineCount, skipFooterLineCount);
					}
					else if (!disallowLastRecordTrimming && (readOnlyList.Count == this.Options.ReadInputLineCount || list2.Count > 20))
					{
						list2.RemoveAt(list2.Count - 1);
					}
					int skipLines = this.GetSkipLines(fileProg);
					IEnumerable<StringRegion> enumerable2 = from ls in this.RunFileProg(splitLines, readOnlyList).Take(skipLines)
						select new StringRegion(string.Join("", ls.Select((StringRegion l) => l.Value)), Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens);
					list.AddRange(list2);
					headerLines.AddRange(enumerable2);
				}
			}
			newLines = (from kv in dictionary2
				where kv.Value > 0
				orderby kv.Value descending
				select kv.Key).ToList<string>();
			return list;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000BB620 File Offset: 0x000B9820
		private IEnumerable<IEnumerable<StringRegion>> RunFileProg(splitLines prog, IEnumerable<StringRegion> lines)
		{
			State state = State.CreateForExecution(this._build.Symbol.allLines, lines);
			object obj = prog.Node.Invoke(state);
			if (obj == null)
			{
				return null;
			}
			return obj.ToEnumerable<object>().Cast<IEnumerable<StringRegion>>();
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000BB664 File Offset: 0x000B9864
		private static string TrimEndingNewLine(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			int num = (int)s[s.Length - 1];
			if (num == 13)
			{
				return s.Slice(new int?(0), new int?(-1), 1);
			}
			if (num != 10)
			{
				return s;
			}
			if (s.Length == 1 || s[s.Length - 2] != '\r')
			{
				return s.Slice(new int?(0), new int?(-1), 1);
			}
			return s.Slice(new int?(0), new int?(-2), 1);
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000BB6F0 File Offset: 0x000B98F0
		private static List<string> SplitIntoCells(SplitProgram splitTextProgram, StringRegion record, bool simpleTrim)
		{
			if (splitTextProgram == null)
			{
				return new List<string> { record.Value };
			}
			IEnumerable<string> enumerable = (from cell in splitTextProgram.Run(record)
				where !cell.IsDelimiter
				select cell).Select(delegate(SplitCell cell)
			{
				StringRegion cellValue = cell.CellValue;
				return ((cellValue != null) ? cellValue.Value : null) ?? string.Empty;
			});
			IEnumerable<string> enumerable2;
			if (!simpleTrim)
			{
				enumerable2 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.CleanRow(enumerable, new QuotingConfiguration?(splitTextProgram.Properties.QuotingConfiguration));
			}
			else
			{
				enumerable2 = enumerable.Select((string cell) => cell.Trim(Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.ColumnNameTrimChars.ToArray<char>()));
			}
			enumerable = enumerable2;
			return enumerable.ToList<string>();
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x000BB7B0 File Offset: 0x000B99B0
		private skippedRecords GetSkippedRecords(dataLines dataLines)
		{
			FilterHeader filterHeader;
			if (dataLines.Is_FilterHeader(this._build, out filterHeader))
			{
				return filterHeader.skippedRecords;
			}
			SelectData selectData;
			if (dataLines.Is_SelectData(this._build, out selectData))
			{
				return selectData.skippedRecords;
			}
			FilterRecords filterRecords;
			if (dataLines.Is_FilterRecords(this._build, out filterRecords))
			{
				return filterRecords.skippedRecords;
			}
			dataLines_skippedRecords dataLines_skippedRecords;
			if (dataLines.Is_dataLines_skippedRecords(this._build, out dataLines_skippedRecords))
			{
				return dataLines_skippedRecords.skippedRecords;
			}
			throw new Exception(string.Format("Unknown {0}: {1}", "dataLines", dataLines));
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000BB83C File Offset: 0x000B9A3C
		private skippedFooter GetSkippedFooter(skippedRecords skippedRecords)
		{
			Skip skip;
			if (skippedRecords.Is_Skip(this._build, out skip))
			{
				return skip.skippedFooter;
			}
			skippedRecords_skippedFooter skippedRecords_skippedFooter;
			if (skippedRecords.Is_skippedRecords_skippedFooter(this._build, out skippedRecords_skippedFooter))
			{
				return skippedRecords_skippedFooter.skippedFooter;
			}
			throw new Exception(string.Format("Unknown {0} {1}", "skippedRecords", skippedRecords));
		}

		// Token: 0x06003BB6 RID: 15286 RVA: 0x000BB898 File Offset: 0x000B9A98
		private allRecords GetAllRecords(skippedFooter skippedFooter)
		{
			SkipFooter skipFooter;
			if (skippedFooter.Is_SkipFooter(this._build, out skipFooter))
			{
				return skipFooter.allRecords;
			}
			skippedFooter_allRecords skippedFooter_allRecords;
			if (skippedFooter.Is_skippedFooter_allRecords(this._build, out skippedFooter_allRecords))
			{
				return skippedFooter_allRecords.allRecords;
			}
			throw new Exception(string.Format("Unknown {0} {1}", "skippedFooter", skippedFooter));
		}

		// Token: 0x06003BB7 RID: 15287 RVA: 0x000BB8F4 File Offset: 0x000B9AF4
		private int GetSkipLines(splitLines prog)
		{
			dataLines dataLines = prog.Cast_SplitSequenceLet().dataLines;
			Skip skip;
			if (!this.GetSkippedRecords(dataLines).Is_Skip(this._build, out skip))
			{
				return 0;
			}
			return skip.k.Value;
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000BB93C File Offset: 0x000B9B3C
		private Optional<int> GetHeaderIndex(splitLines prog)
		{
			dataLines dataLines = prog.Cast_SplitSequenceLet().dataLines;
			Skip skip;
			if (!this.GetSkippedRecords(dataLines).Is_Skip(this._build, out skip))
			{
				return Optional<int>.Nothing;
			}
			return skip.headerIndex.Value;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x000BB988 File Offset: 0x000B9B88
		private QuotingConfiguration GetQuotingConf(splitLines prog)
		{
			QuoteRecords quoteRecords;
			if (!this.GetAllRecords(this.GetSkippedFooter(this.GetSkippedRecords(prog.Cast_SplitSequenceLet().dataLines))).Is_QuoteRecords(this._build, out quoteRecords))
			{
				return default(QuotingConfiguration);
			}
			return quoteRecords.quotingConfig.Value;
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x000BB9E4 File Offset: 0x000B9BE4
		private string GetCommentStr(splitLines prog)
		{
			FilterRecords filterRecords;
			if (!prog.Cast_SplitSequenceLet().dataLines.Is_FilterRecords(this._build, out filterRecords))
			{
				return null;
			}
			if (!filterRecords.commentStr.Value.HasValue)
			{
				return null;
			}
			return filterRecords.commentStr.Value.Value;
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x000BBA48 File Offset: 0x000B9C48
		private bool GetSkipEmpty(splitLines prog)
		{
			FilterRecords filterRecords;
			return prog.Cast_SplitSequenceLet().dataLines.Is_FilterRecords(this._build, out filterRecords) && filterRecords.skipEmpty.Value;
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x000BBA88 File Offset: 0x000B9C88
		private topSplit CreateTopSplit(splitLines splitFileProg, SplitProgram splitTextProg, int[] selectColumns)
		{
			regionSplit? regionSplit = ((splitTextProg == null) ? null : new regionSplit?(Witnesses.SplitTextBuilder.Node.Cast.regionSplit(splitTextProg.ProgramNode)));
			return this.CreateTopSplit(splitFileProg, regionSplit, selectColumns);
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x000BBAD4 File Offset: 0x000B9CD4
		private topSplit CreateTopSplit(splitLines splitFileProg, regionSplit? splitTextProg, int[] selectColumns)
		{
			Optional<int> headerIndex = this.GetHeaderIndex(splitFileProg);
			hasHeader hasHeader = this._build.Node.Rule.hasHeader(headerIndex.HasValue);
			splitRecords splitRecords = ((splitTextProg == null) ? this._build.Node.Rule.NoSplit(this._build.Node.Variable.records, hasHeader) : this._build.Node.Rule.TableFromCells(this._build.Node.Rule.SplitToCells(this._build.Node.Rule.SplitTextProg(splitTextProg.Value), this._build.Node.Variable.records), hasHeader));
			return this.CreateTopSplit(splitFileProg, splitRecords, selectColumns);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x000BBBA4 File Offset: 0x000B9DA4
		private topSplit CreateTopSplit(splitLines splitFileProg, splitRecords splitRecord, int[] selectColumns)
		{
			splitRecordsSelect splitRecordsSelect = ((this.Options.FilterEmptyColummns && selectColumns != null) ? this._build.Node.Rule.SelectColumns(this._build.Node.Rule.columnList(selectColumns), splitRecord) : this._build.Node.UnnamedConversion.splitRecordsSelect_splitRecords(splitRecord));
			return this._build.Node.Rule.LetFileRecordSplit(this._build.Node.Rule.LetSplitFile(this._build.Node.Rule.SplitFile(this._build.Node.Variable.file), this._build.Node.Rule.MergeRecordLines(splitFileProg)), splitRecordsSelect);
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000BBC74 File Offset: 0x000B9E74
		private static SplitProgram SplitTextProgWithQuoting(SplitProgram program, QuotingConfiguration conf)
		{
			Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders splitTextBuilder = Witnesses.SplitTextBuilder;
			SplitRegion value = splitTextBuilder.Node.Cast.regionSplit(program.ProgramNode).As_SplitRegion(splitTextBuilder).Value;
			return new SplitProgram(splitTextBuilder.Node.Rule.SplitRegion(value.v, splitTextBuilder.Node.UnnamedConversion.splitMatches_constantDelimiterMatches(splitTextBuilder.Node.Rule.ConstantDelimiterWithQuoting(value.v, splitTextBuilder.Node.Rule.s(program.Properties.Delimiter), splitTextBuilder.Node.Rule.quotingConf(conf))), value.ignoreIndexes, value.numSplits, value.delimiterStart, value.delimiterEnd, value.includeDelimiters, value.fillStrategy));
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000BBD48 File Offset: 0x000B9F48
		private splitLines SplitFileProgWith(splitLines prog, int? skip = null, int? headerIndex = null, int? skipFooter = null, bool? skipEmpty = null, string delimiter = null, string comment = null, bool? hasCommentHeader = null, QuotingConfiguration? quotingConf = null)
		{
			SplitSequenceLet splitSequenceLet = prog.Cast_SplitSequenceLet();
			dataLines dataLines = splitSequenceLet.dataLines;
			skippedRecords skippedRecords = this.GetSkippedRecords(dataLines);
			skippedFooter skippedFooter = this.GetSkippedFooter(skippedRecords);
			allRecords allRecords = this.GetAllRecords(skippedFooter);
			Optional<string> optional = ((delimiter != null) ? delimiter.Some<string>() : Optional<string>.Nothing);
			bool flag = false;
			if (quotingConf != null)
			{
				allRecords = ((quotingConf.Value.QuoteChar != null || quotingConf.Value.EscapeChar != null) ? this._build.Node.Rule.QuoteRecords(this._build.Node.Rule.quotingConfig(quotingConf.Value), this._build.Node.Rule.delimiter(optional), this._build.Node.Variable.allLines) : this._build.Node.UnnamedConversion.allRecords_allLines(this._build.Node.Variable.allLines));
				flag = true;
			}
			if (skipFooter != null)
			{
				skippedFooter = ((skipFooter.Value == 0) ? this._build.Node.UnnamedConversion.skippedFooter_allRecords(allRecords) : this._build.Node.Rule.SkipFooter(this._build.Node.Rule.k(skipFooter.Value), allRecords));
				flag = true;
			}
			else if (flag)
			{
				SkipFooter skipFooter2;
				skippedFooter = (skippedFooter.Is_SkipFooter(this._build, out skipFooter2) ? this._build.Node.Rule.SkipFooter(skipFooter2.k, allRecords) : this._build.Node.UnnamedConversion.skippedFooter_allRecords(allRecords));
			}
			if (skip != null)
			{
				skippedRecords = ((skip.Value == 0) ? this._build.Node.UnnamedConversion.skippedRecords_skippedFooter(skippedFooter) : this._build.Node.Rule.Skip(this._build.Node.Rule.k(skip.Value), this._build.Node.Rule.headerIndex((headerIndex != null) ? headerIndex.Some<int>() : Optional<int>.Nothing), skippedFooter));
				flag = true;
			}
			else if (flag)
			{
				Skip skip2;
				skippedRecords = (skippedRecords.Is_Skip(this._build, out skip2) ? this._build.Node.Rule.Skip(skip2.k, skip2.headerIndex, skippedFooter) : this._build.Node.UnnamedConversion.skippedRecords_skippedFooter(skippedFooter));
			}
			if (skipEmpty != null || comment != null)
			{
				dataLines = (((skipEmpty != null && skipEmpty.Value) || comment != null) ? this._build.Node.Rule.FilterRecords(this._build.Node.Rule.skipEmpty(skipEmpty == null || skipEmpty.Value), this._build.Node.Rule.delimiter(optional), this._build.Node.Rule.commentStr((comment != null) ? comment.Some<string>() : Optional<string>.Nothing), this._build.Node.Rule.hasCommentHeader(hasCommentHeader != null && hasCommentHeader.Value), skippedRecords) : this._build.Node.UnnamedConversion.dataLines_skippedRecords(skippedRecords));
			}
			else if (flag)
			{
				FilterRecords filterRecords;
				FilterHeader filterHeader;
				SelectData selectData;
				if (dataLines.Is_FilterRecords(this._build, out filterRecords))
				{
					dataLines = this._build.Node.Rule.FilterRecords(filterRecords.skipEmpty, filterRecords.delimiter, filterRecords.commentStr, filterRecords.hasCommentHeader, skippedRecords);
				}
				else if (dataLines.Is_FilterHeader(this._build, out filterHeader))
				{
					dataLines = this._build.Node.Rule.FilterHeader(filterHeader.basicLinePredicate, skippedRecords);
				}
				else if (dataLines.Is_SelectData(this._build, out selectData))
				{
					dataLines = this._build.Node.Rule.SelectData(selectData.basicLinePredicate, skippedRecords);
				}
				else
				{
					dataLines = this._build.Node.UnnamedConversion.dataLines_skippedRecords(skippedRecords);
				}
			}
			return this._build.Node.Rule.SplitSequenceLet(dataLines, splitSequenceLet.splitSequence);
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000BC1CC File Offset: 0x000BA3CC
		[WitnessFunction("SplitSequenceLet", 0)]
		internal PrefixSpec WitnessFilteredLinesInSplitSequenceLet(GrammarRule rule, PrefixSpec spec)
		{
			return new PrefixSpec(spec.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, (KeyValuePair<State, IEnumerable<object>> kv) => kv.Value.SelectMany((object list) => list.ToEnumerable<object>())));
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000BC228 File Offset: 0x000BA428
		[WitnessFunction("SplitSequenceLet", 1, DependsOnParameters = new int[] { 0 })]
		internal PrefixSpec WitnessSplitSequenceInSplitSequenceLet(GrammarRule rule, PrefixSpec spec, ExampleSpec lsSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				State key = keyValuePair.Key;
				State state = key.Bind(this._build.Symbol.ls, lsSpec.Examples[key].ToEnumerable<object>().Cast<StringRegion>());
				dictionary[state] = keyValuePair.Value;
			}
			return new PrefixSpec(dictionary);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000BC2C0 File Offset: 0x000BA4C0
		[WitnessFunction("SplitSequence", 0)]
		internal DisjunctiveExamplesSpec WitnessRInSplitSequence(GrammarRule rule, PrefixSpec spec)
		{
			if (this.Options.IgnoreSplitSequence)
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				State key = keyValuePair.Key;
				IEnumerable<Record<LearningCacheSubstring, uint>[]> enumerable = ((IEnumerable<IEnumerable<StringRegion>>)keyValuePair.Value).Select((IEnumerable<StringRegion> rec) => rec.Select((StringRegion l) => new Record<LearningCacheSubstring, uint>(l, l.Start)).ToArray<Record<LearningCacheSubstring, uint>>());
				List<Record<LearningCacheSubstring, uint>> list = new List<Record<LearningCacheSubstring, uint>>();
				List<Record<LearningCacheSubstring, uint>> list2 = new List<Record<LearningCacheSubstring, uint>>();
				foreach (Record<LearningCacheSubstring, uint>[] array in enumerable)
				{
					list.Add(array[0]);
					list2.AddRange(array.Skip(1));
				}
				IEnumerable<RegularExpression> enumerable2 = RegularExpression.LearnRightMatches(list, list2, 1, 0);
				dictionary[key] = enumerable2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000BC3D8 File Offset: 0x000BA5D8
		[WitnessFunction("Skip", 0, DependsOnParameters = new int[] { 2 })]
		internal DisjunctiveExamplesSpec WitnessKInSkip(GrammarRule rule, PrefixSpec spec, ExampleSpec records)
		{
			if (this.Options.IgnoreSkip)
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				State key = keyValuePair.Key;
				StringRegion[] array = ((IEnumerable<StringRegion>)records.Examples[key]).ToArray<StringRegion>();
				StringRegion[] array2 = keyValuePair.Value.Cast<StringRegion>().ToArray<StringRegion>();
				int? num = array.IndexOf(array2[0]);
				if (num != null)
				{
					int? num2 = num;
					int num3 = 0;
					if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
					{
						if (array.Skip(num.Value).ZipWith(array2).Any((Record<StringRegion, StringRegion> tup) => tup.Item1 != tup.Item2))
						{
							return null;
						}
						dictionary[key] = new object[] { num.Value };
						continue;
					}
				}
				return null;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000BC508 File Offset: 0x000BA708
		[WitnessFunction("Skip", 1)]
		internal DisjunctiveExamplesSpec WitnessHeaderIndexInSkip(GrammarRule rule, PrefixSpec spec)
		{
			if (this.Options.IgnoreSkip)
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				dictionary[keyValuePair.Key] = new object[] { Optional<int>.Nothing };
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000BC58C File Offset: 0x000BA78C
		[WitnessFunction("FilterHeader", 1)]
		internal SubsequenceSpec WitnessLinesInFilterHeader(GrammarRule rule, PrefixSpec spec)
		{
			if (!this.Options.IgnoreFilterHeader)
			{
				return new SubsequenceSpec(spec.PositiveExamples);
			}
			return null;
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000BC5A8 File Offset: 0x000BA7A8
		[WitnessFunction("SelectData", 1)]
		internal SubsequenceSpec WitnessLinesInSelectData(GrammarRule rule, PrefixSpec spec)
		{
			if (!this.Options.IgnoreSelectData)
			{
				return new SubsequenceSpec(spec.PositiveExamples);
			}
			return null;
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000BC5C4 File Offset: 0x000BA7C4
		[WitnessFunction("StartsWith", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessRinStartsWith(BlackBoxRule rule, ExampleSpec spec, ExampleSpec sSpec)
		{
			List<Record<LearningCacheSubstring, uint>> list = new List<Record<LearningCacheSubstring, uint>>();
			List<Record<LearningCacheSubstring, uint>> list2 = new List<Record<LearningCacheSubstring, uint>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				LearningCacheSubstring learningCacheSubstring = (LearningCacheSubstring)sSpec.Examples[key];
				if (learningCacheSubstring == null)
				{
					return null;
				}
				if ((bool)spec.Examples[key])
				{
					list.Add(Record.Create<LearningCacheSubstring, uint>(learningCacheSubstring, learningCacheSubstring.Start));
				}
				else
				{
					list2.Add(Record.Create<LearningCacheSubstring, uint>(learningCacheSubstring, learningCacheSubstring.Start));
				}
			}
			RegularExpression[] regexes = (from regex in RegularExpression.LearnRightMatches(list, list2, RegularExpression.DefaultTokenCount, 0)
				where regex.Count > 0
				select regex).ToArray<RegularExpression>();
			return DisjunctiveExamplesSpec.From(spec.Examples.ToDictionary((KeyValuePair<State, object> example) => example.Key, (KeyValuePair<State, object> example) => regexes));
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000BC700 File Offset: 0x000BA900
		// Note: this type is marked as 'beforefieldinit'.
		static Witnesses()
		{
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			dictionary["KthKeyValue"] = 1.0;
			dictionary["KthKeyQuote"] = 1.0;
			dictionary["KthTwoLineKeyValue"] = 0.8;
			dictionary["KthKeyValueFw"] = 0.6;
			dictionary["KthLine"] = 0.6;
			Witnesses.ColumnSelectorWeights = dictionary;
			Witnesses.LikelyQuoteChars = new char?[]
			{
				new char?('"'),
				null,
				new char?('|')
			};
			Witnesses.LikelyEscapeChars = new char?[]
			{
				null,
				new char?('\\')
			};
			Witnesses.PrefixRegexes = new Lazy<IReadOnlyList<Record<RegularExpression, Regex>>>(() => Witnesses.GetPrefixRegexes().ToList<Record<RegularExpression, Regex>>());
			Witnesses.SplitTextBuilder = Language.Build;
			Witnesses.ValidColumnsThreshold = delegate(int colNum)
			{
				if (colNum > 4)
				{
					return 0.2;
				}
				return 0.5;
			};
			Witnesses.FixedWidthValidColumnsThreshold = (int _) => 0.8;
			Witnesses.LikelyDelimiters = new char[] { ',', '\t', ' ', ';', '|' };
			Witnesses.LikelyCommentStrs = new string[] { "#", "%" };
			Witnesses.NewLineChars = new char[] { '\n', '\r' };
			Witnesses.ColumnNameLearningTokens = new Lazy<IReadOnlyDictionary<string, Token>>(delegate
			{
				Dictionary<string, Token> dictionary2 = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.Tokens.ToDictionary<string, Token>();
				dictionary2.Remove("Digits");
				dictionary2.Remove("SignedNumber");
				dictionary2.Remove("Alphanum");
				return dictionary2;
			});
			Witnesses.HeaderRegex = new Regex("^(\\d+_)?[\\p{L}\\s/._\\-()\\[\\]#]+\\d*\\)?$", RegexOptions.Compiled);
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000BC8D0 File Offset: 0x000BAAD0
		[CompilerGenerated]
		internal static string[][] <TryGetQuotedProgram>g__RunProgram|84_0(SplitProgram prog, ref Witnesses.<>c__DisplayClass84_0 A_1)
		{
			return A_1.records.Select((StringRegion record) => prog.Run(record).Select(delegate(SplitCell cell)
			{
				StringRegion cellValue = cell.CellValue;
				if (cellValue == null)
				{
					return null;
				}
				return cellValue.Value;
			}).ToArray<string>()).ToArray<string[]>();
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x000BC908 File Offset: 0x000BAB08
		[CompilerGenerated]
		internal static bool <TryGetQuotedProgram>g__CompareOutput|84_1(IReadOnlyCollection<IReadOnlyCollection<string>> other, ref Witnesses.<>c__DisplayClass84_0 A_1)
		{
			if (A_1.output.Length != other.Count)
			{
				return false;
			}
			return A_1.output.ZipWith(other).All(delegate(Record<string[], IReadOnlyCollection<string>> recs)
			{
				if (recs.Item1.Length == recs.Item2.Count)
				{
					return recs.Item1.ZipWith(recs.Item2).All((Record<string, string> cell) => cell.Item1 == cell.Item2);
				}
				return false;
			});
		}

		// Token: 0x04001B75 RID: 7029
		private const double MinSelectRatio = 0.5;

		// Token: 0x04001B76 RID: 7030
		private static readonly IReadOnlyCollection<string> LikelySeparators = new string[] { ":", "=" };

		// Token: 0x04001B77 RID: 7031
		private static readonly IReadOnlyCollection<Regex> LikelyKeys = new Regex[]
		{
			new Regex("^\\p{L}+(\\s?[\\p{L}\\p{N}\\[\\]_-])*$", RegexOptions.Compiled)
		};

		// Token: 0x04001B78 RID: 7032
		private static readonly IReadOnlyCollection<Regex> BadKeyValueLines = new Regex[]
		{
			new Regex("^https?://", RegexOptions.Compiled)
		};

		// Token: 0x04001B79 RID: 7033
		private static readonly IReadOnlyCollection<char> WhitespaceChars = Microsoft.ProgramSynthesis.Compound.Split.Semantics.Semantics.KeyValueTrimChars;

		// Token: 0x04001B7A RID: 7034
		private static readonly IReadOnlyDictionary<string, double> ColumnSelectorWeights;

		// Token: 0x04001B7B RID: 7035
		private const string WhiteSpaceTokenName = "WhiteSpace";

		// Token: 0x04001B7C RID: 7036
		private static readonly IReadOnlyList<char?> LikelyQuoteChars;

		// Token: 0x04001B7D RID: 7037
		private static readonly IReadOnlyList<char?> LikelyEscapeChars;

		// Token: 0x04001B7E RID: 7038
		private static readonly Lazy<IReadOnlyList<Record<RegularExpression, Regex>>> PrefixRegexes;

		// Token: 0x04001B7F RID: 7039
		private readonly Microsoft.ProgramSynthesis.Compound.Split.Build.GrammarBuilders _build;

		// Token: 0x04001B80 RID: 7040
		private readonly Symbol _startSymbol;

		// Token: 0x04001B81 RID: 7041
		private readonly IReadOnlyCollection<int> SkipLineMaxCandidates = new int[] { 15, 30 };

		// Token: 0x04001B83 RID: 7043
		private const int DelimiterLinesMin = 20;

		// Token: 0x04001B84 RID: 7044
		private const int DelimiterSkipLineMax = 15;

		// Token: 0x04001B85 RID: 7045
		private const double MinHoleColumnRatio = 0.5;

		// Token: 0x04001B86 RID: 7046
		private const double MinHoleAvgRatio = 0.5;

		// Token: 0x04001B87 RID: 7047
		private const double MaxSparseDataRows = 0.2;

		// Token: 0x04001B88 RID: 7048
		private const double MaxDistinctValRatio = 0.2;

		// Token: 0x04001B89 RID: 7049
		private static readonly Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders SplitTextBuilder;

		// Token: 0x04001B8A RID: 7050
		private static readonly Func<int, double> ValidColumnsThreshold;

		// Token: 0x04001B8B RID: 7051
		private static readonly Func<int, double> FixedWidthValidColumnsThreshold;

		// Token: 0x04001B8C RID: 7052
		private static readonly IReadOnlyList<char> LikelyDelimiters;

		// Token: 0x04001B8D RID: 7053
		private static readonly IReadOnlyList<string> LikelyCommentStrs;

		// Token: 0x04001B8E RID: 7054
		private static readonly IReadOnlyCollection<char> NewLineChars;

		// Token: 0x04001B8F RID: 7055
		private static readonly Lazy<IReadOnlyDictionary<string, Token>> ColumnNameLearningTokens;

		// Token: 0x04001B90 RID: 7056
		private static readonly Regex HeaderRegex;

		// Token: 0x020009B4 RID: 2484
		public struct LearnResult
		{
			// Token: 0x06003BCC RID: 15308 RVA: 0x000BC957 File Offset: 0x000BAB57
			public LearnResult(topSplit program, IReadOnlyList<string> columnNames, IReadOnlyList<string> newLines, int skipEmptyAndCommentLinesCount, bool hasEmptyLines)
			{
				this.Program = program;
				this.ColumnNames = columnNames;
				this.NewLines = newLines;
				this.SkipEmptyAndCommentLinesCount = skipEmptyAndCommentLinesCount;
				this.HasEmptyLines = hasEmptyLines;
			}

			// Token: 0x17000A96 RID: 2710
			// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000BC97E File Offset: 0x000BAB7E
			public readonly topSplit Program { get; }

			// Token: 0x17000A97 RID: 2711
			// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000BC986 File Offset: 0x000BAB86
			public readonly IReadOnlyList<string> ColumnNames { get; }

			// Token: 0x17000A98 RID: 2712
			// (get) Token: 0x06003BCF RID: 15311 RVA: 0x000BC98E File Offset: 0x000BAB8E
			public readonly IReadOnlyList<string> NewLines { get; }

			// Token: 0x17000A99 RID: 2713
			// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000BC996 File Offset: 0x000BAB96
			public readonly int SkipEmptyAndCommentLinesCount { get; }

			// Token: 0x17000A9A RID: 2714
			// (get) Token: 0x06003BD1 RID: 15313 RVA: 0x000BC99E File Offset: 0x000BAB9E
			public readonly bool HasEmptyLines { get; }
		}

		// Token: 0x020009B5 RID: 2485
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001B96 RID: 7062
			public static Func<ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>, ProgramSetBuilder<splitLines>> <0>__Intersect;

			// Token: 0x04001B97 RID: 7063
			public static Func<string, HashSet<RegularExpression>> <1>__LearnLongestPrefixRegexes;

			// Token: 0x04001B98 RID: 7064
			public static Func<string, bool> <2>__IsNullOrEmpty;
		}
	}
}
