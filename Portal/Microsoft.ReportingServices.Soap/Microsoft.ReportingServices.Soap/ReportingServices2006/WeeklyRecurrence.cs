using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000131 RID: 305
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class WeeklyRecurrence : RecurrencePattern
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00016F80 File Offset: 0x00015180
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00016F88 File Offset: 0x00015188
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

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00016F91 File Offset: 0x00015191
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00016F99 File Offset: 0x00015199
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

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00016FA2 File Offset: 0x000151A2
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00016FAA File Offset: 0x000151AA
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

		// Token: 0x040003C8 RID: 968
		private int weeksIntervalField;

		// Token: 0x040003C9 RID: 969
		private bool weeksIntervalFieldSpecified;

		// Token: 0x040003CA RID: 970
		private DaysOfWeekSelector daysOfWeekField;
	}
}
