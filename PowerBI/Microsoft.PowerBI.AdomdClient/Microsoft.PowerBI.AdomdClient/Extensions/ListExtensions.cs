using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000157 RID: 343
	internal static class ListExtensions
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x0003A424 File Offset: 0x00038624
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
