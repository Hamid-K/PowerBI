using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016A RID: 362
	public class PinLabel : ReportObject
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x000201B2 File Offset: 0x0001E3B2
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x000201C5 File Offset: 0x0001E3C5
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000201D4 File Offset: 0x0001E3D4
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x000201E2 File Offset: 0x0001E3E2
		[ReportExpressionDefaultValue]
		public ReportExpression Text
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000201F6 File Offset: 0x0001E3F6
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x00020204 File Offset: 0x0001E404
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AllowUpsideDown
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00020218 File Offset: 0x0001E418
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00020226 File Offset: 0x0001E426
		[ReportExpressionDefaultValue(typeof(double), 2.0)]
		public ReportExpression<double> DistanceFromScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0002023A File Offset: 0x0001E43A
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00020248 File Offset: 0x0001E448
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> FontAngle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002025C File Offset: 0x0001E45C
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0002026A File Offset: 0x0001E46A
		[ReportExpressionDefaultValue(typeof(Placements), Placements.Inside)]
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0002027E File Offset: 0x0001E47E
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0002028C File Offset: 0x0001E48C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> RotateLabel
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000202A0 File Offset: 0x0001E4A0
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x000202AE File Offset: 0x0001E4AE
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseFontPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000202C2 File Offset: 0x0001E4C2
		public PinLabel()
		{
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000202CA File Offset: 0x0001E4CA
		internal PinLabel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000202D3 File Offset: 0x0001E4D3
		public override void Initialize()
		{
			base.Initialize();
			this.Placement = Placements.Inside;
			this.DistanceFromScale = 2.0;
		}

		// Token: 0x0200039B RID: 923
		internal class Definition : DefinitionStore<PinLabel, PinLabel.Definition.Properties>
		{
			// Token: 0x0600183E RID: 6206 RVA: 0x0003B51B File Offset: 0x0003971B
			private Definition()
			{
			}

			// Token: 0x020004B4 RID: 1204
			internal enum Properties
			{
				// Token: 0x04000DC8 RID: 3528
				Style,
				// Token: 0x04000DC9 RID: 3529
				Text,
				// Token: 0x04000DCA RID: 3530
				AllowUpsideDown,
				// Token: 0x04000DCB RID: 3531
				DistanceFromScale,
				// Token: 0x04000DCC RID: 3532
				FontAngle,
				// Token: 0x04000DCD RID: 3533
				Placement,
				// Token: 0x04000DCE RID: 3534
				RotateLabel,
				// Token: 0x04000DCF RID: 3535
				UseFontPercent,
				// Token: 0x04000DD0 RID: 3536
				PropertyCount
			}
		}
	}
}
