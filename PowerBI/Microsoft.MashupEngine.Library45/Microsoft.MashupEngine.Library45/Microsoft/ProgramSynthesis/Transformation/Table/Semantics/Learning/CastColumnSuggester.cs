using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001AF8 RID: 6904
	internal class CastColumnSuggester : TableAgnosticColumnSuggester
	{
		// Token: 0x0600E3AA RID: 58282 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E3AB RID: 58283 RVA: 0x00305115 File Offset: 0x00303315
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.CastColumn);
		}

		// Token: 0x0600E3AC RID: 58284 RVA: 0x0030512C File Offset: 0x0030332C
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, IEnumerable<object> columnData, string sourceColumnName)
		{
			bool flag = false;
			if (columnData.All((object d) => !(d is string)))
			{
				return null;
			}
			if (columnData.Any((object d) => d != null && !(d is string)))
			{
				columnData = columnData.ToCustomString();
				flag = true;
			}
			IRichDataType richDataType = new RichDataTypeDetector(new IRichDataType[]
			{
				new RichBooleanType(),
				new RichDateType(),
				new RichNumericType()
			}, 1, 0.001).Detect(columnData.Cast<string>(), null, null, null, null);
			if (richDataType == null)
			{
				return null;
			}
			return build.Set.Join.CastColumn(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(sourceColumnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.richDataType(richDataType).ToProgramSet<richDataType>(), build.Node.Rule.isMixedColumn(flag).ToProgramSet<isMixedColumn>());
		}
	}
}
