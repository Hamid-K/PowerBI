using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B1 RID: 433
	internal static class ReadOnlyEnumerableExtensions
	{
		// Token: 0x06001009 RID: 4105 RVA: 0x0003787E File Offset: 0x00035A7E
		internal static bool IsEmptyReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return object.ReferenceEquals(source, ReadOnlyEnumerable<T>.Empty());
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003788C File Offset: 0x00035A8C
		internal static ReadOnlyEnumerable<T> ToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			ReadOnlyEnumerable<T> readOnlyEnumerable = source as ReadOnlyEnumerable<T>;
			if (readOnlyEnumerable == null)
			{
				throw new ODataException(Strings.ReaderUtils_EnumerableModified(collectionName));
			}
			return readOnlyEnumerable;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000378B0 File Offset: 0x00035AB0
		internal static ReadOnlyEnumerable<T> GetOrCreateReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			if (source.IsEmptyReadOnlyEnumerable<T>())
			{
				return new ReadOnlyEnumerable<T>();
			}
			return source.ToReadOnlyEnumerable(collectionName);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x000378C8 File Offset: 0x00035AC8
		internal static ReadOnlyEnumerable<T> ConcatToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName, T item)
		{
			ReadOnlyEnumerable<T> orCreateReadOnlyEnumerable = source.GetOrCreateReadOnlyEnumerable(collectionName);
			orCreateReadOnlyEnumerable.AddToSourceList(item);
			return orCreateReadOnlyEnumerable;
		}
	}
}
