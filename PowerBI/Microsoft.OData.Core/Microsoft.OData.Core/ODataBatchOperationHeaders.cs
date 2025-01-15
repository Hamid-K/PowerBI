using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData
{
	// Token: 0x02000055 RID: 85
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public sealed class ODataBatchOperationHeaders : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00008438 File Offset: 0x00006638
		public ODataBatchOperationHeaders()
		{
			this.caseSensitiveDictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x17000083 RID: 131
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

		// Token: 0x060002AE RID: 686 RVA: 0x00008484 File Offset: 0x00006684
		public void Add(string key, string value)
		{
			this.caseSensitiveDictionary.Add(key, value);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008493 File Offset: 0x00006693
		public bool ContainsKeyOrdinal(string key)
		{
			return this.caseSensitiveDictionary.ContainsKey(key);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000084A1 File Offset: 0x000066A1
		public bool Remove(string key)
		{
			if (this.caseSensitiveDictionary.Remove(key))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			return key != null && this.caseSensitiveDictionary.Remove(key);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000084CD File Offset: 0x000066CD
		public bool TryGetValue(string key, out string value)
		{
			if (this.caseSensitiveDictionary.TryGetValue(key, out value))
			{
				return true;
			}
			key = this.FindKeyIgnoreCase(key);
			if (key == null)
			{
				value = null;
				return false;
			}
			return this.caseSensitiveDictionary.TryGetValue(key, out value);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000084FE File Offset: 0x000066FE
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000084FE File Offset: 0x000066FE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.caseSensitiveDictionary.GetEnumerator();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008510 File Offset: 0x00006710
		private string FindKeyIgnoreCase(string key)
		{
			string text = null;
			foreach (string text2 in this.caseSensitiveDictionary.Keys)
			{
				if (string.Compare(text2, key, StringComparison.OrdinalIgnoreCase) == 0)
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

		// Token: 0x0400013A RID: 314
		private readonly Dictionary<string, string> caseSensitiveDictionary;
	}
}
