using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000131 RID: 305
	[OriginalName("ExtensionType")]
	public enum ExtensionType
	{
		// Token: 0x04000621 RID: 1569
		[OriginalName("Delivery")]
		Delivery,
		// Token: 0x04000622 RID: 1570
		[OriginalName("DeliveryUI")]
		DeliveryUI,
		// Token: 0x04000623 RID: 1571
		[OriginalName("Render")]
		Render,
		// Token: 0x04000624 RID: 1572
		[OriginalName("Data")]
		Data,
		// Token: 0x04000625 RID: 1573
		[OriginalName("All")]
		All
	}
}
