using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Extensions
{
	// Token: 0x0200014C RID: 332
	internal static class ListExtensions
	{
		// Token: 0x06001165 RID: 4453 RVA: 0x0003D058 File Offset: 0x0003B258
		public static int IndexOf<T>(this IList<T> list, Predicate<T> filter)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (filter(list[i]))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
