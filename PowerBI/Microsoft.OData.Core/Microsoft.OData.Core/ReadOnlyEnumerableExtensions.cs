using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000C7 RID: 199
	internal static class ReadOnlyEnumerableExtensions
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x000168AC File Offset: 0x00014AAC
		internal static bool IsEmptyReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return source == ReadOnlyEnumerable<T>.Empty();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000168B8 File Offset: 0x00014AB8
		internal static ReadOnlyEnumerable<T> ToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			ReadOnlyEnumerable<T> readOnlyEnumerable = source as ReadOnlyEnumerable<T>;
			if (readOnlyEnumerable == null)
			{
				throw new ODataException(Strings.ReaderUtils_EnumerableModified(collectionName));
			}
			return readOnlyEnumerable;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000168DC File Offset: 0x00014ADC
		internal static ReadOnlyEnumerable<T> GetOrCreateReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			if (source.IsEmptyReadOnlyEnumerable<T>())
			{
				return new ReadOnlyEnumerable<T>();
			}
			return source.ToReadOnlyEnumerable(collectionName);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000168F4 File Offset: 0x00014AF4
		internal static ReadOnlyEnumerable<T> ConcatToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName, T item)
		{
			ReadOnlyEnumerable<T> orCreateReadOnlyEnumerable = source.GetOrCreateReadOnlyEnumerable(collectionName);
			orCreateReadOnlyEnumerable.AddToSourceList(item);
			return orCreateReadOnlyEnumerable;
		}
	}
}
