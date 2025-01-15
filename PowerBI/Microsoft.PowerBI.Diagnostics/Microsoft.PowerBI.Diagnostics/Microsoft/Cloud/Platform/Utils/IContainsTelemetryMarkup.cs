using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200000A RID: 10
	public interface IContainsTelemetryMarkup
	{
		// Token: 0x06000008 RID: 8
		string ToCustomerContentString();

		// Token: 0x06000009 RID: 9
		string ToEUIIString();

		// Token: 0x0600000A RID: 10
		string ToEUPIString();

		// Token: 0x0600000B RID: 11
		string ToIPString();

		// Token: 0x0600000C RID: 12
		string ToOriginalString();
	}
}
