using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000077 RID: 119
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public enum ExtensionTypeEnum
	{
		// Token: 0x04000162 RID: 354
		Delivery,
		// Token: 0x04000163 RID: 355
		Render,
		// Token: 0x04000164 RID: 356
		Data,
		// Token: 0x04000165 RID: 357
		All
	}
}
