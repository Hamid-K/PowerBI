using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000213 RID: 531
	[XmlInclude(typeof(MonthlyRecurrence))]
	[XmlInclude(typeof(MinuteRecurrence))]
	[XmlInclude(typeof(DailyRecurrence))]
	[XmlInclude(typeof(WeeklyRecurrence))]
	[XmlInclude(typeof(MonthlyDOWRecurrence))]
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class RecurrencePattern
	{
	}
}
