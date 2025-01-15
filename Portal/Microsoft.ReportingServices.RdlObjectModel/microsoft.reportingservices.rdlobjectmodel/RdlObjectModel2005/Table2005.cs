using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000049 RID: 73
	internal class Table2005 : Tablix, IReportItem2005, IUpgradeable, IPageBreakLocation2005
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00004584 File Offset: 0x00002784
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00004598 File Offset: 0x00002798
		[XmlElement(typeof(RdlCollection<TableColumn2005>))]
		[XmlArrayItem("TableColumn", typeof(TableColumn2005))]
		public IList<TableColumn2005> TableColumns
		{
			get
			{
				return (IList<TableColumn2005>)base.PropertyStore.GetObject(39);
			}
			set
			{
				base.PropertyStore.SetObject(39, value);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000045A8 File Offset: 0x000027A8
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000045BC File Offset: 0x000027BC
		public Header2005 Header
		{
			get
			{
				return (Header2005)base.PropertyStore.GetObject(40);
			}
			set
			{
				base.PropertyStore.SetObject(40, value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000045CC File Offset: 0x000027CC
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000045E0 File Offset: 0x000027E0
		[XmlElement(typeof(RdlCollection<TableGroup2005>))]
		[XmlArrayItem("TableGroup", typeof(TableGroup2005))]
		public IList<TableGroup2005> TableGroups
		{
			get
			{
				return (IList<TableGroup2005>)base.PropertyStore.GetObject(41);
			}
			set
			{
				base.PropertyStore.SetObject(41, value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000045F0 File Offset: 0x000027F0
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00004604 File Offset: 0x00002804
		public Details2005 Details
		{
			get
			{
				return (Details2005)base.PropertyStore.GetObject(42);
			}
			set
			{
				base.PropertyStore.SetObject(42, value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00004614 File Offset: 0x00002814
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00004628 File Offset: 0x00002828
		public Footer2005 Footer
		{
			get
			{
				return (Footer2005)base.PropertyStore.GetObject(43);
			}
			set
			{
				base.PropertyStore.SetObject(43, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00004638 File Offset: 0x00002838
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000464C File Offset: 0x0000284C
		[DefaultValue("")]
		public string DetailDataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(44);
			}
			set
			{
				base.PropertyStore.SetObject(44, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000465C File Offset: 0x0000285C
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00004670 File Offset: 0x00002870
		[DefaultValue("")]
		public string DetailDataCollectionName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(45);
			}
			set
			{
				base.PropertyStore.SetObject(45, value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00004680 File Offset: 0x00002880
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000468F File Offset: 0x0000288F
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues(typeof(Constants2005), "Table2005DetailDataElementOutputTypes")]
		public DataElementOutputTypes DetailDataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(46);
			}
			set
			{
				base.PropertyStore.SetInteger(46, (int)value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000469F File Offset: 0x0000289F
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000046AE File Offset: 0x000028AE
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(37);
			}
			set
			{
				base.PropertyStore.SetBoolean(37, value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000046BE File Offset: 0x000028BE
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000046C6 File Offset: 0x000028C6
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000046CF File Offset: 0x000028CF
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000046DE File Offset: 0x000028DE
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(38);
			}
			set
			{
				base.PropertyStore.SetBoolean(38, value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00004702 File Offset: 0x00002902
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00004712 File Offset: 0x00002912
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000471A File Offset: 0x0000291A
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00004723 File Offset: 0x00002923
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00004737 File Offset: 0x00002937
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00004747 File Offset: 0x00002947
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00004754 File Offset: 0x00002954
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

		// Token: 0x0600027E RID: 638 RVA: 0x0000475D File Offset: 0x0000295D
		public Table2005()
		{
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00004765 File Offset: 0x00002965
		public Table2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000476E File Offset: 0x0000296E
		public override void Initialize()
		{
			base.Initialize();
			this.TableColumns = new RdlCollection<TableColumn2005>();
			this.TableGroups = new RdlCollection<TableGroup2005>();
			this.DetailDataElementOutput = DataElementOutputTypes.Output;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004793 File Offset: 0x00002993
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeTable(this);
		}

		// Token: 0x02000317 RID: 791
		public new class Definition : DefinitionStore<Table2005, Table2005.Definition.Properties>
		{
			// Token: 0x06001713 RID: 5907 RVA: 0x00036532 File Offset: 0x00034732
			private Definition()
			{
			}

			// Token: 0x0200044B RID: 1099
			public enum Properties
			{
				// Token: 0x040008DC RID: 2268
				Action = 36,
				// Token: 0x040008DD RID: 2269
				PageBreakAtStart,
				// Token: 0x040008DE RID: 2270
				PageBreakAtEnd,
				// Token: 0x040008DF RID: 2271
				TableColumns,
				// Token: 0x040008E0 RID: 2272
				Header,
				// Token: 0x040008E1 RID: 2273
				TableGroups,
				// Token: 0x040008E2 RID: 2274
				Details,
				// Token: 0x040008E3 RID: 2275
				Footer,
				// Token: 0x040008E4 RID: 2276
				DetailDataElementName,
				// Token: 0x040008E5 RID: 2277
				DetailDataCollectionName,
				// Token: 0x040008E6 RID: 2278
				DetailDataElementOutput
			}
		}
	}
}
