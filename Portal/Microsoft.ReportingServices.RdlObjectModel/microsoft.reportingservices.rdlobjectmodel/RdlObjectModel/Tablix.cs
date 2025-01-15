using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C9 RID: 201
	public class Tablix : DataRegion
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0001D465 File Offset: 0x0001B665
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0001D46D File Offset: 0x0001B66D
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue(false)]
		public bool IsFragment
		{
			get
			{
				return this.m_isFragment;
			}
			set
			{
				this.m_isFragment = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0001D476 File Offset: 0x0001B676
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0001D48A File Offset: 0x0001B68A
		public TablixCorner TablixCorner
		{
			get
			{
				return (TablixCorner)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0001D49A File Offset: 0x0001B69A
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0001D4AE File Offset: 0x0001B6AE
		public TablixBody TablixBody
		{
			get
			{
				return (TablixBody)base.PropertyStore.GetObject(26);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(26, value);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001D4CC File Offset: 0x0001B6CC
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public TablixHierarchy TablixColumnHierarchy
		{
			get
			{
				return (TablixHierarchy)base.PropertyStore.GetObject(27);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0001D4FE File Offset: 0x0001B6FE
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001D512 File Offset: 0x0001B712
		public TablixHierarchy TablixRowHierarchy
		{
			get
			{
				return (TablixHierarchy)base.PropertyStore.GetObject(28);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(28, value);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001D530 File Offset: 0x0001B730
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0001D53F File Offset: 0x0001B73F
		[DefaultValue(LayoutDirections.LTR)]
		public LayoutDirections LayoutDirection
		{
			get
			{
				return (LayoutDirections)base.PropertyStore.GetInteger(29);
			}
			set
			{
				base.PropertyStore.SetInteger(29, (int)value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0001D54F File Offset: 0x0001B74F
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0001D55E File Offset: 0x0001B75E
		[DefaultValue(0)]
		[ValidValues(0, 2147483647)]
		public int GroupsBeforeRowHeaders
		{
			get
			{
				return base.PropertyStore.GetInteger(30);
			}
			set
			{
				((IntProperty)DefinitionStore<Tablix, Tablix.Definition.Properties>.GetProperty(30)).Validate(this, value);
				base.PropertyStore.SetInteger(30, value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001D581 File Offset: 0x0001B781
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0001D590 File Offset: 0x0001B790
		[DefaultValue(false)]
		public bool RepeatColumnHeaders
		{
			get
			{
				return base.PropertyStore.GetBoolean(31);
			}
			set
			{
				base.PropertyStore.SetBoolean(31, value);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0001D5AF File Offset: 0x0001B7AF
		[DefaultValue(false)]
		public bool RepeatRowHeaders
		{
			get
			{
				return base.PropertyStore.GetBoolean(32);
			}
			set
			{
				base.PropertyStore.SetBoolean(32, value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001D5BF File Offset: 0x0001B7BF
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x0001D5CE File Offset: 0x0001B7CE
		[DefaultValue(false)]
		public bool FixedColumnHeaders
		{
			get
			{
				return base.PropertyStore.GetBoolean(33);
			}
			set
			{
				base.PropertyStore.SetBoolean(33, value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001D5DE File Offset: 0x0001B7DE
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0001D5ED File Offset: 0x0001B7ED
		[DefaultValue(false)]
		public bool FixedRowHeaders
		{
			get
			{
				return base.PropertyStore.GetBoolean(34);
			}
			set
			{
				base.PropertyStore.SetBoolean(34, value);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001D5FD File Offset: 0x0001B7FD
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0001D60C File Offset: 0x0001B80C
		[DefaultValue(false)]
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return base.PropertyStore.GetBoolean(35);
			}
			set
			{
				base.PropertyStore.SetBoolean(35, value);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001D61C File Offset: 0x0001B81C
		public Tablix()
		{
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0001D624 File Offset: 0x0001B824
		internal Tablix(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0001D62D File Offset: 0x0001B82D
		public override void Initialize()
		{
			base.Initialize();
			this.TablixBody = new TablixBody();
			this.TablixColumnHierarchy = new TablixHierarchy();
			this.TablixRowHierarchy = new TablixHierarchy();
		}

		// Token: 0x0400017D RID: 381
		private bool m_isFragment;

		// Token: 0x02000373 RID: 883
		internal new class Definition : DefinitionStore<Tablix, Tablix.Definition.Properties>
		{
			// Token: 0x06001808 RID: 6152 RVA: 0x0003B25C File Offset: 0x0003945C
			private Definition()
			{
			}

			// Token: 0x0200048E RID: 1166
			internal enum Properties
			{
				// Token: 0x04000B6D RID: 2925
				Style,
				// Token: 0x04000B6E RID: 2926
				Name,
				// Token: 0x04000B6F RID: 2927
				ActionInfo,
				// Token: 0x04000B70 RID: 2928
				Top,
				// Token: 0x04000B71 RID: 2929
				Left,
				// Token: 0x04000B72 RID: 2930
				Height,
				// Token: 0x04000B73 RID: 2931
				Width,
				// Token: 0x04000B74 RID: 2932
				ZIndex,
				// Token: 0x04000B75 RID: 2933
				Visibility,
				// Token: 0x04000B76 RID: 2934
				ToolTip,
				// Token: 0x04000B77 RID: 2935
				ToolTipLocID,
				// Token: 0x04000B78 RID: 2936
				DocumentMapLabel,
				// Token: 0x04000B79 RID: 2937
				DocumentMapLabelLocID,
				// Token: 0x04000B7A RID: 2938
				Bookmark,
				// Token: 0x04000B7B RID: 2939
				RepeatWith,
				// Token: 0x04000B7C RID: 2940
				CustomProperties,
				// Token: 0x04000B7D RID: 2941
				DataElementName,
				// Token: 0x04000B7E RID: 2942
				DataElementOutput,
				// Token: 0x04000B7F RID: 2943
				KeepTogether,
				// Token: 0x04000B80 RID: 2944
				NoRowsMessage,
				// Token: 0x04000B81 RID: 2945
				DataSetName,
				// Token: 0x04000B82 RID: 2946
				PageBreak,
				// Token: 0x04000B83 RID: 2947
				PageName,
				// Token: 0x04000B84 RID: 2948
				Filters,
				// Token: 0x04000B85 RID: 2949
				SortExpressions,
				// Token: 0x04000B86 RID: 2950
				TablixCorner,
				// Token: 0x04000B87 RID: 2951
				TablixBody,
				// Token: 0x04000B88 RID: 2952
				TablixColumnHierarchy,
				// Token: 0x04000B89 RID: 2953
				TablixRowHierarchy,
				// Token: 0x04000B8A RID: 2954
				LayoutDirection,
				// Token: 0x04000B8B RID: 2955
				GroupsBeforeRowHeaders,
				// Token: 0x04000B8C RID: 2956
				RepeatColumnHeaders,
				// Token: 0x04000B8D RID: 2957
				RepeatRowHeaders,
				// Token: 0x04000B8E RID: 2958
				FixedColumnHeaders,
				// Token: 0x04000B8F RID: 2959
				FixedRowHeaders,
				// Token: 0x04000B90 RID: 2960
				OmitBorderOnPageBreak,
				// Token: 0x04000B91 RID: 2961
				PropertyCount
			}
		}
	}
}
