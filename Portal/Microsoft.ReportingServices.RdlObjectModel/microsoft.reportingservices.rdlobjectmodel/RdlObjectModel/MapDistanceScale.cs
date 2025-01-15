using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000193 RID: 403
	public class MapDistanceScale : MapDockableSubItem
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public MapDistanceScale()
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		internal MapDistanceScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00021CBD File Offset: 0x0001FEBD
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00021CCC File Offset: 0x0001FECC
		[ReportExpressionDefaultValue(typeof(ReportColor), "White")]
		public ReportExpression<ReportColor> ScaleColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00021CE1 File Offset: 0x0001FEE1
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00021CF0 File Offset: 0x0001FEF0
		[ReportExpressionDefaultValue(typeof(ReportColor), "DarkGray")]
		public ReportExpression<ReportColor> ScaleBorderColor
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

		// Token: 0x06000D0A RID: 3338 RVA: 0x00021D05 File Offset: 0x0001FF05
		public override void Initialize()
		{
			base.Initialize();
			this.ScaleColor = new ReportExpression<ReportColor>("White", CultureInfo.InvariantCulture);
			this.ScaleBorderColor = new ReportExpression<ReportColor>("DarkGray", CultureInfo.InvariantCulture);
		}

		// Token: 0x020003C0 RID: 960
		internal new class Definition : DefinitionStore<MapDistanceScale, MapDistanceScale.Definition.Properties>
		{
			// Token: 0x06001864 RID: 6244 RVA: 0x0003B6A1 File Offset: 0x000398A1
			private Definition()
			{
			}

			// Token: 0x020004D8 RID: 1240
			internal enum Properties
			{
				// Token: 0x04000F5F RID: 3935
				Style,
				// Token: 0x04000F60 RID: 3936
				MapLocation,
				// Token: 0x04000F61 RID: 3937
				MapSize,
				// Token: 0x04000F62 RID: 3938
				LeftMargin,
				// Token: 0x04000F63 RID: 3939
				RightMargin,
				// Token: 0x04000F64 RID: 3940
				TopMargin,
				// Token: 0x04000F65 RID: 3941
				BottomMargin,
				// Token: 0x04000F66 RID: 3942
				ZIndex,
				// Token: 0x04000F67 RID: 3943
				ActionInfo,
				// Token: 0x04000F68 RID: 3944
				MapPosition,
				// Token: 0x04000F69 RID: 3945
				DockOutsideViewport,
				// Token: 0x04000F6A RID: 3946
				Hidden,
				// Token: 0x04000F6B RID: 3947
				ToolTip,
				// Token: 0x04000F6C RID: 3948
				ScaleColor,
				// Token: 0x04000F6D RID: 3949
				ScaleBorderColor,
				// Token: 0x04000F6E RID: 3950
				PropertyCount
			}
		}
	}
}
