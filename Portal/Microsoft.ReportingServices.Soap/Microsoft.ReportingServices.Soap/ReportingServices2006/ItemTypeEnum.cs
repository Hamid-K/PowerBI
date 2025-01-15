using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200014B RID: 331
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum ItemTypeEnum
	{
		// Token: 0x04000449 RID: 1097
		Unknown,
		// Token: 0x0400044A RID: 1098
		Folder,
		// Token: 0x0400044B RID: 1099
		Report,
		// Token: 0x0400044C RID: 1100
		Resource,
		// Token: 0x0400044D RID: 1101
		DataSource,
		// Token: 0x0400044E RID: 1102
		Model,
		// Token: 0x0400044F RID: 1103
		Site
	}
}
