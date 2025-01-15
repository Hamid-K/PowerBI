using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Logging
{
	// Token: 0x02000176 RID: 374
	public static class LoggerUtils
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x00019807 File Offset: 0x00017A07
		public static bool IsPIISafeException(this Exception ex)
		{
			return ex is ArgumentException || ex is ArgumentNullException || ex is NullReferenceException || ex is ArgumentOutOfRangeException || ex is InvalidCastException;
		}
	}
}
