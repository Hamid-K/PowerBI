using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000CFD RID: 3325
	[NullableContext(1)]
	[Nullable(0)]
	internal class NonConflictingRegionCollection<TCell> where TCell : class, IWordAmalgamation<TCell>
	{
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x0010B940 File Offset: 0x00109B40
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		public QuadTree<BoundsWrapper<PixelUnit>, PixelUnit> Regions
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get;
		}

		// Token: 0x06005537 RID: 21815 RVA: 0x0010B948 File Offset: 0x00109B48
		private NonConflictingRegionCollection([Nullable(new byte[] { 1, 0, 1, 1 })] QuadTree<BoundsWrapper<PixelUnit>, PixelUnit> regions)
		{
			this.Regions = regions;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x0010B958 File Offset: 0x00109B58
		private static void RecordCompleteRegion([Nullable(new byte[] { 1, 0, 1, 1 })] QuadTree<BoundsWrapper<PixelUnit>, PixelUnit> completedRegions, [Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> newBounds)
		{
			List<BoundsWrapper<PixelUnit>> list = new List<BoundsWrapper<PixelUnit>>();
			foreach (BoundsWrapper<PixelUnit> boundsWrapper in completedRegions.OverlappingElements(newBounds))
			{
				if (boundsWrapper.Contains(newBounds))
				{
					return;
				}
				if (newBounds.Contains(boundsWrapper.Bounds))
				{
					list.Add(boundsWrapper);
				}
			}
			completedRegions.RemoveRange(list);
			completedRegions.Add(newBounds);
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x0010B9E0 File Offset: 0x00109BE0
		public static NonConflictingRegionCollection<TCell> Build([Nullable(new byte[] { 0, 1 })] Bounds<PixelUnit> pageBounds, ConflictCollection<TCell> conflicts)
		{
			NonConflictingRegionCollection<TCell>.<>c__DisplayClass6_0 CS$<>8__locals1 = new NonConflictingRegionCollection<TCell>.<>c__DisplayClass6_0();
			CS$<>8__locals1.completedRegions = new QuadTree<BoundsWrapper<PixelUnit>, PixelUnit>(pageBounds);
			CS$<>8__locals1.partialGrids = new HashSet<NonConflictingRegionCollection<TCell>.PartialGridRegion>
			{
				new NonConflictingRegionCollection<TCell>.PartialGridRegion(0, pageBounds.Horizontal, true, null)
			};
			ConflictBoundary conflictBoundary = new ConflictBoundary(new Conflict(new Bounds<PixelUnit>(pageBounds.Horizontal, new Range<PixelUnit>(pageBounds.Bottom + 1, pageBounds.Bottom + 1))), true);
			using (IEnumerator<ConflictBoundary> enumerator = conflicts.ConflictBoundaries.Values.SelectMany((List<ConflictBoundary> c) => c).AppendItem(conflictBoundary).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ConflictBoundary conflict = enumerator.Current;
					List<NonConflictingRegionCollection<TCell>.PartialGridRegion> list = CS$<>8__locals1.partialGrids.Where((NonConflictingRegionCollection<TCell>.PartialGridRegion partialGrid) => partialGrid.Range.Overlaps(conflict.Range) && partialGrid.Top <= conflict.Conflict.Bounds.Bottom).ToList<NonConflictingRegionCollection<TCell>.PartialGridRegion>();
					if (conflict.Top)
					{
						using (List<NonConflictingRegionCollection<TCell>.PartialGridRegion>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								NonConflictingRegionCollection<TCell>.PartialGridRegion partial2 = enumerator2.Current;
								if (conflict.IsExclusive.Horizontal || partial2.Range.Contains(conflict.Range))
								{
									CS$<>8__locals1.partialGrids.Remove(partial2);
									if (conflict.IsExclusive.Vertical)
									{
										Range<PixelUnit>.MaybeCreate(partial2.Top, conflict.Y - 1).Select(delegate(Range<PixelUnit> range)
										{
											NonConflictingRegionCollection<TCell>.RecordCompleteRegion(CS$<>8__locals1.completedRegions, new Bounds<PixelUnit>(partial2.Range, range));
										});
										CS$<>8__locals1.<Build>g__AddPartialGrid|0(new NonConflictingRegionCollection<TCell>.PartialGridRegion(conflict.Conflict.Bounds.Bottom + 1, partial2.Range, true, new int?(partial2.ZombieUntil)));
									}
									else
									{
										CS$<>8__locals1.partialGrids.Add(new NonConflictingRegionCollection<TCell>.PartialGridRegion(partial2.Top, partial2.Range, partial2.TopFromConflict, new int?(conflict.Conflict.Bounds.Bottom)));
										CS$<>8__locals1.<Build>g__AddPartialGrid|0(new NonConflictingRegionCollection<TCell>.PartialGridRegion(Math.Max(conflict.Conflict.Bounds.Top + 1, partial2.Top), partial2.Range, true, new int?(partial2.ZombieUntil)));
									}
								}
								IEnumerable<Range<PixelUnit>> enumerable;
								if (conflict.IsExclusive.Horizontal)
								{
									enumerable = partial2.Range.Subtract(conflict.Range);
								}
								else
								{
									enumerable = new Optional<Range<PixelUnit>>[]
									{
										Range<PixelUnit>.MaybeCreate(Math.Max(conflict.Range.Min + 1, partial2.Range.Min), partial2.Range.Max),
										Range<PixelUnit>.MaybeCreate(partial2.Range.Min, Math.Min(conflict.Range.Max - 1, partial2.Range.Max))
									}.SelectValues<Range<PixelUnit>>();
								}
								IEnumerable<Range<PixelUnit>> enumerable2 = enumerable;
								Func<Range<PixelUnit>, NonConflictingRegionCollection<TCell>.PartialGridRegion> func;
								Func<Range<PixelUnit>, NonConflictingRegionCollection<TCell>.PartialGridRegion> <>9__6;
								if ((func = <>9__6) == null)
								{
									func = (<>9__6 = (Range<PixelUnit> range) => new NonConflictingRegionCollection<TCell>.PartialGridRegion(partial2.Top, range, false, new int?(partial2.ZombieUntil)));
								}
								foreach (NonConflictingRegionCollection<TCell>.PartialGridRegion partialGridRegion in enumerable2.Select(func))
								{
									CS$<>8__locals1.<Build>g__AddPartialGrid|0(partialGridRegion);
								}
							}
							continue;
						}
					}
					if (!conflict.IsExclusive.Vertical)
					{
						IEnumerable<NonConflictingRegionCollection<TCell>.PartialGridRegion> enumerable3 = list;
						Func<NonConflictingRegionCollection<TCell>.PartialGridRegion, bool> func2;
						Func<NonConflictingRegionCollection<TCell>.PartialGridRegion, bool> <>9__7;
						if ((func2 = <>9__7) == null)
						{
							func2 = (<>9__7 = (NonConflictingRegionCollection<TCell>.PartialGridRegion partial) => partial.Top <= conflict.Conflict.Bounds.Top);
						}
						using (IEnumerator<NonConflictingRegionCollection<TCell>.PartialGridRegion> enumerator3 = enumerable3.Where(func2).GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								NonConflictingRegionCollection<TCell>.PartialGridRegion partial = enumerator3.Current;
								CS$<>8__locals1.partialGrids.Remove(partial);
								Range<PixelUnit>.MaybeCreate(partial.Top, conflict.Y - 1).Select(delegate(Range<PixelUnit> range)
								{
									NonConflictingRegionCollection<TCell>.RecordCompleteRegion(CS$<>8__locals1.completedRegions, new Bounds<PixelUnit>(partial.Range, range));
								});
							}
						}
					}
				}
			}
			return new NonConflictingRegionCollection<TCell>(CS$<>8__locals1.completedRegions);
		}

		// Token: 0x02000CFE RID: 3326
		[Nullable(new byte[] { 0, 0, 1 })]
		internal class PartialGridRegion : Tuple<int, Range<PixelUnit>, bool, int>
		{
			// Token: 0x17000F76 RID: 3958
			// (get) Token: 0x0600553A RID: 21818 RVA: 0x0010BFCC File Offset: 0x0010A1CC
			public int Top
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x17000F77 RID: 3959
			// (get) Token: 0x0600553B RID: 21819 RVA: 0x0010BFD4 File Offset: 0x0010A1D4
			[Nullable(new byte[] { 0, 1 })]
			public Range<PixelUnit> Range
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return base.Item2;
				}
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x0600553C RID: 21820 RVA: 0x0010BFDC File Offset: 0x0010A1DC
			public bool TopFromConflict
			{
				get
				{
					return base.Item3;
				}
			}

			// Token: 0x17000F79 RID: 3961
			// (get) Token: 0x0600553D RID: 21821 RVA: 0x0010BFE4 File Offset: 0x0010A1E4
			public int ZombieUntil
			{
				get
				{
					return base.Item4;
				}
			}

			// Token: 0x0600553E RID: 21822 RVA: 0x0010BFEC File Offset: 0x0010A1EC
			public PartialGridRegion(int top, [Nullable(new byte[] { 0, 1 })] Range<PixelUnit> range, bool topFromConflict, int? zombieUntil)
				: base(top, range, topFromConflict, zombieUntil ?? (-1))
			{
			}
		}
	}
}
