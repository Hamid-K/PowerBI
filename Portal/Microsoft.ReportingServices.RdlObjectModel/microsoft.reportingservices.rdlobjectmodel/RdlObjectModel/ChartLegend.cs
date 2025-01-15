using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000088 RID: 136
	public class ChartLegend : ReportObject, INamedObject
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000186A9 File Offset: 0x000168A9
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x000186BC File Offset: 0x000168BC
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000186CB File Offset: 0x000168CB
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x000186D9 File Offset: 0x000168D9
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000186ED File Offset: 0x000168ED
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00018700 File Offset: 0x00016900
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001870F File Offset: 0x0001690F
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x0001871D File Offset: 0x0001691D
		[ReportExpressionDefaultValue(typeof(ChartPositions), ChartPositions.RightTop)]
		public ReportExpression<ChartPositions> Position
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartPositions>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00018731 File Offset: 0x00016931
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001873F File Offset: 0x0001693F
		[ReportExpressionDefaultValue(typeof(ChartLegendLayouts), ChartLegendLayouts.AutoTable)]
		public ReportExpression<ChartLegendLayouts> Layout
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartLegendLayouts>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00018753 File Offset: 0x00016953
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00018766 File Offset: 0x00016966
		[DefaultValue("")]
		public string DockToChartArea
		{
			get
			{
				return (string)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00018775 File Offset: 0x00016975
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00018783 File Offset: 0x00016983
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> DockOutsideChartArea
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00018797 File Offset: 0x00016997
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x000187AA File Offset: 0x000169AA
		public ChartElementPosition ChartElementPosition
		{
			get
			{
				return (ChartElementPosition)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000187B9 File Offset: 0x000169B9
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x000187CD File Offset: 0x000169CD
		public ChartLegendTitle ChartLegendTitle
		{
			get
			{
				return (ChartLegendTitle)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000187DD File Offset: 0x000169DD
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x000187EC File Offset: 0x000169EC
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AutoFitTextDisabled
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00018801 File Offset: 0x00016A01
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00018810 File Offset: 0x00016A10
		[ReportExpressionDefaultValue(typeof(ReportSize), "7pt")]
		public ReportExpression<ReportSize> MinFontSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00018825 File Offset: 0x00016A25
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00018839 File Offset: 0x00016A39
		[XmlElement(typeof(RdlCollection<ChartLegendColumn>))]
		public IList<ChartLegendColumn> ChartLegendColumns
		{
			get
			{
				return (IList<ChartLegendColumn>)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00018849 File Offset: 0x00016A49
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00018858 File Offset: 0x00016A58
		[ReportExpressionDefaultValue(typeof(ChartHeaderSeparatorTypes), ChartHeaderSeparatorTypes.None)]
		public ReportExpression<ChartHeaderSeparatorTypes> HeaderSeparator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartHeaderSeparatorTypes>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001886D File Offset: 0x00016A6D
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001887C File Offset: 0x00016A7C
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> HeaderSeparatorColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00018891 File Offset: 0x00016A91
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x000188A0 File Offset: 0x00016AA0
		[ReportExpressionDefaultValue(typeof(ChartColumnSeparatorTypes), ChartColumnSeparatorTypes.None)]
		public ReportExpression<ChartColumnSeparatorTypes> ColumnSeparator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartColumnSeparatorTypes>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x000188B5 File Offset: 0x00016AB5
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x000188C4 File Offset: 0x00016AC4
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> ColumnSeparatorColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000188D9 File Offset: 0x00016AD9
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x000188E8 File Offset: 0x00016AE8
		[ReportExpressionDefaultValue(typeof(int), 50)]
		public ReportExpression<int> ColumnSpacing
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000188FD File Offset: 0x00016AFD
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001890C File Offset: 0x00016B0C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> InterlacedRows
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00018921 File Offset: 0x00016B21
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00018930 File Offset: 0x00016B30
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> InterlacedRowsColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00018945 File Offset: 0x00016B45
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00018954 File Offset: 0x00016B54
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> EquallySpacedItems
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00018969 File Offset: 0x00016B69
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00018978 File Offset: 0x00016B78
		[ReportExpressionDefaultValue(typeof(ChartLegendReversedTypes), ChartLegendReversedTypes.Auto)]
		public ReportExpression<ChartLegendReversedTypes> Reversed
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartLegendReversedTypes>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001898D File Offset: 0x00016B8D
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001899C File Offset: 0x00016B9C
		[ReportExpressionDefaultValue(typeof(int), 50)]
		public ReportExpression<int> MaxAutoSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000189B1 File Offset: 0x00016BB1
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x000189C0 File Offset: 0x00016BC0
		[ReportExpressionDefaultValue(typeof(int), 25)]
		public ReportExpression<int> TextWrapThreshold
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000189D5 File Offset: 0x00016BD5
		public ChartLegend()
		{
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000189DD File Offset: 0x00016BDD
		internal ChartLegend(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000189E8 File Offset: 0x00016BE8
		public override void Initialize()
		{
			base.Initialize();
			this.ChartLegendColumns = new RdlCollection<ChartLegendColumn>();
			this.Position = ChartPositions.RightTop;
			this.Layout = ChartLegendLayouts.AutoTable;
			this.ColumnSpacing = 50;
			this.MaxAutoSize = 50;
			this.TextWrapThreshold = 25;
		}

		// Token: 0x0200033E RID: 830
		internal class Definition : DefinitionStore<ChartLegend, ChartLegend.Definition.Properties>
		{
			// Token: 0x060017C1 RID: 6081 RVA: 0x0003AB2B File Offset: 0x00038D2B
			private Definition()
			{
			}

			// Token: 0x0200045D RID: 1117
			internal enum Properties
			{
				// Token: 0x0400096E RID: 2414
				Name,
				// Token: 0x0400096F RID: 2415
				Hidden,
				// Token: 0x04000970 RID: 2416
				Style,
				// Token: 0x04000971 RID: 2417
				Position,
				// Token: 0x04000972 RID: 2418
				Layout,
				// Token: 0x04000973 RID: 2419
				Docking,
				// Token: 0x04000974 RID: 2420
				DockToChartArea,
				// Token: 0x04000975 RID: 2421
				DockOutsideChartArea,
				// Token: 0x04000976 RID: 2422
				ChartElementPosition,
				// Token: 0x04000977 RID: 2423
				ChartLegendTitle,
				// Token: 0x04000978 RID: 2424
				AutoFitTextDisabled,
				// Token: 0x04000979 RID: 2425
				MinFontSize,
				// Token: 0x0400097A RID: 2426
				ChartLegendColumns,
				// Token: 0x0400097B RID: 2427
				HeaderSeparator,
				// Token: 0x0400097C RID: 2428
				HeaderSeparatorColor,
				// Token: 0x0400097D RID: 2429
				ColumnSeparator,
				// Token: 0x0400097E RID: 2430
				ColumnSeparatorColor,
				// Token: 0x0400097F RID: 2431
				ColumnSpacing,
				// Token: 0x04000980 RID: 2432
				InterlacedRows,
				// Token: 0x04000981 RID: 2433
				InterlacedRowsColor,
				// Token: 0x04000982 RID: 2434
				EquallySpacedItems,
				// Token: 0x04000983 RID: 2435
				Reversed,
				// Token: 0x04000984 RID: 2436
				MaxAutoSize,
				// Token: 0x04000985 RID: 2437
				TextWrapThreshold,
				// Token: 0x04000986 RID: 2438
				PropertyCount
			}
		}
	}
}
