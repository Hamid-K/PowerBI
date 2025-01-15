using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200022B RID: 555
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum ParameterStateEnum
	{
		// Token: 0x04000678 RID: 1656
		HasValidValue,
		// Token: 0x04000679 RID: 1657
		MissingValidValue,
		// Token: 0x0400067A RID: 1658
		HasOutstandingDependencies,
		// Token: 0x0400067B RID: 1659
		DynamicValuesUnavailable
	}
}
