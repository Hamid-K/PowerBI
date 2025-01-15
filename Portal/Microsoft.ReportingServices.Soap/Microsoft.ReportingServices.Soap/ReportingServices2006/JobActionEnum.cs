using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200013E RID: 318
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public enum JobActionEnum
	{
		// Token: 0x040003F8 RID: 1016
		Render,
		// Token: 0x040003F9 RID: 1017
		SnapshotCreation,
		// Token: 0x040003FA RID: 1018
		ReportHistoryCreation,
		// Token: 0x040003FB RID: 1019
		ExecuteQuery,
		// Token: 0x040003FC RID: 1020
		GetUserModel
	}
}
