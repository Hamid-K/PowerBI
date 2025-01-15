using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000145 RID: 325
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum ParameterTypeEnum
	{
		// Token: 0x0400041F RID: 1055
		Boolean,
		// Token: 0x04000420 RID: 1056
		DateTime,
		// Token: 0x04000421 RID: 1057
		Integer,
		// Token: 0x04000422 RID: 1058
		Float,
		// Token: 0x04000423 RID: 1059
		String
	}
}
