using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2010.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2010
{
	// Token: 0x02000069 RID: 105
	internal class StateIndicator2010 : StateIndicator, IUpgradeable2010
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x000164D5 File Offset: 0x000146D5
		public void Upgrade(UpgradeImpl2010 upgrader)
		{
			if (base.Style != null)
			{
				base.Style.Border = new Border();
				base.Style.Border.Style = BorderStyles.Solid;
			}
		}
	}
}
