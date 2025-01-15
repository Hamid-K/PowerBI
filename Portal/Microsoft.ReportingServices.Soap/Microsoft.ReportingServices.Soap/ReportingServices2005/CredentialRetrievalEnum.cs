using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200020C RID: 524
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum CredentialRetrievalEnum
	{
		// Token: 0x040005F9 RID: 1529
		Prompt,
		// Token: 0x040005FA RID: 1530
		Store,
		// Token: 0x040005FB RID: 1531
		Integrated,
		// Token: 0x040005FC RID: 1532
		None
	}
}
