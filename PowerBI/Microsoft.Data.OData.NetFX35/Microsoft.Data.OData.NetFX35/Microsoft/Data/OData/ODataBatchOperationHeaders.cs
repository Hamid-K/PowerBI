using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x020001B5 RID: 437
	internal sealed class ODataBatchOperationHeaders : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002D4D4 File Offset: 0x0002B6D4
		public ODataBatchOperationHeaders()
		{
			this.caseSensitiveDictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x170002EE RID: 750
		public string this[string key]
		{
			get
			{
				string text;
				if (this.TryGetValue(key, out text))
				{
					return text;
				}
				throw new KeyNotFoundException(Strings.ODataBatchOperationHeaderDictionary_KeyNotFound(key));
			}
			set
			{
				this.caseSensitiveDictionary[key] = value;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002D520 File Offset: 0x0002B720
		public void Add(string key, string value)
		{
			this.caseSensitiveDictionary.Add(key, value);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002D52F File Offset: 0x0002B72F
		public bool ContainsKeyOrdinal(string key)
		{
			return this.caseSensitiveDictionary.ContainsKey(key);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002D53D File Offset: 0x0002B73D
		public bool Remove(string key)
		{
			if (this.caseSensitiveDictionary.Remove(key))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			return key != null && this.caseSensitiveDictionary.Remove(key);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002D569 File Offset: 0x0002B769
		public bool TryGetValue(string key, out string value)
		{
			if (this.caseSensitiveDictionary.TryGetValue(key, ref value))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			if (key == null)
			{
				value = null;
				return false;
			}
			return this.caseSensitiveDictionary.TryGetValue(key, ref value);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002D59A File Offset: 0x0002B79A
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002D5AC File Offset: 0x0002B7AC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		private string FindKeyIgnoreCase(string key)
		{
			string text = null;
			foreach (string text2 in this.caseSensitiveDictionary.Keys)
			{
				if (string.Compare(text2, key, 5) == 0)
				{
					if (text != null)
					{
						throw new ODataException(Strings.ODataBatchOperationHeaderDictionary_DuplicateCaseInsensitiveKeys(key));
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0400048D RID: 1165
		private readonly Dictionary<string, string> caseSensitiveDictionary;
	}
}
