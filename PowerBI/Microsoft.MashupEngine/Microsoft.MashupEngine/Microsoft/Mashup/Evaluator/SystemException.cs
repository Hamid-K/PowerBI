using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C69 RID: 7273
	internal static class SystemException
	{
		// Token: 0x0600B533 RID: 46387 RVA: 0x0024C8E2 File Offset: 0x0024AAE2
		public static Exception CreateWin32SystemException(string functionName, string piiFreeMessage)
		{
			return SystemException.CreateSystemException(functionName, piiFreeMessage, new Win32Exception(Marshal.GetHRForLastWin32Error()));
		}

		// Token: 0x0600B534 RID: 46388 RVA: 0x0024C8F8 File Offset: 0x0024AAF8
		public static void LogWin32SystemError(string functionName, string piiFreeMessage)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace(functionName, null, TraceEventType.Warning, null))
			{
				hostTrace.Add(new Win32Exception(string.Format(CultureInfo.InvariantCulture, "{0} encountered error {1}", piiFreeMessage, Marshal.GetHRForLastWin32Error())), false);
			}
		}

		// Token: 0x0600B535 RID: 46389 RVA: 0x0024C954 File Offset: 0x0024AB54
		public static Exception CreateSystemException(string functionName, string piiFreeMessage, Exception innerException = null)
		{
			Exception ex2;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace(functionName, null, TraceEventType.Information, null))
			{
				Exception ex;
				if (innerException == null)
				{
					ex = new InvalidOperationException(piiFreeMessage);
				}
				else
				{
					ex = new InvalidOperationException(piiFreeMessage, innerException);
				}
				hostTrace.Add(ex, false);
				ex2 = ex;
			}
			return ex2;
		}

		// Token: 0x0600B536 RID: 46390 RVA: 0x0024C9A8 File Offset: 0x0024ABA8
		public static void LogSystemError(string functionName, string piiFreeMessage)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace(functionName, null, TraceEventType.Warning, null))
			{
				hostTrace.Add(new InvalidOperationException(piiFreeMessage), false);
			}
		}
	}
}
