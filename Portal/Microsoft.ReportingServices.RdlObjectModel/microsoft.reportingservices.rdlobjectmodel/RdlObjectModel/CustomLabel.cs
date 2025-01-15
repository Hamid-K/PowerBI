using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000167 RID: 359
	public class CustomLabel : ReportObject, INamedObject
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0001FDBB File Offset: 0x0001DFBB
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0001FDCE File Offset: 0x0001DFCE
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001FDDD File Offset: 0x0001DFDD
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001FDFF File Offset: 0x0001DFFF
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0001FE0D File Offset: 0x0001E00D
		[ReportExpressionDefaultValue]
		public ReportExpression Text
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0001FE21 File Offset: 0x0001E021
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0001FE2F File Offset: 0x0001E02F
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AllowUpsideDown
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

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0001FE43 File Offset: 0x0001E043
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0001FE51 File Offset: 0x0001E051
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> DistanceFromScale
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

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001FE65 File Offset: 0x0001E065
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0001FE73 File Offset: 0x0001E073
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> FontAngle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001FE87 File Offset: 0x0001E087
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0001FE95 File Offset: 0x0001E095
		[ReportExpressionDefaultValue(typeof(Placements), Placements.Inside)]
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001FEA9 File Offset: 0x0001E0A9
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0001FEB7 File Offset: 0x0001E0B7
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> RotateLabel
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001FECB File Offset: 0x0001E0CB
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0001FEDE File Offset: 0x0001E0DE
		public TickMarkStyle TickMarkStyle
		{
			get
			{
				return (TickMarkStyle)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0001FEED File Offset: 0x0001E0ED
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		[ReportExpressionDefaultValue]
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001FF11 File Offset: 0x0001E111
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0001FF20 File Offset: 0x0001E120
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001FF35 File Offset: 0x0001E135
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0001FF44 File Offset: 0x0001E144
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> UseFontPercent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001FF59 File Offset: 0x0001E159
		public CustomLabel()
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001FF61 File Offset: 0x0001E161
		internal CustomLabel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001FF6A File Offset: 0x0001E16A
		public override void Initialize()
		{
			base.Initialize();
			this.Placement = Placements.Inside;
		}

		// Token: 0x02000398 RID: 920
		internal class Definition : DefinitionStore<CustomLabel, CustomLabel.Definition.Properties>
		{
			// Token: 0x0600183B RID: 6203 RVA: 0x0003B503 File Offset: 0x00039703
			private Definition()
			{
			}

			// Token: 0x020004B1 RID: 1201
			internal enum Properties
			{
				// Token: 0x04000D9F RID: 3487
				Name,
				// Token: 0x04000DA0 RID: 3488
				Style,
				// Token: 0x04000DA1 RID: 3489
				Text,
				// Token: 0x04000DA2 RID: 3490
				AllowUpsideDown,
				// Token: 0x04000DA3 RID: 3491
				DistanceFromScale,
				// Token: 0x04000DA4 RID: 3492
				FontAngle,
				// Token: 0x04000DA5 RID: 3493
				Placement,
				// Token: 0x04000DA6 RID: 3494
				RotateLabel,
				// Token: 0x04000DA7 RID: 3495
				TickMarkStyle,
				// Token: 0x04000DA8 RID: 3496
				Value,
				// Token: 0x04000DA9 RID: 3497
				Hidden,
				// Token: 0x04000DAA RID: 3498
				UseFontPercent,
				// Token: 0x04000DAB RID: 3499
				PropertyCount
			}
		}
	}
}
