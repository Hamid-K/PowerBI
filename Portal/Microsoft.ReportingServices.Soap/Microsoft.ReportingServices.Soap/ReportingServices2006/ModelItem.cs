using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000151 RID: 337
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ModelItem
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x000176DC File Offset: 0x000158DC
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x000176E4 File Offset: 0x000158E4
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x000176ED File Offset: 0x000158ED
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x000176F5 File Offset: 0x000158F5
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

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x000176FE File Offset: 0x000158FE
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00017706 File Offset: 0x00015906
		public ModelItemTypeEnum Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0001770F File Offset: 0x0001590F
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00017717 File Offset: 0x00015917
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00017720 File Offset: 0x00015920
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00017728 File Offset: 0x00015928
		public ModelItem[] ModelItems
		{
			get
			{
				return this.modelItemsField;
			}
			set
			{
				this.modelItemsField = value;
			}
		}

		// Token: 0x0400045B RID: 1115
		private string idField;

		// Token: 0x0400045C RID: 1116
		private string nameField;

		// Token: 0x0400045D RID: 1117
		private ModelItemTypeEnum typeField;

		// Token: 0x0400045E RID: 1118
		private string descriptionField;

		// Token: 0x0400045F RID: 1119
		private ModelItem[] modelItemsField;
	}
}
