using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020BF RID: 8383
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableStack
	{
		// Token: 0x0601198E RID: 72078 RVA: 0x003C3056 File Offset: 0x003C1256
		public static ImmutableStack<T> Create<[Nullable(2)] T>()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x0601198F RID: 72079 RVA: 0x003C305D File Offset: 0x003C125D
		public static ImmutableStack<T> Create<[Nullable(2)] T>(T item)
		{
			return ImmutableStack<T>.Empty.Push(item);
		}

		// Token: 0x06011990 RID: 72080 RVA: 0x003C306C File Offset: 0x003C126C
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

		// Token: 0x06011991 RID: 72081 RVA: 0x003C30C8 File Offset: 0x003C12C8
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

		// Token: 0x06011992 RID: 72082 RVA: 0x003C3107 File Offset: 0x003C1307
		public static IImmutableStack<T> Pop<[Nullable(2)] T>(this IImmutableStack<T> stack, out T value)
		{
			Requires.NotNull<IImmutableStack<T>>(stack, "stack");
			value = stack.Peek();
			return stack.Pop();
		}
	}
}
