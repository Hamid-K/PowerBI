using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C4F RID: 3151
	[NullableContext(1)]
	[Nullable(0)]
	internal class AlignmentBuilder<TCell> : ICellFeature<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x000FF690 File Offset: 0x000FD890
		public HashSet<TCell> Cells { get; }

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x000FF698 File Offset: 0x000FD898
		public int Count
		{
			get
			{
				return this.Cells.Count;
			}
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x000FF6A8 File Offset: 0x000FD8A8
		private AlignmentBuilder(Axis axis, TCell baseCell)
		{
			this._axis = axis;
			this.GetOrderLine = AlignmentBuilder<TCell>.GetBeforeFunc(axis.Perpendicular());
			this.GetBefore = AlignmentBuilder<TCell>.GetBeforeFunc(axis);
			this.GetAfter = AlignmentBuilder<TCell>.GetAfterFunc(axis);
			this.GetCenter = AlignmentBuilder<TCell>.GetCenterFunc(axis);
			this.Cells = new HashSet<TCell>();
			this.AddCell(baseCell);
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x000FF71C File Offset: 0x000FD91C
		public static AxisAlignedList<Alignment<TCell>> Build(ICollection<TCell> cells)
		{
			if (cells.Count == 0)
			{
				Logger.Instance.Debug("No cells for alignment recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\AlignmentBuilder.cs", 49);
				return AxisAlignedList<Alignment<TCell>>.Empty;
			}
			if (cells.Any((TCell cell) => cell.Alignments.Vertical.Any<Alignment<TCell>>() || cell.Alignments.Horizontal.Any<Alignment<TCell>>()))
			{
				throw new ArgumentException("cells", "Cells have already been built into alignments.");
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize Alignments", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\AlignmentBuilder.cs", 57);
			AxisAlignedList<Alignment<TCell>> axisAlignedList = new AxisAlignedList<Alignment<TCell>>(delegate(Axis axis)
			{
				List<AlignmentBuilder<TCell>> list = new List<AlignmentBuilder<TCell>>();
				foreach (KeyValuePair<Justification, Func<Axis, Func<TCell, int>>> keyValuePair in AlignmentBuilder<TCell>.NonUnknownJustificationExtractors)
				{
					Justification justification;
					Func<Axis, Func<TCell, int>> func;
					keyValuePair.Deconstruct(out justification, out func);
					Func<Axis, Func<TCell, int>> func2 = func;
					AlignmentBuilder<TCell>.<>c__DisplayClass16_1 CS$<>8__locals2 = new AlignmentBuilder<TCell>.<>c__DisplayClass16_1();
					CS$<>8__locals2.orderedCellGroups = (from g in cells.GroupBy(func2(axis))
						orderby g.Key
						select g).ToList<IGrouping<int, TCell>>();
					for (int i = 0; i < CS$<>8__locals2.orderedCellGroups.Count; i++)
					{
						AlignmentBuilder<TCell> alignmentBuilder = null;
						foreach (TCell tcell2 in CS$<>8__locals2.<Build>g__CellsInOrderAwayFrom|6(i))
						{
							if (alignmentBuilder == null)
							{
								alignmentBuilder = new AlignmentBuilder<TCell>(axis, tcell2);
							}
							else if (!alignmentBuilder.TryAddCell(tcell2))
							{
								break;
							}
						}
						if (alignmentBuilder != null)
						{
							list.Add(alignmentBuilder);
						}
					}
				}
				return (from builder in list.Where((AlignmentBuilder<TCell> builder) => builder.Count > 1).SortAndRemoveSubordinates(new CompareByCellCountDescending<TCell>(), (AlignmentBuilder<TCell> a, AlignmentBuilder<TCell> b) => b.IsCellSubsetOf(a))
					select builder.BuildAlignment()).ToList<Alignment<TCell>>();
			});
			foreach (Alignment<TCell> alignment in axisAlignedList)
			{
				foreach (TCell tcell in alignment.Cells)
				{
					tcell.AddToAlignment(alignment);
				}
			}
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("Alignments", axisAlignedList, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\AlignmentBuilder.cs", 128);
			return axisAlignedList;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x000FF864 File Offset: 0x000FDA64
		private bool TryAddCell(TCell cell)
		{
			if (this.Cells.Contains(cell))
			{
				return true;
			}
			int num = this.GetBefore(cell);
			int num2 = this.GetAfter(cell);
			bool flag = MathUtils.WithinTolerance(this._averageBefore, (double)num, this._averageCharSize / 2.0);
			bool flag2 = MathUtils.WithinTolerance(this._averageCenter, ((double)num + (double)num2) / 2.0, this._averageCharSize / 2.0);
			bool flag3 = MathUtils.WithinTolerance(this._averageAfter, (double)num2, this._averageCharSize / 2.0);
			if (!flag && !flag3 && !flag2)
			{
				return false;
			}
			if (this.Cells.Any((TCell existing) => cell.Overlaps(existing, this._axis) && !cell.Overlaps(existing, this._axis.Perpendicular())))
			{
				return true;
			}
			switch (this._justification)
			{
			case Justification.Before:
				if (!flag)
				{
					return false;
				}
				break;
			case Justification.After:
				if (!flag3)
				{
					return false;
				}
				break;
			case Justification.Center:
				if (!flag2)
				{
					return false;
				}
				break;
			case Justification.Unknown:
				if (flag && (!flag2 && !flag3))
				{
					this._justification = Justification.Before;
				}
				else if (flag3 && (!flag2 && !flag))
				{
					this._justification = Justification.After;
				}
				else if (flag2 && (!flag && !flag3))
				{
					this._justification = Justification.Center;
				}
				break;
			}
			this.AddCell(cell);
			return true;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x000FF9C4 File Offset: 0x000FDBC4
		private Alignment<TCell> BuildAlignment()
		{
			int num;
			int num2;
			int num3;
			switch (this._justification)
			{
			case Justification.Before:
				num = (int)Math.Round(this._averageBefore);
				num2 = this._maxAfter;
				num3 = (int)Math.Round((this._averageBefore + (double)this._maxAfter) / 2.0);
				break;
			case Justification.After:
				num = this._minBefore;
				num2 = (int)Math.Round(this._averageAfter);
				num3 = (int)Math.Round(((double)this._minBefore + this._averageAfter) / 2.0);
				break;
			case Justification.Center:
			case Justification.Unknown:
				num = this._minBefore;
				num2 = this._maxAfter;
				num3 = (int)Math.Round(this._averageCenter);
				break;
			default:
				throw new InvalidOperationException();
			}
			return new Alignment<TCell>(this.Cells.OrderBy(this.GetOrderLine).ToList<TCell>(), this._axis, this._justification, num, num3, num2, new Range<PixelUnit>(this._minBefore, this._maxAfter));
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x000FFABC File Offset: 0x000FDCBC
		private void AddCell(TCell cell)
		{
			int num = this.GetBefore(cell);
			int num2 = this.GetAfter(cell);
			int num3 = this.GetCenter(cell);
			foreach (IWord word2 in cell.Children.Where((IWord word) => !word.IsImage))
			{
				this._wordCount++;
				this._averageCharSize = MathUtils.UpdateAverage(this._averageCharSize, this._wordCount, word2.AverageGlyphWidth());
			}
			this.Cells.Add(cell);
			this._minBefore = Math.Min(num, this._minBefore);
			this._maxAfter = Math.Max(num2, this._maxAfter);
			switch (this._justification)
			{
			case Justification.Before:
				this._averageBefore = MathUtils.UpdateAverage(this._averageBefore, this.Count, (double)num);
				return;
			case Justification.After:
				this._averageAfter = MathUtils.UpdateAverage(this._averageAfter, this.Count, (double)num2);
				return;
			case Justification.Center:
			case Justification.Unknown:
				this._averageBefore = MathUtils.UpdateAverage(this._averageBefore, this.Count, (double)num);
				this._averageAfter = MathUtils.UpdateAverage(this._averageAfter, this.Count, (double)num2);
				this._averageCenter = MathUtils.UpdateAverage(this._averageCenter, this.Count, (double)num3);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x000FFC50 File Offset: 0x000FDE50
		private static Func<TCell, int> GetBeforeFunc(Axis axis)
		{
			Direction decreasingDirection = axis.Perpendicular().DecreasingDirection();
			return (TCell cell) => cell.PixelBounds.BoundInDirection(decreasingDirection);
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x000FFC73 File Offset: 0x000FDE73
		private static Func<TCell, int> GetAfterFunc(Axis axis)
		{
			Direction increasingDirection = axis.Perpendicular().IncreasingDirection();
			return (TCell cell) => cell.PixelBounds.BoundInDirection(increasingDirection);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x000FFC96 File Offset: 0x000FDE96
		private static Func<TCell, int> GetCenterFunc(Axis axis)
		{
			Axis perpendicularAxis = axis.Perpendicular();
			return (TCell cell) => (int)cell.PixelBounds.CenterAlongAxis(perpendicularAxis);
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600515D RID: 20829 RVA: 0x000FFCB4 File Offset: 0x000FDEB4
		private Func<TCell, int> GetOrderLine { get; }

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600515E RID: 20830 RVA: 0x000FFCBC File Offset: 0x000FDEBC
		private Func<TCell, int> GetBefore { get; }

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x000FFCC4 File Offset: 0x000FDEC4
		private Func<TCell, int> GetAfter { get; }

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x000FFCCC File Offset: 0x000FDECC
		private Func<TCell, int> GetCenter { get; }

		// Token: 0x06005161 RID: 20833 RVA: 0x000FFCD4 File Offset: 0x000FDED4
		// Note: this type is marked as 'beforefieldinit'.
		static AlignmentBuilder()
		{
			Dictionary<Justification, Func<Axis, Func<TCell, int>>> dictionary = new Dictionary<Justification, Func<Axis, Func<TCell, int>>>();
			dictionary[Justification.Before] = new Func<Axis, Func<TCell, int>>(AlignmentBuilder<TCell>.GetBeforeFunc);
			dictionary[Justification.Center] = new Func<Axis, Func<TCell, int>>(AlignmentBuilder<TCell>.GetCenterFunc);
			dictionary[Justification.After] = new Func<Axis, Func<TCell, int>>(AlignmentBuilder<TCell>.GetAfterFunc);
			AlignmentBuilder<TCell>.NonUnknownJustificationExtractors = dictionary;
		}

		// Token: 0x040023FB RID: 9211
		private readonly Axis _axis;

		// Token: 0x040023FC RID: 9212
		private double _averageAfter;

		// Token: 0x040023FD RID: 9213
		private double _averageBefore;

		// Token: 0x040023FE RID: 9214
		private double _averageCenter;

		// Token: 0x040023FF RID: 9215
		private double _averageCharSize;

		// Token: 0x04002400 RID: 9216
		private Justification _justification = Justification.Unknown;

		// Token: 0x04002401 RID: 9217
		private int _maxAfter;

		// Token: 0x04002402 RID: 9218
		private int _minBefore = int.MaxValue;

		// Token: 0x04002403 RID: 9219
		private int _wordCount;

		// Token: 0x04002404 RID: 9220
		private static readonly IReadOnlyDictionary<Justification, Func<Axis, Func<TCell, int>>> NonUnknownJustificationExtractors;
	}
}
