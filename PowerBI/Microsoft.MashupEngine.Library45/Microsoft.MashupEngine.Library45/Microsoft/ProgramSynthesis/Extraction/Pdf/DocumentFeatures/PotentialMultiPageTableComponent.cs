using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D45 RID: 3397
	[NullableContext(1)]
	[Nullable(0)]
	internal class PotentialMultiPageTableComponent
	{
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060056D9 RID: 22233 RVA: 0x00112D30 File Offset: 0x00110F30
		public IProsePdfTable<ICell> TableComponent { get; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060056DA RID: 22234 RVA: 0x00112D38 File Offset: 0x00110F38
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060056DB RID: 22235 RVA: 0x00112D40 File Offset: 0x00110F40
		public int GapPixelsBelow { get; }

		// Token: 0x060056DC RID: 22236 RVA: 0x00112D48 File Offset: 0x00110F48
		public PotentialMultiPageTableComponent(IProsePdfTable<ICell> tableComponent, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, QuadTree<ICell, PixelUnit> cells)
		{
			this.TableComponent = tableComponent;
			this.PixelBounds = tableComponent.CalculateApparentPixelBounds().Value;
			Bounds<PixelUnit> bounds = new Bounds<PixelUnit>(this.PixelBounds.Horizontal, new Range<PixelUnit>(Math.Min(this.PixelBounds.Bottom + 1, pageBounds.Bottom), pageBounds.Bottom));
			int num = cells.OverlappingElements(bounds).MaybeMin((ICell cell) => cell.PixelBounds.Top).OrElse(pageBounds.Bottom);
			this.GapPixelsBelow = num - this.PixelBounds.Bottom;
		}
	}
}
