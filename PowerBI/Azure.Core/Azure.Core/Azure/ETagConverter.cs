using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	internal class ETagConverter : JsonConverter<ETag>
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000025D0 File Offset: 0x000007D0
		public override ETag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			string @string = reader.GetString();
			if (@string == null)
			{
				return default(ETag);
			}
			return new ETag(@string);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000025F8 File Offset: 0x000007F8
		public override void Write(Utf8JsonWriter writer, ETag value, JsonSerializerOptions options)
		{
			if (value == default(ETag))
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteStringValue(value.ToString("H"));
		}
	}
}
