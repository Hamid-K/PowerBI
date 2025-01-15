using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000007 RID: 7
	public class NullTelemetryClient : ITelemetryClient
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020AB File Offset: 0x000002AB
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000020B3 File Offset: 0x000002B3
		public string ClientId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000020BC File Offset: 0x000002BC
		public static NullTelemetryClient Instance { get; } = new NullTelemetryClient();

		// Token: 0x06000019 RID: 25 RVA: 0x000020C3 File Offset: 0x000002C3
		private NullTelemetryClient()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000020CB File Offset: 0x000002CB
		public bool IsEnabled()
		{
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000020CE File Offset: 0x000002CE
		public bool IsEnabled(string eventName)
		{
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000020D1 File Offset: 0x000002D1
		public void Initialize()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020D3 File Offset: 0x000002D3
		public void TrackEvent(TelemetryEventDetails eventDetails)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000020D5 File Offset: 0x000002D5
		public void TrackEvent(string eventName, IDictionary<string, string> stringProperties = null, IDictionary<string, long> longProperties = null, IDictionary<string, bool> boolProperties = null, IDictionary<string, DateTime> dateTimeProperties = null, IDictionary<string, double> doubleProperties = null, IDictionary<string, Guid> guidProperties = null)
		{
		}
	}
}
