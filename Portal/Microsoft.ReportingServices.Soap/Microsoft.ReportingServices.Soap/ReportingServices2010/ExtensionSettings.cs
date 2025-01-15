using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200002F RID: 47
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ExtensionSettings
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000D107 File Offset: 0x0000B307
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x0000D10F File Offset: 0x0000B30F
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

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000D118 File Offset: 0x0000B318
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0000D120 File Offset: 0x0000B320
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

		// Token: 0x040001B9 RID: 441
		private string extensionField;

		// Token: 0x040001BA RID: 442
		private ParameterValueOrFieldReference[] parameterValuesField;
	}
}
