using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200008C RID: 140
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ExecutionInfo3 : ExecutionInfo2
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001B814 File Offset: 0x00019A14
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001B81C File Offset: 0x00019A1C
		public ParametersGridLayoutDefinition ParametersLayout
		{
			get
			{
				return this.parametersLayoutField;
			}
			set
			{
				this.parametersLayoutField = value;
			}
		}

		// Token: 0x040001C2 RID: 450
		private ParametersGridLayoutDefinition parametersLayoutField;
	}
}
