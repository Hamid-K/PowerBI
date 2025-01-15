using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001D RID: 29
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ValidValue
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000CC14 File Offset: 0x0000AE14
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000CC1C File Offset: 0x0000AE1C
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000CC25 File Offset: 0x0000AE25
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x0000CC2D File Offset: 0x0000AE2D
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

		// Token: 0x0400016E RID: 366
		private string labelField;

		// Token: 0x0400016F RID: 367
		private string valueField;
	}
}
