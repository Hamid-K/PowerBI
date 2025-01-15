using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000093 RID: 147
	public class ChartSeries : ReportObject, INamedObject
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00019AF8 File Offset: 0x00017CF8
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x00019B0B File Offset: 0x00017D0B
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

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00019B1A File Offset: 0x00017D1A
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00019B28 File Offset: 0x00017D28
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00019B3C File Offset: 0x00017D3C
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00019B4F File Offset: 0x00017D4F
		[XmlElement(typeof(RdlCollection<ChartDataPoint>))]
		public IList<ChartDataPoint> ChartDataPoints
		{
			get
			{
				return (IList<ChartDataPoint>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00019B5E File Offset: 0x00017D5E
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00019B6C File Offset: 0x00017D6C
		[ReportExpressionDefaultValue(typeof(ChartTypes), ChartTypes.Column)]
		public ReportExpression<ChartTypes> Type
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartTypes>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00019B80 File Offset: 0x00017D80
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x00019B8E File Offset: 0x00017D8E
		[ReportExpressionDefaultValue(typeof(ChartSubtypes), ChartSubtypes.Plain)]
		public ReportExpression<ChartSubtypes> Subtype
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ChartSubtypes>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00019BA2 File Offset: 0x00017DA2
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x00019BB5 File Offset: 0x00017DB5
		public EmptyColorStyle Style
		{
			get
			{
				return (EmptyColorStyle)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00019BC4 File Offset: 0x00017DC4
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00019BD7 File Offset: 0x00017DD7
		public ChartMarker ChartMarker
		{
			get
			{
				return (ChartMarker)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00019BE6 File Offset: 0x00017DE6
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00019BF9 File Offset: 0x00017DF9
		public ChartDataLabel ChartDataLabel
		{
			get
			{
				return (ChartDataLabel)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00019C08 File Offset: 0x00017E08
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00019C1B File Offset: 0x00017E1B
		public ChartEmptyPoints ChartEmptyPoints
		{
			get
			{
				return (ChartEmptyPoints)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00019C2A File Offset: 0x00017E2A
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00019C3E File Offset: 0x00017E3E
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00019C4E File Offset: 0x00017E4E
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00019C62 File Offset: 0x00017E62
		[DefaultValue("")]
		public string LegendName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x00019C72 File Offset: 0x00017E72
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x00019C86 File Offset: 0x00017E86
		public ChartItemInLegend ChartItemInLegend
		{
			get
			{
				return (ChartItemInLegend)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00019C96 File Offset: 0x00017E96
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x00019CAA File Offset: 0x00017EAA
		[DefaultValue("")]
		public string ChartAreaName
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00019CBA File Offset: 0x00017EBA
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x00019CCE File Offset: 0x00017ECE
		[DefaultValue("")]
		public string ValueAxisName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00019CDE File Offset: 0x00017EDE
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x00019CF2 File Offset: 0x00017EF2
		[DefaultValue("")]
		public string CategoryAxisName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00019D02 File Offset: 0x00017F02
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x00019D16 File Offset: 0x00017F16
		public ChartSmartLabel ChartSmartLabel
		{
			get
			{
				return (ChartSmartLabel)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019D26 File Offset: 0x00017F26
		public ChartSeries()
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00019D2E File Offset: 0x00017F2E
		internal ChartSeries(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00019D37 File Offset: 0x00017F37
		public override void Initialize()
		{
			base.Initialize();
			this.ChartDataPoints = new RdlCollection<ChartDataPoint>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.Style = new EmptyColorStyle();
		}

		// Token: 0x02000349 RID: 841
		internal class Definition : DefinitionStore<ChartSeries, ChartSeries.Definition.Properties>
		{
			// Token: 0x060017CC RID: 6092 RVA: 0x0003AB83 File Offset: 0x00038D83
			private Definition()
			{
			}

			// Token: 0x02000468 RID: 1128
			internal enum Properties
			{
				// Token: 0x04000A0F RID: 2575
				Name,
				// Token: 0x04000A10 RID: 2576
				Hidden,
				// Token: 0x04000A11 RID: 2577
				ChartDataPoints,
				// Token: 0x04000A12 RID: 2578
				Type,
				// Token: 0x04000A13 RID: 2579
				Subtype,
				// Token: 0x04000A14 RID: 2580
				Style,
				// Token: 0x04000A15 RID: 2581
				ChartMarker,
				// Token: 0x04000A16 RID: 2582
				ChartDataLabel,
				// Token: 0x04000A17 RID: 2583
				ChartEmptyPoints,
				// Token: 0x04000A18 RID: 2584
				CustomProperties,
				// Token: 0x04000A19 RID: 2585
				LegendName,
				// Token: 0x04000A1A RID: 2586
				ChartItemInLegend,
				// Token: 0x04000A1B RID: 2587
				ChartAreaName,
				// Token: 0x04000A1C RID: 2588
				ValueAxisName,
				// Token: 0x04000A1D RID: 2589
				CategoryAxisName,
				// Token: 0x04000A1E RID: 2590
				PropertyCount,
				// Token: 0x04000A1F RID: 2591
				ChartSmartLabel
			}
		}
	}
}
