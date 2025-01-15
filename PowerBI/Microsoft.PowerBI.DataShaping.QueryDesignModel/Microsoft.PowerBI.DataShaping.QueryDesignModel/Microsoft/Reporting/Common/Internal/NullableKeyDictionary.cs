using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000288 RID: 648
	internal sealed class NullableKeyDictionary<TKey, TValue> where TKey : class
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x0004D760 File Offset: 0x0004B960
		internal NullableKeyDictionary(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, int capacity, IEqualityComparer<TKey> comparer)
		{
			this._dictionary = new Dictionary<object, TValue>(capacity, new NullableKeyDictionary<TKey, TValue>.DenormalizingEqualityComparer(comparer));
			foreach (TValue tvalue in source)
			{
				object obj = NullableKeyDictionary<TKey, TValue>.ToSafeKey(keySelector(tvalue));
				this._dictionary.Add(obj, tvalue);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0004D7D4 File Offset: 0x0004B9D4
		public ICollection<TValue> Values
		{
			get
			{
				return this._dictionary.Values;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0004D7E1 File Offset: 0x0004B9E1
		public bool Remove(TKey key)
		{
			return this._dictionary.Remove(NullableKeyDictionary<TKey, TValue>.ToSafeKey(key));
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0004D7F4 File Offset: 0x0004B9F4
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this._dictionary.TryGetValue(NullableKeyDictionary<TKey, TValue>.ToSafeKey(key), out value);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0004D808 File Offset: 0x0004BA08
		private static object ToSafeKey(TKey key)
		{
			return key ?? NullableKeyDictionary<TKey, TValue>.NullKey;
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0004D81C File Offset: 0x0004BA1C
		private static TKey FromSafeKey(object safeKey)
		{
			if (safeKey == NullableKeyDictionary<TKey, TValue>.NullKey)
			{
				return default(TKey);
			}
			return ArgumentValidation.CheckAs<TKey>(safeKey, "safeKey");
		}

		// Token: 0x04000F2C RID: 3884
		private static readonly object NullKey = new object();

		// Token: 0x04000F2D RID: 3885
		private readonly Dictionary<object, TValue> _dictionary;

		// Token: 0x02000418 RID: 1048
		private sealed class DenormalizingEqualityComparer : EqualityComparer<object>
		{
			// Token: 0x060021B1 RID: 8625 RVA: 0x0005AA82 File Offset: 0x00058C82
			internal DenormalizingEqualityComparer(IEqualityComparer<TKey> comparer)
			{
				this._comparer = ArgumentValidation.CheckNotNull<IEqualityComparer<TKey>>(comparer, "comparer");
			}

			// Token: 0x060021B2 RID: 8626 RVA: 0x0005AA9B File Offset: 0x00058C9B
			public override bool Equals(object x, object y)
			{
				return this._comparer.Equals(NullableKeyDictionary<TKey, TValue>.FromSafeKey(x), NullableKeyDictionary<TKey, TValue>.FromSafeKey(y));
			}

			// Token: 0x060021B3 RID: 8627 RVA: 0x0005AAB4 File Offset: 0x00058CB4
			public override int GetHashCode(object obj)
			{
				return this._comparer.GetHashCode(NullableKeyDictionary<TKey, TValue>.FromSafeKey(obj));
			}

			// Token: 0x04001484 RID: 5252
			private readonly IEqualityComparer<TKey> _comparer;
		}
	}
}
