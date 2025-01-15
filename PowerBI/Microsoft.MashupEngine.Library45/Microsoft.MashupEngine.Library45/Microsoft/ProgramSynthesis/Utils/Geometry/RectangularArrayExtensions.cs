using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005F1 RID: 1521
	public static class RectangularArrayExtensions
	{
		// Token: 0x06002112 RID: 8466 RVA: 0x0005DF38 File Offset: 0x0005C138
		public static RectangularArray<T> Select<T>(this Bounds<TableUnit> bounds, Func<Vector<TableUnit>, T> func)
		{
			RectangularArray<T> rectangularArray = new RectangularArray<T>(bounds.Width(), bounds.Height());
			Vector<TableUnit> vector = bounds.Corner(Ordinal.TopLeft);
			foreach (Vector<TableUnit> vector2 in bounds.AsEnumerable(Axis.Vertical, Derivative.Increasing))
			{
				rectangularArray[vector2 - vector] = func(vector2);
			}
			return rectangularArray;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0005DFB8 File Offset: 0x0005C1B8
		public static IEnumerable<Record<Vector<TableUnit>, T>> Enumerate<T>(this RectangularArray<T> xs, Axis majorAxis = Axis.Horizontal, Derivative derivative = Derivative.Increasing)
		{
			return from pos in xs.Span.AsEnumerable(majorAxis, derivative)
				select Record.Create<Vector<TableUnit>, T>(pos, xs[pos]);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0005DFF8 File Offset: 0x0005C1F8
		public static RectangularArray<T> ToRectangularArray<T>(this IReadOnlyList<IReadOnlyList<T>> xss, Axis majorAxis)
		{
			if (xss.Count == 0)
			{
				return new RectangularArray<T>(0, 0);
			}
			if (majorAxis == Axis.Horizontal)
			{
				return new Bounds<TableUnit>(0, xss[0].Count - 1, 0, xss.Count - 1).Select((Vector<TableUnit> vec) => xss[vec.Y][vec.X]);
			}
			return new Bounds<TableUnit>(0, xss.Count - 1, 0, xss[0].Count - 1).Select((Vector<TableUnit> vec) => xss[vec.X][vec.Y]);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0005E09B File Offset: 0x0005C29B
		public static RectangularArray<T> ToRectangularArray<T>(this IEnumerable<IEnumerable<T>> xss, Axis majorAxis)
		{
			return xss.Select((IEnumerable<T> xs) => xs.ToList<T>()).ToList<IReadOnlyList<T>>().ToRectangularArray(majorAxis);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0005E0CD File Offset: 0x0005C2CD
		[NullableContext(1)]
		public static IEnumerable<T> ToEnumerableNonNull<T>([Nullable(new byte[] { 0, 2 })] this RectangularArray<T> xs) where T : class
		{
			return xs.ToEnumerable();
		}
	}
}
