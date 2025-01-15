using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000240 RID: 576
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum SecurityScopeEnum
	{
		// Token: 0x040006CF RID: 1743
		System,
		// Token: 0x040006D0 RID: 1744
		Catalog,
		// Token: 0x040006D1 RID: 1745
		Model,
		// Token: 0x040006D2 RID: 1746
		All
	}
}
