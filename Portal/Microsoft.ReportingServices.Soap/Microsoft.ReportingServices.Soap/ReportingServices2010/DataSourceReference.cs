using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200002D RID: 45
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSourceReference : DataSourceDefinitionOrReference
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000CFE7 File Offset: 0x0000B1E7
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0000CFEF File Offset: 0x0000B1EF
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

		// Token: 0x040001A9 RID: 425
		private string referenceField;
	}
}
