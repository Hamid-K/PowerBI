using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x020000A9 RID: 169
	internal static class ReadOnlyEnumerableExtensions
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00011FEC File Offset: 0x000101EC
		internal static bool IsEmptyReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return source == ReadOnlyEnumerable<T>.Empty();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00011FF8 File Offset: 0x000101F8
		internal static ReadOnlyEnumerable<T> ToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			ReadOnlyEnumerable<T> readOnlyEnumerable = source as ReadOnlyEnumerable<T>;
			if (readOnlyEnumerable == null)
			{
				throw new ODataException(Strings.ReaderUtils_EnumerableModified(collectionName));
			}
			return readOnlyEnumerable;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001201C File Offset: 0x0001021C
		internal static ReadOnlyEnumerable<T> GetOrCreateReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			if (source.IsEmptyReadOnlyEnumerable<T>())
			{
				return new ReadOnlyEnumerable<T>();
			}
			return source.ToReadOnlyEnumerable(collectionName);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00012034 File Offset: 0x00010234
		internal static ReadOnlyEnumerable<T> ConcatToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName, T item)
		{
			ReadOnlyEnumerable<T> orCreateReadOnlyEnumerable = source.GetOrCreateReadOnlyEnumerable(collectionName);
			orCreateReadOnlyEnumerable.AddToSourceList(item);
			return orCreateReadOnlyEnumerable;
		}
	}
}
