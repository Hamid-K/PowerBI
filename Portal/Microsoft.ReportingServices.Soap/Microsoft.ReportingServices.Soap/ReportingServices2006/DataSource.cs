using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200013C RID: 316
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSource
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0001714F File Offset: 0x0001534F
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x00017157 File Offset: 0x00015357
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

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00017160 File Offset: 0x00015360
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00017168 File Offset: 0x00015368
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

		// Token: 0x040003EB RID: 1003
		private string nameField;

		// Token: 0x040003EC RID: 1004
		private DataSourceDefinitionOrReference itemField;
	}
}
