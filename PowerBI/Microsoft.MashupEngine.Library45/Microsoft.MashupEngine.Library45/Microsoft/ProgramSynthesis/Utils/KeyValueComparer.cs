using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004A4 RID: 1188
	public sealed class KeyValueComparer<TKey, TValue> : IEqualityComparer, IEqualityComparer<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06001AB1 RID: 6833 RVA: 0x000504E4 File Offset: 0x0004E6E4
		private KeyValueComparer(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
		{
			this._keyComparer = keyComparer;
			this._valueComparer = valueComparer;
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000504FA File Offset: 0x0004E6FA
		public static KeyValueComparer<TKey, TValue> ValueEqualityInstance { get; } = new KeyValueComparer<TKey, TValue>(ValueEquality<TKey>.Instance, ValueEquality<TValue>.Instance);

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00050501 File Offset: 0x0004E701
		public static KeyValueComparer<TKey, TValue> DefaultEqualityInstance { get; } = new KeyValueComparer<TKey, TValue>(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00050508 File Offset: 0x0004E708
		bool IEqualityComparer.Equals(object x, object y)
		{
			return x is KeyValuePair<TKey, TValue> && y is KeyValuePair<TKey, TValue> && this.Equals((KeyValuePair<TKey, TValue>)x, (KeyValuePair<TKey, TValue>)y);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0005052E File Offset: 0x0004E72E
		public int GetHashCode(object obj)
		{
			if (!(obj is KeyValuePair<TKey, TValue>))
			{
				return obj.GetHashCode();
			}
			return this.GetHashCode((KeyValuePair<TKey, TValue>)obj);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0005054C File Offset: 0x0004E74C
		public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			return ValueEquality.Comparer.Equals(x.Key, y.Key) && ValueEquality.Comparer.Equals(x.Value, y.Value);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000505A4 File Offset: 0x0004E7A4
		public int GetHashCode(KeyValuePair<TKey, TValue> obj)
		{
			return ((obj.Key == null) ? 0 : ValueEquality.Comparer.GetHashCode(obj.Key)) ^ (14533969 * ((obj.Value == null) ? 0 : ValueEquality.Comparer.GetHashCode(obj.Value)));
		}

		// Token: 0x04000D20 RID: 3360
		private IEqualityComparer<TKey> _keyComparer;

		// Token: 0x04000D21 RID: 3361
		private IEqualityComparer<TValue> _valueComparer;
	}
}
