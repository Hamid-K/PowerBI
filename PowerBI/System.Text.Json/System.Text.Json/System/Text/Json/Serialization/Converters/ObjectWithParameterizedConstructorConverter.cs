using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000EB RID: 235
	internal abstract class ObjectWithParameterizedConstructorConverter<T> : ObjectDefaultConverter<T>
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0003101F File Offset: 0x0002F21F
		internal sealed override bool ConstructorIsParameterized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00031024 File Offset: 0x0002F224
		internal sealed override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, [MaybeNullWhen(false)] out T value)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			if (!jsonTypeInfo.UsesParameterizedConstructor || state.Current.IsPopulating)
			{
				return base.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
			}
			ArgumentState ctorArgumentState = state.Current.CtorArgumentState;
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
					object returnValue = state.Current.ReturnValue;
					ObjectDefaultConverter<T>.PopulatePropertiesFastPath(returnValue, jsonTypeInfo, options, ref reader, ref state);
					value = (T)((object)returnValue);
					return true;
				}
				ReadOnlySpan<byte> originalSpan = reader.OriginalSpan;
				ReadOnlySequence<byte> originalSequence = reader.OriginalSequence;
				this.ReadConstructorArguments(ref state, ref reader, options);
				obj = (T)((object)this.CreateObject(ref state.Current));
				Action<object> onDeserializing = jsonTypeInfo.OnDeserializing;
				if (onDeserializing != null)
				{
					onDeserializing(obj);
				}
				if (ctorArgumentState.FoundPropertyCount > 0)
				{
					global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] foundProperties = ctorArgumentState.FoundProperties;
					for (int i = 0; i < ctorArgumentState.FoundPropertyCount; i++)
					{
						JsonPropertyInfo item = foundProperties[i].Item1;
						long item2 = foundProperties[i].Item3;
						byte[] item3 = foundProperties[i].Item4;
						string item4 = foundProperties[i].Item5;
						Utf8JsonReader utf8JsonReader = (originalSequence.IsEmpty ? new Utf8JsonReader(originalSpan.Slice(checked((int)item2)), true, foundProperties[i].Item2) : new Utf8JsonReader(originalSequence.Slice(item2), true, foundProperties[i].Item2));
						state.Current.JsonPropertyName = item3;
						state.Current.JsonPropertyInfo = item;
						state.Current.NumberHandling = item.EffectiveNumberHandling;
						bool flag = item4 != null;
						if (flag)
						{
							state.Current.JsonPropertyNameAsString = item4;
							JsonSerializer.CreateExtensionDataProperty(obj, item, options);
						}
						ObjectDefaultConverter<T>.ReadPropertyValue(obj, ref state, ref utf8JsonReader, item, flag);
					}
					global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] foundProperties2 = ctorArgumentState.FoundProperties;
					ctorArgumentState.FoundProperties = null;
					ArrayPool<global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>>.Shared.Return(foundProperties2, true);
				}
			}
			else
			{
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
						bool flag2 = jsonConverter.OnTryReadAsObject(ref reader, jsonConverter.Type, options, ref state, out obj2);
						value = (T)((object)obj2);
						state.ExitPolymorphicConverter(flag2);
						return flag2;
					}
				}
				JsonPropertyInfo parentProperty2 = state.ParentProperty;
				if (parentProperty2 != null && parentProperty2.TryGetPrePopulatedValue(ref state))
				{
					object returnValue2 = state.Current.ReturnValue;
					Action<object> onDeserializing2 = jsonTypeInfo.OnDeserializing;
					if (onDeserializing2 != null)
					{
						onDeserializing2(returnValue2);
					}
					state.Current.ObjectState = StackFrameObjectState.CreatedObject;
					state.Current.InitializeRequiredPropertiesValidationState(jsonTypeInfo);
					return base.OnTryRead(ref reader, typeToConvert, options, ref state, out value);
				}
				if (state.Current.ObjectState < StackFrameObjectState.ConstructorArguments)
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
					this.BeginRead(ref state, options);
					state.Current.ObjectState = StackFrameObjectState.ConstructorArguments;
				}
				if (!this.ReadConstructorArgumentsWithContinuation(ref state, ref reader, options))
				{
					value = default(T);
					return false;
				}
				obj = (T)((object)this.CreateObject(ref state.Current));
				if ((state.Current.MetadataPropertyNames & MetadataPropertyName.Id) != MetadataPropertyName.None)
				{
					state.ReferenceResolver.AddReference(state.ReferenceId, obj);
					state.ReferenceId = null;
				}
				Action<object> onDeserializing3 = jsonTypeInfo.OnDeserializing;
				if (onDeserializing3 != null)
				{
					onDeserializing3(obj);
				}
				if (ctorArgumentState.FoundPropertyCount > 0)
				{
					for (int j = 0; j < ctorArgumentState.FoundPropertyCount; j++)
					{
						JsonPropertyInfo item5 = ctorArgumentState.FoundPropertiesAsync[j].Item1;
						object item6 = ctorArgumentState.FoundPropertiesAsync[j].Item2;
						string item7 = ctorArgumentState.FoundPropertiesAsync[j].Item3;
						if (item7 == null)
						{
							if (item6 != null || !item5.IgnoreNullTokensOnRead || default(T) != null)
							{
								item5.Set(obj, item6);
								state.Current.MarkRequiredPropertyAsRead(item5);
							}
						}
						else
						{
							JsonSerializer.CreateExtensionDataProperty(obj, item5, options);
							object valueAsObject = item5.GetValueAsObject(obj);
							IDictionary<string, JsonElement> dictionary = valueAsObject as IDictionary<string, JsonElement>;
							if (dictionary != null)
							{
								dictionary[item7] = (JsonElement)item6;
							}
							else
							{
								((IDictionary<string, object>)valueAsObject)[item7] = item6;
							}
						}
					}
					global::System.ValueTuple<JsonPropertyInfo, object, string>[] foundPropertiesAsync = ctorArgumentState.FoundPropertiesAsync;
					ctorArgumentState.FoundPropertiesAsync = null;
					ArrayPool<global::System.ValueTuple<JsonPropertyInfo, object, string>>.Shared.Return(foundPropertiesAsync, true);
				}
			}
			Action<object> onDeserialized = jsonTypeInfo.OnDeserialized;
			if (onDeserialized != null)
			{
				onDeserialized(obj);
			}
			state.Current.ValidateAllRequiredPropertiesAreRead(jsonTypeInfo);
			value = (T)((object)obj);
			if (state.Current.PropertyRefCache != null)
			{
				state.Current.JsonTypeInfo.UpdateSortedPropertyCache(ref state.Current);
			}
			if (ctorArgumentState.ParameterRefCache != null)
			{
				state.Current.JsonTypeInfo.UpdateSortedParameterCache(ref state.Current);
			}
			return true;
		}

		// Token: 0x06000C65 RID: 3173
		protected abstract void InitializeConstructorArgumentCaches(ref ReadStack state, JsonSerializerOptions options);

		// Token: 0x06000C66 RID: 3174
		protected abstract bool ReadAndCacheConstructorArgument([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonParameterInfo jsonParameterInfo);

		// Token: 0x06000C67 RID: 3175
		protected abstract object CreateObject(ref ReadStackFrame frame);

		// Token: 0x06000C68 RID: 3176 RVA: 0x000315E4 File Offset: 0x0002F7E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ReadConstructorArguments([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			this.BeginRead(ref state, options);
			for (;;)
			{
				(ref reader).ReadWithVerify();
				JsonTokenType tokenType = reader.TokenType;
				if (tokenType == JsonTokenType.EndObject)
				{
					break;
				}
				JsonParameterInfo jsonParameterInfo;
				if (this.TryLookupConstructorParameter(ref state, ref reader, options, out jsonParameterInfo))
				{
					(ref reader).ReadWithVerify();
					if (!jsonParameterInfo.ShouldDeserialize)
					{
						bool flag = reader.TrySkip();
						state.Current.EndConstructorParameter();
					}
					else
					{
						this.ReadAndCacheConstructorArgument(ref state, ref reader, jsonParameterInfo);
						state.Current.EndConstructorParameter();
					}
				}
				else
				{
					ReadOnlySpan<byte> propertyName = JsonSerializer.GetPropertyName(ref state, ref reader);
					bool flag2;
					JsonPropertyInfo jsonPropertyInfo = JsonSerializer.LookupProperty(null, propertyName, ref state, options, out flag2, false);
					if (jsonPropertyInfo.CanDeserialize)
					{
						ArgumentState ctorArgumentState = state.Current.CtorArgumentState;
						if (ctorArgumentState.FoundProperties == null)
						{
							ctorArgumentState.FoundProperties = ArrayPool<global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>>.Shared.Rent(Math.Max(1, state.Current.JsonTypeInfo.PropertyCache.Count));
						}
						else if (ctorArgumentState.FoundPropertyCount == ctorArgumentState.FoundProperties.Length)
						{
							global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] array = ArrayPool<global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>>.Shared.Rent(ctorArgumentState.FoundProperties.Length * 2);
							ctorArgumentState.FoundProperties.CopyTo(array, 0);
							global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] foundProperties = ctorArgumentState.FoundProperties;
							ctorArgumentState.FoundProperties = array;
							ArrayPool<global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>>.Shared.Return(foundProperties, true);
						}
						global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>[] foundProperties2 = ctorArgumentState.FoundProperties;
						ArgumentState argumentState = ctorArgumentState;
						int foundPropertyCount = argumentState.FoundPropertyCount;
						argumentState.FoundPropertyCount = foundPropertyCount + 1;
						foundProperties2[foundPropertyCount] = new global::System.ValueTuple<JsonPropertyInfo, JsonReaderState, long, byte[], string>(jsonPropertyInfo, reader.CurrentState, reader.BytesConsumed, state.Current.JsonPropertyName, state.Current.JsonPropertyNameAsString);
					}
					bool flag3 = reader.TrySkip();
					state.Current.EndProperty();
				}
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00031770 File Offset: 0x0002F970
		private bool ReadConstructorArgumentsWithContinuation([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
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
				JsonParameterInfo jsonParameterInfo;
				JsonPropertyInfo jsonPropertyInfo;
				if (state.Current.PropertyState < StackFramePropertyState.Name)
				{
					state.Current.PropertyState = StackFramePropertyState.Name;
					JsonTokenType tokenType = reader.TokenType;
					if (tokenType == JsonTokenType.EndObject)
					{
						return true;
					}
					if (this.TryLookupConstructorParameter(ref state, ref reader, options, out jsonParameterInfo))
					{
						jsonPropertyInfo = null;
					}
					else
					{
						ReadOnlySpan<byte> propertyName = JsonSerializer.GetPropertyName(ref state, ref reader);
						bool flag;
						jsonPropertyInfo = JsonSerializer.LookupProperty(null, propertyName, ref state, options, out flag, false);
						state.Current.UseExtensionProperty = flag;
					}
				}
				else
				{
					jsonParameterInfo = state.Current.CtorArgumentState.JsonParameterInfo;
					jsonPropertyInfo = state.Current.JsonPropertyInfo;
				}
				if (jsonParameterInfo != null)
				{
					if (!this.HandleConstructorArgumentWithContinuation(ref state, ref reader, jsonParameterInfo))
					{
						return false;
					}
				}
				else if (!ObjectWithParameterizedConstructorConverter<T>.HandlePropertyWithContinuation(ref state, ref reader, jsonPropertyInfo))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003183C File Offset: 0x0002FA3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool HandleConstructorArgumentWithContinuation([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonParameterInfo jsonParameterInfo)
		{
			if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
			{
				if (!jsonParameterInfo.ShouldDeserialize)
				{
					if (!reader.TrySkip())
					{
						return false;
					}
					state.Current.EndConstructorParameter();
					return true;
				}
				else
				{
					state.Current.PropertyState = StackFramePropertyState.ReadValue;
					if (!JsonConverter.SingleValueReadWithReadAhead(jsonParameterInfo.EffectiveConverter.RequiresReadAhead, ref reader, ref state))
					{
						return false;
					}
				}
			}
			if (!this.ReadAndCacheConstructorArgument(ref state, ref reader, jsonParameterInfo))
			{
				return false;
			}
			state.Current.EndConstructorParameter();
			return true;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000318B4 File Offset: 0x0002FAB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool HandlePropertyWithContinuation([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonPropertyInfo jsonPropertyInfo)
		{
			if (state.Current.PropertyState < StackFramePropertyState.ReadValue)
			{
				if (!jsonPropertyInfo.CanDeserialize)
				{
					if (!reader.TrySkip())
					{
						return false;
					}
					state.Current.EndProperty();
					return true;
				}
				else if (!ObjectDefaultConverter<T>.ReadAheadPropertyValue(ref state, ref reader, jsonPropertyInfo))
				{
					return false;
				}
			}
			object obj;
			if (state.Current.UseExtensionProperty)
			{
				if (!jsonPropertyInfo.ReadJsonExtensionDataValue(ref state, ref reader, out obj))
				{
					return false;
				}
			}
			else if (!jsonPropertyInfo.ReadJsonAsObject(ref state, ref reader, out obj))
			{
				return false;
			}
			ArgumentState ctorArgumentState = state.Current.CtorArgumentState;
			if (ctorArgumentState.FoundPropertiesAsync == null)
			{
				ctorArgumentState.FoundPropertiesAsync = ArrayPool<global::System.ValueTuple<JsonPropertyInfo, object, string>>.Shared.Rent(Math.Max(1, state.Current.JsonTypeInfo.PropertyCache.Count));
			}
			else if (ctorArgumentState.FoundPropertyCount == ctorArgumentState.FoundPropertiesAsync.Length)
			{
				global::System.ValueTuple<JsonPropertyInfo, object, string>[] array = ArrayPool<global::System.ValueTuple<JsonPropertyInfo, object, string>>.Shared.Rent(ctorArgumentState.FoundPropertiesAsync.Length * 2);
				ctorArgumentState.FoundPropertiesAsync.CopyTo(array, 0);
				global::System.ValueTuple<JsonPropertyInfo, object, string>[] foundPropertiesAsync = ctorArgumentState.FoundPropertiesAsync;
				ctorArgumentState.FoundPropertiesAsync = array;
				ArrayPool<global::System.ValueTuple<JsonPropertyInfo, object, string>>.Shared.Return(foundPropertiesAsync, true);
			}
			global::System.ValueTuple<JsonPropertyInfo, object, string>[] foundPropertiesAsync2 = ctorArgumentState.FoundPropertiesAsync;
			ArgumentState argumentState = ctorArgumentState;
			int foundPropertyCount = argumentState.FoundPropertyCount;
			argumentState.FoundPropertyCount = foundPropertyCount + 1;
			foundPropertiesAsync2[foundPropertyCount] = new global::System.ValueTuple<JsonPropertyInfo, object, string>(jsonPropertyInfo, obj, state.Current.JsonPropertyNameAsString);
			state.Current.EndProperty();
			return true;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000319F0 File Offset: 0x0002FBF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void BeginRead([ScopedRef] ref ReadStack state, JsonSerializerOptions options)
		{
			JsonTypeInfo jsonTypeInfo = state.Current.JsonTypeInfo;
			jsonTypeInfo.ValidateCanBeUsedForPropertyMetadataSerialization();
			if (jsonTypeInfo.ParameterCount != jsonTypeInfo.ParameterCache.Count)
			{
				ThrowHelper.ThrowInvalidOperationException_ConstructorParameterIncompleteBinding(this.Type);
			}
			state.Current.InitializeRequiredPropertiesValidationState(jsonTypeInfo);
			state.Current.JsonPropertyInfo = null;
			this.InitializeConstructorArgumentCaches(ref state, options);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00031A50 File Offset: 0x0002FC50
		protected virtual bool TryLookupConstructorParameter([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, JsonSerializerOptions options, out JsonParameterInfo jsonParameterInfo)
		{
			ReadOnlySpan<byte> propertyName = JsonSerializer.GetPropertyName(ref state, ref reader);
			byte[] array;
			jsonParameterInfo = state.Current.JsonTypeInfo.GetParameter(propertyName, ref state.Current, out array);
			state.Current.CtorArgumentState.ParameterIndex++;
			state.Current.JsonPropertyName = array;
			state.Current.CtorArgumentState.JsonParameterInfo = jsonParameterInfo;
			JsonParameterInfo jsonParameterInfo2 = jsonParameterInfo;
			state.Current.NumberHandling = ((jsonParameterInfo2 != null) ? jsonParameterInfo2.NumberHandling : null);
			return jsonParameterInfo != null;
		}
	}
}
