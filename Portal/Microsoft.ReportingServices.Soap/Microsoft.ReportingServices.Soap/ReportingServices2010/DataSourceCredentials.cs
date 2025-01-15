using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200001B RID: 27
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSourceCredentials
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000CA5B File Offset: 0x0000AC5B
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000CA63 File Offset: 0x0000AC63
		public string DataSourceName
		{
			get
			{
				return this.dataSourceNameField;
			}
			set
			{
				this.dataSourceNameField = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public string UserName
		{
			get
			{
				return this.userNameField;
			}
			set
			{
				this.userNameField = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000CA7D File Offset: 0x0000AC7D
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000CA85 File Offset: 0x0000AC85
		public string Password
		{
			get
			{
				return this.passwordField;
			}
			set
			{
				this.passwordField = value;
			}
		}

		// Token: 0x04000155 RID: 341
		private string dataSourceNameField;

		// Token: 0x04000156 RID: 342
		private string userNameField;

		// Token: 0x04000157 RID: 343
		private string passwordField;
	}
}
