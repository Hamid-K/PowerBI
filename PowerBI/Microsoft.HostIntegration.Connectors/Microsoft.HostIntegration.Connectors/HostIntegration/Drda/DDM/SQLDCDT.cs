using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008C3 RID: 2243
	public class SQLDCDT
	{
		// Token: 0x0600475D RID: 18269 RVA: 0x000FCDA4 File Offset: 0x000FAFA4
		internal SQLDCDT()
		{
			this.ListSqldcdtgrp = new List<SQLDCDTGRP>();
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x000FCDB7 File Offset: 0x000FAFB7
		// (set) Token: 0x0600475F RID: 18271 RVA: 0x000FCDBF File Offset: 0x000FAFBF
		public short SqlNum { get; set; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x000FCDC8 File Offset: 0x000FAFC8
		// (set) Token: 0x06004761 RID: 18273 RVA: 0x000FCDD0 File Offset: 0x000FAFD0
		public List<SQLDCDTGRP> ListSqldcdtgrp { get; private set; }
	}
}
