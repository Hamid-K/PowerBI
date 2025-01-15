using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000018 RID: 24
	internal static class AsynchronousExceptionDetection
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000375C File Offset: 0x0000195C
		internal static bool IsStoppingException(Exception ex)
		{
			return (ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003791 File Offset: 0x00001991
		internal static bool IsStoppingExceptionWithUnwrap(Exception e, out Exception unwrapped)
		{
			unwrapped = AsynchronousExceptionDetection.UnwrapException(e);
			return AsynchronousExceptionDetection.IsStoppingException(e);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000037A4 File Offset: 0x000019A4
		private static Exception UnwrapException(Exception exception)
		{
			TaskSchedulerException ex = exception as TaskSchedulerException;
			if (ex != null && ex.InnerException != null)
			{
				return AsynchronousExceptionDetection.UnwrapException(ex.InnerException);
			}
			return exception;
		}
	}
}
