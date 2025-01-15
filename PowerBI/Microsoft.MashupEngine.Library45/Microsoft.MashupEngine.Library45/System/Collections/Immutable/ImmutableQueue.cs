using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020AC RID: 8364
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableQueue
	{
		// Token: 0x060117F6 RID: 71670 RVA: 0x003BF38C File Offset: 0x003BD58C
		public static ImmutableQueue<T> Create<[Nullable(2)] T>()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x060117F7 RID: 71671 RVA: 0x003BF393 File Offset: 0x003BD593
		public static ImmutableQueue<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableQueue<T>.Empty.Enqueue(item);
		}

		// Token: 0x060117F8 RID: 71672 RVA: 0x003BF3A0 File Offset: 0x003BD5A0
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

		// Token: 0x060117F9 RID: 71673 RVA: 0x003BF434 File Offset: 0x003BD634
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

		// Token: 0x060117FA RID: 71674 RVA: 0x003BF485 File Offset: 0x003BD685
		public static IImmutableQueue<T> Dequeue<[Nullable(2)] T>(this IImmutableQueue<T> queue, out T value)
		{
			Requires.NotNull<IImmutableQueue<T>>(queue, "queue");
			value = queue.Peek();
			return queue.Dequeue();
		}
	}
}
