using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf
{
	// Token: 0x02000BCB RID: 3019
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	internal class TableBoundsFromIdentifierBoundsProgram : Program<PdfRegion, PdfRegion>
	{
		// Token: 0x06004C94 RID: 19604 RVA: 0x000F10FD File Offset: 0x000EF2FD
		public TableBoundsFromIdentifierBoundsProgram(ProgramNode programNode, double score, [Nullable(new byte[] { 2, 1, 1 })] Func<ProgramNode, ProgramNode> programNormalizingFunc = null)
			: base(programNode, score, programNormalizingFunc)
		{
		}

		// Token: 0x06004C95 RID: 19605 RVA: 0x000F1108 File Offset: 0x000EF308
		public override PdfRegion Run(PdfRegion input)
		{
			return (PdfRegion)base.ProgramNode.Invoke(State.CreateForExecution(base.ProgramNode.Grammar.InputSymbol, input));
		}
	}
}
