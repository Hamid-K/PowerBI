using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DDD RID: 3549
	public class Program : Program<ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x06005A06 RID: 23046 RVA: 0x0011DC78 File Offset: 0x0011BE78
		public Program(ProgramNode programNode, double score)
			: base(programNode, score, null)
		{
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x0011DC83 File Offset: 0x0011BE83
		public override SpreadsheetArea Run(ISpreadsheetPair input)
		{
			return (SpreadsheetArea)base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input));
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x0011E97C File Offset: 0x0011CB7C
		public Program.Summary SummarizeFor(ISpreadsheetPair input, bool cleanup = true, bool learnTitle = true, TimeSpan? titleTimeout = null)
		{
			SpreadsheetArea spreadsheetArea = this.Run(input);
			ISpreadsheet withFormatting = input.WithFormatting;
			Bounds<TableUnit> span = spreadsheetArea.Span;
			if (!cleanup)
			{
				spreadsheetArea = new SpreadsheetArea(spreadsheetArea.Spreadsheet, span, null, null, null);
			}
			MProgram mprogram = MLearner.Instance.LearnBestTranslation(this, input, cleanup, default(CancellationToken));
			string text = null;
			if (learnTitle)
			{
				try
				{
					TimeSpan timeSpan = titleTimeout.GetValueOrDefault();
					if (titleTimeout == null)
					{
						timeSpan = Program.DefaultTitleTimeout;
						titleTimeout = new TimeSpan?(timeSpan);
					}
					CancellationTokenSource cancellationTokenSource;
					if (!Debugger.IsAttached)
					{
						TimeSpan? timeSpan2 = titleTimeout;
						timeSpan = TimeSpan.Zero;
						if (timeSpan2 == null || (timeSpan2 != null && timeSpan2.GetValueOrDefault() != timeSpan))
						{
							cancellationTokenSource = new CancellationTokenSource(titleTimeout.Value);
							goto IL_00B5;
						}
					}
					cancellationTokenSource = null;
					IL_00B5:
					using (CancellationTokenSource cancellationTokenSource2 = cancellationTokenSource)
					{
						CancellationToken cancellationToken = ((cancellationTokenSource2 != null) ? cancellationTokenSource2.Token : CancellationToken.None);
						Program program = new Program(Language.Build.Node.Rule.Output(Language.Build.Node.Rule.Trim(Language.Build.Node.UnnamedConversion.area_trimLeft(Language.Build.Node.UnnamedConversion.trimLeft_trimBottom(Language.Build.Node.UnnamedConversion.trimBottom_trimTop(Language.Build.Node.UnnamedConversion.trimTop_sheetSection(Language.Build.Node.UnnamedConversion.sheetSection_horizontalSheetSection(Language.Build.Node.UnnamedConversion.horizontalSheetSection_verticalSheetSection(Language.Build.Node.UnnamedConversion.verticalSheetSection_uncleanedSheetSection(Language.Build.Node.Rule.Area(Language.Build.Node.Rule.WithFormatting(Language.Build.Node.Variable.sheetPair), Language.Build.Node.Rule.index(span.Left), Language.Build.Node.Rule.index(span.Top), Language.Build.Node.Rule.index(span.Right), Language.Build.Node.Rule.index(span.Bottom))))))))))).Node, 0.0);
						TitleProgram titleProgram = TitleLearner.Instance.LearnFor(program, input, cancellationToken);
						text = ((titleProgram != null) ? titleProgram.RunForString(input) : null);
					}
				}
				catch (OperationCanceledException)
				{
				}
			}
			if (text == null)
			{
				text = spreadsheetArea.Name;
			}
			Bounds<TableUnit> bounds = span;
			int num = withFormatting.Size.Y - span.Bottom - 1;
			IReadOnlyList<string> readOnlyList;
			if (span.Width() >= withFormatting.Size.X && spreadsheetArea.IncludedColumns == null)
			{
				readOnlyList = null;
			}
			else
			{
				IReadOnlyList<string> readOnlyList2 = spreadsheetArea.IncludedColumnsEnumerable.Select((int columnIndex) => "Column" + (columnIndex + 1).ToString()).ToList<string>();
				readOnlyList = readOnlyList2;
			}
			return new Program.Summary(bounds, num, readOnlyList, cleanup && spreadsheetArea.ContainsEmptyRow, null, text, mprogram);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x0011ECC0 File Offset: 0x0011CEC0
		public string TranslateToMFor(ISpreadsheetPair input, Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals, IEnumerable<KeyValuePair<string, string>> setup, bool cleanup = true)
		{
			return Program.TranslateToMFor(this.SummarizeFor(input, cleanup, true, null), localizedStrings, escapeLiterals, setup, cleanup);
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x0011ECEC File Offset: 0x0011CEEC
		public static string TranslateToMFor(Program.Summary summary, Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals, IEnumerable<KeyValuePair<string, string>> setup, bool cleanup = true)
		{
			Program.<>c__DisplayClass6_0 CS$<>8__locals1 = new Program.<>c__DisplayClass6_0();
			CS$<>8__locals1.escapeLiterals = escapeLiterals;
			CS$<>8__locals1.localizedStrings = localizedStrings;
			CS$<>8__locals1.pqCodeBuilder = new Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.PowerQueryMCodeBuilder(setup, CS$<>8__locals1.localizedStrings, CS$<>8__locals1.escapeLiterals);
			int? skip = summary.Skip;
			if (skip != null)
			{
				CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.Skip, new string[] { skip.Value.ToString() }, null);
			}
			int? removeLastN = summary.RemoveLastN;
			if (removeLastN != null)
			{
				CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.RemoveLastN, new string[] { removeLastN.Value.ToString() }, null);
			}
			IReadOnlyList<string> selectColumns = summary.SelectColumns;
			if (selectColumns != null)
			{
				CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.SelectColumns, new string[] { "{" + string.Join(", ", selectColumns.Select(new Func<string, string>(CS$<>8__locals1.escapeLiterals.EscapeString))) + "}" }, null);
			}
			if (cleanup)
			{
				if (summary.ContainsBlankRows)
				{
					Program.<>c__DisplayClass6_1 CS$<>8__locals2;
					CS$<>8__locals2.filterNullAndWhitespace = CS$<>8__locals1.pqCodeBuilder.AddStep("each List.Select(_, each _ <> null and (not (_ is text) or Text.Trim(_) <> \"\"))", CS$<>8__locals1.localizedStrings.FilterNullAndWhitespace, true);
					string text = CS$<>8__locals1.<TranslateToMFor>g__ListRemoveEmpty|1("Record.FieldValues(_)", ref CS$<>8__locals2);
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each not List.IsEmpty(" + text + ")" }, CS$<>8__locals1.localizedStrings.RemovedBlankRows);
				}
				int? numHeaderRows = summary.NumHeaderRows;
				if (numHeaderRows != null)
				{
					int valueOrDefault = numHeaderRows.GetValueOrDefault();
					if (valueOrDefault > 1)
					{
						CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.Transpose, new string[0], null);
						CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.CombineColumns, new string[]
						{
							"{" + string.Join(", ", from num in Enumerable.Range(1, valueOrDefault).Reverse<int>()
								select CS$<>8__locals1.pqCodeBuilder.EscapeString(CS$<>8__locals1.localizedStrings.ColumnPrefix + num.ToString())) + "}",
							"Combiner.CombineTextByDelimiter(" + CS$<>8__locals1.escapeLiterals.EscapeString(CS$<>8__locals1.localizedStrings.CombinedHeadersDelimiter) + ", QuoteStyle.None)",
							"\"Header\""
						}, null);
						CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.Transpose, new string[0], null);
					}
					CS$<>8__locals1.pqCodeBuilder.AddStep(MTableFunctionName.PromoteHeaders, new string[0], null);
				}
			}
			return CS$<>8__locals1.pqCodeBuilder.GetCode();
		}

		// Token: 0x04002A33 RID: 10803
		private static readonly TimeSpan DefaultTitleTimeout = TimeSpan.FromSeconds(1.0);

		// Token: 0x02000DDE RID: 3550
		public class Summary
		{
			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x06005A0C RID: 23052 RVA: 0x0011EF6F File Offset: 0x0011D16F
			public Bounds<TableUnit> Span { get; }

			// Token: 0x1700106E RID: 4206
			// (get) Token: 0x06005A0D RID: 23053 RVA: 0x0011EF78 File Offset: 0x0011D178
			public int? Skip
			{
				get
				{
					if (this.Span.Top != 0)
					{
						return new int?(this.Span.Top);
					}
					return null;
				}
			}

			// Token: 0x1700106F RID: 4207
			// (get) Token: 0x06005A0E RID: 23054 RVA: 0x0011EFB2 File Offset: 0x0011D1B2
			public int? RemoveLastN { get; }

			// Token: 0x17001070 RID: 4208
			// (get) Token: 0x06005A0F RID: 23055 RVA: 0x0011EFBA File Offset: 0x0011D1BA
			public IReadOnlyList<string> SelectColumns { get; }

			// Token: 0x17001071 RID: 4209
			// (get) Token: 0x06005A10 RID: 23056 RVA: 0x0011EFC2 File Offset: 0x0011D1C2
			public bool ContainsBlankRows { get; }

			// Token: 0x17001072 RID: 4210
			// (get) Token: 0x06005A11 RID: 23057 RVA: 0x0011EFCA File Offset: 0x0011D1CA
			public int? NumHeaderRows { get; }

			// Token: 0x17001073 RID: 4211
			// (get) Token: 0x06005A12 RID: 23058 RVA: 0x0011EFD2 File Offset: 0x0011D1D2
			public string Title { get; }

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x06005A13 RID: 23059 RVA: 0x0011EFDA File Offset: 0x0011D1DA
			public MProgram MProgram { get; }

			// Token: 0x17001075 RID: 4213
			// (get) Token: 0x06005A14 RID: 23060 RVA: 0x0011EFE2 File Offset: 0x0011D1E2
			public IReadOnlyList<IReadOnlyList<string>> SerializedProgram
			{
				get
				{
					MProgram mprogram = this.MProgram;
					if (mprogram == null)
					{
						return null;
					}
					return mprogram.SerializableProgram;
				}
			}

			// Token: 0x06005A15 RID: 23061 RVA: 0x0011EFF8 File Offset: 0x0011D1F8
			public string TranslateToM(Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Translation.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeLiterals, string workbookFormula, string sheetName)
			{
				IReadOnlyList<KeyValuePair<string, string>> readOnlyList = new KeyValuePair<string, string>[]
				{
					KVP.Create<string, string>(localizedStrings.Source, "Excel.Workbook(" + workbookFormula + ", null, false)"),
					KVP.Create<string, string>(sheetName + localizedStrings.NavigationSuffixForSheet, escapeLiterals.EscapeIdentifier(localizedStrings.Source) + "{[Item=" + escapeLiterals.EscapeString(sheetName) + ",Kind=\"Sheet\"]}[Data]")
				};
				MProgram mprogram = this.MProgram;
				return ((mprogram != null) ? mprogram.TranslateToM(localizedStrings, escapeLiterals, readOnlyList) : null) ?? Program.TranslateToMFor(this, localizedStrings, escapeLiterals, readOnlyList, true);
			}

			// Token: 0x06005A16 RID: 23062 RVA: 0x0011F090 File Offset: 0x0011D290
			internal Summary(Bounds<TableUnit> span, int removeLastN, IReadOnlyList<string> selectColumns, bool containsBlankRows, int? numHeaderRows, string title, MProgram mProgram)
			{
				this.Span = span;
				this.RemoveLastN = ((removeLastN == 0) ? null : new int?(removeLastN));
				this.SelectColumns = selectColumns;
				this.ContainsBlankRows = containsBlankRows;
				this.NumHeaderRows = numHeaderRows;
				this.Title = title;
				this.MProgram = mProgram;
			}
		}
	}
}
