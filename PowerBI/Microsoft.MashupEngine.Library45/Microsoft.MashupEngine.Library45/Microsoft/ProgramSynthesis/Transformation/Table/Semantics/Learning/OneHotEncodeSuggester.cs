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
	// Token: 0x02001B1A RID: 6938
	internal class OneHotEncodeSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E451 RID: 58449 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E452 RID: 58450 RVA: 0x00306B91 File Offset: 0x00304D91
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.OneHotEncoding);
		}

		// Token: 0x0600E453 RID: 58451 RVA: 0x00306BA4 File Offset: 0x00304DA4
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			if (!this.ShouldBeOneHotEncoded(columnData))
			{
				return null;
			}
			return build.Set.Join.OneHotEncode(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>());
		}

		// Token: 0x0600E454 RID: 58452 RVA: 0x00306C08 File Offset: 0x00304E08
		private bool ShouldBeOneHotEncoded(IEnumerable<object> columnData)
		{
			if (columnData.Any((object d) => d != null && !(d is string)))
			{
				return false;
			}
			if (columnData.Distinct<object>().Count<object>() == 1)
			{
				return false;
			}
			IEnumerable<IRichDataType> enumerable = new RichDataTypeDetector(new IRichDataType[]
			{
				new RichDateType(),
				new RichNumericType(),
				new RichCategoricalType(10, 5, 0.5)
			}, 1, 0.001).DetectAll(columnData.Cast<string>(), null, null, null);
			return !enumerable.OfType<RichNumericType>().Any<RichNumericType>() && !enumerable.OfType<RichDateType>().Any<RichDateType>() && enumerable.OfType<RichCategoricalType>().Any<RichCategoricalType>();
		}

		// Token: 0x04005684 RID: 22148
		private const int NumberOfDifferentCategoriesThreshold = 10;

		// Token: 0x04005685 RID: 22149
		private const int MinimumSamplesRequired = 5;

		// Token: 0x04005686 RID: 22150
		private const double FractionOfDifferentCategoriesThreshold = 0.5;
	}
}
