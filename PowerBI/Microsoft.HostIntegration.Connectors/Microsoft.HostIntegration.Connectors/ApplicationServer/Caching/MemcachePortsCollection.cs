using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal sealed class MemcachePortsCollection : ConfigurationElementCollection, IDictionary<string, int>, ICollection<KeyValuePair<string, int>>, IEnumerable<KeyValuePair<string, int>>, IEnumerable, ISerializable
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x00016D51 File Offset: 0x00014F51
		internal MemcachePortsCollection()
		{
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		protected override ConfigurationElement CreateNewElement()
		{
			return new MemcachePortElement();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		protected override object GetElementKey(ConfigurationElement element)
		{
			MemcachePortElement memcachePortElement = element as MemcachePortElement;
			if (memcachePortElement != null)
			{
				return memcachePortElement.CacheName;
			}
			return null;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00002B16 File Offset: 0x00000D16
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001EF07 File Offset: 0x0001D107
		public void Add(string key, int value)
		{
			this.BaseAdd(new MemcachePortElement(key, value));
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001EF16 File Offset: 0x0001D116
		public bool ContainsKey(string key)
		{
			return base.BaseGet(key) is MemcachePortElement;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001EF28 File Offset: 0x0001D128
		public ICollection<string> Keys
		{
			get
			{
				object[] array = base.BaseGetAllKeys();
				List<string> list = new List<string>(array.Length);
				list.AddRange(array.OfType<string>());
				return list;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001EF54 File Offset: 0x0001D154
		public bool Remove(string key)
		{
			MemcachePortElement memcachePortElement = base.BaseGet(key) as MemcachePortElement;
			if (memcachePortElement != null)
			{
				base.BaseRemove(key);
				return true;
			}
			return false;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001EF7C File Offset: 0x0001D17C
		public bool TryGetValue(string key, out int value)
		{
			MemcachePortElement memcachePortElement = base.BaseGet(key) as MemcachePortElement;
			if (memcachePortElement != null)
			{
				value = memcachePortElement.MemcacheSocketPort;
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001EFA8 File Offset: 0x0001D1A8
		public ICollection<int> Values
		{
			get
			{
				object[] array = base.BaseGetAllKeys();
				List<int> list = new List<int>(array.Length);
				foreach (string text in array)
				{
					list.Add(this[text]);
				}
				return list;
			}
		}

		// Token: 0x170001CC RID: 460
		public int this[string key]
		{
			get
			{
				MemcachePortElement memcachePortElement = base.BaseGet(key) as MemcachePortElement;
				if (memcachePortElement != null)
				{
					return memcachePortElement.MemcacheSocketPort;
				}
				return 0;
			}
			set
			{
				MemcachePortElement memcachePortElement = base.BaseGet(key) as MemcachePortElement;
				if (memcachePortElement != null)
				{
					memcachePortElement.MemcacheSocketPort = value;
					return;
				}
				this.Add(key, value);
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001F049 File Offset: 0x0001D249
		public void Add(KeyValuePair<string, int> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C524 File Offset: 0x0001A724
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001F060 File Offset: 0x0001D260
		public bool Contains(KeyValuePair<string, int> item)
		{
			MemcachePortElement memcachePortElement = base.BaseGet(item.Key) as MemcachePortElement;
			return memcachePortElement != null && memcachePortElement.MemcacheSocketPort == item.Value;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001F094 File Offset: 0x0001D294
		public void CopyTo(KeyValuePair<string, int>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			foreach (KeyValuePair<string, int> keyValuePair in this.GetAll())
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0001F0FC File Offset: 0x0001D2FC
		public new bool IsReadOnly
		{
			get
			{
				return this.IsReadOnly();
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001F104 File Offset: 0x0001D304
		public bool Remove(KeyValuePair<string, int> item)
		{
			MemcachePortElement memcachePortElement = base.BaseGet(item.Key) as MemcachePortElement;
			if (memcachePortElement != null && memcachePortElement.MemcacheSocketPort == item.Value)
			{
				base.BaseRemove(item.Key);
				return true;
			}
			return false;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001F146 File Offset: 0x0001D346
		public new IEnumerator<KeyValuePair<string, int>> GetEnumerator()
		{
			return this.GetAll().GetEnumerator();
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001F154 File Offset: 0x0001D354
		private IEnumerable<KeyValuePair<string, int>> GetAll()
		{
			return from key in base.BaseGetAllKeys().OfType<string>()
				let entry = base.BaseGet(key) as MemcachePortElement
				where entry != null
				select new KeyValuePair<string, int>(key, entry.MemcacheSocketPort);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		private MemcachePortsCollection(SerializationInfo info, StreamingContext context)
		{
			try
			{
				List<KeyValuePair<string, int>> list = info.GetValue("memcachePorts", typeof(List<KeyValuePair<string, int>>)) as List<KeyValuePair<string, int>>;
				if (list != null)
				{
					foreach (KeyValuePair<string, int> keyValuePair in list)
					{
						this.Add(keyValuePair);
					}
				}
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001F248 File Offset: 0x0001D448
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(this.GetAll());
			if (list != null && list.Count != 0)
			{
				info.AddValue("memcachePorts", list, typeof(List<KeyValuePair<string, int>>));
			}
		}
	}
}
