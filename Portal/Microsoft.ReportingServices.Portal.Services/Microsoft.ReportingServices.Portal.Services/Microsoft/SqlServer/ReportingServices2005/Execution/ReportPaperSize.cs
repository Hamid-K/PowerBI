using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000082 RID: 130
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ReportPaperSize
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001B4BA File Offset: 0x000196BA
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001B4C2 File Offset: 0x000196C2
		public double Height
		{
			get
			{
				return this.heightField;
			}
			set
			{
				this.heightField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001B4CB File Offset: 0x000196CB
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001B4D3 File Offset: 0x000196D3
		public double Width
		{
			get
			{
				return this.widthField;
			}
			set
			{
				this.widthField = value;
			}
		}

		// Token: 0x04000182 RID: 386
		private double heightField;

		// Token: 0x04000183 RID: 387
		private double widthField;
	}
}
