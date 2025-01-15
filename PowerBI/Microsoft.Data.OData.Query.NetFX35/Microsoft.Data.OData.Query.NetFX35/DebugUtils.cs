using System;
using System.Diagnostics;

namespace Microsoft.Data.Experimental.OData
{
	// Token: 0x02000018 RID: 24
	internal static class DebugUtils
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003776 File Offset: 0x00001976
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003778 File Offset: 0x00001978
		[Conditional("DEBUG")]
		internal static void CheckNoExternalCallers(bool checkPublicMethods)
		{
		}
	}
}
