using System;
using Microsoft.SqlServer.ReportingServices2010;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000084 RID: 132
	public class CreateReportEditSessionResult
	{
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00004A17 File Offset: 0x00002C17
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00004A1F File Offset: 0x00002C1F
		public string Report { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00004A28 File Offset: 0x00002C28
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00004A30 File Offset: 0x00002C30
		public Warning[] Warnings { get; set; }
	}
}
