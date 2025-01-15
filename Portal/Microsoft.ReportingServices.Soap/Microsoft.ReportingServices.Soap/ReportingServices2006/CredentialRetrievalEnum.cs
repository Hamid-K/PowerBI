using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000127 RID: 295
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum CredentialRetrievalEnum
	{
		// Token: 0x040003A5 RID: 933
		Prompt,
		// Token: 0x040003A6 RID: 934
		Store,
		// Token: 0x040003A7 RID: 935
		Integrated,
		// Token: 0x040003A8 RID: 936
		None
	}
}
