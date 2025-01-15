using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200066D RID: 1645
	public abstract class LevelBasedTracePoint : BaseTracePoint, ITracePointLevel
	{
		// Token: 0x06003713 RID: 14099 RVA: 0x000B9A54 File Offset: 0x000B7C54
		protected LevelBasedTracePoint()
		{
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x000B9ACE File Offset: 0x000B7CCE
		public LevelBasedTracePoint(ITraceContainer traceContainer, int tracepointIdentifier)
			: base(traceContainer, tracepointIdentifier, true)
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000B9AD9 File Offset: 0x000B7CD9
		public LevelBasedTracePoint(BaseTracePoint parentTracePoint, int tracepointIdentifier)
			: base(parentTracePoint, tracepointIdentifier, true)
		{
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
		public virtual bool IsEnabled(TraceLevel traceLevel)
		{
			return this.parentTraceContainer.ShouldTrace && traceLevel <= (TraceLevel)base.CurrentTraceValue;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000B9A7F File Offset: 0x000B7C7F
		public void Trace(TraceLevel traceFlags, string message)
		{
			base.Trace((int)traceFlags, message);
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000B9A89 File Offset: 0x000B7C89
		public void Trace(TraceLevel traceFlags, string[] messages)
		{
			base.Trace((int)traceFlags, messages);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000B9A93 File Offset: 0x000B7C93
		public void Trace(TraceLevel traceLevel, Exception e)
		{
			base.Trace((int)traceLevel, e);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000B9A9D File Offset: 0x000B7C9D
		public void Trace(TraceLevel traceLevel, byte[] arrayOfBytes)
		{
			base.Trace((int)traceLevel, arrayOfBytes);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000B9AA7 File Offset: 0x000B7CA7
		public void Trace(TraceLevel traceLevel, byte[] arrayOfBytes, List<IgnoredTraceData> ignoredData)
		{
			base.Trace((int)traceLevel, arrayOfBytes, ignoredData);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000B9AB2 File Offset: 0x000B7CB2
		public void Trace(TraceLevel traceLevel, byte[] arrayOfBytes, int start, int length)
		{
			base.Trace((int)traceLevel, arrayOfBytes, start, length);
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000B9ABF File Offset: 0x000B7CBF
		public void Trace(TraceLevel traceLevel, byte[] arrayOfBytes, int start, int length, List<IgnoredTraceData> ignoredData)
		{
			base.Trace((int)traceLevel, arrayOfBytes, start, length, ignoredData);
		}
	}
}
