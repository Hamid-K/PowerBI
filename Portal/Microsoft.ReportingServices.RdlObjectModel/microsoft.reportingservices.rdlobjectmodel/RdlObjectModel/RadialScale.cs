using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000155 RID: 341
	public class RadialScale : GaugeScale
	{
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0001EE83 File Offset: 0x0001D083
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x0001EE92 File Offset: 0x0001D092
		[ValidValues(0.0, 1.7976931348623157E+308)]
		[ReportExpressionDefaultValue(typeof(double), 37.0)]
		public ReportExpression<double> Radius
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(23);
			}
			set
			{
				base.PropertyStore.SetObject(23, value);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0001EEA7 File Offset: 0x0001D0A7
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0001EEB6 File Offset: 0x0001D0B6
		[ValidValues(0.0, 360.0)]
		[ReportExpressionDefaultValue(typeof(double), 20.0)]
		public ReportExpression<double> StartAngle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(24);
			}
			set
			{
				base.PropertyStore.SetObject(24, value);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x0001EECB File Offset: 0x0001D0CB
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x0001EEDA File Offset: 0x0001D0DA
		[ValidValues(0.0, 360.0)]
		[ReportExpressionDefaultValue(typeof(double), 320.0)]
		public ReportExpression<double> SweepAngle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001EEEF File Offset: 0x0001D0EF
		public RadialScale()
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001EEF7 File Offset: 0x0001D0F7
		internal RadialScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001EF00 File Offset: 0x0001D100
		public override void Initialize()
		{
			base.Initialize();
			this.Radius = 37.0;
			this.StartAngle = 20.0;
			this.SweepAngle = 320.0;
		}

		// Token: 0x02000386 RID: 902
		internal new class Definition : DefinitionStore<RadialScale, RadialScale.Definition.Properties>
		{
			// Token: 0x06001829 RID: 6185 RVA: 0x0003B473 File Offset: 0x00039673
			private Definition()
			{
			}

			// Token: 0x0200049F RID: 1183
			internal enum Properties
			{
				// Token: 0x04000CB4 RID: 3252
				Name,
				// Token: 0x04000CB5 RID: 3253
				GaugePointers,
				// Token: 0x04000CB6 RID: 3254
				ScaleRanges,
				// Token: 0x04000CB7 RID: 3255
				Style,
				// Token: 0x04000CB8 RID: 3256
				CustomLabels,
				// Token: 0x04000CB9 RID: 3257
				Interval,
				// Token: 0x04000CBA RID: 3258
				IntervalOffset,
				// Token: 0x04000CBB RID: 3259
				Logarithmic,
				// Token: 0x04000CBC RID: 3260
				LogarithmicBase,
				// Token: 0x04000CBD RID: 3261
				MaximumValue,
				// Token: 0x04000CBE RID: 3262
				MinimumValue,
				// Token: 0x04000CBF RID: 3263
				Multiplier,
				// Token: 0x04000CC0 RID: 3264
				Reversed,
				// Token: 0x04000CC1 RID: 3265
				GaugeMajorTickMarks,
				// Token: 0x04000CC2 RID: 3266
				GaugeMinorTickMarks,
				// Token: 0x04000CC3 RID: 3267
				MaximumPin,
				// Token: 0x04000CC4 RID: 3268
				MinimumPin,
				// Token: 0x04000CC5 RID: 3269
				ScaleLabels,
				// Token: 0x04000CC6 RID: 3270
				TickMarksOnTop,
				// Token: 0x04000CC7 RID: 3271
				ToolTip,
				// Token: 0x04000CC8 RID: 3272
				ActionInfo,
				// Token: 0x04000CC9 RID: 3273
				Hidden,
				// Token: 0x04000CCA RID: 3274
				Width,
				// Token: 0x04000CCB RID: 3275
				Radius,
				// Token: 0x04000CCC RID: 3276
				StartAngle,
				// Token: 0x04000CCD RID: 3277
				SweepAngle,
				// Token: 0x04000CCE RID: 3278
				PropertyCount
			}
		}
	}
}
