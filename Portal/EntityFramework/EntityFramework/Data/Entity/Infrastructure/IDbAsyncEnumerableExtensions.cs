using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000244 RID: 580
	internal static class IDbAsyncEnumerableExtensions
	{
		// Token: 0x06001E4A RID: 7754 RVA: 0x000547FC File Offset: 0x000529FC
		internal static async Task ForEachAsync(this IDbAsyncEnumerable source, Action<object> action, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator enumerator = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = enumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						object obj = enumerator.Current;
						Task<bool> task = enumerator.MoveNextAsync(cancellationToken);
						action(obj);
						cultureAwaiter = task.WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
					}
					while (cultureAwaiter.GetResult());
				}
			}
			IDbAsyncEnumerator enumerator = null;
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00054851 File Offset: 0x00052A51
		internal static Task ForEachAsync<T>(this IDbAsyncEnumerable<T> source, Action<T> action, CancellationToken cancellationToken)
		{
			return IDbAsyncEnumerableExtensions.ForEachAsync<T>(source.GetAsyncEnumerator(), action, cancellationToken);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00054860 File Offset: 0x00052A60
		private static async Task ForEachAsync<T>(IDbAsyncEnumerator<T> enumerator, Action<T> action, CancellationToken cancellationToken)
		{
			using (enumerator)
			{
				cancellationToken.ThrowIfCancellationRequested();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = enumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						T t = enumerator.Current;
						Task<bool> task = enumerator.MoveNextAsync(cancellationToken);
						action(t);
						cultureAwaiter = task.WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
					}
					while (cultureAwaiter.GetResult());
				}
			}
			IDbAsyncEnumerator<T> dbAsyncEnumerator = null;
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000548B5 File Offset: 0x00052AB5
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable source)
		{
			return source.ToListAsync(CancellationToken.None);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000548C4 File Offset: 0x00052AC4
		internal static async Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable source, CancellationToken cancellationToken)
		{
			List<T> list = new List<T>();
			await source.ForEachAsync(delegate(object e)
			{
				list.Add((T)((object)e));
			}, cancellationToken).WithCurrentCulture();
			return list;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00054911 File Offset: 0x00052B11
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable<T> source)
		{
			return source.ToListAsync(CancellationToken.None);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00054920 File Offset: 0x00052B20
		internal static Task<List<T>> ToListAsync<T>(this IDbAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();
			List<T> list = new List<T>();
			source.ForEachAsync(new Action<T>(list.Add), cancellationToken).ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
					return;
				}
				if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
					return;
				}
				tcs.TrySetResult(list);
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00054984 File Offset: 0x00052B84
		internal static Task<T[]> ToArrayAsync<T>(this IDbAsyncEnumerable<T> source)
		{
			return source.ToArrayAsync(CancellationToken.None);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00054994 File Offset: 0x00052B94
		internal static async Task<T[]> ToArrayAsync<T>(this IDbAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return (await source.ToListAsync(cancellationToken).WithCurrentCulture<List<T>>()).ToArray();
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000549E1 File Offset: 0x00052BE1
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, null, CancellationToken.None);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000549F5 File Offset: 0x00052BF5
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, null, cancellationToken);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00054A05 File Offset: 0x00052C05
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, comparer, CancellationToken.None);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00054A19 File Offset: 0x00052C19
		internal static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, IDbAsyncEnumerableExtensions.IdentityFunction<TSource>.Instance, comparer, cancellationToken);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00054A29 File Offset: 0x00052C29
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, null, CancellationToken.None);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00054A39 File Offset: 0x00052C39
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, null, cancellationToken);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00054A45 File Offset: 0x00052C45
		internal static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return source.ToDictionaryAsync(keySelector, elementSelector, comparer, CancellationToken.None);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00054A58 File Offset: 0x00052C58
		internal static async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IDbAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Dictionary<TKey, TElement> d = new Dictionary<TKey, TElement>(comparer);
			await source.ForEachAsync(delegate(TSource element)
			{
				d.Add(keySelector(element), elementSelector(element));
			}, cancellationToken).WithCurrentCulture();
			return d;
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00054ABE File Offset: 0x00052CBE
		internal static IDbAsyncEnumerable<TResult> Cast<TResult>(this IDbAsyncEnumerable source)
		{
			return new IDbAsyncEnumerableExtensions.CastDbAsyncEnumerable<TResult>(source);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00054AC6 File Offset: 0x00052CC6
		internal static Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.FirstAsync(CancellationToken.None);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00054AD3 File Offset: 0x00052CD3
		internal static Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.FirstAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00054AE4 File Offset: 0x00052CE4
		internal static async Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					return e.Current;
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			throw Error.EmptySequence();
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00054B34 File Offset: 0x00052D34
		internal static async Task<TSource> FirstAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult() && predicate(e.Current))
				{
					return e.Current;
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			throw Error.NoMatch();
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00054B89 File Offset: 0x00052D89
		internal static Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.FirstOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00054B96 File Offset: 0x00052D96
		internal static Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.FirstOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x00054BA4 File Offset: 0x00052DA4
		internal static async Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					return e.Current;
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return default(TSource);
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x00054BF4 File Offset: 0x00052DF4
		internal static async Task<TSource> FirstOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult() && predicate(e.Current))
				{
					return e.Current;
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return default(TSource);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00054C49 File Offset: 0x00052E49
		internal static Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.SingleAsync(CancellationToken.None);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00054C58 File Offset: 0x00052E58
		internal static async Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					throw Error.EmptySequence();
				}
				cancellationToken.ThrowIfCancellationRequested();
				TSource result = e.Current;
				cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					return result;
				}
				result = default(TSource);
			}
			IDbAsyncEnumerator<TSource> e = null;
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x00054CA5 File Offset: 0x00052EA5
		internal static Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.SingleAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00054CB4 File Offset: 0x00052EB4
		internal static async Task<TSource> SingleAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			TSource result = default(TSource);
			long count = 0L;
			long num;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					if (predicate(e.Current))
					{
						result = e.Current;
						num = count;
						count = checked(num + 1L);
					}
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			num = count;
			if (num == 0L)
			{
				throw Error.NoMatch();
			}
			if (num != 1L)
			{
				throw Error.MoreThanOneMatch();
			}
			return result;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00054D09 File Offset: 0x00052F09
		internal static Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.SingleOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00054D18 File Offset: 0x00052F18
		internal static async Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					return default(TSource);
				}
				cancellationToken.ThrowIfCancellationRequested();
				TSource result = e.Current;
				cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					return result;
				}
				result = default(TSource);
			}
			IDbAsyncEnumerator<TSource> e = null;
			throw Error.MoreThanOneElement();
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00054D65 File Offset: 0x00052F65
		internal static Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.SingleOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00054D74 File Offset: 0x00052F74
		internal static async Task<TSource> SingleOrDefaultAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			TSource result = default(TSource);
			long count = 0L;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					if (predicate(e.Current))
					{
						result = e.Current;
						long num = count;
						count = checked(num + 1L);
					}
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			if (count < 2L)
			{
				return result;
			}
			throw Error.MoreThanOneMatch();
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00054DC9 File Offset: 0x00052FC9
		internal static Task<bool> ContainsAsync<TSource>(this IDbAsyncEnumerable<TSource> source, TSource value)
		{
			return source.ContainsAsync(value, CancellationToken.None);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x00054DD8 File Offset: 0x00052FD8
		internal static async Task<bool> ContainsAsync<TSource>(this IDbAsyncEnumerable<TSource> source, TSource value, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						goto Block_5;
					}
					if (EqualityComparer<TSource>.Default.Equals(e.Current, value))
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
				return true;
				Block_5:;
			}
			IDbAsyncEnumerator<TSource> e = null;
			return false;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00054E2D File Offset: 0x0005302D
		internal static Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.AnyAsync(CancellationToken.None);
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00054E3C File Offset: 0x0005303C
		internal static async Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (cultureAwaiter.GetResult())
				{
					return true;
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return false;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00054E89 File Offset: 0x00053089
		internal static Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.AnyAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00054E98 File Offset: 0x00053098
		internal static async Task<bool> AnyAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						goto Block_5;
					}
					if (predicate(e.Current))
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
				return true;
				Block_5:;
			}
			IDbAsyncEnumerator<TSource> e = null;
			return false;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00054EED File Offset: 0x000530ED
		internal static Task<bool> AllAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.AllAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x00054EFC File Offset: 0x000530FC
		internal static async Task<bool> AllAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						goto Block_5;
					}
					if (!predicate(e.Current))
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
				}
				return false;
				Block_5:;
			}
			IDbAsyncEnumerator<TSource> e = null;
			return true;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x00054F51 File Offset: 0x00053151
		internal static Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.CountAsync(CancellationToken.None);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00054F60 File Offset: 0x00053160
		internal static async Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int count = 0;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					int num = count;
					count = checked(num + 1);
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return count;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00054FAD File Offset: 0x000531AD
		internal static Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.CountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x00054FBC File Offset: 0x000531BC
		internal static async Task<int> CountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int count = 0;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					if (predicate(e.Current))
					{
						int num = count;
						count = checked(num + 1);
					}
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return count;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x00055011 File Offset: 0x00053211
		internal static Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.LongCountAsync(CancellationToken.None);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x00055020 File Offset: 0x00053220
		internal static async Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long count = 0L;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					long num = count;
					count = checked(num + 1L);
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return count;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0005506D File Offset: 0x0005326D
		internal static Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return source.LongCountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0005507C File Offset: 0x0005327C
		internal static async Task<long> LongCountAsync<TSource>(this IDbAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long count = 0L;
			using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					if (predicate(e.Current))
					{
						long num = count;
						count = checked(num + 1L);
					}
				}
			}
			IDbAsyncEnumerator<TSource> e = null;
			return count;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000550D1 File Offset: 0x000532D1
		internal static Task<TSource> MinAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.MinAsync(CancellationToken.None);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000550E0 File Offset: 0x000532E0
		internal static async Task<TSource> MinAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			TSource value = default(TSource);
			TSource tsource;
			if (value == null)
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						if (e.Current != null && (value == null || comparer.Compare(e.Current, value) < 0))
						{
							value = e.Current;
						}
					}
				}
				IDbAsyncEnumerator<TSource> e = null;
				tsource = value;
			}
			else
			{
				bool hasValue = false;
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						if (hasValue)
						{
							if (comparer.Compare(e.Current, value) < 0)
							{
								value = e.Current;
							}
						}
						else
						{
							value = e.Current;
							hasValue = true;
						}
					}
				}
				IDbAsyncEnumerator<TSource> e = null;
				if (!hasValue)
				{
					throw Error.EmptySequence();
				}
				tsource = value;
			}
			return tsource;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0005512D File Offset: 0x0005332D
		internal static Task<TSource> MaxAsync<TSource>(this IDbAsyncEnumerable<TSource> source)
		{
			return source.MaxAsync(CancellationToken.None);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0005513C File Offset: 0x0005333C
		internal static async Task<TSource> MaxAsync<TSource>(this IDbAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Comparer<TSource> comparer = Comparer<TSource>.Default;
			TSource value = default(TSource);
			TSource tsource;
			if (value == null)
			{
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						if (e.Current != null && (value == null || comparer.Compare(e.Current, value) > 0))
						{
							value = e.Current;
						}
					}
				}
				IDbAsyncEnumerator<TSource> e = null;
				tsource = value;
			}
			else
			{
				bool hasValue = false;
				using (IDbAsyncEnumerator<TSource> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						if (hasValue)
						{
							if (comparer.Compare(e.Current, value) > 0)
							{
								value = e.Current;
							}
						}
						else
						{
							value = e.Current;
							hasValue = true;
						}
					}
				}
				IDbAsyncEnumerator<TSource> e = null;
				if (!hasValue)
				{
					throw Error.EmptySequence();
				}
				tsource = value;
			}
			return tsource;
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00055189 File Offset: 0x00053389
		internal static Task<int> SumAsync(this IDbAsyncEnumerable<int> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00055198 File Offset: 0x00053398
		internal static async Task<int> SumAsync(this IDbAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						sum += unchecked((long)e.Current);
					}
				}
				IDbAsyncEnumerator<int> e = null;
			}
			return (int)sum;
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000551E5 File Offset: 0x000533E5
		internal static Task<int?> SumAsync(this IDbAsyncEnumerable<int?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000551F4 File Offset: 0x000533F4
		internal static async Task<int?> SumAsync(this IDbAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int?> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						int? num = e.Current;
						if (num != null)
						{
							long num2 = sum;
							num = e.Current;
							sum = num2 + unchecked((long)num.GetValueOrDefault());
						}
					}
				}
				IDbAsyncEnumerator<int?> e = null;
			}
			return new int?((int)sum);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00055241 File Offset: 0x00053441
		internal static Task<long> SumAsync(this IDbAsyncEnumerable<long> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00055250 File Offset: 0x00053450
		internal static async Task<long> SumAsync(this IDbAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						sum += e.Current;
					}
				}
				IDbAsyncEnumerator<long> e = null;
				return sum;
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0005529D File Offset: 0x0005349D
		internal static Task<long?> SumAsync(this IDbAsyncEnumerable<long?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000552AC File Offset: 0x000534AC
		internal static async Task<long?> SumAsync(this IDbAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			using (IDbAsyncEnumerator<long?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					long? num = e.Current;
					if (num != null)
					{
						long num2 = sum;
						num = e.Current;
						sum = checked(num2 + num.GetValueOrDefault());
					}
				}
			}
			IDbAsyncEnumerator<long?> e = null;
			return new long?(sum);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000552F9 File Offset: 0x000534F9
		internal static Task<float> SumAsync(this IDbAsyncEnumerable<float> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00055308 File Offset: 0x00053508
		internal static async Task<float> SumAsync(this IDbAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<float> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += (double)e.Current;
				}
			}
			IDbAsyncEnumerator<float> e = null;
			return (float)sum;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00055355 File Offset: 0x00053555
		internal static Task<float?> SumAsync(this IDbAsyncEnumerable<float?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00055364 File Offset: 0x00053564
		internal static async Task<float?> SumAsync(this IDbAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<float?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					float? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						num = e.Current;
						sum = num2 + (double)num.GetValueOrDefault();
					}
				}
			}
			IDbAsyncEnumerator<float?> e = null;
			return new float?((float)sum);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000553B1 File Offset: 0x000535B1
		internal static Task<double> SumAsync(this IDbAsyncEnumerable<double> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000553C0 File Offset: 0x000535C0
		internal static async Task<double> SumAsync(this IDbAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<double> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
				}
			}
			IDbAsyncEnumerator<double> e = null;
			return sum;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x0005540D File Offset: 0x0005360D
		internal static Task<double?> SumAsync(this IDbAsyncEnumerable<double?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x0005541C File Offset: 0x0005361C
		internal static async Task<double?> SumAsync(this IDbAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			using (IDbAsyncEnumerator<double?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					double? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						num = e.Current;
						sum = num2 + num.GetValueOrDefault();
					}
				}
			}
			IDbAsyncEnumerator<double?> e = null;
			return new double?(sum);
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00055469 File Offset: 0x00053669
		internal static Task<decimal> SumAsync(this IDbAsyncEnumerable<decimal> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00055478 File Offset: 0x00053678
		internal static async Task<decimal> SumAsync(this IDbAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			using (IDbAsyncEnumerator<decimal> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
				}
			}
			IDbAsyncEnumerator<decimal> e = null;
			return sum;
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000554C5 File Offset: 0x000536C5
		internal static Task<decimal?> SumAsync(this IDbAsyncEnumerable<decimal?> source)
		{
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000554D4 File Offset: 0x000536D4
		internal static async Task<decimal?> SumAsync(this IDbAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			using (IDbAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					decimal? num = e.Current;
					if (num != null)
					{
						decimal num2 = sum;
						num = e.Current;
						sum = num2 + num.GetValueOrDefault();
					}
				}
			}
			IDbAsyncEnumerator<decimal?> e = null;
			return new decimal?(sum);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00055521 File Offset: 0x00053721
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<int> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00055530 File Offset: 0x00053730
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<int> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						sum += unchecked((long)e.Current);
						long num = count;
						count = num + 1L;
					}
				}
				IDbAsyncEnumerator<int> e = null;
				if (count > 0L)
				{
					return (double)sum / (double)count;
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0005557D File Offset: 0x0005377D
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<int?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0005558C File Offset: 0x0005378C
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<int?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<int?> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						int? num = e.Current;
						if (num != null)
						{
							long num2 = sum;
							num = e.Current;
							sum = num2 + unchecked((long)num.GetValueOrDefault());
							long num3 = count;
							count = num3 + 1L;
						}
					}
				}
				IDbAsyncEnumerator<int?> e = null;
				if (count > 0L)
				{
					return new double?((double)sum / (double)count);
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000555D9 File Offset: 0x000537D9
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<long> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000555E8 File Offset: 0x000537E8
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<long> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						sum += e.Current;
						long num = count;
						count = num + 1L;
					}
				}
				IDbAsyncEnumerator<long> e = null;
				if (count > 0L)
				{
					return (double)sum / (double)count;
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00055635 File Offset: 0x00053835
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<long?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00055644 File Offset: 0x00053844
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<long?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long sum = 0L;
			long count = 0L;
			checked
			{
				using (IDbAsyncEnumerator<long?> e = source.GetAsyncEnumerator())
				{
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						cancellationToken.ThrowIfCancellationRequested();
						long? num = e.Current;
						if (num != null)
						{
							long num2 = sum;
							num = e.Current;
							sum = num2 + num.GetValueOrDefault();
							long num3 = count;
							count = num3 + 1L;
						}
					}
				}
				IDbAsyncEnumerator<long?> e = null;
				if (count > 0L)
				{
					return new double?((double)sum / (double)count);
				}
				throw Error.EmptySequence();
			}
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x00055691 File Offset: 0x00053891
		internal static Task<float> AverageAsync(this IDbAsyncEnumerable<float> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000556A0 File Offset: 0x000538A0
		internal static async Task<float> AverageAsync(this IDbAsyncEnumerable<float> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<float> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += (double)e.Current;
					long num = count;
					count = checked(num + 1L);
				}
			}
			IDbAsyncEnumerator<float> e = null;
			if (count > 0L)
			{
				return (float)(sum / (double)count);
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000556ED File Offset: 0x000538ED
		internal static Task<float?> AverageAsync(this IDbAsyncEnumerable<float?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000556FC File Offset: 0x000538FC
		internal static async Task<float?> AverageAsync(this IDbAsyncEnumerable<float?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<float?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					float? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						num = e.Current;
						sum = num2 + (double)num.GetValueOrDefault();
						long num3 = count;
						count = checked(num3 + 1L);
					}
				}
			}
			IDbAsyncEnumerator<float?> e = null;
			if (count > 0L)
			{
				return new float?((float)(sum / (double)count));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00055749 File Offset: 0x00053949
		internal static Task<double> AverageAsync(this IDbAsyncEnumerable<double> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00055758 File Offset: 0x00053958
		internal static async Task<double> AverageAsync(this IDbAsyncEnumerable<double> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<double> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
					long num = count;
					count = checked(num + 1L);
				}
			}
			IDbAsyncEnumerator<double> e = null;
			if (count > 0L)
			{
				return (double)((float)(sum / (double)count));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000557A5 File Offset: 0x000539A5
		internal static Task<double?> AverageAsync(this IDbAsyncEnumerable<double?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000557B4 File Offset: 0x000539B4
		internal static async Task<double?> AverageAsync(this IDbAsyncEnumerable<double?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			double sum = 0.0;
			long count = 0L;
			using (IDbAsyncEnumerator<double?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					double? num = e.Current;
					if (num != null)
					{
						double num2 = sum;
						num = e.Current;
						sum = num2 + num.GetValueOrDefault();
						long num3 = count;
						count = checked(num3 + 1L);
					}
				}
			}
			IDbAsyncEnumerator<double?> e = null;
			if (count > 0L)
			{
				return new double?((double)((float)(sum / (double)count)));
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00055801 File Offset: 0x00053A01
		internal static Task<decimal> AverageAsync(this IDbAsyncEnumerable<decimal> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00055810 File Offset: 0x00053A10
		internal static async Task<decimal> AverageAsync(this IDbAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			long count = 0L;
			using (IDbAsyncEnumerator<decimal> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					sum += e.Current;
					long num = count;
					count = checked(num + 1L);
				}
			}
			IDbAsyncEnumerator<decimal> e = null;
			if (count > 0L)
			{
				return sum / count;
			}
			throw Error.EmptySequence();
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0005585D File Offset: 0x00053A5D
		internal static Task<decimal?> AverageAsync(this IDbAsyncEnumerable<decimal?> source)
		{
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0005586C File Offset: 0x00053A6C
		internal static async Task<decimal?> AverageAsync(this IDbAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			decimal sum = 0m;
			long count = 0L;
			using (IDbAsyncEnumerator<decimal?> e = source.GetAsyncEnumerator())
			{
				for (;;)
				{
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = e.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (!cultureAwaiter.GetResult())
					{
						break;
					}
					cancellationToken.ThrowIfCancellationRequested();
					decimal? num = e.Current;
					if (num != null)
					{
						decimal num2 = sum;
						num = e.Current;
						sum = num2 + num.GetValueOrDefault();
						long num3 = count;
						count = checked(num3 + 1L);
					}
				}
			}
			IDbAsyncEnumerator<decimal?> e = null;
			if (count > 0L)
			{
				return new decimal?(sum / count);
			}
			throw Error.EmptySequence();
		}

		// Token: 0x02000943 RID: 2371
		private class CastDbAsyncEnumerable<TResult> : IDbAsyncEnumerable<TResult>, IDbAsyncEnumerable
		{
			// Token: 0x06005DA6 RID: 23974 RVA: 0x001410C6 File Offset: 0x0013F2C6
			public CastDbAsyncEnumerable(IDbAsyncEnumerable sourceEnumerable)
			{
				this._underlyingEnumerable = sourceEnumerable;
			}

			// Token: 0x06005DA7 RID: 23975 RVA: 0x001410D5 File Offset: 0x0013F2D5
			public IDbAsyncEnumerator<TResult> GetAsyncEnumerator()
			{
				return this._underlyingEnumerable.GetAsyncEnumerator().Cast<TResult>();
			}

			// Token: 0x06005DA8 RID: 23976 RVA: 0x001410E7 File Offset: 0x0013F2E7
			IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
			{
				return this._underlyingEnumerable.GetAsyncEnumerator();
			}

			// Token: 0x0400259F RID: 9631
			private readonly IDbAsyncEnumerable _underlyingEnumerable;
		}

		// Token: 0x02000944 RID: 2372
		private static class IdentityFunction<TElement>
		{
			// Token: 0x1700106D RID: 4205
			// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x001410F4 File Offset: 0x0013F2F4
			internal static Func<TElement, TElement> Instance
			{
				get
				{
					return (TElement x) => x;
				}
			}
		}
	}
}
