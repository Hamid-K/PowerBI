using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000E RID: 14
	public class NameDoesNotContainRule : IEventTransmissionRule
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002A03 File Offset: 0x00000C03
		public NameDoesNotContainRule(string term)
		{
			this.Term = term;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A12 File Offset: 0x00000C12
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002A1A File Offset: 0x00000C1A
		private string Term { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002A23 File Offset: 0x00000C23
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			return !telemetryEvent.Name.Contains(this.Term);
		}
	}
}
