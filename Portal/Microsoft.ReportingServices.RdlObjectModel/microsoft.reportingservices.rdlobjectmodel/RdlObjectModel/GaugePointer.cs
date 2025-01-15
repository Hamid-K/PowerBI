using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000156 RID: 342
	[XmlElementClass("LinearPointer", typeof(LinearPointer))]
	[XmlElementClass("RadialPointer", typeof(RadialPointer))]
	public class GaugePointer : ReportObject, INamedObject
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001EF4F File Offset: 0x0001D14F
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0001EF62 File Offset: 0x0001D162
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

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001EF71 File Offset: 0x0001D171
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001EF84 File Offset: 0x0001D184
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

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001EF93 File Offset: 0x0001D193
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0001EFA6 File Offset: 0x0001D1A6
		public GaugeInputValue GaugeInputValue
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001EFB5 File Offset: 0x0001D1B5
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0001EFC3 File Offset: 0x0001D1C3
		[ReportExpressionDefaultValue(typeof(BarStartTypes), BarStartTypes.ScaleStart)]
		public ReportExpression<BarStartTypes> BarStart
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<BarStartTypes>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0001EFD7 File Offset: 0x0001D1D7
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x0001EFE5 File Offset: 0x0001D1E5
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0001F00C File Offset: 0x0001D20C
		public PointerImage PointerImage
		{
			get
			{
				return (PointerImage)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0001F01B File Offset: 0x0001D21B
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x0001F029 File Offset: 0x0001D229
		public ReportExpression<double> MarkerLength
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

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001F03D File Offset: 0x0001D23D
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0001F04B File Offset: 0x0001D24B
		[ReportExpressionDefaultValue(typeof(MarkerStyles), MarkerStyles.Triangle)]
		public ReportExpression<MarkerStyles> MarkerStyle
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MarkerStyles>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001F05F File Offset: 0x0001D25F
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x0001F06D File Offset: 0x0001D26D
		public ReportExpression<Placements> Placement
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<Placements>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001F081 File Offset: 0x0001D281
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0001F090 File Offset: 0x0001D290
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> SnappingEnabled
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0001F0A5 File Offset: 0x0001D2A5
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> SnappingInterval
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001F0C9 File Offset: 0x0001D2C9
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001F0ED File Offset: 0x0001D2ED
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0001F101 File Offset: 0x0001D301
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0001F111 File Offset: 0x0001D311
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x0001F120 File Offset: 0x0001D320
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0001F135 File Offset: 0x0001D335
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x0001F144 File Offset: 0x0001D344
		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001F159 File Offset: 0x0001D359
		public GaugePointer()
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001F161 File Offset: 0x0001D361
		internal GaugePointer(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001F16A File Offset: 0x0001D36A
		public override void Initialize()
		{
			base.Initialize();
			this.MarkerStyle = MarkerStyles.Triangle;
		}

		// Token: 0x02000387 RID: 903
		internal class Definition : DefinitionStore<GaugePointer, GaugePointer.Definition.Properties>
		{
			// Token: 0x0600182A RID: 6186 RVA: 0x0003B47B File Offset: 0x0003967B
			private Definition()
			{
			}

			// Token: 0x020004A0 RID: 1184
			internal enum Properties
			{
				// Token: 0x04000CD0 RID: 3280
				Name,
				// Token: 0x04000CD1 RID: 3281
				Style,
				// Token: 0x04000CD2 RID: 3282
				GaugeInputValue,
				// Token: 0x04000CD3 RID: 3283
				BarStart,
				// Token: 0x04000CD4 RID: 3284
				DistanceFromScale,
				// Token: 0x04000CD5 RID: 3285
				PointerImage,
				// Token: 0x04000CD6 RID: 3286
				MarkerLength,
				// Token: 0x04000CD7 RID: 3287
				MarkerStyle,
				// Token: 0x04000CD8 RID: 3288
				Placement,
				// Token: 0x04000CD9 RID: 3289
				SnappingEnabled,
				// Token: 0x04000CDA RID: 3290
				SnappingInterval,
				// Token: 0x04000CDB RID: 3291
				ToolTip,
				// Token: 0x04000CDC RID: 3292
				ActionInfo,
				// Token: 0x04000CDD RID: 3293
				Hidden,
				// Token: 0x04000CDE RID: 3294
				Width,
				// Token: 0x04000CDF RID: 3295
				PropertyCount
			}
		}
	}
}
