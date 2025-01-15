using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000129 RID: 297
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSourceReference : DataSourceDefinitionOrReference
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00016D31 File Offset: 0x00014F31
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x00016D39 File Offset: 0x00014F39
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

		// Token: 0x040003A9 RID: 937
		private string referenceField;
	}
}
