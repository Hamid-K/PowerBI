using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001D RID: 29
	public abstract class BaseTelemetryEvent : ITelemetryEvent
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002CDC File Offset: 0x00000EDC
		public BaseTelemetryEvent(string name, TelemetryUse use)
		{
			this.Name = name;
			this.Use = use;
			this.Time = DateTime.UtcNow;
			this.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002D21 File Offset: 0x00000F21
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002D29 File Offset: 0x00000F29
		public string Name { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002D32 File Offset: 0x00000F32
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002D3A File Offset: 0x00000F3A
		public TelemetryUse Use { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002D43 File Offset: 0x00000F43
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002D4B File Offset: 0x00000F4B
		public DateTime Time { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002D5C File Offset: 0x00000F5C
		public string Id { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008F RID: 143
		public abstract Dictionary<string, string> Properties { get; }
	}
}
