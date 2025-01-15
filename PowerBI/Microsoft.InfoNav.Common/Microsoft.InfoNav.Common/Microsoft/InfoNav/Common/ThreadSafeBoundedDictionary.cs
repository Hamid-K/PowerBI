using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000074 RID: 116
	public sealed class ThreadSafeBoundedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000AB32 File Offset: 0x00008D32
		public ThreadSafeBoundedDictionary(int compacity, IEqualityComparer<TKey> comparer)
		{
			this._capacity = compacity;
			this._dictionary = new Dictionary<TKey, TValue>(compacity, comparer);
			this._lock = new ReaderWriterLockSlim();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000AB59 File Offset: 0x00008D59
		public ThreadSafeBoundedDictionary(int capacity)
			: this(capacity, EqualityComparer<TKey>.Default)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000AB68 File Offset: 0x00008D68
		public int Count
		{
			get
			{
				this._lock.EnterReadLock();
				int count;
				try
				{
					count = this._dictionary.Count;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public bool IsReadOnly
		{
			get
			{
				this._lock.EnterReadLock();
				bool isReadOnly;
				try
				{
					isReadOnly = this._dictionary.IsReadOnly;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return isReadOnly;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		public bool IsFull
		{
			get
			{
				this._lock.EnterReadLock();
				bool flag;
				try
				{
					flag = this._dictionary.Count > this._capacity;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return flag;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public ICollection<TKey> Keys
		{
			get
			{
				this._lock.EnterReadLock();
				ICollection<TKey> keys;
				try
				{
					keys = this._dictionary.Keys;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return keys;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000AC80 File Offset: 0x00008E80
		public ICollection<TValue> Values
		{
			get
			{
				this._lock.EnterReadLock();
				ICollection<TValue> values;
				try
				{
					values = this._dictionary.Values;
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return values;
			}
		}

		// Token: 0x17000062 RID: 98
		public TValue this[TKey key]
		{
			get
			{
				this._lock.EnterReadLock();
				TValue tvalue;
				try
				{
					tvalue = this._dictionary[key];
				}
				finally
				{
					this._lock.ExitReadLock();
				}
				return tvalue;
			}
			set
			{
				this._lock.EnterUpgradeableReadLock();
				try
				{
					if (this._dictionary.ContainsKey(key))
					{
						this._lock.EnterWriteLock();
						try
						{
							this._dictionary[key] = value;
							return;
						}
						finally
						{
							this._lock.ExitWriteLock();
						}
					}
					this.Add(key, value);
				}
				finally
				{
					this._lock.ExitUpgradeableReadLock();
				}
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000AD88 File Offset: 0x00008F88
		public void Add(TKey key, TValue value)
		{
			this._lock.EnterWriteLock();
			try
			{
				if (this._dictionary.Count <= this._capacity)
				{
					this._dictionary.Add(key, value);
				}
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public void Clear()
		{
			this._lock.EnterWriteLock();
			try
			{
				this._dictionary.Clear();
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AE3C File Offset: 0x0000903C
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			this._lock.EnterReadLock();
			bool flag;
			try
			{
				flag = this._dictionary.Contains(item);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AE80 File Offset: 0x00009080
		public bool ContainsKey(TKey key)
		{
			this._lock.EnterReadLock();
			bool flag;
			try
			{
				flag = this._dictionary.ContainsKey(key);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000AEC4 File Offset: 0x000090C4
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this._lock.EnterReadLock();
			try
			{
				this._dictionary.CopyTo(array, arrayIndex);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000AF08 File Offset: 0x00009108
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000AF18 File Offset: 0x00009118
		public bool Remove(TKey key)
		{
			this._lock.EnterWriteLock();
			bool flag;
			try
			{
				flag = this._dictionary.Remove(key);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
			return flag;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000AF5C File Offset: 0x0000915C
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			this._lock.EnterWriteLock();
			bool flag;
			try
			{
				flag = this._dictionary.Remove(item);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
			return flag;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000AFA0 File Offset: 0x000091A0
		public bool TryGetValue(TKey key, out TValue value)
		{
			this._lock.EnterReadLock();
			bool flag;
			try
			{
				flag = this._dictionary.TryGetValue(key, out value);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			return flag;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000AFE8 File Offset: 0x000091E8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x040000EB RID: 235
		private readonly int _capacity;

		// Token: 0x040000EC RID: 236
		private readonly IDictionary<TKey, TValue> _dictionary;

		// Token: 0x040000ED RID: 237
		private readonly ReaderWriterLockSlim _lock;
	}
}
