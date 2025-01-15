using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x0200013C RID: 316
	internal sealed class ODataBatchOperationHeaders : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0002D3AA File Offset: 0x0002B5AA
		public ODataBatchOperationHeaders()
		{
			this.caseSensitiveDictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x17000261 RID: 609
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

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002D3F8 File Offset: 0x0002B5F8
		public void Add(string key, string value)
		{
			this.caseSensitiveDictionary.Add(key, value);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002D407 File Offset: 0x0002B607
		public bool ContainsKeyOrdinal(string key)
		{
			return this.caseSensitiveDictionary.ContainsKey(key);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002D415 File Offset: 0x0002B615
		public bool Remove(string key)
		{
			if (this.caseSensitiveDictionary.Remove(key))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			return key != null && this.caseSensitiveDictionary.Remove(key);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002D441 File Offset: 0x0002B641
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

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002D472 File Offset: 0x0002B672
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002D484 File Offset: 0x0002B684
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002D498 File Offset: 0x0002B698
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

		// Token: 0x040004FF RID: 1279
		private readonly Dictionary<string, string> caseSensitiveDictionary;
	}
}
