using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x0200000A RID: 10
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Extension
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000C700 File Offset: 0x0000A900
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000C708 File Offset: 0x0000A908
		public string ExtensionTypeName
		{
			get
			{
				return this.extensionTypeNameField;
			}
			set
			{
				this.extensionTypeNameField = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000C711 File Offset: 0x0000A911
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000C719 File Offset: 0x0000A919
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000C722 File Offset: 0x0000A922
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000C72A File Offset: 0x0000A92A
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000C733 File Offset: 0x0000A933
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000C73B File Offset: 0x0000A93B
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000C744 File Offset: 0x0000A944
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000C74C File Offset: 0x0000A94C
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

		// Token: 0x04000124 RID: 292
		private string extensionTypeNameField;

		// Token: 0x04000125 RID: 293
		private string nameField;

		// Token: 0x04000126 RID: 294
		private string localizedNameField;

		// Token: 0x04000127 RID: 295
		private bool visibleField;

		// Token: 0x04000128 RID: 296
		private bool isModelGenerationSupportedField;
	}
}
