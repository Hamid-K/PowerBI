using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	internal abstract class JsonDictionaryConverter<TDictionary, TKey, TValue> : JsonDictionaryConverter<TDictionary>
	{
		// Token: 0x0600086B RID: 2155
		protected abstract void Add(TKey key, in TValue value, JsonSerializerOptions options, ref ReadStack state);

		// Token: 0x0600086C RID: 2156 RVA: 0x00025697 File Offset: 0x00023897
		protected virtual void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002569C File Offset: 0x0002389C
		protected virtual void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			JsonPropertyInfo parentProperty = state.ParentProperty;
			if (parentProperty != null && parentProperty.TryGetPrePopulatedValue(ref state))
			{
				return;
			}
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (jsonTypeInfo.CreateObject == null)
			{
				if (this.Type.IsAbstract || this.Type.IsInterface)
				{
					ThrowHelper.ThrowNotSupportedException_CannotPopulateCollection(this.Type, ref reader, ref state);
				}
				else
				{
					ThrowHelper.ThrowNotSupportedException_DeserializeNoConstructor(this.Type, ref reader, ref state);
				}
			}
			state.Current.ReturnValue = jsonTypeInfo.CreateObject();
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0002571F File Offset: 0x0002391F
		internal override Type ElementType
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002572B File Offset: 0x0002392B
		internal override Type KeyType
		{
			get
			{
				return typeof(TKey);
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00025737 File Offset: 0x00023937
		protected static JsonConverter<T> GetConverter<T>(JsonTypeInfo typeInfo)
		{
			return ((JsonTypeInfo<T>)typeInfo).EffectiveConverter;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00025744 File Offset: 0x00023944
		internal sealed override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, [MaybeNullWhen(false)] out TDictionary value)
		{
			JsonTypeInfo keyTypeInfo = state.Current.JsonTypeInfo.KeyTypeInfo;
			JsonTypeInfo elementTypeInfo = state.Current.JsonTypeInfo.ElementTypeInfo;
			if (state.SupportContinuation || state.Current.CanContainMetadata)
			{
				JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
				if (state.Current.ObjectState == StackFrameObjectState.None)
				{
					if (reader.TokenType != JsonTokenType.StartObject)
					{
						ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
					}
					state.Current.ObjectState = StackFrameObjectState.StartToken;
				}
				if (state.Current.CanContainMetadata && state.Current.ObjectState < StackFrameObjectState.ReadMetadata)
				{
					if (!JsonSerializer.TryReadMetadata(this, jsonTypeInfo, ref reader, ref state))
					{
						value = default(TDictionary);
						return false;
					}
					if (state.Current.MetadataPropertyNames == MetadataPropertyName.Ref)
					{
						value = JsonSerializer.ResolveReferenceId<TDictionary>(ref state);
						return true;
					}
					state.Current.ObjectState = StackFrameObjectState.ReadMetadata;
				}
				if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Type) != MetadataPropertyName.None && state.Current.PolymorphicSerializationState != PolymorphicSerializationState.PolymorphicReEntryStarted)
				{
					JsonConverter jsonConverter = base.ResolvePolymorphicConverter(jsonTypeInfo, ref state);
					if (jsonConverter != null)
					{
						object obj;
						bool flag = jsonConverter.OnTryReadAsObject(ref reader, jsonConverter.Type, options, ref state, out obj);
						value = (TDictionary)((object)obj);
						state.ExitPolymorphicConverter(flag);
						return flag;
					}
				}
				if (state.Current.ObjectState < StackFrameObjectState.CreatedObject)
				{
					if (state.Current.CanContainMetadata)
					{
						JsonSerializer.ValidateMetadataForObjectConverter(ref state);
					}
					this.CreateCollection(ref reader, ref state);
					if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Id) != MetadataPropertyName.None)
					{
						state.ReferenceResolver.AddReference(state.ReferenceId, state.Current.ReturnValue);
						state.ReferenceId = null;
					}
					state.Current.ObjectState = StackFrameObjectState.CreatedObject;
				}
				if (this._keyConverter == null)
				{
					this._keyConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TKey>(keyTypeInfo);
				}
				if (this._valueConverter == null)
				{
					this._valueConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TValue>(elementTypeInfo);
				}
				TKey tkey;
				for (;;)
				{
					if (state.Current.PropertyState == StackFramePropertyState.None)
					{
						state.Current.PropertyState = StackFramePropertyState.ReadName;
						if (!reader.Read())
						{
							break;
						}
					}
					if (state.Current.PropertyState < StackFramePropertyState.Name)
					{
						if (reader.TokenType == JsonTokenType.EndObject)
						{
							goto IL_04B9;
						}
						state.Current.PropertyState = StackFramePropertyState.Name;
						if (state.Current.CanContainMetadata)
						{
							ReadOnlySpan<byte> span = (ref reader).GetSpan();
							if (JsonSerializer.IsMetadataPropertyName(span, state.Current.BaseJsonTypeInfo.PolymorphicTypeResolver))
							{
								ThrowHelper.ThrowUnexpectedMetadataException(span, ref reader, ref state);
							}
						}
						state.Current.JsonPropertyInfo = keyTypeInfo.PropertyInfoForTypeInfo;
						tkey = JsonDictionaryConverter<TDictionary, TKey, TValue>.<OnTryRead>g__ReadDictionaryKey|10_0(this._keyConverter, ref reader, ref state, options);
					}
					else
					{
						tkey = (TKey)((object)state.Current.DictionaryKey);
					}
					if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
					{
						state.Current.PropertyState = StackFramePropertyState.ReadValue;
						if (!JsonConverter.SingleValueReadWithReadAhead(this._valueConverter.RequiresReadAhead, ref reader, ref state))
						{
							goto Block_30;
						}
					}
					if (state.Current.PropertyState < StackFramePropertyState.TryRead)
					{
						state.Current.JsonPropertyInfo = elementTypeInfo.PropertyInfoForTypeInfo;
						TValue tvalue;
						bool flag2;
						if (!this._valueConverter.TryRead(ref reader, typeof(TValue), options, ref state, out tvalue, out flag2))
						{
							goto Block_32;
						}
						this.Add(tkey, in tvalue, options, ref state);
						state.Current.EndElement();
					}
				}
				value = default(TDictionary);
				return false;
				Block_30:
				state.Current.DictionaryKey = tkey;
				value = default(TDictionary);
				return false;
				Block_32:
				state.Current.DictionaryKey = tkey;
				value = default(TDictionary);
				return false;
			}
			if (reader.TokenType != JsonTokenType.StartObject)
			{
				ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
			}
			this.CreateCollection(ref reader, ref state);
			if (this._keyConverter == null)
			{
				this._keyConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TKey>(keyTypeInfo);
			}
			if (this._valueConverter == null)
			{
				this._valueConverter = JsonDictionaryConverter<TDictionary, TKey, TValue>.GetConverter<TValue>(elementTypeInfo);
			}
			if (this._valueConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				for (;;)
				{
					(ref reader).ReadWithVerify();
					if (reader.TokenType == JsonTokenType.EndObject)
					{
						break;
					}
					state.Current.JsonPropertyInfo = keyTypeInfo.PropertyInfoForTypeInfo;
					TKey tkey2 = JsonDictionaryConverter<TDictionary, TKey, TValue>.<OnTryRead>g__ReadDictionaryKey|10_0(this._keyConverter, ref reader, ref state, options);
					(ref reader).ReadWithVerify();
					state.Current.JsonPropertyInfo = elementTypeInfo.PropertyInfoForTypeInfo;
					TValue tvalue2 = this._valueConverter.Read(ref reader, this.ElementType, options);
					this.Add(tkey2, in tvalue2, options, ref state);
				}
			}
			else
			{
				for (;;)
				{
					(ref reader).ReadWithVerify();
					if (reader.TokenType == JsonTokenType.EndObject)
					{
						break;
					}
					state.Current.JsonPropertyInfo = keyTypeInfo.PropertyInfoForTypeInfo;
					TKey tkey3 = JsonDictionaryConverter<TDictionary, TKey, TValue>.<OnTryRead>g__ReadDictionaryKey|10_0(this._keyConverter, ref reader, ref state, options);
					(ref reader).ReadWithVerify();
					state.Current.JsonPropertyInfo = elementTypeInfo.PropertyInfoForTypeInfo;
					bool flag2;
					TValue tvalue3;
					this._valueConverter.TryRead(ref reader, this.ElementType, options, ref state, out tvalue3, out flag2);
					this.Add(tkey3, in tvalue3, options, ref state);
				}
			}
			IL_04B9:
			this.ConvertCollection(ref state, options);
			value = (TDictionary)((object)state.Current.ReturnValue);
			return true;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00025C2C File Offset: 0x00023E2C
		internal sealed override bool OnTryWrite(Utf8JsonWriter writer, TDictionary dictionary, JsonSerializerOptions options, ref WriteStack state)
		{
			if (dictionary == null)
			{
				writer.WriteNullValue();
				return true;
			}
			if (!state.Current.ProcessedStartToken)
			{
				state.Current.ProcessedStartToken = true;
				writer.WriteStartObject();
				if (state.CurrentContainsMetadata && this.CanHaveMetadata)
				{
					JsonSerializer.WriteMetadataForObject(this, ref state, writer);
				}
				state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			}
			bool flag = this.OnWriteResume(writer, dictionary, options, ref state);
			if (flag && !state.Current.ProcessedEndToken)
			{
				state.Current.ProcessedEndToken = true;
				writer.WriteEndObject();
			}
			return flag;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025CE0 File Offset: 0x00023EE0
		[CompilerGenerated]
		internal static TKey <OnTryRead>g__ReadDictionaryKey|10_0(JsonConverter<TKey> keyConverter, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			string @string = reader.GetString();
			state.Current.JsonPropertyNameAsString = @string;
			TKey tkey;
			if (keyConverter.IsInternalConverter && keyConverter.Type == typeof(string))
			{
				tkey = (TKey)((object)@string);
			}
			else
			{
				tkey = keyConverter.ReadAsPropertyNameCore(ref reader, keyConverter.Type, options);
			}
			return tkey;
		}

		// Token: 0x040002EC RID: 748
		protected JsonConverter<TKey> _keyConverter;

		// Token: 0x040002ED RID: 749
		protected JsonConverter<TValue> _valueConverter;
	}
}
