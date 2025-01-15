using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CC0 RID: 3264
	[NullableContext(1)]
	[Nullable(0)]
	internal class Grid<TCell> : IPixelBounded, IBounded<PixelUnit>, ITableBounded, IBounded<TableUnit>, ICellFeature<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x0010898A File Offset: 0x00106B8A
		public IProsePdfTable<TCell> Table { get; }

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x00108992 File Offset: 0x00106B92
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<TableUnit> TableBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x0010899A File Offset: 0x00106B9A
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005405 RID: 21509 RVA: 0x001089A2 File Offset: 0x00106BA2
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x001089AA File Offset: 0x00106BAA
		[Nullable(new byte[] { 0, 1 })]
		Bounds<TableUnit> IBounded<TableUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.TableBounds;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005407 RID: 21511 RVA: 0x001089B2 File Offset: 0x00106BB2
		public HashSet<TCell> Cells { get; }

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x001089BA File Offset: 0x00106BBA
		public int Count
		{
			get
			{
				return this.Cells.Count;
			}
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x001089C8 File Offset: 0x00106BC8
		public Grid(IProsePdfTable<TCell> table, HashSet<TCell> cells)
		{
			this.Table = table;
			this.Cells = cells;
			Bounds<TableUnit>? bounds = this.Table.FindTableBounds(this.Cells);
			if (cells.Count == 0 || bounds == null)
			{
				throw new ArgumentException("Grid may not be empty.");
			}
			this.TableBounds = bounds.Value;
			this.PixelBounds = Bounds<PixelUnit>.Join(this.Cells.Select((TCell cell) => cell.PixelBounds));
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00108A59 File Offset: 0x00106C59
		public override string ToString()
		{
			return string.Format("{{ TableBounds: {0}, PixelBounds: {1}, Cell Count: {2} }}", this.TableBounds, this.PixelBounds, this.Cells.Count);
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00108A8B File Offset: 0x00106C8B
		public string MinimalToString()
		{
			return string.Format("{{TableBounds:{0}}}", this.TableBounds);
		}
	}
}
