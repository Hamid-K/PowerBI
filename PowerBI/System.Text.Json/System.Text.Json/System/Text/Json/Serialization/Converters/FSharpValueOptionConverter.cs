using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E0 RID: 224
	internal sealed class FSharpValueOptionConverter<TValueOption, TElement> : JsonConverter<TValueOption> where TValueOption : struct, IEquatable<TValueOption>
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0002FFE4 File Offset: 0x0002E1E4
		internal override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002FFF3 File Offset: 0x0002E1F3
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpValueOptionConverter(JsonConverter<TElement> elementConverter)
		{
			this._elementConverter = elementConverter;
			this._optionValueGetter = FSharpCoreReflectionProxy.Instance.CreateFSharpValueOptionValueGetter<TValueOption, TElement>();
			this._optionConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpValueOptionSomeConstructor<TValueOption, TElement>();
			base.ConverterStrategy = elementConverter.ConverterStrategy;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00030030 File Offset: 0x0002E230
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out TValueOption value)
		{
			if (!state.IsContinuation && reader.TokenType == JsonTokenType.Null)
			{
				value = default(TValueOption);
				return true;
			}
			state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			TElement telement;
			bool flag;
			if (this._elementConverter.TryRead(ref reader, typeof(TElement), options, ref state, out telement, out flag))
			{
				value = this._optionConstructor(telement);
				return true;
			}
			value = default(TValueOption);
			return false;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000300BC File Offset: 0x0002E2BC
		internal override bool OnTryWrite(Utf8JsonWriter writer, TValueOption value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (value.Equals(default(TValueOption)))
			{
				writer.WriteNullValue();
				return true;
			}
			TElement telement = this._optionValueGetter(ref value);
			state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			return this._elementConverter.TryWrite(writer, in telement, options, ref state);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003012C File Offset: 0x0002E32C
		public override void Write(Utf8JsonWriter writer, TValueOption value, JsonSerializerOptions options)
		{
			if (value.Equals(default(TValueOption)))
			{
				writer.WriteNullValue();
				return;
			}
			TElement telement = this._optionValueGetter(ref value);
			this._elementConverter.Write(writer, telement, options);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00030174 File Offset: 0x0002E374
		public override TValueOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return default(TValueOption);
			}
			TElement telement = this._elementConverter.Read(ref reader, typeToConvert, options);
			return this._optionConstructor(telement);
		}

		// Token: 0x04000401 RID: 1025
		private readonly JsonConverter<TElement> _elementConverter;

		// Token: 0x04000402 RID: 1026
		private readonly FSharpCoreReflectionProxy.StructGetter<TValueOption, TElement> _optionValueGetter;

		// Token: 0x04000403 RID: 1027
		private readonly Func<TElement, TValueOption> _optionConstructor;
	}
}
