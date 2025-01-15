using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000D4 RID: 212
	internal class ImmutableDictionaryOfTKeyTValueConverter<TDictionary, TKey, TValue> : DictionaryDefaultConverter<TDictionary, TKey, TValue> where TDictionary : IReadOnlyDictionary<TKey, TValue>
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002F609 File Offset: 0x0002D809
		protected sealed override void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state)
		{
			((Dictionary<TKey, TValue>)state.Current.ReturnValue)[key] = value;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002F628 File Offset: 0x0002D828
		internal sealed override bool CanHaveMetadata
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002F62B File Offset: 0x0002D82B
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002F62E File Offset: 0x0002D82E
		protected sealed override void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			state.Current.ReturnValue = new Dictionary<TKey, TValue>();
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002F640 File Offset: 0x0002D840
		protected sealed override void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
			Func<IEnumerable<KeyValuePair<TKey, TValue>>, TDictionary> func = (Func<IEnumerable<KeyValuePair<TKey, TValue>>, TDictionary>)state.Current.JsonTypeInfo.CreateObjectWithArgs;
			state.Current.ReturnValue = func((Dictionary<TKey, TValue>)state.Current.ReturnValue);
		}
	}
}
