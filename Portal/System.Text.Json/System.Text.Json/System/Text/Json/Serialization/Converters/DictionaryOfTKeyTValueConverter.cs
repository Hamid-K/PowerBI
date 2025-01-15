using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C8 RID: 200
	internal sealed class DictionaryOfTKeyTValueConverter<TCollection, TKey, TValue> : DictionaryDefaultConverter<TCollection, TKey, TValue> where TCollection : Dictionary<TKey, TValue>
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0002E3A9 File Offset: 0x0002C5A9
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002E3AC File Offset: 0x0002C5AC
		protected override void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue))[key] = value;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002E3D0 File Offset: 0x0002C5D0
		protected internal override bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			Dictionary<TKey, TValue>.Enumerator enumerator;
			if (state.Current.CollectionEnumerator == null)
			{
				enumerator = value.GetEnumerator();
				if (!enumerator.MoveNext())
				{
					enumerator.Dispose();
					return true;
				}
			}
			else
			{
				enumerator = (Dictionary<TKey, TValue>.Enumerator)state.Current.CollectionEnumerator;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (this._keyConverter == null)
			{
				this._keyConverter = JsonDictionaryConverter<TCollection, TKey, TValue>.GetConverter<TKey>(jsonTypeInfo.KeyTypeInfo);
			}
			if (this._valueConverter == null)
			{
				this._valueConverter = JsonDictionaryConverter<TCollection, TKey, TValue>.GetConverter<TValue>(jsonTypeInfo.ElementTypeInfo);
			}
			if (state.SupportContinuation || !this._valueConverter.CanUseDirectReadOrWrite || state.Current.NumberHandling != null)
			{
				while (!JsonConverter.ShouldFlush(writer, ref state))
				{
					KeyValuePair<TKey, TValue> keyValuePair;
					if (state.Current.PropertyState < StackFramePropertyState.Name)
					{
						state.Current.PropertyState = StackFramePropertyState.Name;
						keyValuePair = enumerator.Current;
						TKey key = keyValuePair.Key;
						this._keyConverter.WriteAsPropertyNameCore(writer, key, options, state.Current.IsWritingExtensionDataProperty);
					}
					keyValuePair = enumerator.Current;
					TValue value2 = keyValuePair.Value;
					if (!this._valueConverter.TryWrite(writer, in value2, options, ref state))
					{
						state.Current.CollectionEnumerator = enumerator;
						return false;
					}
					state.Current.EndDictionaryEntry();
					if (!enumerator.MoveNext())
					{
						goto IL_01B0;
					}
				}
				state.Current.CollectionEnumerator = enumerator;
				return false;
			}
			do
			{
				KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
				TKey key2 = keyValuePair.Key;
				this._keyConverter.WriteAsPropertyNameCore(writer, key2, options, state.Current.IsWritingExtensionDataProperty);
				JsonConverter<TValue> valueConverter = this._valueConverter;
				keyValuePair = enumerator.Current;
				valueConverter.Write(writer, keyValuePair.Value, options);
			}
			while (enumerator.MoveNext());
			IL_01B0:
			enumerator.Dispose();
			return true;
		}
	}
}
