using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000084 RID: 132
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class DataSourcePrompt
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001B4FE File Offset: 0x000196FE
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0001B506 File Offset: 0x00019706
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001B50F File Offset: 0x0001970F
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0001B517 File Offset: 0x00019717
		public string DataSourceID
		{
			get
			{
				return this.dataSourceIDField;
			}
			set
			{
				this.dataSourceIDField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001B520 File Offset: 0x00019720
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0001B528 File Offset: 0x00019728
		public string Prompt
		{
			get
			{
				return this.promptField;
			}
			set
			{
				this.promptField = value;
			}
		}

		// Token: 0x04000186 RID: 390
		private string nameField;

		// Token: 0x04000187 RID: 391
		private string dataSourceIDField;

		// Token: 0x04000188 RID: 392
		private string promptField;
	}
}
