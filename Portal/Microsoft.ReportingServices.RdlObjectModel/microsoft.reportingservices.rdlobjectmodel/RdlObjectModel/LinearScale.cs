using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000154 RID: 340
	public class LinearScale : GaugeScale
	{
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0001EDC3 File Offset: 0x0001CFC3
		[ReportExpressionDefaultValue(typeof(double), 8.0)]
		public ReportExpression<double> StartMargin
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0001EDE7 File Offset: 0x0001CFE7
		[ReportExpressionDefaultValue(typeof(double), 8.0)]
		public ReportExpression<double> EndMargin
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0001EE0B File Offset: 0x0001D00B
		[ReportExpressionDefaultValue(typeof(double), 50.0)]
		public ReportExpression<double> Position
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

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001EE20 File Offset: 0x0001D020
		public LinearScale()
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001EE28 File Offset: 0x0001D028
		internal LinearScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001EE34 File Offset: 0x0001D034
		public override void Initialize()
		{
			base.Initialize();
			this.StartMargin = 8.0;
			this.EndMargin = 8.0;
			this.Position = 50.0;
		}

		// Token: 0x02000385 RID: 901
		internal new class Definition : DefinitionStore<LinearScale, LinearScale.Definition.Properties>
		{
			// Token: 0x06001828 RID: 6184 RVA: 0x0003B46B File Offset: 0x0003966B
			private Definition()
			{
			}

			// Token: 0x0200049E RID: 1182
			internal enum Properties
			{
				// Token: 0x04000C98 RID: 3224
				Name,
				// Token: 0x04000C99 RID: 3225
				GaugePointers,
				// Token: 0x04000C9A RID: 3226
				ScaleRanges,
				// Token: 0x04000C9B RID: 3227
				Style,
				// Token: 0x04000C9C RID: 3228
				CustomLabels,
				// Token: 0x04000C9D RID: 3229
				Interval,
				// Token: 0x04000C9E RID: 3230
				IntervalOffset,
				// Token: 0x04000C9F RID: 3231
				Logarithmic,
				// Token: 0x04000CA0 RID: 3232
				LogarithmicBase,
				// Token: 0x04000CA1 RID: 3233
				MaximumValue,
				// Token: 0x04000CA2 RID: 3234
				MinimumValue,
				// Token: 0x04000CA3 RID: 3235
				Multiplier,
				// Token: 0x04000CA4 RID: 3236
				Reversed,
				// Token: 0x04000CA5 RID: 3237
				GaugeMajorTickMarks,
				// Token: 0x04000CA6 RID: 3238
				GaugeMinorTickMarks,
				// Token: 0x04000CA7 RID: 3239
				MaximumPin,
				// Token: 0x04000CA8 RID: 3240
				MinimumPin,
				// Token: 0x04000CA9 RID: 3241
				ScaleLabels,
				// Token: 0x04000CAA RID: 3242
				TickMarksOnTop,
				// Token: 0x04000CAB RID: 3243
				ToolTip,
				// Token: 0x04000CAC RID: 3244
				ActionInfo,
				// Token: 0x04000CAD RID: 3245
				Hidden,
				// Token: 0x04000CAE RID: 3246
				Width,
				// Token: 0x04000CAF RID: 3247
				StartMargin,
				// Token: 0x04000CB0 RID: 3248
				EndMargin,
				// Token: 0x04000CB1 RID: 3249
				Position,
				// Token: 0x04000CB2 RID: 3250
				PropertyCount
			}
		}
	}
}
