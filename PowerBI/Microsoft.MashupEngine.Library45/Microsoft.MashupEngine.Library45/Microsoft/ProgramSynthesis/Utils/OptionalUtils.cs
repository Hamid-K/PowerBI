using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004D9 RID: 1241
	public static class OptionalUtils
	{
		// Token: 0x06001B9E RID: 7070 RVA: 0x00053227 File Offset: 0x00051427
		public static IEnumerable<T> AsEnumerable<T>(this Optional<T> optional)
		{
			if (optional.HasValue)
			{
				yield return optional.Value;
			}
			yield break;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00053237 File Offset: 0x00051437
		public static Optional<T> Then<T>(this bool condition, Func<T> f)
		{
			if (!condition)
			{
				return Optional<T>.Nothing;
			}
			return f().Some<T>();
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0005324D File Offset: 0x0005144D
		public static Optional<T> Then<T>(this bool condition, T t)
		{
			if (!condition)
			{
				return Optional<T>.Nothing;
			}
			return t.Some<T>();
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0005325E File Offset: 0x0005145E
		public static T OrElse<T>(this Optional<T> optional, T @default)
		{
			if (!optional.HasValue)
			{
				return @default;
			}
			return optional.Value;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00053272 File Offset: 0x00051472
		public static T OrElseCompute<T>(this Optional<T> optional, Func<T> defaultFunc)
		{
			if (!optional.HasValue)
			{
				return defaultFunc();
			}
			return optional.Value;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0005328B File Offset: 0x0005148B
		public static Optional<T> OrElseComputeOptional<T>(this Optional<T> optional, Func<Optional<T>> defaultFunc)
		{
			if (!optional.HasValue)
			{
				return defaultFunc();
			}
			return optional;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x000532A0 File Offset: 0x000514A0
		public static T OrElseDefault<T>(this Optional<T> optional)
		{
			if (!optional.HasValue)
			{
				return default(T);
			}
			return optional.Value;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000532C7 File Offset: 0x000514C7
		public static IEnumerable<T> OrElseEmpty<T>(this Optional<IEnumerable<T>> optional)
		{
			if (!optional.HasValue)
			{
				return Enumerable.Empty<T>();
			}
			return optional.Value;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000532E0 File Offset: 0x000514E0
		public static IEnumerable<T> OrElseEmpty<T>(this Optional<IReadOnlyCollection<T>> optional)
		{
			if (!optional.HasValue)
			{
				return Enumerable.Empty<T>();
			}
			return optional.Value;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00053308 File Offset: 0x00051508
		public static T? OrElseNull<T>(this Optional<T> optional) where T : struct
		{
			if (!optional.HasValue)
			{
				return null;
			}
			return new T?(optional.Value);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00053334 File Offset: 0x00051534
		public static Optional<TResult> Select<T, TResult>(this Optional<T> optional, Func<T, TResult> f)
		{
			if (!optional.HasValue)
			{
				return Optional<TResult>.Nothing;
			}
			return new Optional<TResult>(f(optional.Value));
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x00053358 File Offset: 0x00051558
		public static Optional<TResult> Select2<T1, T2, TResult>(this Optional<Record<T1, T2>> optional, Func<T1, T2, TResult> f)
		{
			return optional.Select((Record<T1, T2> r) => f(r.Item1, r.Item2));
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00053384 File Offset: 0x00051584
		public static void Select<T>(this Optional<T> optional, Action<T> f)
		{
			if (optional.HasValue)
			{
				f(optional.Value);
			}
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0005339C File Offset: 0x0005159C
		public static Optional<T> Cast<T>(this IOptional optional)
		{
			if (!optional.HasValue)
			{
				return Optional<T>.Nothing;
			}
			return ((T)((object)optional.Value)).Some<T>();
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x000533BC File Offset: 0x000515BC
		public static Optional<T> MaybeCast<T>(this IOptional optional) where T : class
		{
			if (!optional.HasValue)
			{
				return Optional<T>.Nothing;
			}
			return (optional.Value as T).SomeIfNotNull<T>();
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x000533E1 File Offset: 0x000515E1
		public static Optional<TResult> SelectMany<T, TResult>(this Optional<T> optional, Func<T, TResult?> f) where TResult : struct
		{
			if (!optional.HasValue)
			{
				return Optional<TResult>.Nothing;
			}
			return f(optional.Value).SomeIfNotNull<TResult>();
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x00053404 File Offset: 0x00051604
		public static Optional<TResult> SelectMany<T, TResult>(this Optional<T> optional, Func<T, Optional<TResult>> f)
		{
			if (!optional.HasValue)
			{
				return Optional<TResult>.Nothing;
			}
			return f(optional.Value);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x00053424 File Offset: 0x00051624
		public static Optional<V> SelectMany<T, U, V>(this Optional<T> optional, Func<T, Optional<U>> f, Func<T, U, V> combiner)
		{
			if (!optional.HasValue)
			{
				return Optional<V>.Nothing;
			}
			Optional<U> optional2 = f(optional.Value);
			if (!optional2.HasValue)
			{
				return Optional<V>.Nothing;
			}
			return combiner(optional.Value, optional2.Value).Some<V>();
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00053478 File Offset: 0x00051678
		public static IEnumerable<TResult> SelectMany<T, TCollection, TResult>(this IEnumerable<T> source, Func<T, Optional<TCollection>> collectionSelector, Func<T, TCollection, TResult> resultSelector)
		{
			return source.SelectMany((T t) => collectionSelector(t).AsEnumerable<TCollection>(), resultSelector);
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000534A8 File Offset: 0x000516A8
		public static IEnumerable<TResult> SelectMany<T, TResult>(this IEnumerable<T> source, Func<T, Optional<TResult>> collectionSelector)
		{
			return source.SelectMany((T x) => collectionSelector(x).AsEnumerable<TResult>());
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x000534D4 File Offset: 0x000516D4
		public static IEnumerable<TResult> SelectMany<T, TResult>(this Optional<T> source, Func<T, IEnumerable<TResult>> resultSelector)
		{
			return source.AsEnumerable<T>().SelectMany(resultSelector);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000534E2 File Offset: 0x000516E2
		public static Optional<T> Where<T>(this Optional<T> optional, Func<T, bool> predicate)
		{
			if (!optional.HasValue || predicate(optional.Value))
			{
				return optional;
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x00053504 File Offset: 0x00051704
		public static Optional<Record<T1, T2>> Where2<T1, T2>(this Optional<Record<T1, T2>> optional, Func<T1, T2, bool> predicate)
		{
			return optional.Where((Record<T1, T2> r) => predicate(r.Item1, r.Item2));
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x00053530 File Offset: 0x00051730
		public static Optional<T> Some<T>(this T? a) where T : struct
		{
			if (a == null)
			{
				return Optional<T>.Nothing;
			}
			return a.GetValueOrDefault().Some<T>();
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0005354D File Offset: 0x0005174D
		public static Optional<T> Some<T>(this T a)
		{
			return new Optional<T>(a);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x00053555 File Offset: 0x00051755
		public static Optional<T?> SomeNullable<T>(this T a) where T : struct
		{
			return new Optional<T?>(new T?(a));
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00053562 File Offset: 0x00051762
		public static Optional<T?> SomeNullable<T>(this T? a) where T : struct
		{
			return new Optional<T?>(a);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0005356A File Offset: 0x0005176A
		[NullableContext(1)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static Optional<T> SomeIfNotNull<T>([Nullable(2)] this T a) where T : class
		{
			if (a != null)
			{
				return new Optional<T>(a);
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00053580 File Offset: 0x00051780
		public static Optional<T> SomeIfNotNull<T>(this T? a) where T : struct
		{
			if (a != null)
			{
				return new Optional<T>(a.Value);
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000535A0 File Offset: 0x000517A0
		public static Optional<T> MaybeOnly<T>(this IEnumerable<T> items)
		{
			Optional<T> optional = Optional<T>.Nothing;
			foreach (T t in items)
			{
				if (optional.HasValue)
				{
					return Optional<T>.Nothing;
				}
				optional = t.Some<T>();
			}
			return optional;
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00053604 File Offset: 0x00051804
		public static Optional<T> MaybeFirst<T>(this IEnumerable<T> items)
		{
			using (IEnumerator<T> enumerator = items.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.Some<T>();
				}
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00053654 File Offset: 0x00051854
		public static Optional<T> MaybeFirst<T>(this IEnumerable<T> items, Func<T, bool> predicate)
		{
			using (IEnumerator<T> enumerator = items.Where(predicate).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current.Some<T>();
				}
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000536AC File Offset: 0x000518AC
		public static Optional<T> MaybeElementAt<T>(this IEnumerable<T> items, int k)
		{
			return items.Skip(k).MaybeFirst<T>();
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000536BC File Offset: 0x000518BC
		public static Optional<T> MaybeLast<T>(this IEnumerable<T> items)
		{
			Optional<T> optional = Optional<T>.Nothing;
			foreach (T t in items)
			{
				optional = t.Some<T>();
			}
			return optional;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0005370C File Offset: 0x0005190C
		public static Optional<T> MaybeLast<T>(this IEnumerable<T> items, Func<T, bool> predicate)
		{
			Optional<T> optional = Optional<T>.Nothing;
			foreach (T t in items.Where(predicate))
			{
				optional = t.Some<T>();
			}
			return optional;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00053760 File Offset: 0x00051960
		public static Optional<TValue> MaybeGet<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key)
		{
			TValue tvalue;
			if (!dict.TryGetValue(key, out tvalue))
			{
				return Optional<TValue>.Nothing;
			}
			return tvalue.Some<TValue>();
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00053784 File Offset: 0x00051984
		public static Optional<T> MaybeElementAt<T>(this IReadOnlyList<T> xs, int index)
		{
			if (index < 0 || index >= xs.Count)
			{
				return Optional<T>.Nothing;
			}
			return xs[index].Some<T>();
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000537A8 File Offset: 0x000519A8
		public static Optional<T> FirstValue<T>(this IEnumerable<Optional<T>> optionals)
		{
			return (from el in optionals
				from value in el
				select value).MaybeFirst<T>();
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00053800 File Offset: 0x00051A00
		public static Optional<T> FirstValue<S, T>(this IEnumerable<S> seq, Func<S, Optional<T>> f)
		{
			return (from el in seq
				from value in f(el)
				select value).MaybeFirst<T>();
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00053850 File Offset: 0x00051A50
		public static Optional<T> LastValue<T>(this IEnumerable<Optional<T>> optionals)
		{
			return (from el in optionals
				from value in el
				select value).MaybeLast<T>();
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000538A8 File Offset: 0x00051AA8
		public static Optional<T> LastValue<S, T>(this IEnumerable<S> seq, Func<S, Optional<T>> f)
		{
			return (from el in seq
				from value in f(el)
				select value).MaybeLast<T>();
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000538F8 File Offset: 0x00051AF8
		public static bool AnyNothing<T>(this IEnumerable<Optional<T>> optionals)
		{
			return optionals.Any((Optional<T> m) => !m.HasValue);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00053920 File Offset: 0x00051B20
		public static Optional<IReadOnlyList<T>> WholeReadOnlyListOfValues<T>(this IEnumerable<Optional<T>> optionals)
		{
			List<T> list = new List<T>();
			foreach (Optional<T> optional in optionals)
			{
				if (!optional.HasValue)
				{
					return Optional<IReadOnlyList<T>>.Nothing;
				}
				list.Add(optional.Value);
			}
			return list.Some<IReadOnlyList<T>>();
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00053990 File Offset: 0x00051B90
		public static Optional<IEnumerable<T>> WholeSequenceOfValues<T>(this IEnumerable<Optional<T>> optionals)
		{
			return optionals.WholeReadOnlyListOfValues<T>().Cast<IEnumerable<T>>();
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000539A4 File Offset: 0x00051BA4
		public static IEnumerable<TResult> SelectValues<TInput, TResult>(this IEnumerable<TInput> xs, Func<TInput, Optional<TResult>> func)
		{
			return xs.Select((TInput x) => func(x)).Where(delegate(Optional<TResult> el)
			{
				Optional<TResult> optional = el;
				return optional.HasValue;
			}).Select(delegate(Optional<TResult> el)
			{
				Optional<TResult> optional2 = el;
				return optional2.Value;
			});
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00053A18 File Offset: 0x00051C18
		public static IEnumerable<T> SelectValues<T>(this IEnumerable<Optional<T>> xs)
		{
			return xs.Where(delegate(Optional<T> x)
			{
				Optional<T> optional = x;
				return optional.HasValue;
			}).Select(delegate(Optional<T> x)
			{
				Optional<T> optional2 = x;
				return optional2.Value;
			});
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00053A70 File Offset: 0x00051C70
		public static Optional<int> MaybeMin<T>(this IEnumerable<T> xs, Func<T, int> func)
		{
			bool any = false;
			int num = xs.Aggregate(int.MaxValue, delegate(int currentMin, T x)
			{
				any = true;
				int num2 = func(x);
				if (num2 >= currentMin)
				{
					return currentMin;
				}
				return num2;
			});
			if (!any)
			{
				return Optional<int>.Nothing;
			}
			return num.Some<int>();
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00053ABD File Offset: 0x00051CBD
		public static Optional<int> MaybeMin(this IEnumerable<int> xs)
		{
			return xs.MaybeMin((int x) => x);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00053AE4 File Offset: 0x00051CE4
		public static Optional<int> MaybeMax<T>(this IEnumerable<T> xs, Func<T, int> func)
		{
			bool any = false;
			int num = xs.Aggregate(int.MinValue, delegate(int currentMin, T x)
			{
				any = true;
				int num2 = func(x);
				if (num2 <= currentMin)
				{
					return currentMin;
				}
				return num2;
			});
			if (!any)
			{
				return Optional<int>.Nothing;
			}
			return num.Some<int>();
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00053B31 File Offset: 0x00051D31
		public static Optional<int> MaybeMax(this IEnumerable<int> xs)
		{
			return xs.MaybeMax((int x) => x);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00053B58 File Offset: 0x00051D58
		public static Optional<double> MaybeAverage<T>(this IEnumerable<T> xs, Func<T, double> func)
		{
			double num;
			int num2;
			xs.Aggregate(Record.Create<double, int>(0.0, 0), (Record<double, int> acc, T x) => Record.Create<double, int>(acc.Item1 + func(x), acc.Item2 + 1)).Deconstruct(out num, out num2);
			double num3 = num;
			int num4 = num2;
			if (num4 <= 0)
			{
				return Optional<double>.Nothing;
			}
			return (num3 / (double)num4).Some<double>();
		}
	}
}
