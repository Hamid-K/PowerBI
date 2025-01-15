using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000317 RID: 791
	public class Extension
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0006EB4C File Offset: 0x0006CD4C
		public Extension()
		{
			this.ExtensionType = ExtensionTypeEnum.All;
			this.Name = null;
			this.LocalizedName = null;
			this.Visible = false;
			this.IsModelGenerationSupported = false;
		}

		// Token: 0x04000AA2 RID: 2722
		public ExtensionTypeEnum ExtensionType;

		// Token: 0x04000AA3 RID: 2723
		public string Name;

		// Token: 0x04000AA4 RID: 2724
		public string LocalizedName;

		// Token: 0x04000AA5 RID: 2725
		public bool Visible;

		// Token: 0x04000AA6 RID: 2726
		public bool IsModelGenerationSupported;
	}
}
