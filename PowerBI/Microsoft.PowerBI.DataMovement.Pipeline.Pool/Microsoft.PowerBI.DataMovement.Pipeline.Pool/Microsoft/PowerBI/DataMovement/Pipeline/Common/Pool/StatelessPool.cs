using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.ExceptionUtilities;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Pool
{
	// Token: 0x0200000C RID: 12
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })]
	internal abstract class StatelessPool<[global::System.Runtime.CompilerServices.Nullable(2)] TObjectKey, [global::System.Runtime.CompilerServices.Nullable(0)] TObject> : BasePool<TObjectKey, TObjectKey, TObject> where TObject : IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000029F4 File Offset: 0x00000BF4
		protected StatelessPool(int minItems, int maxItems, TimeSpan oldestAllowed, int lookaheadProactiveCreation)
			: base(minItems, maxItems, oldestAllowed)
		{
			this.m_buckets = new Dictionary<TObjectKey, PoolBucket<TObjectKey, TObject>>();
			this.m_lookaheadProactiveCreation = lookaheadProactiveCreation;
			this.m_inProgressLookaheadCreation = new Dictionary<TObjectKey, int>();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A34 File Offset: 0x00000C34
		internal override string DebugDump(int bucketsToDetail)
		{
			string text = string.Empty;
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				text += "==============================================================================\r\n";
				text += string.Format(CultureInfo.InvariantCulture, "{0} pool metadata: min={1}, max={2}, expire={3}\r\n", new object[]
				{
					base.GetType().Name,
					base.MinItems,
					base.MaxItems,
					base.OldestAllowed
				});
				text += string.Format(CultureInfo.InvariantCulture, "Object count: {0}\r\n", base.Count);
				text += string.Format(CultureInfo.InvariantCulture, "Distinct count: {0}\r\n", this.m_buckets.Count);
				SortedList<int, TObjectKey> sortedList = new SortedList<int, TObjectKey>(this.m_buckets.Count, OneWayDuplicateKeyComparer<int>.Instance);
				foreach (KeyValuePair<TObjectKey, PoolBucket<TObjectKey, TObject>> keyValuePair in this.m_buckets)
				{
					sortedList.Add(keyValuePair.Value.Queue.Count, keyValuePair.Key);
				}
				if (0 < this.m_buckets.Count)
				{
					text += "------------------------------------------------------------------------------\r\n";
					text += string.Format(CultureInfo.InvariantCulture, "Bucket dump ({0} buckets max):\r\n", bucketsToDetail);
					int num = this.m_buckets.Count - 1;
					int num2 = 0;
					while (num >= 0 && num2 < bucketsToDetail)
					{
						text += string.Format(CultureInfo.InvariantCulture, "  {0:D3}: {1} [{2} connections]\r\n", num2, sortedList.Values[num], this.m_buckets[sortedList.Values[num]].Queue.Count);
						num--;
						num2++;
					}
				}
				text += "==============================================================================";
			}
			return text;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C6C File Offset: 0x00000E6C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!this.m_disposed)
			{
				if (disposing)
				{
					this.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C90 File Offset: 0x00000E90
		public virtual async Task<PoolObject<TObjectKey, TObject>> Get(TObjectKey key, bool forceNew = false)
		{
			PoolObject<TObjectKey, TObject> poolObject = null;
			object bucketsLock = this.m_bucketsLock;
			int willProactivelyCreate;
			lock (bucketsLock)
			{
				willProactivelyCreate = this.m_lookaheadProactiveCreation;
				PoolBucket<TObjectKey, TObject> poolBucket;
				if (this.m_buckets.TryGetValue(key, out poolBucket))
				{
					if (forceNew)
					{
						willProactivelyCreate -= poolBucket.Queue.Count;
					}
					else
					{
						PoolObject<TObjectKey, TObject> poolObject2;
						while (poolBucket.Queue.Count > 0 && poolBucket.Queue.TryDequeue(out poolObject2))
						{
							base.DeleteFromSortedList(poolObject2);
							if (this.IsValidObject(poolObject2.Object))
							{
								willProactivelyCreate -= poolBucket.Queue.Count;
								poolObject = poolObject2;
								break;
							}
							base.SafeCleanup(poolObject2);
						}
					}
				}
				willProactivelyCreate -= this.GetCurrentLookaheadCreationCount(key);
				if (willProactivelyCreate > 0)
				{
					this.RegisterLookaheadCreations(key, willProactivelyCreate);
				}
			}
			if (poolObject == null)
			{
				poolObject = await base.CreatePoolObject(key, key);
			}
			for (int i = 0; i < willProactivelyCreate; i++)
			{
				try
				{
					this.StartProactivePoolCreation(key).DoNotWait();
				}
				catch (Exception ex)
				{
					if (ex.IsFatal())
					{
						throw;
					}
					this.DeregisterLookaheadCreation(key);
					TraceSourceBase<PoolTraceSource>.Tracer.TraceError("Unexpected error creating look ahead pool items: {0}", new object[] { ex });
					break;
				}
			}
			return poolObject;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CE4 File Offset: 0x00000EE4
		protected virtual Task StartProactivePoolCreation(TObjectKey key)
		{
			StatelessPool<TObjectKey, TObject>.<>c__DisplayClass10_0 CS$<>8__locals1 = new StatelessPool<TObjectKey, TObject>.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.key = key;
			return Task.Factory.StartNew<Task>(delegate
			{
				StatelessPool<TObjectKey, TObject>.<>c__DisplayClass10_0.<<StartProactivePoolCreation>b__0>d <<StartProactivePoolCreation>b__0>d;
				<<StartProactivePoolCreation>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<StartProactivePoolCreation>b__0>d.<>4__this = CS$<>8__locals1;
				<<StartProactivePoolCreation>b__0>d.<>1__state = -1;
				<<StartProactivePoolCreation>b__0>d.<>t__builder.Start<StatelessPool<TObjectKey, TObject>.<>c__DisplayClass10_0.<<StartProactivePoolCreation>b__0>d>(ref <<StartProactivePoolCreation>b__0>d);
				return <<StartProactivePoolCreation>b__0>d.<>t__builder.Task;
			}, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D28 File Offset: 0x00000F28
		public void Return(PoolObject<TObjectKey, TObject> poolObject)
		{
			RuntimeChecks.CheckNotDisposed(this.m_disposed, this);
			if (poolObject == null)
			{
				return;
			}
			if (!this.IsValidObject(poolObject.Object) || !this.IsPoolableObject(poolObject.Object))
			{
				poolObject.Dispose();
				return;
			}
			PoolObject<TObjectKey, TObject> poolObject2 = null;
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				if (base.Count > base.MaxItems - 1)
				{
					poolObject2 = poolObject;
				}
				else
				{
					PoolBucket<TObjectKey, TObject> poolBucket;
					if (!this.m_buckets.TryGetValue(poolObject.Key, out poolBucket))
					{
						poolBucket = (this.m_buckets[poolObject.Key] = new PoolBucket<TObjectKey, TObject>());
					}
					poolObject.MarkAccess();
					poolBucket.Queue.Enqueue(poolObject);
					base.AddToSortedList(poolObject);
				}
			}
			if (poolObject2 != null)
			{
				base.SafeCleanup(poolObject2);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DFC File Offset: 0x00000FFC
		private int GetCurrentLookaheadCreationCount(TObjectKey key)
		{
			object inProgressLookaheadCreationLock = this.m_inProgressLookaheadCreationLock;
			int num;
			lock (inProgressLookaheadCreationLock)
			{
				int num2;
				num = (this.m_inProgressLookaheadCreation.TryGetValue(key, out num2) ? num2 : 0);
			}
			return num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E4C File Offset: 0x0000104C
		private void RegisterLookaheadCreations(TObjectKey key, int countToRegister)
		{
			object inProgressLookaheadCreationLock = this.m_inProgressLookaheadCreationLock;
			lock (inProgressLookaheadCreationLock)
			{
				int num = this.GetCurrentLookaheadCreationCount(key) + countToRegister;
				this.m_inProgressLookaheadCreation[key] = num;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EA0 File Offset: 0x000010A0
		private void DeregisterLookaheadCreation(TObjectKey key)
		{
			object inProgressLookaheadCreationLock = this.m_inProgressLookaheadCreationLock;
			lock (inProgressLookaheadCreationLock)
			{
				int num = this.GetCurrentLookaheadCreationCount(key) - 1;
				if (num == 0)
				{
					this.m_inProgressLookaheadCreation.Remove(key);
				}
				else
				{
					this.m_inProgressLookaheadCreation[key] = num;
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F04 File Offset: 0x00001104
		private async Task ProactivePoolingCreation(object stateInfo)
		{
			StatelessPool<TObjectKey, TObject>.ProactivePoolingState proactivePoolingState = (StatelessPool<TObjectKey, TObject>.ProactivePoolingState)stateInfo;
			try
			{
				PoolObject<TObjectKey, TObject> poolObject = await base.CreatePoolObject(proactivePoolingState.Key, proactivePoolingState.Key);
				this.Return(poolObject);
			}
			finally
			{
				this.DeregisterLookaheadCreation(proactivePoolingState.Key);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F50 File Offset: 0x00001150
		protected override void BalanceAndShrinkIteration()
		{
			try
			{
				int num = -1;
				int num2 = -1;
				List<PoolObject<TObjectKey, TObject>> list = null;
				object bucketsLock = this.m_bucketsLock;
				lock (bucketsLock)
				{
					DateTime dateTime;
					list = base.CollectForDeletionNotSynchronized(out dateTime);
					foreach (PoolBucket<TObjectKey, TObject> poolBucket in this.m_buckets.Values)
					{
						ConcurrentQueue<PoolObject<TObjectKey, TObject>> queue = poolBucket.Queue;
						PoolObject<TObjectKey, TObject> poolObject;
						while (queue.Count > 0 && queue.TryPeek(out poolObject) && !(poolObject.LastAccessed > dateTime))
						{
							PoolObject<TObjectKey, TObject> poolObject2;
							queue.TryDequeue(out poolObject2);
						}
					}
					List<TObjectKey> list2 = new List<TObjectKey>();
					foreach (TObjectKey tobjectKey in this.m_buckets.Keys)
					{
						if (this.m_buckets[tobjectKey].Queue.Count == 0)
						{
							list2.Add(tobjectKey);
						}
					}
					foreach (TObjectKey tobjectKey2 in list2)
					{
						this.m_buckets.Remove(tobjectKey2);
					}
					num = base.Count;
					num2 = this.m_buckets.Count;
				}
				foreach (PoolObject<TObjectKey, TObject> poolObject3 in list)
				{
					base.SafeCleanup(poolObject3);
				}
				TraceSourceBase<PoolTraceSource>.Tracer.TraceInformation(string.Format(CultureInfo.InvariantCulture, "Pool cleaner connections removed: {0}, count: {1}, buckets: {2}", list.Count, num, num2));
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch
			{
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031BC File Offset: 0x000013BC
		public void Clear()
		{
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				foreach (PoolBucket<TObjectKey, TObject> poolBucket in this.m_buckets.Values)
				{
					PoolObject<TObjectKey, TObject> poolObject;
					while (poolBucket.Queue.Count > 0 && poolBucket.Queue.TryDequeue(out poolObject))
					{
						base.DeleteFromSortedList(poolObject);
						base.SafeCleanup(poolObject);
					}
				}
			}
		}

		// Token: 0x04000022 RID: 34
		private readonly int m_lookaheadProactiveCreation;

		// Token: 0x04000023 RID: 35
		private readonly object m_inProgressLookaheadCreationLock = new object();

		// Token: 0x04000024 RID: 36
		private Dictionary<TObjectKey, int> m_inProgressLookaheadCreation;

		// Token: 0x04000025 RID: 37
		private bool m_disposed;

		// Token: 0x04000026 RID: 38
		protected readonly object m_bucketsLock = new object();

		// Token: 0x04000027 RID: 39
		protected readonly Dictionary<TObjectKey, PoolBucket<TObjectKey, TObject>> m_buckets;

		// Token: 0x02000011 RID: 17
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class ProactivePoolingState
		{
			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600004B RID: 75 RVA: 0x0000369E File Offset: 0x0000189E
			// (set) Token: 0x0600004C RID: 76 RVA: 0x000036A6 File Offset: 0x000018A6
			internal TObjectKey Key { get; set; }
		}
	}
}
