using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000022 RID: 34
	internal static class SafeExceptions
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00005438 File Offset: 0x00003638
		internal static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005487 File Offset: 0x00003687
		internal static bool TraceIsSafeException(Exception e)
		{
			return SafeExceptions.IsSafeException(e);
		}
	}
}
