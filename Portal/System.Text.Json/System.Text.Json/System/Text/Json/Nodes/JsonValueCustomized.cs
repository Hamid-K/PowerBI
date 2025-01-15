using System;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Nodes
{
	// Token: 0x02000062 RID: 98
	internal sealed class JsonValueCustomized<TValue> : JsonValue<TValue>
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00023104 File Offset: 0x00021304
		public JsonValueCustomized(TValue value, JsonTypeInfo<TValue> jsonTypeInfo, JsonNodeOptions? options = null)
			: base(value, options)
		{
			this._jsonTypeInfo = jsonTypeInfo;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00023118 File Offset: 0x00021318
		public override void WriteTo(Utf8JsonWriter writer, JsonSerializerOptions options = null)
		{
			if (writer == null)
			{
				ThrowHelper.ThrowArgumentNullException("writer");
			}
			JsonTypeInfo<TValue> jsonTypeInfo = this._jsonTypeInfo;
			if (options != null && options != jsonTypeInfo.Options)
			{
				options.MakeReadOnly();
				jsonTypeInfo = (JsonTypeInfo<TValue>)options.GetTypeInfoInternal(typeof(TValue), true, new bool?(true), false, false);
			}
			jsonTypeInfo.Serialize(writer, in this.Value, null);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00023178 File Offset: 0x00021378
		internal override JsonNode DeepCloneCore()
		{
			return JsonSerializer.SerializeToNode<TValue>(this.Value, this._jsonTypeInfo);
		}

		// Token: 0x04000289 RID: 649
		private readonly JsonTypeInfo<TValue> _jsonTypeInfo;
	}
}
