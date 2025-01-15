using System;

namespace Microsoft.AnalysisServices.PlatformHost
{
	// Token: 0x02000031 RID: 49
	public static class ExceptionExtensions
	{
		// Token: 0x060020F8 RID: 8440 RVA: 0x0004DBD5 File Offset: 0x0004BDD5
		public static string ToString(this Exception ex, bool convertToCCON)
		{
			if (convertToCCON)
			{
				return "<ccon>" + ex.ToString() + "</ccon>";
			}
			return ex.ToString();
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0004DBF6 File Offset: 0x0004BDF6
		public static string GetMessage(this Exception ex, bool convertToCCON)
		{
			if (convertToCCON)
			{
				return "<ccon>" + ex.Message + "</ccon>";
			}
			return ex.Message;
		}

		// Token: 0x04000149 RID: 329
		private const string CCONStartTag = "<ccon>";

		// Token: 0x0400014A RID: 330
		private const string CCONEndTag = "</ccon>";
	}
}
