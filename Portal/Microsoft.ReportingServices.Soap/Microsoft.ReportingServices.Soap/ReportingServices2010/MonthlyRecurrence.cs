using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000014 RID: 20
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class MonthlyRecurrence : RecurrencePattern
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000C8DF File Offset: 0x0000AADF
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
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

		// Token: 0x0400013B RID: 315
		private string daysField;

		// Token: 0x0400013C RID: 316
		private MonthsOfYearSelector monthsOfYearField;
	}
}
