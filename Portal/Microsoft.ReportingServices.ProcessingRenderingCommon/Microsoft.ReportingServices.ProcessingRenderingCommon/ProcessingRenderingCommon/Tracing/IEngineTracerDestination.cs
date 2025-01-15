using System;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing
{
	// Token: 0x020000D2 RID: 210
	public interface IEngineTracerDestination
	{
		// Token: 0x06000726 RID: 1830
		void Log(EngineTracer.Level level, string message);

		// Token: 0x06000727 RID: 1831
		void Log(EngineTracer.Level level, string messageFormat, params object[] parameters);

		// Token: 0x06000728 RID: 1832
		void Log(Exception exception, EngineTracer.Level level, string messageFormat, params object[] parameters);
	}
}
