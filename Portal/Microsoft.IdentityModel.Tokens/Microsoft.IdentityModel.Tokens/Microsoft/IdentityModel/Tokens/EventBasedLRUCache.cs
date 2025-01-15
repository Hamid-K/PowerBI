using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000139 RID: 313
	internal class EventBasedLRUCache<TKey, TValue>
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003CF16 File Offset: 0x0003B116
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0003CF1E File Offset: 0x0003B11E
		internal EventBasedLRUCache<TKey, TValue>.ItemRemoved OnItemRemoved { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0003CF27 File Offset: 0x0003B127
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0003CF2F File Offset: 0x0003B12F
		internal long EventQueueTaskIdleTimeoutInSeconds
		{
			get
			{
				return this._eventQueueTaskIdleTimeoutInSeconds;
			}
			set
			{
				if (value <= 0L)
				{
					throw new ArgumentOutOfRangeException("value", "EventQueueTaskExecutionTimeInSeconds must be positive.");
				}
				this._eventQueueTaskIdleTimeoutInSeconds = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003CF4D File Offset: 0x0003B14D
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0003CF55 File Offset: 0x0003B155
		private int EventQueuePollingInterval
		{
			get
			{
				return this._eventQueuePollingInterval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "EventQueuePollingInterval must be positive.");
				}
				this._eventQueuePollingInterval = value;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003CF74 File Offset: 0x0003B174
		internal EventBasedLRUCache(int capacity, TaskCreationOptions options = TaskCreationOptions.None, IEqualityComparer<TKey> comparer = null, bool removeExpiredValues = false, int removeExpiredValuesIntervalInSeconds = 300, bool maintainLRU = false)
		{
			if (capacity <= 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("capacity"));
			}
			this._capacity = capacity;
			this._options = options;
			this._map = new ConcurrentDictionary<TKey, LRUCacheItem<TKey, TValue>>(comparer ?? EqualityComparer<TKey>.Default);
			this._removeExpiredValuesIntervalInSeconds = removeExpiredValuesIntervalInSeconds;
			this._removeExpiredValues = removeExpiredValues;
			this._eventQueueTaskStopTime = DateTime.UtcNow;
			this._maintainLRU = maintainLRU;
			this._dueForExpiredValuesRemoval = DateTime.UtcNow.AddSeconds((double)this._removeExpiredValuesIntervalInSeconds);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003D040 File Offset: 0x0003B240
		private void DomainProcessExit(object sender, EventArgs e)
		{
			this.StopEventQueueTaskImmediately();
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003D048 File Offset: 0x0003B248
		private void DomainUnload(object sender, EventArgs e)
		{
			this.StopEventQueueTaskImmediately();
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003D050 File Offset: 0x0003B250
		internal void StopEventQueueTask()
		{
			this.StopEventQueueTaskImmediately();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003D058 File Offset: 0x0003B258
		private void StopEventQueueTaskImmediately()
		{
			this._shouldStopImmediately = true;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003D061 File Offset: 0x0003B261
		private void AddActionToEventQueue(Action action)
		{
			this._eventQueue.Enqueue(action);
			this.StartEventQueueTaskIfNotRunning();
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003D075 File Offset: 0x0003B275
		public bool Contains(TKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			return this._map.ContainsKey(key);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003D098 File Offset: 0x0003B298
		private void EventQueueTaskAction()
		{
			Interlocked.Increment(ref this._taskCount);
			while (!this._shouldStopImmediately)
			{
				Interlocked.Exchange(ref this._eventQueueTaskState, 1);
				try
				{
					if (this._removeExpiredValues && DateTime.UtcNow >= this._dueForExpiredValuesRemoval)
					{
						if (this._maintainLRU)
						{
							this.RemoveExpiredValuesLRU();
						}
						else
						{
							this.RemoveExpiredValues();
						}
						this._dueForExpiredValuesRemoval = DateTime.UtcNow.AddSeconds((double)this._removeExpiredValuesIntervalInSeconds);
					}
					Action action;
					if (this._eventQueue.TryDequeue(out action))
					{
						if (action != null)
						{
							action();
						}
					}
					else if (DateTime.UtcNow > this._eventQueueTaskStopTime)
					{
						if (Interlocked.CompareExchange(ref this._eventQueueTaskState, 0, 1) == 1)
						{
							break;
						}
					}
					else
					{
						Thread.Sleep(this._eventQueuePollingInterval);
					}
				}
				catch (Exception ex)
				{
					LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10900: EventBasedLRUCache._eventQueue encountered an error while processing a cache operation. Exception '{0}'.", new object[] { ex }), Array.Empty<object>());
				}
			}
			Interlocked.Decrement(ref this._taskCount);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		internal int RemoveExpiredValuesLRU()
		{
			int num = 0;
			try
			{
				LinkedListNode<LRUCacheItem<TKey, TValue>> next;
				for (LinkedListNode<LRUCacheItem<TKey, TValue>> linkedListNode = this._doubleLinkedList.First; linkedListNode != null; linkedListNode = next)
				{
					next = linkedListNode.Next;
					if (linkedListNode.Value.ExpirationTime < DateTime.UtcNow)
					{
						this._doubleLinkedList.Remove(linkedListNode);
						LRUCacheItem<TKey, TValue> lrucacheItem;
						if (this._map.TryRemove(linkedListNode.Value.Key, out lrucacheItem))
						{
							EventBasedLRUCache<TKey, TValue>.ItemRemoved onItemRemoved = this.OnItemRemoved;
							if (onItemRemoved != null)
							{
								onItemRemoved(lrucacheItem.Value);
							}
						}
						num++;
					}
				}
			}
			catch (ObjectDisposedException ex)
			{
				LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10902: Object disposed exception in '{0}': '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("RemoveExpiredValuesLRU"),
					ex
				}), Array.Empty<object>());
			}
			return num;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003D264 File Offset: 0x0003B464
		internal int RemoveExpiredValues()
		{
			int num = 0;
			try
			{
				foreach (KeyValuePair<TKey, LRUCacheItem<TKey, TValue>> keyValuePair in this._map)
				{
					if (keyValuePair.Value.ExpirationTime < DateTime.UtcNow)
					{
						LRUCacheItem<TKey, TValue> lrucacheItem;
						if (this._map.TryRemove(keyValuePair.Value.Key, out lrucacheItem))
						{
							EventBasedLRUCache<TKey, TValue>.ItemRemoved onItemRemoved = this.OnItemRemoved;
							if (onItemRemoved != null)
							{
								onItemRemoved(lrucacheItem.Value);
							}
						}
						num++;
					}
				}
			}
			catch (ObjectDisposedException ex)
			{
				LogHelper.LogWarning(LogHelper.FormatInvariant("IDX10902: Object disposed exception in '{0}': '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("RemoveExpiredValues"),
					ex
				}), Array.Empty<object>());
			}
			return num;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003D33C File Offset: 0x0003B53C
		private void CompactLRU()
		{
			int num = this.CalculateNewCacheSize();
			while (this._map.Count > num && this._doubleLinkedList.Count > 0)
			{
				LinkedListNode<LRUCacheItem<TKey, TValue>> last = this._doubleLinkedList.Last;
				LRUCacheItem<TKey, TValue> lrucacheItem;
				if (this._map.TryRemove(last.Value.Key, out lrucacheItem))
				{
					EventBasedLRUCache<TKey, TValue>.ItemRemoved onItemRemoved = this.OnItemRemoved;
					if (onItemRemoved != null)
					{
						onItemRemoved(lrucacheItem.Value);
					}
				}
				this._doubleLinkedList.RemoveLast();
			}
			this._compactionState = 0;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003D3C0 File Offset: 0x0003B5C0
		private void Compact()
		{
			int num = this.CalculateNewCacheSize();
			while (this._map.Count > num)
			{
				KeyValuePair<TKey, LRUCacheItem<TKey, TValue>> keyValuePair = this._map.FirstOrDefault<KeyValuePair<TKey, LRUCacheItem<TKey, TValue>>>();
				LRUCacheItem<TKey, TValue> lrucacheItem;
				if (!keyValuePair.Equals(null) && this._map.TryRemove(keyValuePair.Key, out lrucacheItem))
				{
					EventBasedLRUCache<TKey, TValue>.ItemRemoved onItemRemoved = this.OnItemRemoved;
					if (onItemRemoved != null)
					{
						onItemRemoved(lrucacheItem.Value);
					}
				}
			}
			this._compactionState = 0;
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003D434 File Offset: 0x0003B634
		protected int CalculateNewCacheSize()
		{
			int num = Math.Min(this._map.Count, this._capacity);
			return num - (int)((double)num * this._compactionPercentage);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003D458 File Offset: 0x0003B658
		private DateTime SetTaskEndTime()
		{
			return DateTime.UtcNow.AddSeconds((double)this.EventQueueTaskIdleTimeoutInSeconds);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003D479 File Offset: 0x0003B679
		public void SetValue(TKey key, TValue value)
		{
			this.SetValue(key, value, DateTime.MaxValue);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003D48C File Offset: 0x0003B68C
		public bool SetValue(TKey key, TValue value, DateTime expirationTime)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (value == null)
			{
				throw LogHelper.LogArgumentNullException("value");
			}
			if (this._removeExpiredValues && expirationTime < DateTime.UtcNow)
			{
				return false;
			}
			LRUCacheItem<TKey, TValue> cacheItem;
			if (this._map.TryGetValue(key, out cacheItem))
			{
				cacheItem.Value = value;
				cacheItem.ExpirationTime = expirationTime;
				if (this._maintainLRU)
				{
					this.AddActionToEventQueue(delegate
					{
						this._doubleLinkedList.Remove(cacheItem);
						this._doubleLinkedList.AddFirst(cacheItem);
					});
				}
			}
			else
			{
				if ((double)this._map.Count / (double)this._capacity >= this._maxCapacityPercentage && Interlocked.CompareExchange(ref this._compactionState, 1, 0) == 0)
				{
					if (this._maintainLRU)
					{
						this.AddActionToEventQueue(new Action(this.CompactLRU));
					}
					else
					{
						this.AddActionToEventQueue(new Action(this.Compact));
					}
				}
				LRUCacheItem<TKey, TValue> newCacheItem = new LRUCacheItem<TKey, TValue>(key, value, expirationTime);
				if (this._maintainLRU)
				{
					this.AddActionToEventQueue(delegate
					{
						this._doubleLinkedList.Remove(newCacheItem);
						this._doubleLinkedList.AddFirst(newCacheItem);
					});
				}
				this._map[key] = newCacheItem;
			}
			return true;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003D5D2 File Offset: 0x0003B7D2
		private void StartEventQueueTaskIfNotRunning()
		{
			this._eventQueueTaskStopTime = this.SetTaskEndTime();
			if (Interlocked.CompareExchange(ref this._eventQueueTaskState, 2, 1) == 1)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref this._eventQueueTaskState, 1, 0) == 0)
			{
				Task.Run(new Action(this.EventQueueTaskAction));
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003D612 File Offset: 0x0003B812
		internal KeyValuePair<TKey, LRUCacheItem<TKey, TValue>>[] ToArray()
		{
			return this._map.ToArray();
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003D620 File Offset: 0x0003B820
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			LRUCacheItem<TKey, TValue> cacheItem;
			if (!this._map.TryGetValue(key, out cacheItem))
			{
				value = default(TValue);
				return false;
			}
			if (this._maintainLRU)
			{
				this.AddActionToEventQueue(delegate
				{
					this._doubleLinkedList.Remove(cacheItem);
					this._doubleLinkedList.AddFirst(cacheItem);
				});
			}
			value = ((cacheItem != null) ? cacheItem.Value : default(TValue));
			return cacheItem != null;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		public bool TryRemove(TKey key, out TValue value)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			LRUCacheItem<TKey, TValue> cacheItem;
			if (!this._map.TryRemove(key, out cacheItem))
			{
				value = default(TValue);
				return false;
			}
			if (this._maintainLRU)
			{
				this.AddActionToEventQueue(delegate
				{
					this._doubleLinkedList.Remove(cacheItem);
				});
			}
			value = cacheItem.Value;
			EventBasedLRUCache<TKey, TValue>.ItemRemoved onItemRemoved = this.OnItemRemoved;
			if (onItemRemoved != null)
			{
				onItemRemoved(cacheItem.Value);
			}
			return true;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003D746 File Offset: 0x0003B946
		internal LinkedList<LRUCacheItem<TKey, TValue>> LinkedList
		{
			get
			{
				return this._doubleLinkedList;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003D74E File Offset: 0x0003B94E
		internal long LinkedListCount
		{
			get
			{
				return (long)this._doubleLinkedList.Count;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003D75C File Offset: 0x0003B95C
		internal long MapCount
		{
			get
			{
				return (long)this._map.Count;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003D76A File Offset: 0x0003B96A
		internal ICollection<LRUCacheItem<TKey, TValue>> MapValues
		{
			get
			{
				return this._map.Values;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003D777 File Offset: 0x0003B977
		internal long EventQueueCount
		{
			get
			{
				return (long)this._eventQueue.Count;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003D785 File Offset: 0x0003B985
		internal int TaskCount
		{
			get
			{
				return this._taskCount;
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003D78D File Offset: 0x0003B98D
		internal void WaitForProcessing()
		{
			while (!this._eventQueue.IsEmpty)
			{
			}
		}

		// Token: 0x040004E1 RID: 1249
		private readonly int _capacity;

		// Token: 0x040004E2 RID: 1250
		private readonly double _compactionPercentage = 0.2;

		// Token: 0x040004E3 RID: 1251
		private LinkedList<LRUCacheItem<TKey, TValue>> _doubleLinkedList = new LinkedList<LRUCacheItem<TKey, TValue>>();

		// Token: 0x040004E4 RID: 1252
		private ConcurrentQueue<Action> _eventQueue = new ConcurrentQueue<Action>();

		// Token: 0x040004E5 RID: 1253
		private ConcurrentDictionary<TKey, LRUCacheItem<TKey, TValue>> _map;

		// Token: 0x040004E6 RID: 1254
		private readonly double _maxCapacityPercentage = 0.95;

		// Token: 0x040004E7 RID: 1255
		private readonly bool _removeExpiredValues;

		// Token: 0x040004E8 RID: 1256
		private readonly int _removeExpiredValuesIntervalInSeconds;

		// Token: 0x040004E9 RID: 1257
		private readonly bool _maintainLRU;

		// Token: 0x040004EA RID: 1258
		private readonly TaskCreationOptions _options;

		// Token: 0x040004EB RID: 1259
		private DateTime _dueForExpiredValuesRemoval;

		// Token: 0x040004EC RID: 1260
		private int _taskCount;

		// Token: 0x040004ED RID: 1261
		private int _eventQueuePollingInterval = 50;

		// Token: 0x040004EE RID: 1262
		private long _eventQueueTaskIdleTimeoutInSeconds = 120L;

		// Token: 0x040004EF RID: 1263
		private DateTime _eventQueueTaskStopTime;

		// Token: 0x040004F0 RID: 1264
		private const int EventQueueTaskStopped = 0;

		// Token: 0x040004F1 RID: 1265
		private const int EventQueueTaskRunning = 1;

		// Token: 0x040004F2 RID: 1266
		private const int EventQueueTaskDoNotStop = 2;

		// Token: 0x040004F3 RID: 1267
		private int _eventQueueTaskState;

		// Token: 0x040004F4 RID: 1268
		private const int CompactionNotQueued = 0;

		// Token: 0x040004F5 RID: 1269
		private const int CompactionQueuedOrRunning = 1;

		// Token: 0x040004F6 RID: 1270
		private int _compactionState;

		// Token: 0x040004F7 RID: 1271
		private bool _shouldStopImmediately;

		// Token: 0x02000272 RID: 626
		// (Invoke) Token: 0x060014CB RID: 5323
		internal delegate void ItemRemoved(TValue Value);
	}
}
