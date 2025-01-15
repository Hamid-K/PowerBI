using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000F RID: 15
	public class NameDoesntMatchRule : IEventTransmissionRule
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002A39 File Offset: 0x00000C39
		public NameDoesntMatchRule(HashSet<string> matches)
		{
			this.Matches = matches;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A50 File Offset: 0x00000C50
		private HashSet<string> Matches { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00002A59 File Offset: 0x00000C59
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			return !this.Matches.Contains(telemetryEvent.Name);
		}
	}
}
