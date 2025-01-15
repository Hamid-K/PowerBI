using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000014 RID: 20
	public class AllMatchesRule : IEventTransmissionRule
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002BDE File Offset: 0x00000DDE
		public AllMatchesRule(params IEventTransmissionRule[] rules)
		{
			this.Rules = rules;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002BED File Offset: 0x00000DED
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002BF5 File Offset: 0x00000DF5
		private IEventTransmissionRule[] Rules { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002C00 File Offset: 0x00000E00
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			IEventTransmissionRule[] rules = this.Rules;
			for (int i = 0; i < rules.Length; i++)
			{
				if (!rules[i].ShouldAccept(telemetryEvent))
				{
					return false;
				}
			}
			return true;
		}
	}
}
