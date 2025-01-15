using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000162 RID: 354
	internal class UpdatePowerBIReportActionParameters : UpdateItemContentActionParameters
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00030B15 File Offset: 0x0002ED15
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00030B1D File Offset: 0x0002ED1D
		internal Stream Original { get; set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00030B26 File Offset: 0x0002ED26
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x00030B2E File Offset: 0x0002ED2E
		internal Stream Pbix { get; set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00030B37 File Offset: 0x0002ED37
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x00030B3F File Offset: 0x0002ED3F
		internal Stream Model { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x00030B48 File Offset: 0x0002ED48
		// (set) Token: 0x06000D59 RID: 3417 RVA: 0x00030B50 File Offset: 0x0002ED50
		internal string DataModelParameters { get; set; }

		// Token: 0x06000D5A RID: 3418 RVA: 0x00030B59 File Offset: 0x0002ED59
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
			if (base.CatalogItemContent == null)
			{
				throw new MissingParameterException("CatalogItemContent");
			}
			if (this.Original == null)
			{
				throw new MissingParameterException("ExtendedContent: Original PBIX");
			}
		}
	}
}
