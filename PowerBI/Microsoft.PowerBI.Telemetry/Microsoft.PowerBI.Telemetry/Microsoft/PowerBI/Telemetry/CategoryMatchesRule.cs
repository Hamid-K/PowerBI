using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000010 RID: 16
	public class CategoryMatchesRule : IEventTransmissionRule
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002A6F File Offset: 0x00000C6F
		public CategoryMatchesRule(params TelemetryUse[] use)
		{
			this.Use = use;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A7E File Offset: 0x00000C7E
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A86 File Offset: 0x00000C86
		private TelemetryUse[] Use { get; set; }

		// Token: 0x06000048 RID: 72 RVA: 0x00002A90 File Offset: 0x00000C90
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			foreach (TelemetryUse telemetryUse in this.Use)
			{
				if (telemetryEvent.Use == telemetryUse)
				{
					return true;
				}
			}
			return false;
		}
	}
}
