using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B16 RID: 6934
	internal class MultiLabelBinarizer : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E43F RID: 58431 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E440 RID: 58432 RVA: 0x0030683F File Offset: 0x00304A3F
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.MultiLabelBinarizer);
		}

		// Token: 0x0600E441 RID: 58433 RVA: 0x00306858 File Offset: 0x00304A58
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			if (columnData.Any((object d) => d != null && !(d is string)))
			{
				return null;
			}
			string[] array = new string[] { ",", "|", ";" };
			int num = (int)Math.Floor(0.5 * (double)columnData.Count<object>());
			string confirmedDelim = null;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string delim = array2[i];
				if (columnData.Count(delegate(object d)
				{
					string text = d as string;
					return text != null && text.Contains(delim);
				}) > num)
				{
					confirmedDelim = delim;
					break;
				}
			}
			if (confirmedDelim == null)
			{
				return null;
			}
			List<IEnumerable<string>> list = (from d in columnData
				where d != null
				select from a in d.ToString().Split(new string[] { confirmedDelim }, StringSplitOptions.RemoveEmptyEntries)
					select a.Trim() into a
					where a.Length > 0
					select a).ToList<IEnumerable<string>>();
			if (list.Select((IEnumerable<string> ls) => ls.Count<string>()).Distinct<int>().Count((int c) => c > 1) == 1)
			{
				return null;
			}
			if (!this.ShouldBeEncoded(list.SelectMany((IEnumerable<string> ls) => ls)))
			{
				return null;
			}
			return build.Set.Join.MultiLabelBinarizer(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.delimiter(confirmedDelim.ToString()).ToProgramSet<delimiter>());
		}

		// Token: 0x0600E442 RID: 58434 RVA: 0x00306A50 File Offset: 0x00304C50
		private bool ShouldBeEncoded(IEnumerable<string> maybeLabels)
		{
			if (maybeLabels.Distinct<string>().Count<string>() == 1)
			{
				return false;
			}
			IEnumerable<IRichDataType> enumerable = new RichDataTypeDetector(new IRichDataType[]
			{
				new RichDateType(),
				new RichNumericType(),
				new RichCategoricalType(50, 5, 0.5)
			}, 1, 0.001).DetectAll(maybeLabels.Cast<string>(), null, null, null);
			return !enumerable.OfType<RichNumericType>().Any<RichNumericType>() && !enumerable.OfType<RichDateType>().Any<RichDateType>() && enumerable.OfType<RichCategoricalType>().Any<RichCategoricalType>();
		}

		// Token: 0x04005677 RID: 22135
		private const int NumberOfDifferentCategoriesThreshold = 50;

		// Token: 0x04005678 RID: 22136
		private const int MinimumSamplesRequired = 5;

		// Token: 0x04005679 RID: 22137
		private const double FractionOfDifferentCategoriesThreshold = 0.5;
	}
}
