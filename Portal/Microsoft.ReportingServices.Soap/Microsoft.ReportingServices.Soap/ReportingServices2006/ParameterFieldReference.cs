using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200011B RID: 283
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ParameterFieldReference : ParameterValueOrFieldReference
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000168D5 File Offset: 0x00014AD5
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000168DD File Offset: 0x00014ADD
		public string ParameterName
		{
			get
			{
				return this.parameterNameField;
			}
			set
			{
				this.parameterNameField = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000168E6 File Offset: 0x00014AE6
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x000168EE File Offset: 0x00014AEE
		public string FieldAlias
		{
			get
			{
				return this.fieldAliasField;
			}
			set
			{
				this.fieldAliasField = value;
			}
		}

		// Token: 0x04000364 RID: 868
		private string parameterNameField;

		// Token: 0x04000365 RID: 869
		private string fieldAliasField;
	}
}
