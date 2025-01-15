using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.TI.Linq;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000753 RID: 1875
	internal class DeterminantInformation
	{
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000CB2F0 File Offset: 0x000C94F0
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x000CB2F8 File Offset: 0x000C94F8
		internal List<Determinant> Determinants { get; set; }

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000CB301 File Offset: 0x000C9501
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x000CB309 File Offset: 0x000C9509
		internal Dictionary<int, LinqHostEnvironment> IdToHes { get; set; }

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000CB312 File Offset: 0x000C9512
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000CB31A File Offset: 0x000C951A
		internal Dictionary<int, LEEndpoint> IdToEndpoints { get; set; }

		// Token: 0x06003B96 RID: 15254 RVA: 0x000CB323 File Offset: 0x000C9523
		internal DeterminantInformation()
		{
			this.Determinants = new List<Determinant>();
			this.IdToHes = new Dictionary<int, LinqHostEnvironment>();
			this.IdToEndpoints = new Dictionary<int, LEEndpoint>();
		}
	}
}
