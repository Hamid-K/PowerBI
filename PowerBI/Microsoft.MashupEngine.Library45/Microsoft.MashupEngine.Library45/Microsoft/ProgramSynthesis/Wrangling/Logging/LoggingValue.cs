using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x0200017A RID: 378
	public static class LoggingValue
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00019834 File Offset: 0x00017A34
		public static string AsLoggingValue(this bool p)
		{
			if (!p)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x04000418 RID: 1048
		public const string True = "True";

		// Token: 0x04000419 RID: 1049
		public const string False = "False";

		// Token: 0x0400041A RID: 1050
		public const string OmitString = "<omitted-may-contain-PII>";
	}
}
