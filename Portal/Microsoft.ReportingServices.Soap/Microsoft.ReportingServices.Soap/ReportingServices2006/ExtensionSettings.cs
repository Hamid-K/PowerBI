using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200011A RID: 282
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ExtensionSettings
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x000168AB File Offset: 0x00014AAB
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x000168B3 File Offset: 0x00014AB3
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

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000168BC File Offset: 0x00014ABC
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x000168C4 File Offset: 0x00014AC4
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

		// Token: 0x04000362 RID: 866
		private string extensionField;

		// Token: 0x04000363 RID: 867
		private ParameterValueOrFieldReference[] parameterValuesField;
	}
}
