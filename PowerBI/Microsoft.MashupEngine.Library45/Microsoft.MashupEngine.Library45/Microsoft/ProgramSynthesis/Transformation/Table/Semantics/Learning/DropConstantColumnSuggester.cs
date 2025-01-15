using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B01 RID: 6913
	internal class DropConstantColumnSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E401 RID: 58369 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E402 RID: 58370 RVA: 0x00305A8F File Offset: 0x00303C8F
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropConstantColumn);
		}

		// Token: 0x0600E403 RID: 58371 RVA: 0x00305AA4 File Offset: 0x00303CA4
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			if (!columnData.Skip(1).Any<object>())
			{
				return null;
			}
			IGrouping<object, object> grouping = (from v in columnData
				group v by v).ArgMax((IGrouping<object, object> g) => g.Count<object>());
			if (grouping != null && (double)grouping.Count<object>() / (double)columnData.Count<object>() >= 1.0)
			{
				table table = build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable);
				return build.Set.Join.DropColumn(table.ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.dropCondition(new ConstantCondition(grouping.First<object>())).ToProgramSet<dropCondition>());
			}
			return null;
		}

		// Token: 0x0400564D RID: 22093
		private const double CONSTANT_THRESHOLD = 1.0;
	}
}
