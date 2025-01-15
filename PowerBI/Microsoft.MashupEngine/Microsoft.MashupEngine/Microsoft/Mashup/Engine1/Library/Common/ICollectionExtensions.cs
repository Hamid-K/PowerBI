using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D0 RID: 4304
	internal static class ICollectionExtensions
	{
		// Token: 0x060070C4 RID: 28868 RVA: 0x0018387C File Offset: 0x00181A7C
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			foreach (T t in items)
			{
				collection.Add(t);
			}
		}
	}
}
