using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006BA RID: 1722
	public class DdmReaderTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003828 RID: 14376 RVA: 0x000BCF36 File Offset: 0x000BB136
		public DdmReaderTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.DdmReader)
		{
		}

		// Token: 0x17000C6F RID: 3183
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
