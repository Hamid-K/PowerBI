using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200022E RID: 558
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public enum ItemTypeEnum
	{
		// Token: 0x04000693 RID: 1683
		Unknown,
		// Token: 0x04000694 RID: 1684
		Folder,
		// Token: 0x04000695 RID: 1685
		Report,
		// Token: 0x04000696 RID: 1686
		Resource,
		// Token: 0x04000697 RID: 1687
		LinkedReport,
		// Token: 0x04000698 RID: 1688
		DataSource,
		// Token: 0x04000699 RID: 1689
		Model
	}
}
