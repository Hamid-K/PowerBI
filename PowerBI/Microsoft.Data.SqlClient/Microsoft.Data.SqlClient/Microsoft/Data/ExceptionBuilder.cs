using System;
using System.Security;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data
{
	// Token: 0x02000009 RID: 9
	internal static class ExceptionBuilder
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x0000A938 File Offset: 0x00008B38
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<string>(trace, e.Message);
				try
				{
					SqlClientEventSource.Log.TryAdvancedTraceEvent<string>("<comm.ADP.TraceException|ERR|ADV> Environment StackTrace = '{0}'", Environment.StackTrace);
				}
				catch (SecurityException)
				{
				}
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000A984 File Offset: 0x00008B84
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> Message='{0}'", e);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000A994 File Offset: 0x00008B94
		internal static ArgumentException _Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000A9AF File Offset: 0x00008BAF
		internal static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument(StringsHelper.GetString(Strings.Data_InvalidOffsetLength, Array.Empty<object>()));
		}
	}
}
