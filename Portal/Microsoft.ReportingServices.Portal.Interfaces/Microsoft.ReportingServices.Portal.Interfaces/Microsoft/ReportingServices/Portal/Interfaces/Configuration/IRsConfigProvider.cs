using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Configuration
{
	// Token: 0x020000C8 RID: 200
	public interface IRsConfigProvider
	{
		// Token: 0x060005AE RID: 1454
		object Load();

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060005AF RID: 1455
		// (remove) Token: 0x060005B0 RID: 1456
		event EventHandler ConfigurationChanged;
	}
}
