using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000020 RID: 32
	internal class DataSource2005 : DataSource, IUpgradeable
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00002E2D File Offset: 0x0000102D
		public DataSource2005()
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002E35 File Offset: 0x00001035
		public DataSource2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00002E3E File Offset: 0x0000103E
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeDataSource(this);
		}
	}
}
