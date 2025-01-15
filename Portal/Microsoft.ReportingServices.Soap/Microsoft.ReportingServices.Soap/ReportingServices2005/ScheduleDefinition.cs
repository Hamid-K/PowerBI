using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000211 RID: 529
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ScheduleDefinition : ScheduleDefinitionOrReference
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00021D97 File Offset: 0x0001FF97
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00021D9F File Offset: 0x0001FF9F
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00021DA8 File Offset: 0x0001FFA8
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00021DB0 File Offset: 0x0001FFB0
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

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00021DB9 File Offset: 0x0001FFB9
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x00021DC1 File Offset: 0x0001FFC1
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00021DCA File Offset: 0x0001FFCA
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00021DD2 File Offset: 0x0001FFD2
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

		// Token: 0x04000609 RID: 1545
		private DateTime startDateTimeField;

		// Token: 0x0400060A RID: 1546
		private DateTime endDateField;

		// Token: 0x0400060B RID: 1547
		private bool endDateFieldSpecified;

		// Token: 0x0400060C RID: 1548
		private RecurrencePattern itemField;
	}
}
