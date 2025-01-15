using System;
using System.Diagnostics;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D4 RID: 468
	internal class TraceWrapper
	{
		// Token: 0x0600146D RID: 5229 RVA: 0x00045669 File Offset: 0x00043869
		public static void WriteLine(string message)
		{
			Trace.WriteLine(message);
		}
	}
}
