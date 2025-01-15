using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D6 RID: 214
	internal sealed class IReadOnlyDictionaryOfTKeyTValueConverter<TDictionary, TKey, TValue> : DictionaryDefaultConverter<TDictionary, TKey, TValue> where TDictionary : IReadOnlyDictionary<TKey, TValue>
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0002F71B File Offset: 0x0002D91B
		protected override void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state)
		{
			((Dictionary<TKey, TValue>)state.Current.ReturnValue)[key] = value;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002F73A File Offset: 0x0002D93A
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002F73D File Offset: 0x0002D93D
		protected override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			if (!this._isDeserializable)
			{
				ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
			}
			state.Current.ReturnValue = new Dictionary<TKey, TValue>();
		}

		// Token: 0x040003F9 RID: 1017
		private readonly bool _isDeserializable = typeof(TDictionary).IsAssignableFrom(typeof(Dictionary<TKey, TValue>));
	}
}
