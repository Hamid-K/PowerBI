using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000080 RID: 128
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ParametersGridLayoutDefinition
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001B443 File Offset: 0x00019643
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001B44B File Offset: 0x0001964B
		public int NumberOfColumns
		{
			get
			{
				return this.numberOfColumnsField;
			}
			set
			{
				this.numberOfColumnsField = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001B454 File Offset: 0x00019654
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001B45C File Offset: 0x0001965C
		public int NumberOfRows
		{
			get
			{
				return this.numberOfRowsField;
			}
			set
			{
				this.numberOfRowsField = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001B465 File Offset: 0x00019665
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0001B46D File Offset: 0x0001966D
		public ParametersGridCellDefinition[] CellDefinitions
		{
			get
			{
				return this.cellDefinitionsField;
			}
			set
			{
				this.cellDefinitionsField = value;
			}
		}

		// Token: 0x0400017B RID: 379
		private int numberOfColumnsField;

		// Token: 0x0400017C RID: 380
		private int numberOfRowsField;

		// Token: 0x0400017D RID: 381
		private ParametersGridCellDefinition[] cellDefinitionsField;
	}
}
