using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000139 RID: 313
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum ScheduleStateEnum
	{
		// Token: 0x040003E0 RID: 992
		Ready,
		// Token: 0x040003E1 RID: 993
		Running,
		// Token: 0x040003E2 RID: 994
		Paused,
		// Token: 0x040003E3 RID: 995
		Expired,
		// Token: 0x040003E4 RID: 996
		Failing
	}
}
