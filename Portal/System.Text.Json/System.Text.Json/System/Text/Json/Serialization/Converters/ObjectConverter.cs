using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E6 RID: 230
	internal abstract class ObjectConverter : JsonConverter<object>
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x0003050C File Offset: 0x0002E70C
		private protected override ConverterStrategy GetDefaultConverterStrategy()
		{
			return ConverterStrategy.Object;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003050F File Offset: 0x0002E70F
		public ObjectConverter()
		{
			base.CanBePolymorphic = true;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0003051E File Offset: 0x0002E71E
		public sealed override object ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ThrowHelper.ThrowNotSupportedException_DictionaryKeyTypeNotSupported(this.Type, this);
			return null;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003052D File Offset: 0x0002E72D
		internal sealed override object ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ThrowHelper.ThrowNotSupportedException_DictionaryKeyTypeNotSupported(this.Type, this);
			return null;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0003053C File Offset: 0x0002E73C
		public sealed override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteStartObject();
			writer.WriteEndObject();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00030554 File Offset: 0x0002E754
		public sealed override void WriteAsPropertyName(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
		{
			this.WriteAsPropertyNameCore(writer, value, options, false);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00030560 File Offset: 0x0002E760
		internal sealed override void WriteAsPropertyNameCore(Utf8JsonWriter writer, object value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			Type type = value.GetType();
			if (type == this.Type)
			{
				ThrowHelper.ThrowNotSupportedException_DictionaryKeyTypeNotSupported(type, this);
			}
			JsonConverter converterInternal = options.GetConverterInternal(type);
			converterInternal.WriteAsPropertyNameCoreAsObject(writer, value, options, isWritingExtensionDataProperty);
		}
	}
}
