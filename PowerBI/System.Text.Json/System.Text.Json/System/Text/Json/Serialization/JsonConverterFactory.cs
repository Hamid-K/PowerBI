using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008F RID: 143
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonConverterFactory : JsonConverter
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00026158 File Offset: 0x00024358
		private protected override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.None;
		}

		// Token: 0x060008B5 RID: 2229
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options);

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002615B File Offset: 0x0002435B
		internal sealed override Type KeyType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0002615E File Offset: 0x0002435E
		internal sealed override Type ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00026164 File Offset: 0x00024364
		internal JsonConverter GetConverterInternal(Type typeToConvert, JsonSerializerOptions options)
		{
			JsonConverter jsonConverter = this.CreateConverter(typeToConvert, options);
			if (jsonConverter != null)
			{
				if (jsonConverter is JsonConverterFactory)
				{
					ThrowHelper.ThrowInvalidOperationException_SerializerConverterFactoryReturnsJsonConverterFactorty(base.GetType());
				}
			}
			else
			{
				ThrowHelper.ThrowInvalidOperationException_SerializerConverterFactoryReturnsNull(base.GetType());
			}
			return jsonConverter;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000261A0 File Offset: 0x000243A0
		internal sealed override object ReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x000261A7 File Offset: 0x000243A7
		internal sealed override bool OnTryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000261AE File Offset: 0x000243AE
		internal sealed override bool TryReadAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000261B5 File Offset: 0x000243B5
		internal sealed override object ReadAsPropertyNameAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000261BC File Offset: 0x000243BC
		internal sealed override object ReadAsPropertyNameCoreAsObject(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000261C3 File Offset: 0x000243C3
		internal sealed override object ReadNumberWithCustomHandlingAsObject(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x000261CA File Offset: 0x000243CA
		internal sealed override void WriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000261D1 File Offset: 0x000243D1
		internal sealed override bool OnTryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000261D8 File Offset: 0x000243D8
		internal sealed override bool TryWriteAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, ref WriteStack state)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000261DF File Offset: 0x000243DF
		internal sealed override void WriteAsPropertyNameAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000261E6 File Offset: 0x000243E6
		public sealed override Type Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000261E9 File Offset: 0x000243E9
		internal sealed override void WriteAsPropertyNameCoreAsObject(Utf8JsonWriter writer, object value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000261F0 File Offset: 0x000243F0
		internal sealed override void WriteNumberWithCustomHandlingAsObject(Utf8JsonWriter writer, object value, JsonNumberHandling handling)
		{
			throw new InvalidOperationException();
		}
	}
}
