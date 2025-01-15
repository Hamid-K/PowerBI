using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006E1 RID: 1761
	public class ScriptTracePoint : SiTracePoint
	{
		// Token: 0x06003876 RID: 14454 RVA: 0x000BD8BF File Offset: 0x000BBABF
		public ScriptTracePoint(SiTraceContainer traceContainer)
			: base(traceContainer, TracePointIdentifiers.Script)
		{
		}

		// Token: 0x17000C85 RID: 3205
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
