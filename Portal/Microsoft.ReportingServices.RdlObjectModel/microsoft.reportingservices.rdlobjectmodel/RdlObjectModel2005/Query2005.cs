using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000021 RID: 33
	internal class Query2005 : Query, IUpgradeable
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00002E47 File Offset: 0x00001047
		public Query2005()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002E4F File Offset: 0x0000104F
		public Query2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002E58 File Offset: 0x00001058
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeQuery(this);
		}
	}
}
