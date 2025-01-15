using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F6 RID: 502
	internal abstract class ExpandablePool<T> : Pool<T> where T : class
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00036CC7 File Offset: 0x00034EC7
		public ExpandablePool(long initialPoolSize, long maxPoolSize, int percentGrowth)
			: base(maxPoolSize)
		{
			this._currentLoad = initialPoolSize;
			this._percentGrowth = percentGrowth;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00036CE0 File Offset: 0x00034EE0
		internal override T GetObjectFromPool()
		{
			T t = base.GetObjectFromPool();
			while (t == null && this.CanGrow())
			{
				long num = 0L;
				if (Monitor.TryEnter(this))
				{
					try
					{
						t = base.GetObjectFromPool();
						while (t == null && this.CanGrow())
						{
							num += this.GrowInternal();
							t = base.GetObjectFromPool();
						}
						if (t != null)
						{
							return t;
						}
					}
					finally
					{
						Monitor.Exit(this);
						if (Provider.IsEnabled(TraceLevel.Warning))
						{
							EventLogWriter.WriteWarning(this.LogSource, "Pool Grown(Sync) by {0}, CurrentCapacity {1}, MaxCapacity {2} ", new object[] { num, this._currentLoad, base.Size });
						}
						EventLogProvider.EventWritePoolGrowth(this.LogSource, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, this.PoolName), this.CurrentCapacity, this.MaxCapacity, num);
					}
				}
				Thread.Sleep(1);
				t = base.GetObjectFromPool();
			}
			return t;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00036DEC File Offset: 0x00034FEC
		internal long Grow()
		{
			long num = 0L;
			lock (this)
			{
				if (this.CanGrow())
				{
					num = this.GrowInternal();
				}
			}
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this.LogSource, "Pool Grown(ASync) by {0}, CurrentCapacity {1}, MaxCapacity {2} ", new object[] { num, this._currentLoad, base.Size });
			}
			EventLogProvider.EventWritePoolGrowth(this.LogSource, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, this.PoolName), this.CurrentCapacity, this.MaxCapacity, num);
			return num;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00036EA4 File Offset: 0x000350A4
		private long GrowInternal()
		{
			long num = (long)Math.Ceiling((double)base.Size / 100.0 * (double)this._percentGrowth);
			if (num + this._currentLoad > base.Size)
			{
				num = base.Size - this._currentLoad;
			}
			this.LoadPool(num);
			this._currentLoad += num;
			return num;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00036F05 File Offset: 0x00035105
		private bool CanGrow()
		{
			return base.Size - this._currentLoad > 0L;
		}

		// Token: 0x0600106F RID: 4207
		internal abstract void LoadPool(long itemCount);

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00036F18 File Offset: 0x00035118
		public long AvailableObjects
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00036F20 File Offset: 0x00035120
		public long CurrentCapacity
		{
			get
			{
				return this._currentLoad;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00036F28 File Offset: 0x00035128
		public long MaxCapacity
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x04000AC6 RID: 2758
		private long _currentLoad;

		// Token: 0x04000AC7 RID: 2759
		private int _percentGrowth;
	}
}
