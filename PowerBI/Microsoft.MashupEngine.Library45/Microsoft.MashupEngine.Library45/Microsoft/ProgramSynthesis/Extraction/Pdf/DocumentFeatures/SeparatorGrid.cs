using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D63 RID: 3427
	[NullableContext(1)]
	[Nullable(0)]
	public class SeparatorGrid : IPixelBounded, IBounded<PixelUnit>
	{
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005785 RID: 22405 RVA: 0x001161F7 File Offset: 0x001143F7
		public SeparatorGroup SeparatorGroup { get; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005786 RID: 22406 RVA: 0x001161FF File Offset: 0x001143FF
		[Nullable(new byte[] { 0, 1 })]
		public RectangularArray<SeparatorGrid.SeparatorGridCell> Cells
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005787 RID: 22407 RVA: 0x00116208 File Offset: 0x00114408
		public IEnumerable<SeparatorGrid.SeparatorGridCell> CellsEnumerable
		{
			get
			{
				return this.Cells.ToEnumerable().Distinct<SeparatorGrid.SeparatorGridCell>();
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005788 RID: 22408 RVA: 0x00116228 File Offset: 0x00114428
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005789 RID: 22409 RVA: 0x00116230 File Offset: 0x00114430
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x0600578A RID: 22410 RVA: 0x00116238 File Offset: 0x00114438
		public int LineWidth { get; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x0600578B RID: 22411 RVA: 0x00116240 File Offset: 0x00114440
		public Directed<bool> HasEdgeSeparators { get; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600578C RID: 22412 RVA: 0x00116248 File Offset: 0x00114448
		public Color? Color
		{
			get
			{
				return this.SeparatorGroup.StrokingColor;
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600578D RID: 22413 RVA: 0x00116255 File Offset: 0x00114455
		// (set) Token: 0x0600578E RID: 22414 RVA: 0x0011625D File Offset: 0x0011445D
		[Nullable(new byte[] { 2, 1, 1 })]
		internal MultiValueDictionary<ITextRun, SeparatorGrid.SeparatorGridCell> CellsByTextRun
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			private set;
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x00116268 File Offset: 0x00114468
		internal void InitializeCellsByTextRun(QuadTree<ITextRun, PixelUnit> textRuns)
		{
			if (this.CellsByTextRun != null)
			{
				return;
			}
			MultiValueDictionary<ITextRun, SeparatorGrid.SeparatorGridCell> multiValueDictionary = new MultiValueDictionary<ITextRun, SeparatorGrid.SeparatorGridCell>();
			foreach (SeparatorGrid.SeparatorGridCell separatorGridCell in this.CellsEnumerable)
			{
				foreach (ITextRun textRun in textRuns.OverlappingElements(separatorGridCell.PixelBounds))
				{
					multiValueDictionary.Add(textRun, separatorGridCell);
				}
			}
			this.CellsByTextRun = multiValueDictionary;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x0011630C File Offset: 0x0011450C
		public SeparatorGrid(SeparatorGroup separatorGroup, [Nullable(new byte[] { 0, 1 })] RectangularArray<SeparatorGrid.SeparatorGridCell> cells, int lineWidth, Directed<bool> hasEdgeSeparators)
		{
			this.SeparatorGroup = separatorGroup;
			this.Cells = cells;
			this.PixelBounds = separatorGroup.PixelBounds;
			this.LineWidth = lineWidth;
			this.HasEdgeSeparators = hasEdgeSeparators;
		}

		// Token: 0x02000D64 RID: 3428
		[Nullable(0)]
		public class SeparatorGridCell : IPixelBounded, IBounded<PixelUnit>
		{
			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x06005791 RID: 22417 RVA: 0x0011633D File Offset: 0x0011453D
			[Nullable(new byte[] { 0, 1 })]
			Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.PixelBounds;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x06005792 RID: 22418 RVA: 0x00116345 File Offset: 0x00114545
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<PixelUnit> PixelBounds
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this.ShadedBounds.PixelBounds;
				}
			}

			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x06005793 RID: 22419 RVA: 0x00116352 File Offset: 0x00114552
			public ShadedBounds ShadedBounds { get; }

			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x06005794 RID: 22420 RVA: 0x0011635A File Offset: 0x0011455A
			[Nullable(new byte[] { 0, 1 })]
			public Bounds<TableUnit> Span
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x06005795 RID: 22421 RVA: 0x00116362 File Offset: 0x00114562
			public SeparatorGridCell(ShadedBounds shadedBounds, [Nullable(new byte[] { 0, 1 })] Bounds<TableUnit> span)
			{
				this.ShadedBounds = shadedBounds;
				this.Span = span;
			}

			// Token: 0x06005796 RID: 22422 RVA: 0x00116378 File Offset: 0x00114578
			[return: Nullable(2)]
			internal ICell AsICell(QuadTree<ICell, PixelUnit> cells, out IReadOnlyList<ICell> mergedCells)
			{
				mergedCells = cells.OverlappingElements(this.PixelBounds).ToList<ICell>();
				if (!mergedCells.Any<ICell>())
				{
					return null;
				}
				return CellBuilder.BuildCellFromUnsortedCellGroup(mergedCells).WithBounds(this.PixelBounds, true);
			}
		}
	}
}
