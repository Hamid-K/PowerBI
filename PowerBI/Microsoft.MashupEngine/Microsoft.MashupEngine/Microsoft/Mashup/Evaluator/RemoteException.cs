using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D30 RID: 7472
	public static class RemoteException
	{
		// Token: 0x0600BA2C RID: 47660 RVA: 0x0025B4E0 File Offset: 0x002596E0
		public static Exception ToRemoteException(this Exception exception)
		{
			ErrorException ex = exception.ToEvaluationException() as ErrorException;
			string text;
			if (ex != null && !ex.IsExpected && RemoteException.TryGetExpectedRemoteException(ex, out text))
			{
				exception = new ErrorException(text, null, null, ex.IsRecoverable, true, ex);
			}
			return exception;
		}

		// Token: 0x0600BA2D RID: 47661 RVA: 0x0025B524 File Offset: 0x00259724
		private static bool TryGetExpectedRemoteException(ErrorException errorException, out string message)
		{
			if (errorException.IsExpected)
			{
				message = errorException.Message;
				return true;
			}
			if (errorException.ClassName != null && RemoteException.expectedRemoteExceptions.TryGetValue(errorException.ClassName, out message))
			{
				return true;
			}
			if (errorException.InnerException != null)
			{
				return RemoteException.TryGetExpectedRemoteException(errorException.InnerException, out message);
			}
			message = null;
			return false;
		}

		// Token: 0x04005EC1 RID: 24257
		private static readonly Dictionary<string, string> expectedRemoteExceptions = new Dictionary<string, string>
		{
			{
				typeof(OutOfMemoryException).FullName,
				Strings.Evaluation_OutOfMemory
			},
			{
				typeof(StackOverflowException).FullName,
				Strings.Evaluation_StackOverflow
			}
		};
	}
}
