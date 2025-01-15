using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001C6 RID: 454
	internal static class EnumerableExtensions
	{
		// Token: 0x0600142A RID: 5162 RVA: 0x00044C21 File Offset: 0x00042E21
		internal static bool IsNullOrEmpty<T>(this IEnumerable<T> input)
		{
			return input == null || !input.Any<T>();
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00044C31 File Offset: 0x00042E31
		internal static string AsSingleString(this IEnumerable<string> input)
		{
			if (input.IsNullOrEmpty<string>())
			{
				return string.Empty;
			}
			return string.Join(" ", input);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00044C4C File Offset: 0x00042E4C
		internal static bool ContainsOrdinalIgnoreCase(this IEnumerable<string> set, string toLookFor)
		{
			return set.Any((string el) => el.Equals(toLookFor, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00044C78 File Offset: 0x00042E78
		internal static List<T> FilterWithLogging<T>(this List<T> list, Func<T, bool> predicate, ILoggerAdapter logger, string logPrefix, bool updateOriginalCollection = true)
		{
			logger.Verbose(() => string.Format("{0} - item count before: {1} ", logPrefix, list.Count));
			if (updateOriginalCollection)
			{
				list.RemoveAll((T e) => !predicate(e));
			}
			else
			{
				list = list.Where(predicate).ToList<T>();
			}
			logger.Verbose(() => string.Format("{0} - item count after: {1} ", logPrefix, list.Count));
			return list;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00044D04 File Offset: 0x00042F04
		internal static void MergeDifferentEntries<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> other)
		{
			if (source == null || other == null)
			{
				return;
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in other)
			{
				if (!source.ContainsKey(keyValuePair.Key))
				{
					source[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}
	}
}
