using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x0200006C RID: 108
	public static class QueryableExtensions
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000C168 File Offset: 0x0000A368
		public static IQueryable<T> Include<T>(this IQueryable<T> source, string path)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotEmpty(path, "path");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery != null)
			{
				return dbQuery.Include(path);
			}
			ObjectQuery<T> objectQuery = source as ObjectQuery<T>;
			if (objectQuery != null)
			{
				return objectQuery.Include(path);
			}
			return QueryableExtensions.CommonInclude<IQueryable<T>>(source, path);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		public static IQueryable Include(this IQueryable source, string path)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotEmpty(path, "path");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonInclude<IQueryable>(source, path);
			}
			return dbQuery.Include(path);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		private static T CommonInclude<T>(T source, string path)
		{
			MethodInfo runtimeMethod = source.GetType().GetRuntimeMethod("Include", (MethodInfo p) => p.IsPublic && !p.IsStatic, new Type[][]
			{
				new Type[] { typeof(string) },
				new Type[] { typeof(IComparable) },
				new Type[] { typeof(ICloneable) },
				new Type[] { typeof(IComparable<string>) },
				new Type[] { typeof(IEnumerable<char>) },
				new Type[] { typeof(IEnumerable) },
				new Type[] { typeof(IEquatable<string>) },
				new Type[] { typeof(object) }
			});
			if (runtimeMethod != null && typeof(T).IsAssignableFrom(runtimeMethod.ReturnType))
			{
				return (T)((object)runtimeMethod.Invoke(source, new object[] { path }));
			}
			return source;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000C330 File Offset: 0x0000A530
		public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Expression<Func<T, TProperty>>>(path, "path");
			string text;
			if (!DbHelpers.TryParsePath(path.Body, out text) || text == null)
			{
				throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
			}
			return source.Include(text);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000C380 File Offset: 0x0000A580
		public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source) where T : class
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsNoTracking<IQueryable<T>>(source);
			}
			return dbQuery.AsNoTracking();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000C3B4 File Offset: 0x0000A5B4
		public static IQueryable AsNoTracking(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsNoTracking<IQueryable>(source);
			}
			return dbQuery.AsNoTracking();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		private static T CommonAsNoTracking<T>(T source) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateNoTrackingQuery(objectQuery));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("AsNoTracking", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, null));
			}
			return source;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000C460 File Offset: 0x0000A660
		[Obsolete("LINQ queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public static IQueryable<T> AsStreaming<T>(this IQueryable<T> source)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsStreaming<IQueryable<T>>(source);
			}
			return dbQuery.AsStreaming();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000C494 File Offset: 0x0000A694
		[Obsolete("LINQ queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public static IQueryable AsStreaming(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonAsStreaming<IQueryable>(source);
			}
			return dbQuery.AsStreaming();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		private static T CommonAsStreaming<T>(T source) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateStreamingQuery(objectQuery));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("AsStreaming", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, null));
			}
			return source;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000C540 File Offset: 0x0000A740
		internal static IQueryable<T> WithExecutionStrategy<T>(this IQueryable<T> source, IDbExecutionStrategy executionStrategy)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			DbQuery<T> dbQuery = source as DbQuery<T>;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonWithExecutionStrategy<IQueryable<T>>(source, executionStrategy);
			}
			return dbQuery.WithExecutionStrategy(executionStrategy);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000C574 File Offset: 0x0000A774
		internal static IQueryable WithExecutionStrategy(this IQueryable source, IDbExecutionStrategy executionStrategy)
		{
			Check.NotNull<IQueryable>(source, "source");
			DbQuery dbQuery = source as DbQuery;
			if (dbQuery == null)
			{
				return QueryableExtensions.CommonWithExecutionStrategy<IQueryable>(source, executionStrategy);
			}
			return dbQuery.WithExecutionStrategy(executionStrategy);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		private static T CommonWithExecutionStrategy<T>(T source, IDbExecutionStrategy executionStrategy) where T : class
		{
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return (T)((object)DbHelpers.CreateQueryWithExecutionStrategy(objectQuery, executionStrategy));
			}
			MethodInfo publicInstanceMethod = source.GetType().GetPublicInstanceMethod("WithExecutionStrategy", new Type[0]);
			if (publicInstanceMethod != null && typeof(T).IsAssignableFrom(publicInstanceMethod.ReturnType))
			{
				return (T)((object)publicInstanceMethod.Invoke(source, new object[] { executionStrategy }));
			}
			return source;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000C62C File Offset: 0x0000A82C
		public static void Load(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			using (IEnumerator enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000C678 File Offset: 0x0000A878
		public static Task LoadAsync(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.LoadAsync(CancellationToken.None);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000C691 File Offset: 0x0000A891
		public static Task LoadAsync(this IQueryable source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.ForEachAsync(delegate(object e)
			{
			}, cancellationToken);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000C6C5 File Offset: 0x0000A8C5
		public static Task ForEachAsync(this IQueryable source, Action<object> action)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotNull<Action<object>>(action, "action");
			return source.AsDbAsyncEnumerable().ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		public static Task ForEachAsync(this IQueryable source, Action<object> action, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			Check.NotNull<Action<object>>(action, "action");
			return source.AsDbAsyncEnumerable().ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000C717 File Offset: 0x0000A917
		public static Task ForEachAsync<T>(this IQueryable<T> source, Action<T> action)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Action<T>>(action, "action");
			return source.AsDbAsyncEnumerable<T>().ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000C742 File Offset: 0x0000A942
		public static Task ForEachAsync<T>(this IQueryable<T> source, Action<T> action, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<T>>(source, "source");
			Check.NotNull<Action<T>>(action, "action");
			return source.AsDbAsyncEnumerable<T>().ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000C769 File Offset: 0x0000A969
		public static Task<List<object>> ToListAsync(this IQueryable source)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.AsDbAsyncEnumerable().ToListAsync<object>();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000C782 File Offset: 0x0000A982
		public static Task<List<object>> ToListAsync(this IQueryable source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable>(source, "source");
			return source.AsDbAsyncEnumerable().ToListAsync(cancellationToken);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C79C File Offset: 0x0000A99C
		public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToListAsync<TSource>();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToListAsync(cancellationToken);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToArrayAsync<TSource>();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AsDbAsyncEnumerable<TSource>().ToArrayAsync(cancellationToken);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C802 File Offset: 0x0000AA02
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000C828 File Offset: 0x0000AA28
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, cancellationToken);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000C84F File Offset: 0x0000AA4F
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, comparer);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C876 File Offset: 0x0000AA76
		public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, comparer, cancellationToken);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C89E File Offset: 0x0000AA9E
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000C905 File Offset: 0x0000AB05
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, comparer);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000C939 File Offset: 0x0000AB39
		public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Func<TSource, TKey>>(keySelector, "keySelector");
			Check.NotNull<Func<TSource, TElement>>(elementSelector, "elementSelector");
			return source.AsDbAsyncEnumerable<TSource>().ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000C96F File Offset: 0x0000AB6F
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.FirstAsync(CancellationToken.None);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C988 File Offset: 0x0000AB88
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._first.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C9F6 File Offset: 0x0000ABF6
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.FirstAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._first_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.FirstOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._firstOrDefault.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000CB26 File Offset: 0x0000AD26
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.FirstOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._firstOrDefault_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000CBCF File Offset: 0x0000ADCF
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.SingleAsync(CancellationToken.None);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._single.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000CC56 File Offset: 0x0000AE56
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.SingleAsync(predicate, CancellationToken.None);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._single_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000CCFF File Offset: 0x0000AEFF
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.SingleOrDefaultAsync(CancellationToken.None);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._singleOrDefault.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000CD86 File Offset: 0x0000AF86
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.SingleOrDefaultAsync(predicate, CancellationToken.None);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._singleOrDefault_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000CE2F File Offset: 0x0000B02F
		public static Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> source, TSource item)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.ContainsAsync(item, CancellationToken.None);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public static Task<bool> ContainsAsync<TSource>(this IQueryable<TSource> source, TSource item, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._contains.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Constant(item, typeof(TSource))
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000CED2 File Offset: 0x0000B0D2
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.AnyAsync(CancellationToken.None);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._any.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000CF5A File Offset: 0x0000B15A
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.AnyAsync(predicate, CancellationToken.None);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000CF80 File Offset: 0x0000B180
		public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._any_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000D003 File Offset: 0x0000B203
		public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.AllAsync(predicate, CancellationToken.None);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000D02C File Offset: 0x0000B22C
		public static Task<bool> AllAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<bool>(Expression.Call(null, QueryableExtensions._all_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.CountAsync(CancellationToken.None);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._count.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000D136 File Offset: 0x0000B336
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.CountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000D15C File Offset: 0x0000B35C
		public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._count_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D1DF File Offset: 0x0000B3DF
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.LongCountAsync(CancellationToken.None);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._longCount.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000D266 File Offset: 0x0000B466
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			return source.LongCountAsync(predicate, CancellationToken.None);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000D28C File Offset: 0x0000B48C
		public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, bool>>>(predicate, "predicate");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._longCount_Predicate.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(predicate)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D30F File Offset: 0x0000B50F
		public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.MinAsync(CancellationToken.None);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D328 File Offset: 0x0000B528
		public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._min.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D396 File Offset: 0x0000B596
		public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			return source.MinAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public static Task<TResult> MinAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TResult>(Expression.Call(null, QueryableExtensions._min_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource),
					typeof(TResult)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D44C File Offset: 0x0000B64C
		public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			return source.MaxAsync(CancellationToken.None);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000D468 File Offset: 0x0000B668
		public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TSource>(Expression.Call(null, QueryableExtensions._max.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			return source.MaxAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		public static Task<TResult> MaxAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, TResult>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<TResult>(Expression.Call(null, QueryableExtensions._max_Selector.MakeGenericMethod(new Type[]
				{
					typeof(TSource),
					typeof(TResult)
				}), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D58C File Offset: 0x0000B78C
		public static Task<int> SumAsync(this IQueryable<int> source)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		public static Task<int> SumAsync(this IQueryable<int> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._sum_Int, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000D5FE File Offset: 0x0000B7FE
		public static Task<int?> SumAsync(this IQueryable<int?> source)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D618 File Offset: 0x0000B818
		public static Task<int?> SumAsync(this IQueryable<int?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int?>(Expression.Call(null, QueryableExtensions._sum_IntNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000D66E File Offset: 0x0000B86E
		public static Task<long> SumAsync(this IQueryable<long> source)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000D688 File Offset: 0x0000B888
		public static Task<long> SumAsync(this IQueryable<long> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._sum_Long, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D6DE File Offset: 0x0000B8DE
		public static Task<long?> SumAsync(this IQueryable<long?> source)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public static Task<long?> SumAsync(this IQueryable<long?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long?>(Expression.Call(null, QueryableExtensions._sum_LongNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D74E File Offset: 0x0000B94E
		public static Task<float> SumAsync(this IQueryable<float> source)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D768 File Offset: 0x0000B968
		public static Task<float> SumAsync(this IQueryable<float> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._sum_Float, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D7BE File Offset: 0x0000B9BE
		public static Task<float?> SumAsync(this IQueryable<float?> source)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		public static Task<float?> SumAsync(this IQueryable<float?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._sum_FloatNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D82E File Offset: 0x0000BA2E
		public static Task<double> SumAsync(this IQueryable<double> source)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000D848 File Offset: 0x0000BA48
		public static Task<double> SumAsync(this IQueryable<double> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._sum_Double, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D89E File Offset: 0x0000BA9E
		public static Task<double?> SumAsync(this IQueryable<double?> source)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public static Task<double?> SumAsync(this IQueryable<double?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._sum_DoubleNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000D90E File Offset: 0x0000BB0E
		public static Task<decimal> SumAsync(this IQueryable<decimal> source)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000D928 File Offset: 0x0000BB28
		public static Task<decimal> SumAsync(this IQueryable<decimal> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._sum_Decimal, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D97E File Offset: 0x0000BB7E
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> source)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			return source.SumAsync(CancellationToken.None);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D998 File Offset: 0x0000BB98
		public static Task<decimal?> SumAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._sum_DecimalNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000D9EE File Offset: 0x0000BBEE
		public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public static Task<int> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int>(Expression.Call(null, QueryableExtensions._sum_Int_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000DA97 File Offset: 0x0000BC97
		public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000DAC0 File Offset: 0x0000BCC0
		public static Task<int?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<int?>(Expression.Call(null, QueryableExtensions._sum_IntNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000DB43 File Offset: 0x0000BD43
		public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public static Task<long> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long>(Expression.Call(null, QueryableExtensions._sum_Long_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000DBEF File Offset: 0x0000BDEF
		public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000DC18 File Offset: 0x0000BE18
		public static Task<long?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<long?>(Expression.Call(null, QueryableExtensions._sum_LongNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public static Task<float> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._sum_Float_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000DD47 File Offset: 0x0000BF47
		public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public static Task<float?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._sum_FloatNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public static Task<double> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._sum_Double_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000DE9F File Offset: 0x0000C09F
		public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		public static Task<double?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._sum_DoubleNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000DF4B File Offset: 0x0000C14B
		public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000DF74 File Offset: 0x0000C174
		public static Task<decimal> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._sum_Decimal_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000DFF7 File Offset: 0x0000C1F7
		public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			return source.SumAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E020 File Offset: 0x0000C220
		public static Task<decimal?> SumAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._sum_DecimalNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E0A3 File Offset: 0x0000C2A3
		public static Task<double> AverageAsync(this IQueryable<int> source)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		public static Task<double> AverageAsync(this IQueryable<int> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Int, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E112 File Offset: 0x0000C312
		public static Task<double?> AverageAsync(this IQueryable<int?> source)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E12C File Offset: 0x0000C32C
		public static Task<double?> AverageAsync(this IQueryable<int?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<int?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_IntNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E182 File Offset: 0x0000C382
		public static Task<double> AverageAsync(this IQueryable<long> source)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E19C File Offset: 0x0000C39C
		public static Task<double> AverageAsync(this IQueryable<long> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Long, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		public static Task<double?> AverageAsync(this IQueryable<long?> source)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E20C File Offset: 0x0000C40C
		public static Task<double?> AverageAsync(this IQueryable<long?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<long?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_LongNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E262 File Offset: 0x0000C462
		public static Task<float> AverageAsync(this IQueryable<float> source)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E27C File Offset: 0x0000C47C
		public static Task<float> AverageAsync(this IQueryable<float> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._average_Float, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E2D2 File Offset: 0x0000C4D2
		public static Task<float?> AverageAsync(this IQueryable<float?> source)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E2EC File Offset: 0x0000C4EC
		public static Task<float?> AverageAsync(this IQueryable<float?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<float?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._average_FloatNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E342 File Offset: 0x0000C542
		public static Task<double> AverageAsync(this IQueryable<double> source)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000E35C File Offset: 0x0000C55C
		public static Task<double> AverageAsync(this IQueryable<double> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Double, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E3B2 File Offset: 0x0000C5B2
		public static Task<double?> AverageAsync(this IQueryable<double?> source)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		public static Task<double?> AverageAsync(this IQueryable<double?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<double?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_DoubleNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000E422 File Offset: 0x0000C622
		public static Task<decimal> AverageAsync(this IQueryable<decimal> source)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000E43C File Offset: 0x0000C63C
		public static Task<decimal> AverageAsync(this IQueryable<decimal> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._average_Decimal, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000E492 File Offset: 0x0000C692
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			return source.AverageAsync(CancellationToken.None);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public static Task<decimal?> AverageAsync(this IQueryable<decimal?> source, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<decimal?>>(source, "source");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._average_DecimalNullable, new Expression[] { source.Expression }), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000E502 File Offset: 0x0000C702
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000E528 File Offset: 0x0000C728
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Int_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E5AB File Offset: 0x0000C7AB
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, int?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_IntNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000E657 File Offset: 0x0000C857
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000E680 File Offset: 0x0000C880
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Long_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000E703 File Offset: 0x0000C903
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000E72C File Offset: 0x0000C92C
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, long?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_LongNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E7AF File Offset: 0x0000C9AF
		public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		public static Task<float> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float>(Expression.Call(null, QueryableExtensions._average_Float_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E85B File Offset: 0x0000CA5B
		public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000E884 File Offset: 0x0000CA84
		public static Task<float?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, float?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<float?>(Expression.Call(null, QueryableExtensions._average_FloatNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000E907 File Offset: 0x0000CB07
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000E930 File Offset: 0x0000CB30
		public static Task<double> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double>(Expression.Call(null, QueryableExtensions._average_Double_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000E9B3 File Offset: 0x0000CBB3
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public static Task<double?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, double?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<double?>(Expression.Call(null, QueryableExtensions._average_DoubleNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EA5F File Offset: 0x0000CC5F
		public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public static Task<decimal> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal>(Expression.Call(null, QueryableExtensions._average_Decimal_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000EB0B File Offset: 0x0000CD0B
		public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			return source.AverageAsync(selector, CancellationToken.None);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000EB34 File Offset: 0x0000CD34
		public static Task<decimal?> AverageAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector, CancellationToken cancellationToken)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<TSource, decimal?>>>(selector, "selector");
			cancellationToken.ThrowIfCancellationRequested();
			IDbAsyncQueryProvider dbAsyncQueryProvider = source.Provider as IDbAsyncQueryProvider;
			if (dbAsyncQueryProvider != null)
			{
				return dbAsyncQueryProvider.ExecuteAsync<decimal?>(Expression.Call(null, QueryableExtensions._average_DecimalNullable_Selector.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[]
				{
					source.Expression,
					Expression.Quote(selector)
				}), cancellationToken);
			}
			throw Error.IQueryable_Provider_Not_Async();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, Expression<Func<int>> countAccessor)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<int>>>(countAccessor, "countAccessor");
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, QueryableExtensions._skip.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression, countAccessor.Body }));
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, Expression<Func<int>> countAccessor)
		{
			Check.NotNull<IQueryable<TSource>>(source, "source");
			Check.NotNull<Expression<Func<int>>>(countAccessor, "countAccessor");
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, QueryableExtensions._take.MakeGenericMethod(new Type[] { typeof(TSource) }), new Expression[] { source.Expression, countAccessor.Body }));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000EC90 File Offset: 0x0000CE90
		internal static ObjectQuery TryGetObjectQuery(this IQueryable source)
		{
			if (source == null)
			{
				return null;
			}
			ObjectQuery objectQuery = source as ObjectQuery;
			if (objectQuery != null)
			{
				return objectQuery;
			}
			IInternalQueryAdapter internalQueryAdapter = source as IInternalQueryAdapter;
			if (internalQueryAdapter != null)
			{
				return internalQueryAdapter.InternalQuery.ObjectQuery;
			}
			return null;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		private static IDbAsyncEnumerable AsDbAsyncEnumerable(this IQueryable source)
		{
			IDbAsyncEnumerable dbAsyncEnumerable = source as IDbAsyncEnumerable;
			if (dbAsyncEnumerable != null)
			{
				return dbAsyncEnumerable;
			}
			throw Error.IQueryable_Not_Async(string.Empty);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		private static IDbAsyncEnumerable<T> AsDbAsyncEnumerable<T>(this IQueryable<T> source)
		{
			IDbAsyncEnumerable<T> dbAsyncEnumerable = source as IDbAsyncEnumerable<T>;
			if (dbAsyncEnumerable != null)
			{
				return dbAsyncEnumerable;
			}
			string text = "<";
			Type typeFromHandle = typeof(T);
			throw Error.IQueryable_Not_Async(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + ">");
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000ED2F File Offset: 0x0000CF2F
		private static MethodInfo GetMethod(string methodName, Func<Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 0);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000ED39 File Offset: 0x0000CF39
		private static MethodInfo GetMethod(string methodName, Func<Type, Type, Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 2);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000ED43 File Offset: 0x0000CF43
		private static MethodInfo GetMethod(string methodName, Func<Type, Type[]> getParameterTypes)
		{
			return QueryableExtensions.GetMethod(methodName, getParameterTypes, 1);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000ED50 File Offset: 0x0000CF50
		private static MethodInfo GetMethod(string methodName, Delegate getParameterTypesDelegate, int genericArgumentsCount)
		{
			foreach (MethodInfo methodInfo in typeof(Queryable).GetDeclaredMethods(methodName))
			{
				Type[] genericArguments = methodInfo.GetGenericArguments();
				if (genericArguments.Length == genericArgumentsCount)
				{
					MethodInfo methodInfo2 = methodInfo;
					object[] array = genericArguments;
					if (QueryableExtensions.Matches(methodInfo2, (Type[])getParameterTypesDelegate.DynamicInvoke(array)))
					{
						return methodInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		private static bool Matches(MethodInfo methodInfo, Type[] parameterTypes)
		{
			return (from p in methodInfo.GetParameters()
				select p.ParameterType).SequenceEqual(parameterTypes);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000EE00 File Offset: 0x0000D000
		private static string PrettyPrint(MethodInfo getParameterTypesMethod, int genericArgumentsCount)
		{
			Type[] array = new Type[genericArgumentsCount];
			for (int i = 0; i < genericArgumentsCount; i++)
			{
				array[i] = typeof(object);
			}
			object obj = null;
			object[] array2 = array;
			Type[] array3 = (Type[])getParameterTypesMethod.Invoke(obj, array2);
			string[] array4 = new string[array3.Length];
			for (int j = 0; j < array3.Length; j++)
			{
				array4[j] = array3[j].ToString();
			}
			return "(" + string.Join(", ", array4) + ")";
		}

		// Token: 0x040000CC RID: 204
		private static readonly MethodInfo _first = QueryableExtensions.GetMethod("First", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000CD RID: 205
		private static readonly MethodInfo _first_Predicate = QueryableExtensions.GetMethod("First", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000CE RID: 206
		private static readonly MethodInfo _firstOrDefault = QueryableExtensions.GetMethod("FirstOrDefault", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000CF RID: 207
		private static readonly MethodInfo _firstOrDefault_Predicate = QueryableExtensions.GetMethod("FirstOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000D0 RID: 208
		private static readonly MethodInfo _single = QueryableExtensions.GetMethod("Single", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000D1 RID: 209
		private static readonly MethodInfo _single_Predicate = QueryableExtensions.GetMethod("Single", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000D2 RID: 210
		private static readonly MethodInfo _singleOrDefault = QueryableExtensions.GetMethod("SingleOrDefault", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000D3 RID: 211
		private static readonly MethodInfo _singleOrDefault_Predicate = QueryableExtensions.GetMethod("SingleOrDefault", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000D4 RID: 212
		private static readonly MethodInfo _contains = QueryableExtensions.GetMethod("Contains", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			T
		});

		// Token: 0x040000D5 RID: 213
		private static readonly MethodInfo _any = QueryableExtensions.GetMethod("Any", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000D6 RID: 214
		private static readonly MethodInfo _any_Predicate = QueryableExtensions.GetMethod("Any", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000D7 RID: 215
		private static readonly MethodInfo _all_Predicate = QueryableExtensions.GetMethod("All", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000D8 RID: 216
		private static readonly MethodInfo _count = QueryableExtensions.GetMethod("Count", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000D9 RID: 217
		private static readonly MethodInfo _count_Predicate = QueryableExtensions.GetMethod("Count", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000DA RID: 218
		private static readonly MethodInfo _longCount = QueryableExtensions.GetMethod("LongCount", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000DB RID: 219
		private static readonly MethodInfo _longCount_Predicate = QueryableExtensions.GetMethod("LongCount", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(bool)
			}) })
		});

		// Token: 0x040000DC RID: 220
		private static readonly MethodInfo _min = QueryableExtensions.GetMethod("Min", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000DD RID: 221
		private static readonly MethodInfo _min_Selector = QueryableExtensions.GetMethod("Min", (Type T, Type U) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[] { T, U }) })
		});

		// Token: 0x040000DE RID: 222
		private static readonly MethodInfo _max = QueryableExtensions.GetMethod("Max", (Type T) => new Type[] { typeof(IQueryable<>).MakeGenericType(new Type[] { T }) });

		// Token: 0x040000DF RID: 223
		private static readonly MethodInfo _max_Selector = QueryableExtensions.GetMethod("Max", (Type T, Type U) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[] { T, U }) })
		});

		// Token: 0x040000E0 RID: 224
		private static readonly MethodInfo _sum_Int = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<int>) });

		// Token: 0x040000E1 RID: 225
		private static readonly MethodInfo _sum_IntNullable = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<int?>) });

		// Token: 0x040000E2 RID: 226
		private static readonly MethodInfo _sum_Long = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<long>) });

		// Token: 0x040000E3 RID: 227
		private static readonly MethodInfo _sum_LongNullable = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<long?>) });

		// Token: 0x040000E4 RID: 228
		private static readonly MethodInfo _sum_Float = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<float>) });

		// Token: 0x040000E5 RID: 229
		private static readonly MethodInfo _sum_FloatNullable = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<float?>) });

		// Token: 0x040000E6 RID: 230
		private static readonly MethodInfo _sum_Double = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<double>) });

		// Token: 0x040000E7 RID: 231
		private static readonly MethodInfo _sum_DoubleNullable = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<double?>) });

		// Token: 0x040000E8 RID: 232
		private static readonly MethodInfo _sum_Decimal = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<decimal>) });

		// Token: 0x040000E9 RID: 233
		private static readonly MethodInfo _sum_DecimalNullable = QueryableExtensions.GetMethod("Sum", () => new Type[] { typeof(IQueryable<decimal?>) });

		// Token: 0x040000EA RID: 234
		private static readonly MethodInfo _sum_Int_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(int)
			}) })
		});

		// Token: 0x040000EB RID: 235
		private static readonly MethodInfo _sum_IntNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(int?)
			}) })
		});

		// Token: 0x040000EC RID: 236
		private static readonly MethodInfo _sum_Long_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(long)
			}) })
		});

		// Token: 0x040000ED RID: 237
		private static readonly MethodInfo _sum_LongNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(long?)
			}) })
		});

		// Token: 0x040000EE RID: 238
		private static readonly MethodInfo _sum_Float_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(float)
			}) })
		});

		// Token: 0x040000EF RID: 239
		private static readonly MethodInfo _sum_FloatNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(float?)
			}) })
		});

		// Token: 0x040000F0 RID: 240
		private static readonly MethodInfo _sum_Double_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(double)
			}) })
		});

		// Token: 0x040000F1 RID: 241
		private static readonly MethodInfo _sum_DoubleNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(double?)
			}) })
		});

		// Token: 0x040000F2 RID: 242
		private static readonly MethodInfo _sum_Decimal_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(decimal)
			}) })
		});

		// Token: 0x040000F3 RID: 243
		private static readonly MethodInfo _sum_DecimalNullable_Selector = QueryableExtensions.GetMethod("Sum", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(decimal?)
			}) })
		});

		// Token: 0x040000F4 RID: 244
		private static readonly MethodInfo _average_Int = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<int>) });

		// Token: 0x040000F5 RID: 245
		private static readonly MethodInfo _average_IntNullable = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<int?>) });

		// Token: 0x040000F6 RID: 246
		private static readonly MethodInfo _average_Long = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<long>) });

		// Token: 0x040000F7 RID: 247
		private static readonly MethodInfo _average_LongNullable = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<long?>) });

		// Token: 0x040000F8 RID: 248
		private static readonly MethodInfo _average_Float = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<float>) });

		// Token: 0x040000F9 RID: 249
		private static readonly MethodInfo _average_FloatNullable = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<float?>) });

		// Token: 0x040000FA RID: 250
		private static readonly MethodInfo _average_Double = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<double>) });

		// Token: 0x040000FB RID: 251
		private static readonly MethodInfo _average_DoubleNullable = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<double?>) });

		// Token: 0x040000FC RID: 252
		private static readonly MethodInfo _average_Decimal = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<decimal>) });

		// Token: 0x040000FD RID: 253
		private static readonly MethodInfo _average_DecimalNullable = QueryableExtensions.GetMethod("Average", () => new Type[] { typeof(IQueryable<decimal?>) });

		// Token: 0x040000FE RID: 254
		private static readonly MethodInfo _average_Int_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(int)
			}) })
		});

		// Token: 0x040000FF RID: 255
		private static readonly MethodInfo _average_IntNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(int?)
			}) })
		});

		// Token: 0x04000100 RID: 256
		private static readonly MethodInfo _average_Long_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(long)
			}) })
		});

		// Token: 0x04000101 RID: 257
		private static readonly MethodInfo _average_LongNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(long?)
			}) })
		});

		// Token: 0x04000102 RID: 258
		private static readonly MethodInfo _average_Float_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(float)
			}) })
		});

		// Token: 0x04000103 RID: 259
		private static readonly MethodInfo _average_FloatNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(float?)
			}) })
		});

		// Token: 0x04000104 RID: 260
		private static readonly MethodInfo _average_Double_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(double)
			}) })
		});

		// Token: 0x04000105 RID: 261
		private static readonly MethodInfo _average_DoubleNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(double?)
			}) })
		});

		// Token: 0x04000106 RID: 262
		private static readonly MethodInfo _average_Decimal_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(decimal)
			}) })
		});

		// Token: 0x04000107 RID: 263
		private static readonly MethodInfo _average_DecimalNullable_Selector = QueryableExtensions.GetMethod("Average", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(Expression<>).MakeGenericType(new Type[] { typeof(Func<, >).MakeGenericType(new Type[]
			{
				T,
				typeof(decimal?)
			}) })
		});

		// Token: 0x04000108 RID: 264
		private static readonly MethodInfo _skip = QueryableExtensions.GetMethod("Skip", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(int)
		});

		// Token: 0x04000109 RID: 265
		private static readonly MethodInfo _take = QueryableExtensions.GetMethod("Take", (Type T) => new Type[]
		{
			typeof(IQueryable<>).MakeGenericType(new Type[] { T }),
			typeof(int)
		});
	}
}
