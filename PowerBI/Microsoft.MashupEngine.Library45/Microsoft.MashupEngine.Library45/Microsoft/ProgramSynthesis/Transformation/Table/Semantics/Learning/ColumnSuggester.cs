using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B22 RID: 6946
	public abstract class ColumnSuggester : ISuggester
	{
		// Token: 0x0600E46B RID: 58475 RVA: 0x00307045 File Offset: 0x00305245
		public ProgramSetBuilder<table> Suggest(GrammarBuilders build, Options options, Table<object> inputTable)
		{
			return this.SuggestByColumn(build, options, inputTable).Values.NormalizedUnion<table>();
		}

		// Token: 0x0600E46C RID: 58476 RVA: 0x0030705C File Offset: 0x0030525C
		public IReadOnlyDictionary<string, ProgramSetBuilder<table>> SuggestByColumn(GrammarBuilders build, Options options, Table<object> inputTable)
		{
			IEnumerable<string> uniqueColumnNames = from name in inputTable.ColumnNames
				group name by name into name
				where name.Count<string>() == 1
				select name.Key;
			return inputTable.ColumnNames.Enumerate<string>().Where2(delegate(int columnIdx, string sourceColumn)
			{
				if (uniqueColumnNames.Contains(sourceColumn))
				{
					IEnumerable<string> focusedColumnNames = options.FocusedColumnNames;
					return focusedColumnNames == null || focusedColumnNames.Contains(sourceColumn);
				}
				return false;
			}).Select2((int columnIdx, string sourceColumn) => KVP.Create<string, ProgramSetBuilder<table>>(sourceColumn, this.SuggestForColumnCached(build, options, inputTable, columnIdx, sourceColumn)))
				.Where2((string sourceColumn, ProgramSetBuilder<table> programSet) => !ProgramSet.IsNullOrEmpty((programSet != null) ? programSet.Set : null))
				.ToDictionary<string, ProgramSetBuilder<table>>();
		}

		// Token: 0x0600E46D RID: 58477 RVA: 0x00307164 File Offset: 0x00305364
		public virtual ProgramSetBuilder<table> SuggestForColumnCached(GrammarBuilders build, Options options, Table<object> inputTable, int columnIndex, string sourceColumnName)
		{
			return this._cachedSuggestion.GetOrAdd(this.GetHashCode(options, inputTable, sourceColumnName), (int _) => this.SuggestForColumn(build, options, inputTable, columnIndex, sourceColumnName));
		}

		// Token: 0x0600E46E RID: 58478
		public abstract ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, Table<object> inputTable, int columnIndex, string sourceColumnName);

		// Token: 0x0600E46F RID: 58479 RVA: 0x003071D4 File Offset: 0x003053D4
		public virtual int GetHashCode(Options options, Table<object> inputTable, string sourceColumnName)
		{
			return new string[]
			{
				inputTable.GetHashCode().ToString(),
				inputTable.HashedColumn(sourceColumnName).ToString(),
				sourceColumnName,
				options.GetHashCode().ToString()
			}.OrderDependentHashCode<string>();
		}

		// Token: 0x0600E470 RID: 58480
		public abstract bool CanBeDestructive();

		// Token: 0x0600E471 RID: 58481
		public abstract bool CanSuggest(Operators allowedOperators);

		// Token: 0x04005694 RID: 22164
		public readonly ConcurrentLruCache<int, ProgramSetBuilder<table>> _cachedSuggestion = new ConcurrentLruCache<int, ProgramSetBuilder<table>>(4096, null, null, null);
	}
}
