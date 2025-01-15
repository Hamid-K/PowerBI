using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000012 RID: 18
	internal static class ErrorUtils
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000035FC File Offset: 0x000017FC
		internal static bool IsStoppingException(Exception ex)
		{
			while (ex != null)
			{
				if ((ex is OutOfMemoryException && !(ex is InsufficientMemoryException)) || ex is ThreadAbortException || ex is AccessViolationException || ex is SEHException || ex is StackOverflowException)
				{
					return true;
				}
				ex = ex.InnerException;
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000364C File Offset: 0x0000184C
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

		// Token: 0x060000A2 RID: 162 RVA: 0x00003690 File Offset: 0x00001890
		public static List<Exception> GetInnermostExceptions(Exception e)
		{
			if (e == null)
			{
				return null;
			}
			List<Exception> list = null;
			Stack<Exception> stack = new Stack<Exception>();
			stack.Push(e);
			while (stack.Count > 0)
			{
				Exception ex = stack.Pop();
				AggregateException ex2 = ex as AggregateException;
				if (ex2 != null)
				{
					using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Exception ex3 = enumerator.Current;
							stack.Push(ex3);
						}
						goto IL_0072;
					}
					goto IL_005E;
				}
				goto IL_005E;
				IL_0072:
				if (ex.InnerException == null)
				{
					Util.AddToLazyList<Exception>(ref list, ex);
					continue;
				}
				continue;
				IL_005E:
				if (ex.InnerException != null)
				{
					stack.Push(ex.InnerException);
					goto IL_0072;
				}
				goto IL_0072;
			}
			return list;
		}
	}
}
