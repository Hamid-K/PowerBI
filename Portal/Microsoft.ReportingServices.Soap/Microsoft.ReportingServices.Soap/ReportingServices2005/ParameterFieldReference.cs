using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000200 RID: 512
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ParameterFieldReference : ParameterValueOrFieldReference
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00021857 File Offset: 0x0001FA57
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0002185F File Offset: 0x0001FA5F
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

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00021868 File Offset: 0x0001FA68
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00021870 File Offset: 0x0001FA70
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

		// Token: 0x040005B8 RID: 1464
		private string parameterNameField;

		// Token: 0x040005B9 RID: 1465
		private string fieldAliasField;
	}
}
