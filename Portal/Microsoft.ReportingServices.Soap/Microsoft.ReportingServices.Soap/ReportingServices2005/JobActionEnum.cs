using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000223 RID: 547
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum JobActionEnum
	{
		// Token: 0x0400064C RID: 1612
		Render,
		// Token: 0x0400064D RID: 1613
		SnapshotCreation,
		// Token: 0x0400064E RID: 1614
		ReportHistoryCreation,
		// Token: 0x0400064F RID: 1615
		ExecuteQuery
	}
}
