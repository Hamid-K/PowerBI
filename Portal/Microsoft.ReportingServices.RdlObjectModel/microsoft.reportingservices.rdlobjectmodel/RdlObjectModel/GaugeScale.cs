using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000153 RID: 339
	[XmlElementClass("LinearScale", typeof(LinearScale))]
	[XmlElementClass("RadialScale", typeof(RadialScale))]
	public class GaugeScale : ReportObject, INamedObject
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001EA08 File Offset: 0x0001CC08
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0001EA1B File Offset: 0x0001CC1B
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001EA2A File Offset: 0x0001CC2A
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0001EA3D File Offset: 0x0001CC3D
		[XmlElement(typeof(RdlCollection<GaugePointer>))]
		public IList<GaugePointer> GaugePointers
		{
			get
			{
				return (IList<GaugePointer>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001EA4C File Offset: 0x0001CC4C
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0001EA5F File Offset: 0x0001CC5F
		[XmlElement(typeof(RdlCollection<ScaleRange>))]
		public IList<ScaleRange> ScaleRanges
		{
			get
			{
				return (IList<ScaleRange>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001EA6E File Offset: 0x0001CC6E
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0001EA81 File Offset: 0x0001CC81
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001EA90 File Offset: 0x0001CC90
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0001EAA3 File Offset: 0x0001CCA3
		[XmlElement(typeof(RdlCollection<CustomLabel>))]
		public IList<CustomLabel> CustomLabels
		{
			get
			{
				return (IList<CustomLabel>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001EAB2 File Offset: 0x0001CCB2
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Interval
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0001EAE2 File Offset: 0x0001CCE2
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> IntervalOffset
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001EAF6 File Offset: 0x0001CCF6
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0001EB04 File Offset: 0x0001CD04
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Logarithmic
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001EB18 File Offset: 0x0001CD18
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0001EB26 File Offset: 0x0001CD26
		[ReportExpressionDefaultValue(typeof(double), 10.0)]
		public ReportExpression<double> LogarithmicBase
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001EB3A File Offset: 0x0001CD3A
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		public GaugeInputValue MaximumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001EB5E File Offset: 0x0001CD5E
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0001EB72 File Offset: 0x0001CD72
		public GaugeInputValue MinimumValue
		{
			get
			{
				return (GaugeInputValue)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001EB82 File Offset: 0x0001CD82
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0001EB91 File Offset: 0x0001CD91
		[ReportExpressionDefaultValue(typeof(double), 1.0)]
		public ReportExpression<double> Multiplier
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001EBA6 File Offset: 0x0001CDA6
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0001EBB5 File Offset: 0x0001CDB5
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Reversed
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001EBCA File Offset: 0x0001CDCA
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0001EBDE File Offset: 0x0001CDDE
		public GaugeTickMarks GaugeMajorTickMarks
		{
			get
			{
				return (GaugeTickMarks)base.PropertyStore.GetObject(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0001EBEE File Offset: 0x0001CDEE
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0001EC02 File Offset: 0x0001CE02
		public GaugeTickMarks GaugeMinorTickMarks
		{
			get
			{
				return (GaugeTickMarks)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001EC12 File Offset: 0x0001CE12
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x0001EC26 File Offset: 0x0001CE26
		public ScalePin MaximumPin
		{
			get
			{
				return (ScalePin)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0001EC36 File Offset: 0x0001CE36
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x0001EC4A File Offset: 0x0001CE4A
		public ScalePin MinimumPin
		{
			get
			{
				return (ScalePin)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0001EC5A File Offset: 0x0001CE5A
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x0001EC6E File Offset: 0x0001CE6E
		public ScaleLabels ScaleLabels
		{
			get
			{
				return (ScaleLabels)base.PropertyStore.GetObject(17);
			}
			set
			{
				base.PropertyStore.SetObject(17, value);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001EC7E File Offset: 0x0001CE7E
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x0001EC8D File Offset: 0x0001CE8D
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> TickMarksOnTop
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(18);
			}
			set
			{
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0001ECA2 File Offset: 0x0001CEA2
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0001ECB1 File Offset: 0x0001CEB1
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0001ECC6 File Offset: 0x0001CEC6
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x0001ECDA File Offset: 0x0001CEDA
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001ECEA File Offset: 0x0001CEEA
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0001ECF9 File Offset: 0x0001CEF9
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(21);
			}
			set
			{
				base.PropertyStore.SetObject(21, value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001ED0E File Offset: 0x0001CF0E
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x0001ED1D File Offset: 0x0001CF1D
		[ReportExpressionDefaultValue(typeof(double), 5.0)]
		public ReportExpression<double> Width
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(22);
			}
			set
			{
				base.PropertyStore.SetObject(22, value);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001ED32 File Offset: 0x0001CF32
		public GaugeScale()
		{
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001ED3A File Offset: 0x0001CF3A
		internal GaugeScale(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001ED44 File Offset: 0x0001CF44
		public override void Initialize()
		{
			base.Initialize();
			this.GaugePointers = new RdlCollection<GaugePointer>();
			this.ScaleRanges = new RdlCollection<ScaleRange>();
			this.CustomLabels = new RdlCollection<CustomLabel>();
			this.LogarithmicBase = 10.0;
			this.Multiplier = 1.0;
			this.Width = 5.0;
		}

		// Token: 0x02000384 RID: 900
		internal class Definition : DefinitionStore<GaugeScale, GaugeScale.Definition.Properties>
		{
			// Token: 0x06001827 RID: 6183 RVA: 0x0003B463 File Offset: 0x00039663
			private Definition()
			{
			}

			// Token: 0x0200049D RID: 1181
			internal enum Properties
			{
				// Token: 0x04000C7F RID: 3199
				Name,
				// Token: 0x04000C80 RID: 3200
				GaugePointers,
				// Token: 0x04000C81 RID: 3201
				ScaleRanges,
				// Token: 0x04000C82 RID: 3202
				Style,
				// Token: 0x04000C83 RID: 3203
				CustomLabels,
				// Token: 0x04000C84 RID: 3204
				Interval,
				// Token: 0x04000C85 RID: 3205
				IntervalOffset,
				// Token: 0x04000C86 RID: 3206
				Logarithmic,
				// Token: 0x04000C87 RID: 3207
				LogarithmicBase,
				// Token: 0x04000C88 RID: 3208
				MaximumValue,
				// Token: 0x04000C89 RID: 3209
				MinimumValue,
				// Token: 0x04000C8A RID: 3210
				Multiplier,
				// Token: 0x04000C8B RID: 3211
				Reversed,
				// Token: 0x04000C8C RID: 3212
				GaugeMajorTickMarks,
				// Token: 0x04000C8D RID: 3213
				GaugeMinorTickMarks,
				// Token: 0x04000C8E RID: 3214
				MaximumPin,
				// Token: 0x04000C8F RID: 3215
				MinimumPin,
				// Token: 0x04000C90 RID: 3216
				ScaleLabels,
				// Token: 0x04000C91 RID: 3217
				TickMarksOnTop,
				// Token: 0x04000C92 RID: 3218
				ToolTip,
				// Token: 0x04000C93 RID: 3219
				ActionInfo,
				// Token: 0x04000C94 RID: 3220
				Hidden,
				// Token: 0x04000C95 RID: 3221
				Width,
				// Token: 0x04000C96 RID: 3222
				PropertyCount
			}
		}
	}
}
