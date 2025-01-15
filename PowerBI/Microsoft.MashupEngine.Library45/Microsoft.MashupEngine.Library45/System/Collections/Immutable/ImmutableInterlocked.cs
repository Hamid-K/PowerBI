using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Immutable
{
	// Token: 0x020020A5 RID: 8357
	[NullableContext(1)]
	[Nullable(0)]
	public static class ImmutableInterlocked
	{
		// Token: 0x060116CD RID: 71373 RVA: 0x003BC40C File Offset: 0x003BA60C
		public static bool Update<[Nullable(2)] T>(ref T location, Func<T, T> transformer) where T : class
		{
			Requires.NotNull<Func<T, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116CE RID: 71374 RVA: 0x003BC464 File Offset: 0x003BA664
		public static bool Update<[Nullable(2)] T, [Nullable(2)] TArg>(ref T location, Func<T, TArg, T> transformer, TArg transformerArgument) where T : class
		{
			Requires.NotNull<Func<T, TArg, T>>(transformer, "transformer");
			T t = Volatile.Read<T>(ref location);
			for (;;)
			{
				T t2 = transformer(t, transformerArgument);
				if (t == t2)
				{
					break;
				}
				T t3 = Interlocked.CompareExchange<T>(ref location, t2, t);
				bool flag = t == t3;
				t = t3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116CF RID: 71375 RVA: 0x003BC4BC File Offset: 0x003BA6BC
		[NullableContext(2)]
		public static bool Update<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 1, 0, 1, 0, 1 })] Func<ImmutableArray<T>, ImmutableArray<T>> transformer)
		{
			Requires.NotNull<Func<ImmutableArray<T>, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(ref location.array);
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer(new ImmutableArray<T>(array));
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(ref location.array, immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116D0 RID: 71376 RVA: 0x003BC518 File Offset: 0x003BA718
		[NullableContext(2)]
		public static bool Update<T, TArg>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 1, 0, 1, 1, 0, 1 })] Func<ImmutableArray<T>, TArg, ImmutableArray<T>> transformer, [Nullable(1)] TArg transformerArgument)
		{
			Requires.NotNull<Func<ImmutableArray<T>, TArg, ImmutableArray<T>>>(transformer, "transformer");
			T[] array = Volatile.Read<T[]>(ref location.array);
			for (;;)
			{
				ImmutableArray<T> immutableArray = transformer(new ImmutableArray<T>(array), transformerArgument);
				if (array == immutableArray.array)
				{
					break;
				}
				T[] array2 = Interlocked.CompareExchange<T[]>(ref location.array, immutableArray.array, array);
				bool flag = array == array2;
				array = array2;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116D1 RID: 71377 RVA: 0x003BC573 File Offset: 0x003BA773
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> InterlockedExchange<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value)
		{
			return new ImmutableArray<T>(Interlocked.Exchange<T[]>(ref location.array, value.array));
		}

		// Token: 0x060116D2 RID: 71378 RVA: 0x003BC58B File Offset: 0x003BA78B
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<T> InterlockedCompareExchange<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> comparand)
		{
			return new ImmutableArray<T>(Interlocked.CompareExchange<T[]>(ref location.array, value.array, comparand.array));
		}

		// Token: 0x060116D3 RID: 71379 RVA: 0x003BC5AC File Offset: 0x003BA7AC
		[NullableContext(2)]
		public static bool InterlockedInitialize<T>([Nullable(new byte[] { 0, 1 })] ref ImmutableArray<T> location, [Nullable(new byte[] { 0, 1 })] ImmutableArray<T> value)
		{
			return ImmutableInterlocked.InterlockedCompareExchange<T>(ref location, value, default(ImmutableArray<T>)).IsDefault;
		}

		// Token: 0x060116D4 RID: 71380 RVA: 0x003BC5D4 File Offset: 0x003BA7D4
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue, [Nullable(2)] TArg>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TArg, TValue> valueFactory, TArg factoryArgument)
		{
			Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key, factoryArgument);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x060116D5 RID: 71381 RVA: 0x003BC61C File Offset: 0x003BA81C
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> valueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(valueFactory, "valueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
			TValue tvalue;
			if (immutableDictionary.TryGetValue(key, out tvalue))
			{
				return tvalue;
			}
			tvalue = valueFactory(key);
			return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, tvalue);
		}

		// Token: 0x060116D6 RID: 71382 RVA: 0x003BC664 File Offset: 0x003BA864
		public static TValue GetOrAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue;
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return value;
				}
			}
			return tvalue;
		}

		// Token: 0x060116D7 RID: 71383 RVA: 0x003BC6B0 File Offset: 0x003BA8B0
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue>>(addValueFactory, "addValueFactory");
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue2;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
				}
				else
				{
					tvalue2 = addValueFactory(key);
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
			}
			while (!flag);
			return tvalue2;
		}

		// Token: 0x060116D8 RID: 71384 RVA: 0x003BC728 File Offset: 0x003BA928
		public static TValue AddOrUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
		{
			Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, "updateValueFactory");
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			TValue tvalue2;
			bool flag;
			do
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (immutableDictionary.TryGetValue(key, out tvalue))
				{
					tvalue2 = updateValueFactory(key, tvalue);
				}
				else
				{
					tvalue2 = addValue;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, tvalue2);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
			}
			while (!flag);
			return tvalue2;
		}

		// Token: 0x060116D9 RID: 71385 RVA: 0x003BC78C File Offset: 0x003BA98C
		public static bool TryAdd<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (immutableDictionary.ContainsKey(key))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Add(key, value);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116DA RID: 71386 RVA: 0x003BC7D4 File Offset: 0x003BA9D4
		public static bool TryUpdate<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, TValue newValue, TValue comparisonValue)
		{
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				TValue tvalue;
				if (!immutableDictionary.TryGetValue(key, out tvalue) || !@default.Equals(tvalue, comparisonValue))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.SetItem(key, newValue);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116DB RID: 71387 RVA: 0x003BC834 File Offset: 0x003BAA34
		public static bool TryRemove<TKey, [Nullable(2)] TValue>(ref ImmutableDictionary<TKey, TValue> location, TKey key, [MaybeNullWhen(false)] out TValue value)
		{
			ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, "location");
				if (!immutableDictionary.TryGetValue(key, out value))
				{
					break;
				}
				ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary.Remove(key);
				ImmutableDictionary<TKey, TValue> immutableDictionary3 = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary);
				bool flag = immutableDictionary == immutableDictionary3;
				immutableDictionary = immutableDictionary3;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060116DC RID: 71388 RVA: 0x003BC87C File Offset: 0x003BAA7C
		public static bool TryPop<[Nullable(2)] T>(ref ImmutableStack<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				if (immutableStack.IsEmpty)
				{
					break;
				}
				ImmutableStack<T> immutableStack2 = immutableStack.Pop(out value);
				ImmutableStack<T> immutableStack3 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack);
				bool flag = immutableStack == immutableStack3;
				immutableStack = immutableStack3;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x060116DD RID: 71389 RVA: 0x003BC8C8 File Offset: 0x003BAAC8
		public static void Push<[Nullable(2)] T>(ref ImmutableStack<T> location, T value)
		{
			ImmutableStack<T> immutableStack = Volatile.Read<ImmutableStack<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableStack<T>>(immutableStack, "location");
				ImmutableStack<T> immutableStack2 = immutableStack.Push(value);
				ImmutableStack<T> immutableStack3 = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack);
				flag = immutableStack == immutableStack3;
				immutableStack = immutableStack3;
			}
			while (!flag);
		}

		// Token: 0x060116DE RID: 71390 RVA: 0x003BC904 File Offset: 0x003BAB04
		public static bool TryDequeue<[Nullable(2)] T>(ref ImmutableQueue<T> location, [MaybeNullWhen(false)] out T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			for (;;)
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				if (immutableQueue.IsEmpty)
				{
					break;
				}
				ImmutableQueue<T> immutableQueue2 = immutableQueue.Dequeue(out value);
				ImmutableQueue<T> immutableQueue3 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue);
				bool flag = immutableQueue == immutableQueue3;
				immutableQueue = immutableQueue3;
				if (flag)
				{
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x060116DF RID: 71391 RVA: 0x003BC950 File Offset: 0x003BAB50
		public static void Enqueue<[Nullable(2)] T>(ref ImmutableQueue<T> location, T value)
		{
			ImmutableQueue<T> immutableQueue = Volatile.Read<ImmutableQueue<T>>(ref location);
			bool flag;
			do
			{
				Requires.NotNull<ImmutableQueue<T>>(immutableQueue, "location");
				ImmutableQueue<T> immutableQueue2 = immutableQueue.Enqueue(value);
				ImmutableQueue<T> immutableQueue3 = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue);
				flag = immutableQueue == immutableQueue3;
				immutableQueue = immutableQueue3;
			}
			while (!flag);
		}
	}
}
