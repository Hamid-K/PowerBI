using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B05 RID: 6917
	internal class DropEmptyColumnSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E40F RID: 58383 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E410 RID: 58384 RVA: 0x00305CC0 File Offset: 0x00303EC0
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropEmptyColumn);
		}

		// Token: 0x0600E411 RID: 58385 RVA: 0x00305CD4 File Offset: 0x00303ED4
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			Func<object, bool> func;
			if ((func = DropEmptyColumnSuggester.<>O.<0>__IsMissing) == null)
			{
				func = (DropEmptyColumnSuggester.<>O.<0>__IsMissing = new Func<object, bool>(Utilities.IsMissing));
			}
			if (columnData.ProportionTrue(func) >= 0.9)
			{
				table table = build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable);
				return build.Set.Join.DropColumn(table.ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.dropCondition(new MissingCondition(0.9, MissingValueType.All)).ToProgramSet<dropCondition>());
			}
			return null;
		}

		// Token: 0x04005653 RID: 22099
		private const double EMPTY_THRESHOLD = 0.9;

		// Token: 0x02001B06 RID: 6918
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005654 RID: 22100
			public static Func<object, bool> <0>__IsMissing;
		}
	}
}
