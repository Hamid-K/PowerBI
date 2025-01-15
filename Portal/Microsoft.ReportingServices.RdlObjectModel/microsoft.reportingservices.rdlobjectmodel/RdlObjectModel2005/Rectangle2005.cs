using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000033 RID: 51
	internal class Rectangle2005 : Rectangle, IReportItem2005, IUpgradeable, IPageBreakLocation2005
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003980 File Offset: 0x00001B80
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00003994 File Offset: 0x00001B94
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000039A4 File Offset: 0x00001BA4
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000039AC File Offset: 0x00001BAC
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000039B5 File Offset: 0x00001BB5
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000039C9 File Offset: 0x00001BC9
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000039D9 File Offset: 0x00001BD9
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000039E6 File Offset: 0x00001BE6
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000039EF File Offset: 0x00001BEF
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000039FE File Offset: 0x00001BFE
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(25);
			}
			set
			{
				base.PropertyStore.SetBoolean(25, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003A0E File Offset: 0x00001C0E
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00003A1D File Offset: 0x00001C1D
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(26);
			}
			set
			{
				base.PropertyStore.SetBoolean(26, value);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00003A2D File Offset: 0x00001C2D
		public Rectangle2005()
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00003A35 File Offset: 0x00001C35
		public Rectangle2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00003A3E File Offset: 0x00001C3E
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeRectangle(this);
		}

		// Token: 0x0200030D RID: 781
		internal new class Definition : DefinitionStore<Rectangle2005, Rectangle2005.Definition.Properties>
		{
			// Token: 0x06001709 RID: 5897 RVA: 0x000364E2 File Offset: 0x000346E2
			private Definition()
			{
			}

			// Token: 0x02000441 RID: 1089
			public enum Properties
			{
				// Token: 0x040008A6 RID: 2214
				Action = 24,
				// Token: 0x040008A7 RID: 2215
				PageBreakAtStart,
				// Token: 0x040008A8 RID: 2216
				PageBreakAtEnd,
				// Token: 0x040008A9 RID: 2217
				PropertyCount
			}
		}
	}
}
