using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200021B RID: 539
	[XmlInclude(typeof(ScheduleReference))]
	[XmlInclude(typeof(ScheduleDefinition))]
	[XmlInclude(typeof(NoSchedule))]
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ScheduleDefinitionOrReference
	{
	}
}
