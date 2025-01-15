using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E5 RID: 229
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x00031AD0 File Offset: 0x0002FCD0
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
