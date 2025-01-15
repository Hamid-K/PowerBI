using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Internal;

namespace System.Threading.Tasks
{
	// Token: 0x02000019 RID: 25
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(SingleProducerSingleConsumerQueue<>.SingleProducerSingleConsumerQueue_DebugView))]
	internal sealed class SingleProducerSingleConsumerQueue<T> : IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002500 File Offset: 0x00000700
		internal SingleProducerSingleConsumerQueue()
		{
			this._head = (this._tail = new SingleProducerSingleConsumerQueue<T>.Segment(32));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002530 File Offset: 0x00000730
		public void Enqueue(T item)
		{
			SingleProducerSingleConsumerQueue<T>.Segment tail = this._tail;
			T[] array = tail._array;
			int last = tail._state._last;
			int num = (last + 1) & (array.Length - 1);
			if (num != tail._state._firstCopy)
			{
				array[last] = item;
				tail._state._last = num;
				return;
			}
			this.EnqueueSlow(item, ref tail);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002594 File Offset: 0x00000794
		private void EnqueueSlow(T item, ref SingleProducerSingleConsumerQueue<T>.Segment segment)
		{
			if (segment._state._firstCopy != segment._state._first)
			{
				segment._state._firstCopy = segment._state._first;
				this.Enqueue(item);
				return;
			}
			int num = this._tail._array.Length << 1;
			if (num > 16777216)
			{
				num = 16777216;
			}
			SingleProducerSingleConsumerQueue<T>.Segment segment2 = new SingleProducerSingleConsumerQueue<T>.Segment(num);
			segment2._array[0] = item;
			segment2._state._last = 1;
			segment2._state._lastCopy = 1;
			try
			{
			}
			finally
			{
				Volatile.Write<SingleProducerSingleConsumerQueue<T>.Segment>(ref this._tail._next, segment2);
				this._tail = segment2;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000265C File Offset: 0x0000085C
		public bool TryDequeue([MaybeNullWhen(false)] out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this._head;
			T[] array = head._array;
			int first = head._state._first;
			if (first != head._state._lastCopy)
			{
				result = array[first];
				array[first] = default(T);
				head._state._first = (first + 1) & (array.Length - 1);
				return true;
			}
			return this.TryDequeueSlow(ref head, ref array, out result);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026D8 File Offset: 0x000008D8
		private bool TryDequeueSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, [MaybeNullWhen(false)] out T result)
		{
			if (segment._state._last != segment._state._lastCopy)
			{
				segment._state._lastCopy = segment._state._last;
				return this.TryDequeue(out result);
			}
			if (segment._next != null && segment._state._first == segment._state._last)
			{
				segment = segment._next;
				array = segment._array;
				this._head = segment;
			}
			int first = segment._state._first;
			if (first == segment._state._last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			array[first] = default(T);
			segment._state._first = (first + 1) & (segment._array.Length - 1);
			segment._state._lastCopy = segment._state._last;
			return true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000027E8 File Offset: 0x000009E8
		public bool TryPeek([MaybeNullWhen(false)] out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this._head;
			T[] array = head._array;
			int first = head._state._first;
			if (first != head._state._lastCopy)
			{
				result = array[first];
				return true;
			}
			return this.TryPeekSlow(ref head, ref array, out result);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000283C File Offset: 0x00000A3C
		private bool TryPeekSlow(ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, [MaybeNullWhen(false)] out T result)
		{
			if (segment._state._last != segment._state._lastCopy)
			{
				segment._state._lastCopy = segment._state._last;
				return this.TryPeek(out result);
			}
			if (segment._next != null && segment._state._first == segment._state._last)
			{
				segment = segment._next;
				array = segment._array;
				this._head = segment;
			}
			int first = segment._state._first;
			if (first == segment._state._last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002904 File Offset: 0x00000B04
		public bool TryDequeueIf(Predicate<T> predicate, [MaybeNullWhen(false)] out T result)
		{
			SingleProducerSingleConsumerQueue<T>.Segment head = this._head;
			T[] array = head._array;
			int first = head._state._first;
			if (first == head._state._lastCopy)
			{
				return this.TryDequeueIfSlow(predicate, ref head, ref array, out result);
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				head._state._first = (first + 1) & (array.Length - 1);
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002998 File Offset: 0x00000B98
		private bool TryDequeueIfSlow(Predicate<T> predicate, ref SingleProducerSingleConsumerQueue<T>.Segment segment, ref T[] array, [MaybeNullWhen(false)] out T result)
		{
			if (segment._state._last != segment._state._lastCopy)
			{
				segment._state._lastCopy = segment._state._last;
				return this.TryDequeueIf(predicate, out result);
			}
			if (segment._next != null && segment._state._first == segment._state._last)
			{
				segment = segment._next;
				array = segment._array;
				this._head = segment;
			}
			int first = segment._state._first;
			if (first == segment._state._last)
			{
				result = default(T);
				return false;
			}
			result = array[first];
			if (predicate == null || predicate(result))
			{
				array[first] = default(T);
				segment._state._first = (first + 1) & (segment._array.Length - 1);
				segment._state._lastCopy = segment._state._last;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public void Clear()
		{
			T t;
			while (this.TryDequeue(out t))
			{
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public bool IsEmpty
		{
			get
			{
				SingleProducerSingleConsumerQueue<T>.Segment head = this._head;
				return head._state._first == head._state._lastCopy && head._state._first == head._state._last && head._next == null;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B39 File Offset: 0x00000D39
		public IEnumerator<T> GetEnumerator()
		{
			SingleProducerSingleConsumerQueue<T>.Segment segment;
			for (segment = this._head; segment != null; segment = segment._next)
			{
				for (int pt = segment._state._first; pt != segment._state._last; pt = (pt + 1) & (segment._array.Length - 1))
				{
					yield return segment._array[pt];
				}
			}
			segment = null;
			yield break;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B48 File Offset: 0x00000D48
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002B50 File Offset: 0x00000D50
		public int Count
		{
			get
			{
				int num = 0;
				for (SingleProducerSingleConsumerQueue<T>.Segment segment = this._head; segment != null; segment = segment._next)
				{
					int num2 = segment._array.Length;
					int first;
					int last;
					do
					{
						first = segment._state._first;
						last = segment._state._last;
					}
					while (first != segment._state._first);
					num += (last - first) & (num2 - 1);
				}
				return num;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002BB8 File Offset: 0x00000DB8
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			int count;
			lock (syncObj)
			{
				count = this.Count;
			}
			return count;
		}

		// Token: 0x04000014 RID: 20
		private const int INIT_SEGMENT_SIZE = 32;

		// Token: 0x04000015 RID: 21
		private const int MAX_SEGMENT_SIZE = 16777216;

		// Token: 0x04000016 RID: 22
		private volatile SingleProducerSingleConsumerQueue<T>.Segment _head;

		// Token: 0x04000017 RID: 23
		private volatile SingleProducerSingleConsumerQueue<T>.Segment _tail;

		// Token: 0x02000049 RID: 73
		[StructLayout(LayoutKind.Sequential)]
		private sealed class Segment
		{
			// Token: 0x06000295 RID: 661 RVA: 0x0000B7C8 File Offset: 0x000099C8
			internal Segment(int size)
			{
				this._array = new T[size];
			}

			// Token: 0x040000D3 RID: 211
			internal SingleProducerSingleConsumerQueue<T>.Segment _next;

			// Token: 0x040000D4 RID: 212
			internal readonly T[] _array;

			// Token: 0x040000D5 RID: 213
			internal SingleProducerSingleConsumerQueue<T>.SegmentState _state;
		}

		// Token: 0x0200004A RID: 74
		private struct SegmentState
		{
			// Token: 0x040000D6 RID: 214
			internal PaddingFor32 _pad0;

			// Token: 0x040000D7 RID: 215
			internal volatile int _first;

			// Token: 0x040000D8 RID: 216
			internal int _lastCopy;

			// Token: 0x040000D9 RID: 217
			internal PaddingFor32 _pad1;

			// Token: 0x040000DA RID: 218
			internal int _firstCopy;

			// Token: 0x040000DB RID: 219
			internal volatile int _last;

			// Token: 0x040000DC RID: 220
			internal PaddingFor32 _pad2;
		}

		// Token: 0x0200004B RID: 75
		private sealed class SingleProducerSingleConsumerQueue_DebugView
		{
			// Token: 0x06000296 RID: 662 RVA: 0x0000B7DC File Offset: 0x000099DC
			public SingleProducerSingleConsumerQueue_DebugView(SingleProducerSingleConsumerQueue<T> queue)
			{
				this._queue = queue;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000297 RID: 663 RVA: 0x0000B7EC File Offset: 0x000099EC
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public T[] Items
			{
				get
				{
					List<T> list = new List<T>();
					foreach (T t in this._queue)
					{
						list.Add(t);
					}
					return list.ToArray();
				}
			}

			// Token: 0x040000DD RID: 221
			private readonly SingleProducerSingleConsumerQueue<T> _queue;
		}
	}
}
