using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002D RID: 45
	internal sealed class DataTransformTracer : ITracer
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00003D59 File Offset: 0x00001F59
		private DataTransformTracer()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003D61 File Offset: 0x00001F61
		public void Trace(TraceLevel level, string message)
		{
			DataTransformTracer.InternalTrace(level, message);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003D6A File Offset: 0x00001F6A
		public void Trace(TraceLevel level, string format, object arg0)
		{
			DataTransformTracer.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003D7E File Offset: 0x00001F7E
		public void Trace(TraceLevel level, string format, object arg0, object arg1)
		{
			DataTransformTracer.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003D94 File Offset: 0x00001F94
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2)
		{
			DataTransformTracer.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003DAC File Offset: 0x00001FAC
		public void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3)
		{
			DataTransformTracer.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2, arg3 }));
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public void Trace(TraceLevel level, string format, params string[] args)
		{
			DataTransformTracer.InternalTrace(level, string.Format(CultureInfo.InvariantCulture, format, args));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003DFC File Offset: 0x00001FFC
		private static void InternalTrace(TraceLevel level, string message)
		{
			TraceType traceType = TelemetryUtils.GetTraceType(level);
			TelemetryService.Instance.Log(new DataTransformHostTrace(traceType, message));
		}

		// Token: 0x040000CD RID: 205
		public static readonly DataTransformTracer Instance = new DataTransformTracer();
	}
}
