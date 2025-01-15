using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DD9 RID: 3545
	public class MProgram : Program<ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x060059EB RID: 23019 RVA: 0x0011DC78 File Offset: 0x0011BE78
		internal MProgram(ProgramNode programNode, double score)
			: base(programNode, score, null)
		{
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x0011DC83 File Offset: 0x0011BE83
		public override SpreadsheetArea Run(ISpreadsheetPair input)
		{
			return (SpreadsheetArea)base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input));
		}

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x0011DCA8 File Offset: 0x0011BEA8
		internal IReadOnlyList<IReadOnlyList<string>> SerializableProgram
		{
			get
			{
				List<IReadOnlyList<string>> res = new List<IReadOnlyList<string>>();
				mProgram program = Language.Build.Node.Cast.mProgram(base.ProgramNode);
				mTable? table = null;
				while (table == null)
				{
					program.Switch(Language.Build, delegate(mProgram_mTable conv)
					{
						table = new mTable?(conv.mTable);
					}, delegate(RemoveEmptyRows removeEmptyRows)
					{
						res.Add(new string[] { "RemoveEmptyRows" });
						program = removeEmptyRows.mProgram;
					}, delegate(RemoveEmptyColumns removeEmptyColumns)
					{
						res.Add(new string[] { "RemoveEmptyColumns" });
						program = removeEmptyColumns.mProgram;
					});
				}
				while (table != null)
				{
					table.Value.Switch(Language.Build, delegate(MWholeSheet wholeSheet)
					{
						table = null;
					}, delegate(KthMSection kthMSection)
					{
						base.<get_SerializableProgram>g__HandleKthMSection|3(kthMSection.k.Value, kthMSection.mSection, 1);
					}, delegate(KthAndNextMSection kthMSection)
					{
						base.<get_SerializableProgram>g__HandleKthMSection|3(kthMSection.k.Value, kthMSection.mSection, 2);
					}, delegate(MTrimTopSingleCellRows trimTopSingleCellRows)
					{
						table = new mTable?(trimTopSingleCellRows.mTable);
						res.Add(new string[] { "TrimTopSingleCellRows" });
					}, delegate(MTrimTopSingleLeftCellRows trimTopSingleLeftCellRows)
					{
						table = new mTable?(trimTopSingleLeftCellRows.mTable);
						res.Add(new string[] { "TrimTopSingleLeftCellRows" });
					}, delegate(MTrimBottomSingleCellRows trimBottomSingleCellRows)
					{
						table = new mTable?(trimBottomSingleCellRows.mTable);
						res.Add(new string[] { "TrimBottomSingleCellRows" });
					}, delegate(MTrimLeftSingleCellColumns trimLeftSingleCellColumns)
					{
						table = new mTable?(trimLeftSingleCellColumns.mTable);
						res.Add(new string[] { "TrimLeftSingleCellColumns" });
					}, delegate(MTrimRightSingleCellColumns trimRightSingleCellColumns)
					{
						table = new mTable?(trimRightSingleCellColumns.mTable);
						res.Add(new string[] { "TrimRightSingleCellColumns" });
					}, delegate(MTrimTopDoubleCellRows trimTopDoubleCellRows)
					{
						table = new mTable?(trimTopDoubleCellRows.mTable);
						res.Add(new string[] { "TrimTopDoubleCellRows" });
					}, delegate(MTrimBottomDoubleCellRows trimBottomDoubleCellRows)
					{
						table = new mTable?(trimBottomDoubleCellRows.mTable);
						res.Add(new string[] { "TrimBottomDoubleCellRows" });
					});
				}
				res.Reverse();
				return res;
			}
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x0011DDEC File Offset: 0x0011BFEC
		public string TranslateToM(Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals, IEnumerable<KeyValuePair<string, string>> setup)
		{
			MProgram.<>c__DisplayClass4_0 CS$<>8__locals1 = new MProgram.<>c__DisplayClass4_0();
			CS$<>8__locals1.localizedStrings = localizedStrings;
			CS$<>8__locals1.escapeLiterals = escapeLiterals;
			CS$<>8__locals1.pqCodeBuilder = new Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.PowerQueryMCodeBuilder(setup, CS$<>8__locals1.localizedStrings, CS$<>8__locals1.escapeLiterals);
			CS$<>8__locals1.transposed = false;
			CS$<>8__locals1.filterNullAndWhitespaceName = null;
			string text = CS$<>8__locals1.<TranslateToM>g__ListRemoveEmpty|2("Record.FieldValues(_)");
			foreach (IReadOnlyList<string> readOnlyList in this.SerializableProgram)
			{
				string text2 = readOnlyList[0];
				if (text2 != null)
				{
					switch (text2.Length)
					{
					case 15:
						if (!(text2 == "RemoveEmptyRows"))
						{
							goto IL_07C6;
						}
						CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(false);
						CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each not List.IsEmpty(" + text + ")" }, CS$<>8__locals1.localizedStrings.RemovedBlankRows);
						continue;
					case 16:
						if (!(text2 == "SplitOnEmptyRows"))
						{
							goto IL_07C6;
						}
						goto IL_043E;
					case 17:
					case 20:
					case 22:
					case 23:
						goto IL_07C6;
					case 18:
					{
						if (!(text2 == "RemoveEmptyColumns"))
						{
							goto IL_07C6;
						}
						CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(false);
						string escapedLastStepName = CS$<>8__locals1.pqCodeBuilder.EscapedLastStepName;
						CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.SelectColumns, new string[] { string.Concat(new string[]
						{
							"List.Select(Table.ColumnNames(",
							escapedLastStepName,
							"), each try not List.IsEmpty(",
							CS$<>8__locals1.<TranslateToM>g__ListRemoveEmpty|2("Table.Column(" + escapedLastStepName + ", _)"),
							") otherwise true)"
						}) }, null);
						continue;
					}
					case 19:
						if (!(text2 == "SplitOnEmptyColumns"))
						{
							goto IL_07C6;
						}
						goto IL_043E;
					case 21:
					{
						char c = text2[7];
						if (c != 'D')
						{
							if (c != 'S')
							{
								goto IL_07C6;
							}
							if (!(text2 == "TrimTopSingleCellRows"))
							{
								goto IL_07C6;
							}
						}
						else if (!(text2 == "TrimTopDoubleCellRows"))
						{
							goto IL_07C6;
						}
						break;
					}
					case 24:
					{
						char c = text2[10];
						if (c != 'D')
						{
							if (c != 'S')
							{
								goto IL_07C6;
							}
							if (!(text2 == "TrimBottomSingleCellRows"))
							{
								goto IL_07C6;
							}
						}
						else if (!(text2 == "TrimBottomDoubleCellRows"))
						{
							goto IL_07C6;
						}
						break;
					}
					case 25:
					{
						char c = text2[4];
						if (c != 'L')
						{
							if (c != 'T')
							{
								goto IL_07C6;
							}
							if (!(text2 == "TrimTopSingleLeftCellRows"))
							{
								goto IL_07C6;
							}
							CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(false);
							CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.Skip, new string[] { "each try List.IsEmpty(" + CS$<>8__locals1.<TranslateToM>g__ListRemoveEmpty|2("List.Skip(Record.FieldValues(_), 1)") + ") otherwise false" }, null);
							continue;
						}
						else if (!(text2 == "TrimLeftSingleCellColumns"))
						{
							goto IL_07C6;
						}
						break;
					}
					case 26:
						if (!(text2 == "TrimRightSingleCellColumns"))
						{
							goto IL_07C6;
						}
						break;
					default:
						goto IL_07C6;
					}
					CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(readOnlyList[0] == "TrimLeftSingleCellColumns" || readOnlyList[0] == "TrimRightSingleCellColumns");
					string text3 = ((readOnlyList[0] == "TrimTopDoubleCellRows" || readOnlyList[0] == "TrimBottomDoubleCellRows") ? "2" : "1");
					CS$<>8__locals1.pqCodeBuilder.AddStep((readOnlyList[0] == "TrimTopSingleCellRows" || readOnlyList[0] == "TrimTopDoubleCellRows" || readOnlyList[0] == "TrimLeftSingleCellColumns") ? MTableFunctionName.Skip : MTableFunctionName.RemoveLastN, new string[] { string.Concat(new string[] { "each try List.IsEmpty(List.Skip(", text, ", ", text3, ")) otherwise false" }) }, null);
					continue;
					IL_043E:
					CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(readOnlyList[0] == "SplitOnEmptyColumns");
					int num = int.Parse(readOnlyList[1]);
					int num2 = int.Parse(readOnlyList[2]);
					string text4 = CS$<>8__locals1.escapeLiterals.EscapeString(CS$<>8__locals1.localizedStrings.IsEmptyRow_ColumnName);
					string text5 = CS$<>8__locals1.escapeLiterals.EscapeFieldIdentifier(CS$<>8__locals1.localizedStrings.IsEmptyRow_ColumnName);
					string text6 = CS$<>8__locals1.escapeLiterals.EscapeString(CS$<>8__locals1.localizedStrings.Index_ColumnName);
					string text7 = CS$<>8__locals1.escapeLiterals.EscapeFieldIdentifier(CS$<>8__locals1.localizedStrings.Index_ColumnName);
					string text8 = CS$<>8__locals1.escapeLiterals.EscapeString(CS$<>8__locals1.localizedStrings.Section_ColumnName);
					string text9 = CS$<>8__locals1.escapeLiterals.EscapeString(CS$<>8__locals1.localizedStrings.Rows_ColumnName);
					string rowsIdentifier = CS$<>8__locals1.escapeLiterals.EscapeFieldIdentifier(CS$<>8__locals1.localizedStrings.Rows_ColumnName);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.AddColumn, new string[]
					{
						text4,
						"each try List.IsEmpty(" + text + ") otherwise false"
					}, null);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.AddIndexColumn, new string[] { text6, "-1" }, null);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.AddColumn, new string[]
					{
						text8,
						string.Concat(new string[]
						{
							"each if [",
							text5,
							"] then -1 else if try ",
							CS$<>8__locals1.pqCodeBuilder.EscapedLastStepName,
							"[",
							text5,
							"]{[",
							text7,
							"]} otherwise true then [",
							text7,
							"] else null"
						})
					}, null);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each not [" + text5 + "]" }, CS$<>8__locals1.localizedStrings.FilterIsEmptyRow);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.FillDown, new string[] { "{" + text8 + "}" }, null);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.Group, new string[]
					{
						"{" + text8 + "}",
						"{{" + text9 + ", each _}}",
						"GroupKind.Local"
					}, null);
					CS$<>8__locals1.pqCodeBuilder.AddStep(string.Join(" & ", from kIdx in Enumerable.Range(num, num2)
						select string.Format("{0}[{1}]{{{2}}}", CS$<>8__locals1.pqCodeBuilder.EscapedLastStepName, rowsIdentifier, kIdx)), CS$<>8__locals1.localizedStrings.SelectedGroup, false);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.RemoveColumns, new string[] { string.Concat(new string[] { "{", text4, ", ", text6, ", ", text8, "}" }) }, null);
					continue;
				}
				IL_07C6:
				throw new NotImplementedException("Unknown step: " + readOnlyList.ToLiteral(null));
			}
			CS$<>8__locals1.<TranslateToM>g__EnsureTransposed|1(false);
			return CS$<>8__locals1.pqCodeBuilder.GetCode();
		}
	}
}
