using System;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x0200000B RID: 11
	internal static class LogMessages
	{
		// Token: 0x04000033 RID: 51
		internal const string MIML10000 = "MIML10000: eventData.Payload is null or empty. Not logging any messages.";

		// Token: 0x04000034 RID: 52
		internal const string MIML10001 = "MIML10001: Cannot create the fileStream or StreamWriter to write logs. See inner exception.";

		// Token: 0x04000035 RID: 53
		internal const string MIML10002 = "MIML10002: Unknown log level: {0}.";

		// Token: 0x04000036 RID: 54
		internal const string MIML10003 = "MIML10003: Sku and version telemetry cannot be manipulated. They are added by default.";
	}
}
