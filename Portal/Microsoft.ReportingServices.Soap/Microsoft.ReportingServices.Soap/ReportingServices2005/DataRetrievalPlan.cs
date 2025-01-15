using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200020A RID: 522
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataRetrievalPlan
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00021B9C File Offset: 0x0001FD9C
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x00021BA4 File Offset: 0x0001FDA4
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

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00021BAD File Offset: 0x0001FDAD
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x00021BB5 File Offset: 0x0001FDB5
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

		// Token: 0x040005E9 RID: 1513
		private DataSourceDefinitionOrReference itemField;

		// Token: 0x040005EA RID: 1514
		private DataSetDefinition dataSetField;
	}
}
