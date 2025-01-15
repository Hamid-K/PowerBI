using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001341 RID: 4929
	internal class InstantFormats
	{
		// Token: 0x060081EF RID: 33263 RVA: 0x001B9258 File Offset: 0x001B7458
		public static string GetOutputFormat(InstantFormats.Date.OutputFormat format)
		{
			return InstantFormats.Date.OutputFormats[(int)format];
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x001B9261 File Offset: 0x001B7461
		public static string GetOutputFormat(InstantFormats.Time.OutputFormat format)
		{
			return InstantFormats.Time.OutputFormats[(int)format];
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x001B926A File Offset: 0x001B746A
		public static string GetOutputFormat(InstantFormats.UtcOffset.OutputFormat format)
		{
			return InstantFormats.UtcOffset.OutputFormats[(int)format];
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x001B9273 File Offset: 0x001B7473
		public static string GetOutputFormat(InstantFormats.DateTime.OutputFormat format)
		{
			return InstantFormats.DateTime.OutputFormats[(int)format];
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x001B927C File Offset: 0x001B747C
		public static string GetOutputFormat(InstantFormats.DateTimeOffset.OutputFormat format)
		{
			return InstantFormats.DateTimeOffset.OutputFormats[(int)format];
		}

		// Token: 0x040046A5 RID: 18085
		private const string T = "T";

		// Token: 0x040046A6 RID: 18086
		private const string escapedT = "\\T";

		// Token: 0x02001342 RID: 4930
		public static class Date
		{
			// Token: 0x040046A7 RID: 18087
			public static readonly Dictionary<string, string> IsoToClrOutputFormatMap = new Dictionary<string, string>
			{
				{ "YYYY-MM-DD", "yyyy-MM-dd" },
				{ "YYYYMMDD", "yyyyMMdd" },
				{ "M/d/yyyy", "M/d/yyyy" }
			};

			// Token: 0x040046A8 RID: 18088
			public static readonly Dictionary<string, string> IsoToClrInputFormatMap = InstantFormats.Date.IsoToClrOutputFormatMap;

			// Token: 0x040046A9 RID: 18089
			public static readonly string[] OutputFormats = InstantFormats.Date.IsoToClrOutputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046AA RID: 18090
			public static readonly string[] InputFormats = InstantFormats.Date.OutputFormats;

			// Token: 0x040046AB RID: 18091
			public static readonly string DefaultOutputFormat = InstantFormats.GetOutputFormat(InstantFormats.Date.OutputFormat.Default);

			// Token: 0x02001343 RID: 4931
			public enum OutputFormat
			{
				// Token: 0x040046AD RID: 18093
				Default,
				// Token: 0x040046AE RID: 18094
				Short
			}
		}

		// Token: 0x02001344 RID: 4932
		public static class Time
		{
			// Token: 0x040046AF RID: 18095
			public static readonly Dictionary<string, string> IsoToClrOutputFormatMap = new Dictionary<string, string>
			{
				{ "hh:mm:ss", "HH:mm:ss" },
				{ "hh:mm:ss.nnn", "HH:mm:ss.fff" },
				{ "hh:mm:ss.nnnnnnn", "HH:mm:ss.fffffff" },
				{ "h:mm tt", "h:mm tt" }
			};

			// Token: 0x040046B0 RID: 18096
			public static readonly Dictionary<string, string> IsoToClrInputFormatMap = new Dictionary<string, string>
			{
				{ "hh:mm", "HH:mm" },
				{ "hh:mm:ss", "HH:mm:ss" },
				{ "hh:mm:ss.nnnnnnn", "HH:mm:ss.FFFFFFF" }
			};

			// Token: 0x040046B1 RID: 18097
			public static readonly string[] InputFormats = InstantFormats.Time.IsoToClrInputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046B2 RID: 18098
			public static readonly string[] OutputFormats = InstantFormats.Time.IsoToClrOutputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046B3 RID: 18099
			public static readonly string DefaultOutputFormat = InstantFormats.GetOutputFormat(InstantFormats.Time.OutputFormat.Default);

			// Token: 0x02001345 RID: 4933
			public enum OutputFormat
			{
				// Token: 0x040046B5 RID: 18101
				Default,
				// Token: 0x040046B6 RID: 18102
				FractionalSeconds
			}
		}

		// Token: 0x02001346 RID: 4934
		public static class UtcOffset
		{
			// Token: 0x040046B7 RID: 18103
			public static readonly Dictionary<string, string> IsoToClrOutputFormatMap = new Dictionary<string, string>
			{
				{ "+hh:mm", "zzz" },
				{ "-hh:mm", "zzz" }
			};

			// Token: 0x040046B8 RID: 18104
			public static readonly Dictionary<string, string> IsoToClrInputFormatMap = InstantFormats.UtcOffset.IsoToClrOutputFormatMap;

			// Token: 0x040046B9 RID: 18105
			public static readonly string[] OutputFormats = InstantFormats.UtcOffset.IsoToClrOutputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046BA RID: 18106
			public static readonly string DefaultOutputFormat = InstantFormats.GetOutputFormat(InstantFormats.UtcOffset.OutputFormat.Positive);

			// Token: 0x02001347 RID: 4935
			public enum OutputFormat
			{
				// Token: 0x040046BC RID: 18108
				Positive,
				// Token: 0x040046BD RID: 18109
				Negative
			}
		}

		// Token: 0x02001348 RID: 4936
		public static class DateTime
		{
			// Token: 0x040046BE RID: 18110
			public static readonly Dictionary<string, string> IsoToClrOutputFormatMap = new Dictionary<string, string>
			{
				{ "YYYY-MM-DDThh:mm:ss", "yyyy-MM-dd\\THH:mm:ss" },
				{ "YYYYMMDDThh:mm:ss", "yyyyMMdd\\THH:mm:ss" },
				{ "YYYY-MM-DDThh:mm:ss.nnn", "yyyy-MM-dd\\THH:mm:ss.fff" },
				{ "YYYYMMDDThh:mm:ss.nnn", "yyyyMMdd\\THH:mm:ss.fff" },
				{ "YYYY-MM-DDThh:mm:ss.nnnnnnn", "yyyy-MM-dd\\THH:mm:ss.fffffff" },
				{ "YYYYMMDDThh:mm:ss.nnnnnnn", "yyyyMMdd\\THH:mm:ss.fffffff" },
				{ "yyyy'-'MM'-'dd'T'HH':'mm':'ss", "yyyy'-'MM'-'dd'T'HH':'mm':'ss" }
			};

			// Token: 0x040046BF RID: 18111
			public static readonly Dictionary<string, string> IsoToClrInputFormatMap = new Dictionary<string, string>
			{
				{ "YYYY-MM-DDThh:mm", "yyyy-MM-dd\\THH:mm" },
				{ "YYYYMMDDThh:mm", "yyyyMMdd\\THH:mm" },
				{ "YYYY-MM-DDThh:mm:ss", "yyyy-MM-dd\\THH:mm:ss" },
				{ "YYYYMMDDThh:mm:ss", "yyyyMMdd\\THH:mm:ss" },
				{ "YYYY-MM-DDThh:mm:ss.nnnnnnn", "yyyy-MM-dd\\THH:mm:ss.FFFFFFF" },
				{ "YYYYMMDDThh:mm:ss.nnnnnnn", "yyyyMMdd\\THH:mm:ss.FFFFFFF" }
			};

			// Token: 0x040046C0 RID: 18112
			public static readonly string[] InputFormats = InstantFormats.DateTime.IsoToClrInputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046C1 RID: 18113
			public static readonly string[] OutputFormats = InstantFormats.DateTime.IsoToClrOutputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046C2 RID: 18114
			public static readonly string DefaultOutputFormat = InstantFormats.GetOutputFormat(InstantFormats.DateTime.OutputFormat.Default);

			// Token: 0x02001349 RID: 4937
			public enum OutputFormat
			{
				// Token: 0x040046C4 RID: 18116
				Default,
				// Token: 0x040046C5 RID: 18117
				DefaultFractionalSeconds,
				// Token: 0x040046C6 RID: 18118
				Short,
				// Token: 0x040046C7 RID: 18119
				ShortFractionalSeconds
			}
		}

		// Token: 0x0200134A RID: 4938
		public static class DateTimeOffset
		{
			// Token: 0x040046C8 RID: 18120
			public static readonly Dictionary<string, string> IsoToClrOutputFormatMap = new Dictionary<string, string>
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
				{ "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", "yyyy'-'MM'-'dd HH':'mm':'ss zzz" }
			};

			// Token: 0x040046C9 RID: 18121
			public static readonly Dictionary<string, string> IsoToClrInputFormatMap = new Dictionary<string, string>
			{
				{ "YYYY-MM-DDThh:mm+hh:mm", "yyyy-MM-dd\\THH:mmzzz" },
				{ "YYYYMMDDThh:mm+hh:mm", "yyyyMMdd\\THH:mmzzz" },
				{ "YYYY-MM-DDThh:mm:ss+hh:mm", "yyyy-MM-dd\\THH:mm:sszzz" },
				{ "YYYYMMDDThh:mm:ss+hh:mm", "yyyyMMdd\\THH:mm:sszzz" },
				{ "YYYY-MM-DDThh:mm:ss.nnnnnnn+hh:mm", "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz" },
				{ "YYYYMMDDThh:mm:ss.nnnnnnn+hh:mm", "yyyyMMdd\\THH:mm:ss.FFFFFFFzzz" },
				{ "YYYY-MM-DDThh:mm-hh:mm", "yyyy-MM-dd\\THH:mmzzz" },
				{ "YYYYMMDDThh:mm-hh:mm", "yyyyMMdd\\THH:mmzzz" },
				{ "YYYY-MM-DDThh:mm:ss-hh:mm", "yyyy-MM-dd\\THH:mm:sszzz" },
				{ "YYYYMMDDThh:mm:ss-hh:mm", "yyyyMMdd\\THH:mm:sszzz" },
				{ "YYYY-MM-DDThh:mm:ss.nnnnnnn-hh:mm", "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz" },
				{ "YYYYMMDDThh:mm:ss.nnnnnnn-hh:mm", "yyyyMMdd\\THH:mm:ss.FFFFFFFzzz" },
				{ "YYYY-MM-DDThh:mmZ", "yyyy-MM-dd\\THH:mmK" },
				{ "YYYYMMDDThh:mmZ", "yyyyMMdd\\THH:mmK" },
				{ "YYYY-MM-DDThh:mm:ssZ", "yyyy-MM-dd\\THH:mm:ssK" },
				{ "YYYYMMDDThh:mm:ssZ", "yyyyMMdd\\THH:mm:ssK" },
				{ "YYYY-MM-DDThh:mm:ss.nnnnnnnZ", "yyyy-MM-dd\\THH:mm:ss.FFFFFFFK" },
				{ "YYYYMMDDThh:mm:ss.nnnnnnnZ", "yyyyMMdd\\THH:mm:ss.FFFFFFFK" }
			};

			// Token: 0x040046CA RID: 18122
			public static readonly string[] OutputFormats = InstantFormats.DateTimeOffset.IsoToClrOutputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046CB RID: 18123
			public static readonly string[] InputFormats = InstantFormats.DateTimeOffset.IsoToClrInputFormatMap.Keys.ToArray<string>();

			// Token: 0x040046CC RID: 18124
			public static readonly string DefaultOutputFormat = InstantFormats.GetOutputFormat(InstantFormats.DateTimeOffset.OutputFormat.Default);

			// Token: 0x0200134B RID: 4939
			public enum OutputFormat
			{
				// Token: 0x040046CE RID: 18126
				Default,
				// Token: 0x040046CF RID: 18127
				DefaultFractionalSeconds = 4
			}
		}
	}
}
