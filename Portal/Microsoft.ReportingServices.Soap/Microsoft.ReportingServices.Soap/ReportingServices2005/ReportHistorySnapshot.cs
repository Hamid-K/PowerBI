using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200021F RID: 543
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ReportHistorySnapshot
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0002205B File Offset: 0x0002025B
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00022063 File Offset: 0x00020263
		public string HistoryID
		{
			get
			{
				return this.historyIDField;
			}
			set
			{
				this.historyIDField = value;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0002206C File Offset: 0x0002026C
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00022074 File Offset: 0x00020274
		public DateTime CreationDate
		{
			get
			{
				return this.creationDateField;
			}
			set
			{
				this.creationDateField = value;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0002207D File Offset: 0x0002027D
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00022085 File Offset: 0x00020285
		public int Size
		{
			get
			{
				return this.sizeField;
			}
			set
			{
				this.sizeField = value;
			}
		}

		// Token: 0x04000639 RID: 1593
		private string historyIDField;

		// Token: 0x0400063A RID: 1594
		private DateTime creationDateField;

		// Token: 0x0400063B RID: 1595
		private int sizeField;
	}
}
