using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ScheduleDefinition : ScheduleDefinitionOrReference
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000C797 File Offset: 0x0000A997
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000C79F File Offset: 0x0000A99F
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000C7B9 File Offset: 0x0000A9B9
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000C7C1 File Offset: 0x0000A9C1
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
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

		// Token: 0x0400012B RID: 299
		private DateTime startDateTimeField;

		// Token: 0x0400012C RID: 300
		private DateTime endDateField;

		// Token: 0x0400012D RID: 301
		private bool endDateFieldSpecified;

		// Token: 0x0400012E RID: 302
		private RecurrencePattern itemField;
	}
}
