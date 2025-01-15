using System;
using System.Threading;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000176 RID: 374
	internal abstract class DbReferenceCollection
	{
		// Token: 0x06001BA2 RID: 7074 RVA: 0x00071690 File Offset: 0x0006F890
		protected DbReferenceCollection()
		{
			this._items = new DbReferenceCollection.CollectionEntry[20];
			this._itemLock = new object();
			this._optimisticCount = 0;
			this._lastItemIndex = 0;
		}

		// Token: 0x06001BA3 RID: 7075
		public abstract void Add(object value, int tag);

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000716C0 File Offset: 0x0006F8C0
		protected void AddItem(object value, int tag)
		{
			bool flag = false;
			object itemLock = this._itemLock;
			lock (itemLock)
			{
				for (int i = 0; i <= this._lastItemIndex; i++)
				{
					if (this._items[i].Tag == 0)
					{
						this._items[i].NewTarget(tag, value);
						flag = true;
						break;
					}
				}
				if (!flag && this._lastItemIndex + 1 < this._items.Length)
				{
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
					flag = true;
				}
				if (!flag)
				{
					for (int j = 0; j <= this._lastItemIndex; j++)
					{
						object obj;
						if (!this._items[j].TryGetTarget(out obj))
						{
							this._items[j].NewTarget(tag, value);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					Array.Resize<DbReferenceCollection.CollectionEntry>(ref this._items, this._items.Length * 2);
					this._lastItemIndex++;
					this._items[this._lastItemIndex].NewTarget(tag, value);
				}
				this._optimisticCount++;
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00071814 File Offset: 0x0006FA14
		internal T FindItem<T>(int tag, Func<T, bool> filterMethod) where T : class
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						object obj;
						if (this._items[i].Tag == tag && this._items[i].TryGetTarget(out obj))
						{
							T t = obj as T;
							if (t != null && filterMethod(t))
							{
								return t;
							}
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
			return default(T);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000718BC File Offset: 0x0006FABC
		public void Notify(int message)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag)
				{
					try
					{
						this._isNotifying = true;
						if (this._optimisticCount > 0)
						{
							for (int i = 0; i <= this._lastItemIndex; i++)
							{
								object obj;
								if (this._items[i].TryGetTarget(out obj))
								{
									this.NotifyItem(message, this._items[i].Tag, obj);
									this._items[i].RemoveTarget();
								}
							}
							this._optimisticCount = 0;
						}
						if (this._items.Length > 100)
						{
							this._lastItemIndex = 0;
							this._items = new DbReferenceCollection.CollectionEntry[20];
						}
					}
					finally
					{
						this._isNotifying = false;
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06001BA7 RID: 7079
		protected abstract void NotifyItem(int message, int tag, object value);

		// Token: 0x06001BA8 RID: 7080
		public abstract void Remove(object value);

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00071994 File Offset: 0x0006FB94
		protected void RemoveItem(object value)
		{
			bool flag = false;
			try
			{
				this.TryEnterItemLock(ref flag);
				if (flag && this._optimisticCount > 0)
				{
					for (int i = 0; i <= this._lastItemIndex; i++)
					{
						object obj;
						if (this._items[i].TryGetTarget(out obj) && value == obj)
						{
							this._items[i].RemoveTarget();
							this._optimisticCount--;
							break;
						}
					}
				}
			}
			finally
			{
				this.ExitItemLockIfNeeded(flag);
			}
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00071A1C File Offset: 0x0006FC1C
		private void TryEnterItemLock(ref bool lockObtained)
		{
			lockObtained = false;
			while (!this._isNotifying && !lockObtained)
			{
				Monitor.TryEnter(this._itemLock, 100, ref lockObtained);
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00071A3F File Offset: 0x0006FC3F
		private void ExitItemLockIfNeeded(bool lockObtained)
		{
			if (lockObtained)
			{
				Monitor.Exit(this._itemLock);
			}
		}

		// Token: 0x04000B52 RID: 2898
		private const int LockPollTime = 100;

		// Token: 0x04000B53 RID: 2899
		private const int DefaultCollectionSize = 20;

		// Token: 0x04000B54 RID: 2900
		private DbReferenceCollection.CollectionEntry[] _items;

		// Token: 0x04000B55 RID: 2901
		private readonly object _itemLock;

		// Token: 0x04000B56 RID: 2902
		private int _optimisticCount;

		// Token: 0x04000B57 RID: 2903
		private int _lastItemIndex;

		// Token: 0x04000B58 RID: 2904
		private volatile bool _isNotifying;

		// Token: 0x02000276 RID: 630
		private struct CollectionEntry
		{
			// Token: 0x06001F43 RID: 8003 RVA: 0x0007FBEA File Offset: 0x0007DDEA
			public void NewTarget(int tag, object target)
			{
				if (this._weak == null)
				{
					this._weak = new WeakReference<object>(target, false);
				}
				else
				{
					this._weak.SetTarget(target);
				}
				this._tag = tag;
			}

			// Token: 0x06001F44 RID: 8004 RVA: 0x0007FC16 File Offset: 0x0007DE16
			public void RemoveTarget()
			{
				this._tag = 0;
				this._weak.SetTarget(null);
			}

			// Token: 0x17000A51 RID: 2641
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0007FC2B File Offset: 0x0007DE2B
			public int Tag
			{
				get
				{
					return this._tag;
				}
			}

			// Token: 0x06001F46 RID: 8006 RVA: 0x0007FC33 File Offset: 0x0007DE33
			public bool TryGetTarget(out object target)
			{
				target = null;
				return this._tag != 0 && this._weak.TryGetTarget(out target);
			}

			// Token: 0x04001769 RID: 5993
			private int _tag;

			// Token: 0x0400176A RID: 5994
			private WeakReference<object> _weak;
		}
	}
}
