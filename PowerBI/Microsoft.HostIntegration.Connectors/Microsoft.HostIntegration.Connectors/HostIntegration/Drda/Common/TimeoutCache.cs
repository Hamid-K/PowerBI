using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200083F RID: 2111
	public class TimeoutCache<Tkey, Tval>
	{
		// Token: 0x06004313 RID: 17171 RVA: 0x000E0CC6 File Offset: 0x000DEEC6
		public TimeoutCache()
		{
			this.cache = new Dictionary<Tkey, Tuple<Tval, DateTime>>();
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x000E0CEC File Offset: 0x000DEEEC
		public int Count
		{
			get
			{
				this.cacheLock.EnterReadLock();
				int count;
				try
				{
					count = this.cache.Count;
				}
				finally
				{
					this.cacheLock.ExitReadLock();
				}
				return count;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x000E0D39 File Offset: 0x000DEF39
		// (set) Token: 0x06004315 RID: 17173 RVA: 0x000E0D30 File Offset: 0x000DEF30
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
			}
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000E0D44 File Offset: 0x000DEF44
		public bool ContainsKey(Tkey key)
		{
			this.cacheLock.EnterUpgradeableReadLock();
			bool flag;
			try
			{
				if (this.cache.ContainsKey(key))
				{
					if (DateTime.Now.CompareTo(this.cache[key].Item2) < 0)
					{
						return true;
					}
					this.cacheLock.EnterWriteLock();
					try
					{
						this.cache.Remove(key);
					}
					finally
					{
						this.cacheLock.ExitWriteLock();
					}
				}
				flag = false;
			}
			finally
			{
				this.cacheLock.ExitUpgradeableReadLock();
			}
			return flag;
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000E0DE4 File Offset: 0x000DEFE4
		public bool Add(Tkey key, Tuple<Tval, DateTime> val)
		{
			this.cacheLock.EnterWriteLock();
			bool flag;
			try
			{
				if (this.maxSize < 0 || this.cache.Count < this.maxSize)
				{
					this.cache[key] = val;
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
			return flag;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000E0E4C File Offset: 0x000DF04C
		public void Remove(Tkey key)
		{
			this.cacheLock.EnterWriteLock();
			try
			{
				this.cache.Remove(key);
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000E0E90 File Offset: 0x000DF090
		public void Clear()
		{
			this.cacheLock.EnterWriteLock();
			try
			{
				this.cache.Clear();
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000E0ED4 File Offset: 0x000DF0D4
		public Tval Get(Tkey key)
		{
			this.cacheLock.EnterReadLock();
			Tval tval;
			try
			{
				if (this.cache.ContainsKey(key))
				{
					tval = this.cache[key].Item1;
				}
				else
				{
					tval = default(Tval);
				}
			}
			finally
			{
				this.cacheLock.ExitReadLock();
			}
			return tval;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x000E0F38 File Offset: 0x000DF138
		public Tval GetAndRemove(Tkey key)
		{
			this.cacheLock.ExitUpgradeableReadLock();
			Tval tval;
			try
			{
				if (this.cache.ContainsKey(key))
				{
					Tval item = this.cache[key].Item1;
					this.cacheLock.EnterWriteLock();
					try
					{
						this.cache.Remove(key);
					}
					finally
					{
						this.cacheLock.ExitWriteLock();
					}
					tval = item;
				}
				else
				{
					tval = default(Tval);
				}
			}
			finally
			{
				this.cacheLock.ExitUpgradeableReadLock();
			}
			return tval;
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000E0FD0 File Offset: 0x000DF1D0
		public void Flush()
		{
			this.cacheLock.EnterWriteLock();
			try
			{
				IEnumerable<Tkey> keys = this.cache.Keys;
				List<Tkey> list = new List<Tkey>();
				foreach (Tkey tkey in keys)
				{
					if (DateTime.Now.CompareTo(this.cache[tkey].Item2) >= 0)
					{
						list.Add(tkey);
					}
				}
				foreach (Tkey tkey2 in list)
				{
					this.cache.Remove(tkey2);
				}
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
		}

		// Token: 0x04002F5B RID: 12123
		protected ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();

		// Token: 0x04002F5C RID: 12124
		protected Dictionary<Tkey, Tuple<Tval, DateTime>> cache;

		// Token: 0x04002F5D RID: 12125
		protected int maxSize = -1;
	}
}
