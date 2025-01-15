using System;
using System.Collections.Generic;

namespace System.Diagnostics
{
	// Token: 0x02000027 RID: 39
	internal sealed class SynchronizedList<T>
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00005A10 File Offset: 0x00003C10
		public SynchronizedList()
		{
			this._list = new List<T>();
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005A24 File Offset: 0x00003C24
		public void Add(T item)
		{
			List<T> list = this._list;
			lock (list)
			{
				this._list.Add(item);
				this._version += 1U;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005A78 File Offset: 0x00003C78
		public bool AddIfNotExist(T item)
		{
			List<T> list = this._list;
			bool flag2;
			lock (list)
			{
				if (!this._list.Contains(item))
				{
					this._list.Add(item);
					this._version += 1U;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public bool Remove(T item)
		{
			List<T> list = this._list;
			bool flag2;
			lock (list)
			{
				if (this._list.Remove(item))
				{
					this._version += 1U;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005B44 File Offset: 0x00003D44
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005B54 File Offset: 0x00003D54
		public void EnumWithFunc<TParent>(ActivitySource.Function<T, TParent> func, ref ActivityCreationOptions<TParent> data, ref ActivitySamplingResult samplingResult, ref ActivityCreationOptions<ActivityContext> dataWithContext)
		{
			uint num = this._version;
			int i = 0;
			while (i < this._list.Count)
			{
				List<T> list = this._list;
				T t;
				lock (list)
				{
					if (num != this._version)
					{
						num = this._version;
						i = 0;
						continue;
					}
					t = this._list[i];
					i++;
				}
				func(t, ref data, ref samplingResult, ref dataWithContext);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005BDC File Offset: 0x00003DDC
		public void EnumWithAction(Action<T, object> action, object arg)
		{
			uint num = this._version;
			int i = 0;
			while (i < this._list.Count)
			{
				List<T> list = this._list;
				T t;
				lock (list)
				{
					if (num != this._version)
					{
						num = this._version;
						i = 0;
						continue;
					}
					t = this._list[i];
					i++;
				}
				action(t, arg);
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly List<T> _list;

		// Token: 0x0400007F RID: 127
		private uint _version;
	}
}
