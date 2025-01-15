using System;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000646 RID: 1606
	internal static class OdbcSearchPattern
	{
		// Token: 0x06003309 RID: 13065 RVA: 0x000A3895 File Offset: 0x000A1A95
		public static string EscapeSearchCharacters(string escapeCharacter, string str)
		{
			if (str == null || string.IsNullOrEmpty(escapeCharacter))
			{
				return str;
			}
			return str.Replace("_", escapeCharacter + "_").Replace("%", escapeCharacter + "%");
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000A38D0 File Offset: 0x000A1AD0
		public static string UnescapeSearchCharacters(string escapeCharacter, string str)
		{
			if (str == null || string.IsNullOrEmpty(escapeCharacter) || !str.Contains(escapeCharacter))
			{
				return str;
			}
			return str.Replace(escapeCharacter + "_", "_").Replace(escapeCharacter + "%", "%");
		}

		// Token: 0x040016B6 RID: 5814
		public const string Underscore = "_";

		// Token: 0x040016B7 RID: 5815
		public const string Percent = "%";
	}
}
