using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DE7 RID: 3559
	public class TitleProgram : Program<ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x06005A36 RID: 23094 RVA: 0x0011F735 File Offset: 0x0011D935
		public TitleProgram(ProgramNode programNode, double score, Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
			: base(programNode, score, programNormalizingFunc)
		{
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x0011DC83 File Offset: 0x0011BE83
		public override SpreadsheetArea Run(ISpreadsheetPair input)
		{
			return (SpreadsheetArea)base.ProgramNode.Invoke(State.CreateForExecution(Language.Grammar.InputSymbol, input));
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x0011F740 File Offset: 0x0011D940
		public static string ToTitleString(SpreadsheetArea area)
		{
			if (area == null)
			{
				return null;
			}
			return string.Join(" ", from row in area.SectionAsStrings.Rows()
				select string.Join(" ", row.Where((string s) => !string.IsNullOrWhiteSpace(s))));
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x0011F794 File Offset: 0x0011D994
		public string RunForString(ISpreadsheetPair input)
		{
			return TitleProgram.ToTitleString(this.Run(input));
		}
	}
}
