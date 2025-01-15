using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableStack
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00008E01 File Offset: 0x00007001
		public static ImmutableStack<T> Create<[Nullable(2)] T>()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008E08 File Offset: 0x00007008
		public static ImmutableStack<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008E18 File Offset: 0x00007018
		public static ImmutableStack<T> CreateRange<[Nullable(2)] T>(IEnumerable<T> items)
		{
			Requires.NotNull<IEnumerable<T>>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T t in items)
			{
				immutableStack = immutableStack.Push(t);
			}
			return immutableStack;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008E74 File Offset: 0x00007074
		public static ImmutableStack<T> Create<[Nullable(2)] T>(params T[] items)
		{
			Requires.NotNull<T[]>(items, "items");
			ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
			foreach (T t in items)
			{
				immutableStack = immutableStack.Push(t);
			}
			return immutableStack;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008EB3 File Offset: 0x000070B3
		public static IImmutableStack<T> Pop<[Nullable(2)] T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
