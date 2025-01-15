using System;
using System.Collections.Generic;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C7 RID: 199
	internal abstract class DictionaryDefaultConverter<TDictionary, TKey, TValue> : JsonDictionaryConverter<TDictionary, TKey, TValue> where TDictionary : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002E264 File Offset: 0x0002C464
		internal override bool CanHaveMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E268 File Offset: 0x0002C468
		protected internal override bool OnWriteResume(Utf8JsonWriter writer, TDictionary value, JsonSerializerOptions options, ref WriteStack state)
		{
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator;
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
				enumerator = (IEnumerator<KeyValuePair<TKey, TValue>>)state.Current.CollectionEnumerator;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (this._keyConverter == null)
			{
				this._keyConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TKey>(jsonTypeInfo.KeyTypeInfo);
			}
			if (this._valueConverter == null)
			{
				this._valueConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TValue>(jsonTypeInfo.ElementTypeInfo);
			}
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
					enumerator.Dispose();
					return true;
				}
			}
			state.Current.CollectionEnumerator = enumerator;
			return false;
		}
	}
}
