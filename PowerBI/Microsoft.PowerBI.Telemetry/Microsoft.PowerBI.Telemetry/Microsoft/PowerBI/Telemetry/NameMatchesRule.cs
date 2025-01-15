using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000C RID: 12
	public class NameMatchesRule : IEventTransmissionRule
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000299D File Offset: 0x00000B9D
		public NameMatchesRule(HashSet<string> matches)
		{
			this.Matches = matches;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000029AC File Offset: 0x00000BAC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000029B4 File Offset: 0x00000BB4
		private HashSet<string> Matches { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x000029BD File Offset: 0x00000BBD
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			return this.Matches.Contains(telemetryEvent.Name);
		}
	}
}
