using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200012A RID: 298
	[OriginalName("KpiValueFormat")]
	public enum KpiValueFormat
	{
		// Token: 0x040005FD RID: 1533
		[OriginalName("General")]
		General,
		// Token: 0x040005FE RID: 1534
		[OriginalName("Abbreviated")]
		Abbreviated,
		// Token: 0x040005FF RID: 1535
		[OriginalName("DefaultCurrency")]
		DefaultCurrency,
		// Token: 0x04000600 RID: 1536
		[OriginalName("DefaultCurrencyWithDecimals")]
		DefaultCurrencyWithDecimals,
		// Token: 0x04000601 RID: 1537
		[OriginalName("AbbreviatedDefaultCurrency")]
		AbbreviatedDefaultCurrency,
		// Token: 0x04000602 RID: 1538
		[OriginalName("Percent")]
		Percent,
		// Token: 0x04000603 RID: 1539
		[OriginalName("PercentWithDecimals")]
		PercentWithDecimals
	}
}
