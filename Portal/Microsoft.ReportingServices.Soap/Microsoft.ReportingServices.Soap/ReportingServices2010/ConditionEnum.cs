using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003D RID: 61
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public enum ConditionEnum
	{
		// Token: 0x040001F8 RID: 504
		Contains,
		// Token: 0x040001F9 RID: 505
		Equals,
		// Token: 0x040001FA RID: 506
		In,
		// Token: 0x040001FB RID: 507
		Between
	}
}
