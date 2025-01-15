using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000212 RID: 530
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DailyRecurrence : RecurrencePattern
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00021DE3 File Offset: 0x0001FFE3
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00021DEB File Offset: 0x0001FFEB
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

		// Token: 0x0400060D RID: 1549
		private int daysIntervalField;
	}
}
