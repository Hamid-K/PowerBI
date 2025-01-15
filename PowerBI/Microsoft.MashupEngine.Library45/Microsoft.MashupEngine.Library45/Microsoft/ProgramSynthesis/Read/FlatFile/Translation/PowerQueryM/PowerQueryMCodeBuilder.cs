using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM
{
	// Token: 0x020012D9 RID: 4825
	internal class PowerQueryMCodeBuilder : PowerQueryMCodeBuilder
	{
		// Token: 0x06009194 RID: 37268 RVA: 0x001EAC84 File Offset: 0x001E8E84
		public PowerQueryMCodeBuilder(string sourceStep, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
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
			MTableFunctionName removeLastN = MTableFunctionName.RemoveLastN;
			dictionary[removeLastN] = localizedStrings.RemovedBottomRows;
			MTableFunctionName renameColumns = MTableFunctionName.RenameColumns;
			dictionary[renameColumns] = localizedStrings.RenameColumns;
			MTableFunctionName selectRows = MTableFunctionName.SelectRows;
			dictionary[selectRows] = localizedStrings.SelectedRows;
			MTableFunctionName skip = MTableFunctionName.Skip;
			dictionary[skip] = localizedStrings.RemovedTopRows;
			base..ctor(sourceStep, localizedStrings, escape, dictionary, null, null);
			Dictionary<MSplitterFunctionName, string> dictionary2 = new Dictionary<MSplitterFunctionName, string>();
			MSplitterFunctionName splitTextByEachDelimiter = MSplitterFunctionName.SplitTextByEachDelimiter;
			dictionary2[splitTextByEachDelimiter] = localizedStrings.SplitColumnByDelimiters;
			MSplitterFunctionName splitTextByPositions = MSplitterFunctionName.SplitTextByPositions;
			dictionary2[splitTextByPositions] = localizedStrings.SplitColumnByPositions;
			MSplitterFunctionName splitTextByRanges = MSplitterFunctionName.SplitTextByRanges;
			dictionary2[splitTextByRanges] = localizedStrings.SplitColumnByRanges;
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
			this._listStepNames = new Dictionary<MListFunctionName, string>();
		}

		// Token: 0x06009195 RID: 37269 RVA: 0x001EAE49 File Offset: 0x001E9049
		public PowerQueryMCodeBuilder(string binaryContent, string encoding, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
			: this(string.Concat(new string[] { "Table.FromColumns({Lines.FromBinary(", binaryContent, ", null, null, ", encoding, ")})" }), localizedStrings, escape)
		{
		}

		// Token: 0x06009196 RID: 37270 RVA: 0x001EAE80 File Offset: 0x001E9080
		public void AddTransformColumnStep(MTextFunctionName funcName, IReadOnlyList<string> arguments, int columnIndex)
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

		// Token: 0x06009197 RID: 37271 RVA: 0x001EAF14 File Offset: 0x001E9114
		public void AddListStep(MListFunctionName funcName, IEnumerable<string> arguments, string stepName = null)
		{
			string text = string.Concat(new string[]
			{
				"List.",
				funcName.Name,
				"(",
				string.Join(", ", arguments.PrependItem(base.EscapedLastStepName)),
				")"
			});
			if (stepName == null && !this._listStepNames.TryGetValue(funcName, out stepName))
			{
				throw new ArgumentException("No step name found for " + funcName.Name);
			}
			base.AddStep(text, stepName, false);
		}

		// Token: 0x06009198 RID: 37272 RVA: 0x001EAF9C File Offset: 0x001E919C
		public void AddSplitColumnStep(MSplitterFunctionName funcName, IReadOnlyList<string> arguments, int columnIndex, IReadOnlyList<string> newColumnNames)
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
				"{" + string.Join(", ", newColumnNames.Select(new Func<string, string>(base.EscapeString))) + "}"
			}, text);
		}

		// Token: 0x04003BD6 RID: 15318
		internal const string ColumnPrefix = "Column";

		// Token: 0x04003BD7 RID: 15319
		private readonly Dictionary<MSplitterFunctionName, string> _splitColumnNames;

		// Token: 0x04003BD8 RID: 15320
		private readonly Dictionary<MTextFunctionName, string> _transformColumnNames;

		// Token: 0x04003BD9 RID: 15321
		private readonly Dictionary<MListFunctionName, string> _listStepNames;
	}
}
