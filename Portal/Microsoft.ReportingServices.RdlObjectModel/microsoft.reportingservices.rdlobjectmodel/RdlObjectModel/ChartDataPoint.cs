using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000094 RID: 148
	public class ChartDataPoint : DataRegionCell
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00019D60 File Offset: 0x00017F60
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00019D73 File Offset: 0x00017F73
		public ChartDataPointValues ChartDataPointValues
		{
			get
			{
				return (ChartDataPointValues)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00019D82 File Offset: 0x00017F82
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00019D95 File Offset: 0x00017F95
		public ChartDataLabel ChartDataLabel
		{
			get
			{
				return (ChartDataLabel)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00019DA4 File Offset: 0x00017FA4
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00019DB2 File Offset: 0x00017FB2
		[ReportExpressionDefaultValue]
		public ReportExpression AxisLabel
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00019DC6 File Offset: 0x00017FC6
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00019DD4 File Offset: 0x00017FD4
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00019DE8 File Offset: 0x00017FE8
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00019DFB File Offset: 0x00017FFB
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00019E0A File Offset: 0x0001800A
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x00019E1D File Offset: 0x0001801D
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

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00019E2C File Offset: 0x0001802C
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00019E3F File Offset: 0x0001803F
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00019E4E File Offset: 0x0001804E
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00019E61 File Offset: 0x00018061
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00019E70 File Offset: 0x00018070
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00019E7E File Offset: 0x0001807E
		[DefaultValue(DataElementOutputTypes.ContentsOnly)]
		[ValidEnumValues("DataPointDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(8);
			}
			set
			{
				((EnumProperty)DefinitionStore<ChartDataPoint, ChartDataPoint.Definition.Properties>.GetProperty(8)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(8, (int)value);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00019E9F File Offset: 0x0001809F
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00019EB3 File Offset: 0x000180B3
		public ChartItemInLegend ChartItemInLegend
		{
			get
			{
				return (ChartItemInLegend)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00019EC3 File Offset: 0x000180C3
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00019ED7 File Offset: 0x000180D7
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019EE7 File Offset: 0x000180E7
		public ChartDataPoint()
		{
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019EEF File Offset: 0x000180EF
		internal ChartDataPoint(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019EF8 File Offset: 0x000180F8
		public override void Initialize()
		{
			base.Initialize();
			this.ChartDataPointValues = new ChartDataPointValues();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.DataElementOutput = DataElementOutputTypes.ContentsOnly;
		}

		// Token: 0x0200034A RID: 842
		internal class Definition : DefinitionStore<ChartDataPoint, ChartDataPoint.Definition.Properties>
		{
			// Token: 0x060017CD RID: 6093 RVA: 0x0003AB8B File Offset: 0x00038D8B
			private Definition()
			{
			}

			// Token: 0x02000469 RID: 1129
			internal enum Properties
			{
				// Token: 0x04000A21 RID: 2593
				ChartDataPointValues,
				// Token: 0x04000A22 RID: 2594
				ChartDataLabel,
				// Token: 0x04000A23 RID: 2595
				AxisLabel,
				// Token: 0x04000A24 RID: 2596
				ToolTip,
				// Token: 0x04000A25 RID: 2597
				ActionInfo,
				// Token: 0x04000A26 RID: 2598
				Style,
				// Token: 0x04000A27 RID: 2599
				ChartMarker,
				// Token: 0x04000A28 RID: 2600
				DataElementName,
				// Token: 0x04000A29 RID: 2601
				DataElementOutput,
				// Token: 0x04000A2A RID: 2602
				ChartItemInLegend,
				// Token: 0x04000A2B RID: 2603
				CustomProperties,
				// Token: 0x04000A2C RID: 2604
				PropertyCount
			}
		}
	}
}
