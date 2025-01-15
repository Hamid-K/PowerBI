using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CAA RID: 3242
	[NullableContext(1)]
	[Nullable(0)]
	public class ContiguousList<TCell> : ICellFeature<TCell>, IPixelBounded, IBounded<PixelUnit> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x00106D73 File Offset: 0x00104F73
		public int Count
		{
			get
			{
				return this.Cells.Count;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x0600533C RID: 21308 RVA: 0x00106D80 File Offset: 0x00104F80
		public HashSet<TCell> Cells { get; }

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x00106D88 File Offset: 0x00104F88
		public Alignment<TCell> BaseAlignment { get; }

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x00106D90 File Offset: 0x00104F90
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<PixelUnit> PixelBounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x00106D98 File Offset: 0x00104F98
		[Nullable(new byte[] { 0, 1 })]
		Bounds<PixelUnit> IBounded<PixelUnit>.Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.PixelBounds;
			}
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x00106DA0 File Offset: 0x00104FA0
		public ContiguousList(IEnumerable<TCell> cells, Alignment<TCell> baseAlignment)
		{
			this.Cells = (cells as HashSet<TCell>) ?? cells.ConvertToHashSet<TCell>();
			this.PixelBounds = Bounds<PixelUnit>.Join(this.Cells.Select((TCell cell) => cell.PixelBounds));
			this.BaseAlignment = baseAlignment;
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x00106E08 File Offset: 0x00105008
		public override string ToString()
		{
			return string.Format("{{ BaseAlignment: {0}, Cells: {1} }}", this.BaseAlignment, StringUtils.BuildLimitedListLiteral<string>(this.Cells.Select((TCell cell) => cell.MinimalToString()), 5));
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x00106E55 File Offset: 0x00105055
		public string MinimalToString()
		{
			return string.Format("{{BaseAlignment:{0},CellsCount:{1}}}", this.BaseAlignment.Justification, this.Cells.Count);
		}
	}
}
