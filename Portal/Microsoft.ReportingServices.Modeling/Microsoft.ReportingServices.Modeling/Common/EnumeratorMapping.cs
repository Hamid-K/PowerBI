using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200000D RID: 13
	internal static class EnumeratorMapping
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002CD8 File Offset: 0x00000ED8
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
