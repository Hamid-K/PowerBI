using System;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x0200008A RID: 138
	internal static class ExceptionExtensions
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x0000B949 File Offset: 0x00009B49
		public static void SetInvocationStackTrace(this Exception e, string stackTrace)
		{
			if (!string.IsNullOrEmpty(stackTrace))
			{
				e.Data[ExceptionExtensions.InvocationStackTraceKey] = stackTrace;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B964 File Offset: 0x00009B64
		public static string GetInvocationStackTrace(this Exception e)
		{
			object obj = e.Data[ExceptionExtensions.InvocationStackTraceKey];
			if (obj == null || !(obj is string))
			{
				return string.Empty;
			}
			return obj as string;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B99C File Offset: 0x00009B9C
		public static void SetInvocationStackTrace(this Exception e, ServiceError serviceError)
		{
			string text = serviceError.Message ?? string.Empty;
			text = text + Environment.NewLine + serviceError.StackTrace;
			e.SetInvocationStackTrace(text);
		}

		// Token: 0x040001AE RID: 430
		private static readonly string InvocationStackTraceKey = "InvocationStackTrace";
	}
}
