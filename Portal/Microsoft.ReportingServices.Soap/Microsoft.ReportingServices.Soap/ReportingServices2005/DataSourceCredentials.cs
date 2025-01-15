using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200022C RID: 556
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSourceCredentials
	{
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00022387 File Offset: 0x00020587
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x0002238F File Offset: 0x0002058F
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

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00022398 File Offset: 0x00020598
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x000223A0 File Offset: 0x000205A0
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000223A9 File Offset: 0x000205A9
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x000223B1 File Offset: 0x000205B1
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

		// Token: 0x0400067C RID: 1660
		private string dataSourceNameField;

		// Token: 0x0400067D RID: 1661
		private string userNameField;

		// Token: 0x0400067E RID: 1662
		private string passwordField;
	}
}
