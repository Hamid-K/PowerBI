using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200036A RID: 874
	internal static class EnumeratorMapping
	{
		// Token: 0x06001CC2 RID: 7362 RVA: 0x00073F30 File Offset: 0x00072130
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
