using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x020001F2 RID: 498
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum AuthenticationMode
	{
		// Token: 0x040004B7 RID: 1207
		None,
		// Token: 0x040004B8 RID: 1208
		Windows,
		// Token: 0x040004B9 RID: 1209
		Passport,
		// Token: 0x040004BA RID: 1210
		Forms
	}
}
