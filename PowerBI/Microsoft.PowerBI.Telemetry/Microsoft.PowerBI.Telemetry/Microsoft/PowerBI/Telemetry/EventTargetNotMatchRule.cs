using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000016 RID: 22
	public class EventTargetNotMatchRule : IEventTransmissionRule
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002C80 File Offset: 0x00000E80
		public EventTargetNotMatchRule(EventTarget target)
		{
			this.Target = target.ToString();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002C9B File Offset: 0x00000E9B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002CA3 File Offset: 0x00000EA3
		private string Target { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00002CAC File Offset: 0x00000EAC
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			string text;
			return !telemetryEvent.Properties.TryGetValue("EventTarget", out text) || text != this.Target;
		}
	}
}
