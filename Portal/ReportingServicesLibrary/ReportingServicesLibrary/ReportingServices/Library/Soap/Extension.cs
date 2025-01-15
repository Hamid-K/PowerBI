using System;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000349 RID: 841
	public class Extension
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x0007185B File Offset: 0x0006FA5B
		public Extension()
		{
			this.ExtensionType = ExtensionTypeEnum.All;
			this.Name = null;
			this.LocalizedName = null;
			this.Visible = false;
		}

		// Token: 0x04000B86 RID: 2950
		public ExtensionTypeEnum ExtensionType;

		// Token: 0x04000B87 RID: 2951
		public string Name;

		// Token: 0x04000B88 RID: 2952
		public string LocalizedName;

		// Token: 0x04000B89 RID: 2953
		public bool Visible;
	}
}
