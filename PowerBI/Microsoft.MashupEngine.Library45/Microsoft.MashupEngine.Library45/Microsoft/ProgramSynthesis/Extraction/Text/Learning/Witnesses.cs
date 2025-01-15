using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Build;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning.Extract;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning.Split;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F5A RID: 3930
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06006D5B RID: 27995 RVA: 0x001648C0 File Offset: 0x00162AC0
		public Witnesses(Grammar grammar, Witnesses.Options options)
			: base(grammar)
		{
			this._build = GrammarBuilders.Instance(grammar);
			this._options = options;
		}

		// Token: 0x06006D5C RID: 27996 RVA: 0x001648DC File Offset: 0x00162ADC
		[RuleLearner("Table")]
		internal Optional<ProgramSet> LearnTable(SynthesisEngine engine, NonterminalRule rule, LearningTask<PrefixTableSpec> task, CancellationToken cancel)
		{
			KeyValuePair<State, ITable<object>> keyValuePair = task.Spec.PrefixTables.First<KeyValuePair<State, ITable<object>>>();
			StringRegion stringRegion = (StringRegion)keyValuePair.Key[this._build.Symbol.v];
			ITable<ExampleCell> table = (ITable<ExampleCell>)keyValuePair.Value;
			if (!table.Rows.Any<IEnumerable<ExampleCell>>() || !table.Rows.First<IEnumerable<ExampleCell>>().Any<ExampleCell>())
			{
				throw new EmptyTableException();
			}
			List<string> list = table.ColumnNames.ToList<string>();
			foreach (List<List<StringRegionCell>> list2 in ExampleUtils.EnumerateExampleMappings(stringRegion, table))
			{
				if (cancel.IsCancellationRequested)
				{
					return Optional<ProgramSet>.Nothing;
				}
				Optional<ProgramSet> optional = this.LearnTable(stringRegion, list, list2, cancel);
				if (optional.HasValue)
				{
					return optional;
				}
			}
			return Optional<ProgramSet>.Nothing;
		}

		// Token: 0x06006D5D RID: 27997 RVA: 0x001649D4 File Offset: 0x00162BD4
		private Optional<ProgramSet> LearnTable(StringRegion input, List<string> columnNames, List<List<StringRegionCell>> allRows, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.columnNames = columnNames;
			List<List<StringRegionCell>> list = new List<List<StringRegionCell>>(allRows);
			while (list.Count > 0)
			{
				if (list.Last<List<StringRegionCell>>().Any((StringRegionCell cell) => cell.IsUserSpecified))
				{
					break;
				}
				list.RemoveAt(list.Count - 1);
			}
			if (list.Count == 0 || list[0].Count == 0)
			{
				return Optional<ProgramSet>.Nothing;
			}
			StringRegionCell stringRegionCell = list[0][0];
			int num = 0;
			if (stringRegionCell.Start > 0U)
			{
				StringRegion stringRegion = input.Slice(0U, list[0][0].Start);
				num = Semantics.SplitLines(stringRegion).Count - 1;
				Optional<char> optional = stringRegion.MaybeLastChar();
				if (optional.HasValue && (optional.Value == '\r' || optional.Value == '\n'))
				{
					num++;
				}
			}
			LearnResult<colSplit> learnResult = null;
			records records = default(records);
			foreach (Record<records, TableExample> record in GroupLearner.Learn(input, num, list, this._build))
			{
				records records2;
				TableExample tableExample;
				record.Deconstruct(out records2, out tableExample);
				records records3 = records2;
				TableExample tableExample2 = tableExample;
				if (cancel.IsCancellationRequested)
				{
					return this.<LearnTable>g__CreateTableProgram|6_1(records, learnResult, ref CS$<>8__locals1);
				}
				LearnResult<colSplit> learnResult2 = this.LearnColSplit(tableExample2, learnResult, cancel);
				if (learnResult2 != null)
				{
					if (!learnResult2.HasNullOrWhiteSpace)
					{
						return this.<LearnTable>g__CreateTableProgram|6_1(records3, learnResult2, ref CS$<>8__locals1);
					}
					if (learnResult2 > learnResult)
					{
						learnResult = learnResult2;
						records = records3;
					}
				}
			}
			return this.<LearnTable>g__CreateTableProgram|6_1(records, learnResult, ref CS$<>8__locals1);
		}

		// Token: 0x06006D5E RID: 27998 RVA: 0x00164B84 File Offset: 0x00162D84
		private LearnResult<colSplit> LearnColSplit(TableExample tableExample, LearnResult<colSplit> topResult, CancellationToken cancel)
		{
			if (cancel.IsCancellationRequested)
			{
				return null;
			}
			if (tableExample.ColumnCount != 1)
			{
				foreach (Record<ProgramSetBuilder<split>, ExtractExample, TableExample> record in SplitLearner.Learn(tableExample, this._build, cancel))
				{
					ProgramSetBuilder<split> programSetBuilder;
					ExtractExample extractExample;
					TableExample tableExample2;
					record.Deconstruct(out programSetBuilder, out extractExample, out tableExample2);
					ProgramSetBuilder<split> programSetBuilder2 = programSetBuilder;
					ExtractExample extractExample2 = extractExample;
					TableExample tableExample3 = tableExample2;
					if (cancel.IsCancellationRequested)
					{
						break;
					}
					LearnResult<trimExtract> learnResult = ExtractLearner.Learn(extractExample2, this._build, cancel);
					if (learnResult != null)
					{
						ProgramSetBuilder<extractTup> programSetBuilder3 = this._build.Set.Join.LetExtractTup(this._build.Set.ExplicitJoin.First(ProgramSetBuilder.List<tup>(this._build.Symbol.tup, new tup[] { this._build.Node.Variable.tup })), learnResult.ProgramSet);
						LearnResult<colSplit> learnResult2 = this.LearnColSplit(tableExample3, null, cancel);
						if (learnResult2 != null)
						{
							ProgramSetBuilder<_LetB2> programSetBuilder4 = this._build.Set.Join.LetPrepend(this._build.Set.Join.Second(ProgramSetBuilder.List<tup>(this._build.Symbol.tup, new tup[] { this._build.Node.Variable.tup })), this._build.Set.Join.Prepend(programSetBuilder3, learnResult2.ProgramSet));
							LearnResult<colSplit> learnResult3 = new LearnResult<colSplit>(this._build.Set.Join.LetSplit(programSetBuilder2, programSetBuilder4), (learnResult.NullRatio + learnResult2.NullRatio * (double)tableExample3.ColumnCount) / (double)(tableExample3.ColumnCount + 1), (learnResult.NullOrWhitespaceRatio + learnResult2.NullOrWhitespaceRatio * (double)tableExample3.ColumnCount) / (double)(tableExample3.ColumnCount + 1), false);
							if (!learnResult3.HasNullOrWhiteSpace)
							{
								return learnResult3;
							}
							if (learnResult3 > topResult)
							{
								topResult = learnResult3;
							}
						}
					}
				}
				return topResult;
			}
			LearnResult<trimExtract> learnResult4 = ExtractLearner.Learn(new ExtractExample(tableExample.RowExamples.Select((Record<StringRegion, IReadOnlyList<StringRegionCell>> e) => new Record<StringRegion, StringRegionCell>(e.Item1, e.Item2.Single<StringRegionCell>())).ToList<Record<StringRegion, StringRegionCell>>(), tableExample.AdditionalInputs), this._build, cancel);
			if (learnResult4 != null)
			{
				return new LearnResult<colSplit>(this._build.Set.ExplicitJoin.List(learnResult4.ProgramSet), learnResult4.NullRatio, learnResult4.NullOrWhitespaceRatio, false);
			}
			return null;
		}

		// Token: 0x06006D60 RID: 28000 RVA: 0x00164E8C File Offset: 0x0016308C
		[CompilerGenerated]
		private Optional<ProgramSet> <LearnTable>g__CreateTableProgram|6_1(records recordNode, LearnResult<colSplit> colSplit, ref Witnesses.<>c__DisplayClass6_0 A_3)
		{
			if (colSplit == null)
			{
				return Optional<ProgramSet>.Nothing;
			}
			ProgramSetBuilder<columnNames> programSetBuilder = ProgramSetBuilder.List<columnNames>(this._build.Symbol.columnNames, new columnNames[] { this._build.Node.Rule.columnNames(A_3.columnNames) });
			ProgramSet programSet = ProgramSet.Join(this._build.Rule.RowMap.Body[0].LambdaRule, new ProgramSet[] { colSplit.ProgramSet.Set });
			ProgramSet programSet2 = ProgramSet.Join(this._build.Rule.RowMap, new ProgramSet[]
			{
				programSet,
				ProgramSet.List(this._build.Symbol.records, new ProgramNode[] { recordNode.Node })
			});
			return this._build.Set.Join.Table(programSetBuilder, this._build.Set.Cast.table(programSet2)).Set.Some<ProgramSet>();
		}

		// Token: 0x04002F47 RID: 12103
		public static readonly Dictionary<Regex, string> GroupRegexes = new Dictionary<Regex, string>
		{
			{
				new Regex("^\\d", RegexOptions.ExplicitCapture | RegexOptions.Compiled),
				"{\"0\"..\"9\"}"
			},
			{
				new Regex("^[\\p{Lu}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled),
				"{\"A\"..\"Z\"}"
			},
			{
				new Regex("^[\\p{Ll}]", RegexOptions.ExplicitCapture | RegexOptions.Compiled),
				"{\"a\"..\"z\"}"
			}
		};

		// Token: 0x04002F48 RID: 12104
		internal const int BranchLimit = 5;

		// Token: 0x04002F49 RID: 12105
		private readonly GrammarBuilders _build;

		// Token: 0x04002F4A RID: 12106
		private readonly Witnesses.Options _options;

		// Token: 0x02000F5B RID: 3931
		public class Options : DSLOptions
		{
		}
	}
}
