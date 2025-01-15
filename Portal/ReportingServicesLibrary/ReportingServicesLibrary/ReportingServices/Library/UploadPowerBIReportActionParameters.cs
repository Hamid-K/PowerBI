using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200015E RID: 350
	internal class UploadPowerBIReportActionParameters : CreateItemActionParameters
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00030885 File Offset: 0x0002EA85
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0003088D File Offset: 0x0002EA8D
		internal Stream Original { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00030896 File Offset: 0x0002EA96
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0003089E File Offset: 0x0002EA9E
		internal Stream Pbix { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x000308A7 File Offset: 0x0002EAA7
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x000308AF File Offset: 0x0002EAAF
		internal Stream Model { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000308B8 File Offset: 0x0002EAB8
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x000308C0 File Offset: 0x0002EAC0
		internal string DataModelParameters { get; set; }

		// Token: 0x06000D40 RID: 3392 RVA: 0x000308C9 File Offset: 0x0002EAC9
		internal override void Validate()
		{
			if (base.ItemName == null)
			{
				throw new MissingParameterException("Name");
			}
			if (base.ParentPath == null)
			{
				throw new MissingParameterException("Parent");
			}
			if (this.Original == null)
			{
				throw new MissingParameterException("ExtendedContent: Original PBIX");
			}
		}
	}
}
