using System;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008BE RID: 2238
	public class SQLDHGRP
	{
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06004704 RID: 18180 RVA: 0x000FCADA File Offset: 0x000FACDA
		// (set) Token: 0x06004705 RID: 18181 RVA: 0x000FCAE2 File Offset: 0x000FACE2
		public short SqldHold { get; set; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06004706 RID: 18182 RVA: 0x000FCAEB File Offset: 0x000FACEB
		// (set) Token: 0x06004707 RID: 18183 RVA: 0x000FCAF3 File Offset: 0x000FACF3
		public short SqldReturn { get; set; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x000FCAFC File Offset: 0x000FACFC
		// (set) Token: 0x06004709 RID: 18185 RVA: 0x000FCB04 File Offset: 0x000FAD04
		public short SqldScroll { get; set; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x0600470A RID: 18186 RVA: 0x000FCB0D File Offset: 0x000FAD0D
		// (set) Token: 0x0600470B RID: 18187 RVA: 0x000FCB15 File Offset: 0x000FAD15
		public short SqldSensitive { get; set; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x000FCB1E File Offset: 0x000FAD1E
		// (set) Token: 0x0600470D RID: 18189 RVA: 0x000FCB26 File Offset: 0x000FAD26
		public short SqldFCode { get; set; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x0600470E RID: 18190 RVA: 0x000FCB2F File Offset: 0x000FAD2F
		// (set) Token: 0x0600470F RID: 18191 RVA: 0x000FCB37 File Offset: 0x000FAD37
		public short SqldKeyType { get; set; }

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x000FCB40 File Offset: 0x000FAD40
		// (set) Token: 0x06004711 RID: 18193 RVA: 0x000FCB48 File Offset: 0x000FAD48
		public short SqldOptlck { get; set; }

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x000FCB51 File Offset: 0x000FAD51
		// (set) Token: 0x06004713 RID: 18195 RVA: 0x000FCB59 File Offset: 0x000FAD59
		public string SqldRdbnam { get; set; }

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06004714 RID: 18196 RVA: 0x000FCB62 File Offset: 0x000FAD62
		// (set) Token: 0x06004715 RID: 18197 RVA: 0x000FCB6A File Offset: 0x000FAD6A
		public string SqldSchema { get; set; }

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x000FCB73 File Offset: 0x000FAD73
		// (set) Token: 0x06004717 RID: 18199 RVA: 0x000FCB7B File Offset: 0x000FAD7B
		public string SqldModule { get; set; }
	}
}
