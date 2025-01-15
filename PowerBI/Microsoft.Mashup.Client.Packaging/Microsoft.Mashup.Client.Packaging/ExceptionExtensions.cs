using System;
using System.Reflection;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x02000006 RID: 6
	public static class ExceptionExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		public static int GetHResult(this Exception e)
		{
			object obj = null;
			try
			{
				obj = ExceptionExtensions.hResult.GetValue(e, 52, null, null, null);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is TargetException || ex is TargetParameterCountException || ex is MethodAccessException || ex is TargetInvocationException)
				{
					return 0;
				}
				throw;
			}
			if (obj != null && obj is int)
			{
				return (int)obj;
			}
			return 0;
		}

		// Token: 0x04000031 RID: 49
		public const string InvocationStackTraceKey = "InvocationStackTrace";

		// Token: 0x04000032 RID: 50
		private static readonly MethodInfo prepForRemoting = typeof(Exception).GetMethod("PrepForRemoting", 36);

		// Token: 0x04000033 RID: 51
		private static readonly PropertyInfo hResult = typeof(Exception).GetProperty("HResult", 52);
	}
}
