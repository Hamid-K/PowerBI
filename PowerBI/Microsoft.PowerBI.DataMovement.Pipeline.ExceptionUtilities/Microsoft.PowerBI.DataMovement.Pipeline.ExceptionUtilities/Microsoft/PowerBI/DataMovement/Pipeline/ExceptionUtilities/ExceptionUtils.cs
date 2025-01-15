using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Microsoft.PowerBI.DataMovement.Pipeline.ExceptionUtilities
{
	// Token: 0x02000009 RID: 9
	internal static class ExceptionUtils
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002183 File Offset: 0x00000383
		internal static bool IsFatal(this Exception e)
		{
			return e is ThreadAbortException || e is OutOfMemoryException || e is StackOverflowException;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A0 File Offset: 0x000003A0
		internal static ExceptionDispatchInfo Unwrap(this AggregateException ex)
		{
			if (ex == null)
			{
				return null;
			}
			ReadOnlyCollection<Exception> innerExceptions = ex.InnerExceptions;
			if (innerExceptions == null)
			{
				return null;
			}
			if (innerExceptions.Count == 0)
			{
				return null;
			}
			Exception ex2 = innerExceptions.First<Exception>();
			AggregateException ex3 = ex2 as AggregateException;
			if (ex3 != null)
			{
				return ex3.Unwrap();
			}
			return ExceptionDispatchInfo.Capture(ex2);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		internal static T GetInnerException<T>(this Exception e) where T : Exception
		{
			AggregateException ex = e as AggregateException;
			if (ex != null)
			{
				foreach (Exception ex2 in ex.InnerExceptions)
				{
					T innerException = ex2.GetInnerException<T>();
					if (innerException != null)
					{
						return innerException;
					}
				}
				return default(T);
			}
			T t = e as T;
			while (t == null && e != null)
			{
				e = e.InnerException;
				t = e as T;
			}
			return t;
		}
	}
}
