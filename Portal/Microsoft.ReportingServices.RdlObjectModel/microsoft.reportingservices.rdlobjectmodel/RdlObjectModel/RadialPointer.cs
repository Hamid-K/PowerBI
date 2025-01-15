using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000158 RID: 344
	public class RadialPointer : GaugePointer
	{
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0001F1D7 File Offset: 0x0001D3D7
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0001F1E6 File Offset: 0x0001D3E6
		[ReportExpressionDefaultValue(typeof(RadialPointerTypes), RadialPointerTypes.Needle)]
		public ReportExpression<RadialPointerTypes> Type
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<RadialPointerTypes>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0001F1FB File Offset: 0x0001D3FB
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0001F20F File Offset: 0x0001D40F
		public PointerCap PointerCap
		{
			get
			{
				return (PointerCap)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001F21F File Offset: 0x0001D41F
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0001F22E File Offset: 0x0001D42E
		[ReportExpressionDefaultValue(typeof(NeedleStyles), NeedleStyles.Triangular)]
		public ReportExpression<NeedleStyles> NeedleStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<NeedleStyles>>(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001F243 File Offset: 0x0001D443
		public RadialPointer()
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001F24B File Offset: 0x0001D44B
		internal RadialPointer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000389 RID: 905
		internal new class Definition : DefinitionStore<RadialPointer, RadialPointer.Definition.Properties>
		{
			// Token: 0x0600182C RID: 6188 RVA: 0x0003B48B File Offset: 0x0003968B
			private Definition()
			{
			}

			// Token: 0x020004A2 RID: 1186
			internal enum Properties
			{
				// Token: 0x04000CF4 RID: 3316
				Name,
				// Token: 0x04000CF5 RID: 3317
				Style,
				// Token: 0x04000CF6 RID: 3318
				GaugeInputValue,
				// Token: 0x04000CF7 RID: 3319
				BarStart,
				// Token: 0x04000CF8 RID: 3320
				DistanceFromScale,
				// Token: 0x04000CF9 RID: 3321
				PointerImage,
				// Token: 0x04000CFA RID: 3322
				MarkerLength,
				// Token: 0x04000CFB RID: 3323
				MarkerStyle,
				// Token: 0x04000CFC RID: 3324
				Placement,
				// Token: 0x04000CFD RID: 3325
				SnappingEnabled,
				// Token: 0x04000CFE RID: 3326
				SnappingInterval,
				// Token: 0x04000CFF RID: 3327
				ToolTip,
				// Token: 0x04000D00 RID: 3328
				ActionInfo,
				// Token: 0x04000D01 RID: 3329
				Hidden,
				// Token: 0x04000D02 RID: 3330
				Width,
				// Token: 0x04000D03 RID: 3331
				Type,
				// Token: 0x04000D04 RID: 3332
				PointerCap,
				// Token: 0x04000D05 RID: 3333
				NeedleStyle,
				// Token: 0x04000D06 RID: 3334
				PropertyCount
			}
		}
	}
}
