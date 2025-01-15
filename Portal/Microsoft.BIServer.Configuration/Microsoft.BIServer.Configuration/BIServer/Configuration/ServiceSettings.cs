using System;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000022 RID: 34
	public class ServiceSettings
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000051B9 File Offset: 0x000033B9
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000051C1 File Offset: 0x000033C1
		public bool IsEventService { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000051CA File Offset: 0x000033CA
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000051D2 File Offset: 0x000033D2
		public int MaxQueueThreads { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000051DB File Offset: 0x000033DB
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000051E3 File Offset: 0x000033E3
		public int PollingInterval { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000051EC File Offset: 0x000033EC
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000051F4 File Offset: 0x000033F4
		public int MaxCatalogConnectionPoolSizePerProcess { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000051FD File Offset: 0x000033FD
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00005205 File Offset: 0x00003405
		public bool IsDataModelRefreshService { get; set; }
	}
}
