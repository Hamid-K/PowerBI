using System;
using System.Threading;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020000D2 RID: 210
	internal class AsynchronousExceptionDetection
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x000082EF File Offset: 0x000064EF
		private AsynchronousExceptionDetection()
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000082F7 File Offset: 0x000064F7
		internal static bool IsStoppingException(Exception e)
		{
			return e is OutOfMemoryException || e is StackOverflowException || e is ThreadAbortException;
		}
	}
}
