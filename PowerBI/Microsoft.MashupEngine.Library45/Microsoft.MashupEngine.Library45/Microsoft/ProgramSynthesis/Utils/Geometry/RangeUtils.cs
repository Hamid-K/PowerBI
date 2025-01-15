using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005EA RID: 1514
	public static class RangeUtils
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x0005D334 File Offset: 0x0005B534
		public static Range<TUnit> Range<TUnit>(this IEnumerable<int> xs) where TUnit : BoundsUnit
		{
			Record<int, int> record = xs.Extrema(null);
			return new Range<TUnit>(record.Item1, record.Item2);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x0005D35A File Offset: 0x0005B55A
		public static bool WithinToleranceOnBothSides<TUnit>(Range<TUnit> range, Range<TUnit> other, int tolerance) where TUnit : BoundsUnit
		{
			return MathUtils.WithinTolerance(range.Min, other.Min, tolerance) && MathUtils.WithinTolerance(range.Max, other.Max, tolerance);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0005D388 File Offset: 0x0005B588
		public static bool AreDisjoint<TUnit>(this IEnumerable<Range<TUnit>> xs) where TUnit : BoundsUnit
		{
			return xs.OrderBy((Range<TUnit> x) => x.Min).Windowed<Range<TUnit>>().All2((Range<TUnit> x, Range<TUnit> y) => x.Max < y.Min);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0005D3E3 File Offset: 0x0005B5E3
		public static IEnumerable<Range<T>> AsRanges<T>(this IEnumerable<int> xs) where T : BoundsUnit
		{
			int? num = null;
			int? num2 = null;
			foreach (int x in xs)
			{
				if (num2 != null)
				{
					int valueOrDefault = num2.GetValueOrDefault();
					if (MathUtils.WithinTolerance(valueOrDefault, x, 1))
					{
						num2 = new int?(x);
						continue;
					}
					yield return Range<T>.CreateUnordered(num.Value, valueOrDefault);
				}
				num2 = new int?(x);
				num = num2;
			}
			IEnumerator<int> enumerator = null;
			if (num != null)
			{
				yield return Range<T>.CreateUnordered(num.Value, num2.Value);
			}
			yield break;
			yield break;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0005D3F3 File Offset: 0x0005B5F3
		private static IEnumerable<T> Slice<T, TUnit>(this IEnumerable<T> xs, Range<TUnit> range) where TUnit : BoundsUnit
		{
			return xs.Skip(range.Min).Take(range.Size());
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0005D40E File Offset: 0x0005B60E
		public static IEnumerable<T> Slice<T>(this IEnumerable<T> xs, Range<IndexUnit> range)
		{
			return xs.Slice(range);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0005D417 File Offset: 0x0005B617
		public static IEnumerable<T> Slice<T>(this IEnumerable<T> xs, Range<TableUnit> range)
		{
			return xs.Slice(range);
		}
	}
}
