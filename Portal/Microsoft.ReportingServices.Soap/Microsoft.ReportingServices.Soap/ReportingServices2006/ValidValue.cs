using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000120 RID: 288
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ValidValue
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00016AA6 File Offset: 0x00014CA6
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00016AAE File Offset: 0x00014CAE
		public string Label
		{
			get
			{
				return this.labelField;
			}
			set
			{
				this.labelField = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00016AB7 File Offset: 0x00014CB7
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00016ABF File Offset: 0x00014CBF
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400037D RID: 893
		private string labelField;

		// Token: 0x0400037E RID: 894
		private string valueField;
	}
}
