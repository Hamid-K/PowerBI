using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000D RID: 13
	public class NameContainsRule : IEventTransmissionRule
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000029D0 File Offset: 0x00000BD0
		public NameContainsRule(string term)
		{
			this.Term = term;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000029DF File Offset: 0x00000BDF
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000029E7 File Offset: 0x00000BE7
		private string Term { get; set; }

		// Token: 0x0600003C RID: 60 RVA: 0x000029F0 File Offset: 0x00000BF0
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			return telemetryEvent.Name.Contains(this.Term);
		}
	}
}
