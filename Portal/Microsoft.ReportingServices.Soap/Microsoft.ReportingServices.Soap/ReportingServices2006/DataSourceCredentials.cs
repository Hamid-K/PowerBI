using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000147 RID: 327
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSourceCredentials
	{
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00017405 File Offset: 0x00015605
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0001740D File Offset: 0x0001560D
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

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00017416 File Offset: 0x00015616
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0001741E File Offset: 0x0001561E
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

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00017427 File Offset: 0x00015627
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0001742F File Offset: 0x0001562F
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

		// Token: 0x04000429 RID: 1065
		private string dataSourceNameField;

		// Token: 0x0400042A RID: 1066
		private string userNameField;

		// Token: 0x0400042B RID: 1067
		private string passwordField;
	}
}
