using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
	// Token: 0x02000008 RID: 8
	public class HeaderDictionary : IHeaderDictionary, IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable, IDictionary<string, string[]>, ICollection<KeyValuePair<string, string[]>>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000235D File Offset: 0x0000055D
		public HeaderDictionary(IDictionary<string, string[]> store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000237A File Offset: 0x0000057A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002382 File Offset: 0x00000582
		private IDictionary<string, string[]> Store { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000238B File Offset: 0x0000058B
		public ICollection<string> Keys
		{
			get
			{
				return this.Store.Keys;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002398 File Offset: 0x00000598
		public ICollection<string[]> Values
		{
			get
			{
				return this.Store.Values;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023A5 File Offset: 0x000005A5
		public int Count
		{
			get
			{
				return this.Store.Count;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023B2 File Offset: 0x000005B2
		public bool IsReadOnly
		{
			get
			{
				return this.Store.IsReadOnly;
			}
		}

		// Token: 0x1700000C RID: 12
		public string this[string key]
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

		// Token: 0x1700000D RID: 13
		string[] IDictionary<string, string[]>.this[string key]
		{
			get
			{
				return this.Store[key];
			}
			set
			{
				this.Store[key] = value;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023EF File Offset: 0x000005EF
		public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
		{
			return this.Store.GetEnumerator();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023FC File Offset: 0x000005FC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002404 File Offset: 0x00000604
		public string Get(string key)
		{
			return OwinHelpers.GetHeader(this.Store, key);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002412 File Offset: 0x00000612
		public IList<string> GetValues(string key)
		{
			return OwinHelpers.GetHeaderUnmodified(this.Store, key);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002420 File Offset: 0x00000620
		public IList<string> GetCommaSeparatedValues(string key)
		{
			IEnumerable<string> values = OwinHelpers.GetHeaderSplit(this.Store, key);
			if (values != null)
			{
				return values.ToList<string>();
			}
			return null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002445 File Offset: 0x00000645
		public void Append(string key, string value)
		{
			OwinHelpers.AppendHeader(this.Store, key, value);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002454 File Offset: 0x00000654
		public void AppendValues(string key, params string[] values)
		{
			OwinHelpers.AppendHeaderUnmodified(this.Store, key, values);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002463 File Offset: 0x00000663
		public void AppendCommaSeparatedValues(string key, params string[] values)
		{
			OwinHelpers.AppendHeaderJoined(this.Store, key, values);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002472 File Offset: 0x00000672
		public void Set(string key, string value)
		{
			OwinHelpers.SetHeader(this.Store, key, value);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002481 File Offset: 0x00000681
		public void SetValues(string key, params string[] values)
		{
			OwinHelpers.SetHeaderUnmodified(this.Store, key, values);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002490 File Offset: 0x00000690
		public void SetCommaSeparatedValues(string key, params string[] values)
		{
			OwinHelpers.SetHeaderJoined(this.Store, key, values);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000249F File Offset: 0x0000069F
		public void Add(string key, string[] value)
		{
			this.Store.Add(key, value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024AE File Offset: 0x000006AE
		public bool ContainsKey(string key)
		{
			return this.Store.ContainsKey(key);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024BC File Offset: 0x000006BC
		public bool Remove(string key)
		{
			return this.Store.Remove(key);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024CA File Offset: 0x000006CA
		public bool TryGetValue(string key, out string[] value)
		{
			return this.Store.TryGetValue(key, out value);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024D9 File Offset: 0x000006D9
		public void Add(KeyValuePair<string, string[]> item)
		{
			this.Store.Add(item);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024E7 File Offset: 0x000006E7
		public void Clear()
		{
			this.Store.Clear();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024F4 File Offset: 0x000006F4
		public bool Contains(KeyValuePair<string, string[]> item)
		{
			return this.Store.Contains(item);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002502 File Offset: 0x00000702
		public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
		{
			this.Store.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002511 File Offset: 0x00000711
		public bool Remove(KeyValuePair<string, string[]> item)
		{
			return this.Store.Remove(item);
		}
	}
}
