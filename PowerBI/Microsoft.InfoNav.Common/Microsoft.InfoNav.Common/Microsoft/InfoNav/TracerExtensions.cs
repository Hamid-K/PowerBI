using System;
using System.Diagnostics;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001D RID: 29
	public static class TracerExtensions
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000058CB File Offset: 0x00003ACB
		public static void Trace(this ITracer tracer, TraceLevel level, string message)
		{
			switch (level)
			{
			case TraceLevel.Error:
				tracer.TraceError(message);
				return;
			case TraceLevel.Warning:
				tracer.TraceWarning(message);
				return;
			case TraceLevel.Info:
				tracer.TraceInformation(message);
				return;
			}
			tracer.TraceVerbose(message);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005906 File Offset: 0x00003B06
		public static void Trace(this ITracer tracer, TraceLevel level, string format, object arg0)
		{
			switch (level)
			{
			case TraceLevel.Error:
				tracer.TraceError(format, arg0);
				return;
			case TraceLevel.Warning:
				tracer.TraceWarning(format, arg0);
				return;
			case TraceLevel.Info:
				tracer.TraceInformation(format, arg0);
				return;
			}
			tracer.TraceVerbose(format, arg0);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005948 File Offset: 0x00003B48
		public static void Trace(this ITracer tracer, TraceLevel level, string format, object arg0, object arg1)
		{
			switch (level)
			{
			case TraceLevel.Error:
				tracer.TraceError(format, arg0, arg1);
				return;
			case TraceLevel.Warning:
				tracer.TraceWarning(format, arg0, arg1);
				return;
			case TraceLevel.Info:
				tracer.TraceInformation(format, arg0, arg1);
				return;
			}
			tracer.TraceVerbose(format, arg0, arg1);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000599C File Offset: 0x00003B9C
		public static void Trace(this ITracer tracer, TraceLevel level, string format, object arg0, object arg1, object arg2)
		{
			switch (level)
			{
			case TraceLevel.Error:
				tracer.TraceError(format, arg0, arg1, arg2);
				return;
			case TraceLevel.Warning:
				tracer.TraceWarning(format, arg0, arg1, arg2);
				return;
			case TraceLevel.Info:
				tracer.TraceInformation(format, arg0, arg1, arg2);
				return;
			}
			tracer.TraceVerbose(format, arg0, arg1, arg2);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000059F8 File Offset: 0x00003BF8
		public static void Trace(this ITracer tracer, TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3)
		{
			switch (level)
			{
			case TraceLevel.Error:
				tracer.TraceError(format, arg0, arg1, arg2, arg3);
				return;
			case TraceLevel.Warning:
				tracer.TraceWarning(format, arg0, arg1, arg2, arg3);
				return;
			case TraceLevel.Info:
				tracer.TraceInformation(format, arg0, arg1, arg2, arg3);
				return;
			}
			tracer.TraceVerbose(format, arg0, arg1, arg2, arg3);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005A5A File Offset: 0x00003C5A
		public static void SanitizedTraceError(this ITracer tracer, string format, params string[] args)
		{
			tracer.SanitizedTrace(TraceLevel.Error, format, args);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005A65 File Offset: 0x00003C65
		public static void SanitizedTraceWarning(this ITracer tracer, string format, params string[] args)
		{
			tracer.SanitizedTrace(TraceLevel.Warning, format, args);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005A70 File Offset: 0x00003C70
		public static void SanitizedTraceInformation(this ITracer tracer, string format, params string[] args)
		{
			tracer.SanitizedTrace(TraceLevel.Info, format, args);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005A7B File Offset: 0x00003C7B
		public static void SanitizedTraceVerbose(this ITracer tracer, string format, params string[] args)
		{
			tracer.SanitizedTrace(TraceLevel.Verbose, format, args);
		}
	}
}
