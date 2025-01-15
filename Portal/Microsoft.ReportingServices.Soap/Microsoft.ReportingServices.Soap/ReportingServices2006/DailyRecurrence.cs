using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200012D RID: 301
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DailyRecurrence : RecurrencePattern
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00016E61 File Offset: 0x00015061
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x00016E69 File Offset: 0x00015069
		public int DaysInterval
		{
			get
			{
				return this.daysIntervalField;
			}
			set
			{
				this.daysIntervalField = value;
			}
		}

		// Token: 0x040003B9 RID: 953
		private int daysIntervalField;
	}
}
