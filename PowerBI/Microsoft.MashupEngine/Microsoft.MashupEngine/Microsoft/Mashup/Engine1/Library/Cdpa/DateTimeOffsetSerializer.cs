using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E4C RID: 3660
	internal static class DateTimeOffsetSerializer
	{
		// Token: 0x0600625F RID: 25183 RVA: 0x00151DEC File Offset: 0x0014FFEC
		public static string ToString(DateTimeOffset? value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
		}

		// Token: 0x040035A5 RID: 13733
		private const string utcDateTimeOffsetIso8601 = "yyyy-MM-ddTHH:mm:ssZ";
	}
}
