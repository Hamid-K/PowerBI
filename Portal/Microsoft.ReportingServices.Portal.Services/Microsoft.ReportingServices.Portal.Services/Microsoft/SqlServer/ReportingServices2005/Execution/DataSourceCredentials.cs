using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x0200007D RID: 125
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class DataSourceCredentials
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001B388 File Offset: 0x00019588
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001B390 File Offset: 0x00019590
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001B399 File Offset: 0x00019599
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001B3A1 File Offset: 0x000195A1
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001B3AA File Offset: 0x000195AA
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001B3B2 File Offset: 0x000195B2
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

		// Token: 0x04000170 RID: 368
		private string dataSourceNameField;

		// Token: 0x04000171 RID: 369
		private string userNameField;

		// Token: 0x04000172 RID: 370
		private string passwordField;
	}
}
