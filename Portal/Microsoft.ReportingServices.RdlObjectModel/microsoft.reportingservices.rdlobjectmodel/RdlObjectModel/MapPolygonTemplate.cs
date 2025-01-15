using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200017B RID: 379
	public class MapPolygonTemplate : MapSpatialElementTemplate
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x00020B90 File Offset: 0x0001ED90
		public MapPolygonTemplate()
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00020B98 File Offset: 0x0001ED98
		internal MapPolygonTemplate(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		[ReportExpressionDefaultValue(typeof(double), "1")]
		public ReportExpression<double> ScaleFactor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00020BC5 File Offset: 0x0001EDC5
		// (set) Token: 0x06000C12 RID: 3090 RVA: 0x00020BD4 File Offset: 0x0001EDD4
		[ReportExpressionDefaultValue(typeof(double), 0)]
		public ReportExpression<double> CenterPointOffsetX
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00020BE9 File Offset: 0x0001EDE9
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		[ReportExpressionDefaultValue(typeof(double), 0)]
		public ReportExpression<double> CenterPointOffsetY
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00020C0D File Offset: 0x0001EE0D
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x00020C1C File Offset: 0x0001EE1C
		[ReportExpressionDefaultValue(typeof(MapAutoBools), MapAutoBools.Auto)]
		public ReportExpression<MapAutoBools> ShowLabel
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapAutoBools>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00020C31 File Offset: 0x0001EE31
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x00020C40 File Offset: 0x0001EE40
		[ReportExpressionDefaultValue(typeof(MapPolygonLabelPlacements), MapPolygonLabelPlacements.MiddleCenter)]
		public ReportExpression<MapPolygonLabelPlacements> LabelPlacement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapPolygonLabelPlacements>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00020C55 File Offset: 0x0001EE55
		public override void Initialize()
		{
			base.Initialize();
			this.ScaleFactor = 1.0;
			this.ShowLabel = MapAutoBools.Auto;
			this.LabelPlacement = MapPolygonLabelPlacements.MiddleCenter;
		}

		// Token: 0x020003A9 RID: 937
		internal new class Definition : DefinitionStore<MapPolygonTemplate, MapPolygonTemplate.Definition.Properties>
		{
			// Token: 0x0600184D RID: 6221 RVA: 0x0003B5E9 File Offset: 0x000397E9
			private Definition()
			{
			}

			// Token: 0x020004C1 RID: 1217
			internal enum Properties
			{
				// Token: 0x04000E3E RID: 3646
				Style,
				// Token: 0x04000E3F RID: 3647
				ActionInfo,
				// Token: 0x04000E40 RID: 3648
				Hidden,
				// Token: 0x04000E41 RID: 3649
				OffsetX,
				// Token: 0x04000E42 RID: 3650
				OffsetY,
				// Token: 0x04000E43 RID: 3651
				Label,
				// Token: 0x04000E44 RID: 3652
				ToolTip,
				// Token: 0x04000E45 RID: 3653
				DataElementName,
				// Token: 0x04000E46 RID: 3654
				DataElementOutput,
				// Token: 0x04000E47 RID: 3655
				DataElementLabel,
				// Token: 0x04000E48 RID: 3656
				ScaleFactor,
				// Token: 0x04000E49 RID: 3657
				CenterPointOffsetX,
				// Token: 0x04000E4A RID: 3658
				CenterPointOffsetY,
				// Token: 0x04000E4B RID: 3659
				ShowLabel,
				// Token: 0x04000E4C RID: 3660
				LabelPlacement,
				// Token: 0x04000E4D RID: 3661
				PropertyCount
			}
		}
	}
}
