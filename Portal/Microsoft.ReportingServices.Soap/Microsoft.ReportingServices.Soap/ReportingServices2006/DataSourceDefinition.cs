using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000126 RID: 294
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class DataSourceDefinition : DataSourceDefinitionOrReference
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x00016C44 File Offset: 0x00014E44
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x00016C4C File Offset: 0x00014E4C
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

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x00016C55 File Offset: 0x00014E55
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x00016C5D File Offset: 0x00014E5D
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

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00016C66 File Offset: 0x00014E66
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00016C6E File Offset: 0x00014E6E
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x00016C77 File Offset: 0x00014E77
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x00016C7F File Offset: 0x00014E7F
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x00016C88 File Offset: 0x00014E88
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x00016C90 File Offset: 0x00014E90
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

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00016C99 File Offset: 0x00014E99
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x00016CA1 File Offset: 0x00014EA1
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00016CAA File Offset: 0x00014EAA
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x00016CB2 File Offset: 0x00014EB2
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00016CBB File Offset: 0x00014EBB
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x00016CC3 File Offset: 0x00014EC3
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

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00016CCC File Offset: 0x00014ECC
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00016CD4 File Offset: 0x00014ED4
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

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00016CDD File Offset: 0x00014EDD
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00016CE5 File Offset: 0x00014EE5
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

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00016CEE File Offset: 0x00014EEE
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00016CF6 File Offset: 0x00014EF6
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

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00016CFF File Offset: 0x00014EFF
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00016D07 File Offset: 0x00014F07
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00016D10 File Offset: 0x00014F10
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00016D18 File Offset: 0x00014F18
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

		// Token: 0x04000397 RID: 919
		private string extensionField;

		// Token: 0x04000398 RID: 920
		private string connectStringField;

		// Token: 0x04000399 RID: 921
		private bool useOriginalConnectStringField;

		// Token: 0x0400039A RID: 922
		private bool originalConnectStringExpressionBasedField;

		// Token: 0x0400039B RID: 923
		private CredentialRetrievalEnum credentialRetrievalField;

		// Token: 0x0400039C RID: 924
		private bool windowsCredentialsField;

		// Token: 0x0400039D RID: 925
		private bool impersonateUserField;

		// Token: 0x0400039E RID: 926
		private bool impersonateUserFieldSpecified;

		// Token: 0x0400039F RID: 927
		private string promptField;

		// Token: 0x040003A0 RID: 928
		private string userNameField;

		// Token: 0x040003A1 RID: 929
		private string passwordField;

		// Token: 0x040003A2 RID: 930
		private bool enabledField;

		// Token: 0x040003A3 RID: 931
		private bool enabledFieldSpecified;
	}
}
