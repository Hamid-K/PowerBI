using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000124 RID: 292
	internal static class ExecTrace
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0002B84A File Offset: 0x00029A4A
		public static void TraceVerbose(string message)
		{
			ExecTrace._Trace(TraceLevel.Verbose, message);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002B853 File Offset: 0x00029A53
		public static void TraceWarning(string message)
		{
			ExecTrace._Trace(TraceLevel.Warning, message);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002B85C File Offset: 0x00029A5C
		public static void TraceWarning(string format, params object[] args)
		{
			ExecTrace._Trace(TraceLevel.Warning, format, args);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002B866 File Offset: 0x00029A66
		public static void TraceInfo(string message)
		{
			ExecTrace._Trace(TraceLevel.Info, message);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002B86F File Offset: 0x00029A6F
		private static void _Trace(TraceLevel level, string message)
		{
			ExecTrace.m_tracer.Trace(level, message);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002B87D File Offset: 0x00029A7D
		private static void _Trace(TraceLevel level, string format, params object[] args)
		{
			ExecTrace.m_tracer.Trace(level, format, args);
		}

		// Token: 0x040004BE RID: 1214
		private static readonly RSTrace m_tracer = RSTrace.CatalogTrace;
	}
}
