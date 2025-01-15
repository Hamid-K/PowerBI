using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D27 RID: 3367
	[NullableContext(1)]
	public interface IProsePdfTable<TCell> : IPdfTable, ITableBounded, IBounded<TableUnit> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x0600564B RID: 22091
		[Nullable(new byte[] { 0, 2 })]
		RectangularArray<TCell> Table
		{
			[return: Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x0600564C RID: 22092
		IProsePdfTable<TCell> Section([Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> bounds);

		// Token: 0x0600564D RID: 22093
		IProsePdfTable<TCell> CollapsedSection([Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> bounds, SeparatorCollection separators, PdfAnalyzerOptions options, bool cleanTable = true, [Nullable(2)] AxisAligned<bool> combineAlongAxis = null);

		// Token: 0x0600564E RID: 22094
		[return: Nullable(new byte[] { 0, 0, 1 })]
		Optional<Bounds<PixelUnit>> CalculateApparentPixelBounds();

		// Token: 0x0600564F RID: 22095
		[return: Nullable(new byte[] { 0, 0, 1 })]
		Optional<Bounds<PixelUnit>> CalculateStablePixelBounds();

		// Token: 0x06005650 RID: 22096
		[return: Nullable(new byte[] { 0, 1 })]
		Optional<IApparentPixelBounded> CalculateApparentPixelBoundsWrapper();

		// Token: 0x06005651 RID: 22097
		[return: Nullable(new byte[] { 0, 1 })]
		Bounds<TableUnit>? FindTableBounds(IEnumerable<TCell> cells);

		// Token: 0x06005652 RID: 22098
		[return: Nullable(new byte[] { 0, 1 })]
		Bounds<TableUnit>? FindTableBounds([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> bounds);
	}
}
