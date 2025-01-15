using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005E2 RID: 1506
	public static class IBoundedExtensions
	{
		// Token: 0x0600207C RID: 8316 RVA: 0x0005C818 File Offset: 0x0005AA18
		public static bool IsAfter<TUnit>(this IBounded<TUnit> a, IBounded<TUnit> b, Axis axis) where TUnit : BoundsUnit
		{
			return a.IsAfter(b.Bounds, axis);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0005C828 File Offset: 0x0005AA28
		public static bool IsAfter<TUnit>(this IBounded<TUnit> a, Bounds<TUnit> b, Axis axis) where TUnit : BoundsUnit
		{
			return a.Bounds.IsAfter(b, axis, false);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0005C848 File Offset: 0x0005AA48
		public static bool IsAfter<TUnit>(this IBounded<TUnit> a, int line, Axis axis) where TUnit : BoundsUnit
		{
			return a.Bounds.IsAfter(line, axis);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0005C865 File Offset: 0x0005AA65
		public static bool IsBefore<TUnit>(this IBounded<TUnit> a, IBounded<TUnit> b, Axis axis) where TUnit : BoundsUnit
		{
			return a.IsBefore(b.Bounds, axis);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0005C874 File Offset: 0x0005AA74
		public static bool IsBefore<TUnit>(this IBounded<TUnit> a, Bounds<TUnit> b, Axis axis) where TUnit : BoundsUnit
		{
			return a.Bounds.IsBefore(b, axis, false);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0005C894 File Offset: 0x0005AA94
		public static bool IsBefore<TUnit>(this IBounded<TUnit> a, int line, Axis axis) where TUnit : BoundsUnit
		{
			return a.Bounds.IsBefore(line, axis);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0005C8B4 File Offset: 0x0005AAB4
		public static bool Contains<TUnit>(this IBounded<TUnit> possibleSuperset, IBounded<TUnit> other) where TUnit : BoundsUnit
		{
			return possibleSuperset.Bounds.Contains(other.Bounds);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0005C8D8 File Offset: 0x0005AAD8
		public static bool Contains<TUnit>(this IBounded<TUnit> possibleSuperset, Bounds<TUnit> bounds) where TUnit : BoundsUnit
		{
			return possibleSuperset.Bounds.Contains(bounds);
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0005C8F4 File Offset: 0x0005AAF4
		public static bool Contains<TUnit>(this IBounded<TUnit> possibleSuperset, Range<TUnit> range, Axis axis) where TUnit : BoundsUnit
		{
			return possibleSuperset.Bounds.Contains(range, axis);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0005C914 File Offset: 0x0005AB14
		public static bool IsBoundsSubsetOf<TUnit>(this IBounded<TUnit> possibleSubset, IBounded<TUnit> other) where TUnit : BoundsUnit
		{
			return possibleSubset.Bounds.IsSubsetOf(other.Bounds);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0005C938 File Offset: 0x0005AB38
		public static bool IsBoundsSubsetOf<TUnit>(this IBounded<TUnit> possibleSubset, Bounds<TUnit> bounds) where TUnit : BoundsUnit
		{
			return possibleSubset.Bounds.IsSubsetOf(bounds);
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x0005C954 File Offset: 0x0005AB54
		public static bool IsBoundsSubsetOf<TUnit>(this IBounded<TUnit> possibleSubset, Range<TUnit> range, Axis axis) where TUnit : BoundsUnit
		{
			return possibleSubset.Bounds.IsSubsetOf(range, axis);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x0005C974 File Offset: 0x0005AB74
		public static bool Overlaps<TUnit>(this IBounded<TUnit> possibleOverlapper, IBounded<TUnit> other) where TUnit : BoundsUnit
		{
			return possibleOverlapper.Bounds.Overlaps(other.Bounds);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0005C998 File Offset: 0x0005AB98
		public static bool Overlaps<TUnit>(this IBounded<TUnit> possibleOverlapper, Bounds<TUnit> bounds) where TUnit : BoundsUnit
		{
			return possibleOverlapper.Bounds.Overlaps(bounds);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
		public static bool Overlaps<TUnit>(this IBounded<TUnit> possibleOverlapper, Range<TUnit> range, Axis axis) where TUnit : BoundsUnit
		{
			return possibleOverlapper.Bounds.Overlaps(range, axis);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0005C9D4 File Offset: 0x0005ABD4
		public static bool Overlaps<TUnit>(this IBounded<TUnit> possibleOverlapper, IBounded<TUnit> other, Axis axis) where TUnit : BoundsUnit
		{
			return possibleOverlapper.Bounds.Overlaps(other.Bounds[axis], axis);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0005CA00 File Offset: 0x0005AC00
		public static IEnumerable<T> OrderByClosest<T, TUnit>(this IEnumerable<T> enumerable, Direction dir) where T : IBounded<TUnit> where TUnit : BoundsUnit
		{
			return enumerable.OrderByClosest((T bounded) => bounded.Bounds.BoundInDirection(dir.Opposite()), dir);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0005CA34 File Offset: 0x0005AC34
		public static IEnumerable<T> OrderByClosest<T>(this IEnumerable<T> enumerable, Func<T, int> map, Direction dir)
		{
			Derivative derivative = dir.Derivative();
			if (derivative == Derivative.Decreasing)
			{
				return enumerable.OrderByDescending(map);
			}
			if (derivative == Derivative.Increasing)
			{
				return enumerable.OrderBy(map);
			}
			throw new ArgumentException("dir", string.Format("Invalid direction: {0}", dir));
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0005CA7A File Offset: 0x0005AC7A
		public static Optional<Bounds<TUnit>> MaybeBetween<TUnit>(this IBounded<TUnit> first, IBounded<TUnit> second) where TUnit : BoundsUnit
		{
			return Bounds<TUnit>.MaybeBetween(first.Bounds, second.Bounds);
		}
	}
}
