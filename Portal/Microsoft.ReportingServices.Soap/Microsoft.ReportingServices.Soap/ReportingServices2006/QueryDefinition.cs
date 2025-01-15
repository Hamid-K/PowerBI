using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000121 RID: 289
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class QueryDefinition
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00016AD0 File Offset: 0x00014CD0
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00016AD8 File Offset: 0x00014CD8
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00016AE1 File Offset: 0x00014CE1
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00016AE9 File Offset: 0x00014CE9
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

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00016AF2 File Offset: 0x00014CF2
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00016AFA File Offset: 0x00014CFA
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

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00016B03 File Offset: 0x00014D03
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00016B0B File Offset: 0x00014D0B
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

		// Token: 0x0400037F RID: 895
		private string commandTypeField;

		// Token: 0x04000380 RID: 896
		private string commandTextField;

		// Token: 0x04000381 RID: 897
		private int timeoutField;

		// Token: 0x04000382 RID: 898
		private bool timeoutFieldSpecified;
	}
}
