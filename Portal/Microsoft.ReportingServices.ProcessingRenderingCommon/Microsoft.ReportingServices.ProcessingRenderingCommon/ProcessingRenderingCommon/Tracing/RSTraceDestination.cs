using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing
{
	// Token: 0x020000D4 RID: 212
	internal sealed class RSTraceDestination : IEngineTracerDestination
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x000135E9 File Offset: 0x000117E9
		public RSTraceDestination(RSTrace rsTrace)
		{
			this._rsTrace = rsTrace;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000135F8 File Offset: 0x000117F8
		public void Log(EngineTracer.Level level, string message)
		{
			this._rsTrace.Trace(this.MapTrace(level), message);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001360D File Offset: 0x0001180D
		public void Log(EngineTracer.Level level, string messageFormat, params object[] parameters)
		{
			this._rsTrace.Trace(this.MapTrace(level), messageFormat, parameters);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00013623 File Offset: 0x00011823
		public void Log(Exception exception, EngineTracer.Level level, string messageFormat, params object[] parameters)
		{
			this._rsTrace.Trace(this.MapTrace(level), messageFormat, new object[] { parameters, exception });
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00013648 File Offset: 0x00011848
		private TraceLevel MapTrace(EngineTracer.Level level)
		{
			switch (level)
			{
			case EngineTracer.Level.Trace:
			case EngineTracer.Level.Debug:
				return TraceLevel.Verbose;
			case EngineTracer.Level.Info:
				return TraceLevel.Info;
			case EngineTracer.Level.Warn:
				return TraceLevel.Warning;
			case EngineTracer.Level.Error:
			case EngineTracer.Level.Fatal:
				return TraceLevel.Error;
			case EngineTracer.Level.Off:
				return TraceLevel.Off;
			default:
				throw new Exception(string.Format("Unhandled EngineTracer.Level of {0}", level));
			}
		}

		// Token: 0x04000437 RID: 1079
		private readonly RSTrace _rsTrace;
	}
}
