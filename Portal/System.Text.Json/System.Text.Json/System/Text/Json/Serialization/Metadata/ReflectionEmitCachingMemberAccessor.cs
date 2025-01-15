using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000B5 RID: 181
	[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
	internal sealed class ReflectionEmitCachingMemberAccessor : MemberAccessor
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002CCEF File Offset: 0x0002AEEF
		public static void Clear()
		{
			ReflectionEmitCachingMemberAccessor.s_cache.Clear();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002CCFB File Offset: 0x0002AEFB
		public override Action<TCollection, object> CreateAddMethodDelegate<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] TCollection>()
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Action<TCollection, object>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateAddMethodDelegate", typeof(TCollection), null), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> _) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateAddMethodDelegate<TCollection>());
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002CD3B File Offset: 0x0002AF3B
		public override Func<object> CreateParameterlessConstructor([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type type, ConstructorInfo ctorInfo)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<object>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateParameterlessConstructor", type, ctorInfo), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateParameterlessConstructor(key.Item2, (ConstructorInfo)key.Item3));
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002CD72 File Offset: 0x0002AF72
		public override Func<object, TProperty> CreateFieldGetter<TProperty>(FieldInfo fieldInfo)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<object, TProperty>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateFieldGetter", typeof(TProperty), fieldInfo), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateFieldGetter<TProperty>((FieldInfo)key.Item3));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002CDB2 File Offset: 0x0002AFB2
		public override Action<object, TProperty> CreateFieldSetter<TProperty>(FieldInfo fieldInfo)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Action<object, TProperty>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateFieldSetter", typeof(TProperty), fieldInfo), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateFieldSetter<TProperty>((FieldInfo)key.Item3));
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0002CDF2 File Offset: 0x0002AFF2
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection> CreateImmutableDictionaryCreateRangeDelegate<TCollection, TKey, TValue>()
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<IEnumerable<KeyValuePair<TKey, TValue>>, TCollection>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateImmutableDictionaryCreateRangeDelegate", typeof(global::System.ValueTuple<TCollection, TKey, TValue>), null), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> _) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateImmutableDictionaryCreateRangeDelegate<TCollection, TKey, TValue>());
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0002CE32 File Offset: 0x0002B032
		[RequiresUnreferencedCode("System.Collections.Immutable converters use Reflection to find and create Immutable Collection types, which requires unreferenced code.")]
		public override Func<IEnumerable<TElement>, TCollection> CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>()
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<IEnumerable<TElement>, TCollection>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateImmutableEnumerableCreateRangeDelegate", typeof(global::System.ValueTuple<TCollection, TElement>), null), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> _) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateImmutableEnumerableCreateRangeDelegate<TCollection, TElement>());
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002CE72 File Offset: 0x0002B072
		public override Func<object[], T> CreateParameterizedConstructor<T>(ConstructorInfo constructor)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<object[], T>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateParameterizedConstructor", typeof(T), constructor), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateParameterizedConstructor<T>((ConstructorInfo)key.Item3));
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002CEB2 File Offset: 0x0002B0B2
		public override JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3> CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>(ConstructorInfo constructor)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<JsonTypeInfo.ParameterizedConstructorDelegate<T, TArg0, TArg1, TArg2, TArg3>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreateParameterizedConstructor", typeof(T), constructor), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreateParameterizedConstructor<T, TArg0, TArg1, TArg2, TArg3>((ConstructorInfo)key.Item3));
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002CEF2 File Offset: 0x0002B0F2
		public override Func<object, TProperty> CreatePropertyGetter<TProperty>(PropertyInfo propertyInfo)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Func<object, TProperty>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreatePropertyGetter", typeof(TProperty), propertyInfo), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreatePropertyGetter<TProperty>((PropertyInfo)key.Item3));
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002CF32 File Offset: 0x0002B132
		public override Action<object, TProperty> CreatePropertySetter<TProperty>(PropertyInfo propertyInfo)
		{
			return ReflectionEmitCachingMemberAccessor.s_cache.GetOrAdd<Action<object, TProperty>>(new global::System.ValueTuple<string, Type, MemberInfo>("CreatePropertySetter", typeof(TProperty), propertyInfo), ([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })] global::System.ValueTuple<string, Type, MemberInfo> key) => ReflectionEmitCachingMemberAccessor.s_sourceAccessor.CreatePropertySetter<TProperty>((PropertyInfo)key.Item3));
		}

		// Token: 0x040003EE RID: 1006
		private static readonly ReflectionEmitMemberAccessor s_sourceAccessor = new ReflectionEmitMemberAccessor();

		// Token: 0x040003EF RID: 1007
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "id", "declaringType", "member" })]
		private static readonly ReflectionEmitCachingMemberAccessor.Cache<global::System.ValueTuple<string, Type, MemberInfo>> s_cache = new ReflectionEmitCachingMemberAccessor.Cache<global::System.ValueTuple<string, Type, MemberInfo>>(TimeSpan.FromMilliseconds(1000.0), TimeSpan.FromMilliseconds(200.0));

		// Token: 0x0200014E RID: 334
		private sealed class Cache<TKey>
		{
			// Token: 0x06000E1A RID: 3610 RVA: 0x00036D64 File Offset: 0x00034F64
			public Cache(TimeSpan slidingExpiration, TimeSpan evictionInterval)
			{
				this._slidingExpirationTicks = slidingExpiration.Ticks;
				this._evictionIntervalTicks = evictionInterval.Ticks;
				this._lastEvictedTicks = DateTime.UtcNow.Ticks;
			}

			// Token: 0x06000E1B RID: 3611 RVA: 0x00036DB0 File Offset: 0x00034FB0
			public TValue GetOrAdd<TValue>(TKey key, Func<TKey, TValue> valueFactory) where TValue : class
			{
				ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry orAdd = this._cache.GetOrAdd(key, (TKey key) => new ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry(valueFactory(key)));
				long ticks = DateTime.UtcNow.Ticks;
				Volatile.Write(ref orAdd.LastUsedTicks, ticks);
				if (ticks - Volatile.Read(ref this._lastEvictedTicks) >= this._evictionIntervalTicks && Interlocked.CompareExchange(ref this._evictLock, 1, 0) == 0)
				{
					if (ticks - this._lastEvictedTicks >= this._evictionIntervalTicks)
					{
						this.EvictStaleCacheEntries(ticks);
						Volatile.Write(ref this._lastEvictedTicks, ticks);
					}
					Volatile.Write(ref this._evictLock, 0);
				}
				return (TValue)((object)orAdd.Value);
			}

			// Token: 0x06000E1C RID: 3612 RVA: 0x00036E5C File Offset: 0x0003505C
			public void Clear()
			{
				this._cache.Clear();
				this._lastEvictedTicks = DateTime.UtcNow.Ticks;
			}

			// Token: 0x06000E1D RID: 3613 RVA: 0x00036E88 File Offset: 0x00035088
			private void EvictStaleCacheEntries(long utcNowTicks)
			{
				foreach (KeyValuePair<TKey, ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry> keyValuePair in this._cache)
				{
					if (utcNowTicks - Volatile.Read(ref keyValuePair.Value.LastUsedTicks) >= this._slidingExpirationTicks)
					{
						ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry cacheEntry;
						this._cache.TryRemove(keyValuePair.Key, out cacheEntry);
					}
				}
			}

			// Token: 0x04000525 RID: 1317
			private int _evictLock;

			// Token: 0x04000526 RID: 1318
			private long _lastEvictedTicks;

			// Token: 0x04000527 RID: 1319
			private readonly long _evictionIntervalTicks;

			// Token: 0x04000528 RID: 1320
			private readonly long _slidingExpirationTicks;

			// Token: 0x04000529 RID: 1321
			private readonly ConcurrentDictionary<TKey, ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry> _cache = new ConcurrentDictionary<TKey, ReflectionEmitCachingMemberAccessor.Cache<TKey>.CacheEntry>();

			// Token: 0x02000179 RID: 377
			private sealed class CacheEntry
			{
				// Token: 0x06000E89 RID: 3721 RVA: 0x000379A2 File Offset: 0x00035BA2
				public CacheEntry(object value)
				{
					this.Value = value;
				}

				// Token: 0x04000572 RID: 1394
				public readonly object Value;

				// Token: 0x04000573 RID: 1395
				public long LastUsedTicks;
			}
		}
	}
}
