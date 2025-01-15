using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000205 RID: 517
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ValidValue
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00021A28 File Offset: 0x0001FC28
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x00021A30 File Offset: 0x0001FC30
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

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00021A39 File Offset: 0x0001FC39
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x00021A41 File Offset: 0x0001FC41
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

		// Token: 0x040005D1 RID: 1489
		private string labelField;

		// Token: 0x040005D2 RID: 1490
		private string valueField;
	}
}
