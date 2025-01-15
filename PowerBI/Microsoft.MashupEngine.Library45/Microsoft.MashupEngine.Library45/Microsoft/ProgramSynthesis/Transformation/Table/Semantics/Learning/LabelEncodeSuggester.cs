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
	// Token: 0x02001B14 RID: 6932
	internal class LabelEncodeSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E438 RID: 58424 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool CanBeDestructive()
		{
			return false;
		}

		// Token: 0x0600E439 RID: 58425 RVA: 0x0030670D File Offset: 0x0030490D
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.LabelEncoding);
		}

		// Token: 0x0600E43A RID: 58426 RVA: 0x00306720 File Offset: 0x00304920
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			if (columnData.Any((object d) => d != null && !(d is string)))
			{
				return null;
			}
			if (columnData.Distinct<object>().Count<object>() == 1)
			{
				return null;
			}
			IEnumerable<IRichDataType> enumerable = new RichDataTypeDetector(new IRichDataType[]
			{
				new RichDateType(),
				new RichNumericType(),
				new RichCategoricalType(50, 5, 0.5)
			}, 1, 0.001).DetectAll(columnData.Cast<string>(), null, null, null);
			if (enumerable.OfType<RichNumericType>().Any<RichNumericType>() || enumerable.OfType<RichDateType>().Any<RichDateType>() || !enumerable.OfType<RichCategoricalType>().Any<RichCategoricalType>())
			{
				return null;
			}
			return build.Set.Join.LabelEncode(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>());
		}

		// Token: 0x04005672 RID: 22130
		private const int NumberOfDifferentCategoriesThreshold = 50;

		// Token: 0x04005673 RID: 22131
		private const int MinimumSamplesRequired = 5;

		// Token: 0x04005674 RID: 22132
		private const double FractionOfDifferentCategoriesThreshold = 0.5;
	}
}
