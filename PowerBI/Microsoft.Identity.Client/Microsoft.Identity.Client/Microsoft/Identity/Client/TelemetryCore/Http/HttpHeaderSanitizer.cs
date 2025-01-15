using System;
using System.Text;

namespace Microsoft.Identity.Client.TelemetryCore.Http
{
	// Token: 0x020001E9 RID: 489
	internal class HttpHeaderSanitizer
	{
		// Token: 0x060014E6 RID: 5350 RVA: 0x00045F88 File Offset: 0x00044188
		public static string SanitizeHeader(string value)
		{
			string text = value;
			if (HttpHeaderSanitizer.HeaderValueNeedsEncoding(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in value)
				{
					if (c < ' ' && c != '\t')
					{
						stringBuilder.Append(HttpHeaderSanitizer.s_headerEncodingTable[(int)c]);
					}
					else if (c == '\u007f')
					{
						stringBuilder.Append("%7f");
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00046004 File Offset: 0x00044204
		private static bool HeaderValueNeedsEncoding(string value)
		{
			foreach (char c in value)
			{
				if ((c < ' ' && c != '\t') || c == '\u007f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040008AD RID: 2221
		private static readonly string[] s_headerEncodingTable = new string[]
		{
			"%00", "%01", "%02", "%03", "%04", "%05", "%06", "%07", "%08", "%09",
			"%0a", "%0b", "%0c", "%0d", "%0e", "%0f", "%10", "%11", "%12", "%13",
			"%14", "%15", "%16", "%17", "%18", "%19", "%1a", "%1b", "%1c", "%1d",
			"%1e", "%1f"
		};
	}
}
