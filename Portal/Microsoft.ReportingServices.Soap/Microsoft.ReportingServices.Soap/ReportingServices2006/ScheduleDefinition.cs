using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200012C RID: 300
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ScheduleDefinition : ScheduleDefinitionOrReference
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00016E15 File Offset: 0x00015015
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00016E1D File Offset: 0x0001501D
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTimeField;
			}
			set
			{
				this.startDateTimeField = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00016E26 File Offset: 0x00015026
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x00016E2E File Offset: 0x0001502E
		public DateTime EndDate
		{
			get
			{
				return this.endDateField;
			}
			set
			{
				this.endDateField = value;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00016E37 File Offset: 0x00015037
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x00016E3F File Offset: 0x0001503F
		[XmlIgnore]
		public bool EndDateSpecified
		{
			get
			{
				return this.endDateFieldSpecified;
			}
			set
			{
				this.endDateFieldSpecified = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00016E48 File Offset: 0x00015048
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00016E50 File Offset: 0x00015050
		[XmlElement("DailyRecurrence", typeof(DailyRecurrence))]
		[XmlElement("MinuteRecurrence", typeof(MinuteRecurrence))]
		[XmlElement("MonthlyDOWRecurrence", typeof(MonthlyDOWRecurrence))]
		[XmlElement("MonthlyRecurrence", typeof(MonthlyRecurrence))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrence))]
		public RecurrencePattern Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x040003B5 RID: 949
		private DateTime startDateTimeField;

		// Token: 0x040003B6 RID: 950
		private DateTime endDateField;

		// Token: 0x040003B7 RID: 951
		private bool endDateFieldSpecified;

		// Token: 0x040003B8 RID: 952
		private RecurrencePattern itemField;
	}
}
