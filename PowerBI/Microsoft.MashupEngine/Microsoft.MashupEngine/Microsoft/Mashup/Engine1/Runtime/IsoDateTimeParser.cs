using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001354 RID: 4948
	public class IsoDateTimeParser
	{
		// Token: 0x06008221 RID: 33313 RVA: 0x001BA1B5 File Offset: 0x001B83B5
		public static bool TryParseTime(string text, out DateTime dateTime)
		{
			return FastIsoDateTimeParser.TryParseTime(text, out dateTime);
		}

		// Token: 0x06008222 RID: 33314 RVA: 0x001BA1BE File Offset: 0x001B83BE
		public static bool TryParseDate(string text, out DateTime dateTime)
		{
			return FastIsoDateTimeParser.TryParseDate(text, out dateTime);
		}

		// Token: 0x06008223 RID: 33315 RVA: 0x001BA1C7 File Offset: 0x001B83C7
		public static bool TryParseDateTime(string text, out DateTime dateTime)
		{
			return FastIsoDateTimeParser.TryParseDateTime(text, out dateTime);
		}

		// Token: 0x06008224 RID: 33316 RVA: 0x001BA1D0 File Offset: 0x001B83D0
		public static bool TryParseDateTimeOffset(string text, out DateTimeOffset dateTimeOffset)
		{
			return FastIsoDateTimeParser.TryParseDateTimeOffset(text, out dateTimeOffset);
		}
	}
}
