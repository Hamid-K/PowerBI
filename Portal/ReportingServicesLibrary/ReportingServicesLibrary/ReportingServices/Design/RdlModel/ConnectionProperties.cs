using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003C3 RID: 963
	public sealed class ConnectionProperties
	{
		// Token: 0x04000D81 RID: 3457
		public string DataProvider;

		// Token: 0x04000D82 RID: 3458
		public string ConnectString = "";

		// Token: 0x04000D83 RID: 3459
		[DefaultValue(false)]
		public bool IntegratedSecurity;

		// Token: 0x04000D84 RID: 3460
		[DefaultValue("")]
		public string Prompt;
	}
}
