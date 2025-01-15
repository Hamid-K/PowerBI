using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	public class ActivityTagsCollection : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004AD8 File Offset: 0x00002CD8
		public ActivityTagsCollection()
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004AEC File Offset: 0x00002CEC
		public ActivityTagsCollection([Nullable(new byte[] { 1, 0, 1, 2 })] IEnumerable<KeyValuePair<string, object>> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			foreach (KeyValuePair<string, object> keyValuePair in list)
			{
				if (keyValuePair.Key != null)
				{
					this[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x1700003F RID: 63
		[Nullable(2)]
		public object this[string key]
		{
			[return: Nullable(2)]
			get
			{
				int num = this.FindIndex(key);
				if (num >= 0)
				{
					return this._list[num].Value;
				}
				return null;
			}
			[param: Nullable(2)]
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = this.FindIndex(key);
				if (value == null)
				{
					if (num >= 0)
					{
						this._list.RemoveAt(num);
					}
					return;
				}
				if (num >= 0)
				{
					this._list[num] = new KeyValuePair<string, object>(key, value);
					return;
				}
				this._list.Add(new KeyValuePair<string, object>(key, value));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004C00 File Offset: 0x00002E00
		public ICollection<string> Keys
		{
			get
			{
				List<string> list = new List<string>(this._list.Count);
				foreach (KeyValuePair<string, object> keyValuePair in this._list)
				{
					list.Add(keyValuePair.Key);
				}
				return list;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004C6C File Offset: 0x00002E6C
		[Nullable(new byte[] { 1, 2 })]
		public ICollection<object> Values
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				List<object> list = new List<object>(this._list.Count);
				foreach (KeyValuePair<string, object> keyValuePair in this._list)
				{
					list.Add(keyValuePair.Value);
				}
				return list;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004CDB File Offset: 0x00002EDB
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public void Add(string key, [Nullable(2)] object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.FindIndex(key);
			if (num >= 0)
			{
				throw new InvalidOperationException(SR.Format(SR.KeyAlreadyExist, key));
			}
			this._list.Add(new KeyValuePair<string, object>(key, value));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004D34 File Offset: 0x00002F34
		public void Add([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			if (item.Key == null)
			{
				throw new ArgumentNullException("item");
			}
			int num = this.FindIndex(item.Key);
			if (num >= 0)
			{
				throw new InvalidOperationException(SR.Format(SR.KeyAlreadyExist, item.Key));
			}
			this._list.Add(item);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004D8A File Offset: 0x00002F8A
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004D97 File Offset: 0x00002F97
		public bool Contains([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004DA5 File Offset: 0x00002FA5
		public bool ContainsKey(string key)
		{
			return this.FindIndex(key) >= 0;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public void CopyTo([Nullable(new byte[] { 1, 0, 1, 2 })] KeyValuePair<string, object>[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004DC3 File Offset: 0x00002FC3
		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return new ActivityTagsCollection.Enumerator(this._list);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004DD5 File Offset: 0x00002FD5
		public ActivityTagsCollection.Enumerator GetEnumerator()
		{
			return new ActivityTagsCollection.Enumerator(this._list);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004DE2 File Offset: 0x00002FE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ActivityTagsCollection.Enumerator(this._list);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.FindIndex(key);
			if (num >= 0)
			{
				this._list.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004E2A File Offset: 0x0000302A
		public bool Remove([Nullable(new byte[] { 0, 1, 2 })] KeyValuePair<string, object> item)
		{
			return this._list.Remove(item);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004E38 File Offset: 0x00003038
		public bool TryGetValue(string key, [Nullable(2)] out object value)
		{
			int num = this.FindIndex(key);
			if (num >= 0)
			{
				value = this._list[num].Value;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004E70 File Offset: 0x00003070
		private int FindIndex(string key)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				if (this._list[i].Key == key)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000054 RID: 84
		private List<KeyValuePair<string, object>> _list = new List<KeyValuePair<string, object>>();

		// Token: 0x02000080 RID: 128
		[NullableContext(0)]
		public struct Enumerator : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
		{
			// Token: 0x0600033C RID: 828 RVA: 0x0000CBF7 File Offset: 0x0000ADF7
			internal Enumerator(List<KeyValuePair<string, object>> list)
			{
				this._enumerator = list.GetEnumerator();
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x0600033D RID: 829 RVA: 0x0000CC05 File Offset: 0x0000AE05
			[Nullable(new byte[] { 0, 1, 2 })]
			public KeyValuePair<string, object> Current
			{
				[return: Nullable(new byte[] { 0, 1, 2 })]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0000CC12 File Offset: 0x0000AE12
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator)this._enumerator).Current;
				}
			}

			// Token: 0x0600033F RID: 831 RVA: 0x0000CC24 File Offset: 0x0000AE24
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000CC31 File Offset: 0x0000AE31
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000CC3E File Offset: 0x0000AE3E
			void IEnumerator.Reset()
			{
				((IEnumerator)this._enumerator).Reset();
			}

			// Token: 0x04000193 RID: 403
			private List<KeyValuePair<string, object>>.Enumerator _enumerator;
		}
	}
}
