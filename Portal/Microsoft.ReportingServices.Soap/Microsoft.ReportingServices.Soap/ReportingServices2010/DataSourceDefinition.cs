using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000029 RID: 41
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class DataSourceDefinition : DataSourceDefinitionOrReference
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000CEF2 File Offset: 0x0000B0F2
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000CEFA File Offset: 0x0000B0FA
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000CF03 File Offset: 0x0000B103
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000CF0B File Offset: 0x0000B10B
		public string ConnectString
		{
			get
			{
				return this.connectStringField;
			}
			set
			{
				this.connectStringField = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000CF14 File Offset: 0x0000B114
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public bool UseOriginalConnectString
		{
			get
			{
				return this.useOriginalConnectStringField;
			}
			set
			{
				this.useOriginalConnectStringField = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000CF25 File Offset: 0x0000B125
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000CF2D File Offset: 0x0000B12D
		public bool OriginalConnectStringExpressionBased
		{
			get
			{
				return this.originalConnectStringExpressionBasedField;
			}
			set
			{
				this.originalConnectStringExpressionBasedField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000CF36 File Offset: 0x0000B136
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000CF3E File Offset: 0x0000B13E
		public CredentialRetrievalEnum CredentialRetrieval
		{
			get
			{
				return this.credentialRetrievalField;
			}
			set
			{
				this.credentialRetrievalField = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000CF47 File Offset: 0x0000B147
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000CF4F File Offset: 0x0000B14F
		public bool WindowsCredentials
		{
			get
			{
				return this.windowsCredentialsField;
			}
			set
			{
				this.windowsCredentialsField = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000CF58 File Offset: 0x0000B158
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000CF60 File Offset: 0x0000B160
		public bool ImpersonateUser
		{
			get
			{
				return this.impersonateUserField;
			}
			set
			{
				this.impersonateUserField = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000CF69 File Offset: 0x0000B169
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000CF71 File Offset: 0x0000B171
		[XmlIgnore]
		public bool ImpersonateUserSpecified
		{
			get
			{
				return this.impersonateUserFieldSpecified;
			}
			set
			{
				this.impersonateUserFieldSpecified = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000CF7A File Offset: 0x0000B17A
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000CF82 File Offset: 0x0000B182
		public string Prompt
		{
			get
			{
				return this.promptField;
			}
			set
			{
				this.promptField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000CF8B File Offset: 0x0000B18B
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000CF93 File Offset: 0x0000B193
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000CF9C File Offset: 0x0000B19C
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000CFAD File Offset: 0x0000B1AD
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000CFB5 File Offset: 0x0000B1B5
		public bool Enabled
		{
			get
			{
				return this.enabledField;
			}
			set
			{
				this.enabledField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000CFBE File Offset: 0x0000B1BE
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000CFC6 File Offset: 0x0000B1C6
		[XmlIgnore]
		public bool EnabledSpecified
		{
			get
			{
				return this.enabledFieldSpecified;
			}
			set
			{
				this.enabledFieldSpecified = value;
			}
		}

		// Token: 0x04000197 RID: 407
		private string extensionField;

		// Token: 0x04000198 RID: 408
		private string connectStringField;

		// Token: 0x04000199 RID: 409
		private bool useOriginalConnectStringField;

		// Token: 0x0400019A RID: 410
		private bool originalConnectStringExpressionBasedField;

		// Token: 0x0400019B RID: 411
		private CredentialRetrievalEnum credentialRetrievalField;

		// Token: 0x0400019C RID: 412
		private bool windowsCredentialsField;

		// Token: 0x0400019D RID: 413
		private bool impersonateUserField;

		// Token: 0x0400019E RID: 414
		private bool impersonateUserFieldSpecified;

		// Token: 0x0400019F RID: 415
		private string promptField;

		// Token: 0x040001A0 RID: 416
		private string userNameField;

		// Token: 0x040001A1 RID: 417
		private string passwordField;

		// Token: 0x040001A2 RID: 418
		private bool enabledField;

		// Token: 0x040001A3 RID: 419
		private bool enabledFieldSpecified;
	}
}
