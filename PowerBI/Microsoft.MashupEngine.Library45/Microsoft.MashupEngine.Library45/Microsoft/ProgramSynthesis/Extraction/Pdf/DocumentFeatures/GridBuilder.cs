using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CC2 RID: 3266
	[NullableContext(1)]
	[Nullable(0)]
	internal class GridBuilder<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x0600540F RID: 21519 RVA: 0x00108AB0 File Offset: 0x00106CB0
		public static IReadOnlyList<Grid<TCell>> RecognizeGrids(PdfAnalyzerOptions options, SeparatorCollection separatorCollection, NonConflictingRegionCollection<TCell> nonConflictingRegions, QuadTree<TCell, PixelUnit> cells, IProsePdfTable<TCell> fullTable)
		{
			List<HashSet<TCell>> list = new List<HashSet<TCell>>();
			foreach (BoundsWrapper<PixelUnit> boundsWrapper in nonConflictingRegions.Regions)
			{
				List<TCell> list2 = cells.ContainedElements(boundsWrapper.Bounds).ToList<TCell>();
				if (list2.Any<TCell>())
				{
					list.AddRange(GridBuilder<TCell>.SplitGridsOnGaps(list2, options, separatorCollection));
				}
			}
			List<Grid<TCell>> list3 = new List<Grid<TCell>>();
			foreach (HashSet<TCell> hashSet in list.SortAndRemoveSubordinates(Comparer<HashSet<TCell>>.Create((HashSet<TCell> a, HashSet<TCell> b) => a.Count - b.Count), (HashSet<TCell> a, HashSet<TCell> b) => b.IsSubsetOf(a)))
			{
				GridBuilder<TCell>.FilterOutUnalignedCells(hashSet);
				if (GridBuilder<TCell>.GridLargeEnough(hashSet))
				{
					if (hashSet.Any((TCell cell) => cell.Children.Any<IWord>()))
					{
						list3.Add(new Grid<TCell>(fullTable, hashSet));
					}
				}
			}
			return list3;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00108BF4 File Offset: 0x00106DF4
		private static IEnumerable<HashSet<TCell>> SplitGridsOnGaps(IReadOnlyList<TCell> cells, PdfAnalyzerOptions options, SeparatorCollection separatorCollection)
		{
			Optional<int> optional = (from t in cells.SelectMany((TCell c) => c.TextRuns)
				where !t.IsRotated
				select t).MaybeMax((ITextRun textRun) => textRun.ApparentPixelBounds.Height());
			if (!optional.HasValue)
			{
				yield return new HashSet<TCell>(cells);
				yield break;
			}
			int value = optional.Value;
			int splitHeight = (int)Math.Ceiling((double)value * 5.875);
			bool enableSeparatedSplit = options.Version >= PdfAnalyzerVersion.V1_2;
			int separatedSplitHeight = (enableSeparatedSplit ? ((int)Math.Ceiling((double)value * 3.0)) : int.MaxValue);
			HashSet<TCell> hashSet = new HashSet<TCell>();
			int? num = null;
			TCell currentBottomCell = default(TCell);
			foreach (TCell cell in cells.OrderByClosest(Direction.Down))
			{
				if (num != null && currentBottomCell != null)
				{
					int? num2 = cell.PixelBounds.Top - num;
					int num3 = splitHeight;
					if (!((num2.GetValueOrDefault() >= num3) & (num2 != null)))
					{
						if (!enableSeparatedSplit)
						{
							goto IL_02FD;
						}
						num2 = cell.PixelBounds.Top - num;
						num3 = separatedSplitHeight;
						if (!((num2.GetValueOrDefault() >= num3) & (num2 != null)) || !separatorCollection.AnySeparates(Axis.Horizontal, cell, currentBottomCell) || separatorCollection.GetBackgroundColor(cell).ColorEquals(separatorCollection.GetBackgroundColor(currentBottomCell)))
						{
							goto IL_02FD;
						}
					}
					yield return hashSet;
					hashSet = new HashSet<TCell>();
					num = null;
				}
				IL_02FD:
				hashSet.Add(cell);
				if (num == null || cell.PixelBounds.Bottom > num.Value)
				{
					num = new int?(cell.PixelBounds.Bottom);
					currentBottomCell = cell;
				}
				cell = default(TCell);
			}
			IEnumerator<TCell> enumerator = null;
			if (hashSet.Any<TCell>())
			{
				yield return hashSet;
			}
			yield break;
			yield break;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00108C14 File Offset: 0x00106E14
		private static void FilterOutUnalignedCells(HashSet<TCell> gridCells)
		{
			MultiValueDictionary<Alignment<TCell>, TCell> alignedCellsLookup = MultiValueDictionary<Alignment<TCell>, TCell>.Create<HashSet<TCell>>();
			HashSet<Alignment<TCell>> hashSet = new HashSet<Alignment<TCell>>();
			using (HashSet<TCell>.Enumerator enumerator = gridCells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TCell tcell = enumerator.Current;
					foreach (Alignment<TCell> alignment4 in tcell.Alignments)
					{
						alignedCellsLookup.Add(alignment4, tcell);
						int count = alignedCellsLookup[alignment4].Count;
						if (count == 1)
						{
							hashSet.Add(alignment4);
						}
						else if (count > 1)
						{
							hashSet.Remove(alignment4);
						}
					}
				}
				goto IL_01CF;
			}
			IL_00BB:
			Alignment<TCell> alignment2 = hashSet.First<Alignment<TCell>>();
			hashSet.Remove(alignment2);
			TCell tcell2 = alignedCellsLookup[alignment2].Single<TCell>();
			IEnumerable<Alignment<TCell>> horizontal = tcell2.Alignments.Horizontal;
			Func<Alignment<TCell>, bool> <>9__0;
			Func<Alignment<TCell>, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (Alignment<TCell> alignment) => alignedCellsLookup[alignment].Count > 1);
			}
			if (horizontal.Any(func))
			{
				IEnumerable<Alignment<TCell>> vertical = tcell2.Alignments.Vertical;
				Func<Alignment<TCell>, bool> <>9__1;
				Func<Alignment<TCell>, bool> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (Alignment<TCell> alignment) => alignedCellsLookup[alignment].Count > 1);
				}
				if (vertical.Any(func2))
				{
					goto IL_01CF;
				}
			}
			gridCells.Remove(tcell2);
			foreach (Alignment<TCell> alignment3 in tcell2.Alignments)
			{
				alignedCellsLookup.Remove(alignment3, tcell2);
				IReadOnlyCollection<TCell> readOnlyCollection;
				if (!alignedCellsLookup.TryGetValue(alignment3, out readOnlyCollection))
				{
					hashSet.Remove(alignment3);
				}
				else if (readOnlyCollection.Count == 1)
				{
					hashSet.Add(alignment3);
				}
			}
			IL_01CF:
			if (!hashSet.Any<Alignment<TCell>>())
			{
				return;
			}
			goto IL_00BB;
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x00108E24 File Offset: 0x00107024
		private static bool GridLargeEnough(HashSet<TCell> gridCells)
		{
			if (gridCells.IsEmpty<TCell>())
			{
				return false;
			}
			using (IEnumerator<Axis> enumerator = AxisUtilities.Axes.GetEnumerator())
			{
				Func<ContiguousList<TCell>, bool> <>9__1;
				while (enumerator.MoveNext())
				{
					Axis axis = enumerator.Current;
					IEnumerable<ContiguousList<TCell>> enumerable = gridCells.SelectMany((TCell cell) => cell.ContiguousLists[axis]).ConvertToHashSet<ContiguousList<TCell>>();
					Func<ContiguousList<TCell>, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ContiguousList<TCell> contiguousList) => contiguousList.Cells.Intersect(gridCells).Count<TCell>() >= 2);
					}
					if (!enumerable.Any(func))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x040025E6 RID: 9702
		private const double VerticalGapLimitInRows = 5.875;

		// Token: 0x040025E7 RID: 9703
		private const double SeparatedVerticalGapLimitInRows = 3.0;
	}
}
