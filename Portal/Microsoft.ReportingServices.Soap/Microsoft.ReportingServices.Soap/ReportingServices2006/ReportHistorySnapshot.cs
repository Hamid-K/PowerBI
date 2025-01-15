using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200013A RID: 314
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ReportHistorySnapshot
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x000170D9 File Offset: 0x000152D9
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x000170E1 File Offset: 0x000152E1
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

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x000170EA File Offset: 0x000152EA
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x000170F2 File Offset: 0x000152F2
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

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000170FB File Offset: 0x000152FB
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x00017103 File Offset: 0x00015303
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

		// Token: 0x040003E5 RID: 997
		private string historyIDField;

		// Token: 0x040003E6 RID: 998
		private DateTime creationDateField;

		// Token: 0x040003E7 RID: 999
		private int sizeField;
	}
}
