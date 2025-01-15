using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008C4 RID: 2244
	public class SQLDCDTGRP
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x000FCDD9 File Offset: 0x000FAFD9
		// (set) Token: 0x06004763 RID: 18275 RVA: 0x000FCDE1 File Offset: 0x000FAFE1
		public short SqlPrecision { get; set; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x000FCDEA File Offset: 0x000FAFEA
		// (set) Token: 0x06004765 RID: 18277 RVA: 0x000FCDF2 File Offset: 0x000FAFF2
		public short SqlScale { get; set; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x000FCDFB File Offset: 0x000FAFFB
		// (set) Token: 0x06004767 RID: 18279 RVA: 0x000FCE03 File Offset: 0x000FB003
		public long SqlLength { get; set; }

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x000FCE0C File Offset: 0x000FB00C
		// (set) Token: 0x06004769 RID: 18281 RVA: 0x000FCE14 File Offset: 0x000FB014
		public short SqlType { get; set; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x000FCE1D File Offset: 0x000FB01D
		// (set) Token: 0x0600476B RID: 18283 RVA: 0x000FCE25 File Offset: 0x000FB025
		public short SqlCcsid { get; set; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x000FCE2E File Offset: 0x000FB02E
		// (set) Token: 0x0600476D RID: 18285 RVA: 0x000FCE36 File Offset: 0x000FB036
		public long SqlArrExtent { get; set; }

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x000FCE3F File Offset: 0x000FB03F
		// (set) Token: 0x0600476F RID: 18287 RVA: 0x000FCE47 File Offset: 0x000FB047
		public string SqlName { get; set; }

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000FCE50 File Offset: 0x000FB050
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x000FCE58 File Offset: 0x000FB058
		public SQLUDTGRP SqlUdtGrp { get; set; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x000FCE61 File Offset: 0x000FB061
		// (set) Token: 0x06004773 RID: 18291 RVA: 0x000FCE69 File Offset: 0x000FB069
		public SQLDCDT SqlDcdt { get; set; }
	}
}
