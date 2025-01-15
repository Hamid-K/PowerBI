using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000028 RID: 40
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSource
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000CEC8 File Offset: 0x0000B0C8
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000CED0 File Offset: 0x0000B0D0
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000CED9 File Offset: 0x0000B0D9
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0000CEE1 File Offset: 0x0000B0E1
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

		// Token: 0x04000195 RID: 405
		private string nameField;

		// Token: 0x04000196 RID: 406
		private DataSourceDefinitionOrReference itemField;
	}
}
