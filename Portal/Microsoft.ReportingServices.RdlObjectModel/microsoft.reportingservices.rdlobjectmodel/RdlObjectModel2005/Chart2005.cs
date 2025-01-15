using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000002 RID: 2
	internal class Chart2005 : Chart, IReportItem2005, IUpgradeable, IPageBreakLocation2005
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		[DefaultValue(ChartTypes2005.Column)]
		public ChartTypes2005 Type
		{
			get
			{
				return (ChartTypes2005)base.PropertyStore.GetInteger(41);
			}
			set
			{
				base.PropertyStore.SetInteger(41, (int)value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206F File Offset: 0x0000026F
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000207E File Offset: 0x0000027E
		[DefaultValue(ChartSubtypes2005.Plain)]
		public ChartSubtypes2005 Subtype
		{
			get
			{
				return (ChartSubtypes2005)base.PropertyStore.GetInteger(42);
			}
			set
			{
				base.PropertyStore.SetInteger(42, (int)value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000208E File Offset: 0x0000028E
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020A2 File Offset: 0x000002A2
		[XmlElement(typeof(RdlCollection<SeriesGrouping2005>))]
		[XmlArrayItem("SeriesGrouping", typeof(SeriesGrouping2005))]
		public IList<SeriesGrouping2005> SeriesGroupings
		{
			get
			{
				return (IList<SeriesGrouping2005>)base.PropertyStore.GetObject(43);
			}
			set
			{
				base.PropertyStore.SetObject(43, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020B2 File Offset: 0x000002B2
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020C6 File Offset: 0x000002C6
		[XmlElement(typeof(RdlCollection<CategoryGrouping2005>))]
		[XmlArrayItem("CategoryGrouping", typeof(CategoryGrouping2005))]
		public IList<CategoryGrouping2005> CategoryGroupings
		{
			get
			{
				return (IList<CategoryGrouping2005>)base.PropertyStore.GetObject(44);
			}
			set
			{
				base.PropertyStore.SetObject(44, value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020D6 File Offset: 0x000002D6
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020EA File Offset: 0x000002EA
		[XmlElement(typeof(RdlCollection<ChartSeries2005>))]
		[XmlArrayItem("ChartSeries", typeof(ChartSeries2005))]
		public new IList<ChartSeries2005> ChartData
		{
			get
			{
				return (IList<ChartSeries2005>)base.PropertyStore.GetObject(45);
			}
			set
			{
				base.PropertyStore.SetObject(45, value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000210E File Offset: 0x0000030E
		public Legend2005 Legend
		{
			get
			{
				return (Legend2005)base.PropertyStore.GetObject(46);
			}
			set
			{
				base.PropertyStore.SetObject(46, value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000211E File Offset: 0x0000031E
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002132 File Offset: 0x00000332
		public CategoryAxis2005 CategoryAxis
		{
			get
			{
				return (CategoryAxis2005)base.PropertyStore.GetObject(47);
			}
			set
			{
				base.PropertyStore.SetObject(47, value);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002142 File Offset: 0x00000342
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002156 File Offset: 0x00000356
		public ValueAxis2005 ValueAxis
		{
			get
			{
				return (ValueAxis2005)base.PropertyStore.GetObject(48);
			}
			set
			{
				base.PropertyStore.SetObject(48, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000217A File Offset: 0x0000037A
		public Title2005 Title
		{
			get
			{
				return (Title2005)base.PropertyStore.GetObject(49);
			}
			set
			{
				base.PropertyStore.SetObject(49, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000218A File Offset: 0x0000038A
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002199 File Offset: 0x00000399
		[DefaultValue(0)]
		public int PointWidth
		{
			get
			{
				return base.PropertyStore.GetInteger(50);
			}
			set
			{
				base.PropertyStore.SetInteger(50, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000021A9 File Offset: 0x000003A9
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000021BD File Offset: 0x000003BD
		public ThreeDProperties2005 ThreeDProperties
		{
			get
			{
				return (ThreeDProperties2005)base.PropertyStore.GetObject(52);
			}
			set
			{
				base.PropertyStore.SetObject(52, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021E1 File Offset: 0x000003E1
		public PlotArea2005 PlotArea
		{
			get
			{
				return (PlotArea2005)base.PropertyStore.GetObject(53);
			}
			set
			{
				base.PropertyStore.SetObject(53, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021F1 File Offset: 0x000003F1
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002200 File Offset: 0x00000400
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return base.PropertyStore.GetBoolean(39);
			}
			set
			{
				base.PropertyStore.SetBoolean(39, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002218 File Offset: 0x00000418
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002221 File Offset: 0x00000421
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002230 File Offset: 0x00000430
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return base.PropertyStore.GetBoolean(40);
			}
			set
			{
				base.PropertyStore.SetBoolean(40, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002240 File Offset: 0x00000440
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002254 File Offset: 0x00000454
		public Microsoft.ReportingServices.RdlObjectModel.Action Action
		{
			get
			{
				return (Microsoft.ReportingServices.RdlObjectModel.Action)base.PropertyStore.GetObject(38);
			}
			set
			{
				base.PropertyStore.SetObject(38, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002264 File Offset: 0x00000464
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000226C File Offset: 0x0000046C
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002275 File Offset: 0x00000475
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002289 File Offset: 0x00000489
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002299 File Offset: 0x00000499
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022A6 File Offset: 0x000004A6
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

		// Token: 0x06000027 RID: 39 RVA: 0x000022AF File Offset: 0x000004AF
		public Chart2005()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000022B7 File Offset: 0x000004B7
		public Chart2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000022C0 File Offset: 0x000004C0
		public override void Initialize()
		{
			base.Initialize();
			this.SeriesGroupings = new RdlCollection<SeriesGrouping2005>();
			this.CategoryGroupings = new RdlCollection<CategoryGrouping2005>();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000022DE File Offset: 0x000004DE
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeChart(this);
		}

		// Token: 0x020002EB RID: 747
		internal new class Definition : DefinitionStore<Chart2005, Chart2005.Definition.Properties>
		{
			// Token: 0x060016E7 RID: 5863 RVA: 0x000363D2 File Offset: 0x000345D2
			private Definition()
			{
			}

			// Token: 0x0200041F RID: 1055
			public enum Properties
			{
				// Token: 0x040007F2 RID: 2034
				Action = 38,
				// Token: 0x040007F3 RID: 2035
				PageBreakAtStart,
				// Token: 0x040007F4 RID: 2036
				PageBreakAtEnd,
				// Token: 0x040007F5 RID: 2037
				Type,
				// Token: 0x040007F6 RID: 2038
				Subtype,
				// Token: 0x040007F7 RID: 2039
				SeriesGroupings,
				// Token: 0x040007F8 RID: 2040
				CategoryGroupings,
				// Token: 0x040007F9 RID: 2041
				ChartData,
				// Token: 0x040007FA RID: 2042
				Legend,
				// Token: 0x040007FB RID: 2043
				CategoryAxis,
				// Token: 0x040007FC RID: 2044
				ValueAxis,
				// Token: 0x040007FD RID: 2045
				Title,
				// Token: 0x040007FE RID: 2046
				PointWidth,
				// Token: 0x040007FF RID: 2047
				Palette,
				// Token: 0x04000800 RID: 2048
				ThreeDProperties,
				// Token: 0x04000801 RID: 2049
				PlotArea,
				// Token: 0x04000802 RID: 2050
				PropertyCount
			}
		}
	}
}
