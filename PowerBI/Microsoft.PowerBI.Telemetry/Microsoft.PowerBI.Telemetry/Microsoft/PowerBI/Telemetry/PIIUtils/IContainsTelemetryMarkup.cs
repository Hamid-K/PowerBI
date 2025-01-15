using System;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x02000037 RID: 55
	public interface IContainsTelemetryMarkup
	{
		// Token: 0x06000136 RID: 310
		string ToCustomerContentString();

		// Token: 0x06000137 RID: 311
		string ToEUIIString();

		// Token: 0x06000138 RID: 312
		string ToEUPIString();

		// Token: 0x06000139 RID: 313
		string ToIPString();

		// Token: 0x0600013A RID: 314
		string ToOriginalString();
	}
}
