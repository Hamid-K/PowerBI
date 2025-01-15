using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000076 RID: 118
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class Extension
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001B281 File Offset: 0x00019481
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001B289 File Offset: 0x00019489
		public ExtensionTypeEnum ExtensionType
		{
			get
			{
				return this.extensionTypeField;
			}
			set
			{
				this.extensionTypeField = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001B292 File Offset: 0x00019492
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001B29A File Offset: 0x0001949A
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001B2A3 File Offset: 0x000194A3
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001B2AB File Offset: 0x000194AB
		public string LocalizedName
		{
			get
			{
				return this.localizedNameField;
			}
			set
			{
				this.localizedNameField = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001B2B4 File Offset: 0x000194B4
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001B2BC File Offset: 0x000194BC
		public bool Visible
		{
			get
			{
				return this.visibleField;
			}
			set
			{
				this.visibleField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001B2C5 File Offset: 0x000194C5
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0001B2CD File Offset: 0x000194CD
		public bool IsModelGenerationSupported
		{
			get
			{
				return this.isModelGenerationSupportedField;
			}
			set
			{
				this.isModelGenerationSupportedField = value;
			}
		}

		// Token: 0x0400015C RID: 348
		private ExtensionTypeEnum extensionTypeField;

		// Token: 0x0400015D RID: 349
		private string nameField;

		// Token: 0x0400015E RID: 350
		private string localizedNameField;

		// Token: 0x0400015F RID: 351
		private bool visibleField;

		// Token: 0x04000160 RID: 352
		private bool isModelGenerationSupportedField;
	}
}
