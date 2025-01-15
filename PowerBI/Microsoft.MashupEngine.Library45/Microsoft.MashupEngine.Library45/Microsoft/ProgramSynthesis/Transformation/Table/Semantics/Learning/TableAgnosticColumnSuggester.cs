using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B26 RID: 6950
	public abstract class TableAgnosticColumnSuggester : ColumnSuggester
	{
		// Token: 0x0600E47E RID: 58494 RVA: 0x003072DC File Offset: 0x003054DC
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, Table<object> inputTable, int columnIndex, string sourceColumnName)
		{
			return this.SuggestForColumn(build, options, inputTable.Column(sourceColumnName), sourceColumnName);
		}

		// Token: 0x0600E47F RID: 58495
		public abstract ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName);

		// Token: 0x0600E480 RID: 58496 RVA: 0x003072F0 File Offset: 0x003054F0
		public override int GetHashCode(Options options, Table<object> inputTable, string sourceColumnName)
		{
			return new string[]
			{
				inputTable.HashedColumn(sourceColumnName).ToString(),
				sourceColumnName,
				options.GetHashCode().ToString()
			}.OrderDependentHashCode<string>();
		}
	}
}
