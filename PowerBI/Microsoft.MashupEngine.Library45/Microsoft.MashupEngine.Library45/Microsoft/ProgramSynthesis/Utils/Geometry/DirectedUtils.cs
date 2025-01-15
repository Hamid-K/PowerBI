using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005CD RID: 1485
	public static class DirectedUtils
	{
		// Token: 0x06002030 RID: 8240 RVA: 0x0005C34A File Offset: 0x0005A54A
		public static DirectedList<T> SelectMany<T>(this IEnumerable<Directed<T>> xs)
		{
			xs.ToList<Directed<T>>();
			return new DirectedList<T>((Direction dir) => xs.Select((Directed<T> x) => x[dir]));
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0005C374 File Offset: 0x0005A574
		public static DirectedList<TResult> SelectMany<T, TResult>(this IEnumerable<T> xs, Func<T, Directed<TResult>> func)
		{
			return xs.Select(func).SelectMany<TResult>();
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x0005C384 File Offset: 0x0005A584
		public static bool All<T>(this Directed<T> xs, Func<Direction, T, bool> func)
		{
			return DirectionUtilities.Directions.All((Direction dir) => func(dir, xs[dir]));
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		public static bool All<T>(this Directed<T> xs, Func<T, bool> func)
		{
			return DirectionUtilities.Directions.All((Direction dir) => func(xs[dir]));
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0005C3F4 File Offset: 0x0005A5F4
		public static bool Any<T>(this Directed<T> xs, Func<Direction, T, bool> func)
		{
			return DirectionUtilities.Directions.Any((Direction dir) => func(dir, xs[dir]));
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0005C42C File Offset: 0x0005A62C
		public static bool Any<T>(this Directed<T> xs, Func<T, bool> func)
		{
			return DirectionUtilities.Directions.Any((Direction dir) => func(xs[dir]));
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x0005C464 File Offset: 0x0005A664
		public static IEnumerable<Record<Direction, T>> Enumerate<T>(this Directed<T> xs)
		{
			return DirectionUtilities.Directions.Select((Direction dir) => Record.Create<Direction, T>(dir, xs[dir]));
		}
	}
}
