using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract
{
	// Token: 0x02000F86 RID: 3974
	internal class BetweenDelimitersExtractLearner : ExtractLearner
	{
		// Token: 0x06006E13 RID: 28179 RVA: 0x00167304 File Offset: 0x00165504
		protected override LearnResult<extract> LearnImpl(ExtractExample example, GrammarBuilders builder, LearnResult<extract> topResult, CancellationToken cancel)
		{
			List<string> list = null;
			List<string> list2 = null;
			foreach (Record<StringRegion, StringRegionCell> record in example.Examples)
			{
				StringRegion stringRegion;
				StringRegionCell stringRegionCell;
				record.Deconstruct(out stringRegion, out stringRegionCell);
				StringRegion stringRegion2 = stringRegion;
				StringRegionCell stringRegionCell2 = stringRegionCell;
				if (stringRegionCell2 == null)
				{
					return null;
				}
				if (list == null)
				{
					list = new List<string>();
					if (stringRegion2.Start == stringRegionCell2.Start)
					{
						list.Add(null);
					}
					else
					{
						string value = stringRegion2.Slice(stringRegion2.Start, stringRegionCell2.Start).Value;
						int num = 0;
						int i = value.Length - 1;
						while (i >= Math.Max(0, value.Length - 10))
						{
							char c = value[i];
							if (c == '\r' || c == '\n')
							{
								num++;
								if (num > 2)
								{
									break;
								}
								goto IL_00D3;
							}
							else if (!char.IsLetter(c) || i <= 0 || !char.IsLetter(value[i - 1]))
							{
								goto IL_00D3;
							}
							IL_0111:
							i--;
							continue;
							IL_00D3:
							string text = value.Substring(i);
							int num2 = value.IndexOf(text, StringComparison.Ordinal);
							if ((ulong)stringRegion2.Start + (ulong)((long)num2) + (ulong)((long)text.Length) == (ulong)stringRegionCell2.Start)
							{
								list.Add(text);
							}
							goto IL_0111;
						}
					}
					list2 = new List<string>();
					if (stringRegionCell2.End == stringRegion2.End)
					{
						list2.Add(null);
					}
					else
					{
						string value2 = stringRegion2.Slice(stringRegionCell2.End, stringRegion2.End).Value;
						int num3 = 0;
						int j = 1;
						while (j <= Math.Min(value2.Length, 10))
						{
							char c2 = value2[j - 1];
							if (c2 == '\r' || c2 == '\n')
							{
								num3++;
								if (num3 > 2)
								{
									break;
								}
								goto IL_01C4;
							}
							else if (!char.IsLetter(c2) || j >= value2.Length || !char.IsLetter(value2[j]))
							{
								goto IL_01C4;
							}
							IL_0204:
							j++;
							continue;
							IL_01C4:
							string text2 = value2.Substring(0, j);
							Optional<uint> optional = stringRegion2.PositionOf(text2, stringRegionCell2.Start, StringComparison.Ordinal);
							if (optional.HasValue && optional.Value == stringRegionCell2.End)
							{
								list2.Add(text2);
							}
							goto IL_0204;
						}
					}
				}
				else
				{
					for (int k = list.Count - 1; k >= 0; k--)
					{
						string text3 = list[k];
						int num4 = ((text3 != null) ? text3.Length : 0);
						Optional<uint> optional2 = ((text3 == null) ? stringRegion2.Start.Some<uint>() : stringRegion2.PositionOf(text3, StringComparison.Ordinal));
						if (!optional2.HasValue || (ulong)optional2.Value + (ulong)((long)num4) != (ulong)stringRegionCell2.Start)
						{
							list.RemoveAt(k);
						}
					}
					for (int l = list2.Count - 1; l >= 0; l--)
					{
						string text4 = list2[l];
						Optional<uint> optional3 = ((text4 == null) ? stringRegion2.End.Some<uint>() : stringRegion2.PositionOf(text4, stringRegionCell2.Start, StringComparison.Ordinal));
						if (!optional3.HasValue || optional3.Value != stringRegionCell2.End)
						{
							list2.RemoveAt(l);
						}
					}
					if (list.Count == 0 && list2.Count == 0)
					{
						break;
					}
				}
			}
			if (list == null || list2 == null || (list.Count == 0 && list2.Count == 0))
			{
				return null;
			}
			IEnumerable<Record<string, string>> enumerable = list.CartesianProduct(list2).OrderBy(delegate(Record<string, string> tup)
			{
				string item = tup.Item1;
				if (item == null)
				{
					string item2 = tup.Item2;
					int? num5 = ((item2 != null) ? new int?(item2.Length) : null);
					return ((num5 != null) ? new int?(num5.GetValueOrDefault()) : null).GetValueOrDefault();
				}
				return item.Length;
			}).Take(5);
			LearnResult<extract> learnResult = topResult;
			foreach (Record<string, string> record2 in enumerable)
			{
				string text5;
				string text6;
				record2.Deconstruct(out text5, out text6);
				string text7 = text5;
				string text8 = text6;
				if (cancel.IsCancellationRequested)
				{
					break;
				}
				Optional<string> startDelimiter = text7.Some<string>();
				Optional<string> endDelimiter = text8.Some<string>();
				LearnResult<extract> learnResult2 = LearnResult<extract>.BuildLearnResult(builder.Set.Join.BetweenDelimiters(ProgramSetBuilder.List<row>(builder.Symbol.row, new row[] { builder.Node.Variable.row }), ProgramSetBuilder.List<del>(builder.Symbol.del, new del[] { builder.Node.Rule.del(startDelimiter) }), ProgramSetBuilder.List<del>(builder.Symbol.del, new del[] { builder.Node.Rule.del(endDelimiter) })), example, (StringRegion input) => Semantics.BetweenDelimiters(input, startDelimiter, endDelimiter));
				if (!learnResult2.HasNullOrWhiteSpace)
				{
					return learnResult2;
				}
				if (learnResult2 > learnResult)
				{
					learnResult = learnResult2;
				}
			}
			if (!(learnResult > topResult))
			{
				return null;
			}
			return learnResult;
		}

		// Token: 0x04002FF2 RID: 12274
		private const int MaxNewLineCharCount = 2;

		// Token: 0x04002FF3 RID: 12275
		private const int MaxDelimiterLength = 10;
	}
}
