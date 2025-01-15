using System;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000E5 RID: 229
	internal abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x00031980 File Offset: 0x0002FB80
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
