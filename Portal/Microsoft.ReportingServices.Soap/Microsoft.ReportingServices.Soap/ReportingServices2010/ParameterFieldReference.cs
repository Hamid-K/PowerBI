using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000030 RID: 48
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class ParameterFieldReference : ParameterValueOrFieldReference
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000D131 File Offset: 0x0000B331
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000D139 File Offset: 0x0000B339
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000D142 File Offset: 0x0000B342
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000D14A File Offset: 0x0000B34A
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

		// Token: 0x040001BB RID: 443
		private string parameterNameField;

		// Token: 0x040001BC RID: 444
		private string fieldAliasField;
	}
}
