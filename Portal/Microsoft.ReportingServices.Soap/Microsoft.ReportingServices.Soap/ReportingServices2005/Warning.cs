using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000235 RID: 565
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Warning
	{
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0002260A File Offset: 0x0002080A
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x00022612 File Offset: 0x00020812
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

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0002261B File Offset: 0x0002081B
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x00022623 File Offset: 0x00020823
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0002262C File Offset: 0x0002082C
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00022634 File Offset: 0x00020834
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0002263D File Offset: 0x0002083D
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00022645 File Offset: 0x00020845
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0002264E File Offset: 0x0002084E
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x00022656 File Offset: 0x00020856
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

		// Token: 0x040006B1 RID: 1713
		private string codeField;

		// Token: 0x040006B2 RID: 1714
		private string severityField;

		// Token: 0x040006B3 RID: 1715
		private string objectNameField;

		// Token: 0x040006B4 RID: 1716
		private string objectTypeField;

		// Token: 0x040006B5 RID: 1717
		private string messageField;
	}
}
