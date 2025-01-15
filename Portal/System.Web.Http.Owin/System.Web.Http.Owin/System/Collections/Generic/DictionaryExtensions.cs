using System;
using System.ComponentModel;

namespace System.Collections.Generic
{
	// Token: 0x0200001A RID: 26
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal static class DictionaryExtensions
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003CFB File Offset: 0x00001EFB
		public static void RemoveFromDictionary<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> removeCondition)
		{
			dictionary.RemoveFromDictionary((KeyValuePair<TKey, TValue> entry, Func<KeyValuePair<TKey, TValue>, bool> innerCondition) => innerCondition(entry), removeCondition);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003D24 File Offset: 0x00001F24
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00003DB0 File Offset: 0x00001FB0
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

		// Token: 0x060000D4 RID: 212 RVA: 0x00003DE6 File Offset: 0x00001FE6
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
