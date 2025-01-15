using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Translation
{
	// Token: 0x02000FA0 RID: 4000
	internal class PowerQueryMCodeBuilder : PowerQueryMCodeBuilder
	{
		// Token: 0x06006E9E RID: 28318 RVA: 0x0016992C File Offset: 0x00167B2C
		public PowerQueryMCodeBuilder(string binaryContent, string encoding, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			string text = string.Concat(new string[] { "Table.FromColumns({Lines.FromBinary(", binaryContent, ", null, null, ", encoding, ")})" });
			Dictionary<MTableFunctionName, string> dictionary = new Dictionary<MTableFunctionName, string>();
			MTableFunctionName addColumn = MTableFunctionName.AddColumn;
			dictionary[addColumn] = localizedStrings.AddedColumn;
			MTableFunctionName addIndexColumn = MTableFunctionName.AddIndexColumn;
			dictionary[addIndexColumn] = localizedStrings.AddedIndex;
			MTableFunctionName fillDown = MTableFunctionName.FillDown;
			dictionary[fillDown] = localizedStrings.FilledDown;
			MTableFunctionName group = MTableFunctionName.Group;
			dictionary[group] = localizedStrings.GroupedRows;
			MTableFunctionName removeColumns = MTableFunctionName.RemoveColumns;
			dictionary[removeColumns] = localizedStrings.RemovedColumns;
			MTableFunctionName renameColumns = MTableFunctionName.RenameColumns;
			dictionary[renameColumns] = localizedStrings.RenameColumns;
			MTableFunctionName selectRows = MTableFunctionName.SelectRows;
			dictionary[selectRows] = localizedStrings.SelectedRows;
			MTableFunctionName skip = MTableFunctionName.Skip;
			dictionary[skip] = localizedStrings.RemovedTopRows;
			base..ctor(text, localizedStrings, escape, dictionary, null, null);
			Dictionary<MSplitterFunctionName, string> dictionary2 = new Dictionary<MSplitterFunctionName, string>();
			MSplitterFunctionName splitTextByEachDelimiter = MSplitterFunctionName.SplitTextByEachDelimiter;
			dictionary2[splitTextByEachDelimiter] = localizedStrings.SplitColumnByDelimiters;
			MSplitterFunctionName splitTextByPositions = MSplitterFunctionName.SplitTextByPositions;
			dictionary2[splitTextByPositions] = localizedStrings.SplitColumnByPositions;
			this._splitColumnNames = dictionary2;
			Dictionary<MTextFunctionName, string> dictionary3 = new Dictionary<MTextFunctionName, string>();
			MTextFunctionName afterDelimiter = MTextFunctionName.AfterDelimiter;
			dictionary3[afterDelimiter] = localizedStrings.ExtractedTextAfterDelimiter;
			MTextFunctionName beforeDelimiter = MTextFunctionName.BeforeDelimiter;
			dictionary3[beforeDelimiter] = localizedStrings.ExtractedTextBeforeDelimiter;
			MTextFunctionName betweenDelimiters = MTextFunctionName.BetweenDelimiters;
			dictionary3[betweenDelimiters] = localizedStrings.ExtractedTextBetweenDelimiter;
			MTextFunctionName end = MTextFunctionName.End;
			dictionary3[end] = localizedStrings.ExtractedLastCharacters;
			MTextFunctionName range = MTextFunctionName.Range;
			dictionary3[range] = localizedStrings.ExtractedTextRange;
			MTextFunctionName start = MTextFunctionName.Start;
			dictionary3[start] = localizedStrings.ExtractedFirstCharacters;
			MTextFunctionName trim = MTextFunctionName.Trim;
			dictionary3[trim] = localizedStrings.TrimmedText;
			this._transformColumnNames = dictionary3;
		}

		// Token: 0x06006E9F RID: 28319 RVA: 0x00169AE8 File Offset: 0x00167CE8
		public void AddTransformStep(MTextFunctionName funcName, IReadOnlyList<string> arguments, int columnIndex)
		{
			string text;
			if (!this._transformColumnNames.TryGetValue(funcName, out text))
			{
				throw new ArgumentException("No name found for Text." + funcName.Name);
			}
			base.AddStep(MTableFunctionName.TransformColumns, new string[] { string.Concat(new string[]
			{
				"{{",
				base.EscapeString("Column" + columnIndex.ToString()),
				", each ",
				funcName.Invoke(arguments),
				", type text}}"
			}) }, text);
		}

		// Token: 0x06006EA0 RID: 28320 RVA: 0x00169B7C File Offset: 0x00167D7C
		public void AddSplitStep(MSplitterFunctionName funcName, IReadOnlyList<string> arguments, int columnIndex)
		{
			string text;
			if (!this._splitColumnNames.TryGetValue(funcName, out text))
			{
				throw new ArgumentException("No name found for Splitter." + funcName.Name);
			}
			base.AddStep(MTableFunctionName.SplitColumn, new string[]
			{
				base.EscapeString("Column" + columnIndex.ToString()),
				string.Concat(new string[]
				{
					"Splitter.",
					funcName.Name,
					"(",
					string.Join(", ", arguments),
					")"
				}),
				string.Concat(new string[]
				{
					"{",
					base.EscapeString("Column" + columnIndex.ToString()),
					", ",
					base.EscapeString("Column" + (columnIndex + 1).ToString()),
					"}"
				})
			}, text);
		}

		// Token: 0x0400302E RID: 12334
		internal const string ColumnPrefix = "Column";

		// Token: 0x0400302F RID: 12335
		private readonly Dictionary<MSplitterFunctionName, string> _splitColumnNames;

		// Token: 0x04003030 RID: 12336
		private readonly Dictionary<MTextFunctionName, string> _transformColumnNames;
	}
}
