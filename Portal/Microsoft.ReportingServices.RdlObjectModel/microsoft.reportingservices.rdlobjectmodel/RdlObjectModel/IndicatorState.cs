using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000152 RID: 338
	public class IndicatorState : ReportObject, INamedObject
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001E8ED File Offset: 0x0001CAED
		public IndicatorState()
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001E8F5 File Offset: 0x0001CAF5
		internal IndicatorState(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0001E8FE File Offset: 0x0001CAFE
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0001E911 File Offset: 0x0001CB11
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

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0001E920 File Offset: 0x0001CB20
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0001E933 File Offset: 0x0001CB33
		public GaugeInputValue StartValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0001E942 File Offset: 0x0001CB42
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0001E955 File Offset: 0x0001CB55
		public GaugeInputValue EndValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0001E964 File Offset: 0x0001CB64
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0001E972 File Offset: 0x0001CB72
		public ReportExpression<ReportColor> Color
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0001E986 File Offset: 0x0001CB86
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0001E994 File Offset: 0x0001CB94
		public ReportExpression<double> ScaleFactor
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		public ReportExpression<GaugeStateIndicatorStyles> IndicatorStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<GaugeStateIndicatorStyles>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0001E9CA File Offset: 0x0001CBCA
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0001E9DD File Offset: 0x0001CBDD
		public IndicatorImage IndicatorImage
		{
			get
			{
				return (IndicatorImage)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		public override void Initialize()
		{
			base.Initialize();
			this.ScaleFactor = 1.0;
		}

		// Token: 0x02000383 RID: 899
		internal class Definition : DefinitionStore<IndicatorState, IndicatorState.Definition.Properties>
		{
			// Token: 0x06001826 RID: 6182 RVA: 0x0003B45B File Offset: 0x0003965B
			private Definition()
			{
			}

			// Token: 0x0200049C RID: 1180
			internal enum Properties
			{
				// Token: 0x04000C76 RID: 3190
				Name,
				// Token: 0x04000C77 RID: 3191
				StartValue,
				// Token: 0x04000C78 RID: 3192
				EndValue,
				// Token: 0x04000C79 RID: 3193
				Color,
				// Token: 0x04000C7A RID: 3194
				ScaleFactor,
				// Token: 0x04000C7B RID: 3195
				IndicatorStyle,
				// Token: 0x04000C7C RID: 3196
				IndicatorImage,
				// Token: 0x04000C7D RID: 3197
				PropertyCount
			}
		}
	}
}
