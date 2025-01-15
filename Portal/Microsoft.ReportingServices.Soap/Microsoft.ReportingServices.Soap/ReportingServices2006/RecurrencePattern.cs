using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200012E RID: 302
	[XmlInclude(typeof(MonthlyRecurrence))]
	[XmlInclude(typeof(DailyRecurrence))]
	[XmlInclude(typeof(WeeklyRecurrence))]
	[XmlInclude(typeof(MinuteRecurrence))]
	[XmlInclude(typeof(MonthlyDOWRecurrence))]
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class RecurrencePattern
	{
	}
}
