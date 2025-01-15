using System;
using System.Globalization;
using Microsoft.IdentityModel.Json.Bson;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000E1 RID: 225
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Microsoft.IdentityModel.Json.Bson for more details.")]
	internal class BsonObjectIdConverter : JsonConverter
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x000312DC File Offset: 0x0002F4DC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			BsonObjectId bsonObjectId = (BsonObjectId)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
				return;
			}
			writer.WriteValue(bsonObjectId.Value);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00031313 File Offset: 0x0002F513
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Bytes)
			{
				throw new JsonSerializationException("Expected Bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			return new BsonObjectId((byte[])reader.Value);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0003134F File Offset: 0x0002F54F
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BsonObjectId);
		}
	}
}
