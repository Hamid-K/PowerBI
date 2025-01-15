using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005A9 RID: 1449
	public static class AxisAlignedLineUtilities
	{
		// Token: 0x06001F80 RID: 8064 RVA: 0x0005A4F8 File Offset: 0x000586F8
		public static Optional<AxisAlignedLine<TUnit>> MaybeAsAxisAlignedLine<TUnit>(this Record<Vector<TUnit>, Vector<TUnit>> line) where TUnit : BoundsUnit
		{
			Vector<TUnit> item = line.Item1;
			Vector<TUnit> item2 = line.Item2;
			if (MathUtils.WithinTolerance(item.X, item2.X, 1))
			{
				return new AxisAlignedLine<TUnit>(Axis.Vertical, Range<TUnit>.CreateUnordered(item.Y, item2.Y), item.X).Some<AxisAlignedLine<TUnit>>();
			}
			if (MathUtils.WithinTolerance(item.Y, item2.Y, 1))
			{
				return new AxisAlignedLine<TUnit>(Axis.Horizontal, Range<TUnit>.CreateUnordered(item.X, item2.X), item.Y).Some<AxisAlignedLine<TUnit>>();
			}
			return Optional<AxisAlignedLine<TUnit>>.Nothing;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0005A588 File Offset: 0x00058788
		public static Optional<Bounds<PixelUnit>> MaybeAsBox(this IEnumerable<AxisAlignedLine<PixelUnit>> linesEnumerable)
		{
			IReadOnlyList<AxisAlignedLine<PixelUnit>> readOnlyList = ((linesEnumerable != null) ? linesEnumerable.ToList<AxisAlignedLine<PixelUnit>>() : null);
			if (readOnlyList == null || readOnlyList.Count != 4)
			{
				return Optional<Bounds<PixelUnit>>.Nothing;
			}
			IReadOnlyList<int> readOnlyList2 = (from line in readOnlyList
				where line.Axis == Axis.Vertical
				select line into l
				select l.Position).Distinct<int>().ToList<int>();
			if (readOnlyList2.Count != 2)
			{
				return default(Optional<Bounds<PixelUnit>>);
			}
			IReadOnlyList<int> readOnlyList3 = (from line in readOnlyList
				where line.Axis == Axis.Horizontal
				select line into l
				select l.Position).Distinct<int>().ToList<int>();
			if (readOnlyList3.Count != 2)
			{
				return Optional<Bounds<PixelUnit>>.Nothing;
			}
			return new Bounds<PixelUnit>(readOnlyList2.Min(), readOnlyList2.Max(), readOnlyList3.Min(), readOnlyList3.Max()).Some<Bounds<PixelUnit>>();
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0005A6A0 File Offset: 0x000588A0
		public static IEnumerable<Bounds<PixelUnit>> AsBoxes(this IEnumerable<AxisAlignedLine<PixelUnit>> linesEnumerable)
		{
			IReadOnlyList<AxisAlignedLine<PixelUnit>> readOnlyList = ((linesEnumerable != null) ? linesEnumerable.ToList<AxisAlignedLine<PixelUnit>>() : null);
			if (readOnlyList == null)
			{
				yield break;
			}
			Ranges<PixelUnit> ranges = Ranges<PixelUnit>.Empty;
			int num = -1;
			foreach (IGrouping<int, AxisAlignedLine<PixelUnit>> group in from line in readOnlyList
				where line.Axis == Axis.Horizontal
				orderby line.Position
				group line by line.Position)
			{
				if (ranges.Any<Range<PixelUnit>>())
				{
					Range<PixelUnit> vertical = new Range<PixelUnit>(num, group.Key);
					foreach (Range<PixelUnit> range in ranges)
					{
						yield return new Bounds<PixelUnit>(range, vertical);
					}
					IEnumerator<Range<PixelUnit>> enumerator2 = null;
				}
				foreach (AxisAlignedLine<PixelUnit> axisAlignedLine in group)
				{
					IReadOnlyList<Range<PixelUnit>> readOnlyList2 = axisAlignedLine.Range.Subtract(ranges);
					Ranges<PixelUnit> ranges2 = ranges.Intersect(axisAlignedLine.Range);
					ranges = ranges.Subtract(ranges2).Join(readOnlyList2);
				}
				num = group.Key;
				group = null;
			}
			IEnumerator<IGrouping<int, AxisAlignedLine<PixelUnit>>> enumerator = null;
			yield break;
			yield break;
		}
	}
}
