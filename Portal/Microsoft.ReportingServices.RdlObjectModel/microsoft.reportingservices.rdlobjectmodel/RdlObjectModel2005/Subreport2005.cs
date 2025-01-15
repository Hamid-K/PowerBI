using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000048 RID: 72
	internal class Subreport2005 : Subreport, IReportItem2005, IUpgradeable
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000044EA File Offset: 0x000026EA
		// (set) Token: 0x06000254 RID: 596 RVA: 0x000044F2 File Offset: 0x000026F2
		[ReportExpressionDefaultValue]
		public ReportExpression NoRows
		{
			get
			{
				return base.NoRowsMessage;
			}
			set
			{
				base.NoRowsMessage = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000044FB File Offset: 0x000026FB
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000450F File Offset: 0x0000270F
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000451F File Offset: 0x0000271F
		// (set) Token: 0x06000258 RID: 600 RVA: 0x00004527 File Offset: 0x00002727
		[ReportExpressionDefaultValue]
		public ReportExpression Label
		{
			get
			{
				return base.DocumentMapLabel;
			}
			set
			{
				base.DocumentMapLabel = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00004530 File Offset: 0x00002730
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00004544 File Offset: 0x00002744
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00004554 File Offset: 0x00002754
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00004561 File Offset: 0x00002761
		public new Style2005 Style
		{
			get
			{
				return (Style2005)base.Style;
			}
			set
			{
				base.Style = value;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000456A File Offset: 0x0000276A
		public Subreport2005()
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00004572 File Offset: 0x00002772
		public Subreport2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000457B File Offset: 0x0000277B
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeSubreport(this);
		}

		// Token: 0x02000316 RID: 790
		internal new class Definition : DefinitionStore<Subreport2005, Subreport2005.Definition.Properties>
		{
			// Token: 0x06001712 RID: 5906 RVA: 0x0003652A File Offset: 0x0003472A
			private Definition()
			{
			}

			// Token: 0x0200044A RID: 1098
			public enum Properties
			{
				// Token: 0x040008D9 RID: 2265
				Action = 24,
				// Token: 0x040008DA RID: 2266
				PropertyCount
			}
		}
	}
}
