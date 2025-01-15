using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008D RID: 141
	public class ChartLegendCustomItemCell : ReportObject, INamedObject
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00018DCE File Offset: 0x00016FCE
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00018DE1 File Offset: 0x00016FE1
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00018DF0 File Offset: 0x00016FF0
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00018DFE File Offset: 0x00016FFE
		public ChartLegendItemCellTypes CellType
		{
			get
			{
				return (ChartLegendItemCellTypes)base.PropertyStore.GetInteger(1);
			}
			set
			{
				base.PropertyStore.SetInteger(1, (int)value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00018E0D File Offset: 0x0001700D
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00018E1B File Offset: 0x0001701B
		[ReportExpressionDefaultValue]
		public ReportExpression Text
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

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00018E2F File Offset: 0x0001702F
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00018E3D File Offset: 0x0001703D
		[DefaultValue(1)]
		[ValidValues(0, 2147483647)]
		public int CellSpan
		{
			get
			{
				return base.PropertyStore.GetInteger(3);
			}
			set
			{
				((IntProperty)DefinitionStore<ChartLegendCustomItemCell, ChartLegendCustomItemCell.Definition.Properties>.GetProperty(3)).Validate(this, value);
				base.PropertyStore.SetInteger(3, value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00018E5E File Offset: 0x0001705E
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x00018E71 File Offset: 0x00017071
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00018E80 File Offset: 0x00017080
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00018E93 File Offset: 0x00017093
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00018EA2 File Offset: 0x000170A2
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x00018EB0 File Offset: 0x000170B0
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00018EC4 File Offset: 0x000170C4
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x00018ED2 File Offset: 0x000170D2
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> ImageHeight
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00018EE6 File Offset: 0x000170E6
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00018EF5 File Offset: 0x000170F5
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> ImageWidth
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

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00018F0A File Offset: 0x0001710A
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x00018F19 File Offset: 0x00017119
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> SymbolHeight
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x00018F2E File Offset: 0x0001712E
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00018F3D File Offset: 0x0001713D
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> SymbolWidth
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00018F52 File Offset: 0x00017152
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00018F61 File Offset: 0x00017161
		[DefaultValue(ChartLegendItemAlignmentTypes.Center)]
		public ChartLegendItemAlignmentTypes Alignment
		{
			get
			{
				return (ChartLegendItemAlignmentTypes)base.PropertyStore.GetInteger(12);
			}
			set
			{
				base.PropertyStore.SetInteger(12, (int)value);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00018F71 File Offset: 0x00017171
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00018F80 File Offset: 0x00017180
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> TopMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00018F95 File Offset: 0x00017195
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00018FA4 File Offset: 0x000171A4
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> BottomMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00018FB9 File Offset: 0x000171B9
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00018FC8 File Offset: 0x000171C8
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> LeftMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018FDD File Offset: 0x000171DD
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00018FEC File Offset: 0x000171EC
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> RightMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019001 File Offset: 0x00017201
		public ChartLegendCustomItemCell()
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00019009 File Offset: 0x00017209
		internal ChartLegendCustomItemCell(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000343 RID: 835
		internal class Definition : DefinitionStore<ChartLegendCustomItemCell, ChartLegendCustomItemCell.Definition.Properties>
		{
			// Token: 0x060017C6 RID: 6086 RVA: 0x0003AB53 File Offset: 0x00038D53
			private Definition()
			{
			}

			// Token: 0x02000462 RID: 1122
			internal enum Properties
			{
				// Token: 0x040009AA RID: 2474
				Name,
				// Token: 0x040009AB RID: 2475
				CellType,
				// Token: 0x040009AC RID: 2476
				Text,
				// Token: 0x040009AD RID: 2477
				CellSpan,
				// Token: 0x040009AE RID: 2478
				Style,
				// Token: 0x040009AF RID: 2479
				ActionInfo,
				// Token: 0x040009B0 RID: 2480
				ToolTip,
				// Token: 0x040009B1 RID: 2481
				ToolTipLocID,
				// Token: 0x040009B2 RID: 2482
				ImageHeight,
				// Token: 0x040009B3 RID: 2483
				ImageWidth,
				// Token: 0x040009B4 RID: 2484
				SymbolHeight,
				// Token: 0x040009B5 RID: 2485
				SymbolWidth,
				// Token: 0x040009B6 RID: 2486
				Alignment,
				// Token: 0x040009B7 RID: 2487
				TopMargin,
				// Token: 0x040009B8 RID: 2488
				BottomMargin,
				// Token: 0x040009B9 RID: 2489
				LeftMargin,
				// Token: 0x040009BA RID: 2490
				RightMargin,
				// Token: 0x040009BB RID: 2491
				PropertyCount
			}
		}
	}
}
