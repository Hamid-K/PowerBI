using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B10 RID: 6928
	internal class FillMissingValuesSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E42F RID: 58415 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E430 RID: 58416 RVA: 0x00306514 File Offset: 0x00304714
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.FillMissingValues);
		}

		// Token: 0x0600E431 RID: 58417 RVA: 0x0030652C File Offset: 0x0030472C
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			HashSet<object> missingvalues = new HashSet<object>();
			IEnumerable<object> enumerable = columnData.Where(delegate(object d)
			{
				if (Utilities.IsMissing(d))
				{
					missingvalues.Add(d);
					return false;
				}
				return true;
			});
			double num = 1.0 - (double)enumerable.Count<object>() / (double)columnData.Count<object>();
			if (num < 0.01)
			{
				return null;
			}
			if (num > 0.4)
			{
				return null;
			}
			FillMethod fillMethod;
			object obj;
			if (Utilities.IsInteger(enumerable, false))
			{
				fillMethod = FillMethod.RoundedMean;
				IEnumerable<object> enumerable2 = enumerable;
				Func<object, long> func;
				if ((func = FillMissingValuesSuggester.<>O.<0>__ToInt64) == null)
				{
					func = (FillMissingValuesSuggester.<>O.<0>__ToInt64 = new Func<object, long>(Convert.ToInt64));
				}
				obj = (int)Math.Round(enumerable2.Select(func).Average());
			}
			else if (Utilities.IsDouble(enumerable, false))
			{
				fillMethod = FillMethod.Mean;
				IEnumerable<object> enumerable3 = enumerable.Where((object d) => Utilities.IsDouble(d) && !Utilities.IsNa(d));
				Func<object, double> func2;
				if ((func2 = FillMissingValuesSuggester.<>O.<1>__ToDouble) == null)
				{
					func2 = (FillMissingValuesSuggester.<>O.<1>__ToDouble = new Func<object, double>(Convert.ToDouble));
				}
				obj = enumerable3.Select(func2).Average();
			}
			else
			{
				fillMethod = FillMethod.Mode;
				obj = enumerable.Mode(null);
			}
			return build.Set.Join.FillMissingValues(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.fillValue(obj).ToProgramSet<fillValue>(), build.Node.Rule.missingValueMarkers(missingvalues).ToProgramSet<missingValueMarkers>(), build.Node.Rule.fillMethod(fillMethod).ToProgramSet<fillMethod>());
		}

		// Token: 0x0400566B RID: 22123
		private const double MIN_FRACTION_OF_MISSING_VALUES = 0.01;

		// Token: 0x0400566C RID: 22124
		private const double MAX_FRACTION_OF_MISSING_VALUES = 0.4;

		// Token: 0x02001B11 RID: 6929
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400566D RID: 22125
			public static Func<object, long> <0>__ToInt64;

			// Token: 0x0400566E RID: 22126
			public static Func<object, double> <1>__ToDouble;
		}
	}
}
