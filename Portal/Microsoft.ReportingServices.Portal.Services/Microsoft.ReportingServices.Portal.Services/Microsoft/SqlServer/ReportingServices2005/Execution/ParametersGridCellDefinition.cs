using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200007F RID: 127
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ParametersGridCellDefinition
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001B410 File Offset: 0x00019610
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001B418 File Offset: 0x00019618
		public int RowIndex
		{
			get
			{
				return this.rowIndexField;
			}
			set
			{
				this.rowIndexField = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001B421 File Offset: 0x00019621
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001B429 File Offset: 0x00019629
		public int ColumnsIndex
		{
			get
			{
				return this.columnsIndexField;
			}
			set
			{
				this.columnsIndexField = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001B432 File Offset: 0x00019632
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001B43A File Offset: 0x0001963A
		public string ParameterName
		{
			get
			{
				return this.parameterNameField;
			}
			set
			{
				this.parameterNameField = value;
			}
		}

		// Token: 0x04000178 RID: 376
		private int rowIndexField;

		// Token: 0x04000179 RID: 377
		private int columnsIndexField;

		// Token: 0x0400017A RID: 378
		private string parameterNameField;
	}
}
