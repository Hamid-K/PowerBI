using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000089 RID: 137
	internal abstract class JsonCollectionConverter<TCollection, TElement> : JsonResumableConverter<TCollection>
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000250E5 File Offset: 0x000232E5
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000250E8 File Offset: 0x000232E8
		private protected sealed override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.Enumerable;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000250EB File Offset: 0x000232EB
		internal override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x0600085E RID: 2142
		protected abstract void Add(in TElement value, ref ReadStack state);

		// Token: 0x0600085F RID: 2143 RVA: 0x000250F8 File Offset: 0x000232F8
		protected virtual void CreateCollection(ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state, JsonSerializerOptions options)
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

		// Token: 0x06000860 RID: 2144 RVA: 0x0002517B File Offset: 0x0002337B
		protected virtual void ConvertCollection(ref ReadStack state, JsonSerializerOptions options)
		{
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002517D File Offset: 0x0002337D
		protected static JsonConverter<TElement> GetElementConverter(JsonTypeInfo elementTypeInfo)
		{
			return ((JsonTypeInfo<TElement>)elementTypeInfo).EffectiveConverter;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002518A File Offset: 0x0002338A
		protected static JsonConverter<TElement> GetElementConverter(ref WriteStack state)
		{
			return (JsonConverter<TElement>)state.Current.JsonPropertyInfo.EffectiveConverter;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000251A4 File Offset: 0x000233A4
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, [MaybeNullWhen(false)] out TCollection value)
		{
			JsonTypeInfo elementTypeInfo = state.Current.JsonTypeInfo.ElementTypeInfo;
			if (!state.SupportContinuation && !state.Current.CanContainMetadata)
			{
				if (reader.TokenType != JsonTokenType.StartArray)
				{
					ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
				}
				this.CreateCollection(ref reader, ref state, options);
				state.Current.JsonPropertyInfo = elementTypeInfo.PropertyInfoForTypeInfo;
				JsonConverter<TElement> elementConverter = JsonCollectionConverter<TCollection, TElement>.GetElementConverter(elementTypeInfo);
				if (elementConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
				{
					for (;;)
					{
						(ref reader).ReadWithVerify();
						if (reader.TokenType == JsonTokenType.EndArray)
						{
							break;
						}
						TElement telement = elementConverter.Read(ref reader, elementConverter.Type, options);
						this.Add(in telement, ref state);
					}
				}
				else
				{
					for (;;)
					{
						(ref reader).ReadWithVerify();
						if (reader.TokenType == JsonTokenType.EndArray)
						{
							break;
						}
						TElement telement2;
						bool flag;
						elementConverter.TryRead(ref reader, typeof(TElement), options, ref state, out telement2, out flag);
						this.Add(in telement2, ref state);
					}
				}
			}
			else
			{
				JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
				if (state.Current.ObjectState == StackFrameObjectState.None)
				{
					if (reader.TokenType == JsonTokenType.StartArray)
					{
						state.Current.ObjectState = StackFrameObjectState.ReadMetadata;
					}
					else if (state.Current.CanContainMetadata)
					{
						if (reader.TokenType != JsonTokenType.StartObject)
						{
							ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
						}
						state.Current.ObjectState = StackFrameObjectState.StartToken;
					}
					else
					{
						ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
					}
				}
				if (state.Current.CanContainMetadata && state.Current.ObjectState < StackFrameObjectState.ReadMetadata)
				{
					if (!JsonSerializer.TryReadMetadata(this, jsonTypeInfo, ref reader, ref state))
					{
						value = default(TCollection);
						return false;
					}
					if (state.Current.MetadataPropertyNames == MetadataPropertyName.Ref)
					{
						value = JsonSerializer.ResolveReferenceId<TCollection>(ref state);
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
						bool flag2 = jsonConverter.OnTryReadAsObject(ref reader, jsonConverter.Type, options, ref state, out obj);
						value = (TCollection)((object)obj);
						state.ExitPolymorphicConverter(flag2);
						return flag2;
					}
				}
				if (state.Current.ObjectState < StackFrameObjectState.CreatedObject)
				{
					if (state.Current.CanContainMetadata)
					{
						JsonSerializer.ValidateMetadataForArrayConverter(this, ref reader, ref state);
					}
					this.CreateCollection(ref reader, ref state, options);
					if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Id) != MetadataPropertyName.None)
					{
						state.ReferenceResolver.AddReference(state.ReferenceId, state.Current.ReturnValue);
						state.ReferenceId = null;
					}
					state.Current.ObjectState = StackFrameObjectState.CreatedObject;
				}
				if (state.Current.ObjectState < StackFrameObjectState.ReadElements)
				{
					JsonConverter<TElement> elementConverter2 = JsonCollectionConverter<TCollection, TElement>.GetElementConverter(elementTypeInfo);
					state.Current.JsonPropertyInfo = elementTypeInfo.PropertyInfoForTypeInfo;
					for (;;)
					{
						if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
						{
							state.Current.PropertyState = StackFramePropertyState.ReadValue;
							if (!JsonConverter.SingleValueReadWithReadAhead(elementConverter2.RequiresReadAhead, ref reader, ref state))
							{
								break;
							}
						}
						if (state.Current.PropertyState < StackFramePropertyState.ReadValueIsEnd)
						{
							if (reader.TokenType == JsonTokenType.EndArray)
							{
								goto IL_035F;
							}
							state.Current.PropertyState = StackFramePropertyState.ReadValueIsEnd;
						}
						if (state.Current.PropertyState < StackFramePropertyState.TryRead)
						{
							bool flag;
							TElement telement3;
							if (!elementConverter2.TryRead(ref reader, typeof(TElement), options, ref state, out telement3, out flag))
							{
								goto Block_27;
							}
							this.Add(in telement3, ref state);
							state.Current.EndElement();
						}
					}
					value = default(TCollection);
					return false;
					Block_27:
					value = default(TCollection);
					return false;
					IL_035F:
					state.Current.ObjectState = StackFrameObjectState.ReadElements;
				}
				if (state.Current.ObjectState < StackFrameObjectState.EndToken)
				{
					state.Current.ObjectState = StackFrameObjectState.EndToken;
					if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Values) != MetadataPropertyName.None && !reader.Read())
					{
						value = default(TCollection);
						return false;
					}
				}
				if (state.Current.ObjectState < StackFrameObjectState.EndTokenValidation && (state.Current.MetadataPropertyNames & MetadataPropertyName.Values) != MetadataPropertyName.None && reader.TokenType != JsonTokenType.EndObject)
				{
					ThrowHelper.ThrowJsonException_MetadataInvalidPropertyInArrayMetadata(ref state, typeToConvert, in reader);
				}
			}
			this.ConvertCollection(ref state, options);
			value = (TCollection)((object)state.Current.ReturnValue);
			return true;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000255B0 File Offset: 0x000237B0
		internal override bool OnTryWrite(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state)
		{
			bool flag;
			if (value == null)
			{
				writer.WriteNullValue();
				flag = true;
			}
			else
			{
				if (!state.Current.ProcessedStartToken)
				{
					state.Current.ProcessedStartToken = true;
					if (state.CurrentContainsMetadata && this.CanHaveMetadata)
					{
						state.Current.MetadataPropertyName = JsonSerializer.WriteMetadataForCollection(this, ref state, writer);
					}
					writer.WriteStartArray();
					state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
				}
				flag = this.OnWriteResume(writer, value, options, ref state);
				if (flag && !state.Current.ProcessedEndToken)
				{
					state.Current.ProcessedEndToken = true;
					writer.WriteEndArray();
					if (state.Current.MetadataPropertyName != MetadataPropertyName.None)
					{
						writer.WriteEndObject();
					}
				}
			}
			return flag;
		}

		// Token: 0x06000865 RID: 2149
		protected abstract bool OnWriteResume(Utf8JsonWriter writer, TCollection value, JsonSerializerOptions options, ref WriteStack state);
	}
}
