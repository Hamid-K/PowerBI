using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DD3 RID: 3539
	public class Loader : SimpleProgramLoader<Program, ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x0011D746 File Offset: 0x0011B946
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x0011D74D File Offset: 0x0011B94D
		public override Program Create(ProgramNode program)
		{
			return new Program(program, 0.0);
		}
	}
}
