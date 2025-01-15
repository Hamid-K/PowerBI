using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200020E RID: 526
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSourceReference : DataSourceDefinitionOrReference
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00021CB3 File Offset: 0x0001FEB3
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x00021CBB File Offset: 0x0001FEBB
		public string Reference
		{
			get
			{
				return this.referenceField;
			}
			set
			{
				this.referenceField = value;
			}
		}

		// Token: 0x040005FD RID: 1533
		private string referenceField;
	}
}
