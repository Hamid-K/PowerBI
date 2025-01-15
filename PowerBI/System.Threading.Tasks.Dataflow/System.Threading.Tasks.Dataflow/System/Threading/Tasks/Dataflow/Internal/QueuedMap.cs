using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000041 RID: 65
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(EnumerableDebugView<, >))]
	internal sealed class QueuedMap<TKey, TValue>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x000091B4 File Offset: 0x000073B4
		internal QueuedMap()
		{
			this._queue = new QueuedMap<TKey, TValue>.ArrayBasedLinkedQueue<KeyValuePair<TKey, TValue>>();
			this._mapKeyToIndex = new Dictionary<TKey, int>();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000091D2 File Offset: 0x000073D2
		internal QueuedMap(int capacity)
		{
			this._queue = new QueuedMap<TKey, TValue>.ArrayBasedLinkedQueue<KeyValuePair<TKey, TValue>>(capacity);
			this._mapKeyToIndex = new Dictionary<TKey, int>(capacity);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000091F4 File Offset: 0x000073F4
		internal void Push(TKey key, TValue value)
		{
			int num;
			if (!this._queue.IsEmpty && this._mapKeyToIndex.TryGetValue(key, out num))
			{
				this._queue.Replace(num, new KeyValuePair<TKey, TValue>(key, value));
				return;
			}
			num = this._queue.Enqueue(new KeyValuePair<TKey, TValue>(key, value));
			this._mapKeyToIndex.Add(key, num);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009254 File Offset: 0x00007454
		internal bool TryPop(out KeyValuePair<TKey, TValue> item)
		{
			bool flag = this._queue.TryDequeue(out item);
			if (flag)
			{
				this._mapKeyToIndex.Remove(item.Key);
			}
			return flag;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009284 File Offset: 0x00007484
		internal int PopRange(KeyValuePair<TKey, TValue>[] items, int arrayOffset, int count)
		{
			int num = 0;
			int num2 = arrayOffset;
			KeyValuePair<TKey, TValue> keyValuePair;
			while (num < count && this.TryPop(out keyValuePair))
			{
				items[num2] = keyValuePair;
				num2++;
				num++;
			}
			return num;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000092B6 File Offset: 0x000074B6
		internal int Count
		{
			get
			{
				return this._mapKeyToIndex.Count;
			}
		}

		// Token: 0x0400009C RID: 156
		private readonly QueuedMap<TKey, TValue>.ArrayBasedLinkedQueue<KeyValuePair<TKey, TValue>> _queue;

		// Token: 0x0400009D RID: 157
		private readonly Dictionary<TKey, int> _mapKeyToIndex;

		// Token: 0x02000086 RID: 134
		private sealed class ArrayBasedLinkedQueue<T>
		{
			// Token: 0x06000434 RID: 1076 RVA: 0x0000FC43 File Offset: 0x0000DE43
			internal ArrayBasedLinkedQueue()
			{
				this._storage = new List<KeyValuePair<int, T>>();
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0000FC6B File Offset: 0x0000DE6B
			internal ArrayBasedLinkedQueue(int capacity)
			{
				this._storage = new List<KeyValuePair<int, T>>(capacity);
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0000FC94 File Offset: 0x0000DE94
			internal int Enqueue(T item)
			{
				int num;
				if (this._freeIndex != -1)
				{
					num = this._freeIndex;
					this._freeIndex = this._storage[this._freeIndex].Key;
					this._storage[num] = new KeyValuePair<int, T>(-1, item);
				}
				else
				{
					num = this._storage.Count;
					this._storage.Add(new KeyValuePair<int, T>(-1, item));
				}
				if (this._headIndex == -1)
				{
					this._headIndex = num;
				}
				else
				{
					this._storage[this._tailIndex] = new KeyValuePair<int, T>(num, this._storage[this._tailIndex].Value);
				}
				this._tailIndex = num;
				return num;
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0000FD50 File Offset: 0x0000DF50
			internal bool TryDequeue([MaybeNullWhen(false)] out T item)
			{
				if (this._headIndex == -1)
				{
					item = default(T);
					return false;
				}
				item = this._storage[this._headIndex].Value;
				int key = this._storage[this._headIndex].Key;
				this._storage[this._headIndex] = new KeyValuePair<int, T>(this._freeIndex, default(T));
				this._freeIndex = this._headIndex;
				this._headIndex = key;
				if (this._headIndex == -1)
				{
					this._tailIndex = -1;
				}
				return true;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
			internal void Replace(int index, T item)
			{
				this._storage[index] = new KeyValuePair<int, T>(this._storage[index].Key, item);
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000FE27 File Offset: 0x0000E027
			internal bool IsEmpty
			{
				get
				{
					return this._headIndex == -1;
				}
			}

			// Token: 0x040001B6 RID: 438
			private const int TERMINATOR_INDEX = -1;

			// Token: 0x040001B7 RID: 439
			private readonly List<KeyValuePair<int, T>> _storage;

			// Token: 0x040001B8 RID: 440
			private int _headIndex = -1;

			// Token: 0x040001B9 RID: 441
			private int _tailIndex = -1;

			// Token: 0x040001BA RID: 442
			private int _freeIndex = -1;
		}
	}
}
