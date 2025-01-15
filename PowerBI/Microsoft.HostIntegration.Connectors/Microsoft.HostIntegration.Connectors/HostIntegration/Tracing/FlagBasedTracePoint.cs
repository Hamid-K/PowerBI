using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200066C RID: 1644
	public abstract class FlagBasedTracePoint : BaseTracePoint, ITracePointFlags
	{
		// Token: 0x06003708 RID: 14088 RVA: 0x000B9A54 File Offset: 0x000B7C54
		protected FlagBasedTracePoint()
		{
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x000B9A5C File Offset: 0x000B7C5C
		public FlagBasedTracePoint(ITraceContainer traceContainer, int tracepointIdentifier)
			: base(traceContainer, tracepointIdentifier, false)
		{
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x000B9A67 File Offset: 0x000B7C67
		public FlagBasedTracePoint(BaseTracePoint parentTracePoint, int tracepointIdentifier)
			: base(parentTracePoint, tracepointIdentifier, false)
		{
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x000B9A72 File Offset: 0x000B7C72
		public virtual bool IsEnabled(TraceFlags traceFlags)
		{
			return (traceFlags & (TraceFlags)base.CurrentTraceValue) > TraceFlags.None;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x000B9A7F File Offset: 0x000B7C7F
		public void Trace(TraceFlags traceFlags, string message)
		{
			base.Trace((int)traceFlags, message);
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x000B9A89 File Offset: 0x000B7C89
		public void Trace(TraceFlags traceFlags, string[] messages)
		{
			base.Trace((int)traceFlags, messages);
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x000B9A93 File Offset: 0x000B7C93
		public void Trace(TraceFlags traceFlags, Exception e)
		{
			base.Trace((int)traceFlags, e);
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x000B9A9D File Offset: 0x000B7C9D
		public void Trace(TraceFlags traceFlags, byte[] arrayOfBytes)
		{
			base.Trace((int)traceFlags, arrayOfBytes);
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000B9AA7 File Offset: 0x000B7CA7
		public void Trace(TraceFlags traceFlags, byte[] arrayOfBytes, List<IgnoredTraceData> ignoredData)
		{
			base.Trace((int)traceFlags, arrayOfBytes, ignoredData);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000B9AB2 File Offset: 0x000B7CB2
		public void Trace(TraceFlags traceFlags, byte[] arrayOfBytes, int start, int length)
		{
			base.Trace((int)traceFlags, arrayOfBytes, start, length);
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x000B9ABF File Offset: 0x000B7CBF
		public void Trace(TraceFlags traceFlags, byte[] arrayOfBytes, int start, int length, List<IgnoredTraceData> ignoredData)
		{
			base.Trace((int)traceFlags, arrayOfBytes, start, length, ignoredData);
		}
	}
}
