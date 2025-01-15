using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x0200020B RID: 523
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class DataSourceDefinition : DataSourceDefinitionOrReference
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00021BC6 File Offset: 0x0001FDC6
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00021BCE File Offset: 0x0001FDCE
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00021BD7 File Offset: 0x0001FDD7
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x00021BDF File Offset: 0x0001FDDF
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

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00021BE8 File Offset: 0x0001FDE8
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x00021BF0 File Offset: 0x0001FDF0
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00021BF9 File Offset: 0x0001FDF9
		// (set) Token: 0x06001440 RID: 5184 RVA: 0x00021C01 File Offset: 0x0001FE01
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00021C0A File Offset: 0x0001FE0A
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x00021C12 File Offset: 0x0001FE12
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

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00021C1B File Offset: 0x0001FE1B
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x00021C23 File Offset: 0x0001FE23
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00021C2C File Offset: 0x0001FE2C
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00021C34 File Offset: 0x0001FE34
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00021C3D File Offset: 0x0001FE3D
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00021C45 File Offset: 0x0001FE45
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00021C4E File Offset: 0x0001FE4E
		// (set) Token: 0x0600144A RID: 5194 RVA: 0x00021C56 File Offset: 0x0001FE56
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00021C5F File Offset: 0x0001FE5F
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00021C67 File Offset: 0x0001FE67
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

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00021C70 File Offset: 0x0001FE70
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00021C78 File Offset: 0x0001FE78
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

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00021C81 File Offset: 0x0001FE81
		// (set) Token: 0x06001450 RID: 5200 RVA: 0x00021C89 File Offset: 0x0001FE89
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00021C92 File Offset: 0x0001FE92
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00021C9A File Offset: 0x0001FE9A
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

		// Token: 0x040005EB RID: 1515
		private string extensionField;

		// Token: 0x040005EC RID: 1516
		private string connectStringField;

		// Token: 0x040005ED RID: 1517
		private bool useOriginalConnectStringField;

		// Token: 0x040005EE RID: 1518
		private bool originalConnectStringExpressionBasedField;

		// Token: 0x040005EF RID: 1519
		private CredentialRetrievalEnum credentialRetrievalField;

		// Token: 0x040005F0 RID: 1520
		private bool windowsCredentialsField;

		// Token: 0x040005F1 RID: 1521
		private bool impersonateUserField;

		// Token: 0x040005F2 RID: 1522
		private bool impersonateUserFieldSpecified;

		// Token: 0x040005F3 RID: 1523
		private string promptField;

		// Token: 0x040005F4 RID: 1524
		private string userNameField;

		// Token: 0x040005F5 RID: 1525
		private string passwordField;

		// Token: 0x040005F6 RID: 1526
		private bool enabledField;

		// Token: 0x040005F7 RID: 1527
		private bool enabledFieldSpecified;
	}
}
