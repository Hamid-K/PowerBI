using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Microsoft.Owin.Host.HttpListener.RequestProcessing
{
	// Token: 0x0200000F RID: 15
	internal abstract class HeadersDictionaryBase : IDictionary<string, string[]>, ICollection<KeyValuePair<string, string[]>>, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005753 File Offset: 0x00003953
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000575B File Offset: 0x0000395B
		protected virtual WebHeaderCollection Headers { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005764 File Offset: 0x00003964
		public virtual ICollection<string> Keys
		{
			get
			{
				return this.Headers.AllKeys;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005771 File Offset: 0x00003971
		public virtual ICollection<string[]> Values
		{
			get
			{
				return this.Select((KeyValuePair<string, string[]> pair) => pair.Value).ToList<string[]>();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000579D File Offset: 0x0000399D
		public int Count
		{
			get
			{
				return this.Keys.Count<string>();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000057AA File Offset: 0x000039AA
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000045 RID: 69
		public string[] this[string key]
		{
			get
			{
				return this.Get(key);
			}
			set
			{
				this.Set(key, value);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000057C0 File Offset: 0x000039C0
		public bool ContainsKey(string key)
		{
			return this.Keys.Contains(key, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000057D3 File Offset: 0x000039D3
		public virtual bool Remove(string key)
		{
			if (this.ContainsKey(key))
			{
				this.Headers.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000057ED File Offset: 0x000039ED
		protected virtual void RemoveSilent(string header)
		{
			this.Headers.Remove(header);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000057FC File Offset: 0x000039FC
		protected virtual string[] Get(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			string[] values;
			if (!this.TryGetValue(key, out values))
			{
				throw new KeyNotFoundException(key);
			}
			return values;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000582C File Offset: 0x00003A2C
		protected void Set(string key, string[] value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (value == null || value.Length == 0)
			{
				this.RemoveSilent(key);
				return;
			}
			this.Set(key, value[0]);
			for (int i = 1; i < value.Length; i++)
			{
				this.Append(key, value[i]);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005878 File Offset: 0x00003A78
		protected virtual void Set(string key, string value)
		{
			this.Headers.Set(key, value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005888 File Offset: 0x00003A88
		public void Add(string key, string[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (this.ContainsKey(key))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_DuplicateKey, new object[] { key }));
			}
			for (int i = 0; i < values.Length; i++)
			{
				this.Append(key, values[i]);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000058E3 File Offset: 0x00003AE3
		protected virtual void Append(string key, string value)
		{
			this.Headers.Add(key, value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000058F4 File Offset: 0x00003AF4
		public virtual bool TryGetValue(string key, out string[] value)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				value = null;
				return false;
			}
			string[] keys = this.Headers.AllKeys;
			for (int i = 0; i < keys.Length; i++)
			{
				if (string.Equals(key, keys[i], StringComparison.OrdinalIgnoreCase))
				{
					value = this.Headers.GetValues(i);
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005948 File Offset: 0x00003B48
		public void Add(KeyValuePair<string, string[]> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000595E File Offset: 0x00003B5E
		public void Clear()
		{
			this.Headers.Clear();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000596C File Offset: 0x00003B6C
		public bool Contains(KeyValuePair<string, string[]> item)
		{
			string[] value;
			return this.TryGetValue(item.Key, out value) && item.Value == value;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005998 File Offset: 0x00003B98
		public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex > this.Count - array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, string.Empty);
			}
			foreach (KeyValuePair<string, string[]> item in this)
			{
				array[arrayIndex++] = item;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005A18 File Offset: 0x00003C18
		public bool Remove(KeyValuePair<string, string[]> item)
		{
			return this.Contains(item) && this.Remove(item.Key);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005A32 File Offset: 0x00003C32
		public virtual IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Headers.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, string[]>(this.Headers.GetKey(i), this.Headers.GetValues(i));
				num = i;
			}
			yield break;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005A41 File Offset: 0x00003C41
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
