using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000032 RID: 50
	internal class Line2005 : Line, IReportItem2005, IUpgradeable
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000038F7 File Offset: 0x00001AF7
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000390B File Offset: 0x00001B0B
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000391B File Offset: 0x00001B1B
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00003923 File Offset: 0x00001B23
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000392C File Offset: 0x00001B2C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00003940 File Offset: 0x00001B40
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003950 File Offset: 0x00001B50
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000395D File Offset: 0x00001B5D
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

		// Token: 0x060001AF RID: 431 RVA: 0x00003966 File Offset: 0x00001B66
		public Line2005()
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000396E File Offset: 0x00001B6E
		public Line2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003977 File Offset: 0x00001B77
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeReportItem(this);
		}

		// Token: 0x0200030C RID: 780
		internal new class Definition : DefinitionStore<Line2005, Line2005.Definition.Properties>
		{
			// Token: 0x06001708 RID: 5896 RVA: 0x000364DA File Offset: 0x000346DA
			private Definition()
			{
			}

			// Token: 0x02000440 RID: 1088
			public enum Properties
			{
				// Token: 0x040008A3 RID: 2211
				Action = 18,
				// Token: 0x040008A4 RID: 2212
				PropertyCount
			}
		}
	}
}
