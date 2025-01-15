using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000A9 RID: 169
	internal class HeartbeatPropertyPayload
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000154B2 File Offset: 0x000136B2
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x000154BC File Offset: 0x000136BC
		public string PayloadValue
		{
			get
			{
				return this.payloadValue;
			}
			set
			{
				string text = value ?? string.Empty;
				if (!this.payloadValue.Equals(text, StringComparison.Ordinal))
				{
					this.IsUpdated = true;
					this.payloadValue = text;
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000154F1 File Offset: 0x000136F1
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x000154F9 File Offset: 0x000136F9
		public bool IsHealthy
		{
			get
			{
				return this.isHealthy;
			}
			set
			{
				this.IsUpdated = this.isHealthy != value;
				this.isHealthy = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00015514 File Offset: 0x00013714
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001551C File Offset: 0x0001371C
		public bool IsUpdated { get; set; }

		// Token: 0x04000208 RID: 520
		private string payloadValue = string.Empty;

		// Token: 0x04000209 RID: 521
		private bool isHealthy = true;
	}
}
