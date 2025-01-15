using System;

namespace Microsoft.PowerBI.ReportServer.AsServer.Artifacts
{
	// Token: 0x02000034 RID: 52
	public class PbixModelRole
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005693 File Offset: 0x00003893
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000569B File Offset: 0x0000389B
		public long Id { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000056A4 File Offset: 0x000038A4
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000056AC File Offset: 0x000038AC
		public Guid ModelRoleId { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000056B5 File Offset: 0x000038B5
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000056BD File Offset: 0x000038BD
		public string ModelRoleName { get; set; }
	}
}
