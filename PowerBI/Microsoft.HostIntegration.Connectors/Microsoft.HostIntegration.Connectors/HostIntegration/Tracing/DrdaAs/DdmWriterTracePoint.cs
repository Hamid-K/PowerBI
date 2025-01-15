using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006BB RID: 1723
	public class DdmWriterTracePoint : DrdaAsTracePoint
	{
		// Token: 0x0600382A RID: 14378 RVA: 0x000BCF41 File Offset: 0x000BB141
		public DdmWriterTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DdmWriter)
		{
		}

		// Token: 0x17000C70 RID: 3184
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
