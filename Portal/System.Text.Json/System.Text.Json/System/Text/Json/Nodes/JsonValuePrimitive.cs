using System;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Nodes
{
	// Token: 0x02000064 RID: 100
	internal sealed class JsonValuePrimitive<TValue> : JsonValue<TValue>
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x00023F62 File Offset: 0x00022162
		public JsonValuePrimitive(TValue value, JsonConverter<TValue> converter, JsonNodeOptions? options = null)
			: base(value, options)
		{
			this._converter = converter;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00023F74 File Offset: 0x00022174
		public override void WriteTo(Utf8JsonWriter writer, JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			JsonConverter<TValue> converter = this._converter;
			if (options == null)
			{
				options = JsonValuePrimitive<TValue>.s_defaultOptions;
			}
			if (converter.IsInternalConverterForNumberType)
			{
				converter.WriteNumberWithCustomHandling(writer, this.Value, options.NumberHandling);
				return;
			}
			converter.Write(writer, this.Value, options);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00023FCC File Offset: 0x000221CC
		internal override JsonNode DeepCloneCore()
		{
			TValue value = this.Value;
			if (value is JsonElement)
			{
				return new JsonValuePrimitive<JsonElement>((value as JsonElement).Clone(), JsonMetadataServices.JsonElementConverter, base.Options);
			}
			return new JsonValuePrimitive<TValue>(this.Value, this._converter, base.Options);
		}

		// Token: 0x0400028B RID: 651
		private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions();

		// Token: 0x0400028C RID: 652
		private readonly JsonConverter<TValue> _converter;
	}
}
