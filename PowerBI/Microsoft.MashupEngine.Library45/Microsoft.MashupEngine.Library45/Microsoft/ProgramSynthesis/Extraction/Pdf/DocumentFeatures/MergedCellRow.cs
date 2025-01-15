using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CED RID: 3309
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class MergedCellRow<TCell, [Nullable(0)] TMergedCell> where TCell : class, IWordAmalgamation<TCell> where TMergedCell : MergedCell<TCell>
	{
		// Token: 0x060054ED RID: 21741 RVA: 0x0010ABDE File Offset: 0x00108DDE
		protected MergedCellRow(IReadOnlyList<TMergedCell> mergedCells)
		{
			this.MergedCells = mergedCells;
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x0010ABED File Offset: 0x00108DED
		public override string ToString()
		{
			return string.Join<TMergedCell>(", ", this.MergedCells);
		}

		// Token: 0x04002670 RID: 9840
		public IReadOnlyList<TMergedCell> MergedCells;
	}
}
