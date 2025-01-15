using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000134 RID: 308
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class MonthlyDOWRecurrence : RecurrencePattern
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00017053 File Offset: 0x00015253
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0001705B File Offset: 0x0001525B
		public WeekNumberEnum WhichWeek
		{
			get
			{
				return this.whichWeekField;
			}
			set
			{
				this.whichWeekField = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00017064 File Offset: 0x00015264
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0001706C File Offset: 0x0001526C
		[XmlIgnore]
		public bool WhichWeekSpecified
		{
			get
			{
				return this.whichWeekFieldSpecified;
			}
			set
			{
				this.whichWeekFieldSpecified = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00017075 File Offset: 0x00015275
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0001707D File Offset: 0x0001527D
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00017086 File Offset: 0x00015286
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0001708E File Offset: 0x0001528E
		public MonthsOfYearSelector MonthsOfYear
		{
			get
			{
				return this.monthsOfYearField;
			}
			set
			{
				this.monthsOfYearField = value;
			}
		}

		// Token: 0x040003D3 RID: 979
		private WeekNumberEnum whichWeekField;

		// Token: 0x040003D4 RID: 980
		private bool whichWeekFieldSpecified;

		// Token: 0x040003D5 RID: 981
		private DaysOfWeekSelector daysOfWeekField;

		// Token: 0x040003D6 RID: 982
		private MonthsOfYearSelector monthsOfYearField;
	}
}
