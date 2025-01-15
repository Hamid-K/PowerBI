using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Lucia
{
	// Token: 0x02000011 RID: 17
	public static class ExceptionExtensions
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000293C File Offset: 0x00000B3C
		public static bool IsFatal(this Exception ex)
		{
			if ((ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException || ex is TypeInitializationException)
			{
				return true;
			}
			if (ex is TargetInvocationException)
			{
				return ex.InnerException.IsFatal();
			}
			AggregateException ex2 = ex as AggregateException;
			if (ex2 != null)
			{
				for (int i = 0; i < ex2.InnerExceptions.Count; i++)
				{
					if (ex2.InnerExceptions[i].IsFatal())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
