using System;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B03 RID: 6915
	internal class DropDuplicateColumnSuggester : ColumnSuggester
	{
		// Token: 0x0600E409 RID: 58377 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E40A RID: 58378 RVA: 0x00305BAF File Offset: 0x00303DAF
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropDuplicateColumn);
		}

		// Token: 0x0600E40B RID: 58379 RVA: 0x00305BC4 File Offset: 0x00303DC4
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, Table<object> inputTable, int columnIdx, string columnName)
		{
			Optional<int> optional = Enumerable.Range(0, columnIdx).MaybeFirst((int otherColumnIndex) => inputTable.HashedNormalizedColumn(columnName) == inputTable.HashedNormalizedColumn(inputTable.ColumnNames.ElementAt(otherColumnIndex)));
			if (optional.HasValue)
			{
				return build.Set.Join.DropColumn(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(columnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.dropCondition(new DuplicateCondition(inputTable.ColumnNames.ElementAt(optional.Value))).ToProgramSet<dropCondition>());
			}
			return null;
		}
	}
}
