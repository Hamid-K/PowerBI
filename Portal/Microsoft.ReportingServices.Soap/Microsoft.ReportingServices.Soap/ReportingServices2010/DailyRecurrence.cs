using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200000F RID: 15
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DailyRecurrence : RecurrencePattern
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		public int DaysInterval
		{
			get
			{
				return this.daysIntervalField;
			}
			set
			{
				this.daysIntervalField = value;
			}
		}

		// Token: 0x0400012F RID: 303
		private int daysIntervalField;
	}
}
