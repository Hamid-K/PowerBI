using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200002A RID: 42
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public enum CredentialRetrievalEnum
	{
		// Token: 0x040001A5 RID: 421
		Prompt,
		// Token: 0x040001A6 RID: 422
		Store,
		// Token: 0x040001A7 RID: 423
		Integrated,
		// Token: 0x040001A8 RID: 424
		None
	}
}
