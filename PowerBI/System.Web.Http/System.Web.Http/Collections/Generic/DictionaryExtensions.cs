using System;
using System.ComponentModel;

namespace System.Collections.Generic
{
	// Token: 0x02000189 RID: 393
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class DictionaryExtensions
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0001A44B File Offset: 0x0001864B
		public static void RemoveFromDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> removeCondition)
		{
			dictionary.RemoveFromDictionary((KeyValuePair<TKey, TValue> entry, Func<KeyValuePair<TKey, TValue>, bool> innerCondition) => innerCondition(entry), removeCondition);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001A474 File Offset: 0x00018674
		public static void RemoveFromDictionary<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, TState, bool> removeCondition, TState state)
		{
			int num = 0;
			TKey[] array = new TKey[dictionary.Count];
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				if (removeCondition(keyValuePair, state))
				{
					array[num] = keyValuePair.Key;
					num++;
				}
			}
			for (int i = 0; i < num; i++)
			{
				dictionary.Remove(array[i]);
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001A500 File Offset: 0x00018700
		public static bool TryGetValue<T>(this IDictionary<string, object> collection, string key, out T value)
		{
			object obj;
			if (collection.TryGetValue(key, out obj) && obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001A536 File Offset: 0x00018736
		internal static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(this IDictionary<string, TValue> dictionary, string prefix)
		{
			TValue tvalue;
			if (dictionary.TryGetValue(prefix, out tvalue))
			{
				yield return new KeyValuePair<string, TValue>(prefix, tvalue);
			}
			foreach (KeyValuePair<string, TValue> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
				{
					if (prefix.Length == 0)
					{
						yield return keyValuePair;
					}
					else
					{
						char c = key[prefix.Length];
						if (c == '.' || c == '[')
						{
							yield return keyValuePair;
						}
					}
				}
			}
			IEnumerator<KeyValuePair<string, TValue>> enumerator = null;
			yield break;
			yield break;
		}
	}
}
