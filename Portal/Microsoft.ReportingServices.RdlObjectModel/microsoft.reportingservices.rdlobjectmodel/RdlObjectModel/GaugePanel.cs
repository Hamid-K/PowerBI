using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000149 RID: 329
	public class GaugePanel : DataRegion
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x0001DB45 File Offset: 0x0001BD45
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x0001DB59 File Offset: 0x0001BD59
		[XmlElement(typeof(RdlCollection<LinearGauge>))]
		public IList<LinearGauge> LinearGauges
		{
			get
			{
				return (IList<LinearGauge>)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0001DB69 File Offset: 0x0001BD69
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x0001DB7D File Offset: 0x0001BD7D
		[XmlElement(typeof(RdlCollection<RadialGauge>))]
		public IList<RadialGauge> RadialGauges
		{
			get
			{
				return (IList<RadialGauge>)base.PropertyStore.GetObject(26);
			}
			set
			{
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001DB8D File Offset: 0x0001BD8D
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0001DBA1 File Offset: 0x0001BDA1
		[XmlElement(typeof(RdlCollection<NumericIndicator>))]
		public IList<NumericIndicator> NumericIndicators
		{
			get
			{
				return (IList<NumericIndicator>)base.PropertyStore.GetObject(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0001DBB1 File Offset: 0x0001BDB1
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0001DBC5 File Offset: 0x0001BDC5
		[XmlElement(typeof(RdlCollection<StateIndicator>))]
		public IList<StateIndicator> StateIndicators
		{
			get
			{
				return (IList<StateIndicator>)base.PropertyStore.GetObject(28);
			}
			set
			{
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001DBD5 File Offset: 0x0001BDD5
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001DBE9 File Offset: 0x0001BDE9
		[XmlElement(typeof(RdlCollection<GaugeImage>))]
		public IList<GaugeImage> GaugeImages
		{
			get
			{
				return (IList<GaugeImage>)base.PropertyStore.GetObject(29);
			}
			set
			{
				base.PropertyStore.SetObject(29, value);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001DBF9 File Offset: 0x0001BDF9
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0001DC0D File Offset: 0x0001BE0D
		[XmlElement(typeof(RdlCollection<GaugeLabel>))]
		public IList<GaugeLabel> GaugeLabels
		{
			get
			{
				return (IList<GaugeLabel>)base.PropertyStore.GetObject(30);
			}
			set
			{
				base.PropertyStore.SetObject(30, value);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001DC1D File Offset: 0x0001BE1D
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0001DC31 File Offset: 0x0001BE31
		public GaugeMember GaugeMember
		{
			get
			{
				return (GaugeMember)base.PropertyStore.GetObject(31);
			}
			set
			{
				base.PropertyStore.SetObject(31, value);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001DC41 File Offset: 0x0001BE41
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0001DC50 File Offset: 0x0001BE50
		[ReportExpressionDefaultValue(typeof(AntiAliasingTypes), AntiAliasingTypes.All)]
		public ReportExpression<AntiAliasingTypes> AntiAliasing
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<AntiAliasingTypes>>(32);
			}
			set
			{
				base.PropertyStore.SetObject(32, value);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0001DC65 File Offset: 0x0001BE65
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0001DC74 File Offset: 0x0001BE74
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> AutoLayout
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(33);
			}
			set
			{
				base.PropertyStore.SetObject(33, value);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0001DC89 File Offset: 0x0001BE89
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x0001DC9D File Offset: 0x0001BE9D
		public BackFrame BackFrame
		{
			get
			{
				return (BackFrame)base.PropertyStore.GetObject(34);
			}
			set
			{
				base.PropertyStore.SetObject(34, value);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x0001DCAD File Offset: 0x0001BEAD
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x0001DCBC File Offset: 0x0001BEBC
		[ValidValues(0.0, 100.0)]
		[ReportExpressionDefaultValue(typeof(double), 25.0)]
		public ReportExpression<double> ShadowIntensity
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(35);
			}
			set
			{
				base.PropertyStore.SetObject(35, value);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0001DCD1 File Offset: 0x0001BED1
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		[ReportExpressionDefaultValue(typeof(TextAntiAliasingQualityTypes), TextAntiAliasingQualityTypes.High)]
		public ReportExpression<TextAntiAliasingQualityTypes> TextAntiAliasingQuality
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<TextAntiAliasingQualityTypes>>(36);
			}
			set
			{
				base.PropertyStore.SetObject(36, value);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0001DCF5 File Offset: 0x0001BEF5
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0001DD09 File Offset: 0x0001BF09
		public TopImage TopImage
		{
			get
			{
				return (TopImage)base.PropertyStore.GetObject(37);
			}
			set
			{
				base.PropertyStore.SetObject(37, value);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001DD19 File Offset: 0x0001BF19
		public GaugePanel()
		{
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001DD21 File Offset: 0x0001BF21
		internal GaugePanel(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public override void Initialize()
		{
			base.Initialize();
			this.LinearGauges = new RdlCollection<LinearGauge>();
			this.RadialGauges = new RdlCollection<RadialGauge>();
			this.NumericIndicators = new RdlCollection<NumericIndicator>();
			this.StateIndicators = new RdlCollection<StateIndicator>();
			this.GaugeImages = new RdlCollection<GaugeImage>();
			this.GaugeLabels = new RdlCollection<GaugeLabel>();
			this.ShadowIntensity = 25.0;
		}

		// Token: 0x02000378 RID: 888
		internal new class Definition : DefinitionStore<GaugePanel, GaugePanel.Definition.Properties>
		{
			// Token: 0x0600180D RID: 6157 RVA: 0x0003B284 File Offset: 0x00039484
			private Definition()
			{
			}

			// Token: 0x02000493 RID: 1171
			internal enum Properties
			{
				// Token: 0x04000BBC RID: 3004
				Style,
				// Token: 0x04000BBD RID: 3005
				Name,
				// Token: 0x04000BBE RID: 3006
				ActionInfo,
				// Token: 0x04000BBF RID: 3007
				Top,
				// Token: 0x04000BC0 RID: 3008
				Left,
				// Token: 0x04000BC1 RID: 3009
				Height,
				// Token: 0x04000BC2 RID: 3010
				Width,
				// Token: 0x04000BC3 RID: 3011
				ZIndex,
				// Token: 0x04000BC4 RID: 3012
				Visibility,
				// Token: 0x04000BC5 RID: 3013
				ToolTip,
				// Token: 0x04000BC6 RID: 3014
				ToolTipLocID,
				// Token: 0x04000BC7 RID: 3015
				DocumentMapLabel,
				// Token: 0x04000BC8 RID: 3016
				DocumentMapLabelLocID,
				// Token: 0x04000BC9 RID: 3017
				Bookmark,
				// Token: 0x04000BCA RID: 3018
				RepeatWith,
				// Token: 0x04000BCB RID: 3019
				CustomProperties,
				// Token: 0x04000BCC RID: 3020
				DataElementName,
				// Token: 0x04000BCD RID: 3021
				DataElementOutput,
				// Token: 0x04000BCE RID: 3022
				KeepTogether,
				// Token: 0x04000BCF RID: 3023
				NoRowsMessage,
				// Token: 0x04000BD0 RID: 3024
				DataSetName,
				// Token: 0x04000BD1 RID: 3025
				PageBreak,
				// Token: 0x04000BD2 RID: 3026
				PageName,
				// Token: 0x04000BD3 RID: 3027
				Filters,
				// Token: 0x04000BD4 RID: 3028
				SortExpressions,
				// Token: 0x04000BD5 RID: 3029
				LinearGauges,
				// Token: 0x04000BD6 RID: 3030
				RadialGauges,
				// Token: 0x04000BD7 RID: 3031
				NumericIndicators,
				// Token: 0x04000BD8 RID: 3032
				StateIndicators,
				// Token: 0x04000BD9 RID: 3033
				GaugeImages,
				// Token: 0x04000BDA RID: 3034
				GaugeLabels,
				// Token: 0x04000BDB RID: 3035
				GaugeMember,
				// Token: 0x04000BDC RID: 3036
				AntiAliasing,
				// Token: 0x04000BDD RID: 3037
				AutoLayout,
				// Token: 0x04000BDE RID: 3038
				BackFrame,
				// Token: 0x04000BDF RID: 3039
				ShadowIntensity,
				// Token: 0x04000BE0 RID: 3040
				TextAntiAliasingQuality,
				// Token: 0x04000BE1 RID: 3041
				TopImage,
				// Token: 0x04000BE2 RID: 3042
				PropertyCount
			}
		}
	}
}
