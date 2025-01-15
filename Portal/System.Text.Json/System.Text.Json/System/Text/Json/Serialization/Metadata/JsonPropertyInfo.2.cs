using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000AE RID: 174
	internal sealed class JsonPropertyInfo<T> : JsonPropertyInfo
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x0002A85E File Offset: 0x00028A5E
		internal JsonPropertyInfo(Type declaringType, JsonTypeInfo declaringTypeInfo, JsonSerializerOptions options)
			: base(declaringType, typeof(T), declaringTypeInfo, options)
		{
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002A873 File Offset: 0x00028A73
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0002A87B File Offset: 0x00028A7B
		internal new Func<object, T> Get
		{
			get
			{
				return this._typedGet;
			}
			set
			{
				this.SetGetter(value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002A884 File Offset: 0x00028A84
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0002A88C File Offset: 0x00028A8C
		internal new Action<object, T> Set
		{
			get
			{
				return this._typedSet;
			}
			set
			{
				this.SetSetter(value);
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002A898 File Offset: 0x00028A98
		private protected override void SetGetter(Delegate getter)
		{
			if (getter == null)
			{
				this._typedGet = null;
				this._untypedGet = null;
				return;
			}
			Func<object, T> typedGetter = getter as Func<object, T>;
			if (typedGetter != null)
			{
				this._typedGet = typedGetter;
				Func<object, object> func = getter as Func<object, object>;
				this._untypedGet = ((func != null) ? func : ((object obj) => typedGetter(obj)));
				return;
			}
			Func<object, object> untypedGet = (Func<object, object>)getter;
			this._typedGet = (object obj) => (T)((object)untypedGet(obj));
			this._untypedGet = untypedGet;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002A930 File Offset: 0x00028B30
		private protected override void SetSetter(Delegate setter)
		{
			if (setter == null)
			{
				this._typedSet = null;
				this._untypedSet = null;
				return;
			}
			Action<object, T> typedSetter = setter as Action<object, T>;
			if (typedSetter != null)
			{
				this._typedSet = typedSetter;
				Action<object, object> action = setter as Action<object, object>;
				this._untypedSet = ((action != null) ? action : delegate(object obj, object value)
				{
					typedSetter(obj, (T)((object)value));
				});
				return;
			}
			Action<object, object> untypedSet = (Action<object, object>)setter;
			this._typedSet = delegate(object obj, T value)
			{
				untypedSet(obj, value);
			};
			this._untypedSet = untypedSet;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0002A9C5 File Offset: 0x00028BC5
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0002A9CD File Offset: 0x00028BCD
		internal new Func<object, T, bool> ShouldSerialize
		{
			get
			{
				return this._shouldSerializeTyped;
			}
			set
			{
				this.SetShouldSerialize(value);
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002A9D8 File Offset: 0x00028BD8
		private protected override void SetShouldSerialize(Delegate predicate)
		{
			if (predicate == null)
			{
				this._shouldSerializeTyped = null;
				this._shouldSerialize = null;
				return;
			}
			Func<object, T, bool> typedPredicate = predicate as Func<object, T, bool>;
			if (typedPredicate != null)
			{
				this._shouldSerializeTyped = typedPredicate;
				Func<object, object, bool> func = typedPredicate as Func<object, object, bool>;
				this._shouldSerialize = ((func != null) ? func : ((object obj, object value) => typedPredicate(obj, (T)((object)value))));
				return;
			}
			Func<object, object, bool> untypedPredicate = (Func<object, object, bool>)predicate;
			this._shouldSerializeTyped = (object obj, T value) => untypedPredicate(obj, value);
			this._shouldSerialize = untypedPredicate;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002AA74 File Offset: 0x00028C74
		internal override object DefaultValue
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002AA90 File Offset: 0x00028C90
		internal override bool PropertyTypeCanBeNull
		{
			get
			{
				return default(T) == null;
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002AAAE File Offset: 0x00028CAE
		internal override JsonParameterInfo CreateJsonParameterInfo(JsonParameterInfoValues parameterInfoValues)
		{
			return new JsonParameterInfo<T>(parameterInfoValues, this);
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002AAB7 File Offset: 0x00028CB7
		internal new JsonConverter<T> EffectiveConverter
		{
			get
			{
				return this._typedEffectiveConverter;
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002AABF File Offset: 0x00028CBF
		[RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
		[RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
		internal override void DetermineReflectionPropertyAccessors(MemberInfo memberInfo, bool useNonPublicAccessors)
		{
			DefaultJsonTypeInfoResolver.DeterminePropertyAccessors<T>(this, memberInfo, useNonPublicAccessors);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002AACC File Offset: 0x00028CCC
		private protected override void DetermineEffectiveConverter(JsonTypeInfo jsonTypeInfo)
		{
			JsonConverter jsonConverter = base.Options.ExpandConverterFactory(base.CustomConverter, base.PropertyType);
			JsonConverter<T> jsonConverter2 = ((jsonConverter != null) ? jsonConverter.CreateCastingConverter<T>() : null) ?? ((JsonTypeInfo<T>)jsonTypeInfo).EffectiveConverter;
			this._effectiveConverter = jsonConverter2;
			this._typedEffectiveConverter = jsonConverter2;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002AB1A File Offset: 0x00028D1A
		internal override object GetValueAsObject(object obj)
		{
			if (base.IsForTypeInfo)
			{
				return obj;
			}
			return this.Get(obj);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002AB38 File Offset: 0x00028D38
		internal override bool GetMemberAndWriteJson(object obj, ref WriteStack state, Utf8JsonWriter writer)
		{
			T t = this.Get(obj);
			if (!this.EffectiveConverter.IsValueType && base.Options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.IgnoreCycles && t != null && !state.IsContinuation && this.EffectiveConverter.ConverterStrategy != ConverterStrategy.Value && state.ReferenceResolver.ContainsReferenceForCycleDetection(t))
			{
				t = default(T);
			}
			if (base.IgnoreDefaultValuesOnWrite)
			{
				if (JsonPropertyInfo<T>.IsDefaultValue(t))
				{
					return true;
				}
			}
			else
			{
				Func<object, T, bool> shouldSerialize = this.ShouldSerialize;
				if (shouldSerialize != null && !shouldSerialize(obj, t))
				{
					return true;
				}
			}
			if (t == null)
			{
				if (this.EffectiveConverter.HandleNullOnWrite)
				{
					if (state.Current.PropertyState < StackFramePropertyState.Name)
					{
						state.Current.PropertyState = StackFramePropertyState.Name;
						writer.WritePropertyNameSection(base.EscapedNameSection);
					}
					int currentDepth = writer.CurrentDepth;
					this.EffectiveConverter.Write(writer, t, base.Options);
					if (currentDepth != writer.CurrentDepth)
					{
						ThrowHelper.ThrowJsonException_SerializationConverterWrite(this.EffectiveConverter);
					}
				}
				else
				{
					writer.WriteNullSection(base.EscapedNameSection);
				}
				return true;
			}
			if (state.Current.PropertyState < StackFramePropertyState.Name)
			{
				state.Current.PropertyState = StackFramePropertyState.Name;
				writer.WritePropertyNameSection(base.EscapedNameSection);
			}
			return this.EffectiveConverter.TryWrite(writer, in t, base.Options, ref state);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002AC98 File Offset: 0x00028E98
		internal override bool GetMemberAndWriteJsonExtensionData(object obj, ref WriteStack state, Utf8JsonWriter writer)
		{
			T t = this.Get(obj);
			Func<object, T, bool> shouldSerialize = this.ShouldSerialize;
			return (shouldSerialize != null && !shouldSerialize(obj, t)) || t == null || this.EffectiveConverter.TryWriteDataExtensionProperty(writer, t, base.Options, ref state);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002ACF0 File Offset: 0x00028EF0
		internal override bool ReadJsonAndSetMember(object obj, [ScopedRef] ref ReadStack state, ref Utf8JsonReader reader)
		{
			bool flag = reader.TokenType == JsonTokenType.Null;
			bool flag2;
			if (flag && !this.EffectiveConverter.HandleNullOnRead && !state.IsContinuation)
			{
				if (default(T) != null || !base.CanDeserialize)
				{
					if (default(T) == null)
					{
						ThrowHelper.ThrowInvalidOperationException_DeserializeUnableToAssignNull(this.EffectiveConverter.Type);
					}
					ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.EffectiveConverter.Type);
				}
				if (!base.IgnoreNullTokensOnRead)
				{
					T t = default(T);
					this.Set(obj, t);
				}
				flag2 = true;
				state.Current.MarkRequiredPropertyAsRead(this);
			}
			else if (this.EffectiveConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				if (!flag || !base.IgnoreNullTokensOnRead || default(T) != null)
				{
					T t2 = this.EffectiveConverter.Read(ref reader, base.PropertyType, base.Options);
					this.Set(obj, t2);
				}
				flag2 = true;
				state.Current.MarkRequiredPropertyAsRead(this);
			}
			else
			{
				flag2 = true;
				if (!flag || !base.IgnoreNullTokensOnRead || default(T) != null || state.IsContinuation)
				{
					state.Current.ReturnValue = obj;
					T t3;
					bool flag3;
					flag2 = this.EffectiveConverter.TryRead(ref reader, base.PropertyType, base.Options, ref state, out t3, out flag3);
					if (flag2)
					{
						if ((typeof(T).IsValueType || !flag3) && base.CanDeserialize)
						{
							this.Set(obj, t3);
						}
						state.Current.MarkRequiredPropertyAsRead(this);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002AEA0 File Offset: 0x000290A0
		internal override bool ReadJsonAsObject([ScopedRef] ref ReadStack state, ref Utf8JsonReader reader, out object value)
		{
			bool flag = reader.TokenType == JsonTokenType.Null;
			bool flag2;
			if (flag && !this.EffectiveConverter.HandleNullOnRead && !state.IsContinuation)
			{
				if (default(T) != null)
				{
					ThrowHelper.ThrowJsonException_DeserializeUnableToConvertValue(this.EffectiveConverter.Type);
				}
				value = default(T);
				flag2 = true;
			}
			else if (this.EffectiveConverter.CanUseDirectReadOrWrite && state.Current.NumberHandling == null)
			{
				value = this.EffectiveConverter.Read(ref reader, base.PropertyType, base.Options);
				flag2 = true;
			}
			else
			{
				T t;
				bool flag3;
				flag2 = this.EffectiveConverter.TryRead(ref reader, base.PropertyType, base.Options, ref state, out t, out flag3);
				value = t;
			}
			return flag2;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002AF70 File Offset: 0x00029170
		private protected override void ConfigureIgnoreCondition(JsonIgnoreCondition? ignoreCondition)
		{
			if (ignoreCondition != null)
			{
				switch (ignoreCondition.GetValueOrDefault())
				{
				case JsonIgnoreCondition.Never:
				{
					Func<object, T, bool> func;
					if ((func = JsonPropertyInfo<T>.<>O.<0>__ShouldSerializeIgnoreConditionNever) == null)
					{
						func = (JsonPropertyInfo<T>.<>O.<0>__ShouldSerializeIgnoreConditionNever = new Func<object, T, bool>(JsonPropertyInfo<T>.<ConfigureIgnoreCondition>g__ShouldSerializeIgnoreConditionNever|31_0));
					}
					this.ShouldSerialize = func;
					return;
				}
				case JsonIgnoreCondition.Always:
				{
					Func<object, T, bool> func2;
					if ((func2 = JsonPropertyInfo<T>.<>O.<1>__ShouldSerializeIgnoreConditionAlways) == null)
					{
						func2 = (JsonPropertyInfo<T>.<>O.<1>__ShouldSerializeIgnoreConditionAlways = new Func<object, T, bool>(JsonPropertyInfo<T>.<ConfigureIgnoreCondition>g__ShouldSerializeIgnoreConditionAlways|31_1));
					}
					this.ShouldSerialize = func2;
					return;
				}
				case JsonIgnoreCondition.WhenWritingDefault:
				{
					Func<object, T, bool> func3;
					if ((func3 = JsonPropertyInfo<T>.<>O.<2>__ShouldSerializeIgnoreWhenWritingDefault) == null)
					{
						func3 = (JsonPropertyInfo<T>.<>O.<2>__ShouldSerializeIgnoreWhenWritingDefault = new Func<object, T, bool>(JsonPropertyInfo<T>.<ConfigureIgnoreCondition>g__ShouldSerializeIgnoreWhenWritingDefault|31_2));
					}
					this.ShouldSerialize = func3;
					base.IgnoreDefaultValuesOnWrite = true;
					break;
				}
				case JsonIgnoreCondition.WhenWritingNull:
					if (this.PropertyTypeCanBeNull)
					{
						Func<object, T, bool> func4;
						if ((func4 = JsonPropertyInfo<T>.<>O.<2>__ShouldSerializeIgnoreWhenWritingDefault) == null)
						{
							func4 = (JsonPropertyInfo<T>.<>O.<2>__ShouldSerializeIgnoreWhenWritingDefault = new Func<object, T, bool>(JsonPropertyInfo<T>.<ConfigureIgnoreCondition>g__ShouldSerializeIgnoreWhenWritingDefault|31_2));
						}
						this.ShouldSerialize = func4;
						base.IgnoreDefaultValuesOnWrite = true;
						return;
					}
					ThrowHelper.ThrowInvalidOperationException_IgnoreConditionOnValueTypeInvalid(base.MemberName, base.DeclaringType);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002B058 File Offset: 0x00029258
		private static bool IsDefaultValue(T value)
		{
			if (default(T) != null)
			{
				return EqualityComparer<T>.Default.Equals(default(T), value);
			}
			return value == null;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002B093 File Offset: 0x00029293
		[CompilerGenerated]
		internal static bool <ConfigureIgnoreCondition>g__ShouldSerializeIgnoreConditionNever|31_0(object _, T value)
		{
			return true;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002B096 File Offset: 0x00029296
		[CompilerGenerated]
		internal static bool <ConfigureIgnoreCondition>g__ShouldSerializeIgnoreConditionAlways|31_1(object _, T value)
		{
			return false;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002B09C File Offset: 0x0002929C
		[CompilerGenerated]
		internal static bool <ConfigureIgnoreCondition>g__ShouldSerializeIgnoreWhenWritingDefault|31_2(object _, T value)
		{
			if (default(T) != null)
			{
				return !EqualityComparer<T>.Default.Equals(default(T), value);
			}
			return value != null;
		}

		// Token: 0x0400039A RID: 922
		private Func<object, T> _typedGet;

		// Token: 0x0400039B RID: 923
		private Action<object, T> _typedSet;

		// Token: 0x0400039C RID: 924
		private Func<object, T, bool> _shouldSerializeTyped;

		// Token: 0x0400039D RID: 925
		private JsonConverter<T> _typedEffectiveConverter;

		// Token: 0x0200013F RID: 319
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000507 RID: 1287
			public static Func<object, T, bool> <0>__ShouldSerializeIgnoreConditionNever;

			// Token: 0x04000508 RID: 1288
			public static Func<object, T, bool> <1>__ShouldSerializeIgnoreConditionAlways;

			// Token: 0x04000509 RID: 1289
			public static Func<object, T, bool> <2>__ShouldSerializeIgnoreWhenWritingDefault;
		}
	}
}
