using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000011 RID: 17
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class MinuteRecurrence : RecurrencePattern
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000C804 File Offset: 0x0000AA04
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000C80C File Offset: 0x0000AA0C
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

		// Token: 0x04000130 RID: 304
		private int minutesIntervalField;
	}
}
