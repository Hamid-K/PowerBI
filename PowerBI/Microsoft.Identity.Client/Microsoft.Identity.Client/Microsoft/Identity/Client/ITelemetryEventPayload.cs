using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200013D RID: 317
	[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface ITelemetryEventPayload
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000FEB RID: 4075
		string Name { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000FEC RID: 4076
		IReadOnlyDictionary<string, bool> BoolValues { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000FED RID: 4077
		IReadOnlyDictionary<string, long> Int64Values { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000FEE RID: 4078
		IReadOnlyDictionary<string, int> IntValues { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000FEF RID: 4079
		IReadOnlyDictionary<string, string> StringValues { get; }

		// Token: 0x06000FF0 RID: 4080
		string ToJsonString();
	}
}
