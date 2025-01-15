using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002064 RID: 8292
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AllocFreeConcurrentStack<[Nullable(2)] T>
	{
		// Token: 0x060113F2 RID: 70642 RVA: 0x003B5F20 File Offset: 0x003B4120
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack.Count < 35)
			{
				threadLocalStack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x060113F3 RID: 70643 RVA: 0x003B5F4C File Offset: 0x003B414C
		public static bool TryTake([MaybeNullWhen(false)] out T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack != null && threadLocalStack.Count > 0)
			{
				item = threadLocalStack.Pop().Value;
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x17002E12 RID: 11794
		// (get) Token: 0x060113F4 RID: 70644 RVA: 0x003B5F88 File Offset: 0x003B4188
		[Nullable(new byte[] { 1, 0, 1 })]
		private static Stack<RefAsValueType<T>> ThreadLocalStack
		{
			get
			{
				Dictionary<Type, object> dictionary = AllocFreeConcurrentStack.t_stacks;
				if (dictionary == null)
				{
					dictionary = (AllocFreeConcurrentStack.t_stacks = new Dictionary<Type, object>());
				}
				object obj;
				if (!dictionary.TryGetValue(AllocFreeConcurrentStack<T>.s_typeOfT, out obj))
				{
					obj = new Stack<RefAsValueType<T>>(35);
					dictionary.Add(AllocFreeConcurrentStack<T>.s_typeOfT, obj);
				}
				return (Stack<RefAsValueType<T>>)obj;
			}
		}

		// Token: 0x040068A0 RID: 26784
		private const int MaxSize = 35;

		// Token: 0x040068A1 RID: 26785
		private static readonly Type s_typeOfT = typeof(T);
	}
}
