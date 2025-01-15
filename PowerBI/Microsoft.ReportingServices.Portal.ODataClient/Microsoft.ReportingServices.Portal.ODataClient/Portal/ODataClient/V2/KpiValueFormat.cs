using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200009C RID: 156
	[OriginalName("KpiValueFormat")]
	public enum KpiValueFormat
	{
		// Token: 0x04000337 RID: 823
		[OriginalName("General")]
		General,
		// Token: 0x04000338 RID: 824
		[OriginalName("Abbreviated")]
		Abbreviated,
		// Token: 0x04000339 RID: 825
		[OriginalName("DefaultCurrency")]
		DefaultCurrency,
		// Token: 0x0400033A RID: 826
		[OriginalName("DefaultCurrencyWithDecimals")]
		DefaultCurrencyWithDecimals,
		// Token: 0x0400033B RID: 827
		[OriginalName("AbbreviatedDefaultCurrency")]
		AbbreviatedDefaultCurrency,
		// Token: 0x0400033C RID: 828
		[OriginalName("Percent")]
		Percent,
		// Token: 0x0400033D RID: 829
		[OriginalName("PercentWithDecimals")]
		PercentWithDecimals
	}
}
