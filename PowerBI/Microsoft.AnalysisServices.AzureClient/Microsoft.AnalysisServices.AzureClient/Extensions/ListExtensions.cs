using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AzureClient.Extensions
{
	// Token: 0x0200003A RID: 58
	internal static class ListExtensions
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00008F1C File Offset: 0x0000711C
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
