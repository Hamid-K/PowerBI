using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	[Nullable(0)]
	internal class TelemetryPolicy : HttpPipelineSynchronousPolicy
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0000F4BB File Offset: 0x0000D6BB
		public TelemetryPolicy(TelemetryDetails telemetryDetails)
		{
			this._defaultHeader = telemetryDetails.ToString();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public override void OnSendingRequest(HttpMessage message)
		{
			object obj;
			if (message.TryGetProperty(typeof(UserAgentValueKey), out obj))
			{
				message.Request.Headers.Add(HttpHeader.Names.UserAgent, (string)obj);
				return;
			}
			message.Request.Headers.Add(HttpHeader.Names.UserAgent, this._defaultHeader);
		}

		// Token: 0x0400020F RID: 527
		private readonly string _defaultHeader;
	}
}
