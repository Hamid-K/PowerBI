using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005D9 RID: 1497
	internal static class EnumeratorMapping
	{
		// Token: 0x060053E7 RID: 21479 RVA: 0x001616D0 File Offset: 0x0015F8D0
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
