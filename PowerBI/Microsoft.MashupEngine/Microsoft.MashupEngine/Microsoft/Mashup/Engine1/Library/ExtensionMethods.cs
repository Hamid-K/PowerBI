using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200024D RID: 589
	internal static class ExtensionMethods
	{
		// Token: 0x06001947 RID: 6471 RVA: 0x00031D98 File Offset: 0x0002FF98
		public static void AddRange<K, V>(this Dictionary<K, V> dictionary1, Dictionary<K, V> dictionary2)
		{
			foreach (KeyValuePair<K, V> keyValuePair in dictionary2)
			{
				dictionary1.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00031DF4 File Offset: 0x0002FFF4
		public static V GetValueOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V defaultValue = default(V))
		{
			V v;
			if (dictionary.TryGetValue(key, out v))
			{
				return v;
			}
			return defaultValue;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00031E10 File Offset: 0x00030010
		public static V GetAndRemoveOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V defaultValue = default(V))
		{
			V v;
			if (dictionary.TryGetValue(key, out v))
			{
				dictionary.Remove(key);
				return v;
			}
			return defaultValue;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00031E33 File Offset: 0x00030033
		public static string GetStringOrNull<K>(this IDictionary<K, object> dictionary, K key)
		{
			return dictionary.GetValueOrDefault(key, null) as string;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00031E42 File Offset: 0x00030042
		public static long CopyTo(this Stream source, Stream destination)
		{
			return source.CopyTo(destination, 40960);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00031E50 File Offset: 0x00030050
		public static byte[] Buffer(this Stream stream)
		{
			MemoryStream memoryStream = new MemoryStream();
			stream.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00031E70 File Offset: 0x00030070
		public static RecordFieldEnumerable GetFields(this RecordValue record)
		{
			return new RecordFieldEnumerable(record);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00031E78 File Offset: 0x00030078
		public static Dictionary<string, Value> ToDictionary(this Value value)
		{
			if (value.IsNull)
			{
				return null;
			}
			RecordValue asRecord = value.AsRecord;
			Dictionary<string, Value> dictionary = new Dictionary<string, Value>(asRecord.Count);
			for (int i = 0; i < asRecord.Count; i++)
			{
				dictionary[asRecord.Keys[i]] = asRecord[i];
			}
			return dictionary;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00031ECD File Offset: 0x000300CD
		public static BinaryValue WithExpressionFromValue(this BinaryValue binary, Value expressionValue)
		{
			return new WithExpressionFromValueBinaryValue(binary, expressionValue);
		}

		// Token: 0x040006CC RID: 1740
		private const int defaultBufferSize = 40960;
	}
}
