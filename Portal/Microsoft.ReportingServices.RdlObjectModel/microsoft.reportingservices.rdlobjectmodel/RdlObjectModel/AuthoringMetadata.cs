using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200007F RID: 127
	public class AuthoringMetadata
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00017E14 File Offset: 0x00016014
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00017E1C File Offset: 0x0001601C
		public AuthoringToolProperty CreatedBy { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00017E25 File Offset: 0x00016025
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00017E2D File Offset: 0x0001602D
		public AuthoringToolProperty UpdatedBy { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00017E36 File Offset: 0x00016036
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00017E3E File Offset: 0x0001603E
		public string LastModifiedTimestamp { get; set; }
	}
}
