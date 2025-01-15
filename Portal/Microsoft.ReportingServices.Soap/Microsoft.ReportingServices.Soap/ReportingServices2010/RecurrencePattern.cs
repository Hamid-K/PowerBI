using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000010 RID: 16
	[XmlInclude(typeof(MinuteRecurrence))]
	[XmlInclude(typeof(WeeklyRecurrence))]
	[XmlInclude(typeof(MonthlyRecurrence))]
	[XmlInclude(typeof(MonthlyDOWRecurrence))]
	[XmlInclude(typeof(DailyRecurrence))]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class RecurrencePattern
	{
	}
}
