using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003E RID: 62
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ItemHistorySnapshot
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000D549 File Offset: 0x0000B749
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000D551 File Offset: 0x0000B751
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000D55A File Offset: 0x0000B75A
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000D562 File Offset: 0x0000B762
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000D56B File Offset: 0x0000B76B
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000D573 File Offset: 0x0000B773
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

		// Token: 0x040001FC RID: 508
		private string historyIDField;

		// Token: 0x040001FD RID: 509
		private DateTime creationDateField;

		// Token: 0x040001FE RID: 510
		private int sizeField;
	}
}
