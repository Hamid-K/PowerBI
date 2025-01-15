using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000012 RID: 18
	public class PropertyOrDefaultMatchesRule : IEventTransmissionRule
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B0A File Offset: 0x00000D0A
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002B12 File Offset: 0x00000D12
		private string PropertyName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B1B File Offset: 0x00000D1B
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002B23 File Offset: 0x00000D23
		private string Match { get; set; }

		// Token: 0x0600004F RID: 79 RVA: 0x00002B2C File Offset: 0x00000D2C
		public PropertyOrDefaultMatchesRule(string name, string match)
		{
			this.PropertyName = name;
			this.Match = match;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B44 File Offset: 0x00000D44
		public bool ShouldAccept(ITelemetryEvent telemetryEvent)
		{
			string text;
			return !telemetryEvent.Properties.TryGetValue(this.PropertyName, out text) || this.Match.Equals(text);
		}
	}
}
