using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000026 RID: 38
	public sealed class TelemetrySettings
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00003C44 File Offset: 0x00001E44
		public TelemetrySettings(string clientActivityId = null)
		{
			this._clientActivityId = clientActivityId;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003C53 File Offset: 0x00001E53
		public string ClientActivityId
		{
			get
			{
				return this._clientActivityId;
			}
		}

		// Token: 0x04000095 RID: 149
		private readonly string _clientActivityId;
	}
}
