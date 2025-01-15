using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AllocFreeConcurrentStack<[Nullable(2)] T>
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static void TryAdd(T item)
		{
			Stack<RefAsValueType<T>> threadLocalStack = AllocFreeConcurrentStack<T>.ThreadLocalStack;
			if (threadLocalStack.Count < 35)
			{
				threadLocalStack.Push(new RefAsValueType<T>(item));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CEC File Offset: 0x00000EEC
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D28 File Offset: 0x00000F28
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

		// Token: 0x0400000D RID: 13
		private const int MaxSize = 35;

		// Token: 0x0400000E RID: 14
		private static readonly Type s_typeOfT = typeof(T);
	}
}
