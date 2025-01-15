using System;

namespace Microsoft.HostIntegration.Tracing.TiWip
{
	// Token: 0x020006ED RID: 1773
	public class StateMachineTracePoint : TiWipTracePoint
	{
		// Token: 0x0600388A RID: 14474 RVA: 0x000BDD6D File Offset: 0x000BBF6D
		public StateMachineTracePoint(TBGenTracePoint parentTracePoint)
			: base(parentTracePoint, TracePointIdentifiers.StateMachine)
		{
		}

		// Token: 0x17000C8C RID: 3212
		public object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
