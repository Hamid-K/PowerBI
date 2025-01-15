using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000217 RID: 535
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class WeeklyRecurrence : RecurrencePattern
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00021F1B File Offset: 0x0002011B
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00021F23 File Offset: 0x00020123
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00021F2C File Offset: 0x0002012C
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x00021F34 File Offset: 0x00020134
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00021F3D File Offset: 0x0002013D
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x00021F45 File Offset: 0x00020145
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

		// Token: 0x0400061D RID: 1565
		private int weeksIntervalField;

		// Token: 0x0400061E RID: 1566
		private bool weeksIntervalFieldSpecified;

		// Token: 0x0400061F RID: 1567
		private DaysOfWeekSelector daysOfWeekField;
	}
}
