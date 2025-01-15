using System;

namespace Microsoft.Identity.Client.ApiConfig.Parameters
{
	// Token: 0x020002D0 RID: 720
	internal abstract class AbstractAcquireTokenConfidentialClientParameters
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x00056EAD File Offset: 0x000550AD
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x00056EB5 File Offset: 0x000550B5
		public bool? SendX5C { get; set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00056EBE File Offset: 0x000550BE
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00056EC6 File Offset: 0x000550C6
		public bool SpaCode { get; set; }
	}
}
