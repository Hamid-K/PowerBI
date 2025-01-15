using System;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x0200000B RID: 11
	internal class SkuCanUpgradeTo : Attribute
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000281E File Offset: 0x00000A1E
		public SkuCanUpgradeTo()
		{
			this.SupportsUpgradeTo = new SkuType[0];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002832 File Offset: 0x00000A32
		public SkuCanUpgradeTo(SkuType[] supportsUpgradeTo)
		{
			this.SupportsUpgradeTo = supportsUpgradeTo;
		}

		// Token: 0x04000037 RID: 55
		public SkuType[] SupportsUpgradeTo;
	}
}
