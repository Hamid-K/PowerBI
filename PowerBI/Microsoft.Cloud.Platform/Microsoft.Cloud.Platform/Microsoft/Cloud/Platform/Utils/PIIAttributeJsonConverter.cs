using System;
using Newtonsoft.Json;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B0 RID: 432
	public sealed class PIIAttributeJsonConverter : JsonConverter
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0000E568 File Offset: 0x0000C768
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00014B8A File Offset: 0x00012D8A
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00026D8A File Offset: 0x00024F8A
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteValue(string.Empty);
				return;
			}
			writer.WriteValue(((string)value).MarkAsPrivate().ObfuscatePrivateValue(false));
		}
	}
}
