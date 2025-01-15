using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000164 RID: 356
	public class PointerCap : ReportObject
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001FA94 File Offset: 0x0001DC94
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0001FAA7 File Offset: 0x0001DCA7
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0001FAB6 File Offset: 0x0001DCB6
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0001FAC9 File Offset: 0x0001DCC9
		public CapImage CapImage
		{
			get
			{
				return (CapImage)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0001FAD8 File Offset: 0x0001DCD8
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0001FAE6 File Offset: 0x0001DCE6
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> OnTop
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0001FAFA File Offset: 0x0001DCFA
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0001FB08 File Offset: 0x0001DD08
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Reflection
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001FB1C File Offset: 0x0001DD1C
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0001FB2A File Offset: 0x0001DD2A
		[ReportExpressionDefaultValue(typeof(CapStyles), CapStyles.RoundedDark)]
		public ReportExpression<CapStyles> CapStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<CapStyles>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0001FB3E File Offset: 0x0001DD3E
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0001FB4C File Offset: 0x0001DD4C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001FB60 File Offset: 0x0001DD60
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0001FB6E File Offset: 0x0001DD6E
		[ReportExpressionDefaultValue(typeof(double), 26.0)]
		public ReportExpression<double> Width
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

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001FB82 File Offset: 0x0001DD82
		public PointerCap()
		{
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001FB8A File Offset: 0x0001DD8A
		internal PointerCap(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001FB93 File Offset: 0x0001DD93
		public override void Initialize()
		{
			base.Initialize();
			this.Width = 26.0;
		}

		// Token: 0x02000395 RID: 917
		internal class Definition : DefinitionStore<PointerCap, PointerCap.Definition.Properties>
		{
			// Token: 0x06001838 RID: 6200 RVA: 0x0003B4EB File Offset: 0x000396EB
			private Definition()
			{
			}

			// Token: 0x020004AE RID: 1198
			internal enum Properties
			{
				// Token: 0x04000D7B RID: 3451
				Style,
				// Token: 0x04000D7C RID: 3452
				CapImage,
				// Token: 0x04000D7D RID: 3453
				OnTop,
				// Token: 0x04000D7E RID: 3454
				Reflection,
				// Token: 0x04000D7F RID: 3455
				CapStyle,
				// Token: 0x04000D80 RID: 3456
				Hidden,
				// Token: 0x04000D81 RID: 3457
				Width,
				// Token: 0x04000D82 RID: 3458
				PropertyCount
			}
		}
	}
}
