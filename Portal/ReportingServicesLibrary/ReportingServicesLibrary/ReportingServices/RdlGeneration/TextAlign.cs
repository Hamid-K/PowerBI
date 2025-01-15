using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x0200037E RID: 894
	internal static class TextAlign
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000783D8 File Offset: 0x000765D8
		internal static IList<string> List
		{
			get
			{
				return Array.AsReadOnly<string>(TextAlign.m_list);
			}
		}

		// Token: 0x04000C6A RID: 3178
		internal const string General = "General";

		// Token: 0x04000C6B RID: 3179
		internal const string Left = "Left";

		// Token: 0x04000C6C RID: 3180
		internal const string Center = "Center";

		// Token: 0x04000C6D RID: 3181
		internal const string Right = "Right";

		// Token: 0x04000C6E RID: 3182
		private static string[] m_list = new string[] { "General", "Left", "Center", "Right" };
	}
}
