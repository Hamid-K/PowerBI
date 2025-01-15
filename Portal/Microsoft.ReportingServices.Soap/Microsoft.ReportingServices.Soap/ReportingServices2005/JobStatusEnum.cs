using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000225 RID: 549
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum JobStatusEnum
	{
		// Token: 0x04000654 RID: 1620
		New,
		// Token: 0x04000655 RID: 1621
		Running,
		// Token: 0x04000656 RID: 1622
		CancelRequested
	}
}
