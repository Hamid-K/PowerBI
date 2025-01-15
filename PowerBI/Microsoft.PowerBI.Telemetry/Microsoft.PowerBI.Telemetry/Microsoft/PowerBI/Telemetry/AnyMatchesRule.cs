using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000015 RID: 21
	public class AnyMatchesRule : IEventTransmissionRule
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002C30 File Offset: 0x00000E30
		public AnyMatchesRule(params IEventTransmissionRule[] rules)
		{
			this.Rules = rules;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C3F File Offset: 0x00000E3F
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002C47 File Offset: 0x00000E47
		private IEventTransmissionRule[] Rules { get; set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002C50 File Offset: 0x00000E50
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			IEventTransmissionRule[] rules = this.Rules;
			for (int i = 0; i < rules.Length; i++)
			{
				if (rules[i].ShouldAccept(telemetryEvent))
				{
					return true;
				}
			}
			return false;
		}
	}
}
