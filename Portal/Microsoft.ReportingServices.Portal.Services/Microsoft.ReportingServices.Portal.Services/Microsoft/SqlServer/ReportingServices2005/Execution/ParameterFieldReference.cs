using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200007A RID: 122
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ParameterFieldReference : ParameterValueOrFieldReference
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001B32B File Offset: 0x0001952B
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0001B333 File Offset: 0x00019533
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001B33C File Offset: 0x0001953C
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001B344 File Offset: 0x00019544
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

		// Token: 0x0400016B RID: 363
		private string parameterNameField;

		// Token: 0x0400016C RID: 364
		private string fieldAliasField;
	}
}
