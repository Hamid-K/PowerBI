using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000231 RID: 561
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class ModelItem
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00022537 File Offset: 0x00020737
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x0002253F File Offset: 0x0002073F
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00022548 File Offset: 0x00020748
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00022550 File Offset: 0x00020750
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00022559 File Offset: 0x00020759
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x00022561 File Offset: 0x00020761
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

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0002256A File Offset: 0x0002076A
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00022572 File Offset: 0x00020772
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

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0002257B File Offset: 0x0002077B
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00022583 File Offset: 0x00020783
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

		// Token: 0x0400069F RID: 1695
		private string idField;

		// Token: 0x040006A0 RID: 1696
		private string nameField;

		// Token: 0x040006A1 RID: 1697
		private ModelItemTypeEnum typeField;

		// Token: 0x040006A2 RID: 1698
		private string descriptionField;

		// Token: 0x040006A3 RID: 1699
		private ModelItem[] modelItemsField;
	}
}
