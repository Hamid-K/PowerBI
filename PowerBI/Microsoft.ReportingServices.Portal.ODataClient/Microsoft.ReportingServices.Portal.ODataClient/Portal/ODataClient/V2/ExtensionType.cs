using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x020000A1 RID: 161
	[OriginalName("ExtensionType")]
	public enum ExtensionType
	{
		// Token: 0x04000351 RID: 849
		[OriginalName("Delivery")]
		Delivery,
		// Token: 0x04000352 RID: 850
		[OriginalName("DeliveryUI")]
		DeliveryUI,
		// Token: 0x04000353 RID: 851
		[OriginalName("Render")]
		Render,
		// Token: 0x04000354 RID: 852
		[OriginalName("Data")]
		Data,
		// Token: 0x04000355 RID: 853
		[OriginalName("All")]
		All
	}
}
