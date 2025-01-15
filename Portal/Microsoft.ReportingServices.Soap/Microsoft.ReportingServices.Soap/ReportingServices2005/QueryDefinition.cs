using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000206 RID: 518
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class QueryDefinition
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00021A52 File Offset: 0x0001FC52
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00021A5A File Offset: 0x0001FC5A
		public string CommandType
		{
			get
			{
				return this.commandTypeField;
			}
			set
			{
				this.commandTypeField = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00021A63 File Offset: 0x0001FC63
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00021A6B File Offset: 0x0001FC6B
		public string CommandText
		{
			get
			{
				return this.commandTextField;
			}
			set
			{
				this.commandTextField = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00021A74 File Offset: 0x0001FC74
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00021A7C File Offset: 0x0001FC7C
		public int Timeout
		{
			get
			{
				return this.timeoutField;
			}
			set
			{
				this.timeoutField = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00021A85 File Offset: 0x0001FC85
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00021A8D File Offset: 0x0001FC8D
		[XmlIgnore]
		public bool TimeoutSpecified
		{
			get
			{
				return this.timeoutFieldSpecified;
			}
			set
			{
				this.timeoutFieldSpecified = value;
			}
		}

		// Token: 0x040005D3 RID: 1491
		private string commandTypeField;

		// Token: 0x040005D4 RID: 1492
		private string commandTextField;

		// Token: 0x040005D5 RID: 1493
		private int timeoutField;

		// Token: 0x040005D6 RID: 1494
		private bool timeoutFieldSpecified;
	}
}
