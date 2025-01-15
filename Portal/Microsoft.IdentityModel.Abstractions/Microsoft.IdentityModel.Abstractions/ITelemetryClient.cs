using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000004 RID: 4
	public interface ITelemetryClient
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		string ClientId { get; set; }

		// Token: 0x06000005 RID: 5
		void Initialize();

		// Token: 0x06000006 RID: 6
		bool IsEnabled();

		// Token: 0x06000007 RID: 7
		bool IsEnabled(string eventName);

		// Token: 0x06000008 RID: 8
		void TrackEvent(TelemetryEventDetails eventDetails);

		// Token: 0x06000009 RID: 9
		void TrackEvent(string eventName, IDictionary<string, string> stringProperties = null, IDictionary<string, long> longProperties = null, IDictionary<string, bool> boolProperties = null, IDictionary<string, DateTime> dateTimeProperties = null, IDictionary<string, double> doubleProperties = null, IDictionary<string, Guid> guidProperties = null);
	}
}
