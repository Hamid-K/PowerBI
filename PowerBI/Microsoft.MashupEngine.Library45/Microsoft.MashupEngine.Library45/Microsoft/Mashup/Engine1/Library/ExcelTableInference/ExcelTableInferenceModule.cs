using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Excel;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.Mashup.Engine1.Library.ExcelTableInference
{
	// Token: 0x02000015 RID: 21
	public sealed class ExcelTableInferenceModule : Module45
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003DB4 File Offset: 0x00001FB4
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ExcelTableInferenceModule.TablesFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003DE5 File Offset: 0x00001FE5
		public override string Name
		{
			get
			{
				return "ExcelTableInference";
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003DEC File Offset: 0x00001FEC
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "ExcelTableInference.InferTables";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x04000050 RID: 80
		public const string ExcelTableInferenceInferTablesName = "ExcelTableInference.InferTables";

		// Token: 0x04000051 RID: 81
		private Keys exportKeys;

		// Token: 0x02000016 RID: 22
		private class TablesFunctionValue : NativeFunctionValue3<TextValue, BinaryValue, TextValue, RecordValue>
		{
			// Token: 0x06000067 RID: 103 RVA: 0x00003E28 File Offset: 0x00002028
			public TablesFunctionValue(IEngineHost host)
				: base(TypeValue.Text, 3, "workbookBinary", TypeValue.Binary, "binaryMCode", TypeValue.Text, "localizedStrings", TypeValue.Record)
			{
				this.host = host;
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00003E68 File Offset: 0x00002068
			public override TextValue TypedInvoke(BinaryValue workbookBinary, TextValue binaryMCode, RecordValue localizedStrings)
			{
				TableValue tableValue;
				try
				{
					ExcelFile excelFile = new ExcelFile(this.host, workbookBinary);
					tableValue = new ExcelReaderOpenXml(this.host, excelFile, false, false, false, true).ReadTables();
				}
				catch (FileFormatException)
				{
					return TextValue.Empty;
				}
				List<RecordValue> list = new List<RecordValue>();
				List<string> list2;
				IReadOnlyList<KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair>> readOnlyList = ProgramSynthesisSpreadsheetAdapter.ReadExcelWorkbook(tableValue, out list2);
				NumberValue numberValue = NumberValue.New(list2.Count);
				NumberValue numberValue2 = NumberValue.New(readOnlyList.Count);
				foreach (KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair> keyValuePair in readOnlyList)
				{
					string key = keyValuePair.Key;
					TextValue textValue = TextValue.New(key);
					ProgramSynthesisSpreadsheetAdapter.Pair value = keyValuePair.Value;
					try
					{
						foreach (Program program in Learner.Instance.LearnAllTables(value, false, false, default(CancellationToken)))
						{
							Bounds<TableUnit> span = program.Run(value).Span;
							Program.Summary summary = program.SummarizeFor(value, true, true, null);
							string text = summary.TranslateToM(new ExcelTableInferenceModule.TablesFunctionValue.LocalizedStrings(localizedStrings), new ExcelTableInferenceModule.TablesFunctionValue.EscapeHelper(Engine.Instance), binaryMCode.AsString, key);
							list.Add(RecordValue.New(ExcelTableInferenceModule.TablesFunctionValue.TableResultKeys, new Value[]
							{
								textValue,
								TextValue.NewOrNull(summary.Title),
								NumberValue.New(span.Width()),
								NumberValue.New(span.Height()),
								NumberValue.New(value.WithFormatting.Size.X - span.Width()),
								NumberValue.New(value.WithFormatting.Size.Y - span.Height()),
								TextValue.NewOrNull(text),
								LogicalValue.New(summary.SerializedProgram == null)
							}));
						}
					}
					catch (TaskCanceledException)
					{
						return TextValue.Empty;
					}
					catch (AggregateException ex)
					{
						AggregateException ex2;
						if (!ex2.Flatten().InnerExceptions.All((Exception ex) => ex is TaskCanceledException))
						{
							throw ex2;
						}
						return TextValue.Empty;
					}
				}
				Keys resultKeys = ExcelTableInferenceModule.TablesFunctionValue.ResultKeys;
				Value[] array = new Value[4];
				array[0] = ListValue.New(list);
				array[1] = ListValue.New(readOnlyList.Select((KeyValuePair<string, ProgramSynthesisSpreadsheetAdapter.Pair> kv) => kv.Key).Concat(list2).ToArray<string>());
				array[2] = numberValue2;
				array[3] = numberValue;
				Value value2 = RecordValue.New(resultKeys, array);
				return Library.Text.FromBinary.Invoke(JsonModule.Json.FromValue.Invoke(value2)).AsText;
			}

			// Token: 0x04000052 RID: 82
			private static readonly Keys ResultKeys = Keys.New("Tables", "DisallowedNames", "WorkbookSheetCount", "WorkbookTableCount");

			// Token: 0x04000053 RID: 83
			private static readonly Keys TableResultKeys = Keys.New(new string[] { "SheetName", "Title", "ColumnCount", "RowCount", "ColumnsOmitted", "RowsOmitted", "MCode", "IsSimpleRectangle" });

			// Token: 0x04000054 RID: 84
			private readonly IEngineHost host;

			// Token: 0x02000017 RID: 23
			private class EscapeHelper : IEscapePowerQueryM
			{
				// Token: 0x0600006A RID: 106 RVA: 0x00004213 File Offset: 0x00002413
				public EscapeHelper(IEngine engine)
				{
					this._engine = engine;
				}

				// Token: 0x0600006B RID: 107 RVA: 0x00004222 File Offset: 0x00002422
				public string EscapeFieldIdentifier(string fieldIdentifier)
				{
					return this._engine.EscapeFieldIdentifier(fieldIdentifier);
				}

				// Token: 0x0600006C RID: 108 RVA: 0x00004230 File Offset: 0x00002430
				public string EscapeIdentifier(string identifier)
				{
					return this._engine.EscapeIdentifier(identifier);
				}

				// Token: 0x0600006D RID: 109 RVA: 0x0000423E File Offset: 0x0000243E
				public string EscapeString(string s)
				{
					return this._engine.EscapeString(s);
				}

				// Token: 0x04000055 RID: 85
				private readonly IEngine _engine;
			}

			// Token: 0x02000018 RID: 24
			private class LocalizedStrings : Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.ILocalizedPowerQueryMStrings, Microsoft.ProgramSynthesis.Translation.PowerQuery.ILocalizedPowerQueryMStrings
			{
				// Token: 0x0600006E RID: 110 RVA: 0x0000424C File Offset: 0x0000244C
				public LocalizedStrings(RecordValue localizedStrings)
				{
					this.AddedColumn = localizedStrings["AddedColumn"].AsString;
					this.AddedIndex = localizedStrings["AddedIndex"].AsString;
					this.ColumnPrefix = localizedStrings["ColumnPrefix"].AsString;
					this.CombinedHeadersDelimiter = localizedStrings["CombinedHeadersDelimiter"].AsString;
					this.FilledDown = localizedStrings["FilledDown"].AsString;
					this.FilterIsEmptyRow = localizedStrings["FilterIsEmptyRow"].AsString;
					this.FilterNullAndWhitespace = localizedStrings["FilterNullAndWhitespace"].AsString;
					this.GroupedRows = localizedStrings["GroupedRows"].AsString;
					this.Index_ColumnName = localizedStrings["Index_ColumnName"].AsString;
					this.IsEmptyRow_ColumnName = localizedStrings["IsEmptyRow_ColumnName"].AsString;
					this.MergedColumns = localizedStrings["MergedColumns"].AsString;
					this.NavigationSuffixForSheet = localizedStrings["NavigationSuffixForSheet"].AsString;
					this.PromotedHeaders = localizedStrings["PromotedHeaders"].AsString;
					this.RemovedBlankRows = localizedStrings["RemovedBlankRows"].AsString;
					this.RemovedBottomRows = localizedStrings["RemovedBottomRows"].AsString;
					this.RemovedColumns = localizedStrings["RemovedColumns"].AsString;
					this.RemovedOtherColumns = localizedStrings["RemovedOtherColumns"].AsString;
					this.RemovedTopRows = localizedStrings["RemovedTopRows"].AsString;
					this.Rows_ColumnName = localizedStrings["Rows_ColumnName"].AsString;
					this.Section_ColumnName = localizedStrings["Section_ColumnName"].AsString;
					this.SelectedGroup = localizedStrings["SelectedGroup"].AsString;
					this.Source = localizedStrings["Source"].AsString;
					this.TransposedTable = localizedStrings["TransposedTable"].AsString;
				}

				// Token: 0x17000013 RID: 19
				// (get) Token: 0x0600006F RID: 111 RVA: 0x00004459 File Offset: 0x00002659
				public string AddedColumn { get; }

				// Token: 0x17000014 RID: 20
				// (get) Token: 0x06000070 RID: 112 RVA: 0x00004461 File Offset: 0x00002661
				public string AddedIndex { get; }

				// Token: 0x17000015 RID: 21
				// (get) Token: 0x06000071 RID: 113 RVA: 0x00004469 File Offset: 0x00002669
				public string ColumnPrefix { get; }

				// Token: 0x17000016 RID: 22
				// (get) Token: 0x06000072 RID: 114 RVA: 0x00004471 File Offset: 0x00002671
				public string CombinedHeadersDelimiter { get; }

				// Token: 0x17000017 RID: 23
				// (get) Token: 0x06000073 RID: 115 RVA: 0x00004479 File Offset: 0x00002679
				public string FilledDown { get; }

				// Token: 0x17000018 RID: 24
				// (get) Token: 0x06000074 RID: 116 RVA: 0x00004481 File Offset: 0x00002681
				public string FilterIsEmptyRow { get; }

				// Token: 0x17000019 RID: 25
				// (get) Token: 0x06000075 RID: 117 RVA: 0x00004489 File Offset: 0x00002689
				public string FilterNullAndWhitespace { get; }

				// Token: 0x1700001A RID: 26
				// (get) Token: 0x06000076 RID: 118 RVA: 0x00004491 File Offset: 0x00002691
				public string GroupedRows { get; }

				// Token: 0x1700001B RID: 27
				// (get) Token: 0x06000077 RID: 119 RVA: 0x00004499 File Offset: 0x00002699
				public string Index_ColumnName { get; }

				// Token: 0x1700001C RID: 28
				// (get) Token: 0x06000078 RID: 120 RVA: 0x000044A1 File Offset: 0x000026A1
				public string IsEmptyRow_ColumnName { get; }

				// Token: 0x1700001D RID: 29
				// (get) Token: 0x06000079 RID: 121 RVA: 0x000044A9 File Offset: 0x000026A9
				public string MergedColumns { get; }

				// Token: 0x1700001E RID: 30
				// (get) Token: 0x0600007A RID: 122 RVA: 0x000044B1 File Offset: 0x000026B1
				public string NavigationSuffixForSheet { get; }

				// Token: 0x1700001F RID: 31
				// (get) Token: 0x0600007B RID: 123 RVA: 0x000044B9 File Offset: 0x000026B9
				public string PromotedHeaders { get; }

				// Token: 0x17000020 RID: 32
				// (get) Token: 0x0600007C RID: 124 RVA: 0x000044C1 File Offset: 0x000026C1
				public string RemovedBlankRows { get; }

				// Token: 0x17000021 RID: 33
				// (get) Token: 0x0600007D RID: 125 RVA: 0x000044C9 File Offset: 0x000026C9
				public string RemovedBottomRows { get; }

				// Token: 0x17000022 RID: 34
				// (get) Token: 0x0600007E RID: 126 RVA: 0x000044D1 File Offset: 0x000026D1
				public string RemovedColumns { get; }

				// Token: 0x17000023 RID: 35
				// (get) Token: 0x0600007F RID: 127 RVA: 0x000044D9 File Offset: 0x000026D9
				public string RemovedOtherColumns { get; }

				// Token: 0x17000024 RID: 36
				// (get) Token: 0x06000080 RID: 128 RVA: 0x000044E1 File Offset: 0x000026E1
				public string RemovedTopRows { get; }

				// Token: 0x17000025 RID: 37
				// (get) Token: 0x06000081 RID: 129 RVA: 0x000044E9 File Offset: 0x000026E9
				public string Rows_ColumnName { get; }

				// Token: 0x17000026 RID: 38
				// (get) Token: 0x06000082 RID: 130 RVA: 0x000044F1 File Offset: 0x000026F1
				public string Section_ColumnName { get; }

				// Token: 0x17000027 RID: 39
				// (get) Token: 0x06000083 RID: 131 RVA: 0x000044F9 File Offset: 0x000026F9
				public string SelectedGroup { get; }

				// Token: 0x17000028 RID: 40
				// (get) Token: 0x06000084 RID: 132 RVA: 0x00004501 File Offset: 0x00002701
				public string Source { get; }

				// Token: 0x17000029 RID: 41
				// (get) Token: 0x06000085 RID: 133 RVA: 0x00004509 File Offset: 0x00002709
				public string TransposedTable { get; }
			}
		}

		// Token: 0x0200001A RID: 26
		private enum Exports
		{
			// Token: 0x04000071 RID: 113
			ExcelTableInference_InferTables,
			// Token: 0x04000072 RID: 114
			Count
		}
	}
}
