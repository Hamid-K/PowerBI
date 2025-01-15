using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000168 RID: 360
	public class TickMarkStyle : ReportObject
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001FF7E File Offset: 0x0001E17E
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0001FF91 File Offset: 0x0001E191
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0001FFAE File Offset: 0x0001E1AE
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> DistanceFromScale
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001FFC2 File Offset: 0x0001E1C2
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		[ReportExpressionDefaultValue(typeof(Placements), Placements.Inside)]
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0001FFF2 File Offset: 0x0001E1F2
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> EnableGradient
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00020006 File Offset: 0x0001E206
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00020014 File Offset: 0x0001E214
		[ValidValues(0.0, 100.0)]
		[ReportExpressionDefaultValue(typeof(double), 30.0)]
		public ReportExpression<double> GradientDensity
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00020028 File Offset: 0x0001E228
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002003B File Offset: 0x0001E23B
		public TickMarkImage TickMarkImage
		{
			get
			{
				return (TickMarkImage)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002004A File Offset: 0x0001E24A
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00020058 File Offset: 0x0001E258
		public ReportExpression<double> Length
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002006C File Offset: 0x0001E26C
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002007A File Offset: 0x0001E27A
		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002008E File Offset: 0x0001E28E
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002009C File Offset: 0x0001E29C
		[ReportExpressionDefaultValue(typeof(MarkerStyles), MarkerStyles.Rectangle)]
		public ReportExpression<MarkerStyles> Shape
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MarkerStyles>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x000200B0 File Offset: 0x0001E2B0
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x000200BF File Offset: 0x0001E2BF
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000200D4 File Offset: 0x0001E2D4
		public TickMarkStyle()
		{
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000200DC File Offset: 0x0001E2DC
		internal TickMarkStyle(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000200E5 File Offset: 0x0001E2E5
		public override void Initialize()
		{
			base.Initialize();
			this.Placement = Placements.Inside;
			this.Shape = MarkerStyles.Rectangle;
			this.GradientDensity = 30.0;
		}

		// Token: 0x02000399 RID: 921
		internal class Definition : DefinitionStore<TickMarkStyle, TickMarkStyle.Definition.Properties>
		{
			// Token: 0x0600183C RID: 6204 RVA: 0x0003B50B File Offset: 0x0003970B
			private Definition()
			{
			}

			// Token: 0x020004B2 RID: 1202
			internal enum Properties
			{
				// Token: 0x04000DAD RID: 3501
				Style,
				// Token: 0x04000DAE RID: 3502
				DistanceFromScale,
				// Token: 0x04000DAF RID: 3503
				Placement,
				// Token: 0x04000DB0 RID: 3504
				EnableGradient,
				// Token: 0x04000DB1 RID: 3505
				GradientDensity,
				// Token: 0x04000DB2 RID: 3506
				TickMarkImage,
				// Token: 0x04000DB3 RID: 3507
				Length,
				// Token: 0x04000DB4 RID: 3508
				Width,
				// Token: 0x04000DB5 RID: 3509
				Shape,
				// Token: 0x04000DB6 RID: 3510
				Hidden,
				// Token: 0x04000DB7 RID: 3511
				PropertyCount
			}
		}
	}
}
