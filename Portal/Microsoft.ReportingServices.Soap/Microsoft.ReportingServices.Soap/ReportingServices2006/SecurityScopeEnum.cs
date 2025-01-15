using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000155 RID: 341
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum SecurityScopeEnum
	{
		// Token: 0x0400046E RID: 1134
		System,
		// Token: 0x0400046F RID: 1135
		Catalog,
		// Token: 0x04000470 RID: 1136
		Model,
		// Token: 0x04000471 RID: 1137
		All
	}
}
