using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200015D RID: 349
	public class GaugeImage : GaugePanelItem
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001F6BC File Offset: 0x0001D8BC
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001F6CB File Offset: 0x0001D8CB
		public ReportExpression<SourceType> Source
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<SourceType>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0001F6EF File Offset: 0x0001D8EF
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001F704 File Offset: 0x0001D904
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001F713 File Offset: 0x0001D913
		[ReportExpressionDefaultValue]
		public ReportExpression MIMEType
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001F728 File Offset: 0x0001D928
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0001F737 File Offset: 0x0001D937
		[ReportExpressionDefaultValue(typeof(ReportColor))]
		public ReportExpression<ReportColor> TransparentColor
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001F74C File Offset: 0x0001D94C
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0001F75B File Offset: 0x0001D95B
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Angle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001F770 File Offset: 0x0001D970
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0001F77F File Offset: 0x0001D97F
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Transparency
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001F794 File Offset: 0x0001D994
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0001F7A3 File Offset: 0x0001D9A3
		[ReportExpressionDefaultValue(typeof(ResizeModes), ResizeModes.AutoFit)]
		public ReportExpression<ResizeModes> ResizeMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ResizeModes>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001F7B8 File Offset: 0x0001D9B8
		public GaugeImage()
		{
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		internal GaugeImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Image.GetEmbeddedImgDependencies(base.GetAncestor<Report>(), dependencies, this.Source.Value, this.Value);
		}

		// Token: 0x0200038E RID: 910
		internal new class Definition : DefinitionStore<GaugeImage, GaugeImage.Definition.Properties>
		{
			// Token: 0x06001831 RID: 6193 RVA: 0x0003B4B3 File Offset: 0x000396B3
			private Definition()
			{
			}

			// Token: 0x020004A7 RID: 1191
			internal enum Properties
			{
				// Token: 0x04000D38 RID: 3384
				Name,
				// Token: 0x04000D39 RID: 3385
				Style,
				// Token: 0x04000D3A RID: 3386
				Top,
				// Token: 0x04000D3B RID: 3387
				Left,
				// Token: 0x04000D3C RID: 3388
				Height,
				// Token: 0x04000D3D RID: 3389
				Width,
				// Token: 0x04000D3E RID: 3390
				ZIndex,
				// Token: 0x04000D3F RID: 3391
				Hidden,
				// Token: 0x04000D40 RID: 3392
				ToolTip,
				// Token: 0x04000D41 RID: 3393
				ActionInfo,
				// Token: 0x04000D42 RID: 3394
				ParentItem,
				// Token: 0x04000D43 RID: 3395
				Source,
				// Token: 0x04000D44 RID: 3396
				Value,
				// Token: 0x04000D45 RID: 3397
				MIMEType,
				// Token: 0x04000D46 RID: 3398
				TransparentColor,
				// Token: 0x04000D47 RID: 3399
				Angle,
				// Token: 0x04000D48 RID: 3400
				Transparency,
				// Token: 0x04000D49 RID: 3401
				ResizeMode,
				// Token: 0x04000D4A RID: 3402
				PropertyCount
			}
		}
	}
}
