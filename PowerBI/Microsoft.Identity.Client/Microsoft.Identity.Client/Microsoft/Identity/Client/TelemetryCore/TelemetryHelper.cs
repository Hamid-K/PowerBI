using System;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.TelemetryCore
{
	// Token: 0x020001E2 RID: 482
	internal sealed class TelemetryHelper : IDisposable
	{
		// Token: 0x060014A6 RID: 5286 RVA: 0x00045C19 File Offset: 0x00043E19
		public TelemetryHelper(IHttpTelemetryManager httpTelemetryManager, ApiEvent eventBase)
		{
			this._httpTelemetryManager = httpTelemetryManager;
			this._eventToEnd = eventBase;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00045C2F File Offset: 0x00043E2F
		private void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					IHttpTelemetryManager httpTelemetryManager = this._httpTelemetryManager;
					if (httpTelemetryManager != null)
					{
						httpTelemetryManager.RecordStoppedEvent(this._eventToEnd);
					}
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00045C5A File Offset: 0x00043E5A
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000896 RID: 2198
		private readonly ApiEvent _eventToEnd;

		// Token: 0x04000897 RID: 2199
		private readonly IHttpTelemetryManager _httpTelemetryManager;

		// Token: 0x04000898 RID: 2200
		private bool _disposedValue;
	}
}
