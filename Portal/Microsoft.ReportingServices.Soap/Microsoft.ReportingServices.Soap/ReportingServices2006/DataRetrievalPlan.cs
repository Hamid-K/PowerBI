using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000125 RID: 293
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataRetrievalPlan
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00016C1A File Offset: 0x00014E1A
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x00016C22 File Offset: 0x00014E22
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x00016C2B File Offset: 0x00014E2B
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x00016C33 File Offset: 0x00014E33
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

		// Token: 0x04000395 RID: 917
		private DataSourceDefinitionOrReference itemField;

		// Token: 0x04000396 RID: 918
		private DataSetDefinition dataSetField;
	}
}
