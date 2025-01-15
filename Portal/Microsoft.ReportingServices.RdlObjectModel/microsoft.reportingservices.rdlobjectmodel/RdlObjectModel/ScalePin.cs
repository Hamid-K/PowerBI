using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000169 RID: 361
	public class ScalePin : TickMarkStyle
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00020119 File Offset: 0x0001E319
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00020128 File Offset: 0x0001E328
		[ReportExpressionDefaultValue(typeof(double), 5.0)]
		public ReportExpression<double> Location
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0002013D File Offset: 0x0001E33D
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0002014C File Offset: 0x0001E34C
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Enable
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00020161 File Offset: 0x0001E361
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x00020175 File Offset: 0x0001E375
		public PinLabel PinLabel
		{
			get
			{
				return (PinLabel)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00020185 File Offset: 0x0001E385
		public ScalePin()
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002018D File Offset: 0x0001E38D
		internal ScalePin(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00020196 File Offset: 0x0001E396
		public override void Initialize()
		{
			base.Initialize();
			this.Location = 5.0;
		}

		// Token: 0x0200039A RID: 922
		internal new class Definition : DefinitionStore<ScalePin, ScalePin.Definition.Properties>
		{
			// Token: 0x0600183D RID: 6205 RVA: 0x0003B513 File Offset: 0x00039713
			private Definition()
			{
			}

			// Token: 0x020004B3 RID: 1203
			internal enum Properties
			{
				// Token: 0x04000DB9 RID: 3513
				Style,
				// Token: 0x04000DBA RID: 3514
				DistanceFromScale,
				// Token: 0x04000DBB RID: 3515
				Placement,
				// Token: 0x04000DBC RID: 3516
				EnableGradient,
				// Token: 0x04000DBD RID: 3517
				GradientDensity,
				// Token: 0x04000DBE RID: 3518
				TickMarkImage,
				// Token: 0x04000DBF RID: 3519
				Length,
				// Token: 0x04000DC0 RID: 3520
				Width,
				// Token: 0x04000DC1 RID: 3521
				Shape,
				// Token: 0x04000DC2 RID: 3522
				Hidden,
				// Token: 0x04000DC3 RID: 3523
				Location,
				// Token: 0x04000DC4 RID: 3524
				Enable,
				// Token: 0x04000DC5 RID: 3525
				PinLabel,
				// Token: 0x04000DC6 RID: 3526
				PropertyCount
			}
		}
	}
}
