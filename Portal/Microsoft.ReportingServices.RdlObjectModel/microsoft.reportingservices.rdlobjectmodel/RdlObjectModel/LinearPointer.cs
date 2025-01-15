using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000157 RID: 343
	public class LinearPointer : GaugePointer
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0001F17E File Offset: 0x0001D37E
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x0001F18D File Offset: 0x0001D38D
		[ReportExpressionDefaultValue(typeof(LinearPointerTypes), LinearPointerTypes.Marker)]
		public ReportExpression<LinearPointerTypes> Type
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<LinearPointerTypes>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0001F1A2 File Offset: 0x0001D3A2
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0001F1B6 File Offset: 0x0001D3B6
		public Thermometer Thermometer
		{
			get
			{
				return (Thermometer)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001F1C6 File Offset: 0x0001D3C6
		public LinearPointer()
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001F1CE File Offset: 0x0001D3CE
		internal LinearPointer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x02000388 RID: 904
		internal new class Definition : DefinitionStore<LinearPointer, LinearPointer.Definition.Properties>
		{
			// Token: 0x0600182B RID: 6187 RVA: 0x0003B483 File Offset: 0x00039683
			private Definition()
			{
			}

			// Token: 0x020004A1 RID: 1185
			internal enum Properties
			{
				// Token: 0x04000CE1 RID: 3297
				Name,
				// Token: 0x04000CE2 RID: 3298
				Style,
				// Token: 0x04000CE3 RID: 3299
				GaugeInputValue,
				// Token: 0x04000CE4 RID: 3300
				BarStart,
				// Token: 0x04000CE5 RID: 3301
				DistanceFromScale,
				// Token: 0x04000CE6 RID: 3302
				PointerImage,
				// Token: 0x04000CE7 RID: 3303
				MarkerLength,
				// Token: 0x04000CE8 RID: 3304
				MarkerStyle,
				// Token: 0x04000CE9 RID: 3305
				Placement,
				// Token: 0x04000CEA RID: 3306
				SnappingEnabled,
				// Token: 0x04000CEB RID: 3307
				SnappingInterval,
				// Token: 0x04000CEC RID: 3308
				ToolTip,
				// Token: 0x04000CED RID: 3309
				ActionInfo,
				// Token: 0x04000CEE RID: 3310
				Hidden,
				// Token: 0x04000CEF RID: 3311
				Width,
				// Token: 0x04000CF0 RID: 3312
				Type,
				// Token: 0x04000CF1 RID: 3313
				Thermometer,
				// Token: 0x04000CF2 RID: 3314
				PropertyCount
			}
		}
	}
}
