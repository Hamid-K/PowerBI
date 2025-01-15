using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000128 RID: 296
	[XmlInclude(typeof(DataSourceReference))]
	[XmlInclude(typeof(InvalidDataSourceReference))]
	[XmlInclude(typeof(DataSourceDefinition))]
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSourceDefinitionOrReference
	{
	}
}
