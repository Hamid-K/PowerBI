using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Pool
{
	// Token: 0x0200000B RID: 11
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })]
	internal abstract class StatefulPool<[global::System.Runtime.CompilerServices.Nullable(2)] TObjectKey, [global::System.Runtime.CompilerServices.Nullable(2)] TPoolObjectKey, [global::System.Runtime.CompilerServices.Nullable(0)] TObject> : BasePool<TObjectKey, TPoolObjectKey, TObject> where TObject : IDisposable
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002530 File Offset: 0x00000730
		protected StatefulPool(int minItems, int maxItems)
			: this(minItems, maxItems, TimeSpan.MaxValue)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000253F File Offset: 0x0000073F
		protected StatefulPool(int minItems, int maxItems, TimeSpan oldestAllowed)
			: base(minItems, maxItems, oldestAllowed)
		{
			this.m_buckets = new Dictionary<TPoolObjectKey, PoolObject<TPoolObjectKey, TObject>>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002560 File Offset: 0x00000760
		internal override string DebugDump(int bucketsToDetail)
		{
			string text = string.Empty;
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				text += "==============================================================================\r\n";
				text += string.Format(CultureInfo.InvariantCulture, "{0} pool metadata: min={1}, max={2}, allocated={3}, expire={4}\r\n", new object[]
				{
					base.GetType().Name,
					base.MinItems,
					base.MaxItems,
					this.m_allocatedItems,
					base.OldestAllowed
				});
				text += string.Format(CultureInfo.InvariantCulture, "Object count: {0}\r\n", base.Count);
				if (0 < this.m_buckets.Count)
				{
					text += "------------------------------------------------------------------------------\r\n";
					text += string.Format(CultureInfo.InvariantCulture, "Bucket dump ({0} buckets max):\r\n", bucketsToDetail);
					int num = 0;
					foreach (KeyValuePair<TPoolObjectKey, PoolObject<TPoolObjectKey, TObject>> keyValuePair in this.m_buckets)
					{
						if (num >= bucketsToDetail)
						{
							break;
						}
						text += string.Format(CultureInfo.InvariantCulture, "  {0:D3}: {1}\r\n", num, keyValuePair.Value.Key);
						num++;
					}
				}
				text += "==============================================================================";
			}
			return text;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002704 File Offset: 0x00000904
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002720 File Offset: 0x00000920
		protected virtual async Task<PoolObject<TPoolObjectKey, TObject>> Get(TPoolObjectKey poolKey, TObjectKey objectKey, bool useExisting)
		{
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				if (useExisting)
				{
					PoolObject<TPoolObjectKey, TObject> poolObject;
					if (this.m_buckets.TryGetValue(poolKey, out poolObject))
					{
						this.m_buckets.Remove(poolKey);
						base.DeleteFromSortedList(poolObject);
						if (this.IsValidObject(poolObject.Object))
						{
							return poolObject;
						}
						base.SafeCleanup(poolObject);
					}
					throw new StatefulPoolObjectExpiredException(poolKey.ToString(), string.Empty, Array.Empty<PowerBIErrorDetail>());
				}
			}
			if (!this.AllocateSlot())
			{
				throw new StatefulPoolCapacityExceeded();
			}
			PoolObject<TPoolObjectKey, TObject> poolObject2;
			try
			{
				poolObject2 = await base.CreatePoolObject(poolKey, objectKey);
			}
			catch (Exception)
			{
				this.FreeSlot();
				throw;
			}
			return poolObject2;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000277C File Offset: 0x0000097C
		public virtual void Return(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
			if (poolObject == null)
			{
				return;
			}
			if (!this.IsValidObject(poolObject.Object))
			{
				poolObject.Dispose();
				return;
			}
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				PoolObject<TPoolObjectKey, TObject> poolObject2;
				if (this.m_buckets.TryGetValue(poolObject.Key, out poolObject2))
				{
					TPoolObjectKey key = poolObject.Key;
					throw new DuplicateStatefulPoolObjectReturnException(key.ToString(), string.Empty, Array.Empty<PowerBIErrorDetail>());
				}
				this.m_buckets[poolObject.Key] = poolObject;
				poolObject.MarkAccess();
				base.AddToSortedList(poolObject);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002828 File Offset: 0x00000A28
		protected override void BalanceAndShrinkIteration()
		{
			List<PoolObject<TPoolObjectKey, TObject>> list = null;
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				DateTime dateTime;
				list = base.CollectForDeletionNotSynchronized(out dateTime);
				foreach (PoolObject<TPoolObjectKey, TObject> poolObject in list)
				{
					this.m_buckets[poolObject.Key] = null;
				}
			}
			foreach (PoolObject<TPoolObjectKey, TObject> poolObject2 in list)
			{
				base.SafeCleanup(poolObject2);
			}
			if (list.Count > 0)
			{
				TraceSourceBase<PoolTraceSource>.Tracer.TraceInformation("Pool cleaner has removed {0} connections!", new object[] { list.Count });
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002924 File Offset: 0x00000B24
		protected override void CleanupPoolObject(PoolObject<TPoolObjectKey, TObject> poolObject)
		{
			this.FreeSlot();
			base.CleanupPoolObject(poolObject);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002934 File Offset: 0x00000B34
		public void Clear()
		{
			object bucketsLock = this.m_bucketsLock;
			lock (bucketsLock)
			{
				foreach (PoolObject<TPoolObjectKey, TObject> poolObject in this.m_buckets.Values)
				{
					base.DeleteFromSortedList(poolObject);
					base.SafeCleanup(poolObject);
				}
				this.m_buckets.Clear();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029C8 File Offset: 0x00000BC8
		private bool AllocateSlot()
		{
			if (Interlocked.Increment(ref this.m_allocatedItems) <= base.MaxItems)
			{
				return true;
			}
			this.FreeSlot();
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029E6 File Offset: 0x00000BE6
		private void FreeSlot()
		{
			Interlocked.Decrement(ref this.m_allocatedItems);
		}

		// Token: 0x0400001E RID: 30
		private readonly object m_bucketsLock = new object();

		// Token: 0x0400001F RID: 31
		private readonly Dictionary<TPoolObjectKey, PoolObject<TPoolObjectKey, TObject>> m_buckets;

		// Token: 0x04000020 RID: 32
		private bool m_disposed;

		// Token: 0x04000021 RID: 33
		private int m_allocatedItems;
	}
}
