using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200011D RID: 285
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ParameterValue : ParameterValueOrFieldReference
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00016907 File Offset: 0x00014B07
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0001690F File Offset: 0x00014B0F
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00016918 File Offset: 0x00014B18
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00016920 File Offset: 0x00014B20
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00016929 File Offset: 0x00014B29
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00016931 File Offset: 0x00014B31
		public string Label
		{
			get
			{
				return this.labelField;
			}
			set
			{
				this.labelField = value;
			}
		}

		// Token: 0x04000366 RID: 870
		private string nameField;

		// Token: 0x04000367 RID: 871
		private string valueField;

		// Token: 0x04000368 RID: 872
		private string labelField;
	}
}
