using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200003F RID: 63
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Warning
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000D584 File Offset: 0x0000B784
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x0000D58C File Offset: 0x0000B78C
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0000D595 File Offset: 0x0000B795
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x0000D59D File Offset: 0x0000B79D
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000D5A6 File Offset: 0x0000B7A6
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0000D5AE File Offset: 0x0000B7AE
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000D5B7 File Offset: 0x0000B7B7
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0000D5BF File Offset: 0x0000B7BF
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
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

		// Token: 0x040001FF RID: 511
		private string codeField;

		// Token: 0x04000200 RID: 512
		private string severityField;

		// Token: 0x04000201 RID: 513
		private string objectNameField;

		// Token: 0x04000202 RID: 514
		private string objectTypeField;

		// Token: 0x04000203 RID: 515
		private string messageField;
	}
}
