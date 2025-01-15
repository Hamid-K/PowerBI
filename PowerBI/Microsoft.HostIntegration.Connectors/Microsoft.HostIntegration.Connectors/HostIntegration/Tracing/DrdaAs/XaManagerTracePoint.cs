using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006B8 RID: 1720
	public class XaManagerTracePoint : DrdaAsTracePoint
	{
		// Token: 0x06003824 RID: 14372 RVA: 0x000BCF20 File Offset: 0x000BB120
		public XaManagerTracePoint(SessionTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.XaManager)
		{
		}

		// Token: 0x17000C6D RID: 3181
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
