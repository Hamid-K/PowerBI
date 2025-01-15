using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000DF RID: 223
	internal sealed class FSharpOptionConverter<TOption, TElement> : JsonConverter<TOption> where TOption : class
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002FE39 File Offset: 0x0002E039
		internal override Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0002FE45 File Offset: 0x0002E045
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002FE48 File Offset: 0x0002E048
		[RequiresUnreferencedCode("Uses Reflection to access FSharp.Core components at runtime.")]
		[RequiresDynamicCode("Uses Reflection to access FSharp.Core components at runtime.")]
		public FSharpOptionConverter(JsonConverter<TElement> elementConverter)
		{
			this._elementConverter = elementConverter;
			this._optionValueGetter = FSharpCoreReflectionProxy.Instance.CreateFSharpOptionValueGetter<TOption, TElement>();
			this._optionConstructor = FSharpCoreReflectionProxy.Instance.CreateFSharpOptionSomeConstructor<TOption, TElement>();
			base.ConverterStrategy = elementConverter.ConverterStrategy;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002FE84 File Offset: 0x0002E084
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out TOption value)
		{
			if (!state.IsContinuation && reader.TokenType == JsonTokenType.Null)
			{
				value = default(TOption);
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
			value = default(TOption);
			return false;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002FF10 File Offset: 0x0002E110
		internal override bool OnTryWrite(Utf8JsonWriter writer, TOption value, JsonSerializerOptions options, ref WriteStack state)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return true;
			}
			TElement telement = this._optionValueGetter(value);
			state.Current.JsonPropertyInfo = state.Current.JsonTypeInfo.ElementTypeInfo.PropertyInfoForTypeInfo;
			return this._elementConverter.TryWrite(writer, in telement, options, ref state);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002FF70 File Offset: 0x0002E170
		public override void Write(Utf8JsonWriter writer, TOption value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			TElement telement = this._optionValueGetter(value);
			this._elementConverter.Write(writer, telement, options);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002FFA8 File Offset: 0x0002E1A8
		public override TOption Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return default(TOption);
			}
			TElement telement = this._elementConverter.Read(ref reader, typeToConvert, options);
			return this._optionConstructor(telement);
		}

		// Token: 0x040003FE RID: 1022
		private readonly JsonConverter<TElement> _elementConverter;

		// Token: 0x040003FF RID: 1023
		private readonly Func<TOption, TElement> _optionValueGetter;

		// Token: 0x04000400 RID: 1024
		private readonly Func<TElement, TOption> _optionConstructor;
	}
}
