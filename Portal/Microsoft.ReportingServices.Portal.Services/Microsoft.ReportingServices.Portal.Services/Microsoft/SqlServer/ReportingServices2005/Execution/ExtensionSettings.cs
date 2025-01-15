using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000079 RID: 121
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ExtensionSettings
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001B309 File Offset: 0x00019509
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001B311 File Offset: 0x00019511
		public string Extension
		{
			get
			{
				return this.extensionField;
			}
			set
			{
				this.extensionField = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001B31A File Offset: 0x0001951A
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001B322 File Offset: 0x00019522
		[XmlArrayItem(typeof(ParameterFieldReference))]
		[XmlArrayItem(typeof(ParameterValue))]
		public ParameterValueOrFieldReference[] ParameterValues
		{
			get
			{
				return this.parameterValuesField;
			}
			set
			{
				this.parameterValuesField = value;
			}
		}

		// Token: 0x04000169 RID: 361
		private string extensionField;

		// Token: 0x0400016A RID: 362
		private ParameterValueOrFieldReference[] parameterValuesField;
	}
}
