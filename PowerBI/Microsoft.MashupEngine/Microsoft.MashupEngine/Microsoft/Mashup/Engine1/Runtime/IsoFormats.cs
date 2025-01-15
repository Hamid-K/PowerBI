using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001359 RID: 4953
	internal static class IsoFormats
	{
		// Token: 0x040046E4 RID: 18148
		private const string T = "T";

		// Token: 0x040046E5 RID: 18149
		private const string escapedT = "\\T";

		// Token: 0x040046E6 RID: 18150
		public static readonly Dictionary<string, string> DateIsoToClrFormatMap = new Dictionary<string, string>
		{
			{ "YYYY-MM-DD", "yyyy-MM-dd" },
			{ "YYYYMMDD", "yyyyMMdd" },
			{ "M/d/yyyy", "M/d/yyyy" }
		};

		// Token: 0x040046E7 RID: 18151
		public static readonly Dictionary<string, string> TimeIsoToClrFormatMap = new Dictionary<string, string>
		{
			{ "hh:mm:ss", "HH:mm:ss" },
			{ "hh:mm:ss.nnn", "HH:mm:ss.fff" },
			{ "hh:mm:ss.nnnnnnn", "HH:mm:ss.fffffff" },
			{ "h:mm tt", "h:mm tt" },
			{ "hh:mm", "HH:mm" }
		};

		// Token: 0x040046E8 RID: 18152
		public static readonly Dictionary<string, string> DateTimeIsoToClrFormatMap = new Dictionary<string, string>
		{
			{ "YYYY-MM-DDThh:mm:ss", "yyyy-MM-dd\\THH:mm:ss" },
			{ "YYYYMMDDThh:mm:ss", "yyyyMMdd\\THH:mm:ss" },
			{ "YYYY-MM-DDThh:mm:ss.nnn", "yyyy-MM-dd\\THH:mm:ss.fff" },
			{ "YYYYMMDDThh:mm:ss.nnn", "yyyyMMdd\\THH:mm:ss.fff" },
			{ "YYYY-MM-DDThh:mm:ss.nnnnnnn", "yyyy-MM-dd\\THH:mm:ss.fffffff" },
			{ "YYYYMMDDThh:mm:ss.nnnnnnn", "yyyyMMdd\\THH:mm:ss.fffffff" },
			{ "yyyy'-'MM'-'dd'T'HH':'mm':'ss", "yyyy'-'MM'-'dd'T'HH':'mm':'ss" },
			{ "YYYY-MM-DDThh:mm", "yyyy-MM-dd\\THH:mm" },
			{ "YYYYMMDDThh:mm", "yyyyMMdd\\THH:mm" }
		};

		// Token: 0x040046E9 RID: 18153
		public static readonly Dictionary<string, string> DateTimeZoneIsoToClrFormatMap = new Dictionary<string, string>
		{
			{ "YYYY-MM-DDThh:mm:ss+hh:mm", "yyyy-MM-dd\\THH:mm:sszzz" },
			{ "YYYYMMDDThh:mm:ss+hh:mm", "yyyyMMdd\\THH:mm:sszzz" },
			{ "YYYY-MM-DDThh:mm:ss.nnn+hh:mm", "yyyy-MM-dd\\THH:mm:ss.fffzzz" },
			{ "YYYYMMDDThh:mm:ss.nnn+hh:mm", "yyyyMMdd\\THH:mm:ss.fffzzz" },
			{ "YYYY-MM-DDThh:mm:ss.nnnnnnn+hh:mm", "yyyy-MM-dd\\THH:mm:ss.fffffffzzz" },
			{ "YYYYMMDDThh:mm:ss.nnnnnnn+hh:mm", "yyyyMMdd\\THH:mm:ss.fffffffzzz" },
			{ "YYYY-MM-DDThh:mm:ss-hh:mm", "yyyy-MM-dd\\THH:mm:sszzz" },
			{ "YYYYMMDDThh:mm:ss-hh:mm", "yyyyMMdd\\THH:mm:sszzz" },
			{ "YYYY-MM-DDThh:mm:ss.nnn-hh:mm", "yyyy-MM-dd\\THH:mm:ss.fffzzz" },
			{ "YYYYMMDDThh:mm:ss.nnn-hh:mm", "yyyyMMdd\\THH:mm:ss.fffzzz" },
			{ "YYYY-MM-DDThh:mm:ss.nnnnnnn-hh:mm", "yyyy-MM-dd\\THH:mm:ss.fffffffzzz" },
			{ "YYYYMMDDThh:mm:ss.nnnnnnn-hh:mm", "yyyyMMdd\\THH:mm:ss.fffffffzzz" },
			{ "YYYY-MM-DDThh:mm:ssZ", "yyyy-MM-dd\\THH:mm:ssK" },
			{ "YYYYMMDDThh:mm:ssZ", "yyyyMMdd\\THH:mm:ssK" },
			{ "YYYY-MM-DDThh:mm:ss.nnnZ", "yyyy-MM-dd\\THH:mm:ss.fffK" },
			{ "YYYYMMDDThh:mm:ss.nnnZ", "yyyyMMdd\\THH:mm:ss.fffK" },
			{ "YYYY-MM-DDThh:mm:ss.nnnnnnnZ", "yyyy-MM-dd\\THH:mm:ss.fffffffK" },
			{ "YYYYMMDDThh:mm:ss.nnnnnnnZ", "yyyyMMdd\\THH:mm:ss.fffffffK" },
			{ "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", "yyyy'-'MM'-'dd HH':'mm':'ss zzz" },
			{ "YYYY-MM-DDThh:mm+hh:mm", "yyyy-MM-dd\\THH:mmzzz" },
			{ "YYYYMMDDThh:mm+hh:mm", "yyyyMMdd\\THH:mmzzz" },
			{ "YYYY-MM-DDThh:mm-hh:mm", "yyyy-MM-dd\\THH:mmzzz" },
			{ "YYYYMMDDThh:mm-hh:mm", "yyyyMMdd\\THH:mmzzz" },
			{ "YYYY-MM-DDThh:mmZ", "yyyy-MM-dd\\THH:mmK" },
			{ "YYYYMMDDThh:mmZ", "yyyyMMdd\\THH:mmK" }
		};
	}
}
