using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000088 RID: 136
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public enum ParameterStateEnum
	{
		// Token: 0x040001AA RID: 426
		HasValidValue,
		// Token: 0x040001AB RID: 427
		MissingValidValue,
		// Token: 0x040001AC RID: 428
		HasOutstandingDependencies,
		// Token: 0x040001AD RID: 429
		DynamicValuesUnavailable
	}
}
