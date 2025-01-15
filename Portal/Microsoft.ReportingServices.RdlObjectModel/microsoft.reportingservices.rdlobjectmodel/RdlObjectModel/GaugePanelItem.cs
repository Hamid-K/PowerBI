using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014C RID: 332
	public class GaugePanelItem : ReportObject, INamedObject
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001DF95 File Offset: 0x0001C195
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001DFB7 File Offset: 0x0001C1B7
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001DFCA File Offset: 0x0001C1CA
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001DFD9 File Offset: 0x0001C1D9
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0001DFE7 File Offset: 0x0001C1E7
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Top
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

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001DFFB File Offset: 0x0001C1FB
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0001E009 File Offset: 0x0001C209
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Left
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<double>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001E01D File Offset: 0x0001C21D
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0001E02B File Offset: 0x0001C22B
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Height
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

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0001E03F File Offset: 0x0001C23F
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0001E04D File Offset: 0x0001C24D
		[ReportExpressionDefaultValue(typeof(double), 0.0)]
		public ReportExpression<double> Width
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

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0001E061 File Offset: 0x0001C261
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x0001E06F File Offset: 0x0001C26F
		[ReportExpressionDefaultValue(typeof(int), 0)]
		public ReportExpression<int> ZIndex
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001E083 File Offset: 0x0001C283
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0001E091 File Offset: 0x0001C291
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
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

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001E0A5 File Offset: 0x0001C2A5
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0001E0B3 File Offset: 0x0001C2B3
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001E0C7 File Offset: 0x0001C2C7
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x0001E0DB File Offset: 0x0001C2DB
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x0001E0EB File Offset: 0x0001C2EB
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0001E0FF File Offset: 0x0001C2FF
		[DefaultValue("")]
		public string ParentItem
		{
			get
			{
				return (string)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001E10F File Offset: 0x0001C30F
		public GaugePanelItem()
		{
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001E117 File Offset: 0x0001C317
		internal GaugePanelItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0200037D RID: 893
		internal class Definition : DefinitionStore<GaugePanelItem, GaugePanelItem.Definition.Properties>
		{
			// Token: 0x06001820 RID: 6176 RVA: 0x0003B42B File Offset: 0x0003962B
			private Definition()
			{
			}

			// Token: 0x02000496 RID: 1174
			internal enum Properties
			{
				// Token: 0x04000BF3 RID: 3059
				Name,
				// Token: 0x04000BF4 RID: 3060
				Style,
				// Token: 0x04000BF5 RID: 3061
				Top,
				// Token: 0x04000BF6 RID: 3062
				Left,
				// Token: 0x04000BF7 RID: 3063
				Height,
				// Token: 0x04000BF8 RID: 3064
				Width,
				// Token: 0x04000BF9 RID: 3065
				ZIndex,
				// Token: 0x04000BFA RID: 3066
				Hidden,
				// Token: 0x04000BFB RID: 3067
				ToolTip,
				// Token: 0x04000BFC RID: 3068
				ActionInfo,
				// Token: 0x04000BFD RID: 3069
				ParentItem,
				// Token: 0x04000BFE RID: 3070
				PropertyCount
			}
		}
	}
}
