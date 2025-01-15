using System;
using System.Threading;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001147 RID: 4423
	internal sealed class TimeToLivePool : IPool, IDisposable
	{
		// Token: 0x060073DA RID: 29658 RVA: 0x0018E54C File Offset: 0x0018C74C
		public TimeToLivePool(IPool pool, TimeSpan timeToLive)
		{
			this.syncRoot = new object();
			this.pool = pool;
			this.timeToLive = timeToLive;
			this.timer = new Timer(new TimerCallback(this.OnTimer), null, TimeSpan.Zero, TimeToLivePool.GetPollingInterval(timeToLive));
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x0018E59C File Offset: 0x0018C79C
		public bool TryGet(string key, out IPoolable poolable)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				if (this.pool.TryGet(key, out poolable))
				{
					poolable = ((TimeToLivePool.TimeToLivePoolable)poolable).Poolable;
					flag2 = true;
				}
				else
				{
					poolable = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x060073DC RID: 29660 RVA: 0x0018E600 File Offset: 0x0018C800
		public void Add(IPoolable poolable)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.pool.Add(new TimeToLivePool.TimeToLivePoolable(this, poolable));
			}
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x0018E64C File Offset: 0x0018C84C
		public void Purge()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.pool.Purge();
			}
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x0018E694 File Offset: 0x0018C894
		public void Clear()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.pool.Clear();
			}
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x0018E6DC File Offset: 0x0018C8DC
		public void Dispose()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.pool.Dispose();
			}
			this.timer.Dispose();
		}

		// Token: 0x060073E0 RID: 29664 RVA: 0x0018E72C File Offset: 0x0018C92C
		private static TimeSpan GetPollingInterval(TimeSpan timeSpan)
		{
			return TimeSpan.FromTicks(Math.Max(Math.Min((long)((double)timeSpan.Ticks * 0.25), TimeToLivePool.maxPollingInterval.Ticks), TimeToLivePool.minPollingInterval.Ticks));
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x0018E778 File Offset: 0x0018C978
		private void OnTimer(object obj)
		{
			object obj2 = this.syncRoot;
			lock (obj2)
			{
				this.pool.Purge();
			}
		}

		// Token: 0x04003FC8 RID: 16328
		private const double pollingRatio = 0.25;

		// Token: 0x04003FC9 RID: 16329
		private static readonly TimeSpan minPollingInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x04003FCA RID: 16330
		private static readonly TimeSpan maxPollingInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04003FCB RID: 16331
		private readonly object syncRoot;

		// Token: 0x04003FCC RID: 16332
		private readonly IPool pool;

		// Token: 0x04003FCD RID: 16333
		private readonly TimeSpan timeToLive;

		// Token: 0x04003FCE RID: 16334
		private readonly Timer timer;

		// Token: 0x02001148 RID: 4424
		private class TimeToLivePoolable : IPoolable, IDisposable
		{
			// Token: 0x060073E3 RID: 29667 RVA: 0x0018E7E8 File Offset: 0x0018C9E8
			public TimeToLivePoolable(TimeToLivePool pool, IPoolable poolable)
			{
				this.pool = pool;
				this.poolable = poolable;
				this.pooledAt = DateTime.UtcNow;
			}

			// Token: 0x1700204D RID: 8269
			// (get) Token: 0x060073E4 RID: 29668 RVA: 0x0018E809 File Offset: 0x0018CA09
			public IPoolable Poolable
			{
				get
				{
					return this.poolable;
				}
			}

			// Token: 0x1700204E RID: 8270
			// (get) Token: 0x060073E5 RID: 29669 RVA: 0x0018E811 File Offset: 0x0018CA11
			public string Key
			{
				get
				{
					return this.poolable.Key;
				}
			}

			// Token: 0x1700204F RID: 8271
			// (get) Token: 0x060073E6 RID: 29670 RVA: 0x0018E81E File Offset: 0x0018CA1E
			public bool IsValid
			{
				get
				{
					return this.poolable.IsValid && DateTime.UtcNow - this.pooledAt <= this.pool.timeToLive;
				}
			}

			// Token: 0x060073E7 RID: 29671 RVA: 0x0018E84F File Offset: 0x0018CA4F
			public void Dispose()
			{
				this.poolable.Dispose();
			}

			// Token: 0x04003FCF RID: 16335
			private readonly TimeToLivePool pool;

			// Token: 0x04003FD0 RID: 16336
			private readonly IPoolable poolable;

			// Token: 0x04003FD1 RID: 16337
			private readonly DateTime pooledAt;
		}
	}
}
