using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000190 RID: 400
	public abstract class MapSubItem : ReportObject
	{
		// Token: 0x06000CC7 RID: 3271 RVA: 0x00021841 File Offset: 0x0001FA41
		public MapSubItem()
		{
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00021849 File Offset: 0x0001FA49
		internal MapSubItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00021852 File Offset: 0x0001FA52
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x00021865 File Offset: 0x0001FA65
		public Style Style
		{
			get
			{
				return (Style)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00021874 File Offset: 0x0001FA74
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x00021887 File Offset: 0x0001FA87
		public MapLocation MapLocation
		{
			get
			{
				return (MapLocation)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00021896 File Offset: 0x0001FA96
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x000218A9 File Offset: 0x0001FAA9
		public MapSize MapSize
		{
			get
			{
				return (MapSize)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x000218B8 File Offset: 0x0001FAB8
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x000218C6 File Offset: 0x0001FAC6
		[ReportExpressionDefaultValue(typeof(ReportSize), "0in")]
		public ReportExpression<ReportSize> LeftMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x000218DA File Offset: 0x0001FADA
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x000218E8 File Offset: 0x0001FAE8
		[ReportExpressionDefaultValue(typeof(ReportSize), "0in")]
		public ReportExpression<ReportSize> RightMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x000218FC File Offset: 0x0001FAFC
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0002190A File Offset: 0x0001FB0A
		[ReportExpressionDefaultValue(typeof(ReportSize), "0in")]
		public ReportExpression<ReportSize> TopMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002191E File Offset: 0x0001FB1E
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0002192C File Offset: 0x0001FB2C
		[ReportExpressionDefaultValue(typeof(ReportSize), "0in")]
		public ReportExpression<ReportSize> BottomMargin
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportSize>>(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00021940 File Offset: 0x0001FB40
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0002194E File Offset: 0x0001FB4E
		[ReportExpressionDefaultValue(typeof(int), "0")]
		public ReportExpression<int> ZIndex
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<int>>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00021962 File Offset: 0x0001FB62
		public override void Initialize()
		{
			base.Initialize();
			this.ZIndex = 0;
		}

		// Token: 0x020003BD RID: 957
		internal class Definition : DefinitionStore<MapSubItem, MapSubItem.Definition.Properties>
		{
			// Token: 0x06001861 RID: 6241 RVA: 0x0003B689 File Offset: 0x00039889
			private Definition()
			{
			}

			// Token: 0x020004D5 RID: 1237
			internal enum Properties
			{
				// Token: 0x04000F31 RID: 3889
				Style,
				// Token: 0x04000F32 RID: 3890
				MapLocation,
				// Token: 0x04000F33 RID: 3891
				MapSize,
				// Token: 0x04000F34 RID: 3892
				LeftMargin,
				// Token: 0x04000F35 RID: 3893
				RightMargin,
				// Token: 0x04000F36 RID: 3894
				TopMargin,
				// Token: 0x04000F37 RID: 3895
				BottomMargin,
				// Token: 0x04000F38 RID: 3896
				ZIndex
			}
		}
	}
}
