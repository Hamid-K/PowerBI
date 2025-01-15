using System;

namespace Microsoft.Cloud.Platform.IfxAuditing
{
	// Token: 0x0200032F RID: 815
	public class TargetResource
	{
		// Token: 0x060017FA RID: 6138 RVA: 0x00058583 File Offset: 0x00056783
		public TargetResource(string targetResourceType, string targetResourceName)
		{
			this.TargetResourceType = targetResourceType;
			this.TargetResourceName = targetResourceName;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00058599 File Offset: 0x00056799
		public string TargetResourceType { get; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x000585A1 File Offset: 0x000567A1
		public string TargetResourceName { get; }
	}
}
