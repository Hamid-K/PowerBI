using System;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000E4 RID: 228
	internal abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x00031200 File Offset: 0x0002F400
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
