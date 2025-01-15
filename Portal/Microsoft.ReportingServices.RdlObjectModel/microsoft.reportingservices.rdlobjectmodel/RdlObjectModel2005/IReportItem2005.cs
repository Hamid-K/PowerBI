using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000037 RID: 55
	internal interface IReportItem2005 : IUpgradeable
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001E4 RID: 484
		// (set) Token: 0x060001E5 RID: 485
		Microsoft.ReportingServices.RdlObjectModel.Action Action { get; set; }
	}
}
