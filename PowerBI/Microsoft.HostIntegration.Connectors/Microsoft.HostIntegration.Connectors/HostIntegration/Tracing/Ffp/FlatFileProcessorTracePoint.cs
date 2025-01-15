using System;

namespace Microsoft.HostIntegration.Tracing.Ffp
{
	// Token: 0x0200067F RID: 1663
	public class FlatFileProcessorTracePoint : FfpTracePoint
	{
		// Token: 0x060037B2 RID: 14258 RVA: 0x000BC0B0 File Offset: 0x000BA2B0
		public FlatFileProcessorTracePoint(FlatFileProcessorTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C4B RID: 3147
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
