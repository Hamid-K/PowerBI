using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000039 RID: 57
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataRetrievalPlan
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000D46E File Offset: 0x0000B66E
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000D476 File Offset: 0x0000B676
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000D47F File Offset: 0x0000B67F
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000D487 File Offset: 0x0000B687
		public DataSetDefinition DataSet
		{
			get
			{
				return this.dataSetField;
			}
			set
			{
				this.dataSetField = value;
			}
		}

		// Token: 0x040001EC RID: 492
		private DataSourceDefinitionOrReference itemField;

		// Token: 0x040001ED RID: 493
		private DataSetDefinition dataSetField;
	}
}
