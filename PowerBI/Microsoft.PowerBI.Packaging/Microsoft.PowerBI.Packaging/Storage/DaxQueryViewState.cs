using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000046 RID: 70
	public class DaxQueryViewState
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000070D1 File Offset: 0x000052D1
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000070D9 File Offset: 0x000052D9
		public IReadOnlyList<DaxQueryDocument> Queries { get; set; } = new List<DaxQueryDocument>();

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000070E2 File Offset: 0x000052E2
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000070EA File Offset: 0x000052EA
		public string Settings { get; set; }
	}
}
