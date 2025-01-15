using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000140 RID: 320
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum JobStatusEnum
	{
		// Token: 0x04000401 RID: 1025
		New,
		// Token: 0x04000402 RID: 1026
		Running,
		// Token: 0x04000403 RID: 1027
		CancelRequested
	}
}
