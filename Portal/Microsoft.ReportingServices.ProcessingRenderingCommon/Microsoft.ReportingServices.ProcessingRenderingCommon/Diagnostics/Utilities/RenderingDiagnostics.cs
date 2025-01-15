using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AC RID: 172
	internal sealed class RenderingDiagnostics
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00010C0E File Offset: 0x0000EE0E
		public static bool Enabled
		{
			get
			{
				return RSTrace.RenderingTracer.TraceVerbose;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00010C1A File Offset: 0x0000EE1A
		public static void Trace(RenderingArea renderingArea, TraceLevel traceLevel, string message)
		{
			if (RenderingDiagnostics.Enabled)
			{
				RSTrace.RenderingTracer.Trace(TraceLevel.Verbose, message);
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00010C2F File Offset: 0x0000EE2F
		public static void Trace(RenderingArea renderingArea, TraceLevel traceLevel, string format, params object[] arg)
		{
			if (RenderingDiagnostics.Enabled)
			{
				RSTrace.RenderingTracer.Trace(TraceLevel.Verbose, format, arg);
			}
		}
	}
}
