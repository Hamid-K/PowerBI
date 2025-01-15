using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E4E RID: 3662
	internal static class JsonSerializerExtensions
	{
		// Token: 0x06006261 RID: 25185 RVA: 0x00151E25 File Offset: 0x00150025
		public static string ToJson(this object obj)
		{
			return new JsonSerializer().Serialize(obj);
		}
	}
}
