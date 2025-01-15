using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Utils.Logging;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000C6A RID: 3178
	internal static class BoundedListBuilder
	{
		// Token: 0x060051DB RID: 20955 RVA: 0x00101AA8 File Offset: 0x000FFCA8
		[NullableContext(1)]
		public static AxisAlignedList<IBoundedList<TCell>> Build<TCell>(PdfAnalyzerOptions options, SeparatorCollection separators, QuadTree<TCell, PixelUnit> cells, AxisAlignedList<Alignment<TCell>> alignments, AxisAlignedList<ContiguousList<TCell>> contiguousLists) where TCell : class, IWordAmalgamation<TCell>
		{
			BoundedListBuilder.<>c__DisplayClass0_0<TCell> CS$<>8__locals1 = new BoundedListBuilder.<>c__DisplayClass0_0<TCell>();
			CS$<>8__locals1.alignments = alignments;
			CS$<>8__locals1.contiguousLists = contiguousLists;
			CS$<>8__locals1.separators = separators;
			CS$<>8__locals1.cells = cells;
			if (CS$<>8__locals1.cells.IsEmpty())
			{
				Logger.Instance.Debug("No cells for bounded list recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\BoundedListBuilder.cs", 25);
				return AxisAlignedList<IBoundedList<TCell>>.Empty;
			}
			if (CS$<>8__locals1.cells.Any((TCell cell) => cell.BoundedLists.Vertical.Any<IBoundedList<TCell>>() || cell.BoundedLists.Horizontal.Any<IBoundedList<TCell>>()))
			{
				throw new ArgumentException("These cells have already been built into bounded lists.", "cells");
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Recognize Bounded Lists", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\BoundedListBuilder.cs", 33);
			CS$<>8__locals1.useAlignmentFullRange = options.Version >= PdfAnalyzerVersion.V1_1;
			CS$<>8__locals1.useContiguousLists = options.Version >= PdfAnalyzerVersion.V1_2;
			AxisAlignedList<IBoundedList<TCell>> axisAlignedList = new AxisAlignedList<IBoundedList<TCell>>(delegate(Axis axis)
			{
				BoundedListBuilder.<>c__DisplayClass0_1<TCell> CS$<>8__locals2 = new BoundedListBuilder.<>c__DisplayClass0_1<TCell>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.axis = axis;
				CS$<>8__locals2.perpendicularAxis = CS$<>8__locals2.axis.Perpendicular();
				CS$<>8__locals2.dividers = new SortedSet<int>();
				Alignment<TCell> alignment;
				using (IEnumerator<Alignment<TCell>> enumerator3 = CS$<>8__locals1.alignments[CS$<>8__locals2.axis].GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						alignment = enumerator3.Current;
						Range<PixelUnit> range3 = (CS$<>8__locals1.useAlignmentFullRange ? alignment.FullRange : alignment.Range);
						CS$<>8__locals2.dividers.Add(range3.Min);
						CS$<>8__locals2.dividers.Add(range3.Max);
					}
				}
				if (CS$<>8__locals1.useContiguousLists)
				{
					foreach (ContiguousList<TCell> contiguousList in CS$<>8__locals1.contiguousLists[CS$<>8__locals2.axis])
					{
						Range<PixelUnit> range2 = contiguousList.PixelBounds[CS$<>8__locals2.perpendicularAxis];
						CS$<>8__locals2.dividers.Add(range2.Min);
						CS$<>8__locals2.dividers.Add(range2.Max);
					}
				}
				CS$<>8__locals2.dividers.AddRange(CS$<>8__locals1.separators.Separators[CS$<>8__locals2.axis].Select((Separator sep) => sep.Line.Position));
				IEnumerable<TCell> cells2 = CS$<>8__locals1.cells;
				Func<TCell, bool> func;
				if ((func = CS$<>8__locals2.<>9__8) == null)
				{
					func = (CS$<>8__locals2.<>9__8 = (TCell cell) => cell.Alignments[CS$<>8__locals2.axis].Where((Alignment<TCell> alignment) => alignment.Range.Overlaps(cell.PixelBounds.RangeAlongAxis(CS$<>8__locals2.perpendicularAxis))).IsEmpty<Alignment<TCell>>());
				}
				foreach (TCell tcell2 in cells2.Where(func))
				{
					foreach (Direction direction in CS$<>8__locals2.perpendicularAxis.AlignedDirections())
					{
						CS$<>8__locals2.dividers.Add(tcell2.PixelBounds.BoundInDirection(direction));
					}
				}
				CS$<>8__locals2.singlePixelCellPositions = (from c in CS$<>8__locals1.cells
					where c.PixelBounds[CS$<>8__locals2.perpendicularAxis].Size() == 1
					select c.PixelBounds[CS$<>8__locals2.perpendicularAxis.DecreasingDirection()]).ConvertToHashSet<int>();
				return CS$<>8__locals2.<Build>g__BoundedListRanges|5().Select(delegate(Range<PixelUnit> range)
				{
					IEnumerable<TCell> enumerable = CS$<>8__locals2.CS$<>8__locals1.cells.OverlappingElements(range, CS$<>8__locals2.perpendicularAxis);
					Func<TCell, bool> func2;
					if ((func2 = CS$<>8__locals2.<>9__11) == null)
					{
						func2 = (CS$<>8__locals2.<>9__11 = (TCell cell) => CS$<>8__locals2.axis == Axis.Horizontal || cell.BeforeAlignmentDotRow == null);
					}
					return from listCells in enumerable.GroupBy(func2)
						where listCells.Any<TCell>()
						select new BoundedList<TCell>(CS$<>8__locals2.axis, listCells.ToList<TCell>(), range);
				}).SelectMany((IEnumerable<BoundedList<TCell>> b) => b)
					.ToList<BoundedList<TCell>>();
			});
			foreach (IBoundedList<TCell> boundedList in axisAlignedList)
			{
				foreach (TCell tcell in boundedList.Cells)
				{
					tcell.AddToBoundedList(boundedList);
				}
			}
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("Bounded Lists", axisAlignedList, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\BoundedListBuilder.cs", 143);
			return axisAlignedList;
		}
	}
}
