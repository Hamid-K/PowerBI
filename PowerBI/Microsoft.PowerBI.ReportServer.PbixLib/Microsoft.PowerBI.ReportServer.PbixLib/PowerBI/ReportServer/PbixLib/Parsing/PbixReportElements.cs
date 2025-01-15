using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Packaging.Storage;

namespace Microsoft.PowerBI.ReportServer.PbixLib.Parsing
{
	// Token: 0x0200000C RID: 12
	public sealed class PbixReportElements
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A0C File Offset: 0x00000C0C
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A14 File Offset: 0x00000C14
		public string ModelVersion { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002A1D File Offset: 0x00000C1D
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002A25 File Offset: 0x00000C25
		public string AuthorVersion { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A2E File Offset: 0x00000C2E
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002A36 File Offset: 0x00000C36
		public byte[] DataModel { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A3F File Offset: 0x00000C3F
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002A47 File Offset: 0x00000C47
		public string ReportDocument { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A50 File Offset: 0x00000C50
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002A58 File Offset: 0x00000C58
		public string ReportMobileState { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A61 File Offset: 0x00000C61
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A69 File Offset: 0x00000C69
		public IList<PbixDataSource> DataSources { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002A72 File Offset: 0x00000C72
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002A7A File Offset: 0x00000C7A
		public IDictionary<string, byte[]> CustomVisuals { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002A83 File Offset: 0x00000C83
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A8B File Offset: 0x00000C8B
		public StaticResourceCollection StaticResources { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A94 File Offset: 0x00000C94
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002A9C File Offset: 0x00000C9C
		public ConnectionsSettings ConnectionsSettings { get; set; }
	}
}
