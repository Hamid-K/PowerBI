using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000096 RID: 150
	public class ChartEmptyPoints : ReportObject
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001A060 File Offset: 0x00018260
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0001A073 File Offset: 0x00018273
		public EmptyColorStyle Style
		{
			get
			{
				return (EmptyColorStyle)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001A082 File Offset: 0x00018282
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0001A095 File Offset: 0x00018295
		public ChartMarker ChartMarker
		{
			get
			{
				return (ChartMarker)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001A0A4 File Offset: 0x000182A4
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0001A0B7 File Offset: 0x000182B7
		public ChartDataLabel ChartDataLabel
		{
			get
			{
				return (ChartDataLabel)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001A0C6 File Offset: 0x000182C6
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0001A0D4 File Offset: 0x000182D4
		[ReportExpressionDefaultValue]
		public ReportExpression AxisLabel
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001A0E8 File Offset: 0x000182E8
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0001A0F6 File Offset: 0x000182F6
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001A10A File Offset: 0x0001830A
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001A11D File Offset: 0x0001831D
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001A12C File Offset: 0x0001832C
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0001A13F File Offset: 0x0001833F
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A14E File Offset: 0x0001834E
		public ChartEmptyPoints()
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A156 File Offset: 0x00018356
		internal ChartEmptyPoints(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001A15F File Offset: 0x0001835F
		public override void Initialize()
		{
			base.Initialize();
			this.CustomProperties = new RdlCollection<CustomProperty>();
		}

		// Token: 0x0200034C RID: 844
		internal class Definition : DefinitionStore<ChartEmptyPoints, ChartEmptyPoints.Definition.Properties>
		{
			// Token: 0x060017CF RID: 6095 RVA: 0x0003AB9B File Offset: 0x00038D9B
			private Definition()
			{
			}

			// Token: 0x0200046B RID: 1131
			internal enum Properties
			{
				// Token: 0x04000A39 RID: 2617
				Style,
				// Token: 0x04000A3A RID: 2618
				ChartMarker,
				// Token: 0x04000A3B RID: 2619
				ChartDataLabel,
				// Token: 0x04000A3C RID: 2620
				AxisLabel,
				// Token: 0x04000A3D RID: 2621
				ToolTip,
				// Token: 0x04000A3E RID: 2622
				ActionInfo,
				// Token: 0x04000A3F RID: 2623
				CustomProperties,
				// Token: 0x04000A40 RID: 2624
				PropertyCount
			}
		}
	}
}
