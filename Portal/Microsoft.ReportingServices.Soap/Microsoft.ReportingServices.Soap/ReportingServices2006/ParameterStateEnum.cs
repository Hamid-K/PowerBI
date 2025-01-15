using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000146 RID: 326
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum ParameterStateEnum
	{
		// Token: 0x04000425 RID: 1061
		HasValidValue,
		// Token: 0x04000426 RID: 1062
		MissingValidValue,
		// Token: 0x04000427 RID: 1063
		HasOutstandingDependencies,
		// Token: 0x04000428 RID: 1064
		DynamicValuesUnavailable
	}
}
