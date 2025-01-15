using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Read.FlatFile.Semantics;
using Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001250 RID: 4688
	public class CsvProgram : SimpleProgram
	{
		// Token: 0x06008D03 RID: 36099 RVA: 0x001D9B48 File Offset: 0x001D7D48
		public CsvProgram(IReadOnlyList<string> columnNames, int skip, int skipFooter, string delimiter, bool filterEmptyLines, Optional<string> commentStr, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuote, IReadOnlyList<string> rawColumnNames = null, int skipEmptyAndCommentCount = 0, bool hasEmptyLines = false, bool hasMultiLineRows = false, IEnumerable<string> newLineStrings = null)
			: this(CsvProgram.BuildProgram(columnNames, skip, skipFooter, delimiter, filterEmptyLines, commentStr, quoteChar, escapeChar, doubleQuote), rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrings)
		{
			this.Delimiter = delimiter;
			this.QuoteChar = quoteChar;
			this.EscapeChar = escapeChar;
			this.DoubleQuote = doubleQuote;
		}

		// Token: 0x06008D04 RID: 36100 RVA: 0x001D9B9C File Offset: 0x001D7D9C
		internal CsvProgram(Csv csvNode, IReadOnlyList<string> rawColumnNames = null, int skipEmptyAndCommentCount = 0, bool hasEmptyLines = false, bool hasMultiLineRows = false, IEnumerable<string> newLineStrings = null)
			: base(csvNode, csvNode.columnNames.Value, csvNode.skip.Value, csvNode.skipFooter.Value, csvNode.filterEmptyLines.Value, csvNode.commentStr.Value, rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrings)
		{
			this.Delimiter = csvNode.delimiter.Value;
			this.QuoteChar = csvNode.quoteChar.Value;
			this.EscapeChar = csvNode.escapeChar.Value;
			this.DoubleQuote = csvNode.doubleQuote.Value;
		}

		// Token: 0x1700182F RID: 6191
		// (get) Token: 0x06008D05 RID: 36101 RVA: 0x001D9C61 File Offset: 0x001D7E61
		public string Delimiter { get; }

		// Token: 0x17001830 RID: 6192
		// (get) Token: 0x06008D06 RID: 36102 RVA: 0x001D9C69 File Offset: 0x001D7E69
		public Optional<char> QuoteChar { get; }

		// Token: 0x17001831 RID: 6193
		// (get) Token: 0x06008D07 RID: 36103 RVA: 0x001D9C71 File Offset: 0x001D7E71
		public Optional<char> EscapeChar { get; }

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x06008D08 RID: 36104 RVA: 0x001D9C79 File Offset: 0x001D7E79
		public bool DoubleQuote { get; }

		// Token: 0x06008D09 RID: 36105 RVA: 0x001D9C84 File Offset: 0x001D7E84
		protected override ITable<string> RunInternal(string input, bool trim)
		{
			return Semantics.Csv(input, base.ColumnNames, base.Skip, base.SkipFooter, this.Delimiter, base.FilterEmptyLines, base.CommentStr, this.QuoteChar, this.EscapeChar, this.DoubleQuote, trim);
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x001D9CD0 File Offset: 0x001D7ED0
		protected override ITable<string> RunInternal(TextReader input, bool trim)
		{
			return Semantics.Csv(input, base.ColumnNames, base.Skip, base.SkipFooter, this.Delimiter, base.FilterEmptyLines, base.CommentStr, this.QuoteChar, this.EscapeChar, this.DoubleQuote, trim);
		}

		// Token: 0x06008D0B RID: 36107 RVA: 0x001D9D1C File Offset: 0x001D7F1C
		public override bool IsPySparkSupported()
		{
			if (!base.IsPySparkSupported())
			{
				return false;
			}
			if (this.Delimiter.Length > 1)
			{
				return false;
			}
			if (this.Delimiter.Length == 1 && this.Delimiter[0] > '\u007f')
			{
				return false;
			}
			if (this.EscapeChar.HasValue)
			{
				return false;
			}
			if (base.CommentStr.HasValue && (base.NewLineStrings == null || base.NewLineStrings.Contains("\n")))
			{
				return false;
			}
			if (base.SkipFooter > 0)
			{
				return false;
			}
			IReadOnlyList<string> newLineStrings = base.NewLineStrings;
			return newLineStrings == null || !newLineStrings.Contains("\r");
		}

		// Token: 0x06008D0C RID: 36108 RVA: 0x001D9DC8 File Offset: 0x001D7FC8
		private static Csv BuildProgram(IReadOnlyList<string> columnNames, int skip, int skipFooter, string delimiter, bool filterEmptyLines, Optional<string> commentStr, Optional<char> quoteChar, Optional<char> escapeChar, bool doubleQuote)
		{
			return Program.Builder.Node.Rule.Csv(Program.Builder.Node.Variable.file, Program.Builder.Node.Rule.columnNames(columnNames), Program.Builder.Node.Rule.skip(skip), Program.Builder.Node.Rule.skipFooter(skipFooter), Program.Builder.Node.Rule.delimiter(delimiter), Program.Builder.Node.Rule.filterEmptyLines(filterEmptyLines), Program.Builder.Node.Rule.commentStr(commentStr), Program.Builder.Node.Rule.quoteChar(quoteChar), Program.Builder.Node.Rule.escapeChar(escapeChar), Program.Builder.Node.Rule.doubleQuote(doubleQuote)).Cast_Csv(Program.Builder);
		}

		// Token: 0x06008D0D RID: 36109 RVA: 0x001D9ECC File Offset: 0x001D80CC
		public override string GetMCode(string binaryContent, string encoding, Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			if (this.QuoteChar.Equals('"'.Some<char>()) && !this.EscapeChar.HasValue && this.DoubleQuote && this.Delimiter.Length == 1)
			{
				string[] array = new string[9];
				array[0] = "Csv.Document(";
				array[1] = binaryContent;
				array[2] = ", [Delimiter=";
				array[3] = escape.EscapeString(this.Delimiter);
				array[4] = ", Encoding=";
				array[5] = encoding;
				array[6] = ", QuoteStyle=QuoteStyle.Csv, Columns={";
				array[7] = string.Join(", ", base.ColumnNames.Select(new Func<string, string>(escape.EscapeString)));
				string[] array2 = array;
				array2[8] = "}])";
				Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder powerQueryMCodeBuilder = new Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder(string.Concat(array2), localizedStrings, escape);
				if (base.Skip > 0)
				{
					powerQueryMCodeBuilder.AddStep(MTableFunctionName.Skip, new string[] { base.Skip.ToString() }, null);
				}
				if (base.SkipFooter > 0)
				{
					powerQueryMCodeBuilder.AddStep(MTableFunctionName.RemoveLastN, new string[] { base.SkipFooter.ToString() }, null);
				}
				if (base.FilterEmptyLines)
				{
					powerQueryMCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each not List.IsEmpty(List.Select(Record.FieldValues(_), each _ <> null))" }, null);
				}
				if (base.CommentStr.HasValue)
				{
					powerQueryMCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each try not Text.StartsWith(Text.Trim(List.Skip(Record.FieldValues(_), each _ = null or _ = \"\"){0}), " + powerQueryMCodeBuilder.EscapeString(base.CommentStr.Value) + ") otherwise true" }, null);
				}
				return powerQueryMCodeBuilder.GetCode();
			}
			return null;
		}
	}
}
