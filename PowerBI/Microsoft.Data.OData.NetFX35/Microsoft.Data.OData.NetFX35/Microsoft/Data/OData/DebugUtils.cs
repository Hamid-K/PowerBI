using System;
using System.Diagnostics;

namespace Microsoft.Data.OData
{
	// Token: 0x02000272 RID: 626
	internal static class DebugUtils
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x00049B39 File Offset: 0x00047D39
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers()
		{
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00049B3B File Offset: 0x00047D3B
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers(bool checkPublicMethods)
		{
		}
	}
}
