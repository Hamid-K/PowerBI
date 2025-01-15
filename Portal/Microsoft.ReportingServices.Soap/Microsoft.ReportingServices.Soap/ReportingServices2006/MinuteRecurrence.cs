using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000133 RID: 307
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class MinuteRecurrence : RecurrencePattern
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0001703A File Offset: 0x0001523A
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x00017042 File Offset: 0x00015242
		public int MinutesInterval
		{
			get
			{
				return this.minutesIntervalField;
			}
			set
			{
				this.minutesIntervalField = value;
			}
		}

		// Token: 0x040003D2 RID: 978
		private int minutesIntervalField;
	}
}
