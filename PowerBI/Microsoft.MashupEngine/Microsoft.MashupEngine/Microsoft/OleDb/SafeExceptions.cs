using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Microsoft.OleDb
{
	// Token: 0x02001F29 RID: 7977
	internal static class SafeExceptions
	{
		// Token: 0x0600C380 RID: 50048 RVA: 0x00272DA4 File Offset: 0x00270FA4
		public static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}
	}
}
