using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000221 RID: 545
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSource
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000220D1 File Offset: 0x000202D1
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000220D9 File Offset: 0x000202D9
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000220E2 File Offset: 0x000202E2
		// (set) Token: 0x060014D5 RID: 5333 RVA: 0x000220EA File Offset: 0x000202EA
		[XmlElement("DataSourceDefinition", typeof(DataSourceDefinition))]
		[XmlElement("DataSourceReference", typeof(DataSourceReference))]
		[XmlElement("InvalidDataSourceReference", typeof(InvalidDataSourceReference))]
		public DataSourceDefinitionOrReference Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x0400063F RID: 1599
		private string nameField;

		// Token: 0x04000640 RID: 1600
		private DataSourceDefinitionOrReference itemField;
	}
}
