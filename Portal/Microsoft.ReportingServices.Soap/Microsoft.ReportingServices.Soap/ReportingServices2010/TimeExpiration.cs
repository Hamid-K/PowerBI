using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200000C RID: 12
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class TimeExpiration : ExpirationDefinition
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000C765 File Offset: 0x0000A965
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000C76D File Offset: 0x0000A96D
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

		// Token: 0x04000129 RID: 297
		private int minutesField;
	}
}
