using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000018 RID: 24
	[XmlInclude(typeof(NoSchedule))]
	[XmlInclude(typeof(ScheduleReference))]
	[XmlInclude(typeof(ScheduleDefinition))]
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ScheduleDefinitionOrReference
	{
	}
}
