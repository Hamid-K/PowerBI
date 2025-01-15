using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000087 RID: 135
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public enum ParameterTypeEnum
	{
		// Token: 0x040001A4 RID: 420
		Boolean,
		// Token: 0x040001A5 RID: 421
		DateTime,
		// Token: 0x040001A6 RID: 422
		Integer,
		// Token: 0x040001A7 RID: 423
		Float,
		// Token: 0x040001A8 RID: 424
		String
	}
}
