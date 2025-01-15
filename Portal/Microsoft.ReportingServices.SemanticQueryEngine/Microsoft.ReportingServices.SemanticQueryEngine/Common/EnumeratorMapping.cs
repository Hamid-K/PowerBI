using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000075 RID: 117
	internal static class EnumeratorMapping
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x00015C1C File Offset: 0x00013E1C
		public static IEnumerable<U> Map<T, U>(IEnumerable<T> source, Converter<T, U> mapFunc)
		{
			foreach (T t in source)
			{
				yield return mapFunc(t);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}
	}
}
