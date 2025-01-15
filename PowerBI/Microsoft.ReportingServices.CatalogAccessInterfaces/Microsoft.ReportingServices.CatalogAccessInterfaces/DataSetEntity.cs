using System;

namespace Microsoft.ReportingServices.CatalogAccess
{
	// Token: 0x02000006 RID: 6
	public class DataSetEntity
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000021DF File Offset: 0x000003DF
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000021E7 File Offset: 0x000003E7
		public Guid ID { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000021F0 File Offset: 0x000003F0
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000021F8 File Offset: 0x000003F8
		public Guid? LinkID { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002201 File Offset: 0x00000401
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002209 File Offset: 0x00000409
		public string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002212 File Offset: 0x00000412
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000221A File Offset: 0x0000041A
		public string Path { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002223 File Offset: 0x00000423
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000222B File Offset: 0x0000042B
		public byte[] NtSecDescPrimary { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002234 File Offset: 0x00000434
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000223C File Offset: 0x0000043C
		public Guid? Intermediate { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002245 File Offset: 0x00000445
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000224D File Offset: 0x0000044D
		public string Parameter { get; set; }
	}
}
