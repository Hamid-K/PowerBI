using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BCA RID: 3018
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1, 1 })]
	internal class TableBoundsFromIdentifierBoundsLoader : SimpleProgramLoader<TableBoundsFromIdentifierBoundsProgram, PdfRegion, PdfRegion>
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004C8F RID: 19599 RVA: 0x000F10C9 File Offset: 0x000EF2C9
		public static TableBoundsFromIdentifierBoundsLoader Instance { get; } = new TableBoundsFromIdentifierBoundsLoader();

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004C90 RID: 19600 RVA: 0x000F10D0 File Offset: 0x000EF2D0
		protected override Grammar Grammar
		{
			get
			{
				return Language.Grammar;
			}
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x000F10D7 File Offset: 0x000EF2D7
		public override TableBoundsFromIdentifierBoundsProgram Create(ProgramNode program)
		{
			return new TableBoundsFromIdentifierBoundsProgram(program, 0.0, null);
		}
	}
}
