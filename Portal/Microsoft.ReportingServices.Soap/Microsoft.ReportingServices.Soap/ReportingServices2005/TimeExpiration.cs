using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000227 RID: 551
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class TimeExpiration : ExpirationDefinition
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000221B5 File Offset: 0x000203B5
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x000221BD File Offset: 0x000203BD
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

		// Token: 0x04000657 RID: 1623
		private int minutesField;
	}
}
