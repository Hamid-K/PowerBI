using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000016 RID: 22
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class MonthlyDOWRecurrence : RecurrencePattern
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000C9D5 File Offset: 0x0000ABD5
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000C9DD File Offset: 0x0000ABDD
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000C9E6 File Offset: 0x0000ABE6
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000C9EE File Offset: 0x0000ABEE
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000C9F7 File Offset: 0x0000ABF7
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000C9FF File Offset: 0x0000ABFF
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000CA08 File Offset: 0x0000AC08
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000CA10 File Offset: 0x0000AC10
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

		// Token: 0x04000149 RID: 329
		private WeekNumberEnum whichWeekField;

		// Token: 0x0400014A RID: 330
		private bool whichWeekFieldSpecified;

		// Token: 0x0400014B RID: 331
		private DaysOfWeekSelector daysOfWeekField;

		// Token: 0x0400014C RID: 332
		private MonthsOfYearSelector monthsOfYearField;
	}
}
