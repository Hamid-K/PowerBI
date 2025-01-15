using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001FC RID: 508
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Extension
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x000216C9 File Offset: 0x0001F8C9
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x000216D1 File Offset: 0x0001F8D1
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x000216DA File Offset: 0x0001F8DA
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x000216E2 File Offset: 0x0001F8E2
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

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000216EB File Offset: 0x0001F8EB
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x000216F3 File Offset: 0x0001F8F3
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000216FC File Offset: 0x0001F8FC
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00021704 File Offset: 0x0001F904
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

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0002170D File Offset: 0x0001F90D
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00021715 File Offset: 0x0001F915
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

		// Token: 0x0400059D RID: 1437
		private ExtensionTypeEnum extensionTypeField;

		// Token: 0x0400059E RID: 1438
		private string nameField;

		// Token: 0x0400059F RID: 1439
		private string localizedNameField;

		// Token: 0x040005A0 RID: 1440
		private bool visibleField;

		// Token: 0x040005A1 RID: 1441
		private bool isModelGenerationSupportedField;
	}
}
