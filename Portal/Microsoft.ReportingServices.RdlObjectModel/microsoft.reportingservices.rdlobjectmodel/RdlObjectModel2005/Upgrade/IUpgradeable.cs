using System;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000055 RID: 85
	internal interface IUpgradeable
	{
		// Token: 0x06000384 RID: 900
		void Upgrade(UpgradeImpl2005 upgrader);
	}
}
