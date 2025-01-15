using System;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x0200001C RID: 28
	internal class Constants
	{
		// Token: 0x04000080 RID: 128
		internal const string TelemetryServiceEndpoint = "https://dc.services.visualstudio.com/v2/track";

		// Token: 0x04000081 RID: 129
		internal const string ProfileQueryEndpoint = "https://dc.services.visualstudio.com/api/profiles/{0}/appId";

		// Token: 0x04000082 RID: 130
		internal const string TelemetryNamePrefix = "Microsoft.ApplicationInsights.";

		// Token: 0x04000083 RID: 131
		internal const string DevModeTelemetryNamePrefix = "Microsoft.ApplicationInsights.Dev.";

		// Token: 0x04000084 RID: 132
		internal const string EventNameForUnknownTelemetry = "ConvertedTelemetry";

		// Token: 0x04000085 RID: 133
		internal const int MaxExceptionCountToSave = 10;
	}
}
