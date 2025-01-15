using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B27 RID: 6951
	public class SuggestionLearner
	{
		// Token: 0x0600E482 RID: 58498 RVA: 0x00307330 File Offset: 0x00305530
		public SuggestionLearner(GrammarBuilders build)
		{
			this._programShouldBePrunedCache = new ConcurrentLruCache<Tuple<ProgramNode, int>, bool>(4096, null, null, null);
			this._dqComputer = new DataQualityComputer();
			this._build = build;
		}

		// Token: 0x0600E483 RID: 58499 RVA: 0x003073E4 File Offset: 0x003055E4
		public ProgramSetBuilder<@out> GenerateSuggestions(ITable<object> inputTable, Options options)
		{
			if (!inputTable.Rows.Any<IEnumerable<object>>())
			{
				return null;
			}
			this._samplesForLearning = (from i in Enumerable.Range(0, options.NumSamplesForLearning)
				select Sampler.SampleFromITable(inputTable, options.NumRowsToConsiderForLearning, options.UseAllDataForLearning ? null : options.NumRowsToSampleForLearning, true, !options.UseAllDataForLearning, i) into sample
				where sample != null
				select sample).ToList<Table<object>>();
			if (!this._samplesForLearning.Any<Table<object>>())
			{
				return null;
			}
			List<ISuggester> matchingSuggesters = this._suggesters.Where((ISuggester suggester) => (options.DestructiveSuggestion || !suggester.CanBeDestructive()) && suggester.CanSuggest(options.AllowedOperators)).ToList<ISuggester>();
			ProgramSetBuilder<table> programSetBuilder = matchingSuggesters.Collect((ISuggester suggester) => suggester.Suggest(this._build, options, this._samplesForLearning.First<Table<object>>())).NormalizedUnion<table>();
			if (ProgramSet.IsNullOrEmpty((programSetBuilder != null) ? programSetBuilder.Set : null))
			{
				return null;
			}
			IEnumerable<ProgramNode> realizedPrograms = programSetBuilder.Set.RealizedPrograms;
			List<List<ProgramNode>> validatorProgramsList = this._samplesForLearning.Skip(1).Select(delegate(Table<object> sample)
			{
				ProgramSetBuilder<table> programSetBuilder2 = matchingSuggesters.Collect((ISuggester suggester) => suggester.Suggest(this._build, options, sample)).NormalizedUnion<table>();
				if (!ProgramSet.IsNullOrEmpty((programSetBuilder2 != null) ? programSetBuilder2.Set : null))
				{
					return programSetBuilder2.Set.RealizedPrograms.ToList<ProgramNode>();
				}
				return new List<ProgramNode>();
			}).ToList<List<ProgramNode>>();
			IEnumerable<ProgramNode> enumerable = realizedPrograms.Where(delegate(ProgramNode p)
			{
				Func<ProgramNode, bool> <>9__9;
				return validatorProgramsList.All(delegate(List<ProgramNode> vps)
				{
					Func<ProgramNode, bool> func;
					if ((func = <>9__9) == null)
					{
						func = (<>9__9 = (ProgramNode vp) => vp.Equals(p));
					}
					return vps.Any(func);
				});
			});
			if (!enumerable.Any<ProgramNode>())
			{
				return null;
			}
			return this._build.Set.Join.TTableProgram(ProgramSetBuilder.List<table>(enumerable.Select((ProgramNode node) => this._build.Node.Cast.table(node)).ToArray<table>()));
		}

		// Token: 0x0600E484 RID: 58500 RVA: 0x00307554 File Offset: 0x00305754
		public IEnumerable<Tuple<ProgramNode, double>> GetRankedProgramsWithScores(IEnumerable<ProgramNode> programNodes, ITable<object> inputTable, Options options, CancellationToken cancel)
		{
			SuggestionLearner.<>c__DisplayClass7_0 CS$<>8__locals1 = new SuggestionLearner.<>c__DisplayClass7_0();
			CS$<>8__locals1.inputTable = inputTable;
			CS$<>8__locals1.options = options;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.pruned = new List<ProgramNode>();
			SuggestionLearner.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			List<Table<object>> list;
			if (!CS$<>8__locals1.options.UseLearningDataForRanking)
			{
				list = (from i in Enumerable.Range(0, CS$<>8__locals1.options.NumSamplesForRanking)
					select Sampler.SampleFromITable(CS$<>8__locals1.inputTable, CS$<>8__locals1.options.NumRowsToConsiderForRanking, CS$<>8__locals1.options.UseAllDataForRanking ? null : CS$<>8__locals1.options.NumRowsToSampleForRanking, true, !CS$<>8__locals1.options.UseAllDataForRanking, CS$<>8__locals1.options.NumSamplesForLearning + i) into sample
					where sample != null
					select sample).ToList<Table<object>>();
			}
			else
			{
				list = this._samplesForLearning;
			}
			CS$<>8__locals2.samplesForRanking = list;
			return from p in programNodes
				select new Tuple<ProgramNode, double>(p, CS$<>8__locals1.samplesForRanking.Average(delegate(Table<object> inputTable)
				{
					if (CS$<>8__locals1.pruned.Contains(p))
					{
						return -1.0;
					}
					Table<object> table = SuggestionLearner.Run(p, inputTable);
					if (CS$<>8__locals1.<>4__this.ShouldBePruned(p.Children.ElementAt(0), inputTable, table))
					{
						CS$<>8__locals1.pruned.Add(p);
						return -1.0;
					}
					return CS$<>8__locals1.<>4__this._dqComputer.GetDataQuality(table).Score;
				})) into ps
				where !CS$<>8__locals1.pruned.Contains(ps.Item1)
				orderby ps.Item2 descending
				select ps;
		}

		// Token: 0x0600E485 RID: 58501 RVA: 0x00307634 File Offset: 0x00305834
		private static Table<object> Run(ProgramNode node, ITable<object> input)
		{
			return node.Invoke(State.CreateForExecution(node.Grammar.InputSymbol, input)) as Table<object>;
		}

		// Token: 0x0600E486 RID: 58502 RVA: 0x00307654 File Offset: 0x00305854
		private bool ShouldBePruned(ProgramNode p, Table<object> inputTable, Table<object> outputTable)
		{
			Func<string, bool> <>9__1;
			Func<string, bool> <>9__2;
			return this._programShouldBePrunedCache.GetOrAdd(new Tuple<ProgramNode, int>(p, inputTable.GetHashCode()), delegate(Tuple<ProgramNode, int> _)
			{
				IEnumerable<string> columnNames = outputTable.ColumnNames;
				Func<string, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (string name) => !inputTable.ColumnNames.Contains(name));
				}
				IEnumerable<string> enumerable = columnNames.Where(func);
				if (!enumerable.Any<string>())
				{
					return false;
				}
				IEnumerable<string> enumerable2 = enumerable;
				Func<string, bool> func2;
				if ((func2 = <>9__2) == null)
				{
					func2 = (<>9__2 = (string newCol) => inputTable.ColumnNames.Any((string oldCol) => inputTable.HashedNormalizedColumn(oldCol) == outputTable.HashedNormalizedColumn(newCol)));
				}
				return enumerable2.All(func2);
			});
		}

		// Token: 0x040056A5 RID: 22181
		private readonly GrammarBuilders _build;

		// Token: 0x040056A6 RID: 22182
		private readonly DataQualityComputer _dqComputer;

		// Token: 0x040056A7 RID: 22183
		private readonly ConcurrentLruCache<Tuple<ProgramNode, int>, bool> _programShouldBePrunedCache;

		// Token: 0x040056A8 RID: 22184
		private List<Table<object>> _samplesForLearning;

		// Token: 0x040056A9 RID: 22185
		private readonly ISuggester[] _suggesters = new ISuggester[]
		{
			new CastColumnSuggester(),
			new DropConstantColumnSuggester(),
			new DropDuplicateColumnSuggester(),
			new DropEmptyColumnSuggester(),
			new DropIndexColumnSuggester(),
			new FillMissingValuesSuggester(),
			new LabelEncodeSuggester(),
			new OneHotEncodeSuggester(),
			new DropRowsSuggester(),
			new DropOutlierRowsSuggester(),
			new SplitColumnSuggester(),
			new MultiLabelBinarizer(),
			new AddColumnsFromJson()
		};
	}
}
