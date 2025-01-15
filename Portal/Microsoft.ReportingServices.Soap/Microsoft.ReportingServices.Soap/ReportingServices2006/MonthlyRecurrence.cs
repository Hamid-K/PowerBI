using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200012F RID: 303
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class MonthlyRecurrence : RecurrencePattern
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00016E82 File Offset: 0x00015082
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00016E8A File Offset: 0x0001508A
		public string Days
		{
			get
			{
				return this.daysField;
			}
			set
			{
				this.daysField = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x00016E93 File Offset: 0x00015093
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x00016E9B File Offset: 0x0001509B
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

		// Token: 0x040003BA RID: 954
		private string daysField;

		// Token: 0x040003BB RID: 955
		private MonthsOfYearSelector monthsOfYearField;
	}
}
