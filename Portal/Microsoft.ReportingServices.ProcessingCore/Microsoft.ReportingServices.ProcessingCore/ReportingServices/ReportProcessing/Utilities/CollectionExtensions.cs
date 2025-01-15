using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.ReportingServices.ReportProcessing.Utilities
{
	// Token: 0x020007E1 RID: 2017
	internal static class CollectionExtensions
	{
		// Token: 0x06007142 RID: 28994 RVA: 0x001D7494 File Offset: 0x001D5694
		internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
		{
			if (items != null)
			{
				T[] array = items.ToArray<T>();
				if (array.Length != 0)
				{
					return Array.AsReadOnly<T>(array);
				}
			}
			return CollectionExtensions.EmptyCollections<T>.ReadOnlyCollectionInstance;
		}

		// Token: 0x02000CF3 RID: 3315
		private static class EmptyCollections<T>
		{
			// Token: 0x04004FBD RID: 20413
			internal static readonly T[] ArrayInstance = new T[0];

			// Token: 0x04004FBE RID: 20414
			internal static readonly ReadOnlyCollection<T> ReadOnlyCollectionInstance = Array.AsReadOnly<T>(new T[0]);
		}
	}
}
