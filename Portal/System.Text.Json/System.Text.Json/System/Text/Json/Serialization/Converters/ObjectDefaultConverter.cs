using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000EA RID: 234
	internal class ObjectDefaultConverter<T> : JsonObjectConverter<T>
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0003078B File Offset: 0x0002E98B
		internal override bool CanHaveMetadata
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0003078E File Offset: 0x0002E98E
		internal override bool SupportsCreateObjectDelegate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00030794 File Offset: 0x0002E994
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, [MaybeNullWhen(false)] out T value)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			object obj;
			if (!state.SupportContinuation && !state.Current.CanContainMetadata)
			{
				if (reader.TokenType != JsonTokenType.StartObject)
				{
					ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.Type);
				}
				JsonPropertyInfo parentProperty = state.ParentProperty;
				if (parentProperty != null && parentProperty.TryGetPrePopulatedValue(ref state))
				{
					obj = state.Current.ReturnValue;
				}
				else
				{
					if (jsonTypeInfo.CreateObject == null)
					{
						ThrowHelper.ThrowNotSupportedException_DeserializeNoConstructor(jsonTypeInfo.Type, ref reader, ref state);
					}
					obj = jsonTypeInfo.CreateObject();
				}
				ObjectDefaultConverter<T>.PopulatePropertiesFastPath(obj, jsonTypeInfo, options, ref reader, ref state);
				value = (T)((object)obj);
				return true;
			}
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
					value = default(T);
					return false;
				}
				if (state.Current.MetadataPropertyNames == MetadataPropertyName.Ref)
				{
					value = JsonSerializer.ResolveReferenceId<T>(ref state);
					return true;
				}
				state.Current.ObjectState = StackFrameObjectState.ReadMetadata;
			}
			if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Type) != MetadataPropertyName.None && state.Current.PolymorphicSerializationState != PolymorphicSerializationState.PolymorphicReEntryStarted)
			{
				JsonConverter jsonConverter = base.ResolvePolymorphicConverter(jsonTypeInfo, ref state);
				if (jsonConverter != null)
				{
					object obj2;
					bool flag = jsonConverter.OnTryReadAsObject(ref reader, jsonConverter.Type, options, ref state, out obj2);
					value = (T)((object)obj2);
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
				if (state.Current.MetadataPropertyNames == MetadataPropertyName.Ref)
				{
					value = JsonSerializer.ResolveReferenceId<T>(ref state);
					return true;
				}
				JsonPropertyInfo parentProperty2 = state.ParentProperty;
				if (parentProperty2 != null && parentProperty2.TryGetPrePopulatedValue(ref state))
				{
					obj = state.Current.ReturnValue;
				}
				else
				{
					if (jsonTypeInfo.CreateObject == null)
					{
						ThrowHelper.ThrowNotSupportedException_DeserializeNoConstructor(jsonTypeInfo.Type, ref reader, ref state);
					}
					obj = jsonTypeInfo.CreateObject();
				}
				if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Id) != MetadataPropertyName.None)
				{
					state.ReferenceResolver.AddReference(state.ReferenceId, obj);
					state.ReferenceId = null;
				}
				Action<object> onDeserializing = jsonTypeInfo.OnDeserializing;
				if (onDeserializing != null)
				{
					onDeserializing(obj);
				}
				state.Current.ReturnValue = obj;
				state.Current.ObjectState = StackFrameObjectState.CreatedObject;
				state.Current.InitializeRequiredPropertiesValidationState(jsonTypeInfo);
			}
			else
			{
				obj = state.Current.ReturnValue;
			}
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
				JsonPropertyInfo jsonPropertyInfo;
				if (state.Current.PropertyState < StackFramePropertyState.Name)
				{
					state.Current.PropertyState = StackFramePropertyState.Name;
					JsonTokenType tokenType = reader.TokenType;
					if (tokenType == JsonTokenType.EndObject)
					{
						goto IL_0406;
					}
					ReadOnlySpan<byte> propertyName = JsonSerializer.GetPropertyName(ref state, ref reader);
					bool flag2;
					jsonPropertyInfo = JsonSerializer.LookupProperty(obj, propertyName, ref state, options, out flag2, true);
					state.Current.UseExtensionProperty = flag2;
				}
				else
				{
					jsonPropertyInfo = state.Current.JsonPropertyInfo;
				}
				if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
				{
					if (!jsonPropertyInfo.CanDeserializeOrPopulate)
					{
						if (!reader.TrySkip())
						{
							goto Block_30;
						}
						state.Current.EndProperty();
						continue;
					}
					else if (!ObjectDefaultConverter<T>.ReadAheadPropertyValue(ref state, ref reader, jsonPropertyInfo))
					{
						goto Block_31;
					}
				}
				if (state.Current.PropertyState < StackFramePropertyState.TryRead)
				{
					if (!state.Current.UseExtensionProperty)
					{
						if (!jsonPropertyInfo.ReadJsonAndSetMember(obj, ref state, ref reader))
						{
							goto Block_34;
						}
					}
					else if (!jsonPropertyInfo.ReadJsonAndAddExtensionProperty(obj, ref state, ref reader))
					{
						goto Block_35;
					}
					state.Current.EndProperty();
				}
			}
			state.Current.ReturnValue = obj;
			value = default(T);
			return false;
			Block_30:
			state.Current.ReturnValue = obj;
			value = default(T);
			return false;
			Block_31:
			state.Current.ReturnValue = obj;
			value = default(T);
			return false;
			Block_34:
			state.Current.ReturnValue = obj;
			value = default(T);
			return false;
			Block_35:
			state.Current.ReturnValue = obj;
			value = default(T);
			return false;
			IL_0406:
			Action<object> onDeserialized = jsonTypeInfo.OnDeserialized;
			if (onDeserialized != null)
			{
				onDeserialized(obj);
			}
			state.Current.ValidateAllRequiredPropertiesAreRead(jsonTypeInfo);
			value = (T)((object)obj);
			if (state.Current.PropertyRefCache != null)
			{
				jsonTypeInfo.UpdateSortedPropertyCache(ref state.Current);
			}
			return true;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void PopulatePropertiesFastPath(object obj, JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options, ref Utf8JsonReader reader, [ScopedRef] ref ReadStack state)
		{
			Action<object> onDeserializing = jsonTypeInfo.OnDeserializing;
			if (onDeserializing != null)
			{
				onDeserializing(obj);
			}
			state.Current.InitializeRequiredPropertiesValidationState(jsonTypeInfo);
			for (;;)
			{
				(ref reader).ReadWithVerify();
				JsonTokenType tokenType = reader.TokenType;
				if (tokenType == JsonTokenType.EndObject)
				{
					break;
				}
				ReadOnlySpan<byte> propertyName = JsonSerializer.GetPropertyName(ref state, ref reader);
				bool flag;
				JsonPropertyInfo jsonPropertyInfo = JsonSerializer.LookupProperty(obj, propertyName, ref state, options, out flag, true);
				ObjectDefaultConverter<T>.ReadPropertyValue(obj, ref state, ref reader, jsonPropertyInfo, flag);
			}
			Action<object> onDeserialized = jsonTypeInfo.OnDeserialized;
			if (onDeserialized != null)
			{
				onDeserialized(obj);
			}
			state.Current.ValidateAllRequiredPropertiesAreRead(jsonTypeInfo);
			if (state.Current.PropertyRefCache != null)
			{
				jsonTypeInfo.UpdateSortedPropertyCache(ref state.Current);
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00030C8C File Offset: 0x0002EE8C
		internal sealed override bool OnTryWrite(Utf8JsonWriter writer, T value, JsonSerializerOptions options, ref WriteStack state)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			jsonTypeInfo.ValidateCanBeUsedForPropertyMetadataSerialization();
			object obj = value;
			if (!state.SupportContinuation)
			{
				writer.WriteStartObject();
				if (state.CurrentContainsMetadata && this.CanHaveMetadata)
				{
					JsonSerializer.WriteMetadataForObject(this, ref state, writer);
				}
				Action<object> onSerializing = jsonTypeInfo.OnSerializing;
				if (onSerializing != null)
				{
					onSerializing(obj);
				}
				List<KeyValuePair<string, JsonPropertyInfo>> list = jsonTypeInfo.PropertyCache.List;
				for (int i = 0; i < list.Count; i++)
				{
					JsonPropertyInfo value2 = list[i].Value;
					if (value2.CanSerialize)
					{
						state.Current.JsonPropertyInfo = value2;
						state.Current.NumberHandling = value2.EffectiveNumberHandling;
						bool memberAndWriteJson = value2.GetMemberAndWriteJson(obj, ref state, writer);
						state.Current.EndProperty();
					}
				}
				JsonPropertyInfo extensionDataProperty = jsonTypeInfo.ExtensionDataProperty;
				if (extensionDataProperty != null && extensionDataProperty.CanSerialize)
				{
					state.Current.JsonPropertyInfo = extensionDataProperty;
					state.Current.NumberHandling = extensionDataProperty.EffectiveNumberHandling;
					bool memberAndWriteJsonExtensionData = extensionDataProperty.GetMemberAndWriteJsonExtensionData(obj, ref state, writer);
					state.Current.EndProperty();
				}
				writer.WriteEndObject();
			}
			else
			{
				if (!state.Current.ProcessedStartToken)
				{
					writer.WriteStartObject();
					if (state.CurrentContainsMetadata && this.CanHaveMetadata)
					{
						JsonSerializer.WriteMetadataForObject(this, ref state, writer);
					}
					Action<object> onSerializing2 = jsonTypeInfo.OnSerializing;
					if (onSerializing2 != null)
					{
						onSerializing2(obj);
					}
					state.Current.ProcessedStartToken = true;
				}
				List<KeyValuePair<string, JsonPropertyInfo>> list2 = jsonTypeInfo.PropertyCache.List;
				while (state.Current.EnumeratorIndex < list2.Count)
				{
					JsonPropertyInfo value3 = list2[state.Current.EnumeratorIndex].Value;
					if (value3.CanSerialize)
					{
						state.Current.JsonPropertyInfo = value3;
						state.Current.NumberHandling = value3.EffectiveNumberHandling;
						if (!value3.GetMemberAndWriteJson(obj, ref state, writer))
						{
							return false;
						}
						state.Current.EndProperty();
						state.Current.EnumeratorIndex = state.Current.EnumeratorIndex + 1;
						if (JsonConverter.ShouldFlush(writer, ref state))
						{
							return false;
						}
					}
					else
					{
						state.Current.EnumeratorIndex = state.Current.EnumeratorIndex + 1;
					}
				}
				if (state.Current.EnumeratorIndex == list2.Count)
				{
					JsonPropertyInfo extensionDataProperty2 = jsonTypeInfo.ExtensionDataProperty;
					if (extensionDataProperty2 != null && extensionDataProperty2.CanSerialize)
					{
						state.Current.JsonPropertyInfo = extensionDataProperty2;
						state.Current.NumberHandling = extensionDataProperty2.EffectiveNumberHandling;
						if (!extensionDataProperty2.GetMemberAndWriteJsonExtensionData(obj, ref state, writer))
						{
							return false;
						}
						state.Current.EndProperty();
						state.Current.EnumeratorIndex = state.Current.EnumeratorIndex + 1;
						if (JsonConverter.ShouldFlush(writer, ref state))
						{
							return false;
						}
					}
					else
					{
						state.Current.EnumeratorIndex = state.Current.EnumeratorIndex + 1;
					}
				}
				if (!state.Current.ProcessedEndToken)
				{
					state.Current.ProcessedEndToken = true;
					writer.WriteEndObject();
				}
			}
			Action<object> onSerialized = jsonTypeInfo.OnSerialized;
			if (onSerialized != null)
			{
				onSerialized(obj);
			}
			return true;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00030F90 File Offset: 0x0002F190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static void ReadPropertyValue(object obj, [ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonPropertyInfo jsonPropertyInfo, bool useExtensionProperty)
		{
			if (!jsonPropertyInfo.CanDeserializeOrPopulate)
			{
				bool flag = reader.TrySkip();
			}
			else
			{
				(ref reader).ReadWithVerify();
				if (!useExtensionProperty)
				{
					jsonPropertyInfo.ReadJsonAndSetMember(obj, ref state, ref reader);
				}
				else
				{
					jsonPropertyInfo.ReadJsonAndAddExtensionProperty(obj, ref state, ref reader);
				}
			}
			state.Current.EndProperty();
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00030FD9 File Offset: 0x0002F1D9
		protected static bool ReadAheadPropertyValue([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonPropertyInfo jsonPropertyInfo)
		{
			state.Current.PropertyState = StackFramePropertyState.ReadValue;
			if (!state.Current.UseExtensionProperty)
			{
				if (!JsonConverter.SingleValueReadWithReadAhead(jsonPropertyInfo.EffectiveConverter.RequiresReadAhead, ref reader, ref state))
				{
					return false;
				}
			}
			else if (!JsonConverter.SingleValueReadWithReadAhead(true, ref reader, ref state))
			{
				return false;
			}
			return true;
		}
	}
}
