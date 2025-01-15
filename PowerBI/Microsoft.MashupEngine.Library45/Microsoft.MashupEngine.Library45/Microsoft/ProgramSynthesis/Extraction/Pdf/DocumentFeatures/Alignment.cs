using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C4D RID: 3149
	[NullableContext(1)]
	[Nullable(0)]
	public class Alignment<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005143 RID: 20803 RVA: 0x000FF4D9 File Offset: 0x000FD6D9
		public IReadOnlyList<TCell> Cells { get; }

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x000FF4E1 File Offset: 0x000FD6E1
		public Justification Justification { get; }

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005145 RID: 20805 RVA: 0x000FF4E9 File Offset: 0x000FD6E9
		public Axis Axis { get; }

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x000FF4F1 File Offset: 0x000FD6F1
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> Range
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x000FF4F9 File Offset: 0x000FD6F9
		[Nullable(new byte[] { 0, 1 })]
		public Range<PixelUnit> FullRange
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x000FF504 File Offset: 0x000FD704
		public int Before
		{
			get
			{
				return this.Range.Min;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x000FF520 File Offset: 0x000FD720
		public int After
		{
			get
			{
				return this.Range.Max;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x000FF53B File Offset: 0x000FD73B
		public int Center { get; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600514B RID: 20811 RVA: 0x000FF543 File Offset: 0x000FD743
		public int BaseLine { get; }

		// Token: 0x0600514C RID: 20812 RVA: 0x000FF54C File Offset: 0x000FD74C
		public Alignment(IReadOnlyList<TCell> cells, Axis axis, Justification justification, int before, int center, int after, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> fullRange)
		{
			this.Cells = cells;
			this._cellSet = this.Cells.ConvertToHashSet<TCell>();
			this.Justification = justification;
			this.Axis = axis;
			this.Range = new Range<PixelUnit>(before, after);
			this.FullRange = fullRange;
			this.Center = center;
			switch (this.Justification)
			{
			case Justification.Before:
				this.BaseLine = this.Before;
				return;
			case Justification.After:
				this.BaseLine = this.After;
				return;
			case Justification.Center:
			case Justification.Unknown:
				this.BaseLine = this.Center;
				return;
			default:
				return;
			}
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x000FF5E8 File Offset: 0x000FD7E8
		public bool Contains(TCell cell)
		{
			return this._cellSet.Contains(cell);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x000FF5F8 File Offset: 0x000FD7F8
		public override string ToString()
		{
			return string.Format("{{ {0}: {1}, Cells: {2} }}", this.Justification, this.BaseLine, StringUtils.BuildLimitedListLiteral<string>(this.Cells.Select((TCell cell) => cell.MinimalToString()), 5));
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x000FF655 File Offset: 0x000FD855
		public string MinimalToString()
		{
			return string.Format("{{{0}:{1}}}", this.Justification, this.BaseLine);
		}

		// Token: 0x040023F7 RID: 9207
		private readonly HashSet<TCell> _cellSet;
	}
}
