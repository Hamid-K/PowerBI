using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000064 RID: 100
	internal static class EnumeratorMapping
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00016300 File Offset: 0x00014500
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
