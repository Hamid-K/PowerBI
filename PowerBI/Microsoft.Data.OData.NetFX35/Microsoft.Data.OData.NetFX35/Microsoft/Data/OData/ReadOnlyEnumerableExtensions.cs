using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x02000137 RID: 311
	internal static class ReadOnlyEnumerableExtensions
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x0001A523 File Offset: 0x00018723
		internal static bool IsEmptyReadOnlyEnumerable<T>(this IEnumerable<T> source)
		{
			return object.ReferenceEquals(source, ReadOnlyEnumerable<T>.Empty());
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A530 File Offset: 0x00018730
		internal static ReadOnlyEnumerable<T> ToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			ReadOnlyEnumerable<T> readOnlyEnumerable = source as ReadOnlyEnumerable<T>;
			if (readOnlyEnumerable == null)
			{
				throw new ODataException(Strings.ReaderUtils_EnumerableModified(collectionName));
			}
			return readOnlyEnumerable;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001A554 File Offset: 0x00018754
		internal static ReadOnlyEnumerable<T> GetOrCreateReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName)
		{
			if (source.IsEmptyReadOnlyEnumerable<T>())
			{
				return new ReadOnlyEnumerable<T>();
			}
			return source.ToReadOnlyEnumerable(collectionName);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001A56C File Offset: 0x0001876C
		internal static ReadOnlyEnumerable<T> ConcatToReadOnlyEnumerable<T>(this IEnumerable<T> source, string collectionName, T item)
		{
			ReadOnlyEnumerable<T> orCreateReadOnlyEnumerable = source.GetOrCreateReadOnlyEnumerable(collectionName);
			orCreateReadOnlyEnumerable.AddToSourceList(item);
			return orCreateReadOnlyEnumerable;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001A589 File Offset: 0x00018789
		internal static void AddAction(this ODataEntry entry, ODataAction action)
		{
			entry.Actions = entry.Actions.ConcatToReadOnlyEnumerable("Actions", action);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001A5A2 File Offset: 0x000187A2
		internal static void AddFunction(this ODataEntry entry, ODataFunction function)
		{
			entry.Functions = entry.Functions.ConcatToReadOnlyEnumerable("Functions", function);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001A5BB File Offset: 0x000187BB
		internal static void AddAssociationLink(this ODataEntry entry, ODataAssociationLink associationLink)
		{
			entry.AssociationLinks = entry.AssociationLinks.ConcatToReadOnlyEnumerable("AssociationLinks", associationLink);
		}
	}
}
