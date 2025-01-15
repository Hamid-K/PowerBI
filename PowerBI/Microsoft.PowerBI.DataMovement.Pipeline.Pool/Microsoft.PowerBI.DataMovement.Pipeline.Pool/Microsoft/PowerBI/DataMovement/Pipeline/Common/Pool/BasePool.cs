using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Pool
{
	// Token: 0x0200000A RID: 10
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BasePool<[global::System.Runtime.CompilerServices.Nullable(2)] TObjectKey, [global::System.Runtime.CompilerServices.Nullable(2)] TPoolObjectKey, [global::System.Runtime.CompilerServices.Nullable(0)] TObject> : IDisposable where TObject : IDisposable
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002199 File Offset: 0x00000399
		protected BasePool(int minItems, int maxItems)
			: this(minItems, maxItems, TimeSpan.MaxValue)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021A8 File Offset: 0x000003A8
		protected BasePool(int minItems, int maxItems, TimeSpan oldestAllowed)
		{
			this.m_sortedList = new List<PoolObject<TPoolObjectKey, TObject>>();
			this.m_minItems = minItems;
			this.m_maxItems = maxItems;
			this.m_oldestAllowed = oldestAllowed;
			this.m_disposed = false;
			Task.Factory.StartNew<Task>(async delegate
			{
				await this.BalanceAndShrinkAsync();
			}, this.m_balanceAndShrinkCancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default).DoNotWait();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000221C File Offset: 0x0000041C
		~BasePool()
		{
			this.Dispose(false);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000224C File Offset: 0x0000044C
		protected int MinItems
		{
			get
			{
				return this.m_minItems;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002254 File Offset: 0x00000454
		protected int MaxItems
		{
			get
			{
				return this.m_maxItems;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000225C File Offset: 0x0000045C
		protected TimeSpan OldestAllowed
		{
			get
			{
				return this.m_oldestAllowed;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002264 File Offset: 0x00000464
		protected int Count
		{
			get
			{
				return this.m_sortedList.Count;
			}
		}

		// Token: 0x0600001B RID: 27
		internal abstract string DebugDump(int bucketsToDetail);

		// Token: 0x0600001C RID: 28
		protected abstract Task<TObject> CreateObjectInstance(TObjectKey key);

		// Token: 0x0600001D RID: 29
		protected abstract void BalanceAndShrinkIteration();

		// Token: 0x0600001E RID: 30 RVA: 0x00002271 File Offset: 0x00000471
		protected virtual bool IsValidObject(TObject obj)
		{
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002274 File Offset: 0x00000474
		protected virtual bool IsPoolableObject(TObject obj)
		{
			return true;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002277 File Offset: 0x00000477
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002286 File Offset: 0x00000486
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.Stop();
				this.m_disposed = true;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022A0 File Offset: 0x000004A0
		protected async Task<PoolObject<TPoolObjectKey, TObject>> CreatePoolObject(TPoolObjectKey poolKey, TObjectKey objectKey)
		{
			TObject tobject = await this.CreateObjectInstance(objectKey);
			return new PoolObject<TPoolObjectKey, TObject>(poolKey, tobject, delegate(PoolObject<TPoolObjectKey, TObject> po)
			{
				this.CleanupPoolObject(po);
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022F3 File Offset: 0x000004F3
		private void Stop()
		{
			this.m_balanceAndShrinkCancellationTokenSource.Cancel();
			this.m_balanceAndShrinkCancellationTokenSource.Dispose();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000230C File Offset: 0x0000050C
		private async Task BalanceAndShrinkAsync()
		{
			for (;;)
			{
				await Task.Delay(TimeSpan.FromSeconds(30.0));
				this.BalanceAndShrinkIteration();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002350 File Offset: 0x00000550
		protected List<PoolObject<TPoolObjectKey, TObject>> CollectForDeletionNotSynchronized(out DateTime cutoffDate)
		{
			List<PoolObject<TPoolObjectKey, TObject>> list = new List<PoolObject<TPoolObjectKey, TObject>>();
			DateTime utcNow = DateTime.UtcNow;
			cutoffDate = DateTime.MinValue;
			while (this.m_sortedList.Count > 0)
			{
				PoolObject<TPoolObjectKey, TObject> poolObject = this.m_sortedList[0];
				if ((this.m_minItems < 0 || this.m_sortedList.Count <= this.m_minItems) && poolObject.LastAccessed != cutoffDate && (this.m_oldestAllowed == TimeSpan.MaxValue || poolObject.LastAccessed.Add(this.m_oldestAllowed) > utcNow))
				{
					break;
				}
				cutoffDate = poolObject.LastAccessed;
				list.Add(poolObject);
				this.m_sortedList.RemoveAt(0);
			}
			return list;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002414 File Offset: 0x00000614
		protected void AddToSortedList(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
			this.m_sortedList.Add(poolObject);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002424 File Offset: 0x00000624
		protected void DeleteFromSortedList(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
			int num = 0;
			foreach (PoolObject<TPoolObjectKey, TObject> poolObject2 in this.m_sortedList)
			{
				if (poolObject == poolObject2)
				{
					this.m_sortedList.RemoveAt(num);
					return;
				}
				num++;
			}
			TraceSourceBase<PoolTraceSource>.Tracer.TraceWarning("Bug: object missing from the sorted list!");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002498 File Offset: 0x00000698
		protected virtual void CleanupPoolObject(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000249C File Offset: 0x0000069C
		protected void SafeCleanup(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
			try
			{
				poolObject.Dispose();
			}
			catch (Exception ex)
			{
				TraceSourceBase<PoolTraceSource>.Tracer.TraceError("Unexpected error trying to dispose a pool object: {0}", new object[] { ex.Message });
			}
		}

		// Token: 0x04000017 RID: 23
		private const int CleanerIntervalInSeconds = 30;

		// Token: 0x04000018 RID: 24
		private readonly List<PoolObject<TPoolObjectKey, TObject>> m_sortedList;

		// Token: 0x04000019 RID: 25
		private readonly int m_minItems;

		// Token: 0x0400001A RID: 26
		private readonly int m_maxItems;

		// Token: 0x0400001B RID: 27
		private readonly TimeSpan m_oldestAllowed;

		// Token: 0x0400001C RID: 28
		private readonly CancellationTokenSource m_balanceAndShrinkCancellationTokenSource = new CancellationTokenSource();

		// Token: 0x0400001D RID: 29
		private bool m_disposed;
	}
}
