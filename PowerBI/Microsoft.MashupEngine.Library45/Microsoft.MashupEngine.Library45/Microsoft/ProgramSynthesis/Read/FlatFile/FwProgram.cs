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
	// Token: 0x02001251 RID: 4689
	public class FwProgram : SimpleProgram
	{
		// Token: 0x06008D0E RID: 36110 RVA: 0x001DA066 File Offset: 0x001D8266
		public FwProgram(IReadOnlyList<string> columnNames, int skip, int skipFooter, IReadOnlyList<Record<int, int?>> fieldPositions, bool filterEmptyLines, Optional<string> commentStr, IReadOnlyList<string> rawColumnNames = null, int skipEmptyAndCommentCount = 0, bool hasEmptyLines = false, bool hasMultiLineRows = false, IEnumerable<string> newLineStrings = null)
			: this(FwProgram.BuildProgram(columnNames, skip, skipFooter, fieldPositions, filterEmptyLines, commentStr), rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrings)
		{
			this.FieldPositions = fieldPositions;
		}

		// Token: 0x06008D0F RID: 36111 RVA: 0x001DA090 File Offset: 0x001D8290
		internal FwProgram(Fw fwNode, IReadOnlyList<string> rawColumnNames = null, int skipEmptyAndCommentCount = 0, bool hasEmptyLines = false, bool hasMultiLineRows = false, IEnumerable<string> newLineStrings = null)
			: base(fwNode, fwNode.columnNames.Value, fwNode.skip.Value, fwNode.skipFooter.Value, fwNode.filterEmptyLines.Value, fwNode.commentStr.Value, rawColumnNames, skipEmptyAndCommentCount, hasEmptyLines, hasMultiLineRows, newLineStrings)
		{
			this.FieldPositions = fwNode.fieldPositions.Value;
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x06008D10 RID: 36112 RVA: 0x001DA113 File Offset: 0x001D8313
		public IReadOnlyList<Record<int, int?>> FieldPositions { get; }

		// Token: 0x06008D11 RID: 36113 RVA: 0x001DA11B File Offset: 0x001D831B
		protected override ITable<string> RunInternal(string input, bool trim)
		{
			return Semantics.Fw(input, base.ColumnNames, base.Skip, base.SkipFooter, this.FieldPositions, base.FilterEmptyLines, base.CommentStr, trim);
		}

		// Token: 0x06008D12 RID: 36114 RVA: 0x001DA148 File Offset: 0x001D8348
		protected override ITable<string> RunInternal(TextReader input, bool trim)
		{
			return Semantics.Fw(input, base.ColumnNames, base.Skip, base.SkipFooter, this.FieldPositions, base.FilterEmptyLines, base.CommentStr, trim);
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x001DA178 File Offset: 0x001D8378
		private static Fw BuildProgram(IReadOnlyList<string> columnNames, int skip, int skipFooter, IReadOnlyList<Record<int, int?>> fieldPositions, bool filterEmptyLines, Optional<string> commentStr)
		{
			return Program.Builder.Node.Rule.Fw(Program.Builder.Node.Variable.file, Program.Builder.Node.Rule.columnNames(columnNames), Program.Builder.Node.Rule.skip(skip), Program.Builder.Node.Rule.skipFooter(skipFooter), Program.Builder.Node.Rule.fieldPositions(fieldPositions), Program.Builder.Node.Rule.filterEmptyLines(filterEmptyLines), Program.Builder.Node.Rule.commentStr(commentStr)).Cast_Fw(Program.Builder);
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x001DA23C File Offset: 0x001D843C
		public override string GetMCode(string binaryContent, string encoding, Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escape)
		{
			Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder powerQueryMCodeBuilder = new Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder(binaryContent, encoding, localizedStrings, escape);
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
				powerQueryMCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each Text.Trim([Column1]) <> \"\"" }, null);
			}
			if (base.CommentStr.HasValue)
			{
				powerQueryMCodeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each not Text.StartsWith(Text.TrimStart([Column1]), " + powerQueryMCodeBuilder.EscapeString(base.CommentStr.Value) + "))" }, null);
			}
			if (this.FieldPositions[this.FieldPositions.Count - 1].Item2 == null)
			{
				if (this.FieldPositions.Windowed<Record<int, int?>>().All2(delegate(Record<int, int?> a, Record<int, int?> b)
				{
					int? item = a.Item2;
					int item2 = b.Item1;
					return (item.GetValueOrDefault() == item2) & (item != null);
				}))
				{
					Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder powerQueryMCodeBuilder2 = powerQueryMCodeBuilder;
					MSplitterFunctionName splitTextByPositions = MSplitterFunctionName.SplitTextByPositions;
					string[] array = new string[1];
					array[0] = "{" + string.Join<int>(", ", this.FieldPositions.Select((Record<int, int?> pos) => pos.Item1)) + "}";
					powerQueryMCodeBuilder2.AddSplitColumnStep(splitTextByPositions, array, 1, base.ColumnNames);
					goto IL_01E6;
				}
			}
			Microsoft.ProgramSynthesis.Read.FlatFile.Translation.PowerQueryM.PowerQueryMCodeBuilder powerQueryMCodeBuilder3 = powerQueryMCodeBuilder;
			MSplitterFunctionName splitTextByRanges = MSplitterFunctionName.SplitTextByRanges;
			string[] array2 = new string[1];
			array2[0] = "{" + string.Join(", ", this.FieldPositions.Select(delegate(Record<int, int?> pos)
			{
				string text = "{{{0}, {1}}}";
				object obj = pos.Item1;
				int? item3 = pos.Item2;
				int item4 = pos.Item1;
				int? num;
				int? num2;
				if (item3 == null)
				{
					num = null;
					num2 = num;
				}
				else
				{
					num2 = new int?(item3.GetValueOrDefault() - item4);
				}
				num = num2;
				return string.Format(text, obj, ((num != null) ? num.GetValueOrDefault().ToString() : null) ?? "null");
			})) + "}";
			powerQueryMCodeBuilder3.AddSplitColumnStep(splitTextByRanges, array2, 1, base.ColumnNames);
			IL_01E6:
			powerQueryMCodeBuilder.AddStep(MTableFunctionName.TransformColumns, new string[] { "{}", "Text.Trim" }, localizedStrings.TrimmedAllCells);
			return powerQueryMCodeBuilder.GetCode();
		}
	}
}
