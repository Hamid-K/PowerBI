using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FB5 RID: 4021
	public class FieldProgramLoader : SimpleSymbolProgramLoader<FieldProgram, WebRegion, string[]>
	{
		// Token: 0x06006F0A RID: 28426 RVA: 0x0016B400 File Offset: 0x00169600
		private FieldProgramLoader()
		{
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x0016B408 File Offset: 0x00169608
		public static FieldProgramLoader Instance { get; } = new FieldProgramLoader();

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06006F0C RID: 28428 RVA: 0x0016B40F File Offset: 0x0016960F
		protected override Symbol StartSymbol
		{
			get
			{
				return FieldProgram.ProgramSymbol;
			}
		}

		// Token: 0x06006F0D RID: 28429 RVA: 0x0016B3F8 File Offset: 0x001695F8
		public override FieldProgram Create(ProgramNode program)
		{
			return new FieldProgram(program);
		}
	}
}
