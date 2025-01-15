using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B2 RID: 1202
	[Serializable]
	public class RetryState
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x000839F0 File Offset: 0x00081BF0
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x000839F8 File Offset: 0x00081BF8
		public int RetryCount { get; set; }

		// Token: 0x060024D8 RID: 9432 RVA: 0x00083A01 File Offset: 0x00081C01
		public RetryState()
		{
			this.RetryCount = 0;
		}
	}
}
