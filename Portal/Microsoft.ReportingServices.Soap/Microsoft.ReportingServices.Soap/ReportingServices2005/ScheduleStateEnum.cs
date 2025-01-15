using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200021E RID: 542
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum ScheduleStateEnum
	{
		// Token: 0x04000634 RID: 1588
		Ready,
		// Token: 0x04000635 RID: 1589
		Running,
		// Token: 0x04000636 RID: 1590
		Paused,
		// Token: 0x04000637 RID: 1591
		Expired,
		// Token: 0x04000638 RID: 1592
		Failing
	}
}
