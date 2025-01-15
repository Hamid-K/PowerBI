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
	// Token: 0x02000C9F RID: 3231
	[NullableContext(1)]
	[Nullable(0)]
	internal class ConflictCollection<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06005305 RID: 21253 RVA: 0x00105DB2 File Offset: 0x00103FB2
		public IReadOnlyList<Conflict> Conflicts { get; }

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005306 RID: 21254 RVA: 0x00105DBA File Offset: 0x00103FBA
		public SortedList<int, List<ConflictBoundary>> ConflictBoundaries { get; }

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x00105DC2 File Offset: 0x00103FC2
		public QuadTree<Conflict, PixelUnit> ConflictsTree
		{
			get
			{
				return this._lazyConflictsTree.Value;
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x00105DCF File Offset: 0x00103FCF
		internal ConflictCollection(IReadOnlyList<Conflict> conflicts)
		{
			this.Conflicts = conflicts;
			this.ConflictBoundaries = ConflictCollection<TCell>.SortConflictBoundaries(this.Conflicts);
			this._lazyConflictsTree = new Lazy<QuadTree<Conflict, PixelUnit>>(delegate
			{
				if (this.Conflicts.Count != 0)
				{
					return new QuadTree<Conflict, PixelUnit>(this.Conflicts);
				}
				return new QuadTree<Conflict, PixelUnit>(Bounds<PixelUnit>.Zero);
			});
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x00105E08 File Offset: 0x00104008
		[NullableContext(2)]
		public Conflict DoesConflict([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> region)
		{
			foreach (Conflict conflict in this.ConflictsTree.OverlappingElements(region))
			{
				if (conflict.IsExclusive[Axis.Vertical])
				{
					if (conflict.IsExclusive[Axis.Horizontal])
					{
						if (region.Overlaps(conflict.Bounds))
						{
							return conflict;
						}
					}
					else if (region.Vertical.Overlaps(conflict.Bounds.Vertical) && region.Horizontal.Contains(conflict.Bounds.Horizontal))
					{
						return conflict;
					}
				}
				else if (region.Horizontal.Overlaps(conflict.Bounds.Horizontal) && region.Vertical.Contains(conflict.Bounds.Vertical))
				{
					return conflict;
				}
			}
			return null;
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x00105F1C File Offset: 0x0010411C
		public static ConflictCollection<TCell> Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, QuadTree<TCell, PixelUnit> cells, AxisAlignedList<ContiguousList<TCell>> contiguousLists, Axis? requiredCellContiguousListAxis = null)
		{
			if (cells.IsEmpty())
			{
				Logger.Instance.Debug("No cells for contiguous list recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ConflictCollection.cs", 64);
				return ConflictCollection<TCell>.Empty;
			}
			if (contiguousLists.IsEmpty())
			{
				Logger.Instance.Debug("No alignments for contiguous list recognition", null, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ConflictCollection.cs", 70);
				return ConflictCollection<TCell>.Empty;
			}
			IStopwatchWrapper stopwatchWrapper = Logger.Instance.InfoTiming("Find Conflicts", "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ConflictCollection.cs", 74);
			QuadTree<Conflict, PixelUnit> quadTree = new QuadTree<Conflict, PixelUnit>(pageBounds);
			quadTree.AddRange(ConflictCollection<TCell>.FindCellConflicts(cells, requiredCellContiguousListAxis));
			quadTree.AddRange(ConflictCollection<TCell>.FindContiguousListConflicts(contiguousLists));
			ConflictCollection<TCell>.RemoveSubsetConflicts(quadTree);
			List<Conflict> list = quadTree.ToList<Conflict>();
			ConflictCollection<TCell> conflictCollection = new ConflictCollection<TCell>(list);
			stopwatchWrapper.Stop();
			Logger.Instance.Debug("Conflicts", list, "C:\\_work\\1\\s\\Extraction.Pdf\\Semantics\\DocumentFeatures\\ConflictCollection.cs", 83);
			return conflictCollection;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x00105FD8 File Offset: 0x001041D8
		private static IEnumerable<Conflict> FindCellConflicts(IEnumerable<TCell> cells, Axis? requiredAxis)
		{
			if (requiredAxis != null)
			{
				Axis axis = requiredAxis.GetValueOrDefault();
				Axis perpendicular = requiredAxis.Value.Perpendicular();
				foreach (TCell tcell in cells)
				{
					if (tcell.ContiguousLists[axis].IsEmpty<ContiguousList<TCell>>() || (tcell.ContiguousLists[perpendicular].IsEmpty<ContiguousList<TCell>>() && tcell.TextRuns[0].IsRotated))
					{
						yield return new Conflict(tcell.Bounds);
					}
				}
				IEnumerator<TCell> enumerator = null;
			}
			else
			{
				foreach (TCell tcell2 in cells)
				{
					if (tcell2.ContiguousLists.AsEnumerable.Any((HashSet<ContiguousList<TCell>> list) => list.IsEmpty<ContiguousList<TCell>>()))
					{
						yield return new Conflict(tcell2.Bounds);
					}
				}
				IEnumerator<TCell> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x00105FEF File Offset: 0x001041EF
		private static IEnumerable<Conflict> FindContiguousListConflicts(AxisAlignedList<ContiguousList<TCell>> contiguousLists)
		{
			IEnumerable<Record<ContiguousList<TCell>, ContiguousList<TCell>>> enumerable = contiguousLists.Vertical.UnorderedPairs(false);
			IEnumerable<Record<ContiguousList<TCell>, ContiguousList<TCell>>> enumerable2 = contiguousLists.Horizontal.UnorderedPairs(false);
			foreach (Record<ContiguousList<TCell>, ContiguousList<TCell>> record in enumerable.Concat(enumerable2))
			{
				ContiguousList<TCell> contiguousList;
				ContiguousList<TCell> contiguousList2;
				record.Deconstruct(out contiguousList, out contiguousList2);
				ContiguousList<TCell> contiguousList3 = contiguousList;
				ContiguousList<TCell> contiguousList4 = contiguousList2;
				Axis axis = contiguousList3.BaseAlignment.Axis;
				Axis perpendicular = axis.Perpendicular();
				Optional<Range<PixelUnit>> perpendicularOverlap = contiguousList3.PixelBounds[perpendicular].Intersect(contiguousList4.PixelBounds[perpendicular]);
				if (perpendicularOverlap.HasValue)
				{
					Range<PixelUnit> range = contiguousList3.PixelBounds[axis];
					Range<PixelUnit> range2 = contiguousList4.PixelBounds[axis];
					if (contiguousList3.PixelBounds[axis].Intersect(contiguousList4.PixelBounds[axis]).HasValue)
					{
						if (!contiguousList3.Cells.Overlaps(contiguousList4.Cells))
						{
							IEnumerable<Record<TCell, ContiguousList<TCell>>> enumerable3 = new ContiguousList<TCell>[] { contiguousList3, contiguousList4 }.SelectMany((ContiguousList<TCell> list) => list.Cells.Select((TCell cell) => Record.Create<TCell, ContiguousList<TCell>>(cell, list)));
							Func<Record<TCell, ContiguousList<TCell>>, bool> func;
							Func<Record<TCell, ContiguousList<TCell>>, bool> <>9__1;
							if ((func = <>9__1) == null)
							{
								func = (<>9__1 = (Record<TCell, ContiguousList<TCell>> pair) => pair.Item1.Overlaps(perpendicularOverlap.Value, perpendicular));
							}
							IEnumerable<Record<TCell, ContiguousList<TCell>>> enumerable4 = enumerable3.Where(func);
							Func<Record<TCell, ContiguousList<TCell>>, int> func2;
							Func<Record<TCell, ContiguousList<TCell>>, int> <>9__2;
							if ((func2 = <>9__2) == null)
							{
								func2 = (<>9__2 = (Record<TCell, ContiguousList<TCell>> pair) => pair.Item1.Bounds[axis].Min);
							}
							IEnumerable<Record<TCell, ContiguousList<TCell>>> enumerable5 = enumerable4.OrderBy(func2);
							Func<Record<TCell, ContiguousList<TCell>>, Record<TCell, ContiguousList<TCell>>, Optional<Bounds<PixelUnit>>> func3;
							Func<Record<TCell, ContiguousList<TCell>>, Record<TCell, ContiguousList<TCell>>, Optional<Bounds<PixelUnit>>> <>9__3;
							if ((func3 = <>9__3) == null)
							{
								func3 = (<>9__3 = delegate(Record<TCell, ContiguousList<TCell>> firstCell, Record<TCell, ContiguousList<TCell>> secondCell)
								{
									if (firstCell.Item2 == secondCell.Item2)
									{
										return Optional<Bounds<PixelUnit>>.Nothing;
									}
									return firstCell.Item1.Bounds[axis].BetweenInclusive(secondCell.Item1.Bounds[axis]).Select(delegate(Range<PixelUnit> alignedRange)
									{
										Range<PixelUnit> firstPerpendicular = firstCell.Item1.Bounds[perpendicular];
										Range<PixelUnit> secondPerpendicular = secondCell.Item1.Bounds[perpendicular];
										Dictionary<Axis, Range<PixelUnit>> dictionary2 = new Dictionary<Axis, Range<PixelUnit>>();
										Axis axis2 = axis;
										dictionary2[axis2] = alignedRange;
										Axis perpendicular2 = perpendicular;
										dictionary2[perpendicular2] = firstPerpendicular.Intersect(secondPerpendicular).OrElseCompute(() => firstPerpendicular.BetweenInclusive(secondPerpendicular).Value);
										return new Bounds<PixelUnit>(dictionary2);
									});
								});
							}
							IEnumerable<Bounds<PixelUnit>> enumerable6 = enumerable5.Windowed(func3).SelectValues<Bounds<PixelUnit>>();
							Func<Bounds<PixelUnit>, Conflict> func4;
							Func<Bounds<PixelUnit>, Conflict> <>9__4;
							if ((func4 = <>9__4) == null)
							{
								func4 = (<>9__4 = (Bounds<PixelUnit> bounds) => new Conflict(bounds, perpendicular));
							}
							foreach (Conflict conflict in enumerable6.Select(func4))
							{
								yield return conflict;
							}
							IEnumerator<Conflict> enumerator2 = null;
						}
					}
					else
					{
						Dictionary<Axis, Range<PixelUnit>> dictionary = new Dictionary<Axis, Range<PixelUnit>>();
						Axis axis3 = axis;
						dictionary[axis3] = range.BetweenInclusive(range2).Value;
						Axis perpendicular3 = perpendicular;
						dictionary[perpendicular3] = contiguousList3.PixelBounds[perpendicular].Intersect(contiguousList4.PixelBounds[perpendicular]).Value;
						Bounds<PixelUnit> bounds2 = new Bounds<PixelUnit>(dictionary);
						yield return new Conflict(bounds2, perpendicular);
					}
				}
			}
			IEnumerator<Record<ContiguousList<TCell>, ContiguousList<TCell>>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x00106000 File Offset: 0x00104200
		private static void RemoveSubsetConflicts(QuadTree<Conflict, PixelUnit> conflicts)
		{
			ConflictCollection<TCell>.<>c__DisplayClass15_0 CS$<>8__locals1;
			CS$<>8__locals1.conflicts = conflicts;
			CS$<>8__locals1.removedConflicts = new HashSet<Conflict>();
			foreach (Conflict conflict in CS$<>8__locals1.conflicts.ToList<Conflict>())
			{
				if (!CS$<>8__locals1.removedConflicts.Contains(conflict))
				{
					foreach (Conflict conflict2 in CS$<>8__locals1.conflicts.OverlappingElements(conflict.Bounds).ToList<Conflict>())
					{
						if (conflict2 != conflict)
						{
							ConflictCollection<TCell>.<>c__DisplayClass15_1 CS$<>8__locals2;
							CS$<>8__locals2.removed = false;
							foreach (Axis axis in AxisUtilities.Axes)
							{
								if (!CS$<>8__locals2.removed && !conflict2.IsExclusive[axis])
								{
									Axis axis2 = axis.Perpendicular();
									if (conflict.Bounds[axis2].Contains(conflict2.Bounds[axis2]))
									{
										if (!conflict.IsExclusive[axis])
										{
											if (conflict2.Bounds[axis].Contains(conflict.Bounds[axis]))
											{
												ConflictCollection<TCell>.<RemoveSubsetConflicts>g__RemoveConflict|15_0(conflict2, ref CS$<>8__locals1, ref CS$<>8__locals2);
											}
										}
										else if (conflict.IsExclusive[axis2])
										{
											ConflictCollection<TCell>.<RemoveSubsetConflicts>g__RemoveConflict|15_0(conflict2, ref CS$<>8__locals1, ref CS$<>8__locals2);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x001061FC File Offset: 0x001043FC
		private static SortedList<int, List<ConflictBoundary>> SortConflictBoundaries(IReadOnlyList<Conflict> conflicts)
		{
			return new SortedList<int, List<ConflictBoundary>>((from boundary in conflicts.SelectMany(delegate(Conflict conflict)
				{
					bool[] array = new bool[2];
					array[0] = true;
					return array.Select((bool isTop) => new ConflictBoundary(conflict, isTop));
				})
				group boundary by boundary.Y).ToDictionary((IGrouping<int, ConflictBoundary> group) => group.Key, (IGrouping<int, ConflictBoundary> group) => group.ToList<ConflictBoundary>()));
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001062D1 File Offset: 0x001044D1
		[NullableContext(0)]
		[CompilerGenerated]
		internal static void <RemoveSubsetConflicts>g__RemoveConflict|15_0([Nullable(1)] Conflict conflictToRemove, ref ConflictCollection<TCell>.<>c__DisplayClass15_0 A_1, ref ConflictCollection<TCell>.<>c__DisplayClass15_1 A_2)
		{
			A_1.removedConflicts.Add(conflictToRemove);
			A_1.conflicts.Remove(conflictToRemove);
			A_2.removed = true;
		}

		// Token: 0x04002550 RID: 9552
		private readonly Lazy<QuadTree<Conflict, PixelUnit>> _lazyConflictsTree;

		// Token: 0x04002551 RID: 9553
		private static readonly ConflictCollection<TCell> Empty = new ConflictCollection<TCell>(new Conflict[0]);
	}
}
