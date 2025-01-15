using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Split.Text.Build;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B1C RID: 6940
	internal class SplitColumnSuggester : ColumnSuggester
	{
		// Token: 0x0600E459 RID: 58457 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool CanBeDestructive()
		{
			return false;
		}

		// Token: 0x0600E45A RID: 58458 RVA: 0x00306CDC File Offset: 0x00304EDC
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.SplitText);
		}

		// Token: 0x0600E45B RID: 58459 RVA: 0x00306CF0 File Offset: 0x00304EF0
		public override int GetHashCode(Options options, Table<object> inputTable, string sourceColumnName)
		{
			return inputTable.ColumnNames.PrependItem(inputTable.HashedColumn(sourceColumnName).ToString()).PrependItem(sourceColumnName.ToString()).PrependItem(options.GetHashCode().ToString())
				.OrderDependentHashCode<string>();
		}

		// Token: 0x0600E45C RID: 58460 RVA: 0x00306D3C File Offset: 0x00304F3C
		public override ProgramSetBuilder<table> SuggestForColumn(Microsoft.ProgramSynthesis.Transformation.Table.Build.GrammarBuilders build, Options options, Table<object> inputTable, int columnIdx, string columnName)
		{
			splitCell? orAdd = this._cachedSplitRule.GetOrAdd(inputTable.HashedColumn(columnName), (int _) => this.GetSplitRule(build, inputTable.Column(columnName)));
			if (orAdd != null)
			{
				splitCell valueOrDefault = orAdd.GetValueOrDefault();
				ProgramSetBuilder<newColumns> programSetBuilder = build.Node.Rule.SplitColumn(valueOrDefault, build.Node.Rule.SelectColumnToSplit(build.Node.Variable.inputTable, build.Node.Rule.sourceColumnName(columnName))).ToProgramSet<newColumns>();
				ProgramSetBuilder<table> programSetBuilder2 = build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>();
				ProgramSetBuilder<sourceColumnName> programSetBuilder3 = build.Node.Rule.sourceColumnName(columnName).ToProgramSet<sourceColumnName>();
				return build.Set.Join.AddSplitColumns(programSetBuilder2, programSetBuilder3, programSetBuilder);
			}
			return null;
		}

		// Token: 0x0600E45D RID: 58461 RVA: 0x00306E7C File Offset: 0x0030507C
		private splitCell? GetSplitRule(Microsoft.ProgramSynthesis.Transformation.Table.Build.GrammarBuilders build, IEnumerable<object> columnData)
		{
			Optional<IEnumerable<string>> optional = columnData.Select((object cell) => (cell == null || cell is string).Then(() => (string)cell)).WholeSequenceOfValues<string>();
			if (!optional.HasValue)
			{
				return null;
			}
			ProgramLearner<SplitProgram, StringRegion, SplitCell[]> instance = SplitProgramLearner.Instance;
			IEnumerable<Constraint<StringRegion, SplitCell[]>> enumerable = new SuggestionsMode[]
			{
				new SuggestionsMode(true)
			};
			int num = 1;
			int? num2 = null;
			ProgramSamplingStrategy programSamplingStrategy = ProgramSamplingStrategy.UniformAcrossUnions;
			IEnumerable<string> enumerable2 = optional.Value.Select(delegate(string s)
			{
				if (s != null)
				{
					return s;
				}
				return "";
			});
			Func<string, StringRegion> func;
			if ((func = SplitColumnSuggester.<>O.<0>__CreateStringRegion) == null)
			{
				func = (SplitColumnSuggester.<>O.<0>__CreateStringRegion = new Func<string, StringRegion>(SplitSession.CreateStringRegion));
			}
			SplitProgram splitProgram = instance.LearnTopK(enumerable, num, num2, programSamplingStrategy, enumerable2.Select(func), default(CancellationToken)).TopPrograms.FirstOrDefault<SplitProgram>();
			if (!(splitProgram == null))
			{
				return new splitCell?(build.Node.Rule.Split(regionSplit.CreateSafe(SplitColumnSuggester.SplitBuilder, splitProgram.ProgramNode).Value));
			}
			return null;
		}

		// Token: 0x04005689 RID: 22153
		private readonly ConcurrentLruCache<int, splitCell?> _cachedSplitRule = new ConcurrentLruCache<int, splitCell?>(4096, null, null, null);

		// Token: 0x0400568A RID: 22154
		private static readonly Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders SplitBuilder = Microsoft.ProgramSynthesis.Split.Text.Build.GrammarBuilders.Instance(Language.Grammar);

		// Token: 0x02001B1D RID: 6941
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400568B RID: 22155
			public static Func<string, StringRegion> <0>__CreateStringRegion;
		}
	}
}
