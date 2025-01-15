using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableQueue
	{
		// Token: 0x06000281 RID: 641 RVA: 0x000075F4 File Offset: 0x000057F4
		public static ImmutableQueue<T> Create<[Nullable(2)] T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000075FB File Offset: 0x000057FB
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007608 File Offset: 0x00005808
		public static ImmutableQueue<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			T[] array = items as T[];
			if (array != null)
			{
				return ImmutableQueue.Create<T>(array);
			}
			ImmutableQueue<T> immutableQueue;
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					immutableQueue = ImmutableQueue<T>.Empty;
				}
				else
				{
					ImmutableStack<T> immutableStack = ImmutableStack.Create<T>(enumerator.Current);
					ImmutableStack<T> immutableStack2 = ImmutableStack<T>.Empty;
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						immutableStack2 = immutableStack2.Push(t);
					}
					immutableQueue = new ImmutableQueue<T>(immutableStack, immutableStack2);
				}
			}
			return immutableQueue;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000769C File Offset: 0x0000589C
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			if (items.Length == 0)
			{
				return ImmutableQueue<T>.Empty;
			}
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			for (int i = items.Length - 1; i >= 0; i--)
			{
				immutableStack = immutableStack.Push(items[i]);
			}
			return new ImmutableQueue<T>(immutableStack, ImmutableStack<T>.Empty);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000076ED File Offset: 0x000058ED
		public static IImmutableQueue<T> Dequeue<[Nullable(2)] T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
