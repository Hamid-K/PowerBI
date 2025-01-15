using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BIServer.Telemetry.Interfaces;
using Microsoft.Owin;

namespace Microsoft.BIServer.Telemetry.Services
{
	// Token: 0x02000004 RID: 4
	public class TelemetrySimulator : ITelemetryService
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002265 File Offset: 0x00000465
		public static TelemetrySimulator Instance
		{
			get
			{
				return TelemetrySimulator._instance;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000226C File Offset: 0x0000046C
		public bool IsEnabled()
		{
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000226F File Offset: 0x0000046F
		public void FlushRequests()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002271 File Offset: 0x00000471
		public Task TrackOwinRequestAsync(OwinMiddleware next, IOwinContext context)
		{
			return new Task(delegate
			{
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000226F File Offset: 0x0000046F
		public void TrackEvent(string eventName, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226F File Offset: 0x0000046F
		public void TrackTrace(string trace)
		{
		}

		// Token: 0x0400002C RID: 44
		private static TelemetrySimulator _instance = new TelemetrySimulator();
	}
}
