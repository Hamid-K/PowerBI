using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B3 RID: 947
	public class PackageChangeEventArgs : EventArgs
	{
		// Token: 0x06001D3B RID: 7483 RVA: 0x0006FC46 File Offset: 0x0006DE46
		public PackageChangeEventArgs(PackageAction action, IEnumerable<IPackage> packages)
		{
			this.Action = action;
			this.Packages = packages;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006FC5C File Offset: 0x0006DE5C
		// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0006FC64 File Offset: 0x0006DE64
		public PackageAction Action { get; private set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0006FC6D File Offset: 0x0006DE6D
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0006FC75 File Offset: 0x0006DE75
		public IEnumerable<IPackage> Packages { get; private set; }
	}
}
