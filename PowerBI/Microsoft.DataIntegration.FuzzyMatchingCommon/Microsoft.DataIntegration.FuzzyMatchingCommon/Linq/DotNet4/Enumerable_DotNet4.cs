using System;
using System.Collections.Generic;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Internal.DotNet4;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Linq.DotNet4
{
	// Token: 0x0200002C RID: 44
	public static class Enumerable_DotNet4
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00010053 File Offset: 0x0000E253
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable_DotNet4.ZipIterator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00010087 File Offset: 0x0000E287
		private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			using (IEnumerator<TFirst> e = first.GetEnumerator())
			{
				using (IEnumerator<TSecond> e2 = second.GetEnumerator())
				{
					while (e.MoveNext() && e2.MoveNext())
					{
						yield return resultSelector.Invoke(e.Current, e2.Current);
					}
				}
				IEnumerator<TSecond> e2 = null;
			}
			IEnumerator<TFirst> e = null;
			yield break;
			yield break;
		}
	}
}
