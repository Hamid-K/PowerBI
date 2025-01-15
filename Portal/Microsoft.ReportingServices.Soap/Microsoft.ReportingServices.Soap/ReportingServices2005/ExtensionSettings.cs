using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001FF RID: 511
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ExtensionSettings
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0002182D File Offset: 0x0001FA2D
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00021835 File Offset: 0x0001FA35
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

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0002183E File Offset: 0x0001FA3E
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00021846 File Offset: 0x0001FA46
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

		// Token: 0x040005B6 RID: 1462
		private string extensionField;

		// Token: 0x040005B7 RID: 1463
		private ParameterValueOrFieldReference[] parameterValuesField;
	}
}
