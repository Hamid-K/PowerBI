using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000083 RID: 131
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class PageSettings
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001B4DC File Offset: 0x000196DC
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001B4E4 File Offset: 0x000196E4
		public ReportPaperSize PaperSize
		{
			get
			{
				return this.paperSizeField;
			}
			set
			{
				this.paperSizeField = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001B4ED File Offset: 0x000196ED
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0001B4F5 File Offset: 0x000196F5
		public ReportMargins Margins
		{
			get
			{
				return this.marginsField;
			}
			set
			{
				this.marginsField = value;
			}
		}

		// Token: 0x04000184 RID: 388
		private ReportPaperSize paperSizeField;

		// Token: 0x04000185 RID: 389
		private ReportMargins marginsField;
	}
}
