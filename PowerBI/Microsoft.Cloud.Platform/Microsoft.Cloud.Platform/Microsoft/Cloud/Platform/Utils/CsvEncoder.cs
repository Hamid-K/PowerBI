using System;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CD RID: 461
	public static class CsvEncoder
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x00028C2C File Offset: 0x00026E2C
		public static void Encode(StringBuilder sb, string value)
		{
			if (value == null)
			{
				return;
			}
			if (value.IndexOfAny(CsvEncoder.s_charactersToEscape) > -1)
			{
				sb.Append('"');
				sb.Append(value.Replace("\"", "\"\""));
				sb.Append('"');
				return;
			}
			sb.Append(value);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00028C7D File Offset: 0x00026E7D
		[CanBeNull]
		public static string Encode(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.IndexOfAny(CsvEncoder.s_charactersToEscape) > -1)
			{
				return "\"" + value.Replace("\"", "\"\"") + "\"";
			}
			return value;
		}

		// Token: 0x04000490 RID: 1168
		private static char[] s_charactersToEscape = "\",\n\r".ToCharArray();
	}
}
