using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000194 RID: 404
	public class MapColorScale : MapDockableSubItem
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x00021D37 File Offset: 0x0001FF37
		public MapColorScale()
		{
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00021D3F File Offset: 0x0001FF3F
		internal MapColorScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00021D48 File Offset: 0x0001FF48
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00021D5C File Offset: 0x0001FF5C
		public MapColorScaleTitle MapColorScaleTitle
		{
			get
			{
				return (MapColorScaleTitle)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00021D6C File Offset: 0x0001FF6C
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00021D7B File Offset: 0x0001FF7B
		[ReportExpressionDefaultValue(typeof(ReportSize), "2.25pt")]
		public ReportExpression<ReportSize> TickMarkLength
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00021D90 File Offset: 0x0001FF90
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00021D9F File Offset: 0x0001FF9F
		[ReportExpressionDefaultValue(typeof(ReportColor), "Black")]
		public ReportExpression<ReportColor> ColorBarBorderColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00021DC3 File Offset: 0x0001FFC3
		[ReportExpressionDefaultValue(typeof(int), "1")]
		public ReportExpression<int> LabelInterval
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

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00021DD8 File Offset: 0x0001FFD8
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00021DE7 File Offset: 0x0001FFE7
		[ReportExpressionDefaultValue]
		public ReportExpression LabelFormat
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00021DFC File Offset: 0x0001FFFC
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00021E0B File Offset: 0x0002000B
		[ReportExpressionDefaultValue(typeof(MapLabelPlacements), MapLabelPlacements.Alternate)]
		public ReportExpression<MapLabelPlacements> LabelPlacement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLabelPlacements>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00021E20 File Offset: 0x00020020
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x00021E2F File Offset: 0x0002002F
		[ReportExpressionDefaultValue(typeof(MapLabelBehaviors), MapLabelBehaviors.Auto)]
		public ReportExpression<MapLabelBehaviors> LabelBehavior
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapLabelBehaviors>>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00021E44 File Offset: 0x00020044
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x00021E53 File Offset: 0x00020053
		public ReportExpression<bool> HideEndLabels
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

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00021E68 File Offset: 0x00020068
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x00021E77 File Offset: 0x00020077
		[ReportExpressionDefaultValue(typeof(ReportColor), "White")]
		public ReportExpression<ReportColor> RangeGapColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00021E8C File Offset: 0x0002008C
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x00021E9B File Offset: 0x0002009B
		public ReportExpression NoDataText
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00021EB0 File Offset: 0x000200B0
		public override void Initialize()
		{
			base.Initialize();
			this.TickMarkLength = new ReportExpression<ReportSize>("2.25pt", CultureInfo.InvariantCulture);
			this.ColorBarBorderColor = new ReportExpression<ReportColor>("Black", CultureInfo.InvariantCulture);
			this.LabelInterval = 1;
			this.LabelFormat = "#,##0.##";
			this.LabelPlacement = MapLabelPlacements.Alternate;
			this.LabelBehavior = MapLabelBehaviors.Auto;
			this.RangeGapColor = new ReportExpression<ReportColor>("White", CultureInfo.InvariantCulture);
		}

		// Token: 0x020003C1 RID: 961
		internal new class Definition : DefinitionStore<MapColorScale, MapColorScale.Definition.Properties>
		{
			// Token: 0x06001865 RID: 6245 RVA: 0x0003B6A9 File Offset: 0x000398A9
			private Definition()
			{
			}

			// Token: 0x020004D9 RID: 1241
			internal enum Properties
			{
				// Token: 0x04000F70 RID: 3952
				Style,
				// Token: 0x04000F71 RID: 3953
				MapLocation,
				// Token: 0x04000F72 RID: 3954
				MapSize,
				// Token: 0x04000F73 RID: 3955
				LeftMargin,
				// Token: 0x04000F74 RID: 3956
				RightMargin,
				// Token: 0x04000F75 RID: 3957
				TopMargin,
				// Token: 0x04000F76 RID: 3958
				BottomMargin,
				// Token: 0x04000F77 RID: 3959
				ZIndex,
				// Token: 0x04000F78 RID: 3960
				ActionInfo,
				// Token: 0x04000F79 RID: 3961
				MapPosition,
				// Token: 0x04000F7A RID: 3962
				DockOutsideViewport,
				// Token: 0x04000F7B RID: 3963
				Hidden,
				// Token: 0x04000F7C RID: 3964
				ToolTip,
				// Token: 0x04000F7D RID: 3965
				MapColorScaleTitle,
				// Token: 0x04000F7E RID: 3966
				TickMarkLength,
				// Token: 0x04000F7F RID: 3967
				ColorBarBorderColor,
				// Token: 0x04000F80 RID: 3968
				LabelInterval,
				// Token: 0x04000F81 RID: 3969
				LabelFormat,
				// Token: 0x04000F82 RID: 3970
				LabelPlacement,
				// Token: 0x04000F83 RID: 3971
				LabelBehavior,
				// Token: 0x04000F84 RID: 3972
				HideEndLabels,
				// Token: 0x04000F85 RID: 3973
				RangeGapColor,
				// Token: 0x04000F86 RID: 3974
				NoDataText,
				// Token: 0x04000F87 RID: 3975
				PropertyCount
			}
		}
	}
}
