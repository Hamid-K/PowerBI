using System;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000032 RID: 50
	public interface IContainsTelemetryMarkup
	{
		// Token: 0x0600023E RID: 574
		string ToCustomerContentString();

		// Token: 0x0600023F RID: 575
		string ToEUIIString();

		// Token: 0x06000240 RID: 576
		string ToEUPIString();

		// Token: 0x06000241 RID: 577
		string ToIPString();

		// Token: 0x06000242 RID: 578
		string ToOriginalString();
	}
}
