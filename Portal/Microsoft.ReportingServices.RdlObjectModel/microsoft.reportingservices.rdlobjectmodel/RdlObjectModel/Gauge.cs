using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014D RID: 333
	public class Gauge : GaugePanelItem
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x0001E120 File Offset: 0x0001C320
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x0001E134 File Offset: 0x0001C334
		[XmlElement(typeof(RdlCollection<GaugeScale>))]
		public IList<GaugeScale> GaugeScales
		{
			get
			{
				return (IList<GaugeScale>)base.PropertyStore.GetObject(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0001E144 File Offset: 0x0001C344
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0001E158 File Offset: 0x0001C358
		public BackFrame BackFrame
		{
			get
			{
				return (BackFrame)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0001E168 File Offset: 0x0001C368
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0001E177 File Offset: 0x0001C377
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> ClipContent
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

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001E18C File Offset: 0x0001C38C
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public TopImage TopImage
		{
			get
			{
				return (TopImage)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0001E1BF File Offset: 0x0001C3BF
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> AspectRatio
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		public Gauge()
		{
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001E1DC File Offset: 0x0001C3DC
		internal Gauge(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001E1E5 File Offset: 0x0001C3E5
		public override void Initialize()
		{
			base.Initialize();
			this.GaugeScales = new RdlCollection<GaugeScale>();
		}

		// Token: 0x0200037E RID: 894
		internal new class Definition : DefinitionStore<Gauge, Gauge.Definition.Properties>
		{
			// Token: 0x06001821 RID: 6177 RVA: 0x0003B433 File Offset: 0x00039633
			private Definition()
			{
			}

			// Token: 0x02000497 RID: 1175
			internal enum Properties
			{
				// Token: 0x04000C00 RID: 3072
				Name,
				// Token: 0x04000C01 RID: 3073
				Style,
				// Token: 0x04000C02 RID: 3074
				Top,
				// Token: 0x04000C03 RID: 3075
				Left,
				// Token: 0x04000C04 RID: 3076
				Height,
				// Token: 0x04000C05 RID: 3077
				Width,
				// Token: 0x04000C06 RID: 3078
				ZIndex,
				// Token: 0x04000C07 RID: 3079
				Hidden,
				// Token: 0x04000C08 RID: 3080
				ToolTip,
				// Token: 0x04000C09 RID: 3081
				ActionInfo,
				// Token: 0x04000C0A RID: 3082
				ParentItem,
				// Token: 0x04000C0B RID: 3083
				GaugeScales,
				// Token: 0x04000C0C RID: 3084
				BackFrame,
				// Token: 0x04000C0D RID: 3085
				ClipContent,
				// Token: 0x04000C0E RID: 3086
				TopImage,
				// Token: 0x04000C0F RID: 3087
				AspectRatio,
				// Token: 0x04000C10 RID: 3088
				PropertyCount
			}
		}
	}
}
