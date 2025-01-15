using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000012 RID: 18
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class WeeklyRecurrence : RecurrencePattern
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000C81D File Offset: 0x0000AA1D
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000C825 File Offset: 0x0000AA25
		public int WeeksInterval
		{
			get
			{
				return this.weeksIntervalField;
			}
			set
			{
				this.weeksIntervalField = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000C82E File Offset: 0x0000AA2E
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000C836 File Offset: 0x0000AA36
		[XmlIgnore]
		public bool WeeksIntervalSpecified
		{
			get
			{
				return this.weeksIntervalFieldSpecified;
			}
			set
			{
				this.weeksIntervalFieldSpecified = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000C83F File Offset: 0x0000AA3F
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000C847 File Offset: 0x0000AA47
		public DaysOfWeekSelector DaysOfWeek
		{
			get
			{
				return this.daysOfWeekField;
			}
			set
			{
				this.daysOfWeekField = value;
			}
		}

		// Token: 0x04000131 RID: 305
		private int weeksIntervalField;

		// Token: 0x04000132 RID: 306
		private bool weeksIntervalFieldSpecified;

		// Token: 0x04000133 RID: 307
		private DaysOfWeekSelector daysOfWeekField;
	}
}
