using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000149 RID: 329
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Warning
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0001748C File Offset: 0x0001568C
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x00017494 File Offset: 0x00015694
		public string Code
		{
			get
			{
				return this.codeField;
			}
			set
			{
				this.codeField = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0001749D File Offset: 0x0001569D
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x000174A5 File Offset: 0x000156A5
		public string Severity
		{
			get
			{
				return this.severityField;
			}
			set
			{
				this.severityField = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000174AE File Offset: 0x000156AE
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000174B6 File Offset: 0x000156B6
		public string ObjectName
		{
			get
			{
				return this.objectNameField;
			}
			set
			{
				this.objectNameField = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000174BF File Offset: 0x000156BF
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x000174C7 File Offset: 0x000156C7
		public string ObjectType
		{
			get
			{
				return this.objectTypeField;
			}
			set
			{
				this.objectTypeField = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000174D0 File Offset: 0x000156D0
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000174D8 File Offset: 0x000156D8
		public string Message
		{
			get
			{
				return this.messageField;
			}
			set
			{
				this.messageField = value;
			}
		}

		// Token: 0x04000430 RID: 1072
		private string codeField;

		// Token: 0x04000431 RID: 1073
		private string severityField;

		// Token: 0x04000432 RID: 1074
		private string objectNameField;

		// Token: 0x04000433 RID: 1075
		private string objectTypeField;

		// Token: 0x04000434 RID: 1076
		private string messageField;
	}
}
