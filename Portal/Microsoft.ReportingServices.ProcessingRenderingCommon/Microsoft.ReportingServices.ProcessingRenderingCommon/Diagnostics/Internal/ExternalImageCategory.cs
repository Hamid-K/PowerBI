using System;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C3 RID: 195
	public sealed class ExternalImageCategory
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x00012A13 File Offset: 0x00010C13
		internal ExternalImageCategory()
		{
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00012A1B File Offset: 0x00010C1B
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x00012A23 File Offset: 0x00010C23
		public string Count { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00012A2C File Offset: 0x00010C2C
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00012A34 File Offset: 0x00010C34
		public string ByteCount { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00012A3D File Offset: 0x00010C3D
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00012A45 File Offset: 0x00010C45
		public string ResourceFetchTime { get; set; }
	}
}
