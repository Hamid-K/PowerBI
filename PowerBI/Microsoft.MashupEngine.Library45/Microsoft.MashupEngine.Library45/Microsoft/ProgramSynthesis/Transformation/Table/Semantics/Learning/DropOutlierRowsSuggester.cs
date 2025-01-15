using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B0A RID: 6922
	internal class DropOutlierRowsSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E41C RID: 58396 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E41D RID: 58397 RVA: 0x00305F93 File Offset: 0x00304193
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropOutlierRows);
		}

		// Token: 0x0600E41E RID: 58398 RVA: 0x00305FAC File Offset: 0x003041AC
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			double num = 20.0;
			double num2 = 0.05;
			double num3 = 0.05;
			double num4 = 0.1;
			if (sourceColumnName.EndsWith("_label_encoded"))
			{
				return null;
			}
			List<object> list = columnData.Where((object d) => !Utilities.IsMissing(d)).ToList<object>();
			if (!Utilities.IsNumeric(list, true))
			{
				return null;
			}
			if (!list.Distinct<object>().Skip(2).Any<object>())
			{
				return null;
			}
			List<double> list2 = list.Select(delegate(object d)
			{
				if (d is double)
				{
					return (double)d;
				}
				return Convert.ToDouble(d);
			}).ToList<double>();
			int count = list2.Count;
			int num5 = (int)Math.Floor((double)count * num2);
			int num6 = (int)Math.Floor((double)count * num3);
			List<double> list3 = list2.OrderBy((double a) => a).Skip(num5).Take(count - num5 - num6)
				.ToList<double>();
			if (!list3.Any<double>())
			{
				return null;
			}
			double num7 = list3.Average();
			double num8 = list3.StandardDeviation(new double?(num7));
			Tuple<double, double> validBound = new Tuple<double, double>(Math.Round(num7 - num * num8 - 0.05, 2), Math.Round(num7 + num * num8 + 0.05, 2));
			int num9 = list2.Count((double d) => d <= validBound.Item1 || d >= validBound.Item2);
			if (num9 == 0 || (double)num9 > (double)count * num4)
			{
				return null;
			}
			return build.Set.Join.DropRows(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.dropCondition(new OutlierCondition(sourceColumnName, validBound)).ToProgramSet<dropCondition>());
		}
	}
}
