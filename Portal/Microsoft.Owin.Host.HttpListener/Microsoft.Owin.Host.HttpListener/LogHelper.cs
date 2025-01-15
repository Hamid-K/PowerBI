using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Owin.Host.HttpListener
{
	// Token: 0x02000005 RID: 5
	internal static class LogHelper
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002339 File Offset: 0x00000539
		internal static Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> CreateLogger(Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>> factory, Type type)
		{
			if (factory == null)
			{
				return null;
			}
			return factory(type.FullName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000234C File Offset: 0x0000054C
		internal static void LogInfo(Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> logger, string data)
		{
			if (logger != null)
			{
				logger(TraceEventType.Information, 0, data, null, LogHelper.LogState);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002361 File Offset: 0x00000561
		internal static void LogException(Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool> logger, string location, Exception exception)
		{
			if (logger != null)
			{
				logger(TraceEventType.Error, 0, location, exception, LogHelper.LogStateAndError);
			}
		}

		// Token: 0x04000033 RID: 51
		private static readonly Func<object, Exception, string> LogState = (object state, Exception error) => Convert.ToString(state, CultureInfo.CurrentCulture);

		// Token: 0x04000034 RID: 52
		private static readonly Func<object, Exception, string> LogStateAndError = (object state, Exception error) => string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", new object[] { state, error });
	}
}
