using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation
{
	// Token: 0x02000EF0 RID: 3824
	internal class PowerQueryMCodeBuilder : PowerQueryMCodeBuilder
	{
		// Token: 0x0600681F RID: 26655 RVA: 0x001532E0 File Offset: 0x001514E0
		public PowerQueryMCodeBuilder(IEnumerable<KeyValuePair<string, string>> initialSteps, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals)
			: base(initialSteps, escapeLiterals, PowerQueryMCodeBuilder.BuildStepNamesDictionary(localizedStrings), null)
		{
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x001532F1 File Offset: 0x001514F1
		public PowerQueryMCodeBuilder(string initialTableCode, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals)
			: base(initialTableCode, localizedStrings, escapeLiterals, PowerQueryMCodeBuilder.BuildStepNamesDictionary(localizedStrings), null, null)
		{
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x00153304 File Offset: 0x00151504
		private static Dictionary<MTableFunctionName, string> BuildStepNamesDictionary(ILocalizedPowerQueryMStrings localizedStrings)
		{
			Dictionary<MTableFunctionName, string> dictionary = new Dictionary<MTableFunctionName, string>();
			MTableFunctionName addColumn = MTableFunctionName.AddColumn;
			dictionary[addColumn] = localizedStrings.AddedColumn;
			MTableFunctionName addIndexColumn = MTableFunctionName.AddIndexColumn;
			dictionary[addIndexColumn] = localizedStrings.AddedIndex;
			MTableFunctionName combineColumns = MTableFunctionName.CombineColumns;
			dictionary[combineColumns] = localizedStrings.MergedColumns;
			MTableFunctionName fillDown = MTableFunctionName.FillDown;
			dictionary[fillDown] = localizedStrings.FilledDown;
			MTableFunctionName group = MTableFunctionName.Group;
			dictionary[group] = localizedStrings.GroupedRows;
			MTableFunctionName promoteHeaders = MTableFunctionName.PromoteHeaders;
			dictionary[promoteHeaders] = localizedStrings.PromotedHeaders;
			MTableFunctionName removeColumns = MTableFunctionName.RemoveColumns;
			dictionary[removeColumns] = localizedStrings.RemovedColumns;
			MTableFunctionName removeLastN = MTableFunctionName.RemoveLastN;
			dictionary[removeLastN] = localizedStrings.RemovedBottomRows;
			MTableFunctionName selectColumns = MTableFunctionName.SelectColumns;
			dictionary[selectColumns] = localizedStrings.RemovedOtherColumns;
			MTableFunctionName skip = MTableFunctionName.Skip;
			dictionary[skip] = localizedStrings.RemovedTopRows;
			MTableFunctionName transpose = MTableFunctionName.Transpose;
			dictionary[transpose] = localizedStrings.TransposedTable;
			return dictionary;
		}
	}
}
