using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200002E RID: 46
	internal sealed class ODataBatchOperationHeaders : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000590E File Offset: 0x00003B0E
		public ODataBatchOperationHeaders()
		{
			this.caseSensitiveDictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x1700003F RID: 63
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

		// Token: 0x06000137 RID: 311 RVA: 0x0000595C File Offset: 0x00003B5C
		public void Add(string key, string value)
		{
			this.caseSensitiveDictionary.Add(key, value);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000596B File Offset: 0x00003B6B
		public bool ContainsKeyOrdinal(string key)
		{
			return this.caseSensitiveDictionary.ContainsKey(key);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005979 File Offset: 0x00003B79
		public bool Remove(string key)
		{
			if (this.caseSensitiveDictionary.Remove(key))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			return key != null && this.caseSensitiveDictionary.Remove(key);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000059A5 File Offset: 0x00003BA5
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

		// Token: 0x0600013B RID: 315 RVA: 0x000059D6 File Offset: 0x00003BD6
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000059D6 File Offset: 0x00003BD6
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000059E8 File Offset: 0x00003BE8
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

		// Token: 0x040000D0 RID: 208
		private readonly Dictionary<string, string> caseSensitiveDictionary;
	}
}
