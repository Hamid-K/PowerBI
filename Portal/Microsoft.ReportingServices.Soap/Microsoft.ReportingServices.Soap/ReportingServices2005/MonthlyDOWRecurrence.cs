using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000219 RID: 537
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class MonthlyDOWRecurrence : RecurrencePattern
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00021FD5 File Offset: 0x000201D5
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x00021FDD File Offset: 0x000201DD
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x00021FE6 File Offset: 0x000201E6
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x00021FEE File Offset: 0x000201EE
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x00021FF7 File Offset: 0x000201F7
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x00021FFF File Offset: 0x000201FF
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x00022008 File Offset: 0x00020208
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00022010 File Offset: 0x00020210
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

		// Token: 0x04000627 RID: 1575
		private WeekNumberEnum whichWeekField;

		// Token: 0x04000628 RID: 1576
		private bool whichWeekFieldSpecified;

		// Token: 0x04000629 RID: 1577
		private DaysOfWeekSelector daysOfWeekField;

		// Token: 0x0400062A RID: 1578
		private MonthsOfYearSelector monthsOfYearField;
	}
}
