using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x02000011 RID: 17
	internal class NilDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00005A73 File Offset: 0x00003C73
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005A76 File Offset: 0x00003C76
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00005A79 File Offset: 0x00003C79
		public ICollection<string> Keys
		{
			get
			{
				return NilDictionary.EmptyKeys;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005A80 File Offset: 0x00003C80
		public ICollection<object> Values
		{
			get
			{
				return NilDictionary.EmptyValues;
			}
		}

		// Token: 0x1700004A RID: 74
		public object this[string key]
		{
			get
			{
				throw new KeyNotFoundException(key);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005A96 File Offset: 0x00003C96
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return NilDictionary.EmptyKeyValuePairs.GetEnumerator();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005AA2 File Offset: 0x00003CA2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return NilDictionary.EmptyKeyValuePairs.GetEnumerator();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005AAE File Offset: 0x00003CAE
		public void Add(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005AB5 File Offset: 0x00003CB5
		public void Clear()
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005AB7 File Offset: 0x00003CB7
		public bool Contains(KeyValuePair<string, object> item)
		{
			return false;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005ABA File Offset: 0x00003CBA
		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005ABC File Offset: 0x00003CBC
		public bool Remove(KeyValuePair<string, object> item)
		{
			return false;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005ABF File Offset: 0x00003CBF
		public bool ContainsKey(string key)
		{
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005AC2 File Offset: 0x00003CC2
		public void Add(string key, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005AC9 File Offset: 0x00003CC9
		public bool Remove(string key)
		{
			return false;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005ACC File Offset: 0x00003CCC
		public bool TryGetValue(string key, out object value)
		{
			value = null;
			return false;
		}

		// Token: 0x04000084 RID: 132
		private static readonly string[] EmptyKeys = new string[0];

		// Token: 0x04000085 RID: 133
		private static readonly object[] EmptyValues = new object[0];

		// Token: 0x04000086 RID: 134
		private static readonly IEnumerable<KeyValuePair<string, object>> EmptyKeyValuePairs = Enumerable.Empty<KeyValuePair<string, object>>();
	}
}
