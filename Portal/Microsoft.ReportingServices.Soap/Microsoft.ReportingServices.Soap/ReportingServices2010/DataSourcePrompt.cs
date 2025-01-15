using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000024 RID: 36
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSourcePrompt
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000CDFE File Offset: 0x0000AFFE
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000CE06 File Offset: 0x0000B006
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000CE0F File Offset: 0x0000B00F
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000CE17 File Offset: 0x0000B017
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000CE20 File Offset: 0x0000B020
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0000CE28 File Offset: 0x0000B028
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

		// Token: 0x0400018B RID: 395
		private string nameField;

		// Token: 0x0400018C RID: 396
		private string dataSourceIDField;

		// Token: 0x0400018D RID: 397
		private string promptField;
	}
}
