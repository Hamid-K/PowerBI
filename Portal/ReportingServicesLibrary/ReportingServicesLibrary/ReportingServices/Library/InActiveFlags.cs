using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000239 RID: 569
	internal enum InActiveFlags
	{
		// Token: 0x04000758 RID: 1880
		Active,
		// Token: 0x04000759 RID: 1881
		DeliveryProviderRemoved,
		// Token: 0x0400075A RID: 1882
		SharedDataSourceRemoved,
		// Token: 0x0400075B RID: 1883
		MissingParameterValue = 4,
		// Token: 0x0400075C RID: 1884
		InvalidParameterValue = 8,
		// Token: 0x0400075D RID: 1885
		UnknownItemParameter = 16,
		// Token: 0x0400075E RID: 1886
		MissingExtensionEncryptedSettings = 32,
		// Token: 0x0400075F RID: 1887
		CachingNotEnabledOnItem = 64,
		// Token: 0x04000760 RID: 1888
		DisabledByUser = 128
	}
}
