using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x02000913 RID: 2323
	public class ProgramProperties : IProgramProperties
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x00094308 File Offset: 0x00092508
		internal ProgramProperties(ProgramType type, int skipLinesCount, int skipEmptyAndCommentLinesCount, int skipFooterLinesCount, bool filterEmptyLines, bool hasEmptyLines, string commentStr, bool hasCommentHeader, Optional<int> headerIndex, QuotingConfiguration quotingConf, RegularExpression headerRegex, RegularExpression dataRegex, RegularExpression recordDelimiterRegex, IReadOnlyList<string> rawColumnNames, IReadOnlyList<int> selectedColumns, int columnCount, string columnDelimiter, IReadOnlyList<Record<int, int?>> fieldPositions, IReadOnlyList<string> newLineStrings, IReadOnlyList<InputLineTelemetry> telemetry)
		{
			this.ProgramType = type;
			this.SkipLinesCount = skipLinesCount;
			this.SkipEmptyAndCommentLinesCount = skipEmptyAndCommentLinesCount;
			this.SkipFooterLinesCount = skipFooterLinesCount;
			this.FilterEmptyLines = filterEmptyLines;
			this.HasEmptyLines = hasEmptyLines;
			this.CommentStr = commentStr;
			this.HasCommentHeader = hasCommentHeader;
			this.HeaderIndex = headerIndex;
			this.QuotingConf = quotingConf;
			this.HeaderRegex = headerRegex;
			this.DataRegex = dataRegex;
			this.RecordDelimiterRegex = recordDelimiterRegex;
			this.RawColumnNames = rawColumnNames;
			this.SelectedColumns = selectedColumns;
			this.ColumnCount = columnCount;
			this.ColumnDelimiter = columnDelimiter;
			this.FieldPositions = fieldPositions;
			this.NewLineStrings = newLineStrings;
			this.Telemetry = telemetry;
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600320C RID: 12812 RVA: 0x000943B8 File Offset: 0x000925B8
		public int ColumnCount { get; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000943C0 File Offset: 0x000925C0
		public string ColumnDelimiter { get; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600320E RID: 12814 RVA: 0x000943C8 File Offset: 0x000925C8
		public string CommentStr { get; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000943D0 File Offset: 0x000925D0
		public RegularExpression DataRegex { get; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003210 RID: 12816 RVA: 0x000943D8 File Offset: 0x000925D8
		public IReadOnlyList<Record<int, int?>> FieldPositions { get; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000943E0 File Offset: 0x000925E0
		public bool FilterEmptyLines { get; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06003212 RID: 12818 RVA: 0x000943E8 File Offset: 0x000925E8
		public bool HasCommentHeader { get; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003213 RID: 12819 RVA: 0x000943F0 File Offset: 0x000925F0
		public bool HasEmptyLines { get; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x000943F8 File Offset: 0x000925F8
		public Optional<int> HeaderIndex { get; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x00094400 File Offset: 0x00092600
		public RegularExpression HeaderRegex { get; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x00094408 File Offset: 0x00092608
		public IReadOnlyList<string> NewLineStrings { get; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x00094410 File Offset: 0x00092610
		public ProgramType ProgramType { get; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x00094418 File Offset: 0x00092618
		public QuotingConfiguration QuotingConf { get; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x00094420 File Offset: 0x00092620
		public IReadOnlyList<string> RawColumnNames { get; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x00094428 File Offset: 0x00092628
		public RegularExpression RecordDelimiterRegex { get; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x00094430 File Offset: 0x00092630
		public IReadOnlyList<int> SelectedColumns { get; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00094438 File Offset: 0x00092638
		public int SkipEmptyAndCommentLinesCount { get; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x00094440 File Offset: 0x00092640
		public int SkipFooterLinesCount { get; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00094448 File Offset: 0x00092648
		public int SkipLinesCount { get; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x00094450 File Offset: 0x00092650
		public IReadOnlyList<InputLineTelemetry> Telemetry { get; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x00094458 File Offset: 0x00092658
		public char? QuoteCharacter
		{
			get
			{
				return this.QuotingConf.QuoteChar;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x00094474 File Offset: 0x00092674
		public bool HasNewLineInQuotes
		{
			get
			{
				return this.QuoteCharacter != null || this.EscapeCharacter != null;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000944A4 File Offset: 0x000926A4
		public char? EscapeCharacter
		{
			get
			{
				return this.QuotingConf.EscapeChar;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000944C0 File Offset: 0x000926C0
		public bool HasHeader
		{
			get
			{
				return this.HeaderIndex.HasValue;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000944DC File Offset: 0x000926DC
		public bool IsStandard
		{
			get
			{
				return this.ProgramType == ProgramType.SimpleOneColumn || this.ProgramType == ProgramType.FixedWidth || (this.ProgramType == ProgramType.SimpleDelimiter && this.QuotingConf.Style == QuotingStyle.Standard);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x00094518 File Offset: 0x00092718
		public bool PySparkSupported
		{
			get
			{
				return (this.ProgramType == ProgramType.SimpleOneColumn || this.ProgramType == ProgramType.FixedWidth || this.ProgramType == ProgramType.SimpleDelimiter) && this.QuotingConf.Style == QuotingStyle.Standard && this.EscapeCharacter == null && (this.ColumnDelimiter == null || this.CommentStr == null) && (this.ColumnDelimiter == null || (this.ColumnDelimiter.Length == 1 && this.ColumnDelimiter[0] <= '\u007f')) && this.SkipFooterLinesCount == 0;
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000945A0 File Offset: 0x000927A0
		public ProgramProperties Override(int skipLinesCount, int skipFooterLinesCount, int skipEmptyAndCommentLinesCount, bool filterEmptyLines, string commentStr, bool hasCommentHeader, Optional<int> headerIndex, QuotingConfiguration quotingConf, IReadOnlyList<int> selectedColumns, int columnCount, string columnDelimiter, IReadOnlyList<Record<int, int?>> fieldPositions)
		{
			if (columnDelimiter == null && (quotingConf.QuoteChar != null || quotingConf.EscapeChar != null))
			{
				throw new ArgumentException("QuoteChar and EscapeChar supported only with columnDelimiter");
			}
			if (columnDelimiter != null && fieldPositions != null)
			{
				throw new ArgumentException("Cannot have both columnDelimiter and fieldPositions");
			}
			ProgramType programType;
			if (columnDelimiter != null)
			{
				programType = ProgramType.SimpleDelimiter;
			}
			else if (fieldPositions != null)
			{
				programType = ProgramType.FixedWidth;
			}
			else
			{
				programType = ProgramType.SimpleOneColumn;
			}
			return new ProgramProperties(programType, skipLinesCount, skipEmptyAndCommentLinesCount, skipFooterLinesCount, filterEmptyLines, this.HasEmptyLines, commentStr, hasCommentHeader, headerIndex, quotingConf, null, null, null, this.RawColumnNames, selectedColumns, columnCount, columnDelimiter, fieldPositions, this.NewLineStrings, this.Telemetry);
		}
	}
}
