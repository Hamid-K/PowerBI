using System;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Exceptions
{
	// Token: 0x02000003 RID: 3
	internal static class ExceptionUtils
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000038FC File Offset: 0x00001AFC
		private static T GetExceptionThroughInnerChain<T>(Exception e) where T : Exception
		{
			T t = e as T;
			while (t == null && e != null)
			{
				e = e.InnerException;
				t = e as T;
			}
			return t;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003938 File Offset: 0x00001B38
		public static T GetInnermostException<T>(Exception e) where T : Exception
		{
			if (e == null)
			{
				return default(T);
			}
			T t = default(T);
			do
			{
				T t2 = e as T;
				if (t2 != null)
				{
					t = t2;
				}
				e = e.InnerException;
			}
			while (e != null);
			return t;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000397C File Offset: 0x00001B7C
		public static T GetInnerException<T>(Exception e) where T : Exception
		{
			return ExceptionUtils.GetExceptionThroughInnerChain<T>(e);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003984 File Offset: 0x00001B84
		public static bool ContainsException(Exception e, Predicate<Exception> condition)
		{
			while (e != null)
			{
				if (condition(e))
				{
					return true;
				}
				e = e.InnerException;
			}
			return false;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000399F File Offset: 0x00001B9F
		internal static bool IsStoppingException(Exception e)
		{
			return ExceptionUtils.ContainsException(e, (Exception ex) => ex is OutOfMemoryException || ex is ThreadAbortException || ex is StackOverflowException || ex is EvaluationCopyExpiredException);
		}
	}
}
