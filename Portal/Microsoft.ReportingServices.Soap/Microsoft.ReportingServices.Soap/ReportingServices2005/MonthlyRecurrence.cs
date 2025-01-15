using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000214 RID: 532
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class MonthlyRecurrence : RecurrencePattern
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00021E04 File Offset: 0x00020004
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00021E0C File Offset: 0x0002000C
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00021E15 File Offset: 0x00020015
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00021E1D File Offset: 0x0002001D
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

		// Token: 0x0400060E RID: 1550
		private string daysField;

		// Token: 0x0400060F RID: 1551
		private MonthsOfYearSelector monthsOfYearField;
	}
}
