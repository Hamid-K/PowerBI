using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.Configuration
{
	// Token: 0x020000C5 RID: 197
	public interface IPortalConfigurationManager
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000591 RID: 1425
		IPortalConfiguration Current { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000592 RID: 1426
		// (remove) Token: 0x06000593 RID: 1427
		event EventHandler CurrentConfigurationChanged;
	}
}
