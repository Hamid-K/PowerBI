using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008A RID: 138
	public class ChartLegendColumn : ReportObject, INamedObject
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00018ABC File Offset: 0x00016CBC
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00018ACF File Offset: 0x00016CCF
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00018ADE File Offset: 0x00016CDE
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x00018AEC File Offset: 0x00016CEC
		public ChartLegendColumnTypes ColumnType
		{
			get
			{
				return (ChartLegendColumnTypes)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00018AFB File Offset: 0x00016CFB
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x00018B09 File Offset: 0x00016D09
		[ReportExpressionDefaultValue]
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00018B1D File Offset: 0x00016D1D
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00018B30 File Offset: 0x00016D30
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00018B3F File Offset: 0x00016D3F
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00018B52 File Offset: 0x00016D52
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00018B61 File Offset: 0x00016D61
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00018B6F File Offset: 0x00016D6F
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00018B83 File Offset: 0x00016D83
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x00018B91 File Offset: 0x00016D91
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> MinimumWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00018BA5 File Offset: 0x00016DA5
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00018BB3 File Offset: 0x00016DB3
		[ReportExpressionDefaultValue(typeof(ReportSize))]
		public ReportExpression<ReportSize> MaximumWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00018BC7 File Offset: 0x00016DC7
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00018BD6 File Offset: 0x00016DD6
		[ReportExpressionDefaultValue(typeof(int), 200)]
		public ReportExpression<int> SeriesSymbolWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00018BEB File Offset: 0x00016DEB
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00018BFA File Offset: 0x00016DFA
		[ReportExpressionDefaultValue(typeof(int), 70)]
		public ReportExpression<int> SeriesSymbolHeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018C0F File Offset: 0x00016E0F
		public ChartLegendColumn()
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018C17 File Offset: 0x00016E17
		internal ChartLegendColumn(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00018C20 File Offset: 0x00016E20
		public override void Initialize()
		{
			base.Initialize();
			this.SeriesSymbolWidth = 200;
			this.SeriesSymbolHeight = 70;
		}

		// Token: 0x02000340 RID: 832
		internal class Definition : DefinitionStore<ChartLegendColumn, ChartLegendColumn.Definition.Properties>
		{
			// Token: 0x060017C3 RID: 6083 RVA: 0x0003AB3B File Offset: 0x00038D3B
			private Definition()
			{
			}

			// Token: 0x0200045F RID: 1119
			internal enum Properties
			{
				// Token: 0x0400098E RID: 2446
				Name,
				// Token: 0x0400098F RID: 2447
				ColumnType,
				// Token: 0x04000990 RID: 2448
				Value,
				// Token: 0x04000991 RID: 2449
				Style,
				// Token: 0x04000992 RID: 2450
				ActionInfo,
				// Token: 0x04000993 RID: 2451
				ToolTip,
				// Token: 0x04000994 RID: 2452
				ToolTipLocID,
				// Token: 0x04000995 RID: 2453
				MinimumWidth,
				// Token: 0x04000996 RID: 2454
				MaximumWidth,
				// Token: 0x04000997 RID: 2455
				SeriesSymbolWidth,
				// Token: 0x04000998 RID: 2456
				SeriesSymbolHeight,
				// Token: 0x04000999 RID: 2457
				PropertyCount
			}
		}
	}
}
