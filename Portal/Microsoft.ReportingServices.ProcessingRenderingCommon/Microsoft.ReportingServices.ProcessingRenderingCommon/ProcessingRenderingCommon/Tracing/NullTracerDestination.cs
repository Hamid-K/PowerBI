using System;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing
{
	// Token: 0x020000D3 RID: 211
	internal sealed class NullTracerDestination : IEngineTracerDestination
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x000135DB File Offset: 0x000117DB
		public void Log(EngineTracer.Level level, string message)
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000135DD File Offset: 0x000117DD
		public void Log(EngineTracer.Level level, string messageFormat, params object[] parameters)
		{
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000135DF File Offset: 0x000117DF
		public void Log(Exception exception, EngineTracer.Level level, string messageFormat, params object[] parameters)
		{
		}
	}
}
