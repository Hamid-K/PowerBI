using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000117 RID: 279
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Extension
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00016747 File Offset: 0x00014947
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x0001674F File Offset: 0x0001494F
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00016758 File Offset: 0x00014958
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00016760 File Offset: 0x00014960
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

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00016769 File Offset: 0x00014969
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00016771 File Offset: 0x00014971
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0001677A File Offset: 0x0001497A
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00016782 File Offset: 0x00014982
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0001678B File Offset: 0x0001498B
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00016793 File Offset: 0x00014993
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

		// Token: 0x04000349 RID: 841
		private ExtensionTypeEnum extensionTypeField;

		// Token: 0x0400034A RID: 842
		private string nameField;

		// Token: 0x0400034B RID: 843
		private string localizedNameField;

		// Token: 0x0400034C RID: 844
		private bool visibleField;

		// Token: 0x0400034D RID: 845
		private bool isModelGenerationSupportedField;
	}
}
