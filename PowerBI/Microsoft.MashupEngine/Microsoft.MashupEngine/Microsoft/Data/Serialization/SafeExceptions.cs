using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000156 RID: 342
	internal static class SafeExceptions
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x000093D8 File Offset: 0x000075D8
		public static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}
	}
}
