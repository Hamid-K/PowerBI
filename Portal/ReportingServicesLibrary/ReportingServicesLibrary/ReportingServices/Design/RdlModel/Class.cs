using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003BE RID: 958
	public sealed class Class
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x000025F4 File Offset: 0x000007F4
		public Class()
		{
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0007DDA6 File Offset: 0x0007BFA6
		public Class(string className, string instanceName)
		{
			this.ClassName = className;
			this.InstanceName = instanceName;
		}

		// Token: 0x04000D75 RID: 3445
		public string ClassName;

		// Token: 0x04000D76 RID: 3446
		public string InstanceName;
	}
}
