using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000141 RID: 321
	[Obsolete("Telemetry is sent automatically by MSAL.NET. See https://aka.ms/msal-net-telemetry.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TraceTelemetryConfig : ITelemetryConfig
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0003A784 File Offset: 0x00038984
		public TraceTelemetryConfig()
		{
			this.SessionId = Guid.NewGuid().ToString();
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003A7B0 File Offset: 0x000389B0
		public TelemetryAudienceType AudienceType
		{
			get
			{
				return TelemetryAudienceType.PreProduction;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0003A7B3 File Offset: 0x000389B3
		public string SessionId { get; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0003A7BB File Offset: 0x000389BB
		public Action<ITelemetryEventPayload> DispatchAction
		{
			get
			{
				return delegate(ITelemetryEventPayload payload)
				{
					JObject jobject = new JObject();
					foreach (KeyValuePair<string, bool> keyValuePair in payload.BoolValues)
					{
						jobject[keyValuePair.Key] = keyValuePair.Value;
					}
					foreach (KeyValuePair<string, int> keyValuePair2 in payload.IntValues)
					{
						jobject[keyValuePair2.Key] = keyValuePair2.Value;
					}
					foreach (KeyValuePair<string, long> keyValuePair3 in payload.Int64Values)
					{
						jobject[keyValuePair3.Key] = keyValuePair3.Value;
					}
					foreach (KeyValuePair<string, string> keyValuePair4 in payload.StringValues)
					{
						jobject[keyValuePair4.Key] = keyValuePair4.Value;
					}
					Trace.TraceInformation(JsonHelper.JsonObjectToString(jobject));
					Trace.Flush();
				};
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0003A7DC File Offset: 0x000389DC
		public IEnumerable<string> AllowedScopes
		{
			get
			{
				return CollectionHelpers.GetEmptyReadOnlyList<string>();
			}
		}
	}
}
