using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM
{
	// Token: 0x02000B92 RID: 2962
	internal class PowerQueryMJsonCodeBuilder : PowerQueryMCodeBuilder
	{
		// Token: 0x06004B4F RID: 19279 RVA: 0x000ED23C File Offset: 0x000EB43C
		public PowerQueryMJsonCodeBuilder(string binaryContent, bool isJsonLines, ILocalizedPowerQueryMJsonStrings localizedStrings, IEscapePowerQueryM escape, HashSet<string> forbiddenStepNames, string sourceStepName)
		{
			string text = (isJsonLines ? ("Table.FromColumns({Lines.FromBinary(" + binaryContent + ", null, null)})") : ("Json.Document(" + binaryContent + ")"));
			if (localizedStrings != null)
			{
				Dictionary<MTableFunctionName, string> dictionary = new Dictionary<MTableFunctionName, string>();
				MTableFunctionName fromList = MTableFunctionName.FromList;
				dictionary[fromList] = localizedStrings.ConvertedToTable;
				MTableFunctionName fromRecords = MTableFunctionName.FromRecords;
				dictionary[fromRecords] = localizedStrings.ConvertedToTable;
				MTableFunctionName splitColumn = MTableFunctionName.SplitColumn;
				dictionary[splitColumn] = localizedStrings.SplitColumn;
				MTableFunctionName transformColumns = MTableFunctionName.TransformColumns;
				dictionary[transformColumns] = localizedStrings.TransformColumns;
				base..ctor(text, localizedStrings, escape, dictionary, forbiddenStepNames, sourceStepName);
				this.LocalizedStrings = localizedStrings;
				if (isJsonLines)
				{
					base.AddStep(MTableFunctionName.TransformColumns, new string[] { "{\"Column1\", Json.Document}" }, null);
				}
				return;
			}
			throw new ArgumentNullException("localizedStrings");
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x000ED300 File Offset: 0x000EB500
		internal ILocalizedPowerQueryMJsonStrings LocalizedStrings { get; }

		// Token: 0x06004B51 RID: 19281 RVA: 0x000ED308 File Offset: 0x000EB508
		internal string GetExpandedStep(IReadOnlyList<string> path)
		{
			return this.LocalizedStrings.Expanded(string.Join(".", path));
		}
	}
}
