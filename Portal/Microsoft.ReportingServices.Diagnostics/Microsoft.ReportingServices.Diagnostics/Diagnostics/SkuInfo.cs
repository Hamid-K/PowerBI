using System;
using Microsoft.ReportingServices.Editions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000068 RID: 104
	internal sealed class SkuInfo
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public SkuInfo(SkuType sku, bool isExpired)
		{
			this.Sku = sku;
			this.IsExpired = isExpired;
		}

		// Token: 0x04000335 RID: 821
		public readonly SkuType Sku;

		// Token: 0x04000336 RID: 822
		public readonly bool IsExpired;
	}
}
