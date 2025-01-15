using System;

namespace Microsoft.HostIntegration.Tracing.HostFiles
{
	// Token: 0x0200069A RID: 1690
	public class HostFileClientTracePoint : HostFilesTracePoint
	{
		// Token: 0x060037E8 RID: 14312 RVA: 0x000BC937 File Offset: 0x000BAB37
		protected HostFileClientTracePoint()
		{
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000BC93F File Offset: 0x000BAB3F
		public HostFileClientTracePoint(HostFilesTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C5B RID: 3163
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
