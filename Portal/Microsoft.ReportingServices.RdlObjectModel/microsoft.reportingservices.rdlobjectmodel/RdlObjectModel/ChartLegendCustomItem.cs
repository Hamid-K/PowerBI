using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200008C RID: 140
	public class ChartLegendCustomItem : ReportObject, INamedObject
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00018C9A File Offset: 0x00016E9A
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00018CAD File Offset: 0x00016EAD
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

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00018CBC File Offset: 0x00016EBC
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00018CCF File Offset: 0x00016ECF
		[XmlElement(typeof(RdlCollection<ChartLegendCustomItemCell>))]
		public IList<ChartLegendCustomItemCell> ChartLegendCustomItemCells
		{
			get
			{
				return (IList<ChartLegendCustomItemCell>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00018CDE File Offset: 0x00016EDE
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00018CF1 File Offset: 0x00016EF1
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00018D00 File Offset: 0x00016F00
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00018D13 File Offset: 0x00016F13
		public ChartMarker ChartMarker
		{
			get
			{
				return (ChartMarker)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00018D22 File Offset: 0x00016F22
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00018D30 File Offset: 0x00016F30
		[ReportExpressionDefaultValue(typeof(ChartLegendItemSeparatorTypes), ChartLegendItemSeparatorTypes.None)]
		public ReportExpression<ChartLegendItemSeparatorTypes> Separator
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartLegendItemSeparatorTypes>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00018D44 File Offset: 0x00016F44
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00018D52 File Offset: 0x00016F52
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> SeparatorColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00018D66 File Offset: 0x00016F66
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00018D74 File Offset: 0x00016F74
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00018D88 File Offset: 0x00016F88
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00018D9B File Offset: 0x00016F9B
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018DAA File Offset: 0x00016FAA
		public ChartLegendCustomItem()
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00018DB2 File Offset: 0x00016FB2
		internal ChartLegendCustomItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00018DBB File Offset: 0x00016FBB
		public override void Initialize()
		{
			base.Initialize();
			this.ChartLegendCustomItemCells = new RdlCollection<ChartLegendCustomItemCell>();
		}

		// Token: 0x02000342 RID: 834
		internal class Definition : DefinitionStore<ChartLegendCustomItem, ChartLegendCustomItem.Definition.Properties>
		{
			// Token: 0x060017C5 RID: 6085 RVA: 0x0003AB4B File Offset: 0x00038D4B
			private Definition()
			{
			}

			// Token: 0x02000461 RID: 1121
			internal enum Properties
			{
				// Token: 0x0400099F RID: 2463
				Name,
				// Token: 0x040009A0 RID: 2464
				ChartLegendCustomItemCells,
				// Token: 0x040009A1 RID: 2465
				Style,
				// Token: 0x040009A2 RID: 2466
				ChartMarker,
				// Token: 0x040009A3 RID: 2467
				Separator,
				// Token: 0x040009A4 RID: 2468
				SeparatorColor,
				// Token: 0x040009A5 RID: 2469
				ToolTip,
				// Token: 0x040009A6 RID: 2470
				ToolTipLocID,
				// Token: 0x040009A7 RID: 2471
				ActionInfo,
				// Token: 0x040009A8 RID: 2472
				PropertyCount
			}
		}
	}
}
