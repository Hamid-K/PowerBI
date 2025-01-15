using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C1E RID: 7198
	public static class SafeExceptions
	{
		// Token: 0x0600B3B0 RID: 46000 RVA: 0x00247E38 File Offset: 0x00246038
		public static bool IsSafeException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is AccessViolationException) && !(e is SEHException) && !typeof(SecurityException).IsAssignableFrom(e.GetType());
		}

		// Token: 0x0600B3B1 RID: 46001 RVA: 0x00247E87 File Offset: 0x00246087
		public static bool TraceIsSafeException(IHostTrace trace, Exception e)
		{
			if (!SafeExceptions.IsSafeException(e))
			{
				trace.Add(e, true);
				return false;
			}
			trace.Add(e, TraceEventType.Warning, true);
			return true;
		}

		// Token: 0x0600B3B2 RID: 46002 RVA: 0x00247EA8 File Offset: 0x002460A8
		public static void IgnoreSafeExceptions(IEngineHost host, IHostTrace trace, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.TraceIsSafeException(trace, ex))
				{
					throw;
				}
				host.LogIgnoredException(ex);
			}
		}

		// Token: 0x0600B3B3 RID: 46003 RVA: 0x00247EE4 File Offset: 0x002460E4
		public static void IgnoreSafeExceptions(IEngineHost host, string entryName, Action action)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, entryName, TraceEventType.Information, null))
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
					host.LogIgnoredException(ex);
				}
			}
		}
	}
}
