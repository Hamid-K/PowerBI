using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000143 RID: 323
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class TimeExpiration : ExpirationDefinition
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0001724C File Offset: 0x0001544C
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00017254 File Offset: 0x00015454
		public int Minutes
		{
			get
			{
				return this.minutesField;
			}
			set
			{
				this.minutesField = value;
			}
		}

		// Token: 0x04000405 RID: 1029
		private int minutesField;
	}
}
