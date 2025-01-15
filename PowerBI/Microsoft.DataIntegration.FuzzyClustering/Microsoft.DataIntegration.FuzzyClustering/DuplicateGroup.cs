using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000006 RID: 6
	public class DuplicateGroup
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000232D File Offset: 0x0000052D
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002335 File Offset: 0x00000535
		public int RepresentativeId { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000233E File Offset: 0x0000053E
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002346 File Offset: 0x00000546
		public Dictionary<int, float> Duplicates { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x0000234F File Offset: 0x0000054F
		public DuplicateGroup(int representativeId)
		{
			this.RepresentativeId = representativeId;
			this.Duplicates = new Dictionary<int, float>();
			this.Duplicates[representativeId] = 1f;
		}
	}
}
