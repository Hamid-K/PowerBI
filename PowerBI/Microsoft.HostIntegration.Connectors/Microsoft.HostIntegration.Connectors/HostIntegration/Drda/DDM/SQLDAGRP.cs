using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008BF RID: 2239
	public class SQLDAGRP
	{
		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x000FCB84 File Offset: 0x000FAD84
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x000FCB8C File Offset: 0x000FAD8C
		public short SqlPrecision { get; set; }

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x000FCB95 File Offset: 0x000FAD95
		// (set) Token: 0x0600471C RID: 18204 RVA: 0x000FCB9D File Offset: 0x000FAD9D
		public short SqlScale { get; set; }

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x0600471D RID: 18205 RVA: 0x000FCBA6 File Offset: 0x000FADA6
		// (set) Token: 0x0600471E RID: 18206 RVA: 0x000FCBAE File Offset: 0x000FADAE
		public long SqlLength { get; set; }

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x000FCBB7 File Offset: 0x000FADB7
		// (set) Token: 0x06004720 RID: 18208 RVA: 0x000FCBBF File Offset: 0x000FADBF
		public short SqlType { get; set; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x000FCBC8 File Offset: 0x000FADC8
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x000FCBD0 File Offset: 0x000FADD0
		public short SqlCcsid { get; set; }

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x000FCBD9 File Offset: 0x000FADD9
		// (set) Token: 0x06004724 RID: 18212 RVA: 0x000FCBE1 File Offset: 0x000FADE1
		public long SqlArrExtent { get; set; }

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x000FCBEA File Offset: 0x000FADEA
		// (set) Token: 0x06004726 RID: 18214 RVA: 0x000FCBF2 File Offset: 0x000FADF2
		public short SqlSuSize { get; set; }

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x000FCBFB File Offset: 0x000FADFB
		// (set) Token: 0x06004728 RID: 18216 RVA: 0x000FCC03 File Offset: 0x000FAE03
		public SQLDOPTGRP SqldOptGrp { get; set; }
	}
}
