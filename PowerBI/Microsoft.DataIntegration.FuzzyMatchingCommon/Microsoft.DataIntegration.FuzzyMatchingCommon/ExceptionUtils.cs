using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000014 RID: 20
	public static class ExceptionUtils
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002453 File Offset: 0x00000653
		public static void Assert(Func<bool> predicate)
		{
			ExceptionUtils.Assert(predicate, delegate(Exception e)
			{
				throw e;
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000247A File Offset: 0x0000067A
		public static void Assert(Func<bool> predicate, Action<Exception> exceptionHandler)
		{
			if (!predicate.Invoke())
			{
				exceptionHandler.Invoke(new Exception(string.Format("Assert Failed: {0}", predicate.Method.GetMethodBody().ToString())));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000024A9 File Offset: 0x000006A9
		public static void ThrowIfNull(object obj, string variableName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(string.Format("{0} may not be null.", variableName));
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000024C0 File Offset: 0x000006C0
		public static T TryOrDefault<T>(Func<T> function)
		{
			T t;
			try
			{
				t = function.Invoke();
			}
			catch (Exception)
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000024F4 File Offset: 0x000006F4
		public static bool Try(Action action)
		{
			bool flag;
			try
			{
				action.Invoke();
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002524 File Offset: 0x00000724
		public static bool Try(Action action, Action<Exception> exceptionHandler)
		{
			bool flag;
			try
			{
				action.Invoke();
				flag = true;
			}
			catch (Exception ex)
			{
				exceptionHandler.Invoke(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002558 File Offset: 0x00000758
		public static T Retry<T>(Func<T> action, int numTries = 2)
		{
			return ExceptionUtils.Retry<T>(action, null, numTries);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002564 File Offset: 0x00000764
		public static T Retry<T>(Func<T> action, Func<Exception, Exception> exceptionHandler, int numTries = 2)
		{
			int num = 0;
			T t;
			for (;;)
			{
				try
				{
					t = action.Invoke();
				}
				catch (Exception ex)
				{
					num++;
					if (exceptionHandler != null)
					{
						ex = exceptionHandler.Invoke(ex);
					}
					if (num > numTries)
					{
						throw ex;
					}
					continue;
				}
				break;
			}
			return t;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000025A8 File Offset: 0x000007A8
		public static T Retry<T>(Func<T> action, Action<Exception> exceptionHandler, int numTries = 2)
		{
			int num = 0;
			T t;
			for (;;)
			{
				try
				{
					t = action.Invoke();
				}
				catch (Exception ex)
				{
					num++;
					if (exceptionHandler != null)
					{
						exceptionHandler.Invoke(ex);
					}
					if (num >= numTries)
					{
						throw ex;
					}
					continue;
				}
				break;
			}
			return t;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000025EC File Offset: 0x000007EC
		public static void Retry(Action action, int numTries = 2)
		{
			ExceptionUtils.Retry(action, null, numTries);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000025F8 File Offset: 0x000007F8
		public static void Retry(Action action, Action<Exception> exceptionHandler, int numTries = 2)
		{
			int num = 0;
			for (;;)
			{
				try
				{
					action.Invoke();
				}
				catch (Exception ex)
				{
					num++;
					if (exceptionHandler != null)
					{
						exceptionHandler.Invoke(ex);
					}
					if (num >= numTries)
					{
						throw ex;
					}
					continue;
				}
				break;
			}
		}
	}
}
