using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000085 RID: 133
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ValidValue
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001B531 File Offset: 0x00019731
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001B539 File Offset: 0x00019739
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001B542 File Offset: 0x00019742
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001B54A File Offset: 0x0001974A
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

		// Token: 0x04000189 RID: 393
		private string labelField;

		// Token: 0x0400018A RID: 394
		private string valueField;
	}
}
