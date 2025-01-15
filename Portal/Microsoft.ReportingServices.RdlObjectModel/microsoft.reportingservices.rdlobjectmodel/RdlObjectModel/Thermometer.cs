using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000163 RID: 355
	public class Thermometer : ReportObject
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001F9CB File Offset: 0x0001DBCB
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0001F9DE File Offset: 0x0001DBDE
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001F9ED File Offset: 0x0001DBED
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0001F9FB File Offset: 0x0001DBFB
		[ReportExpressionDefaultValue(typeof(double), 5.0)]
		public ReportExpression<double> BulbOffset
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

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001FA0F File Offset: 0x0001DC0F
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0001FA1D File Offset: 0x0001DC1D
		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> BulbSize
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001FA31 File Offset: 0x0001DC31
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0001FA3F File Offset: 0x0001DC3F
		[ReportExpressionDefaultValue(typeof(ThermometerStyles), ThermometerStyles.Standard)]
		public ReportExpression<ThermometerStyles> ThermometerStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ThermometerStyles>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001FA53 File Offset: 0x0001DC53
		public Thermometer()
		{
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001FA5B File Offset: 0x0001DC5B
		internal Thermometer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public override void Initialize()
		{
			base.Initialize();
			this.BulbOffset = 5.0;
			this.BulbSize = 50.0;
		}

		// Token: 0x02000394 RID: 916
		internal class Definition : DefinitionStore<Thermometer, Thermometer.Definition.Properties>
		{
			// Token: 0x06001837 RID: 6199 RVA: 0x0003B4E3 File Offset: 0x000396E3
			private Definition()
			{
			}

			// Token: 0x020004AD RID: 1197
			internal enum Properties
			{
				// Token: 0x04000D75 RID: 3445
				Style,
				// Token: 0x04000D76 RID: 3446
				BulbOffset,
				// Token: 0x04000D77 RID: 3447
				BulbSize,
				// Token: 0x04000D78 RID: 3448
				ThermometerStyle,
				// Token: 0x04000D79 RID: 3449
				PropertyCount
			}
		}
	}
}
