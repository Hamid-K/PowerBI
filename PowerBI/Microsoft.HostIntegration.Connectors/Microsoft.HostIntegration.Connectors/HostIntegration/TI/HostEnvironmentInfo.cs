using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x0200072B RID: 1835
	public struct HostEnvironmentInfo
	{
		// Token: 0x040022CA RID: 8906
		public string HostEnvironmentName;

		// Token: 0x040022CB RID: 8907
		public string HostEnvironmentPlatformName;

		// Token: 0x040022CC RID: 8908
		public string VendorName;

		// Token: 0x040022CD RID: 8909
		public Guid VendorID;

		// Token: 0x040022CE RID: 8910
		public string PrimitiveConverterClassId;

		// Token: 0x040022CF RID: 8911
		public HostLanguage[] HostLanguages;
	}
}
