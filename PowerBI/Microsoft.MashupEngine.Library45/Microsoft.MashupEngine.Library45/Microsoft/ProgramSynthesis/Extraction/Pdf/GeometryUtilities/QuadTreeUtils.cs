using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C47 RID: 3143
	[NullableContext(1)]
	[Nullable(0)]
	public static class QuadTreeUtils
	{
		// Token: 0x0600511D RID: 20765 RVA: 0x000FEBB4 File Offset: 0x000FCDB4
		[return: Nullable(new byte[] { 1, 0, 1, 0, 1, 1, 1 })]
		public static QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit> ToQuadTree<[Nullable(2)] T, [Nullable(0)] TUnit>(this IEnumerable<T> xs, [Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<T, Bounds<TUnit>> boundsFunc) where TUnit : BoundsUnit
		{
			return new QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit>(bounds, xs.Select((T x) => new BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>(new BoundsWrapper<TUnit>(boundsFunc(x)), x)));
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x000FEBE8 File Offset: 0x000FCDE8
		[return: Nullable(new byte[] { 0, 1, 0, 1, 0, 1, 1, 1 })]
		public static Optional<QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit>> ToQuadTreeWithoutOverlaps<[Nullable(2)] T, [Nullable(0)] TUnit>(this IEnumerable<T> xs, [Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<T, Bounds<TUnit>> boundsFunc) where TUnit : BoundsUnit
		{
			QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit> quadTree = new QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit>(bounds);
			Func<T, BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>> <>9__0;
			Func<T, BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (T x) => new BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>(new BoundsWrapper<TUnit>(boundsFunc(x)), x));
			}
			foreach (BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T> boundedKeyValuePair in xs.Select(func))
			{
				if (quadTree.OverlappingElements(boundedKeyValuePair.Bounds).Any<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>>())
				{
					return Optional<QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit>>.Nothing;
				}
				quadTree.Add(boundedKeyValuePair);
			}
			return quadTree.Some<QuadTree<BoundedKeyValuePair<TUnit, BoundsWrapper<TUnit>, T>, TUnit>>();
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x000FEC90 File Offset: 0x000FCE90
		[return: Nullable(new byte[] { 0, 1, 1, 1 })]
		public static Optional<QuadTree<T, TUnit>> ToQuadTreeWithoutOverlaps<[Nullable(0)] T, [Nullable(0)] TUnit>(this IEnumerable<T> xs, [Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds) where T : IBounded<TUnit> where TUnit : BoundsUnit
		{
			QuadTree<T, TUnit> quadTree = new QuadTree<T, TUnit>(bounds);
			foreach (T t in xs)
			{
				if (quadTree.OverlappingElements(t.Bounds).Any<T>())
				{
					return default(Optional<QuadTree<T, TUnit>>);
				}
				quadTree.Add(t);
			}
			return quadTree.Some<QuadTree<T, TUnit>>();
		}
	}
}
